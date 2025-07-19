using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages card game logic including deck management, turn order, and trick-taking rules
/// Implements standard 52-card deck with turn-based trick-taking mechanics
/// PRD F-2: Turn-based Trick-Taking Core with 10s timer and auto-forfeit
/// </summary>
public partial class CardManager : Node
{
    // Game configuration
    [Export]
    public float TurnDuration = 10.0f; // 10 second turn timer from PRD

    [Export]
    public int CardsPerHand = 13; // Standard trick-taking game

    [Export]
    public int TargetScore = 100; // Points to win the game

    // Game state
    public bool GameInProgress { get; private set; } = false;
    public int CurrentPlayerTurn { get; private set; } = 0;
    public List<int> PlayerOrder { get; private set; } = new();
    public int CurrentDealer { get; private set; } = 0;
    public int CurrentTrickLeader { get; private set; } = 0;

    // Card game data
    public List<Card> Deck { get; private set; } = new();
    public Dictionary<int, List<Card>> PlayerHands { get; private set; } = new();
    public Dictionary<int, int> PlayerScores { get; private set; } = new();
    public List<CardPlay> CurrentTrick { get; private set; } = new();
    public int TricksPlayed { get; private set; } = 0;

    // 🔥 NEW: Track pending card plays to prevent duplicates
    private readonly HashSet<string> pendingCardPlays = new();

    // Turn timer
    private Godot.Timer turnTimer;
    private bool timerActive = false;
    private float networkTurnTimeRemaining = 0.0f; // For clients to track time from host

    // Events
    [Signal]
    public delegate void TurnStartedEventHandler(int playerId);

    [Signal]
    public delegate void TurnEndedEventHandler(int playerId);

    [Signal]
    public delegate void TurnTimerExpiredEventHandler(int playerId);

    [Signal]
    public delegate void TurnTimerUpdatedEventHandler(float timeRemaining);

    [Signal]
    public delegate void CardPlayedEventHandler(int playerId, string cardString);

    [Signal]
    public delegate void TrickCompletedEventHandler(int winnerId);

    [Signal]
    public delegate void HandCompletedEventHandler();

    [Signal]
    public delegate void HandDealtEventHandler();

    [Signal]
    public delegate void GameCompletedEventHandler(int winnerId);

    public override void _Ready()
    {
        // Initialize turn timer
        turnTimer = new Godot.Timer();
        turnTimer.WaitTime = TurnDuration;
        turnTimer.OneShot = true;
        turnTimer.Timeout += OnTurnTimerExpired;
        AddChild(turnTimer);

        // Initialize deck
        InitializeDeck();

        // 🔥 NEW: Connect to MatchManager for Nakama card play synchronization
        var matchManager = MatchManager.Instance;
        if (matchManager != null)
        {
            matchManager.CardPlayReceived += OnNakamaCardPlayReceived;
            matchManager.TurnChangeReceived += OnNakamaTurnChangeReceived;
            matchManager.CardsDealt += OnNakamaCardsDealt;
            GD.Print("CardManager: Connected to MatchManager for card play, turn, and card dealing synchronization");
        }
    }

    public override void _Process(double delta)
    {
        // Host: Send timer updates to clients every 0.1 seconds
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;

        if (timerActive && networkManager != null && networkManager.IsConnected && networkManager.IsHost)
        {
            float timeRemaining = (float)(turnTimer?.TimeLeft ?? 0.0f);

            // Send timer update to clients every frame (throttled by network automatically)
            Rpc(MethodName.NetworkTimerUpdate, timeRemaining);
            EmitSignal(SignalName.TurnTimerUpdated, timeRemaining);
        }
    }

    /// <summary>
    /// Initialize standard 52-card deck
    /// </summary>
    private void InitializeDeck()
    {
        Deck.Clear();

        // Create cards for each suit and rank
        foreach (Suit suit in Enum.GetValues<Suit>())
        {
            foreach (Rank rank in Enum.GetValues<Rank>())
            {
                Deck.Add(new Card(suit, rank));
            }
        }
    }

    // Shuffle seed for synchronization across instances
    private int currentShuffleSeed = 12345; // Default seed

    /// <summary>
    /// Shuffle the deck using Fisher-Yates algorithm
    /// </summary>
    private void ShuffleDeck()
    {
        // 🔥 CRITICAL: For Nakama games, use synchronized seed from MatchManager
        var matchManager = MatchManager.Instance;
        if (matchManager?.HasActiveMatch == true)
        {
            GD.Print($"CardManager: Shuffling deck with Nakama-synchronized seed: {currentShuffleSeed}");
        }
        else
        {
            GD.Print($"CardManager: Shuffling deck with seed: {currentShuffleSeed}");
        }

        var random = new Random(currentShuffleSeed);
        for (int i = Deck.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (Deck[i], Deck[j]) = (Deck[j], Deck[i]);
        }
    }

    /// <summary>
    /// Set shuffle seed for synchronized dealing across instances
    /// </summary>
    /// <param name="seed">Shuffle seed</param>
    public void SetShuffleSeed(int seed)
    {
        currentShuffleSeed = seed;
        GD.Print($"CardManager: Shuffle seed set to: {seed}");
    }

    /// <summary>
    /// Start a new card game with given players (host-authoritative)
    /// </summary>
    /// <param name="playerIds">List of player IDs in turn order</param>
    public void StartGame(List<int> playerIds)
    {
        GD.Print($"CardManager: Starting game with {playerIds.Count} players: [{string.Join(", ", playerIds)}]");

        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;
        var matchManager = MatchManager.Instance;

        // 🔥 CRITICAL: Check if this is a Nakama match game
        bool isNakamaGame = (matchManager?.HasActiveMatch == true);

        if (isNakamaGame)
        {
            GD.Print("CardManager: Detected Nakama game - using local execution with Nakama sync");
            // For Nakama games, run locally on each instance and sync via Nakama messages
            ExecuteGameStart(playerIds);
            return;
        }

        // Traditional ENet multiplayer logic
        if (networkManager != null && networkManager.IsConnected)
        {
            if (networkManager.IsHost)
            {
                // Convert to array for RPC
                int[] playerIdArray = playerIds.ToArray();

                // CRITICAL FIX: Host starts game locally AND notifies clients
                try
                {
                    // Notify clients that game is starting (they will wait for card sync)
                    Rpc(MethodName.NetworkStartGame, playerIdArray);

                    // Host executes game start locally (deals cards and syncs to clients)
                    GD.Print("CardManager: HOST executing game start locally");
                    ExecuteGameStart(playerIds);
                }
                catch (Exception ex)
                {
                    GD.PrintErr($"CardManager: Failed to send NetworkStartGame RPC: {ex.Message}");

                    // Fallback: start game locally if RPC fails
                    ExecuteGameStart(playerIds);
                }
            }
            else
            {
                GD.PrintErr("CardManager: ERROR - Client should never call StartGame directly!");
                return;
            }
        }
        else
        {
            // Single-player or local game
            ExecuteGameStart(playerIds);
        }
    }

