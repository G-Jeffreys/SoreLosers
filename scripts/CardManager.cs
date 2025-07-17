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

    // Turn timer
    private Timer turnTimer;
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
        turnTimer = new Timer();
        turnTimer.WaitTime = TurnDuration;
        turnTimer.OneShot = true;
        turnTimer.Timeout += OnTurnTimerExpired;
        AddChild(turnTimer);

        // Initialize deck
        InitializeDeck();
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

    /// <summary>
    /// Shuffle the deck using Fisher-Yates algorithm
    /// </summary>
    private void ShuffleDeck()
    {
        // Use deterministic seed for testing so both instances have same shuffle
        int seed = 12345; // Fixed seed for consistent deck across instances
        var random = new Random(seed);

        for (int i = Deck.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (Deck[i], Deck[j]) = (Deck[j], Deck[i]);
        }
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

        // If networked, only host should start the game and broadcast to clients
        if (networkManager != null && networkManager.IsConnected)
        {
            if (networkManager.IsHost)
            {
                // Convert to array for RPC
                int[] playerIdArray = playerIds.ToArray();

                // CRITICAL FIX: Add error handling for RPC calls
                try
                {
                    // Broadcast game start to all clients (including self)
                    Rpc(MethodName.NetworkStartGame, playerIdArray);
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
    /// RPC method to synchronize game start across network
    /// CRITICAL FIX: Changed from Authority to AnyPeer for better compatibility
    /// </summary>
    /// <param name="playerIds">Array of player IDs</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void NetworkStartGame(int[] playerIds)
    {
        GD.Print("=== CRITICAL DEBUG: NetworkStartGame RPC RECEIVED ===");
        GD.Print($"CardManager: Received NetworkStartGame RPC with {playerIds.Length} players");
        GD.Print($"CardManager: Sender ID: {GetTree().GetMultiplayer().GetRemoteSenderId()}");
        GD.Print($"CardManager: My ID: {GetTree().GetMultiplayer().GetUniqueId()}");

        var networkManager = GameManager.Instance?.NetworkManager;
        GD.Print($"CardManager: NetworkManager status - IsHost: {networkManager?.IsHost}, IsClient: {networkManager?.IsClient}, IsConnected: {networkManager?.IsConnected}");

        GD.Print($"CardManager: Received player IDs: [{string.Join(", ", playerIds)}]");

        // Check current ConnectedPlayers state
        if (GameManager.Instance?.ConnectedPlayers != null)
        {
            var currentPlayers = GameManager.Instance.ConnectedPlayers.Keys.ToArray();
            GD.Print($"CardManager: Current ConnectedPlayers: [{string.Join(", ", currentPlayers)}]");

            // Check if all received players exist in ConnectedPlayers
            bool allPlayersExist = true;
            foreach (int playerId in playerIds)
            {
                if (!GameManager.Instance.ConnectedPlayers.ContainsKey(playerId))
                {
                    GD.PrintErr($"CardManager: MISSING PLAYER {playerId} in ConnectedPlayers!");
                    allPlayersExist = false;
                }
            }

            if (!allPlayersExist)
            {
                GD.PrintErr("CardManager: NOT ALL PLAYERS EXIST - SYNC FAILED!");
            }
            else
            {
                GD.Print("CardManager: All players exist in ConnectedPlayers - sync successful");
            }
        }

        // Additional validation: only process if sent by host (ID 1) or if we're offline
        if (networkManager != null && networkManager.IsConnected)
        {
            int senderId = GetTree().GetMultiplayer().GetRemoteSenderId();
            if (!networkManager.IsHost && senderId != 1)
            {
                GD.PrintErr($"CardManager: Ignoring NetworkStartGame from non-host sender {senderId}");
                return;
            }
        }

        GD.Print("=== CALLING ExecuteGameStart ===");
        ExecuteGameStart(playerIds.ToList());
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

            GD.Print($"CardManager: HOST dealt {PlayerHands[playerId].Count} cards to player {playerId}");
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
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
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
        int currentPlayerId = PlayerOrder[CurrentPlayerTurn];

        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;

        // If networked, only host manages turn timing
        if (networkManager != null && networkManager.IsConnected && !networkManager.IsHost)
        {
            EmitSignal(SignalName.TurnStarted, currentPlayerId);
            return;
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
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void NetworkTurnStarted(int playerId)
    {
        EmitSignal(SignalName.TurnStarted, playerId);
    }

    /// <summary>
    /// RPC method to synchronize timer updates from host to clients
    /// </summary>
    /// <param name="timeRemaining">Time remaining in seconds</param>
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Unreliable)]
    public void NetworkTimerUpdate(float timeRemaining)
    {
        networkTurnTimeRemaining = timeRemaining;
        EmitSignal(SignalName.TurnTimerUpdated, timeRemaining);
    }

    /// <summary>
    /// End the current turn and move to next player
    /// </summary>
    private void EndTurn()
    {
        int currentPlayerId = PlayerOrder[CurrentPlayerTurn];

        // Stop turn timer
        if (turnTimer != null && timerActive)
        {
            turnTimer.Stop();
            timerActive = false;
        }

        EmitSignal(SignalName.TurnEnded, currentPlayerId);

        // Move to next player
        CurrentPlayerTurn = (CurrentPlayerTurn + 1) % PlayerOrder.Count;

        // Check if trick is complete
        if (CurrentTrick.Count == PlayerOrder.Count)
        {
            CompleteTrick();
        }
        else
        {
            StartTurn();
        }
    }

    /// <summary>
    /// Play a card for the current player - handles both local and network calls
    /// </summary>
    /// <param name="playerId">Player making the move</param>
    /// <param name="card">Card being played</param>
    public bool PlayCard(int playerId, Card card)
    {
        // If we're networked and this is a local player move, broadcast it via RPC
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;

        if (networkManager != null && networkManager.IsConnected)
        {
            // Check if this is a local player making the move
            bool isLocalPlayer = gameManager.LocalPlayer?.PlayerId == playerId;

            if (isLocalPlayer)
            {
                // Broadcast to all clients (including self via CallLocal)
                Rpc(MethodName.NetworkPlayCard, playerId, (int)card.Suit, (int)card.Rank);
                return true; // The RPC call will handle the actual card play
            }
        }

        // Execute local card play (either single-player or from RPC)
        return ExecuteCardPlay(playerId, card);
    }

    /// <summary>
    /// RPC method to synchronize card plays across network
    /// </summary>
    /// <param name="playerId">Player making the move</param>
    /// <param name="suitInt">Card suit as integer</param>
    /// <param name="rankInt">Card rank as integer</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void NetworkPlayCard(int playerId, int suitInt, int rankInt)
    {
        var suit = (Suit)suitInt;
        var rank = (Rank)rankInt;
        var card = new Card(suit, rank);

        GD.Print($"CardManager: Network card play - Player {playerId}: {card}");

        // Execute the card play locally
        ExecuteCardPlay(playerId, card);
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
    /// Complete the current trick and determine winner
    /// </summary>
    private void CompleteTrick()
    {
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
        GD.Print($"CardManager: Player {winnerId} wins trick with {winningPlay.Card}");

        // Award points (1 point per trick)
        PlayerScores[winnerId]++;

        // Update trick leader for next trick
        CurrentTrickLeader = PlayerOrder.IndexOf(winnerId);
        CurrentPlayerTurn = CurrentTrickLeader;

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
        if (!timerActive || !GameInProgress)
            return;

        int currentPlayerId = PlayerOrder[CurrentPlayerTurn];
        GD.Print($"CardManager: Turn timer expired for player {currentPlayerId}");

        timerActive = false;

        // Check if player is at table - affects forfeit behavior
        var gameManager = GameManager.Instance;
        bool isPlayerAtTable = gameManager?.IsPlayerAtTable(currentPlayerId) ?? true;

        if (!isPlayerAtTable)
        {
            // Player is in kitchen, they simply miss their turn
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
            PlayCard(currentPlayerId, cardToPlay);
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
