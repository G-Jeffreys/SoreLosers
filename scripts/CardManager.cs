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
        GD.Print("CardManager: Initializing card game system...");

        // Initialize turn timer
        turnTimer = new Timer();
        turnTimer.WaitTime = TurnDuration;
        turnTimer.OneShot = true;
        turnTimer.Timeout += OnTurnTimerExpired;
        AddChild(turnTimer);

        // Initialize deck
        InitializeDeck();

        GD.Print("CardManager: Card game system initialized successfully");
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
        GD.Print("CardManager: Initializing standard 52-card deck...");

        Deck.Clear();

        // Create cards for each suit and rank
        foreach (Suit suit in Enum.GetValues<Suit>())
        {
            foreach (Rank rank in Enum.GetValues<Rank>())
            {
                Deck.Add(new Card(suit, rank));
            }
        }

        GD.Print($"CardManager: Deck initialized with {Deck.Count} cards");
    }

    /// <summary>
    /// Shuffle the deck using Fisher-Yates algorithm
    /// </summary>
    private void ShuffleDeck()
    {
        GD.Print("CardManager: Shuffling deck...");

        // Use deterministic seed for testing so both instances have same shuffle
        int seed = 12345; // Fixed seed for consistent deck across instances
        var random = new Random(seed);

        for (int i = Deck.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (Deck[i], Deck[j]) = (Deck[j], Deck[i]);
        }

        GD.Print("CardManager: Deck shuffled with deterministic seed");
    }

    /// <summary>
    /// Start a new card game with given players (host-authoritative)
    /// </summary>
    /// <param name="playerIds">List of player IDs in turn order</param>
    public void StartGame(List<int> playerIds)
    {
        var gameManager = GameManager.Instance;
        var networkManager = gameManager?.NetworkManager;

        // If networked, only host should start the game and broadcast to clients
        if (networkManager != null && networkManager.IsConnected)
        {
            if (networkManager.IsHost)
            {
                GD.Print("CardManager: Host starting networked game");

                // Convert to array for RPC
                int[] playerIdArray = playerIds.ToArray();

                // Broadcast game start to all clients (including self)
                Rpc(MethodName.NetworkStartGame, playerIdArray);
            }
            else
            {
                GD.Print("CardManager: Client ignoring game start - waiting for host");
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
    /// </summary>
    /// <param name="playerIds">Array of player IDs</param>
    [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void NetworkStartGame(int[] playerIds)
    {
        GD.Print($"CardManager: Received network game start with {playerIds.Length} players");
        ExecuteGameStart(playerIds.ToList());
    }

    /// <summary>
    /// Execute the actual game start logic
    /// </summary>
    /// <param name="playerIds">List of player IDs in turn order</param>
    private void ExecuteGameStart(List<int> playerIds)
    {
        GD.Print($"CardManager: Starting new game with {playerIds.Count} players");

        if (playerIds.Count < 2 || playerIds.Count > 4)
        {
            GD.PrintErr("CardManager: Invalid player count. Need 2-4 players.");
            return;
        }

        // Initialize game state
        PlayerOrder = new List<int>(playerIds);
        CurrentPlayerTurn = 0;
        CurrentDealer = 0;
        CurrentTrickLeader = 0;
        GameInProgress = true;
        TricksPlayed = 0;

        // Initialize player hands and scores
        PlayerHands.Clear();
        PlayerScores.Clear();

        foreach (int playerId in playerIds)
        {
            PlayerHands[playerId] = new List<Card>();
            PlayerScores[playerId] = 0;
        }

        // Deal first hand
        DealNewHand();

        GD.Print("CardManager: Game started successfully");
    }

    /// <summary>
    /// Deal a new hand of cards to all players
    /// </summary>
    private void DealNewHand()
    {
        GD.Print("CardManager: Dealing new hand...");

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

            GD.Print($"CardManager: Dealt {PlayerHands[playerId].Count} cards to player {playerId}");
        }

        // Notify UI that cards have been dealt
        EmitSignal(SignalName.HandDealt);
        GD.Print("CardManager: Hand dealt, UI notified");

        // Start first trick
        CurrentTrickLeader = GetNextDealer();
        CurrentPlayerTurn = CurrentTrickLeader;

        StartTurn();
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
            GD.Print($"CardManager: Client waiting for host turn management for player {currentPlayerId}");
            EmitSignal(SignalName.TurnStarted, currentPlayerId);
            return;
        }

        GD.Print($"CardManager: Starting turn for player {currentPlayerId}");

        // Check if this is an AI player and auto-play if so
        if (gameManager != null)
        {
            var playerData = gameManager.GetPlayer(currentPlayerId);
            if (playerData != null && playerData.PlayerName.StartsWith("AI_"))
            {
                GD.Print($"CardManager: AI turn detected, auto-playing for {playerData.PlayerName}");

                // Auto-play after consistent delay for synchronization
                GetTree().CreateTimer(0.5f).Timeout += () => AutoPlayAITurn(currentPlayerId);

                // Broadcast turn start to clients
                if (networkManager != null && networkManager.IsConnected)
                {
                    Rpc(MethodName.NetworkTurnStarted, currentPlayerId);
                }

                EmitSignal(SignalName.TurnStarted, currentPlayerId);
                return;
            }
        }

        // Start turn timer for human players (host only)
        if (turnTimer != null)
        {
            timerActive = true;
            turnTimer.Start();
            GD.Print($"CardManager: Turn timer started ({TurnDuration}s)");
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
        GD.Print($"CardManager: Received network turn start for player {playerId}");
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
        GD.Print($"CardManager: Ending turn for player {currentPlayerId}");

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
                GD.Print($"CardManager: Broadcasting card play for local player {playerId}: {card}");

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

        GD.Print($"CardManager: Received network card play - Player {playerId}: {card}");

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
        GD.Print($"CardManager: Player {playerId} attempting to play {card}");

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

        GD.Print($"CardManager: Card {card} played successfully by player {playerId}");

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
            GD.Print($"CardManager: Player {playerId} must follow suit {ledSuit}");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Complete the current trick and determine winner
    /// </summary>
    private void CompleteTrick()
    {
        GD.Print("CardManager: Completing trick...");

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
        GD.Print("CardManager: Completing hand...");

        // Log final scores for this hand
        foreach (var kvp in PlayerScores)
        {
            GD.Print($"CardManager: Player {kvp.Key} scored {kvp.Value} points");
        }

        EmitSignal(SignalName.HandCompleted);

        // Find hand loser (lowest score) for chat intimidation
        int lowestScore = PlayerScores.Values.Min();
        var losers = PlayerScores.Where(kvp => kvp.Value == lowestScore).Select(kvp => kvp.Key).ToList();

        // Apply chat intimidation to losers
        foreach (int loserId in losers)
        {
            GD.Print($"CardManager: Player {loserId} loses hand with {lowestScore} points");

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
            GD.Print($"CardManager: Player {winnerId} wins hand with {highestScore} points");

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
        GD.Print("CardManager: Completing game...");

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
            GD.Print($"CardManager: Player {currentPlayerId} missed turn - currently in kitchen");
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

            GD.Print($"CardManager: Auto-forfeiting player {currentPlayerId} at table with card {cardToPlay}");
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
        if (!GameInProgress || PlayerOrder[CurrentPlayerTurn] != playerId)
        {
            GD.PrintErr($"CardManager: Cannot auto-play for player {playerId} - not their turn");
            return;
        }

        GD.Print($"CardManager: Auto-playing turn for AI player {playerId}");

        // Get valid cards for AI
        var validCards = GetValidCards(playerId);
        if (validCards.Count == 0)
        {
            GD.PrintErr($"CardManager: AI player {playerId} has no valid cards");
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
        GD.Print("CardManager: Ending current game");

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

        GD.Print("CardManager: Game ended successfully");
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
        GD.Print("CardManager: Card game system shutting down");

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