    /// <summary>
    /// RPC method to notify clients that the game is starting (CLIENT ONLY)
    /// Clients should NOT call ExecuteGameStart - they wait for card sync from host
    /// </summary>
    /// <param name="playerIds">Array of player IDs</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    public void NetworkStartGame(int[] playerIds)
    {
        GD.Print("=== CRITICAL DEBUG: NetworkStartGame RPC RECEIVED ===");
        GD.Print($"CardManager: CLIENT received NetworkStartGame RPC with {playerIds.Length} players");
        GD.Print($"CardManager: Sender ID: {GetTree().GetMultiplayer().GetRemoteSenderId()}");
        GD.Print($"CardManager: My ID: {GetTree().GetMultiplayer().GetUniqueId()}");

        var networkManager = GameManager.Instance?.NetworkManager;
        GD.Print($"CardManager: NetworkManager status - IsHost: {networkManager?.IsHost}, IsClient: {networkManager?.IsClient}, IsConnected: {networkManager?.IsConnected}");

        // CRITICAL FIX: Only clients should receive this RPC
        if (networkManager == null || networkManager.IsHost)
        {
            GD.PrintErr("CardManager: Host received NetworkStartGame RPC - this should not happen!");
            return;
        }

        GD.Print($"CardManager: CLIENT received player IDs: [{string.Join(", ", playerIds)}]");

        // Validate players exist in ConnectedPlayers
        if (GameManager.Instance?.ConnectedPlayers != null)
        {
            var currentPlayers = GameManager.Instance.ConnectedPlayers.Keys.ToArray();
            GD.Print($"CardManager: CLIENT ConnectedPlayers: [{string.Join(", ", currentPlayers)}]");

            bool allPlayersExist = true;
            foreach (int playerId in playerIds)
            {
                if (!GameManager.Instance.ConnectedPlayers.ContainsKey(playerId))
                {
                    GD.PrintErr($"CardManager: CLIENT missing player {playerId} in ConnectedPlayers!");
                    allPlayersExist = false;
                }
            }

            if (!allPlayersExist)
            {
                GD.PrintErr("CardManager: CLIENT player sync failed - not all players exist!");
                return;
            }
        }

        // CRITICAL FIX: Clients set up basic game state but do NOT deal cards
        // They wait for NetworkSyncDealtHands from host
        GD.Print("=== CLIENT INITIALIZING GAME STATE (NO CARD DEALING) ===");

        PlayerOrder = new List<int>(playerIds);
        CurrentPlayerTurn = 0;
        CurrentDealer = 0;
        CurrentTrickLeader = 0;
        GameInProgress = true;
        TricksPlayed = 0;

        // Initialize empty hands and scores (cards will come from host)
        PlayerHands.Clear();
        PlayerScores.Clear();
        CurrentTrick.Clear();

        foreach (int playerId in playerIds)
        {
            PlayerHands[playerId] = new List<Card>();
            PlayerScores[playerId] = 0;
        }

        GD.Print($"CardManager: CLIENT game state initialized, waiting for host to deal cards...");
        GD.Print($"CardManager: CLIENT PlayerOrder: [{string.Join(", ", PlayerOrder)}]");
        GD.Print("=== END NetworkStartGame RPC ===");
    }

    /// <summary>
    /// Execute the actual game start logic
    /// </summary>
    /// <param name="playerIds">List of player IDs in turn order</param>
    private void ExecuteGameStart(List<int> playerIds)
    {
        if (playerIds.Count < 2 || playerIds.Count > 4)
        {
            GD.PrintErr("CardManager: Invalid player count. Need 2-4 players.");
            return;
        }

        GD.Print($"CardManager: ExecuteGameStart called with {playerIds.Count} players: [{string.Join(", ", playerIds)}]");

        // CRITICAL FIX: No longer create AI players here - they are synchronized via NetworkSyncPlayers RPC
        var gameManager = GameManager.Instance;
        if (gameManager != null)
        {
            GD.Print($"CardManager: Validating players exist in GameManager ConnectedPlayers");
            GD.Print($"CardManager: GameManager.ConnectedPlayers: [{string.Join(", ", gameManager.ConnectedPlayers.Keys)}]");

            // Validate that all players exist in ConnectedPlayers (they should be synchronized by now)
            foreach (int playerId in playerIds)
            {
                if (!gameManager.ConnectedPlayers.ContainsKey(playerId))
                {
                    GD.PrintErr($"CardManager: Player {playerId} not found in ConnectedPlayers! Game sync may have failed.");
                }
                else
                {
                    var playerData = gameManager.ConnectedPlayers[playerId];
                    GD.Print($"CardManager: Player {playerId} validated: {playerData.PlayerName}");
                }
            }
        }

        // Initialize game state
        PlayerOrder = new List<int>(playerIds);
        CurrentPlayerTurn = 0;
        CurrentDealer = 0;
        CurrentTrickLeader = 0;
        GameInProgress = true;
        TricksPlayed = 0;

        GD.Print($"CardManager: Game initialized with PlayerOrder: [{string.Join(", ", PlayerOrder)}]");
        GD.Print($"CardManager: CurrentPlayerTurn index: {CurrentPlayerTurn}, Player ID: {PlayerOrder[CurrentPlayerTurn]}");
        GD.Print($"CardManager: Host/Client status - NetworkManager.IsHost: {GameManager.Instance?.NetworkManager?.IsHost}, IsConnected: {GameManager.Instance?.NetworkManager?.IsConnected}");

        // Initialize player hands and scores
        PlayerHands.Clear();
        PlayerScores.Clear();

        foreach (int playerId in playerIds)
        {
            PlayerHands[playerId] = new List<Card>();
            PlayerScores[playerId] = 0;
        }

        GD.Print($"CardManager: Initialized hands and scores for {playerIds.Count} players");

        // Deal first hand
        DealNewHand();
    }

    /// <summary>
    /// Deal a new hand of cards to all players (HOST ONLY - syncs to clients)
    /// </summary>
    private void DealNewHand()
    {
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;
        var matchManager = MatchManager.Instance;

        // 🔥 NEW: For Nakama games, only match owner deals cards
        if (matchManager?.HasActiveMatch == true)
        {
            if (matchManager.IsLocalPlayerMatchOwner())
            {
                GD.Print("CardManager: NAKAMA MATCH OWNER - dealing cards and syncing via Nakama");
                HostDealAndSyncCards();
            }
            else
            {
                GD.Print("CardManager: NAKAMA CLIENT - waiting for match owner to deal cards");
                // Non-match-owner instances wait for card synchronization via Nakama
                return;
            }
            return;
        }

        // CRITICAL FIX: Only HOST deals cards, then syncs to clients
        if (networkManager != null && networkManager.IsConnected)
        {
            if (networkManager.IsHost)
            {
                // Host: Deal cards and sync to clients
                HostDealAndSyncCards();
            }
            else
            {
                // Client: Wait for host to sync cards via RPC
                GD.Print("CardManager: Client waiting for host to deal and sync cards...");
                return;
            }
        }
        else
        {
            // Single-player: Deal cards locally
            HostDealAndSyncCards();
        }
    }

