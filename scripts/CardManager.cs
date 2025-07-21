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
    public int TargetScore = 10; // Points to win the game

    // Turn state types - NEW
    public enum TurnState
    {
        PlayerTurn,
        EndOfRound
    }

    // Game state
    public bool GameInProgress { get; private set; } = false;
    public int CurrentPlayerTurn { get; private set; } = 0;
    public List<int> PlayerOrder { get; private set; } = new();
    public int CurrentDealer { get; private set; } = 0;
    public int CurrentTrickLeader { get; private set; } = 0;
    public TurnState CurrentTurnState { get; private set; } = TurnState.PlayerTurn;
    public int CurrentTrickWinner { get; private set; } = -1; // Store winner during end-of-round

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

    // 🔥 NEW: Timer update throttling to prevent network spam
    private float lastTimerUpdateSent = 0.0f;
    private const float TimerUpdateInterval = 0.1f; // Send timer updates every 0.1 seconds (10 FPS)

    // 🔥 NEW: Turn change deduplication to prevent duplicate processing
    private int lastProcessedTurn = -1;
    private int lastProcessedTricks = -1;

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
    public delegate void EndOfRoundStartedEventHandler(int winnerId);

    [Signal]
    public delegate void EndOfRoundCompletedEventHandler();

    [Signal]
    public delegate void HandCompletedEventHandler();

    [Signal]
    public delegate void HandDealtEventHandler();

    [Signal]
    public delegate void GameCompletedEventHandler(int winnerId);

    public override void _Ready()
    {
        // Initialize CardManager
        InitializeDeck();
        InitializeTimers();

        // 🔥 FIXED: Defer MatchManager connection to ensure it's initialized
        CallDeferred(MethodName.ConnectToMatchManager);
    }

    /// <summary>
    /// Initialize game timers
    /// </summary>
    private void InitializeTimers()
    {
        // Initialize turn timer
        turnTimer = new Godot.Timer();
        turnTimer.WaitTime = TurnDuration;
        turnTimer.OneShot = true;
        turnTimer.Timeout += OnTurnTimerExpired;
        AddChild(turnTimer);

        GD.Print("CardManager: Game timers initialized");
    }

    /// <summary>
    /// Connect to MatchManager signals after initialization is complete
    /// </summary>
    private void ConnectToMatchManager()
    {
        var processId = OS.GetProcessId();

        // 🔥 FIXED: Connect to MatchManager for Nakama card play synchronization
        var matchManager = MatchManager.Instance;
        GD.Print($"CardManager[PID:{processId}]: ConnectToMatchManager called - MatchManager.Instance: {matchManager?.GetInstanceId()}");

        if (matchManager != null)
        {
            // 🔥 CRITICAL FIX: Use Godot signal system instead of C# events
            matchManager.Connect(MatchManager.SignalName.CardPlayReceived, new Callable(this, nameof(OnNakamaCardPlayReceived)));
            matchManager.Connect(MatchManager.SignalName.TurnChangeReceived, new Callable(this, nameof(OnNakamaTurnChangeReceived)));
            matchManager.Connect(MatchManager.SignalName.CardsDealt, new Callable(this, nameof(OnNakamaCardsDealt)));
            matchManager.Connect(MatchManager.SignalName.TrickCompletedReceived, new Callable(this, nameof(OnNakamaTrickCompletedReceived)));
            matchManager.Connect(MatchManager.SignalName.TimerUpdateReceived, new Callable(this, nameof(OnNakamaTimerUpdateReceived)));

            GD.Print($"CardManager[PID:{processId}]: Connected to MatchManager using Godot signal system for card play, turn, card dealing, trick completion, and timer synchronization");

            // 🔥 FIXED: Verify the Godot signal connections
            if (matchManager.IsConnected(MatchManager.SignalName.CardsDealt, new Callable(this, nameof(OnNakamaCardsDealt))))
            {
                GD.Print($"CardManager[PID:{processId}]: ✅ Successfully connected to MatchManager.CardsDealt signal");
            }
            else
            {
                GD.PrintErr($"CardManager[PID:{processId}]: ❌ Failed to connect to MatchManager.CardsDealt signal");
            }

            if (matchManager.IsConnected(MatchManager.SignalName.CardPlayReceived, new Callable(this, nameof(OnNakamaCardPlayReceived))))
            {
                GD.Print($"CardManager[PID:{processId}]: ✅ Successfully connected to MatchManager.CardPlayReceived signal");
            }
            else
            {
                GD.PrintErr($"CardManager[PID:{processId}]: ❌ Failed to connect to MatchManager.CardPlayReceived signal");
            }

            if (matchManager.IsConnected(MatchManager.SignalName.TurnChangeReceived, new Callable(this, nameof(OnNakamaTurnChangeReceived))))
            {
                GD.Print($"CardManager[PID:{processId}]: ✅ Successfully connected to MatchManager.TurnChangeReceived signal");
            }
        }
        else
        {
            GD.PrintErr($"CardManager[PID:{processId}]: MatchManager.Instance is null - retrying connection in 1 second");
            // Retry connection after a short delay
            GetTree().CreateTimer(1.0).Timeout += ConnectToMatchManager;
        }
    }

    public override void _Process(double delta)
    {
        // Host: Send timer updates to clients every 0.1 seconds
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;
        var matchManager = MatchManager.Instance;

        if (timerActive)
        {
            float timeRemaining = (float)(turnTimer?.TimeLeft ?? 0.0f);
            float currentTime = Time.GetTicksMsec() / 1000.0f;

            // 🔥 CRITICAL FIX: Don't send timer updates during end-of-round state
            // This prevents host timer updates from interfering with client end-of-round timers
            if (CurrentTurnState == TurnState.EndOfRound)
            {
                // During end-of-round, only emit local signal, don't sync to clients
                EmitSignal(SignalName.TurnTimerUpdated, timeRemaining);
                return;
            }

            // 🔥 MAJOR FIX: Drastically reduce timer update frequency to prevent spam
            if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
            {
                // 🔥 CRITICAL FIX: Only send timer updates every 1 second (not every frame!)
                float reducedInterval = 1.0f; // Send only once per second
                if (currentTime - lastTimerUpdateSent >= reducedInterval)
                {
                    _ = matchManager.SendTimerUpdate(timeRemaining);
                    lastTimerUpdateSent = currentTime;
                }
                EmitSignal(SignalName.TurnTimerUpdated, timeRemaining);
            }
            // Traditional networking fallback
            else if (networkManager != null && networkManager.IsConnected && networkManager.IsHost)
            {
                // Send timer update to clients every frame (throttled by network automatically)
                Rpc(MethodName.NetworkTimerUpdate, timeRemaining);
                EmitSignal(SignalName.TurnTimerUpdated, timeRemaining);
            }
            // Single player or client - just emit local signal
            else
            {
                EmitSignal(SignalName.TurnTimerUpdated, timeRemaining);
            }
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
        CurrentPlayerTurn = 0; // Start with first player
        CurrentDealer = 0;
        CurrentTrickLeader = 0; // Start with first player
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

        // 🔥 CRITICAL FIX: For Nakama games, check if we should execute locally or wait for sync
        var matchManager = MatchManager.Instance;
        if (matchManager?.HasActiveMatch == true)
        {
            if (matchManager.IsLocalPlayerMatchOwner())
            {
                GD.Print($"CardManager: NAKAMA MATCH OWNER - will deal cards and sync to all players");
            }
            else
            {
                GD.Print($"CardManager: NAKAMA CLIENT - will wait for card synchronization from match owner");
                // 🔥 FIXED: For Nakama clients, only initialize minimal state and wait for sync
                InitializeClientGameState(playerIds);
                return; // Exit early - don't execute full game start
            }
        }

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
        CurrentPlayerTurn = 0; // Start with first player
        CurrentDealer = 0;
        CurrentTrickLeader = 0; // Start with first player
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
        GD.Print($"CardManager: Starting card dealing process...");

        // 🔥 FIXED: For Nakama games, only match owner deals cards
        if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager: NAKAMA MATCH OWNER - dealing cards and syncing via Nakama");
            HostDealAndSyncCards();
        }
        else if (matchManager?.HasActiveMatch != true)
        {
            // Traditional network or offline game
            var networkManager = gameManager?.NetworkManager;
            if (networkManager != null && networkManager.IsConnected)
            {
                if (networkManager.IsHost)
                {
                    HostDealAndSyncCards();
                }
                else
                {
                    GD.Print("CardManager: CLIENT waiting for host to deal cards");
                }
            }
            else
            {
                // Single-player: Deal cards locally
                HostDealAndSyncCards();
            }
        }
    }

    /// <summary>
    /// Initialize minimal game state for Nakama clients who will receive synchronization
    /// </summary>
    /// <param name="playerIds">List of player IDs in turn order</param>
    private void InitializeClientGameState(List<int> playerIds)
    {
        GD.Print($"CardManager: NAKAMA CLIENT - initializing minimal game state for synchronization");

        // Set basic game state but don't deal cards or start turns
        PlayerOrder = new List<int>(playerIds);
        CurrentPlayerTurn = 0; // Start with first player 
        CurrentDealer = 0;
        CurrentTrickLeader = 0; // Start with first player
        GameInProgress = true;
        TricksPlayed = 0;

        // Initialize empty hands and scores (cards will come from match owner)
        PlayerHands.Clear();
        PlayerScores.Clear();
        CurrentTrick.Clear();

        foreach (int playerId in playerIds)
        {
            PlayerHands[playerId] = new List<Card>();
            PlayerScores[playerId] = 0;
        }

        GD.Print($"CardManager: NAKAMA CLIENT - game state initialized, waiting for match owner to deal cards");
        GD.Print($"CardManager: NAKAMA CLIENT PlayerOrder: [{string.Join(", ", PlayerOrder)}]");
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

        // 🔥 CRITICAL FIX: Clear pending card plays for new hand
        // This prevents stale pending plays from blocking auto-forfeit
        pendingCardPlays.Clear();
        GD.Print($"CardManager: Cleared pending card plays for new hand");

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
        }

        // 🔥 FIXED: For Nakama games, sync ALL dealt cards to all players AFTER dealing is complete
        var matchManager = MatchManager.Instance;
        if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print("CardManager: NAKAMA MATCH OWNER - syncing ALL dealt cards to all players");

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
        else
        {
            // 🔥 FIXED: For traditional network games, sync to clients
            SyncDealtHandsToClients();
        }

        // Play card shuffle sound effect
        var gameManager = GameManager.Instance;
        var audioManager = gameManager?.AudioManager;
        audioManager?.PlaySFX("card_shuffle");

        // Notify UI that cards have been dealt
        EmitSignal(SignalName.HandDealt);

        // Start first trick
        // 🔥 FIXED: Start with first player (index 0) for the first hand, not GetNextDealer()
        CurrentTrickLeader = 0; // Always start with first player for consistency
        CurrentPlayerTurn = CurrentTrickLeader;

        GD.Print($"========== NEW HAND STARTING ==========");
        GD.Print($"CardManager: HOST starting new hand");
        GD.Print($"CardManager: TrickLeader: {CurrentTrickLeader}, CurrentTurn: {CurrentPlayerTurn}");
        GD.Print($"CardManager: Turn player: {PlayerOrder[CurrentPlayerTurn]}");
        GD.Print($"CardManager: Turn state: {CurrentTurnState}");
        GD.Print($"CardManager: Game in progress: {GameInProgress}");
        GD.Print($"CardManager: Timer active: {timerActive}");
        GD.Print($"CardManager: Timer exists: {turnTimer != null}");

        // 🔥 CRITICAL FIX: Ensure turn state is PlayerTurn before starting turn
        if (CurrentTurnState != TurnState.PlayerTurn)
        {
            GD.PrintErr($"CardManager: CRITICAL ERROR - Turn state is {CurrentTurnState} when starting new hand! Forcing to PlayerTurn.");
            CurrentTurnState = TurnState.PlayerTurn;
            CurrentTrickWinner = -1;
        }

        // 🔥 CRITICAL FIX: Stop any lingering timer before starting new hand
        if (timerActive && turnTimer != null)
        {
            GD.Print($"CardManager: Stopping lingering timer before new hand (was active: {timerActive})");
            turnTimer.Stop();
            timerActive = false;
        }

        StartTurn();

        GD.Print($"CardManager: ========== NEW HAND STARTED ==========");
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

        // 🔥 CRITICAL FIX: Clear pending card plays for new hand on client
        // This prevents stale pending plays from blocking auto-forfeit  
        pendingCardPlays.Clear();
        GD.Print($"CardManager: CLIENT cleared pending card plays for new hand");

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
        // 🔥 FIXED: Ensure consistent turn initialization on client side
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
        GD.Print($"CardManager: StartTurn() called - State: {CurrentTurnState}, GameInProgress: {GameInProgress}");
        GD.Print($"CardManager: CurrentPlayerTurn: {CurrentPlayerTurn}, PlayerOrder.Count: {PlayerOrder.Count}");

        if (!GameInProgress || CurrentPlayerTurn >= PlayerOrder.Count)
        {
            GD.PrintErr($"CardManager: Cannot start turn - GameInProgress: {GameInProgress}, CurrentPlayerTurn: {CurrentPlayerTurn}, PlayerOrder.Count: {PlayerOrder.Count}");
            return;
        }

        // 🔥 CRITICAL CHECK: Make sure we're in the right turn state
        if (CurrentTurnState != TurnState.PlayerTurn)
        {
            GD.PrintErr($"CardManager: Cannot start player turn - currently in {CurrentTurnState} state");
            return;
        }

        int currentPlayerId = PlayerOrder[CurrentPlayerTurn];

        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;
        var matchManager = MatchManager.Instance;

        // 🔥 CRITICAL FIX: For Nakama games, allow clients to manage their own player's turn
        if (matchManager?.HasActiveMatch == true)
        {
            bool isLocalPlayerTurn = (gameManager?.LocalPlayer?.PlayerId == currentPlayerId);

            if (!matchManager.IsLocalPlayerMatchOwner())
            {
                if (isLocalPlayerTurn)
                {
                    // 🔥 CORRECTED: Clients CAN start timer for their OWN turns (needed for auto-forfeit)
                    GD.Print($"CardManager: NAKAMA CLIENT - managing own player's turn timer (Player {currentPlayerId})");
                    // Continue to start timer and allow client to auto-forfeit themselves
                }
                else
                {
                    // 🔥 CORRECTED: Only skip timer for OTHER players' turns
                    GD.Print($"CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player {currentPlayerId}");
                    EmitSignal(SignalName.TurnStarted, currentPlayerId);
                    return;
                }
            }
            else
            {
                GD.Print($"CardManager: NAKAMA MATCH OWNER - managing turn for player {currentPlayerId}");
            }
        }

        // If networked, only host manages turn timing
        if (networkManager != null && networkManager.IsConnected && !networkManager.IsHost)
        {
            EmitSignal(SignalName.TurnStarted, currentPlayerId);
            return;
        }

        // CRITICAL FIX: Ensure timer is stopped before starting new turn
        // This should be rare now since OnNakamaTurnChangeReceived proactively stops timers
        if (turnTimer != null && timerActive)
        {
            var timeLeft = turnTimer?.TimeLeft ?? 0.0;
            GD.PrintErr($"CardManager: ⚠️ UNEXPECTED TIMER CONFLICT - timer still active when starting turn for player {currentPlayerId}, stopping it (had {timeLeft:F1}s remaining)");
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
            GD.Print($"CardManager: Timer state before start - exists: {turnTimer != null}, active: {timerActive}, waitTime: {turnTimer?.WaitTime}");

            // 🔥 CRITICAL FIX: Ensure wait time is properly set before starting
            turnTimer.WaitTime = TurnDuration;
            timerActive = true;
            turnTimer.Start();

            GD.Print($"CardManager: Timer started successfully - duration: {TurnDuration}s, timeLeft: {turnTimer.TimeLeft}, active: {timerActive}");

            // 🔥 FIXED: For Nakama games, sync turn start to all instances
            if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
            {
                GD.Print($"CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances");
                _ = matchManager.SendTurnChange(currentPlayerId, TricksPlayed);
            }

            // Broadcast turn start to clients (traditional networking)
            if (networkManager != null && networkManager.IsConnected)
            {
                GD.Print($"CardManager: Broadcasting turn start to clients via RPC");
                Rpc(MethodName.NetworkTurnStarted, currentPlayerId);
            }
        }
        else
        {
            GD.PrintErr($"CardManager: CRITICAL ERROR - turnTimer is null when trying to start timer for player {currentPlayerId}!");
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

        // 🔥 CRITICAL FIX: For Nakama games, allow clients to end their own player's turn
        if (matchManager?.HasActiveMatch == true)
        {
            int endingPlayerId = PlayerOrder[CurrentPlayerTurn];
            bool isLocalPlayerTurn = (gameManager?.LocalPlayer?.PlayerId == endingPlayerId);

            if (!matchManager.IsLocalPlayerMatchOwner())
            {
                if (isLocalPlayerTurn)
                {
                    GD.Print($"CardManager: NAKAMA CLIENT - ending own player's turn (Player {endingPlayerId})");
                    // Continue to allow client to end its own turn
                }
                else
                {
                    GD.Print($"CardManager: NAKAMA CLIENT - not my turn to end, waiting for match owner");
                    return;
                }
            }
            else
            {
                GD.Print($"CardManager: NAKAMA MATCH OWNER - managing turn ending for Player {endingPlayerId}");
            }
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
            // 🔥 FIXED: Pass the actual player ID and new turn player ID, not just indices
            int newPlayerId = PlayerOrder[CurrentPlayerTurn];
            _ = matchManager.SendTurnChange(newPlayerId, TricksPlayed);
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
                GD.Print($"CardManager: 🚫 Duplicate card play prevented - {playKey} already pending");
                GD.Print($"CardManager: 🚫 Current pending plays: [{string.Join(", ", pendingCardPlays)}]");
                return false;
            }
            pendingCardPlays.Add(playKey);
            GD.Print($"CardManager: ✅ Added pending card play: {playKey} (total pending: {pendingCardPlays.Count})");

            // 🔥 CRITICAL FIX: Both host AND client execute immediately
            // Nakama doesn't echo messages back to sender, so clients must execute locally
            GD.Print($"CardManager: NAKAMA GAME - executing card immediately: Player {playerId}: {card}");

            // 🔥 DEBUG: Log hand before card removal
            var handBeforeRemoval = PlayerHands[playerId];
            var cardsBeforeRemoval = string.Join(", ", handBeforeRemoval.Select(c => c.ToString()));
            GD.Print($"CardManager: 🃏 Player {playerId} hand BEFORE removal: {handBeforeRemoval.Count} cards [{cardsBeforeRemoval}]");

            // Remove card from player's hand immediately
            bool cardRemoved = PlayerHands[playerId].Remove(card);

            // 🔥 DEBUG: Log hand after card removal
            var handAfterRemoval = PlayerHands[playerId];
            var cardsAfterRemoval = string.Join(", ", handAfterRemoval.Select(c => c.ToString()));
            GD.Print($"CardManager: 🃏 Player {playerId} hand AFTER removal: {handAfterRemoval.Count} cards [{cardsAfterRemoval}]");
            GD.Print($"CardManager: 🃏 Card removal success: {cardRemoved}, Card was: {card}");

            // Add to current trick immediately (prevents timer from seeing empty trick)
            CurrentTrick.Add(new CardPlay(playerId, card));
            GD.Print($"CardManager: NAKAMA - Added card to CurrentTrick immediately: {card} (Trick now has {CurrentTrick.Count} cards)");

            // 🔥 NEW: Provide immediate visual feedback by removing card from UI
            GD.Print($"CardManager: 🃏 Emitting CardPlayed signal for Player {playerId}: {card}");
            EmitSignal(SignalName.CardPlayed, playerId, card.ToString());

            // Send to Nakama for synchronization with other players
            GD.Print($"CardManager[PID:{OS.GetProcessId()}]: About to send card play via MatchManager.Instance: {matchManager?.GetInstanceId()}");
            _ = matchManager.SendCardPlay(playerId, card.Suit.ToString(), card.Rank.ToString());

            // 🔥 CRITICAL FIX: Only progress turn immediately for HUMAN players to prevent UI lag
            // AI players should wait for Nakama synchronization to prevent timing issues
            bool isAIPlayer = false;
            if (gameManager != null)
            {
                var playerData = gameManager.GetPlayer(playerId);
                isAIPlayer = playerData?.PlayerName?.StartsWith("AI_") ?? false;
            }

            if (matchManager.IsLocalPlayerMatchOwner() && !isAIPlayer)
            {
                GD.Print($"CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)");
                EndTurn();
            }
            else if (matchManager.IsLocalPlayerMatchOwner() && isAIPlayer)
            {
                // 🔥 CRITICAL FIX: AI players also progress immediately since Nakama doesn't echo back to sender
                GD.Print($"CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)");
                EndTurn();
            }
            else
            {
                GD.Print($"CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)");
                // 🔥 CRITICAL FIX: Client should end its own turn to stop timer locally
                // Host will handle game progression, but client needs to stop its own timer
                bool isLocalPlayerCard = (gameManager?.LocalPlayer?.PlayerId == playerId);
                if (isLocalPlayerCard)
                {
                    GD.Print($"CardManager: NAKAMA CLIENT - ending own turn locally to stop timer");
                    EndTurn();
                }
                // Other players' cards: clients execute locally but don't progress turns - host handles that
            }

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
        GD.Print($"CardManager[PID:{System.Diagnostics.Process.GetCurrentProcess().Id}]: 🃏 BEFORE card removal - Player {playerId} hand size: {PlayerHands[playerId].Count} cards");
        PlayerHands[playerId].Remove(card);
        CurrentTrick.Add(new CardPlay(playerId, card));
        GD.Print($"CardManager[PID:{System.Diagnostics.Process.GetCurrentProcess().Id}]: 🃏 AFTER card removal - Player {playerId} hand size: {PlayerHands[playerId].Count} cards");

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

        // Store winner and enter end-of-round state instead of immediately completing
        CurrentTrickWinner = winnerId;
        var oldState = CurrentTurnState;
        CurrentTurnState = TurnState.EndOfRound;

        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        GD.Print($"CardManager[PID:{processId}]: 🎯 STATE CHANGE: {oldState} → {CurrentTurnState} (winner: {winnerId})");

        // Update trick leader for next trick (but don't start it yet)
        CurrentTrickLeader = PlayerOrder.IndexOf(winnerId);
        CurrentPlayerTurn = CurrentTrickLeader;

        // CRITICAL FIX: Sync trick completion to all clients
        if (networkManager != null && networkManager.IsConnected)
        {
            Rpc(MethodName.NetworkSyncTrickComplete, winnerId, CurrentTrickLeader, CurrentPlayerTurn, PlayerScores[winnerId]);
        }

        // 🔥 NEW: Sync trick completion via Nakama for Nakama games
        var matchManager = MatchManager.Instance;
        if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players");
            _ = matchManager.SendTrickCompleted(winnerId, CurrentTrickLeader, PlayerScores[winnerId]);
        }

        EmitSignal(SignalName.TrickCompleted, winnerId);

        // DON'T clear trick or start next turn yet - start end-of-round phase
        StartEndOfRoundTurn();

        GD.Print($"CardManager: Entered end-of-round state - winner: {winnerId}, 10-second display period started");
    }

    /// <summary>
    /// Start the end-of-round turn phase (10 seconds to view results)
    /// </summary>
    private void StartEndOfRoundTurn()
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        GD.Print($"CardManager[PID:{processId}]: ========== STARTING END-OF-ROUND TURN ==========");
        GD.Print($"CardManager[PID:{processId}]: Starting end-of-round turn - winner: {CurrentTrickWinner}");
        GD.Print($"CardManager[PID:{processId}]: Current turn state: {CurrentTurnState}");

        // Emit signal to show end-of-round display
        EmitSignal(SignalName.EndOfRoundStarted, CurrentTrickWinner);

        // Use existing timer system for 10-second end-of-round phase
        if (timerActive)
        {
            GD.Print($"CardManager[PID:{processId}]: Stopping existing timer for end-of-round (was active: {timerActive})");
            turnTimer.Stop();
        }

        // Check if we should start our own timer or wait for host sync
        var matchManager = MatchManager.Instance;
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;

        bool shouldManageOwnTimer = false;
        string reason = "";

        if (matchManager?.HasActiveMatch == true)
        {
            // For Nakama games: all instances manage their own end-of-round timer
            shouldManageOwnTimer = true;
            reason = matchManager.IsLocalPlayerMatchOwner() ? "Nakama match owner" : "Nakama client";
        }
        else if (networkManager == null || !networkManager.IsConnected)
        {
            // Single player or offline
            shouldManageOwnTimer = true;
            reason = "Single player/offline";
        }
        else if (networkManager.IsHost)
        {
            // Traditional network host
            shouldManageOwnTimer = true;
            reason = "Traditional network host";
        }
        else
        {
            // Traditional network client - don't manage timer
            shouldManageOwnTimer = false;
            reason = "Traditional network client";
        }

        GD.Print($"CardManager[PID:{processId}]: Timer management decision: {shouldManageOwnTimer} ({reason})");

        if (shouldManageOwnTimer)
        {
            turnTimer.WaitTime = TurnDuration; // Same 10 seconds as regular turns
            GD.Print($"CardManager[PID:{processId}]: Starting end-of-round timer - Duration: {TurnDuration} seconds");
            turnTimer.Start();
            timerActive = true;

            GD.Print($"CardManager[PID:{processId}]: End-of-round timer started successfully - timerActive: {timerActive}, WaitTime: {turnTimer.WaitTime}");
        }
        else
        {
            GD.Print($"CardManager[PID:{processId}]: Not starting timer - waiting for host sync");
        }

        // Log sync status
        if (matchManager?.HasActiveMatch == true)
        {
            if (matchManager.IsLocalPlayerMatchOwner())
            {
                GD.Print($"CardManager[PID:{processId}]: NAKAMA MATCH OWNER - end-of-round state started");
            }
            else
            {
                GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - end-of-round state started");
            }
        }

        GD.Print($"CardManager[PID:{processId}]: ========== END-OF-ROUND TURN STARTED ==========");
    }

    /// <summary>
    /// Complete the end-of-round phase and continue to next trick
    /// </summary>
    private void CompleteEndOfRoundTurn()
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        GD.Print($"CardManager[PID:{processId}]: ========== COMPLETING END-OF-ROUND TURN ==========");
        GD.Print($"CardManager[PID:{processId}]: Completing end-of-round turn - cleaning up trick and continuing");
        GD.Print($"CardManager[PID:{processId}]: Current trick has {CurrentTrick.Count} cards");

        // Now do the actual trick completion cleanup
        CurrentTrick.Clear();
        TricksPlayed++;
        CurrentTurnState = TurnState.PlayerTurn;
        CurrentTrickWinner = -1; // Reset

        GD.Print($"CardManager[PID:{processId}]: Trick cleared, TricksPlayed: {TricksPlayed}, State: {CurrentTurnState}");

        // Emit signal to clean up UI
        EmitSignal(SignalName.EndOfRoundCompleted);
        GD.Print($"CardManager[PID:{processId}]: EndOfRoundCompleted signal emitted");

        // Check if hand is complete
        if (PlayerHands[PlayerOrder[0]].Count == 0)
        {
            GD.Print($"CardManager[PID:{processId}]: Hand complete after end-of-round cleanup - calling CompleteHand()");
            CompleteHand();
        }
        else
        {
            GD.Print($"CardManager[PID:{processId}]: Starting next trick after end-of-round cleanup - calling StartTurn()");
            StartTurn();
        }

        GD.Print($"CardManager[PID:{processId}]: ========== END-OF-ROUND TURN COMPLETED ==========");
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

        // Store winner and enter end-of-round state (same as host)
        CurrentTrickWinner = winnerId;
        CurrentTurnState = TurnState.EndOfRound;

        // Update trick leader and current turn (but don't start yet)
        CurrentTrickLeader = newTrickLeader;
        CurrentPlayerTurn = newCurrentTurn;

        // DON'T clear trick yet - wait for end-of-round completion
        // CurrentTrick.Clear();
        // TricksPlayed++;

        // Emit signal for UI updates
        EmitSignal(SignalName.TrickCompleted, winnerId);

        // Start end-of-round phase on client
        StartEndOfRoundTurn();

        GD.Print($"CardManager: CLIENT trick synchronized and entered end-of-round state - Leader: {CurrentTrickLeader}, Turn: {CurrentPlayerTurn}");
    }

    /// <summary>
    /// Complete the current hand and check for game end
    /// </summary>
    private void CompleteHand()
    {
        GD.Print($"========== HAND COMPLETED ==========");
        GD.Print($"CardManager: Hand completed after {TricksPlayed} tricks");

        // Log current scores
        foreach (var score in PlayerScores)
        {
            GD.Print($"CardManager: Player {score.Key} score: {score.Value}");
        }

        int maxScore = PlayerScores.Values.Max();
        GD.Print($"CardManager: Highest score: {maxScore}, Target: {TargetScore}");

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
            GD.Print($"CardManager: Game ending - player reached target score of {TargetScore}");
            CompleteGame();
        }
        else
        {
            GD.Print($"CardManager: Game continuing - dealing next hand (no player has {TargetScore} points yet)");
            // Deal next hand
            CurrentDealer = GetNextDealer();

            // 🔥 CRITICAL FIX: Reset turn state for new hand
            CurrentTurnState = TurnState.PlayerTurn;
            CurrentTrickWinner = -1;

            GD.Print($"CardManager: New dealer: {CurrentDealer}, Turn state reset to PlayerTurn");
            DealNewHand();
        }
    }

    /// <summary>
    /// Complete the game
    /// </summary>
    private void CompleteGame()
    {
        GD.Print($"========== GAME COMPLETED ==========");

        // Find overall winner
        int highestScore = PlayerScores.Values.Max();
        int winnerId = PlayerScores.First(kvp => kvp.Value == highestScore).Key;

        GD.Print($"CardManager: GAME WON by player {winnerId} with {highestScore} points");
        GD.Print($"CardManager: Setting GameInProgress = false");

        GameInProgress = false;

        EmitSignal(SignalName.GameCompleted, winnerId);

        GD.Print($"CardManager: ========== GAME COMPLETED SIGNAL SENT ==========");
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
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        GD.Print($"CardManager[PID:{processId}]: ⏰ ========== TIMER EXPIRED ==========");
        GD.Print($"CardManager[PID:{processId}]: OnTurnTimerExpired called - timerActive: {timerActive}, GameInProgress: {GameInProgress}");
        GD.Print($"CardManager[PID:{processId}]: Current turn state: {CurrentTurnState}");
        GD.Print($"CardManager[PID:{processId}]: Current game state - CurrentPlayerTurn: {CurrentPlayerTurn}, PlayerOrder.Count: {PlayerOrder.Count}");

        // 🔥 CORRECTED: Clients can handle timer expiration for THEIR OWN turns
        // But prevent timer spam by having only host manage authoritative timing
        // Clients will auto-forfeit themselves, host will auto-forfeit AI players

        if (!timerActive || !GameInProgress)
        {
            GD.Print($"CardManager[PID:{processId}]: Timer expired but timerActive={timerActive}, GameInProgress={GameInProgress} - ignoring");
            return;
        }

        // 🔥 NEW: Handle end-of-round timer expiration
        if (CurrentTurnState == TurnState.EndOfRound)
        {
            GD.Print($"CardManager[PID:{processId}]: ========== END-OF-ROUND TIMER EXPIRED ==========");
            GD.Print($"CardManager[PID:{processId}]: End-of-round timer expired - completing end-of-round phase");
            GD.Print($"CardManager[PID:{processId}]: Current trick winner: {CurrentTrickWinner}");
            timerActive = false;
            CompleteEndOfRoundTurn();
            GD.Print($"CardManager[PID:{processId}]: ========== END-OF-ROUND CLEANUP COMPLETED ==========");
            return;
        }

        // CRITICAL FIX: Validate that we're still waiting for the current player's turn
        // If CurrentTrick already has a card from this player, they already played
        if (CurrentPlayerTurn >= PlayerOrder.Count)
        {
            GD.PrintErr($"CardManager[PID:{processId}]: Timer expired but CurrentPlayerTurn {CurrentPlayerTurn} >= PlayerOrder.Count {PlayerOrder.Count} - invalid state");
            timerActive = false;
            return;
        }

        int currentPlayerId = PlayerOrder[CurrentPlayerTurn];
        bool playerAlreadyPlayed = CurrentTrick.Any(cardPlay => cardPlay.PlayerId == currentPlayerId);

        GD.Print($"CardManager[PID:{processId}]: Timer expired for player {currentPlayerId}, playerAlreadyPlayed: {playerAlreadyPlayed}");
        GD.Print($"CardManager[PID:{processId}]: CurrentTrick has {CurrentTrick.Count} cards: [{string.Join(", ", CurrentTrick.Select(cp => $"P{cp.PlayerId}:{cp.Card}"))}]");

        if (playerAlreadyPlayed)
        {
            GD.Print($"CardManager[PID:{processId}]: Timer expired but player {currentPlayerId} already played their card - ignoring timeout");
            timerActive = false; // Ensure timer state is clean
            return;
        }

        GD.Print($"CardManager[PID:{processId}]: Turn timer expired for player {currentPlayerId} - executing auto-forfeit");

        timerActive = false;

        // Check if player is at table - affects forfeit behavior
        var gameManager = GameManager.Instance;
        bool isPlayerAtTable = gameManager?.IsPlayerAtTable(currentPlayerId) ?? true;

        GD.Print($"CardManager[PID:{processId}]: Player {currentPlayerId} isPlayerAtTable: {isPlayerAtTable}");

        if (isPlayerAtTable)
        {
            // 🔥 CRITICAL FIX: Only auto-forfeit if this is the local player's instance
            // Prevents race condition where host auto-forfeits for clients before clients can auto-forfeit themselves
            var matchManager = MatchManager.Instance;
            bool shouldAutoForfeit = false;
            string reason = "";

            if (matchManager?.HasActiveMatch == true)
            {
                // For Nakama games: Determine who should handle auto-forfeit
                var nakamaManager = NakamaManager.Instance;
                var localUserId = nakamaManager?.Session?.UserId;
                var playerUserId = GetNakamaUserIdForGamePlayer(currentPlayerId);

                if (currentPlayerId >= 100)
                {
                    // AI player - match owner should handle auto-forfeit
                    shouldAutoForfeit = matchManager.IsLocalPlayerMatchOwner();
                    reason = shouldAutoForfeit ? "AI player, local is match owner" : "AI player, not match owner";
                }
                else if (!string.IsNullOrEmpty(localUserId) && !string.IsNullOrEmpty(playerUserId))
                {
                    // Human player - only auto-forfeit if this is their own instance
                    shouldAutoForfeit = (localUserId == playerUserId);
                    reason = shouldAutoForfeit ? "local player's own instance" : "different player's instance";
                }
                else
                {
                    // Fallback: match owner handles if we can't determine ownership
                    shouldAutoForfeit = matchManager.IsLocalPlayerMatchOwner();
                    reason = shouldAutoForfeit ? "fallback - match owner" : "fallback - not match owner";
                }
            }
            else
            {
                // Traditional network or offline game - use existing logic
                var networkManager = gameManager?.NetworkManager;
                shouldAutoForfeit = (networkManager == null || !networkManager.IsConnected || networkManager.IsHost);
                reason = shouldAutoForfeit ? "traditional network/offline - host/local" : "traditional network - client";
            }

            GD.Print($"CardManager[PID:{processId}]: Auto-forfeit check for player {currentPlayerId}: {shouldAutoForfeit} ({reason})");

            if (shouldAutoForfeit)
            {
                // Auto-forfeit with lowest card
                GD.Print($"CardManager[PID:{processId}]: Player {currentPlayerId} at table - auto-forfeiting with lowest card");
                var validCards = GetValidCards(currentPlayerId);
                if (validCards.Count > 0)
                {
                    // Find the lowest card to forfeit
                    Card cardToPlay = validCards.OrderBy(c => c.GetValue()).First();
                    GD.Print($"CardManager[PID:{processId}]: Auto-forfeiting player {currentPlayerId} with card {cardToPlay}");

                    // 🔥 DEBUG: Log hand contents before auto-forfeit
                    var handBefore = PlayerHands[currentPlayerId];
                    var cardsBefore = string.Join(", ", handBefore.Select(c => c.ToString()));
                    GD.Print($"CardManager[PID:{processId}]: 🃏 Player {currentPlayerId} hand BEFORE auto-forfeit: {handBefore.Count} cards [{cardsBefore}]");

                    // 🔥 CRITICAL FIX: Use PlayCard() instead of ExecuteCardPlay() to ensure proper network synchronization
                    // This ensures all clients receive notification that the card was auto-played
                    bool success = PlayCard(currentPlayerId, cardToPlay);

                    // 🔥 DEBUG: Log hand contents after auto-forfeit
                    var handAfter = PlayerHands.ContainsKey(currentPlayerId) ? PlayerHands[currentPlayerId] : new List<Card>();
                    var cardsAfter = string.Join(", ", handAfter.Select(c => c.ToString()));
                    GD.Print($"CardManager[PID:{processId}]: 🃏 Player {currentPlayerId} hand AFTER auto-forfeit: {handAfter.Count} cards [{cardsAfter}]");
                    GD.Print($"CardManager[PID:{processId}]: 🃏 Auto-forfeit success: {success}, Card removed: {!handAfter.Contains(cardToPlay)}");
                    if (!success)
                    {
                        GD.PrintErr($"CardManager[PID:{processId}]: Failed to auto-forfeit card {cardToPlay} for player {currentPlayerId}");

                        // 🔥 CRITICAL FIX: Don't call EndTurn() when auto-forfeit fails
                        // This prevents client from progressing turn locally without syncing to host
                        // Instead, let the host handle the timeout situation
                        if (MatchManager.Instance?.HasActiveMatch == true && !MatchManager.Instance.IsLocalPlayerMatchOwner())
                        {
                            GD.Print($"CardManager[PID:{processId}]: CLIENT auto-forfeit failed - NOT calling EndTurn() to prevent desync");
                            // Host will handle this timeout when its timer expires
                        }
                        else
                        {
                            // Host can still call EndTurn() as fallback
                            EndTurn();
                        }
                    }
                    else
                    {
                        // 🔥 CRITICAL FIX: For clients, ensure turn is ended after auto-forfeit
                        // PlayCard() will handle EndTurn() for the client's own card play
                        GD.Print($"CardManager[PID:{processId}]: Auto-forfeit successful for player {currentPlayerId}");
                    }
                }
                else
                {
                    GD.PrintErr($"CardManager[PID:{processId}]: Player {currentPlayerId} at table has no valid cards to auto-play");
                    EndTurn();
                }
            }
            else
            {
                GD.Print($"CardManager[PID:{processId}]: Skipping auto-forfeit for player {currentPlayerId} - {reason}");
            }
        }
        else
        {
            // Player not at table - auto-play any valid card
            GD.Print($"CardManager[PID:{processId}]: Player {currentPlayerId} not at table - auto-playing any valid card");
            var validCards = GetValidCards(currentPlayerId);
            if (validCards.Count > 0)
            {
                var random = new Random();
                Card cardToPlay = validCards[random.Next(validCards.Count)];
                GD.Print($"CardManager[PID:{processId}]: Auto-playing card {cardToPlay} for player {currentPlayerId}");

                // 🔥 CRITICAL FIX: Use PlayCard() instead of ExecuteCardPlay() to ensure proper network synchronization  
                // This ensures all clients receive notification that the card was auto-played
                bool success = PlayCard(currentPlayerId, cardToPlay);
                if (!success)
                {
                    GD.PrintErr($"CardManager[PID:{processId}]: Failed to auto-play card {cardToPlay} for player {currentPlayerId}");
                    EndTurn(); // Fallback: just end turn if card play fails
                }
            }
            else
            {
                GD.PrintErr($"CardManager[PID:{processId}]: Player {currentPlayerId} not at table has no valid cards to auto-play");
                EndTurn();
            }
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
        // 🔥 CRITICAL FIX: Return a NEW list to prevent UI desync issues
        // The UI was getting a reference to the same list that CardManager modifies,
        // causing the UI's currentPlayerCards to change without proper update calls
        var originalHand = PlayerHands.GetValueOrDefault(playerId, new List<Card>());
        return new List<Card>(originalHand); // Return a COPY, not the original reference
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
    /// ENHANCED VERSION - better validation and handles immediate host execution
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <param name="suit">Card suit as string</param>
    /// <param name="rank">Card rank as string</param>
    private void OnNakamaCardPlayReceived(int playerId, string suit, string rank)
    {
        GD.Print($"CardManager: OnNakamaCardPlayReceived called - Player {playerId}: {rank} of {suit}");

        // Convert strings back to enum types
        if (!Enum.TryParse<Suit>(suit, out var cardSuit) || !Enum.TryParse<Rank>(rank, out var cardRank))
        {
            GD.PrintErr($"CardManager: Failed to parse card from Nakama - Suit: {suit}, Rank: {rank}");
            return;
        }

        var card = new Card(cardSuit, cardRank);

        GD.Print($"CardManager: Received card play from Nakama - Player {playerId}: {card}");

        // 🔥 CRITICAL FIX: Skip execution if this is our own card play (we already executed it immediately)
        // The issue was using Game Player IDs instead of Nakama User IDs for filtering
        var matchManager = MatchManager.Instance;
        var nakamaManager = NakamaManager.Instance;
        bool isOwnCardPlay = false;

        if (matchManager?.HasActiveMatch == true && nakamaManager?.IsAuthenticated == true)
        {
            var localUserId = nakamaManager?.Session?.UserId;

            if (!string.IsNullOrEmpty(localUserId))
            {
                // For AI players (ID >= 100), they don't have user IDs, so always let them through
                bool isAIPlayer = (playerId >= 100);

                if (!isAIPlayer)
                {
                    // 🔥 FIXED: Use Nakama User ID mapping instead of Game Player ID
                    // Get the Nakama User ID that corresponds to this game player ID
                    var senderUserId = GetNakamaUserIdForGamePlayer(playerId);

                    if (!string.IsNullOrEmpty(senderUserId))
                    {
                        // Only skip if the sender's Nakama User ID matches our local Nakama User ID
                        isOwnCardPlay = (localUserId == senderUserId);
                        GD.Print($"CardManager[PID:{OS.GetProcessId()}]: OnNakama filtering - Player {playerId}, LocalUserId: {localUserId}, SenderUserId: {senderUserId}, willSkip: {isOwnCardPlay}");
                    }
                    else
                    {
                        // If we can't determine the sender's User ID, don't skip (safer default)
                        GD.Print($"CardManager[PID:{OS.GetProcessId()}]: OnNakama filtering - Player {playerId}, couldn't determine sender User ID, not skipping");
                    }
                }
                else
                {
                    GD.Print($"CardManager[PID:{OS.GetProcessId()}]: OnNakama filtering - Player {playerId} is AI, not skipping");
                }
            }
        }

        if (isOwnCardPlay)
        {
            GD.Print($"CardManager: Skipping own HUMAN card play - Player {playerId}: {card}");
            return;
        }

        // Execute the card play for other players
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

        var matchManager = MatchManager.Instance;
        var gameManager = GameManager.Instance;

        // 🔥 CRITICAL FIX: Distinguish between host's own cards vs other players' cards
        bool isOwnCardPlay = false;
        if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager: DEBUG - Host processing card play for Player {playerId}");

            // Check if this is the host's own card play (human) coming back
            bool isOwnHumanCard = false;
            if (gameManager?.LocalPlayer != null)
            {
                isOwnHumanCard = (gameManager.LocalPlayer.PlayerId == playerId);
                GD.Print($"CardManager: DEBUG - isOwnHumanCard: {isOwnHumanCard} (LocalPlayer.PlayerId: {gameManager.LocalPlayer.PlayerId}, playerId: {playerId})");
            }

            // 🔥 CRITICAL FIX: Use PlayerId range to detect AI players (more reliable than GameManager lookup)
            // AI players have IDs 100+ and are controlled by the host
            bool isOwnAICard = (playerId >= 100);
            GD.Print($"CardManager: DEBUG - isOwnAICard: {isOwnAICard} (playerId: {playerId} >= 100)");

            isOwnCardPlay = isOwnHumanCard || isOwnAICard;
            GD.Print($"CardManager: DEBUG - isOwnCardPlay: {isOwnCardPlay} (isOwnHumanCard: {isOwnHumanCard}, isOwnAICard: {isOwnAICard})");
        }
        else
        {
            GD.Print($"CardManager: DEBUG - Not processing as host card play (HasActiveMatch: {matchManager?.HasActiveMatch}, IsMatchOwner: {matchManager?.IsLocalPlayerMatchOwner()})");
        }

        if (isOwnCardPlay)
        {
            // Host receiving its own card play back - just sync the signal for consistency
            GD.Print($"CardManager: NAKAMA MATCH OWNER - own card already executed locally, just syncing signal");
            EmitSignal(SignalName.CardPlayed, playerId, card.ToString());

            // 🔥 REMOVED: AI turn progression logic since AI now progresses immediately in PlayCard
            // No additional turn progression needed here

            return; // Don't execute again
        }

        // All other cases: execute the card play
        GD.Print($"CardManager: Executing card play from {(matchManager?.IsLocalPlayerMatchOwner() == true ? "client" : "host")} - Player {playerId}: {card}");

        // Remove card from player's hand
        if (PlayerHands.ContainsKey(playerId) && PlayerHands[playerId].Contains(card))
        {
            PlayerHands[playerId].Remove(card);
        }

        // Add to current trick
        CurrentTrick.Add(new CardPlay(playerId, card));

        // Emit signal for UI updates
        EmitSignal(SignalName.CardPlayed, playerId, card.ToString());

        // 🔥 CRITICAL FIX: Host should progress turns for other players' card plays
        if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player {playerId}");
            EndTurn();
        }
        else
        {
            GD.Print($"CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner");
            // Clients don't progress turns - match owner handles turn progression
        }

        GD.Print($"CardManager: Synchronized card play completed - Player {playerId}: {card}");
    }

    /// <summary>
    /// Handle trick completion synchronization from Nakama (for all instances)
    /// </summary>
    /// <param name="winnerId">Player who won the trick</param>
    /// <param name="newTrickLeader">New trick leader index</param>
    /// <param name="winnerScore">Updated score for the winner</param>
    private void OnNakamaTrickCompletedReceived(int winnerId, int newTrickLeader, int winnerScore)
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        var matchManager = MatchManager.Instance;

        GD.Print($"CardManager[PID:{processId}]: OnNakamaTrickCompletedReceived called - Winner: {winnerId}, Leader: {newTrickLeader}, Score: {winnerScore}");

        // For match owner, this is their own trick completion echoed back - just skip
        if (matchManager?.IsLocalPlayerMatchOwner() == true)
        {
            GD.Print($"CardManager[PID:{processId}]: NAKAMA MATCH OWNER - skipping own trick completion");
            return;
        }

        GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - synchronizing trick completion from match owner");

        // Update scores
        if (PlayerScores.ContainsKey(winnerId))
        {
            PlayerScores[winnerId] = winnerScore;
        }

        // Store winner and enter end-of-round state (same as host and traditional clients)
        CurrentTrickWinner = winnerId;
        CurrentTurnState = TurnState.EndOfRound;

        // Update trick leader and current turn (but don't start yet)
        CurrentTrickLeader = newTrickLeader;
        CurrentPlayerTurn = CurrentTrickLeader;

        // DON'T clear trick yet - wait for end-of-round completion
        // CurrentTrick.Clear();
        // TricksPlayed++;

        // Emit signal for UI updates
        EmitSignal(SignalName.TrickCompleted, winnerId);

        // Start end-of-round phase on Nakama client
        StartEndOfRoundTurn();

        GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: {CurrentTrickLeader}, Turn: {CurrentPlayerTurn}");
    }

    /// <summary>
    /// Handle timer update synchronization from Nakama (for non-match-owner instances)
    /// </summary>
    /// <param name="timeRemaining">Time remaining in seconds</param>
    private void OnNakamaTimerUpdateReceived(float timeRemaining)
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        var matchManager = MatchManager.Instance;

        // Only clients should sync timer updates from match owner
        if (matchManager?.IsLocalPlayerMatchOwner() == true)
        {
            return; // Match owner doesn't need to sync its own timer updates
        }

        // 🔥 CRITICAL FIX: Don't sync timer updates during end-of-round state
        // The client is managing its own end-of-round timer and shouldn't be interrupted
        if (CurrentTurnState == TurnState.EndOfRound)
        {
            GD.Print($"CardManager[PID:{processId}]: IGNORING timer update during end-of-round state (timeRemaining: {timeRemaining:F1}s)");
            return;
        }

        // Only log timer sync occasionally to avoid spam (clients now manage their own timers)
        if (timeRemaining <= 1.0f || (timeRemaining % 5.0f < 0.2f))
        {
            GD.Print($"CardManager[PID:{processId}]: Client timer synced to: {timeRemaining:F1}s");
        }

        // Update the local timer state for clients
        networkTurnTimeRemaining = timeRemaining;
        EmitSignal(SignalName.TurnTimerUpdated, timeRemaining);
    }

    /// <summary>
    /// Handle turn change synchronization from Nakama (for non-match-owner instances)
    /// </summary>
    /// <param name="currentPlayerTurn">Current player turn index</param>
    /// <param name="tricksPlayed">Number of tricks played</param>
    private void OnNakamaTurnChangeReceived(int currentPlayerTurn, int tricksPlayed)
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        var matchManager = MatchManager.Instance;

        GD.Print($"CardManager[PID:{processId}]: OnNakamaTurnChangeReceived called - PlayerTurn: {currentPlayerTurn}, Tricks: {tricksPlayed}");

        // 🔥 CRITICAL FIX: Prevent duplicate turn change processing
        if (currentPlayerTurn == lastProcessedTurn && tricksPlayed == lastProcessedTricks)
        {
            GD.Print($"CardManager[PID:{processId}]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: {currentPlayerTurn}, Tricks: {tricksPlayed}) - Last processed: Turn {lastProcessedTurn}, Tricks {lastProcessedTricks}");
            return;
        }

        // Update deduplication tracking
        GD.Print($"CardManager[PID:{processId}]: ✅ Processing NEW turn change (Turn: {currentPlayerTurn}, Tricks: {tricksPlayed}) - Previous: Turn {lastProcessedTurn}, Tricks {lastProcessedTricks}");
        lastProcessedTurn = currentPlayerTurn;
        lastProcessedTricks = tricksPlayed;

        GD.Print($"CardManager[PID:{processId}]: Current state before sync - CurrentPlayerTurn: {CurrentPlayerTurn}, PlayerOrder: [{string.Join(", ", PlayerOrder)}]");

        // Only non-match-owner instances should sync turn changes
        if (matchManager?.HasActiveMatch == true && !matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - synchronizing turn change");

            // 🔥 CRITICAL FIX: Stop any active timer before processing turn change
            // This prevents timer conflicts when AI players' turns end without calling EndTurn() on client
            if (turnTimer != null && timerActive)
            {
                var timeLeft = turnTimer?.TimeLeft ?? 0.0;
                GD.Print($"CardManager[PID:{processId}]: 🛑 Stopping active timer before turn change (had {timeLeft:F1}s remaining)");
                turnTimer.Stop();
                timerActive = false;
            }

            // Validate the turn index is within bounds
            if (currentPlayerTurn >= 0 && currentPlayerTurn < PlayerOrder.Count)
            {
                // 🔥 CRITICAL FIX: Force reset turn state - client may be stuck in EndOfRound
                if (CurrentTurnState != TurnState.PlayerTurn)
                {
                    GD.Print($"CardManager[PID:{processId}]: 🔄 FORCE RESET - Client stuck in {CurrentTurnState}, resetting to PlayerTurn for new turn");
                    CurrentTurnState = TurnState.PlayerTurn;
                    CurrentTrickWinner = -1; // Clear end-of-round data
                }

                // Update local game state
                int previousTurn = CurrentPlayerTurn;
                CurrentPlayerTurn = currentPlayerTurn;
                TricksPlayed = tricksPlayed;

                int currentPlayerId = PlayerOrder[CurrentPlayerTurn];
                GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - turn synchronized: {previousTurn} -> {CurrentPlayerTurn} (Player {currentPlayerId})");

                // 🔥 CRITICAL FIX: If this is our own turn, call StartTurn() to manage timer locally
                var gameManager = GameManager.Instance;
                bool isOurTurn = (gameManager?.LocalPlayer?.PlayerId == currentPlayerId);

                if (isOurTurn)
                {
                    // 🔥 CORRECTED: Clients DO start timers for their own turns (needed for auto-forfeit)
                    GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer");
                    StartTurn();
                }
                else
                {
                    // Just emit signal for other players' turns
                    EmitSignal(SignalName.TurnStarted, currentPlayerId);
                    GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - TurnStarted signal emitted for player {currentPlayerId}");
                }
            }
            else
            {
                GD.PrintErr($"CardManager[PID:{processId}]: NAKAMA CLIENT - Invalid turn index {currentPlayerTurn}, PlayerOrder.Count: {PlayerOrder.Count}");
            }
        }
        else if (matchManager?.HasActiveMatch == true && matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager[PID:{processId}]: NAKAMA MATCH OWNER - ignoring turn change sync (managing turns locally)");
        }
        else
        {
            GD.Print($"CardManager[PID:{processId}]: No active Nakama match - ignoring turn change sync");
        }
    }

    /// <summary>
    /// Handle card dealing synchronization from Nakama
    /// </summary>
    private void OnNakamaCardsDealt()
    {
        var processId = OS.GetProcessId();
        GD.Print($"🔄 CardManager[PID:{processId}]: OnNakamaCardsDealt method called!");

        var matchManager = MatchManager.Instance;
        if (matchManager == null)
        {
            GD.PrintErr($"CardManager[PID:{processId}]: OnNakamaCardsDealt - MatchManager is null!");
            return;
        }

        GD.Print($"CardManager[PID:{processId}]: OnNakamaCardsDealt called - HasActiveMatch: {matchManager.HasActiveMatch}");
        GD.Print($"CardManager[PID:{processId}]: Local player match owner check...");

        // Only non-match-owner instances should sync dealt cards
        if (matchManager.HasActiveMatch && !matchManager.IsLocalPlayerMatchOwner())
        {
            GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - synchronizing dealt cards from match owner");

            var dealtCards = matchManager.LastDealtCards;
            GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - received {dealtCards.Count} player hands");

            if (dealtCards.Count == 0)
            {
                GD.PrintErr($"CardManager[PID:{processId}]: NAKAMA CLIENT - No dealt cards received!");
                return;
            }

            bool anyHandsUpdated = false;
            foreach (var kvp in dealtCards)
            {
                int playerId = kvp.Key;
                var cardStrings = kvp.Value;

                GD.Print($"CardManager[PID:{processId}]: Processing hand for player {playerId} - {cardStrings.Count} cards");

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
                        GD.PrintErr($"CardManager[PID:{processId}]: Failed to parse card string: {cardString}");
                    }
                }

                // Update player's hand
                if (PlayerHands.ContainsKey(playerId))
                {
                    PlayerHands[playerId] = cards;
                    GD.Print($"CardManager[PID:{processId}]: CLIENT hand updated for player {playerId} - {cards.Count} cards");
                    anyHandsUpdated = true;
                }
                else
                {
                    GD.PrintErr($"CardManager[PID:{processId}]: Received cards dealt for player {playerId} but they are not in PlayerHands!");
                    GD.Print($"CardManager[PID:{processId}]: Available PlayerHands keys: [{string.Join(", ", PlayerHands.Keys)}]");
                }
            }

            // 🔥 CRITICAL FIX: Clear pending card plays for Nakama clients too
            pendingCardPlays.Clear();
            GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT cleared pending card plays for new hand");

            if (anyHandsUpdated)
            {
                // Trigger overall UI update
                EmitSignal(SignalName.HandDealt);
                GD.Print($"CardManager[PID:{processId}]: NAKAMA CLIENT - card dealing synchronization completed, UI updated");
            }
            else
            {
                GD.PrintErr($"CardManager[PID:{processId}]: NAKAMA CLIENT - No hands were updated!");
            }
        }
        else
        {
            if (!matchManager.HasActiveMatch)
            {
                GD.Print($"CardManager[PID:{processId}]: OnNakamaCardsDealt - No active match");
            }
            else
            {
                GD.Print($"CardManager[PID:{processId}]: Ignoring cards dealt sync - local player IS the match owner, cards dealt locally");
            }
        }
    }

    /// <summary>
    /// Get the Nakama User ID that corresponds to a game player ID
    /// CRITICAL FIX: This solves the card sync issue by properly mapping player IDs
    /// </summary>
    /// <param name="gamePlayerId">Game player ID (0, 2, 100, 101, etc.)</param>
    /// <returns>Nakama User ID if found, null otherwise</returns>
    private string GetNakamaUserIdForGamePlayer(int gamePlayerId)
    {
        var matchManager = MatchManager.Instance;
        if (matchManager?.Players == null) return null;

        // For AI players, they don't have Nakama User IDs
        if (gamePlayerId >= 100) return null;

        // Convert the Nakama players to a sorted list to match the CardGameUI assignment logic
        var sortedPlayers = matchManager.Players.Keys.OrderBy(k => k).ToList();

        // Find the player index that maps to this game player ID
        // Game player IDs are assigned as playerIndex * 2 in CardGameUI
        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            int assignedGamePlayerId = i * 2;
            if (assignedGamePlayerId == gamePlayerId)
            {
                var nakamaUserId = sortedPlayers[i];
                GD.Print($"CardManager[PID:{OS.GetProcessId()}]: Mapped game player {gamePlayerId} to Nakama user {nakamaUserId}");
                return nakamaUserId;
            }
        }

        GD.PrintErr($"CardManager[PID:{OS.GetProcessId()}]: Could not find Nakama User ID for game player {gamePlayerId}");
        return null;
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