    /// <summary>
    /// Host deals cards and synchronizes them to all clients
    /// </summary>
    private void HostDealAndSyncCards()
    {
        GD.Print("CardManager: HOST dealing cards and syncing to clients");
        GD.Print($"CardManager: HOST using PlayerOrder: [{string.Join(", ", PlayerOrder)}]");

        // Reset deck and shuffle
        InitializeDeck();
        ShuffleDeck();

        // Clear current trick
        CurrentTrick.Clear();
        TricksPlayed = 0;

        // Deal cards to each player
        foreach (int playerId in PlayerOrder)
        {
            PlayerHands[playerId].Clear();

            for (int i = 0; i < CardsPerHand; i++)
            {
                if (Deck.Count > 0)
                {
                    Card card = Deck[0];
                    Deck.RemoveAt(0);
                    PlayerHands[playerId].Add(card);
                }
            }

            GD.Print($"CardManager: HOST dealt 13 cards to player {playerId}");

            // For Nakama games, sync the dealt cards to all other players
            var matchManager = MatchManager.Instance;
            if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
            {
                GD.Print("CardManager: NAKAMA MATCH OWNER - syncing dealt cards to all players");

                // Convert all player hands to string format for synchronization
                var handsToSync = new Dictionary<int, List<string>>();
                foreach (var kvp in PlayerHands)
                {
                    int playerHandId = kvp.Key;
                    var hand = kvp.Value;
                    handsToSync[playerHandId] = hand.Select(card => card.ToString()).ToList();
                }

                // Send to all players (including self for consistency)
                _ = matchManager.SendCardsDealt(handsToSync);
                GD.Print($"CardManager: NAKAMA MATCH OWNER - sent dealt cards for {handsToSync.Count} players");
            }

            // Emit signal for local UI update
            EmitSignal(SignalName.HandDealt, playerId);
        }

        // CRITICAL FIX: Sync dealt hands to all clients
        SyncDealtHandsToClients();

        // Notify UI that cards have been dealt
        EmitSignal(SignalName.HandDealt);

        // Start first trick
        CurrentTrickLeader = GetNextDealer();
        CurrentPlayerTurn = CurrentTrickLeader;

        GD.Print($"CardManager: HOST starting turn - TrickLeader: {CurrentTrickLeader}, CurrentTurn: {CurrentPlayerTurn}, Player: {PlayerOrder[CurrentPlayerTurn]}");

        StartTurn();
    }

    /// <summary>
    /// Synchronize dealt hands from host to all clients
    /// </summary>
    private void SyncDealtHandsToClients()
    {
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;

        if (networkManager == null || !networkManager.IsConnected || !networkManager.IsHost)
        {
            return;
        }

        // CRITICAL FIX: Use PlayerOrder for consistent sync, not PlayerHands.Keys
        var playerIds = new List<int>();
        var playerCardCounts = new List<int>();
        var allCardSuits = new List<int>();
        var allCardRanks = new List<int>();

        GD.Print($"CardManager: HOST syncing cards using PlayerOrder: [{string.Join(", ", PlayerOrder)}]");

        foreach (var playerId in PlayerOrder) // Use PlayerOrder instead of PlayerHands.Keys
        {
            var hand = PlayerHands[playerId];

            playerIds.Add(playerId);
            playerCardCounts.Add(hand.Count);

            // Flatten all cards into single arrays
            for (int i = 0; i < hand.Count; i++)
            {
                allCardSuits.Add((int)hand[i].Suit);
                allCardRanks.Add((int)hand[i].Rank);
            }

            GD.Print($"CardManager: HOST syncing {hand.Count} cards for player {playerId}");
        }

        GD.Print($"CardManager: HOST sending card sync - {playerIds.Count} players, {allCardSuits.Count} total cards");

        // Send to all clients with flattened arrays
        Rpc("NetworkSyncDealtHands",
            playerIds.ToArray(),
            playerCardCounts.ToArray(),
            allCardSuits.ToArray(),
            allCardRanks.ToArray(),
            CurrentTrickLeader,
            CurrentPlayerTurn);
    }

    /// <summary>
    /// RPC method to receive dealt hands from host
    /// </summary>
    /// <param name="playerIds">Array of player IDs</param>
    /// <param name="playerCardCounts">Array of card counts for each player</param>
    /// <param name="allCardSuits">Flattened array of all card suits</param>
    /// <param name="allCardRanks">Flattened array of all card ranks</param>
    /// <param name="trickLeader">Current trick leader</param>
    /// <param name="currentTurn">Current player turn index</param>
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]
    public void NetworkSyncDealtHands(int[] playerIds, int[] playerCardCounts, int[] allCardSuits, int[] allCardRanks, int trickLeader, int currentTurn)
    {
        GD.Print($"CardManager: CLIENT received dealt hands from host - {playerIds.Length} players");

        // CRITICAL DEBUG: Check current PlayerOrder before sync
        GD.Print($"CardManager: CLIENT PlayerOrder BEFORE sync: [{string.Join(", ", PlayerOrder)}]");
        GD.Print($"CardManager: CLIENT received PlayerOrder from host: [{string.Join(", ", playerIds)}]");

        bool shouldUpdateOrder = false;
        string updateReason = "";

        // CRITICAL FIX: Ensure PlayerOrder is synchronized first
        if (PlayerOrder.Count != playerIds.Length)
        {
            shouldUpdateOrder = true;
            updateReason = $"count mismatch - {PlayerOrder.Count} vs {playerIds.Length}";
        }
        else
        {
            // Check if the order is different even if the count is the same
            for (int i = 0; i < PlayerOrder.Count; i++)
            {
                if (PlayerOrder[i] != playerIds[i])
                {
                    shouldUpdateOrder = true;
                    updateReason = $"order differs at index {i} - {PlayerOrder[i]} vs {playerIds[i]}";
                    break;
                }
            }
        }

        if (shouldUpdateOrder)
        {
            GD.Print($"CardManager: CLIENT updating PlayerOrder - {updateReason}");
            PlayerOrder = new List<int>(playerIds);
        }
        else
        {
            GD.Print($"CardManager: CLIENT PlayerOrder already matches host");
        }

        GD.Print($"CardManager: CLIENT PlayerOrder AFTER sync: [{string.Join(", ", PlayerOrder)}]");

        // Clear current hands
        PlayerHands.Clear();
        CurrentTrick.Clear();
        TricksPlayed = 0;

        // Reconstruct hands from flattened data
        int cardIndex = 0;
        for (int p = 0; p < playerIds.Length; p++)
        {
            int playerId = playerIds[p];
            int cardCount = playerCardCounts[p];

            var hand = new List<Card>();
            for (int c = 0; c < cardCount; c++)
            {
                var suit = (Suit)allCardSuits[cardIndex];
                var rank = (Rank)allCardRanks[cardIndex];
                var card = new Card(suit, rank);
                hand.Add(card);
                cardIndex++;
            }

            PlayerHands[playerId] = hand;
            GD.Print($"CardManager: CLIENT synchronized {hand.Count} cards for player {playerId}");
        }

        // Sync game state
        CurrentTrickLeader = trickLeader;
        CurrentPlayerTurn = currentTurn;

        GD.Print($"CardManager: CLIENT game state synchronized - Leader: {CurrentTrickLeader}, Turn: {CurrentPlayerTurn}, Current Player: {(PlayerOrder.Count > CurrentPlayerTurn ? PlayerOrder[CurrentPlayerTurn] : -1)}");

        // Notify UI that cards have been dealt
        EmitSignal(SignalName.HandDealt);

        // Start turn (but don't start timer - host manages that)
        if (PlayerOrder.Count > CurrentPlayerTurn)
        {
            EmitSignal(SignalName.TurnStarted, PlayerOrder[CurrentPlayerTurn]);
        }
    }

    /// <summary>
    /// Start a player's turn (host-authoritative for networked games)
    /// </summary>
    private void StartTurn()
    {
        if (!GameInProgress || CurrentPlayerTurn >= PlayerOrder.Count)
        {
            GD.PrintErr($"CardManager: Cannot start turn - GameInProgress: {GameInProgress}, CurrentPlayerTurn: {CurrentPlayerTurn}, PlayerOrder.Count: {PlayerOrder.Count}");
            return;
        }

        int currentPlayerId = PlayerOrder[CurrentPlayerTurn];

        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;
        var matchManager = MatchManager.Instance;

        // 🔥 NEW: For Nakama games, only match owner manages turns
        if (matchManager?.HasActiveMatch == true)
        {
            if (!matchManager.IsLocalPlayerMatchOwner())
            {
                GD.Print($"CardManager: NAKAMA CLIENT - not managing turns, waiting for match owner");
                EmitSignal(SignalName.TurnStarted, currentPlayerId);
                return;
            }
            GD.Print($"CardManager: NAKAMA MATCH OWNER - managing turn for player {currentPlayerId}");
        }

        // If networked, only host manages turn timing
        if (networkManager != null && networkManager.IsConnected && !networkManager.IsHost)
        {
            EmitSignal(SignalName.TurnStarted, currentPlayerId);
            return;
        }

        // CRITICAL FIX: Ensure timer is stopped before starting new turn
        if (turnTimer != null && timerActive)
        {
            GD.Print($"CardManager: Warning - timer was still active when starting new turn for player {currentPlayerId}, stopping it");
            turnTimer.Stop();
            timerActive = false;
        }

        // Check if this is an AI player and auto-play if so
        if (gameManager != null)
        {
            var playerData = gameManager.GetPlayer(currentPlayerId);
            if (playerData != null && playerData.PlayerName.StartsWith("AI_"))
            {
                GD.Print($"CardManager: AI turn detected for {playerData.PlayerName} (ID: {currentPlayerId})");
                GD.Print($"CardManager: NetworkManager status - IsHost: {networkManager?.IsHost}, IsConnected: {networkManager?.IsConnected}");

                // Auto-play after consistent delay for synchronization
                GetTree().CreateTimer(0.5f).Timeout += () =>
                {
                    GD.Print($"CardManager: Starting AutoPlayAITurn for {playerData.PlayerName} (ID: {currentPlayerId})");
                    AutoPlayAITurn(currentPlayerId);
                };

                // Broadcast turn start to clients
                if (networkManager != null && networkManager.IsConnected)
                {
                    GD.Print($"CardManager: Broadcasting AI turn start for {playerData.PlayerName} to clients");
                    Rpc(MethodName.NetworkTurnStarted, currentPlayerId);
                }

                EmitSignal(SignalName.TurnStarted, currentPlayerId);
                return;
            }
            else if (playerData != null)
            {
                GD.Print($"CardManager: Human player turn for {playerData.PlayerName} (ID: {currentPlayerId})");
            }
            else
            {
                GD.PrintErr($"CardManager: No player data found for ID {currentPlayerId}!");
            }
        }

        // Start turn timer for human players (host only)
        if (turnTimer != null)
        {
            GD.Print($"CardManager: Starting turn timer for player {currentPlayerId}");
            timerActive = true;
            turnTimer.Start();
        }

        // Broadcast turn start to clients
        if (networkManager != null && networkManager.IsConnected)
        {
            Rpc(MethodName.NetworkTurnStarted, currentPlayerId);
        }

        EmitSignal(SignalName.TurnStarted, currentPlayerId);
    }

    /// <summary>
    /// RPC method to synchronize turn starts across network
    /// </summary>
    /// <param name="playerId">Player whose turn is starting</param>
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]
    public void NetworkTurnStarted(int playerId)
    {
        EmitSignal(SignalName.TurnStarted, playerId);
    }

    /// <summary>
    /// RPC method to synchronize timer updates from host to clients
    /// </summary>
    /// <param name="timeRemaining">Time remaining in seconds</param>
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]
    public void NetworkTimerUpdate(float timeRemaining)
    {
        networkTurnTimeRemaining = timeRemaining;
        EmitSignal(SignalName.TurnTimerUpdated, timeRemaining);
    }

    /// <summary>
    /// End the current player's turn and move to next player (HOST ONLY)
    /// </summary>
    private void EndTurn()
    {
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;
        var matchManager = MatchManager.Instance;

        // 🔥 NEW: For Nakama games, only match owner manages turn endings
        if (matchManager?.HasActiveMatch == true)
        {
            if (!matchManager.IsLocalPlayerMatchOwner())
            {
                GD.Print($"CardManager: NAKAMA CLIENT - not managing turn endings, waiting for match owner");
                return;
            }
            GD.Print($"CardManager: NAKAMA MATCH OWNER - managing turn ending");
        }

        // CRITICAL FIX: Only host can manage turn progression
        if (networkManager != null && networkManager.IsConnected && !networkManager.IsHost)
        {
            GD.PrintErr("CardManager: CLIENT attempted to call EndTurn - only host should manage turns!");
            return;
        }

        int currentPlayerId = PlayerOrder[CurrentPlayerTurn];
        GD.Print($"CardManager: HOST EndTurn called for player {currentPlayerId}");

        // CRITICAL FIX: Always stop turn timer and reset state
        if (turnTimer != null)
        {
            if (timerActive)
            {
                GD.Print($"CardManager: HOST stopping timer for player {currentPlayerId}");
                turnTimer.Stop();
            }
            timerActive = false; // Always reset timer state
        }

        EmitSignal(SignalName.TurnEnded, currentPlayerId);

        // Move to next player
        int previousTurn = CurrentPlayerTurn;
        CurrentPlayerTurn = (CurrentPlayerTurn + 1) % PlayerOrder.Count;
        GD.Print($"CardManager: HOST moving to next player - new CurrentPlayerTurn: {CurrentPlayerTurn} (Player {PlayerOrder[CurrentPlayerTurn]})");

        // 🔥 NEW: For Nakama games, sync turn changes to all instances
        if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
        {
            _ = matchManager.SendTurnChange(CurrentPlayerTurn, TricksPlayed);
        }

        // CRITICAL FIX: Sync turn change to all clients
        if (networkManager != null && networkManager.IsConnected)
        {
            Rpc(MethodName.NetworkSyncTurnChange, previousTurn, CurrentPlayerTurn, CurrentTrick.Count);
        }

        // Check if trick is complete
        if (CurrentTrick.Count == PlayerOrder.Count)
        {
            GD.Print($"CardManager: HOST trick complete with {CurrentTrick.Count} cards");
            CompleteTrick();
        }
        else
        {
            GD.Print($"CardManager: HOST trick continues - {CurrentTrick.Count}/{PlayerOrder.Count} cards played");
            StartTurn();
        }
    }

    /// <summary>
    /// RPC method to synchronize turn changes from host to clients
    /// </summary>
    /// <param name="previousTurn">Previous turn index</param>
    /// <param name="newTurn">New turn index</param>
    /// <param name="trickCardCount">Number of cards in current trick</param>
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]
    public void NetworkSyncTurnChange(int previousTurn, int newTurn, int trickCardCount)
    {
        GD.Print($"CardManager: CLIENT received turn change - {previousTurn} -> {newTurn}, trick cards: {trickCardCount}");

        // Update turn state on client
        CurrentPlayerTurn = newTurn;

        // Emit turn ended signal for previous player
        if (previousTurn < PlayerOrder.Count)
        {
            EmitSignal(SignalName.TurnEnded, PlayerOrder[previousTurn]);
        }

        // Emit turn started signal for new player (if trick continues)
        if (trickCardCount < PlayerOrder.Count && newTurn < PlayerOrder.Count)
        {
            EmitSignal(SignalName.TurnStarted, PlayerOrder[newTurn]);
            GD.Print($"CardManager: CLIENT turn started for player {PlayerOrder[newTurn]}");
        }
    }

    /// <summary>
    /// Attempt to play a card for a player
    /// </summary>
    /// <param name="playerId">Player attempting to play</param>
    /// <param name="card">Card to play</param>
    /// <returns>True if card was successfully played</returns>
    public bool PlayCard(int playerId, Card card)
    {
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;
        var matchManager = MatchManager.Instance;

        // 🔥 NEW: For Nakama games, send to MatchManager for synchronization
        if (matchManager?.HasActiveMatch == true)
        {
            GD.Print($"CardManager: Nakama game - sending card play to MatchManager - Player {playerId}: {card}");

            // Validate locally first to provide immediate feedback
            if (PlayerOrder[CurrentPlayerTurn] != playerId)
            {
                GD.PrintErr($"CardManager: Not player {playerId}'s turn");
                return false;
            }

            if (!PlayerHands[playerId].Contains(card))
            {
                GD.PrintErr($"CardManager: Player {playerId} doesn't have card {card}");
                return false;
            }

            if (!IsValidCardPlay(playerId, card))
            {
                GD.PrintErr($"CardManager: Invalid card play: {card}");
                return false;
            }

            // 🔥 NEW: Prevent duplicate card plays
            string playKey = $"{playerId}_{card}";
            if (pendingCardPlays.Contains(playKey))
            {
                GD.Print($"CardManager: Duplicate card play prevented - {playKey} already pending");
                return false;
            }
            pendingCardPlays.Add(playKey);
            GD.Print($"CardManager: Added pending card play: {playKey}");

            // 🔥 NEW: Provide immediate visual feedback by removing card from UI
            // The card will be added back if the play fails during Nakama sync
            EmitSignal(SignalName.CardPlayed, playerId, card.ToString());
            GD.Print($"CardManager: Immediate visual feedback - card removed from UI");

            // 🔥 FIXED: Send to all instances via Nakama but DON'T execute locally
            // All execution will happen when the message is received back from Nakama
            _ = matchManager.SendCardPlay(playerId, card.Suit.ToString(), card.Rank.ToString());

            GD.Print($"CardManager: Card play sent to Nakama - will execute when received back");
            return true;
        }

        // Traditional ENet multiplayer logic
        if (networkManager != null && networkManager.IsConnected)
        {
            bool isLocalPlayer = gameManager.LocalPlayer?.PlayerId == playerId;

            if (isLocalPlayer)
            {
                if (networkManager.IsHost)
                {
                    // HOST playing locally: execute directly (RPC will be sent by ExecuteCardPlay)
                    GD.Print($"CardManager: HOST playing card locally - Player {playerId}: {card}");
                    return ExecuteCardPlay(playerId, card);
                }
                else
                {
                    // CLIENT playing locally: send to host for validation
                    GD.Print($"CardManager: CLIENT sending card play to host - Player {playerId}: {card}");
                    Rpc(MethodName.NetworkPlayCard, playerId, (int)card.Suit, (int)card.Rank);
                    return true; // Host will validate and broadcast result via NetworkCardPlayResult
                }
            }
        }

        // Execute local card play (single-player or offline mode)
        return ExecuteCardPlay(playerId, card);
    }

    /// <summary>
    /// RPC method to send card plays to host for validation and processing
    /// </summary>
    /// <param name="playerId">Player making the move</param>
    /// <param name="suitInt">Card suit as integer</param>
    /// <param name="rankInt">Card rank as integer</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    public void NetworkPlayCard(int playerId, int suitInt, int rankInt)
    {
        var suit = (Suit)suitInt;
        var rank = (Rank)rankInt;
        var card = new Card(suit, rank);

        GD.Print($"CardManager: Network card play received - Player {playerId}: {card}");

        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;

        // CRITICAL FIX: Only host processes card plays
        if (networkManager == null || !networkManager.IsHost)
        {
            GD.PrintErr($"CardManager: Non-host received NetworkPlayCard - ignoring");
            return;
        }

        GD.Print($"CardManager: HOST processing card play from player {playerId}: {card}");

        // Execute the card play on host (ExecuteCardPlay will handle broadcasting on success)
        bool success = ExecuteCardPlay(playerId, card);

        if (!success)
        {
            // Only broadcast failed card play to original sender (success is handled by ExecuteCardPlay)
            RpcId(GetTree().GetMultiplayer().GetRemoteSenderId(), MethodName.NetworkCardPlayResult, playerId, suitInt, rankInt, false);
        }
    }

    /// <summary>
    /// RPC method to notify clients of card play results
    /// </summary>
    /// <param name="playerId">Player who made the move</param>
    /// <param name="suitInt">Card suit as integer</param>
    /// <param name="rankInt">Card rank as integer</param>
    /// <param name="success">Whether the card play was successful</param>
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]
    public void NetworkCardPlayResult(int playerId, int suitInt, int rankInt, bool success)
    {
        var suit = (Suit)suitInt;
        var rank = (Rank)rankInt;
        var card = new Card(suit, rank);

        GD.Print($"CardManager: CLIENT received card play result - Player {playerId}: {card}, Success: {success}");

        if (success)
        {
            // Apply the successful card play on client
            ClientExecuteCardPlay(playerId, card);
        }
        else
        {
            GD.Print($"CardManager: CLIENT card play failed for player {playerId}: {card}");
        }
    }

    /// <summary>
    /// Execute card play on client (without validation, since host already validated)
    /// </summary>
    /// <param name="playerId">Player making the move</param>
    /// <param name="card">Card being played</param>
    private void ClientExecuteCardPlay(int playerId, Card card)
    {
        // Remove card from player's hand
        if (PlayerHands.ContainsKey(playerId) && PlayerHands[playerId].Contains(card))
        {
            PlayerHands[playerId].Remove(card);
        }

        // Add to current trick
        CurrentTrick.Add(new CardPlay(playerId, card));

        // Emit signal for UI updates
        EmitSignal("CardPlayed", playerId, card.ToString());

        GD.Print($"CardManager: CLIENT executed card play - Player {playerId}: {card}");
    }

    /// <summary>
    /// Execute the actual card play logic (called locally or from RPC)
    /// </summary>
    /// <param name="playerId">Player making the move</param>
    /// <param name="card">Card being played</param>
    private bool ExecuteCardPlay(int playerId, Card card)
    {
        // Validate it's the player's turn
        if (PlayerOrder[CurrentPlayerTurn] != playerId)
        {
            GD.PrintErr($"CardManager: Not player {playerId}'s turn");
            return false;
        }

        // NEW: Validate player is at the table (concurrent gameplay requirement)
        var gameManager = GameManager.Instance;
        if (gameManager != null && !gameManager.IsPlayerAtTable(playerId))
        {
            GD.PrintErr($"CardManager: Player {playerId} cannot play card - not at table (currently in kitchen)");
            return false;
        }

        // Validate player has the card
        if (!PlayerHands[playerId].Contains(card))
        {
            GD.PrintErr($"CardManager: Player {playerId} doesn't have card {card}");
            return false;
        }

        // Validate card play follows trick-taking rules
        if (!IsValidCardPlay(playerId, card))
        {
            GD.PrintErr($"CardManager: Invalid card play: {card}");
            return false;
        }

        // Play the card
        PlayerHands[playerId].Remove(card);
        CurrentTrick.Add(new CardPlay(playerId, card));

        // CRITICAL FIX: Sync card play to clients if this is the host
        var networkManager = gameManager?.NetworkManager;
        if (networkManager != null && networkManager.IsConnected && networkManager.IsHost)
        {
            // Send successful card play to all clients
            GD.Print($"CardManager: HOST syncing card play to clients - Player {playerId}: {card}");
            Rpc(MethodName.NetworkCardPlayResult, playerId, (int)card.Suit, (int)card.Rank, true);
        }

        EmitSignal("CardPlayed", playerId, card.ToString());

        EndTurn();
        return true;
    }

    /// <summary>
    /// Validate if a card play is legal according to trick-taking rules
    /// </summary>
    /// <param name="playerId">Player attempting to play</param>
    /// <param name="card">Card being played</param>
    /// <returns>True if valid play</returns>
    public bool IsValidCardPlay(int playerId, Card card)
    {
        // First card of trick - always valid
        if (CurrentTrick.Count == 0)
        {
            return true;
        }

        // Must follow suit if possible
        Suit ledSuit = CurrentTrick[0].Card.Suit;

        // Check if player has cards of led suit
        bool hasLedSuit = PlayerHands[playerId].Any(c => c.Suit == ledSuit);

        if (hasLedSuit && card.Suit != ledSuit)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Complete the current trick and determine winner (HOST ONLY)
    /// </summary>
    private void CompleteTrick()
    {
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;

        // CRITICAL FIX: Only host should complete tricks
        if (networkManager != null && networkManager.IsConnected && !networkManager.IsHost)
        {
            GD.PrintErr("CardManager: CLIENT attempted to call CompleteTrick - only host should complete tricks!");
            return;
        }

        // Determine trick winner (highest card of led suit wins)
        Suit ledSuit = CurrentTrick[0].Card.Suit;
        CardPlay winningPlay = CurrentTrick[0];

        foreach (var cardPlay in CurrentTrick)
        {
            if (cardPlay.Card.Suit == ledSuit && cardPlay.Card.GetValue() > winningPlay.Card.GetValue())
            {
                winningPlay = cardPlay;
            }
        }

        int winnerId = winningPlay.PlayerId;
        GD.Print($"CardManager: HOST Player {winnerId} wins trick with {winningPlay.Card}");

        // Award points (1 point per trick)
        PlayerScores[winnerId]++;

        // Update trick leader for next trick
        CurrentTrickLeader = PlayerOrder.IndexOf(winnerId);
        CurrentPlayerTurn = CurrentTrickLeader;

        // CRITICAL FIX: Sync trick completion to all clients
        if (networkManager != null && networkManager.IsConnected)
        {
            Rpc(MethodName.NetworkSyncTrickComplete, winnerId, CurrentTrickLeader, CurrentPlayerTurn, PlayerScores[winnerId]);
        }

        EmitSignal(SignalName.TrickCompleted, winnerId);

        // Clear current trick
        CurrentTrick.Clear();
        TricksPlayed++;

        // Check if hand is complete
        if (PlayerHands[PlayerOrder[0]].Count == 0)
        {
            CompleteHand();
        }
        else
        {
            StartTurn();
        }
    }

    /// <summary>
    /// RPC method to synchronize trick completion from host to clients
    /// </summary>
    /// <param name="winnerId">Player who won the trick</param>
    /// <param name="newTrickLeader">New trick leader index</param>
    /// <param name="newCurrentTurn">New current turn index</param>
    /// <param name="winnerScore">Updated score for the winner</param>
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]
    public void NetworkSyncTrickComplete(int winnerId, int newTrickLeader, int newCurrentTurn, int winnerScore)
    {
        GD.Print($"CardManager: CLIENT received trick completion - Winner: {winnerId}, Score: {winnerScore}");

        // Update scores
        if (PlayerScores.ContainsKey(winnerId))
        {
            PlayerScores[winnerId] = winnerScore;
        }

        // Update trick leader and current turn
        CurrentTrickLeader = newTrickLeader;
        CurrentPlayerTurn = newCurrentTurn;

        // Clear trick on client
        CurrentTrick.Clear();
        TricksPlayed++;

        // Emit signal for UI updates
        EmitSignal(SignalName.TrickCompleted, winnerId);

        GD.Print($"CardManager: CLIENT trick synchronized - Leader: {CurrentTrickLeader}, Turn: {CurrentPlayerTurn}");
    }

    /// <summary>
    /// Complete the current hand and check for game end
    /// </summary>
    private void CompleteHand()
    {
        EmitSignal(SignalName.HandCompleted);

        // Find hand loser (lowest score) for chat intimidation
        int lowestScore = PlayerScores.Values.Min();
        var losers = PlayerScores.Where(kvp => kvp.Value == lowestScore).Select(kvp => kvp.Key).ToList();

        // Apply chat intimidation to losers
        foreach (int loserId in losers)
        {
            // Notify GameManager of loser for chat intimidation
            if (GameManager.Instance != null)
            {
                var playerData = GameManager.Instance.GetPlayer(loserId);
                if (playerData != null)
                {
                    playerData.AddGameXP(false); // Add XP for losing
                }
            }
        }

        // Award XP to winners
        int highestScore = PlayerScores.Values.Max();
        var winners = PlayerScores.Where(kvp => kvp.Value == highestScore).Select(kvp => kvp.Key).ToList();

        foreach (int winnerId in winners)
        {
            if (GameManager.Instance != null)
            {
                var playerData = GameManager.Instance.GetPlayer(winnerId);
                if (playerData != null)
                {
                    playerData.AddGameXP(true); // Add XP for winning
                }
            }
        }

        // Check for game end
        if (PlayerScores.Values.Max() >= TargetScore)
        {
            CompleteGame();
        }
        else
        {
            // Deal next hand
            CurrentDealer = GetNextDealer();
            DealNewHand();
        }
    }

    /// <summary>
    /// Complete the game
    /// </summary>
    private void CompleteGame()
    {
        // Find overall winner
        int highestScore = PlayerScores.Values.Max();
        int winnerId = PlayerScores.First(kvp => kvp.Value == highestScore).Key;

        GD.Print($"CardManager: Game won by player {winnerId} with {highestScore} points");

        GameInProgress = false;

        EmitSignal(SignalName.GameCompleted, winnerId);
    }

    /// <summary>
    /// Get the next dealer in rotation
    /// </summary>
    /// <returns>Index of next dealer</returns>
    private int GetNextDealer()
    {
        return (CurrentDealer + 1) % PlayerOrder.Count;
    }

    /// <summary>
    /// Handle turn timer expiration (auto-forfeit)
    /// </summary>
    private void OnTurnTimerExpired()
    {
        GD.Print($"CardManager: OnTurnTimerExpired called - timerActive: {timerActive}, GameInProgress: {GameInProgress}");

        if (!timerActive || !GameInProgress)
        {
            GD.Print($"CardManager: Timer expired but timerActive={timerActive}, GameInProgress={GameInProgress} - ignoring");
            return;
        }

        // CRITICAL FIX: Validate that we're still waiting for the current player's turn
        // If CurrentTrick already has a card from this player, they already played
        int currentPlayerId = PlayerOrder[CurrentPlayerTurn];
        bool playerAlreadyPlayed = CurrentTrick.Any(cardPlay => cardPlay.PlayerId == currentPlayerId);

        if (playerAlreadyPlayed)
        {
            GD.Print($"CardManager: Timer expired but player {currentPlayerId} already played their card - ignoring timeout");
            timerActive = false; // Ensure timer state is clean
            return;
        }

        GD.Print($"CardManager: Turn timer expired for player {currentPlayerId}");

        timerActive = false;

        // Check if player is at table - affects forfeit behavior
        var gameManager = GameManager.Instance;
        bool isPlayerAtTable = gameManager?.IsPlayerAtTable(currentPlayerId) ?? true;

        if (!isPlayerAtTable)
        {
            // Player is in kitchen, they simply miss their turn
            GD.Print($"CardManager: Player {currentPlayerId} is in kitchen - missing turn");
            EmitSignal(SignalName.TurnTimerExpired, currentPlayerId);
            EndTurn();
            return;
        }

        // Player is at table but didn't play - auto-forfeit with random card
        var validCards = GetValidCards(currentPlayerId);
        if (validCards.Count > 0)
        {
            var random = new Random();
            Card cardToPlay = validCards[random.Next(validCards.Count)];

            GD.Print($"CardManager: Auto-forfeiting player {currentPlayerId} with card {cardToPlay}");

            // CRITICAL FIX: Call ExecuteCardPlay directly to avoid RPC and timer issues
            ExecuteCardPlay(currentPlayerId, cardToPlay);
        }
        else
        {
            GD.PrintErr($"CardManager: Player {currentPlayerId} at table has no valid cards to auto-play");
            EndTurn();
        }

        EmitSignal(SignalName.TurnTimerExpired, currentPlayerId);
    }

    /// <summary>
    /// Auto-play a turn for AI players
    /// </summary>
    /// <param name="playerId">AI player ID</param>
    private void AutoPlayAITurn(int playerId)
    {
        GD.Print($"CardManager: AutoPlayAITurn called for player {playerId}");
        GD.Print($"CardManager: GameInProgress: {GameInProgress}, CurrentPlayerTurn: {CurrentPlayerTurn}, PlayerOrder[CurrentPlayerTurn]: {(PlayerOrder.Count > CurrentPlayerTurn ? PlayerOrder[CurrentPlayerTurn] : -1)}");

        if (!GameInProgress || PlayerOrder[CurrentPlayerTurn] != playerId)
        {
            GD.PrintErr($"CardManager: Cannot auto-play for player {playerId} - not their turn or game not in progress");
            GD.PrintErr($"CardManager: GameInProgress: {GameInProgress}, Expected player: {playerId}, Current turn player: {(PlayerOrder.Count > CurrentPlayerTurn ? PlayerOrder[CurrentPlayerTurn] : -1)}");
            return;
        }

        // Get valid cards for AI
        var validCards = GetValidCards(playerId);
        GD.Print($"CardManager: AI player {playerId} has {validCards.Count} valid cards");

        if (validCards.Count == 0)
        {
            GD.PrintErr($"CardManager: AI player {playerId} has no valid cards");

            // Check if player has any cards at all
            if (PlayerHands.ContainsKey(playerId))
            {
                GD.PrintErr($"CardManager: AI player {playerId} has {PlayerHands[playerId].Count} total cards");
                if (PlayerHands[playerId].Count > 0)
                {
                    GD.PrintErr($"CardManager: First card: {PlayerHands[playerId][0]}");
                }
            }
            else
            {
                GD.PrintErr($"CardManager: AI player {playerId} not found in PlayerHands!");
            }
            return;
        }

        // Simple AI: play a deterministic card (first valid card for consistency)
        Card cardToPlay = validCards[0]; // Always play first valid card for deterministic behavior

        GD.Print($"CardManager: AI player {playerId} playing {cardToPlay}");

        // Play the card
        bool success = PlayCard(playerId, cardToPlay);
        if (!success)
        {
            GD.PrintErr($"CardManager: Failed to auto-play card {cardToPlay} for AI player {playerId}");
        }
        else
        {
            GD.Print($"CardManager: Successfully auto-played card {cardToPlay} for AI player {playerId}");
        }
    }

    /// <summary>
    /// Get valid cards for a player to play
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <returns>List of valid cards</returns>
    private List<Card> GetValidCards(int playerId)
    {
        var playerHand = PlayerHands[playerId];
        var validCards = new List<Card>();

        foreach (var card in playerHand)
        {
            if (IsValidCardPlay(playerId, card))
            {
                validCards.Add(card);
            }
        }

        return validCards;
    }

    /// <summary>
    /// End the current game
    /// </summary>
    public void EndGame()
    {
        // Stop timer
        if (turnTimer != null && timerActive)
        {
            turnTimer.Stop();
            timerActive = false;
        }

        GameInProgress = false;
        PlayerOrder.Clear();
        CurrentPlayerTurn = 0;
        PlayerHands.Clear();
        PlayerScores.Clear();
        CurrentTrick.Clear();
        TricksPlayed = 0;
    }

    /// <summary>
    /// Get current game state for UI display
    /// </summary>
    /// <returns>Game state information</returns>
    public GameState GetGameState()
    {
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;

        // Use network timer for clients, local timer for host/offline
        float timeRemaining = 0.0f;
        if (networkManager != null && networkManager.IsConnected && !networkManager.IsHost)
        {
            timeRemaining = networkTurnTimeRemaining; // Client uses synchronized time
        }
        else
        {
            timeRemaining = (float)(turnTimer?.TimeLeft ?? 0.0f); // Host/offline uses local timer
        }

        return new GameState
        {
            GameInProgress = GameInProgress,
            CurrentPlayerTurn = GameInProgress ? PlayerOrder[CurrentPlayerTurn] : -1,
            PlayerScores = new Dictionary<int, int>(PlayerScores),
            TricksPlayed = TricksPlayed,
            CurrentTrick = new List<CardPlay>(CurrentTrick),
            TurnTimeRemaining = timeRemaining
        };
    }

    /// <summary>
    /// Get player's current hand
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <returns>List of cards in hand</returns>
    public List<Card> GetPlayerHand(int playerId)
    {
        return PlayerHands.GetValueOrDefault(playerId, new List<Card>());
    }

    public override void _ExitTree()
    {
        if (turnTimer != null)
        {
            turnTimer.QueueFree();
        }
    }

    /// <summary>
    /// Handle card play synchronization from Nakama
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <param name="suit">Card suit as string</param>
    /// <param name="rank">Card rank as string</param>
    private void OnNakamaCardPlayReceived(int playerId, string suit, string rank)
    {
        // Convert strings back to enum types
        if (!Enum.TryParse<Suit>(suit, out var cardSuit) || !Enum.TryParse<Rank>(rank, out var cardRank))
        {
            GD.PrintErr($"CardManager: Failed to parse card from Nakama - Suit: {suit}, Rank: {rank}");
            return;
        }

        var card = new Card(cardSuit, cardRank);

        GD.Print($"CardManager: Received card play from Nakama - Player {playerId}: {card}");

        // Validate if it's the current player's turn
        if (PlayerOrder[CurrentPlayerTurn] != playerId)
        {
            GD.PrintErr($"CardManager: Received card play from player {playerId} but it's not their turn (CurrentTurn: {CurrentPlayerTurn}, PlayerOrder[CurrentTurn]: {PlayerOrder[CurrentPlayerTurn]})");
            return;
        }

        // Validate if the player is at the table
        var gameManager = GameManager.Instance;
        if (gameManager == null || !gameManager.IsPlayerAtTable(playerId))
        {
            GD.PrintErr($"CardManager: Received card play from player {playerId} but they are not at the table.");
            return;
        }

        // Validate if the player has the card
        if (!PlayerHands.ContainsKey(playerId) || !PlayerHands[playerId].Contains(card))
        {
            GD.PrintErr($"CardManager: Received card play from player {playerId} but they do not have the card {card} in their hand.");
            return;
        }

        // Validate if the card play is legal
        if (!IsValidCardPlay(playerId, card))
        {
            GD.PrintErr($"CardManager: Received card play from player {playerId} but it's an invalid play: {card}");
            return;
        }

        // Execute the card play synchronized from another instance
        ExecuteSynchronizedCardPlay(playerId, card);
    }

    /// <summary>
    /// Execute a synchronized card play received from another instance (Nakama)
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <param name="card">Card being played</param>
    private void ExecuteSynchronizedCardPlay(int playerId, Card card)
    {
        GD.Print($"CardManager: Executing synchronized card play - Player {playerId}: {card}");

        // 🔥 NEW: Clear pending play to allow future plays of this card/player combination
        string playKey = $"{playerId}_{card}";
        if (pendingCardPlays.Contains(playKey))
        {
            pendingCardPlays.Remove(playKey);
            GD.Print($"CardManager: Cleared pending card play: {playKey}");
        }

        // Remove card from player's hand
        if (PlayerHands.ContainsKey(playerId) && PlayerHands[playerId].Contains(card))
        {
            PlayerHands[playerId].Remove(card);
        }

        // Add to current trick
        CurrentTrick.Add(new CardPlay(playerId, card));

        // Emit signal for UI updates
        EmitSignal(SignalName.CardPlayed, playerId, card.ToString());

        var matchManager = MatchManager.Instance;

        // 🔥 CRITICAL: For Nakama games, only match owner progresses turns
        // Non-match-owner instances just update their local state
        if (matchManager?.HasActiveMatch == true && !matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner");
            // Don't call EndTurn() - match owner will handle turn progression and sync it
        }
        else
        {
            // Match owner or traditional network game - progress the turn
            EndTurn();
        }

        GD.Print($"CardManager: Synchronized card play completed - Player {playerId}: {card}");
    }

    /// <summary>
    /// Handle turn change synchronization from Nakama (for non-match-owner instances)
    /// </summary>
    /// <param name="currentPlayerTurn">Current player turn index</param>
    /// <param name="tricksPlayed">Number of tricks played</param>
    private void OnNakamaTurnChangeReceived(int currentPlayerTurn, int tricksPlayed)
    {
        var matchManager = MatchManager.Instance;

        // Only non-match-owner instances should sync turn changes
        if (matchManager?.HasActiveMatch == true && !matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager: NAKAMA CLIENT - synchronizing turn change: PlayerTurn={currentPlayerTurn}, Tricks={tricksPlayed}");

            // Update local game state
            CurrentPlayerTurn = currentPlayerTurn;
            TricksPlayed = tricksPlayed;

            // Emit turn started signal if valid
            if (CurrentPlayerTurn < PlayerOrder.Count)
            {
                int currentPlayerId = PlayerOrder[CurrentPlayerTurn];
                EmitSignal(SignalName.TurnStarted, currentPlayerId);
                GD.Print($"CardManager: NAKAMA CLIENT - turn synchronized for player {currentPlayerId}");
            }
        }
        else
        {
            GD.Print($"CardManager: Ignoring turn change sync - match owner manages turns locally");
        }
    }

    /// <summary>
    /// Handle card dealing synchronization from Nakama
    /// </summary>
    private void OnNakamaCardsDealt()
    {
        var matchManager = MatchManager.Instance;
        if (matchManager == null) return;

        // Only non-match-owner instances should sync dealt cards
        if (matchManager.HasActiveMatch && !matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager: NAKAMA CLIENT - synchronizing dealt cards from match owner");

            var dealtCards = matchManager.LastDealtCards;
            foreach (var kvp in dealtCards)
            {
                int playerId = kvp.Key;
                var cardStrings = kvp.Value;

                // Convert card strings back to Card objects
                var cards = new List<Card>();
                foreach (var cardString in cardStrings)
                {
                    if (TryParseCardFromString(cardString, out Card card))
                    {
                        cards.Add(card);
                    }
                    else
                    {
                        GD.PrintErr($"CardManager: Failed to parse card string: {cardString}");
                    }
                }

                // Update player's hand
                if (PlayerHands.ContainsKey(playerId))
                {
                    PlayerHands[playerId] = cards;
                    GD.Print($"CardManager: CLIENT hand updated for player {playerId} - {cards.Count} cards");
                }
                else
                {
                    GD.PrintErr($"CardManager: Received cards dealt for player {playerId} but they are not in PlayerHands!");
                }
            }

            // Trigger UI update
            EmitSignal(SignalName.HandDealt);
            GD.Print($"CardManager: NAKAMA CLIENT - card dealing synchronization completed");
        }
        else
        {
            GD.Print($"CardManager: Ignoring cards dealt sync - match owner deals locally");
        }
    }

    /// <summary>
    /// Try to parse a card from its string representation
    /// </summary>
    /// <param name="cardString">String representation of card (e.g., "Ace of Spades")</param>
    /// <param name="card">Parsed card object</param>
    /// <returns>True if parsing succeeded</returns>
    private bool TryParseCardFromString(string cardString, out Card card)
    {
        card = null;

        try
        {
            // Card ToString() format is "Rank of Suit" (e.g., "Ace of Spades")
            var parts = cardString.Split(new[] { " of " }, StringSplitOptions.None);
            if (parts.Length != 2)
            {
                GD.PrintErr($"CardManager: Invalid card string format: {cardString}");
                return false;
            }

            string rankString = parts[0];
            string suitString = parts[1];

            if (!Enum.TryParse<Rank>(rankString, out var cardRank) || !Enum.TryParse<Suit>(suitString, out var cardSuit))
            {
                GD.PrintErr($"CardManager: Failed to parse rank '{rankString}' or suit '{suitString}' from: {cardString}");
                return false;
            }

            card = new Card(cardSuit, cardRank);
            return true;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"CardManager: Exception parsing card string '{cardString}': {ex.Message}");
            return false;
        }
    }
}

/// <summary>
/// Represents a playing card
/// </summary>
public class Card
{
    public Suit Suit { get; set; }
    public Rank Rank { get; set; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    /// <summary>
    /// Get numerical value for comparison (Ace = 14, King = 13, etc.)
    /// </summary>
    /// <returns>Card value</returns>
    public int GetValue()
    {
        return (int)Rank;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }

    public override bool Equals(object obj)
    {
        if (obj is Card other)
        {
            return Suit == other.Suit && Rank == other.Rank;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Suit, Rank);
    }
}

/// <summary>
/// Card suits
/// </summary>
public enum Suit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

/// <summary>
/// Card ranks (values match standard card values)
/// </summary>
public enum Rank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}

/// <summary>
/// Represents a card played in a trick
/// </summary>
public class CardPlay
{
    public int PlayerId { get; set; }
    public Card Card { get; set; }

    public CardPlay(int playerId, Card card)
    {
        PlayerId = playerId;
        Card = card;
    }

    public override string ToString()
    {
        return $"Player {PlayerId}: {Card}";
    }
}

/// <summary>
/// Current game state snapshot
/// </summary>
public class GameState
{
    public bool GameInProgress { get; set; }
    public int CurrentPlayerTurn { get; set; }
    public Dictionary<int, int> PlayerScores { get; set; } = new();
    public int TricksPlayed { get; set; }
    public List<CardPlay> CurrentTrick { get; set; } = new();
    public float TurnTimeRemaining { get; set; }
}
