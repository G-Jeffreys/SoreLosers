using Godot;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Nakama;

/// <summary>
/// Match manager for SoreLosers card game using Nakama
/// Replaces complex AWS RPC system with simple message-based synchronization
/// Handles card game state, player management, and turn coordination
/// </summary>
public partial class MatchManager : Node
{
    // Singleton instance for global access
    public static MatchManager Instance { get; private set; }

    // Reference to Nakama manager
    private NakamaManager nakama;
    private IMatch currentMatch;

    // Public property to check if we have an active match
    public bool HasActiveMatch => currentMatch != null;

    // Game state - using match-specific player data
    public Dictionary<string, MatchPlayerData> Players { get; private set; } = new();
    public bool IsGameInProgress { get; private set; } = false;
    public string CurrentTurnPlayerId { get; private set; } = "";
    public List<CardPlayData> CurrentTrick { get; private set; } = new();
    public int TricksPlayed { get; private set; } = 0;

    // 🔥 NEW: Synchronized game seed for deterministic behavior
    public int GameSeed { get; private set; } = 0;

    // 🔥 NEW: Store dealt cards for synchronization
    public Dictionary<int, List<string>> LastDealtCards { get; private set; } = new();

    // 🔥 FIXED: Track presences locally since currentMatch.Presences might be readonly
    private List<IUserPresence> localPresences = new();

    // 🔥 FIXED: Track original match owner to prevent ownership flipping
    private string originalMatchOwnerId = "";

    // Match operation codes (replaces 29 RPC methods with 8 message types)
    public enum MatchOpCode
    {
        PlayerJoined = 1,        // Player joins lobby
        PlayerReady = 2,         // Player ready to start
        GameStart = 3,           // Game initialization
        CardPlayed = 4,          // Card play action
        TurnChange = 5,          // Turn progression
        GameEnd = 6,             // Game completion
        ChatMessage = 7,         // Chat message between players
        NameUpdate = 8,          // Player name change
        CardsDealt = 9,          // Card dealing synchronization
        TrickCompleted = 10,     // Trick completion synchronization
        TimerUpdate = 11,        // Turn timer synchronization
        EggThrown = 12           // Egg throwing sabotage
    }

    // Events for UI and game systems
    [Signal] public delegate void PlayerJoinedGameEventHandler();
    [Signal] public delegate void PlayerLeftGameEventHandler();
    [Signal] public delegate void PlayerReadyChangedEventHandler(string playerId, bool isReady);
    [Signal] public delegate void GameStartedEventHandler();
    [Signal] public delegate void ChatMessageReceivedEventHandler(string senderId, string senderName, string message);
    [Signal] public delegate void CardPlayReceivedEventHandler(int playerId, string suit, string rank);
    [Signal] public delegate void TurnChangeReceivedEventHandler(int currentPlayerTurn, int tricksPlayed);
    [Signal] public delegate void CardsDealtEventHandler(); // Simple notification that cards were dealt
    [Signal] public delegate void CardPlayedEventHandler();
    [Signal] public delegate void TurnChangedEventHandler(string currentPlayerId);
    [Signal] public delegate void TrickCompletedEventHandler();
    [Signal] public delegate void TrickCompletedReceivedEventHandler(int winnerId, int newTrickLeader, int winnerScore);
    [Signal] public delegate void TimerUpdateReceivedEventHandler(float timeRemaining);
    [Signal] public delegate void EggThrownEventHandler(int sourcePlayerId, int targetPlayerId, Vector2 targetPosition, float coverage);
    [Signal] public delegate void GameEndedEventHandler(string winnerId);

    public override void _Ready()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GD.PrintErr("MatchManager: Multiple instances detected! This should be a singleton.");
            QueueFree();
            return;
        }

        GD.Print("=== MATCH MANAGER INITIALIZED ===");

        // Get Nakama manager reference
        nakama = NakamaManager.Instance;
        if (nakama == null)
        {
            GD.PrintErr("MatchManager: NakamaManager not found! Make sure it's initialized first.");
            return;
        }

        // 🔥 FIXED: Connect socket events when available
        ConnectSocketEventsIfAvailable();
    }

    /// <summary>
    /// Connect to Nakama socket events if the socket is available
    /// Called when NakamaManager instance becomes available
    /// </summary>
    public void ConnectSocketEventsIfAvailable()
    {
        var processId = OS.GetProcessId();
        nakama = NakamaManager.Instance;

        GD.Print($"MatchManager[PID:{processId}]: ConnectSocketEventsIfAvailable called");
        GD.Print($"MatchManager[PID:{processId}]: NakamaManager.Instance null: {nakama == null}");

        if (nakama?.Socket != null)
        {
            GD.Print($"MatchManager[PID:{processId}]: Socket available - connecting events");
            GD.Print($"MatchManager[PID:{processId}]: Current match ID: {currentMatch?.Id ?? "null"}");

            // Remove any existing connections to prevent duplicates
            nakama.Socket.ReceivedMatchState -= OnMatchStateReceived;
            nakama.Socket.ReceivedMatchPresence -= OnMatchPresenceReceived;

            // Connect events
            nakama.Socket.ReceivedMatchState += OnMatchStateReceived;
            nakama.Socket.ReceivedMatchPresence += OnMatchPresenceReceived;

            GD.Print($"MatchManager[PID:{processId}]: ✅ Connected to Nakama socket events");
            GD.Print($"MatchManager[PID:{processId}]: Socket.IsConnected: {nakama.Socket.IsConnected}");
            GD.Print($"MatchManager[PID:{processId}]: Session valid: {nakama.Session != null && !nakama.Session.IsExpired}");
        }
        else
        {
            if (nakama == null)
            {
                GD.Print($"MatchManager[PID:{processId}]: ❌ NakamaManager.Instance is null");
            }
            else if (nakama.Socket == null)
            {
                GD.Print($"MatchManager[PID:{processId}]: ❌ NakamaManager.Socket is null");
            }
            GD.Print($"MatchManager[PID:{processId}]: Socket not ready yet, will connect events when socket is available");
        }
    }

    public override void _ExitTree()
    {
        // Clean up event connections
        if (nakama?.Socket != null)
        {
            nakama.Socket.ReceivedMatchState -= OnMatchStateReceived;
            nakama.Socket.ReceivedMatchPresence -= OnMatchPresenceReceived;
        }

        if (Instance == this)
        {
            Instance = null;
        }
    }

    /// <summary>
    /// Set the current match and initialize presence tracking
    /// FIXED VERSION - properly tracks original match owner
    /// </summary>
    public void SetCurrentMatch(IMatch match)
    {
        if (match == null)
        {
            GD.PrintErr("MatchManager: SetCurrentMatch called with null match!");
            return;
        }

        var processId = OS.GetProcessId();
        var localUserId = nakama?.Session?.UserId;

        currentMatch = match;
        GD.Print($"MatchManager: Set current match: {match.Id}");

        // Clear and initialize presence tracking
        ConnectSocketEventsIfAvailable();

        var presenceCount = match.Presences.Count();
        GD.Print($"MatchManager: SetCurrentMatch - Match has {presenceCount} presences");

        // 🔥 CRITICAL FIX: Track original match owner on first match set
        // For the match creator, they will be the only presence initially
        // For joiners, the first presence in the list should be the original owner
        if (string.IsNullOrEmpty(originalMatchOwnerId) && match.Presences.Any())
        {
            // If this is a new match with only 1 presence, the local player is the creator
            if (presenceCount == 1 && match.Presences.First().UserId == localUserId)
            {
                originalMatchOwnerId = localUserId;
                GD.Print($"MatchManager[PID:{processId}]: 🏆 MATCH CREATOR - Set originalMatchOwnerId: {originalMatchOwnerId} (local player is owner)");
            }
            // If joining an existing match, the first presence should be the original owner
            else
            {
                originalMatchOwnerId = match.Presences.First().UserId;
                var isLocalOwner = (originalMatchOwnerId == localUserId);
                GD.Print($"MatchManager[PID:{processId}]: 🏆 JOINING MATCH - Set originalMatchOwnerId: {originalMatchOwnerId} (local player is owner: {isLocalOwner})");
            }
        }
        else if (!string.IsNullOrEmpty(originalMatchOwnerId))
        {
            var isLocalOwner = (originalMatchOwnerId == localUserId);
            GD.Print($"MatchManager[PID:{processId}]: 🏆 Match owner already set: {originalMatchOwnerId} (local player is owner: {isLocalOwner})");
        }

        InitializePresenceTracking(match);

        // Emit presence change signal
        CallDeferred(MethodName.EmitPresenceChangedSignal);
    }

    /// <summary>
    /// Delayed roster validation to catch and fix presence issues immediately
    /// SIMPLIFIED VERSION - removes excessive validation that causes loops
    /// </summary>
    private void DelayedRosterValidation()
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        GD.Print($"MatchManager[PID:{processId}]: Basic roster validation - {Players.Count} players in match");

        // SIMPLIFIED: Just log current state, no complex recovery
        // Trust Nakama's presence events to keep state consistent
        GD.Print($"MatchManager[PID:{processId}]: ✅ Roster validation simplified - trusting Nakama presence events");
    }

    #region Player Management

    /// <summary>
    /// Add or update player in the match
    /// </summary>
    private void AddOrUpdatePlayer(string userId, string username, bool isReady = false)
    {
        if (Players.ContainsKey(userId))
        {
            // Update existing player
            Players[userId].IsReady = isReady;
        }
        else
        {
            // Add new player - 🔥 LOBBY SYSTEM: Don't auto-mark as ready, let match owner control start
            var autoReady = false; // Players join lobby but aren't ready until match owner starts game

            var playerData = new MatchPlayerData
            {
                UserId = userId,
                Username = username,
                IsReady = autoReady,
                Score = 0,
                CardCount = 0
            };

            Players[userId] = playerData;
            GD.Print($"MatchManager: Added player: {username} ({userId}) - Waiting in lobby (not auto-ready)");

            // 🔥 CRITICAL: Use CallDeferred for thread safety
            CallDeferred(MethodName.EmitPlayerJoinedSignal);

            // Don't emit ready changed signal since players start not-ready
            // The match owner will start the game manually
        }
    }

    /// <summary>
    /// Remove player from the match
    /// </summary>
    private void RemovePlayer(string userId)
    {
        if (Players.ContainsKey(userId))
        {
            var player = Players[userId];
            Players.Remove(userId);
            GD.Print($"MatchManager: Removed player: {player.Username} ({userId})");

            // 🔥 CRITICAL: Use CallDeferred for thread safety
            CallDeferred(MethodName.EmitPlayerLeftSignal);
        }
    }



    /// <summary>
    /// Check if all players are ready to start
    /// </summary>
    public bool AreAllPlayersReady()
    {
        if (Players.Count < 2) return false; // Need at least 2 players
        return Players.Values.All(p => p.IsReady);
    }

    /// <summary>
    /// Get current player count
    /// </summary>
    public int GetPlayerCount()
    {
        return Players.Count;
    }

    /// <summary>
    /// Get actual match size from current match presences (more reliable than match.Size)
    /// </summary>
    public int GetActualMatchSize()
    {
        if (currentMatch != null)
        {
            var presenceCount = currentMatch.Presences.Count();
            var reportedSize = currentMatch.Size;

            GD.Print($"MatchManager: Match presences count: {presenceCount}, Reported match.Size: {reportedSize}");

            // 🔥 FIX: Use presences count instead of match.Size which can be stale
            return presenceCount;
        }

        GD.Print("MatchManager: GetActualMatchSize() - no current match, returning 0");
        return 0;
    }

    /// <summary>
    /// Get local player data
    /// </summary>
    public MatchPlayerData GetLocalPlayer()
    {
        var localUserId = nakama?.Session?.UserId ?? "";
        return Players.ContainsKey(localUserId) ? Players[localUserId] : null;
    }

    /// <summary>
    /// Check if local player is match owner (original creator/first joiner)
    /// FIXED VERSION - tracks original owner instead of using dynamic sorting
    /// </summary>
    public bool IsLocalPlayerMatchOwner()
    {
        var processId = OS.GetProcessId();
        var localUserId = nakama?.Session?.UserId;

        if (string.IsNullOrEmpty(localUserId))
        {
            GD.PrintErr($"MatchManager[PID:{processId}]: IsLocalPlayerMatchOwner - Local user ID is null or empty!");
            return false;
        }

        // 🔥 CRITICAL FIX: Use stored original match owner instead of dynamic sorting
        // Dynamic sorting by user ID causes ownership to flip when players join
        if (!string.IsNullOrEmpty(originalMatchOwnerId))
        {
            bool isOwner = originalMatchOwnerId == localUserId;
            // 🔥 REMOVED LOGGING: This method is called frequently and was causing infinite loop spam
            return isOwner;
        }

        // 🔥 EMERGENCY FALLBACK: If no original owner tracked, this is a critical error
        // Multiple players should not be determining ownership simultaneously
        GD.PrintErr($"MatchManager[PID:{processId}]: ⚠️ CRITICAL - No original match owner tracked! This indicates a synchronization issue.");
        GD.PrintErr($"MatchManager[PID:{processId}]: Local user: {localUserId}, CurrentMatch: {currentMatch?.Id}, Presences: {currentMatch?.Presences?.Count()}");

        // 🔥 CRITICAL FIX: Never assign ownership in fallback to prevent multiple match owners
        // Instead, explicitly return false and log the issue
        GD.PrintErr($"MatchManager[PID:{processId}]: Returning FALSE to prevent multiple match owners. Original owner must be set during match creation/join.");

        return false;
    }

    #endregion

    #region Match Actions

    /// <summary>
    /// Mark local player as ready
    /// </summary>
    public async Task SetPlayerReady(bool isReady = true)
    {
        var localUserId = nakama?.Session?.UserId ?? "";
        var localUsername = nakama?.Session?.Username ?? "Player";

        if (Players.ContainsKey(localUserId))
        {
            Players[localUserId].IsReady = isReady;
        }

        // Send ready status to all players
        var message = new PlayerReadyMessage
        {
            PlayerId = localUserId,
            PlayerName = localUsername,
            IsReady = isReady
        };

        await SendMatchMessage(MatchOpCode.PlayerReady, message);
        GD.Print($"MatchManager: Set ready status: {isReady}");
    }

    /// <summary>
    /// Start the game (match owner only)
    /// SIMPLIFIED VERSION - removes excessive validation and delays
    /// </summary>
    public async Task StartGame()
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        GD.Print($"MatchManager[PID:{processId}]: StartGame called");

        if (!IsLocalPlayerMatchOwner())
        {
            GD.PrintErr("MatchManager: Only match owner can start the game");
            return;
        }

        GD.Print("MatchManager: Match owner starting game...");

        // SIMPLIFIED: No complex validation or delays - trust current state
        // Force-ready all connected players for manual start
        var readyCount = 0;
        foreach (var player in Players.Values)
        {
            player.IsReady = true;
            readyCount++;
        }
        GD.Print($"MatchManager: Force-readied {readyCount} players for manual start");

        // Generate and set game seed locally for synchronization
        var gameSeed = GenerateGameSeed();
        GameSeed = gameSeed;
        GD.Print($"MatchManager: Match owner setting GameSeed: {gameSeed}");

        // Send game start message to all players
        try
        {
            await SendMatchMessage(MatchOpCode.GameStart, new GameStartMessage { Seed = gameSeed, DealerId = Players.Values.First().UserId });

            // Emit game started signal for local handling
            EmitSignal(SignalName.GameStarted);
            GD.Print("MatchManager: Game started successfully");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Failed to start game: {ex.Message}");
        }
    }

    /// <summary>
    /// Play a card (only during player's turn)
    /// </summary>
    public async Task PlayCard(string suit, string rank)
    {
        var localUserId = nakama?.Session?.UserId ?? "";

        if (!IsGameInProgress)
        {
            GD.PrintErr("MatchManager: Cannot play card - game not in progress");
            return;
        }

        if (CurrentTurnPlayerId != localUserId)
        {
            GD.PrintErr("MatchManager: Cannot play card - not your turn");
            return;
        }

        // Create card play data
        var cardPlay = new CardPlayData
        {
            PlayerId = localUserId,
            PlayerName = nakama?.Session?.Username ?? "Player",
            Suit = suit,
            Rank = rank,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        // Send card play to all players
        await SendMatchMessage(MatchOpCode.CardPlayed, cardPlay);

        GD.Print($"MatchManager: Played card: {rank} of {suit}");
    }

    /// <summary>
    /// Send card play from CardManager to all players (used for syncing card plays)
    /// </summary>
    public async Task SendCardPlay(int playerId, string suit, string rank)
    {
        var playerData = Players.Values.FirstOrDefault(p =>
        {
            // Convert Nakama UserId to deterministic game ID for comparison
            var sortedPlayerIds = Players.Keys.OrderBy(k => k).ToList();
            var playerIndex = sortedPlayerIds.IndexOf(p.UserId);
            var gameId = playerIndex * 2;
            return gameId == playerId;
        });

        var cardPlay = new CardPlayData
        {
            PlayerId = playerId.ToString(), // Use game ID as string
            PlayerName = playerData?.Username ?? $"Player_{playerId}",
            Suit = suit,
            Rank = rank,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        await SendMatchMessage(MatchOpCode.CardPlayed, cardPlay);
        GD.Print($"MatchManager: Synced card play - Player {playerId}: {rank} of {suit}");
    }

    /// <summary>
    /// Send a chat message to all players in the match
    /// </summary>
    public async Task SendChatMessage(string message)
    {
        var localUserId = nakama?.Session?.UserId ?? "";
        var localUsername = nakama?.Session?.Username ?? "Player";

        var chatMessage = new ChatMessageData
        {
            SenderId = localUserId,
            SenderName = localUsername,
            Message = message,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        await SendMatchMessage(MatchOpCode.ChatMessage, chatMessage);
        GD.Print($"MatchManager: Sent chat message: {message}");
    }

    /// <summary>
    /// Update player name and sync to all players
    /// </summary>
    public async Task UpdatePlayerName(string newName)
    {
        var localUserId = nakama?.Session?.UserId ?? "";

        if (string.IsNullOrWhiteSpace(newName))
        {
            GD.PrintErr("MatchManager: Cannot set empty name");
            return;
        }

        // Update local player data
        if (Players.ContainsKey(localUserId))
        {
            var oldName = Players[localUserId].Username;
            Players[localUserId].Username = newName;
            GD.Print($"MatchManager: Updated local name from '{oldName}' to '{newName}'");
        }

        // Send name update to all players
        var nameUpdate = new NameUpdateMessage
        {
            PlayerId = localUserId,
            NewName = newName
        };

        await SendMatchMessage(MatchOpCode.NameUpdate, nameUpdate);
        GD.Print($"MatchManager: Sent name update to all players: {newName}");
    }

    /// <summary>
    /// End the current game
    /// </summary>
    public async Task EndGame(string winnerId)
    {
        if (!IsLocalPlayerMatchOwner())
        {
            GD.PrintErr("MatchManager: Only match owner can end the game");
            return;
        }

        var message = new GameEndMessage
        {
            WinnerId = winnerId,
            FinalScores = Players.ToDictionary(p => p.Key, p => p.Value.Score)
        };

        await SendMatchMessage(MatchOpCode.GameEnd, message);

        // Update local state
        IsGameInProgress = false;
        CurrentTurnPlayerId = "";
        CurrentTrick.Clear();

        EmitSignal(SignalName.GameEnded, winnerId);
        GD.Print($"MatchManager: Game ended - Winner: {Players[winnerId]?.Username}");
    }

    /// <summary>
    /// Send egg throw sabotage to all players for synchronization
    /// </summary>
    public async Task SendEggThrow(int sourcePlayerId, int targetPlayerId, Vector2 targetPosition, float coverage)
    {
        var eggThrow = new EggThrowMessage
        {
            SourcePlayerId = sourcePlayerId,
            TargetPlayerId = targetPlayerId,
            TargetPositionX = targetPosition.X,
            TargetPositionY = targetPosition.Y,
            Coverage = coverage,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        await SendMatchMessage(MatchOpCode.EggThrown, eggThrow);
        GD.Print($"MatchManager: Sent egg throw - Player {sourcePlayerId} -> Player {targetPlayerId} at {targetPosition} with {coverage:P1} coverage");
    }

    /// <summary>
    /// Send turn change from CardManager to all players (used for syncing turn progression)
    /// FIXED VERSION - receives player ID instead of index and converts to Nakama user ID
    /// </summary>
    public async Task SendTurnChange(int currentPlayerId, int tricksPlayed)
    {
        // 🔥 CRITICAL FIX: Convert game player ID to Nakama user ID
        string nextPlayerId = "";

        // Get sorted player list (same as used in CardGameUI for deterministic ordering)
        var sortedPlayerIds = Players.Keys.OrderBy(k => k).ToList();

        // Convert game player ID back to Nakama user ID
        // Game IDs are assigned as: playerIndex * 2 (0, 2, 4, etc.)
        // So to get the playerIndex: currentPlayerId / 2
        if (currentPlayerId < 100) // Human player (0, 2, 4, 6)
        {
            int playerIndex = currentPlayerId / 2;
            if (playerIndex >= 0 && playerIndex < sortedPlayerIds.Count)
            {
                nextPlayerId = sortedPlayerIds[playerIndex];
            }
        }
        else // AI player (100+) - use a placeholder since AI players don't have Nakama user IDs
        {
            nextPlayerId = $"AI_Player_{currentPlayerId}";
        }

        // If we couldn't determine the player ID, use the first player as fallback
        if (string.IsNullOrEmpty(nextPlayerId) && Players.Count > 0)
        {
            nextPlayerId = sortedPlayerIds.FirstOrDefault() ?? "";
        }

        // 🔥 FIXED: Also need to calculate the turn index for CardManager synchronization
        // Find the index of this player ID in the expected PlayerOrder
        int turnIndex = -1;
        var expectedPlayerOrder = new List<int>();

        // Recreate the expected PlayerOrder (same logic as CardGameUI)
        for (int i = 0; i < sortedPlayerIds.Count; i++)
        {
            expectedPlayerOrder.Add(i * 2); // 0, 2, 4, 6
        }

        // Add AI players (100+) to match CardGameUI logic
        if (expectedPlayerOrder.Count < 4)
        {
            int nextAiId = 100;
            while (expectedPlayerOrder.Count < 4)
            {
                expectedPlayerOrder.Add(nextAiId);
                nextAiId++;
            }
        }

        turnIndex = expectedPlayerOrder.IndexOf(currentPlayerId);
        if (turnIndex == -1)
        {
            GD.PrintErr($"MatchManager: Could not find turn index for player ID {currentPlayerId}");
            turnIndex = 0; // Fallback to first player
        }

        var turnChange = new TurnChangeMessage
        {
            CurrentPlayerTurn = turnIndex, // 🔥 FIXED: Send the turn index for CardManager
            TricksPlayed = tricksPlayed,
            NextPlayerId = nextPlayerId // 🔥 FIXED: Now properly set
        };

        await SendMatchMessage(MatchOpCode.TurnChange, turnChange);
        GD.Print($"MatchManager: Synced turn change - CurrentPlayerId: {currentPlayerId}, TurnIndex: {turnIndex}, NextPlayerId: {nextPlayerId}, TricksPlayed: {tricksPlayed}");
    }

    /// <summary>
    /// Send dealt cards from CardManager to all players (used for syncing card dealing)
    /// </summary>
    public async Task SendCardsDealt(Dictionary<int, List<string>> playerHands)
    {
        var cardsDealt = new CardsDealtMessage
        {
            PlayerHands = playerHands
        };

        await SendMatchMessage(MatchOpCode.CardsDealt, cardsDealt);
        GD.Print($"MatchManager: Synced dealt cards to all players - {playerHands.Count} hands");
    }

    /// <summary>
    /// Send trick completion from CardManager to all players (used for syncing trick completion)
    /// </summary>
    public async Task SendTrickCompleted(int winnerId, int newTrickLeader, int winnerScore)
    {
        var trickCompleted = new TrickCompletedMessage
        {
            WinnerId = winnerId,
            NewTrickLeader = newTrickLeader,
            WinnerScore = winnerScore
        };

        await SendMatchMessage(MatchOpCode.TrickCompleted, trickCompleted);
        GD.Print($"MatchManager: Synced trick completion - Winner: {winnerId}, Leader: {newTrickLeader}, Score: {winnerScore}");
    }

    /// <summary>
    /// Send timer update from CardManager to all players (used for syncing turn timer)
    /// </summary>
    public async Task SendTimerUpdate(float timeRemaining)
    {
        var timerUpdate = new TimerUpdateMessage
        {
            TimeRemaining = timeRemaining
        };

        await SendMatchMessage(MatchOpCode.TimerUpdate, timerUpdate);
        // Only log occasional timer updates to avoid spam
        if (timeRemaining % 5.0f < 0.1f) // Log every 5 seconds approximately
        {
            GD.Print($"MatchManager: Timer sync - {timeRemaining:F1}s remaining");
        }
    }

    #endregion

    #region Message Handling

    /// <summary>
    /// Send a match message to all players
    /// </summary>
    private async Task SendMatchMessage<T>(MatchOpCode opCode, T messageData)
    {
        try
        {
            // 🔥 CRITICAL: Enhanced debugging for message sending
            GD.Print($"MatchManager: Attempting to send message {opCode}");
            GD.Print($"MatchManager: Current match null: {currentMatch == null}");
            GD.Print($"MatchManager: Nakama null: {nakama == null}");
            GD.Print($"MatchManager: Nakama socket null: {nakama?.Socket == null}");

            if (currentMatch == null)
            {
                GD.PrintErr("MatchManager: Cannot send message - no active match");
                return;
            }

            if (nakama?.Socket == null)
            {
                GD.PrintErr("MatchManager: Cannot send message - no socket connection");
                // Try to reconnect nakama reference
                nakama = NakamaManager.Instance;
                if (nakama?.Socket == null)
                {
                    GD.PrintErr("MatchManager: Still no socket after getting NakamaManager instance");
                    return;
                }
                GD.Print("MatchManager: Successfully reconnected to NakamaManager");
            }

            var json = JsonSerializer.Serialize(messageData);
            var processId = OS.GetProcessId();
            var localUserId = nakama?.Session?.UserId ?? "unknown";
            GD.Print($"MatchManager[PID:{processId}]: Sending message to match {currentMatch.Id}");
            GD.Print($"MatchManager[PID:{processId}]: Local user ID: {localUserId}");
            await nakama.Socket.SendMatchStateAsync(currentMatch.Id, (long)opCode, json);
            GD.Print($"MatchManager[PID:{processId}]: Successfully sent message {opCode}");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Failed to send message: {ex.Message}");
            GD.PrintErr($"MatchManager: Exception stack trace: {ex.StackTrace}");
        }
    }

    /// <summary>
    /// Handle incoming match state messages
    /// </summary>
    private void OnMatchStateReceived(IMatchState matchState)
    {
        try
        {
            var processId = OS.GetProcessId();
            var opCode = (MatchOpCode)matchState.OpCode;
            var data = System.Text.Encoding.UTF8.GetString(matchState.State);

            // Process the incoming message

            // Only log timer updates if they're significant (reduce spam)
            if (opCode == MatchOpCode.TimerUpdate)
            {
                GD.Print($"MatchManager[PID:{processId}]: Raw TimerUpdate received - OpCode: {matchState.OpCode}, State length: {matchState.State.Length}");
            }
            else
            {
                GD.Print($"MatchManager[PID:{processId}]: Received message {opCode} from {matchState.UserPresence.Username}");
            }

            switch (opCode)
            {
                case MatchOpCode.PlayerReady:
                    HandlePlayerReadyMessage(data);
                    break;
                case MatchOpCode.GameStart:
                    HandleGameStartMessage(data);
                    break;
                case MatchOpCode.CardPlayed:
                    HandleCardPlayedMessage(data);
                    break;
                case MatchOpCode.TurnChange:
                    HandleTurnChangeMessage(data);
                    break;
                case MatchOpCode.GameEnd:
                    HandleGameEndMessage(data);
                    break;
                case MatchOpCode.ChatMessage:
                    HandleChatMessage(data);
                    break;
                case MatchOpCode.NameUpdate:
                    HandleNameUpdateMessage(data);
                    break;
                case MatchOpCode.CardsDealt:
                    HandleCardsDealtMessage(data);
                    break;
                case MatchOpCode.TrickCompleted:
                    HandleTrickCompletedMessage(data);
                    break;
                case MatchOpCode.TimerUpdate:
                    HandleTimerUpdateMessage(data);
                    break;
                case MatchOpCode.EggThrown:
                    HandleEggThrownMessage(data);
                    break;
                default:
                    GD.PrintErr($"MatchManager: Unknown message type: {opCode}");
                    break;
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error processing match state: {ex.Message}");
        }
    }

    /// <summary>
    /// Handle match presence events (players joining/leaving)
    /// FIXED VERSION - prevents duplicate presence additions and collection corruption
    /// </summary>
    private void OnMatchPresenceReceived(IMatchPresenceEvent presenceEvent)
    {
        var processId = OS.GetProcessId();
        var localUserId = nakama?.Session?.UserId;

        GD.Print($"🔥 MatchManager[PID:{processId}]: OnMatchPresenceReceived - Joins: {presenceEvent.Joins.Count()}, Leaves: {presenceEvent.Leaves.Count()}");

        // FIXED: Remove leaving players (no complex validation)
        foreach (var leave in presenceEvent.Leaves)
        {
            // Remove from local tracking
            var removed = localPresences.RemoveAll(p => p.UserId == leave.UserId);
            Players.Remove(leave.UserId);

            GD.Print($"MatchManager[PID:{processId}]: Player LEFT: {leave.Username} ({leave.UserId}) - removed {removed} entries");
        }

        // FIXED: Add joining players (with duplicate prevention)
        foreach (var join in presenceEvent.Joins)
        {
            // 🔥 CRITICAL FIX: Check if player already exists before adding
            if (Players.ContainsKey(join.UserId))
            {
                GD.Print($"MatchManager[PID:{processId}]: Player {join.Username} already in collection - skipping duplicate add");
                continue;
            }

            // Add to local presence tracking
            localPresences.Add(join);

            // Create new MatchPlayerData for the presence
            var playerData = new MatchPlayerData
            {
                UserId = join.UserId,
                Username = join.Username,
                IsReady = false
            };

            Players[join.UserId] = playerData;
            GD.Print($"MatchManager[PID:{processId}]: Player JOINED: {join.Username} ({join.UserId})");
        }

        GD.Print($"MatchManager[PID:{processId}]: Presence update complete - {localPresences.Count} players, {Players.Count} in collection");

        // Emit appropriate signals using CallDeferred for thread safety
        if (presenceEvent.Joins.Any())
        {
            foreach (var join in presenceEvent.Joins)
            {
                // Only emit if this was a new addition (not a duplicate)
                if (Players.ContainsKey(join.UserId))
                {
                    // 🔥 CRITICAL FIX: Use CallDeferred for thread safety - Nakama events come from background thread
                    CallDeferred(MethodName.EmitPlayerJoinedSignal, join.UserId);
                }
            }
            // 🔥 CRITICAL FIX: Use CallDeferred for thread safety
            CallDeferred(MethodName.EmitPlayerJoinedSignal);
        }

        if (presenceEvent.Leaves.Any())
        {
            foreach (var leave in presenceEvent.Leaves)
            {
                // 🔥 CRITICAL FIX: Use CallDeferred for thread safety
                CallDeferred(MethodName.EmitPlayerLeftSignal, leave.UserId);
            }
        }
    }

    #endregion

    #region Message Handlers

    private void HandlePlayerReadyMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<PlayerReadyMessage>(data);
            if (Players.ContainsKey(message.PlayerId))
            {
                Players[message.PlayerId].IsReady = message.IsReady;

                // 🔥 CRITICAL: Use CallDeferred for thread safety
                CallDeferred(MethodName.EmitPlayerReadyChangedSignal, message.PlayerId, message.IsReady);
                GD.Print($"MatchManager: Player {message.PlayerName} ready: {message.IsReady}");
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling player ready message: {ex.Message}");
        }
    }

    private void HandleGameStartMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<GameStartMessage>(data);

            // 🔥 CRITICAL: Store synchronized seed for deterministic behavior
            GameSeed = message.Seed;
            GD.Print($"MatchManager: Received synchronized game seed: {GameSeed}");

            // Update local game state
            IsGameInProgress = true;
            CurrentTurnPlayerId = message.DealerId;
            CurrentTrick.Clear();
            TricksPlayed = 0;

            // 🔥 CRITICAL: Use CallDeferred to emit signal from main thread
            CallDeferred(MethodName.EmitGameStartedSignal);
            GD.Print("MatchManager: Game started with synchronized seed!");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling game start message: {ex.Message}");
        }
    }

    /// <summary>
    /// Emit GameStarted signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitGameStartedSignal()
    {
        EmitSignal(SignalName.GameStarted);
        GD.Print("MatchManager: GameStarted signal emitted from main thread");
    }

    /// <summary>
    /// Emit PlayerJoinedGame signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitPlayerJoinedSignal()
    {
        EmitSignal(SignalName.PlayerJoinedGame);
    }

    /// <summary>
    /// Emit PlayerReadyChanged signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitPlayerReadyChangedSignal(string userId, bool isReady)
    {
        EmitSignal(SignalName.PlayerReadyChanged, userId, isReady);
    }

    private void HandleCardPlayedMessage(string data)
    {
        try
        {
            var cardPlay = JsonSerializer.Deserialize<CardPlayData>(data);

            // Convert player ID string back to game ID for CardManager
            int gamePlayerId = int.Parse(cardPlay.PlayerId);

            GD.Print($"MatchManager: Card play received - Player {gamePlayerId}: {cardPlay.Rank} of {cardPlay.Suit}");

            // Emit signal for CardManager to handle the synchronized card play
            CallDeferred(MethodName.EmitCardPlayReceivedSignal, gamePlayerId, cardPlay.Suit, cardPlay.Rank);

            // Update local trick state if needed
            CurrentTrick.Add(cardPlay);

            GD.Print($"MatchManager: Card play synchronized - {cardPlay.PlayerName} played {cardPlay.Rank} of {cardPlay.Suit}");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling card played message: {ex.Message}");
        }
    }

    /// <summary>
    /// Emit CardPlayReceived signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitCardPlayReceivedSignal(int playerId, string suit, string rank)
    {
        EmitSignal(SignalName.CardPlayReceived, playerId, suit, rank);
        GD.Print($"MatchManager: CardPlayReceived signal emitted for player {playerId}: {rank} of {suit}");
    }

    private void HandleTurnChangeMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<TurnChangeMessage>(data);

            // Update local state (for backwards compatibility)
            CurrentTurnPlayerId = message.NextPlayerId;

            // Emit new turn change signal for CardManager synchronization
            CallDeferred(MethodName.EmitTurnChangeReceivedSignal, message.CurrentPlayerTurn, message.TricksPlayed);

            // 🔥 FIXED: Use CallDeferred for thread safety (was causing threading error)
            CallDeferred(MethodName.EmitTurnChangedSignal, CurrentTurnPlayerId);
            GD.Print($"MatchManager: Turn changed to {message.NextPlayerId}, PlayerTurn: {message.CurrentPlayerTurn}, Tricks: {message.TricksPlayed}");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling turn change message: {ex.Message}");
        }
    }

    /// <summary>
    /// Emit TurnChangeReceived signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitTurnChangeReceivedSignal(int currentPlayerTurn, int tricksPlayed)
    {
        EmitSignal(SignalName.TurnChangeReceived, currentPlayerTurn, tricksPlayed);
        GD.Print($"MatchManager: TurnChangeReceived signal emitted - PlayerTurn: {currentPlayerTurn}, Tricks: {tricksPlayed}");
    }

    /// <summary>
    /// Emit TurnChanged signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitTurnChangedSignal(string currentPlayerId)
    {
        EmitSignal(SignalName.TurnChanged, currentPlayerId);
        GD.Print($"MatchManager: TurnChanged signal emitted - CurrentPlayerId: {currentPlayerId}");
    }

    /// <summary>
    /// Emit PlayerJoined signal from main thread (fixes threading issues)
    /// FIXED VERSION - properly handles both with and without player ID
    /// </summary>
    private void EmitPlayerJoinedSignal(string playerId = "")
    {
        if (string.IsNullOrEmpty(playerId))
        {
            EmitSignal(SignalName.PlayerJoinedGame);
        }
        else
        {
            EmitSignal(SignalName.PlayerJoinedGame, playerId);
        }
        GD.Print($"MatchManager: PlayerJoinedGame signal emitted for player: {playerId}");
    }

    /// <summary>
    /// Emit PlayerLeft signal from main thread (fixes threading issues)
    /// FIXED VERSION - properly passes player ID to signal
    /// </summary>
    private void EmitPlayerLeftSignal(string playerId)
    {
        EmitSignal(SignalName.PlayerLeftGame, playerId);
        GD.Print($"MatchManager: PlayerLeftGame signal emitted for player: {playerId}");
    }

    /// <summary>
    /// Emit PresenceChanged signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitPresenceChangedSignal()
    {
        // 🔥 FIXED: Use PlayerJoinedGame as a general presence change notification
        EmitSignal(SignalName.PlayerJoinedGame);
        GD.Print($"MatchManager: PlayerJoinedGame signal emitted as presence change - current presences: {localPresences.Count}");
    }

    private void HandleGameEndMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<GameEndMessage>(data);

            // Update local state
            IsGameInProgress = false;
            CurrentTurnPlayerId = "";
            CurrentTrick.Clear();

            EmitSignal(SignalName.GameEnded, message.WinnerId);
            GD.Print($"MatchManager: Game ended - Winner: {message.WinnerId}");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling game end message: {ex.Message}");
        }
    }

    private void HandleChatMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<ChatMessageData>(data);

            GD.Print($"MatchManager: Received chat message from {message.SenderName}: {message.Message}");

            // 🔥 CRITICAL: Use CallDeferred to emit signal from main thread and store data
            CallDeferred(MethodName.EmitChatMessageSignal, message.SenderId, message.SenderName, message.Message);
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling chat message: {ex.Message}");
        }
    }

    /// <summary>
    /// Emit ChatMessageReceived signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitChatMessageSignal(string senderId, string senderName, string message)
    {
        GD.Print($"MatchManager: Emitting chat signal for {senderName}: {message}");
        EmitSignal(SignalName.ChatMessageReceived, senderId, senderName, message);
    }

    private void HandleNameUpdateMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<NameUpdateMessage>(data);
            if (Players.ContainsKey(message.PlayerId))
            {
                Players[message.PlayerId].Username = message.NewName;
                GD.Print($"MatchManager: Player {message.PlayerId} name updated to {message.NewName}");
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling name update message: {ex.Message}");
        }
    }

    private void HandleCardsDealtMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<CardsDealtMessage>(data);
            GD.Print($"MatchManager: Received dealt cards message for {message.PlayerHands.Count} players");

            // Store the dealt cards in the property for CardManager to access
            LastDealtCards = message.PlayerHands;

            // 🔥 FIXED: Use CallDeferred for thread safety
            CallDeferred(MethodName.EmitCardsDealtSignalSafe);
            GD.Print($"MatchManager: CardsDealt signal scheduled via CallDeferred");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling dealt cards message: {ex.Message}");
        }
    }

    /// <summary>
    /// Emit CardsDealt signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitCardsDealtSignalSafe()
    {
        GD.Print($"🔄 MatchManager: EmitCardsDealtSignalSafe called - about to emit CardsDealt signal");
        GD.Print($"MatchManager: LastDealtCards contains {LastDealtCards.Count} player hands");

        // Log the actual dealt cards for debugging
        foreach (var kvp in LastDealtCards)
        {
            GD.Print($"MatchManager: Player {kvp.Key} has {kvp.Value.Count} cards");
        }

        EmitSignal(SignalName.CardsDealt);
        GD.Print($"MatchManager: CardsDealt signal emitted safely from main thread - CardManager can access via LastDealtCards property");

        // Signal emitted - debug information removed to reduce spam
    }

    private void HandleTrickCompletedMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<TrickCompletedMessage>(data);

            // 🔥 CRITICAL FIX: Use CallDeferred for thread safety - Nakama events come from background thread
            CallDeferred(MethodName.EmitTrickCompletedReceivedSignal, message.WinnerId, message.NewTrickLeader, message.WinnerScore);
            GD.Print($"MatchManager: Trick completed - Winner: {message.WinnerId}, Leader: {message.NewTrickLeader}, Score: {message.WinnerScore}");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling trick completed message: {ex.Message}");
        }
    }

    /// <summary>
    /// Emit TrickCompletedReceived signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitTrickCompletedReceivedSignal(int winnerId, int newTrickLeader, int winnerScore)
    {
        EmitSignal(SignalName.TrickCompletedReceived, winnerId, newTrickLeader, winnerScore);
        GD.Print($"MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: {winnerId}, Leader: {newTrickLeader}, Score: {winnerScore}");
    }

    /// <summary>
    /// Handle timer update message from match owner
    /// </summary>
    private void HandleTimerUpdateMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<TimerUpdateMessage>(data);

            // Only log significant timer updates (every 5 seconds) to avoid spam
            if (message.TimeRemaining % 5.0f < 0.2f)
            {
                GD.Print($"MatchManager: Timer update received - {message.TimeRemaining:F1}s remaining");
            }

            // 🔥 CRITICAL FIX: Use CallDeferred for thread safety - Nakama events come from background thread
            CallDeferred(MethodName.EmitTimerUpdateReceivedSignal, message.TimeRemaining);
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling timer update message: {ex.Message}");
        }
    }

    /// <summary>
    /// Emit TimerUpdateReceived signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitTimerUpdateReceivedSignal(float timeRemaining)
    {
        EmitSignal(SignalName.TimerUpdateReceived, timeRemaining);
        // Signal emitted - no logging to reduce spam
    }

    /// <summary>
    /// Handle egg thrown message from network
    /// </summary>
    private void HandleEggThrownMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<EggThrowMessage>(data);
            var targetPosition = new Vector2(message.TargetPositionX, message.TargetPositionY);

            GD.Print($"MatchManager: Received egg throw - Player {message.SourcePlayerId} -> Player {message.TargetPlayerId} at {targetPosition} with {message.Coverage:P1} coverage");

            // 🔥 CRITICAL FIX: Use CallDeferred for thread safety - Nakama events come from background thread
            CallDeferred(MethodName.EmitEggThrownSignal, message.SourcePlayerId, message.TargetPlayerId, targetPosition, message.Coverage);
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling egg thrown message: {ex.Message}");
        }
    }

    /// <summary>
    /// Emit EggThrown signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitEggThrownSignal(int sourcePlayerId, int targetPlayerId, Vector2 targetPosition, float coverage)
    {
        EmitSignal(SignalName.EggThrown, sourcePlayerId, targetPlayerId, targetPosition, coverage);
        GD.Print($"MatchManager: EggThrown signal emitted - Player {sourcePlayerId} -> Player {targetPlayerId}");
    }

    #endregion

    #region Message Data Structures

    /// <summary>
    /// Player ready status message
    /// </summary>
    private class PlayerReadyMessage
    {
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public bool IsReady { get; set; }
    }

    /// <summary>
    /// Game start message
    /// </summary>
    private class GameStartMessage
    {
        public int Seed { get; set; }
        public string DealerId { get; set; }
    }

    /// <summary>
    /// Player info for game start
    /// </summary>
    private class PlayerInfo
    {
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
    }

    /// <summary>
    /// Turn change message
    /// </summary>
    private class TurnChangeMessage
    {
        public string NextPlayerId { get; set; }
        public int CurrentPlayerTurn { get; set; }
        public int TricksPlayed { get; set; }
    }

    /// <summary>
    /// Game end message
    /// </summary>
    private class GameEndMessage
    {
        public string WinnerId { get; set; }
        public Dictionary<string, int> FinalScores { get; set; }
    }

    /// <summary>
    /// Chat message data
    /// </summary>
    private class ChatMessageData
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
        public long Timestamp { get; set; }
    }

    /// <summary>
    /// Name update message
    /// </summary>
    private class NameUpdateMessage
    {
        public string PlayerId { get; set; }
        public string NewName { get; set; }
    }

    /// <summary>
    /// Cards dealt message for synchronizing dealt cards across all players
    /// </summary>
    private class CardsDealtMessage
    {
        public Dictionary<int, List<string>> PlayerHands { get; set; }
    }

    /// <summary>
    /// Trick completion message for synchronizing trick completion across all players
    /// </summary>
    private class TrickCompletedMessage
    {
        public int WinnerId { get; set; }
        public int NewTrickLeader { get; set; }
        public int WinnerScore { get; set; }
    }

    /// <summary>
    /// Timer update message for synchronizing turn timer across all players
    /// </summary>
    private class TimerUpdateMessage
    {
        public float TimeRemaining { get; set; }
    }

    /// <summary>
    /// Egg throw message for synchronizing sabotage actions across all players
    /// </summary>
    private class EggThrowMessage
    {
        public int SourcePlayerId { get; set; }
        public int TargetPlayerId { get; set; }
        public float TargetPositionX { get; set; }
        public float TargetPositionY { get; set; }
        public float Coverage { get; set; }
        public long Timestamp { get; set; }
    }

    #endregion

    /// <summary>
    /// REMOVED: Complex roster validation that caused infinite recovery loops
    /// The system now trusts Nakama's presence events for consistency
    /// </summary>
    private bool ValidateAndRecoverPlayerRoster()
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        GD.Print($"MatchManager[PID:{processId}]: ✅ Roster validation simplified - {Players.Count} players, trusting Nakama presence events");

        // SIMPLIFIED: Always return true - no complex validation
        return true;
    }

    /// <summary>
    /// SIMPLIFIED: Basic presence sync without complex recovery logic
    /// </summary>
    public void ForceCompletePresenceSync()
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        GD.Print($"MatchManager[PID:{processId}]: Basic presence sync - {localPresences.Count} presences, {Players.Count} players");

        if (currentMatch == null || nakama?.Session == null)
        {
            GD.PrintErr($"MatchManager[PID:{processId}]: Cannot sync presences - no active match or session");
            return;
        }

        // SIMPLIFIED: Just ensure basic consistency without complex recovery
        var localUserId = nakama.Session.UserId;
        if (!Players.ContainsKey(localUserId))
        {
            // Add self if missing (simple case)
            var localUsername = nakama.Session.Username ?? "LocalPlayer";
            AddOrUpdatePlayer(localUserId, localUsername);
            GD.Print($"MatchManager[PID:{processId}]: Added missing local player: {localUsername}");
        }

        GD.Print($"MatchManager[PID:{processId}]: ✅ Basic presence sync complete - {Players.Count} players");
    }

    /// <summary>
    /// Initialize presence tracking from match data
    /// FIXED VERSION - ensures local player is always in localPresences for consistent match ownership
    /// </summary>
    private void InitializePresenceTracking(IMatch match)
    {
        var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
        GD.Print($"MatchManager[PID:{processId}]: InitializePresenceTracking called");

        // Get NakamaManager reference
        nakama = NakamaManager.Instance;
        if (nakama == null)
        {
            GD.PrintErr("MatchManager: Failed to get NakamaManager instance!");
            return;
        }

        GD.Print("MatchManager: Successfully got NakamaManager reference");

        // Initialize from match data
        if (match.Presences != null)
        {
            localPresences.Clear(); // Start fresh
            localPresences.AddRange(match.Presences);
            GD.Print($"MatchManager: Initialized with {localPresences.Count} presences from match");

            // Add players from presences to Players collection
            foreach (var presence in localPresences)
            {
                AddOrUpdatePlayer(presence.UserId, presence.Username);
                GD.Print($"MatchManager: Added player from presence: {presence.Username} ({presence.UserId})");
            }
        }

        // CRITICAL FIX: Ensure local player is in BOTH Players collection AND localPresences
        // Nakama doesn't send self-presence events, so we must add ourselves explicitly
        var localUserId = nakama?.Session?.UserId;
        var localUsername = nakama?.Session?.Username ?? "LocalPlayer";

        if (!string.IsNullOrEmpty(localUserId))
        {
            // Add to Players collection if missing
            if (!Players.ContainsKey(localUserId))
            {
                AddOrUpdatePlayer(localUserId, localUsername);
                GD.Print($"MatchManager: Added local player to Players: {localUsername} (ID: {localUserId})");
            }

            // CRITICAL: Add to localPresences if missing (this was the bug!)
            if (!localPresences.Any(p => p.UserId == localUserId))
            {
                // Create a mock presence for local player to ensure consistent presence tracking
                var localPresence = new MockUserPresence
                {
                    UserId = localUserId,
                    Username = localUsername,
                    Status = "",
                    SessionId = $"local_session_{localUserId}"
                };
                localPresences.Add(localPresence);
                GD.Print($"MatchManager: Added local player to localPresences: {localUsername} (ID: {localUserId})");
            }
        }

        GD.Print($"MatchManager: ✅ Initialization complete - {Players.Count} players in collection, {localPresences.Count} in presence list");
    }

    /// <summary>
    /// Generate a deterministic game seed for synchronization
    /// </summary>
    private int GenerateGameSeed()
    {
        // Use current timestamp for seed generation to ensure uniqueness
        var seed = (int)(DateTime.UtcNow.Ticks % int.MaxValue);
        GD.Print($"MatchManager: Generated game seed: {seed}");
        return seed;
    }

    /// <summary>
    /// Mock presence implementation for recovery scenarios
    /// </summary>
    private class MockUserPresence : IUserPresence
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public string SessionId { get; set; }
        public bool Persistence { get; set; } = false; // Required by IUserPresence interface
    }
}

/// <summary>
/// Match-specific player data
/// </summary>
public class MatchPlayerData
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public bool IsReady { get; set; }
    public int Score { get; set; }
    public int CardCount { get; set; }
}

/// <summary>
/// Card play data structure
/// </summary>
public class CardPlayData
{
    public string PlayerId { get; set; }
    public string PlayerName { get; set; }
    public string Suit { get; set; }
    public string Rank { get; set; }
    public long Timestamp { get; set; }
}

/// <summary>
/// Chat message data structure
/// </summary>
public class ChatMessageData
{
    public string SenderId { get; set; }
    public string SenderName { get; set; }
    public string Message { get; set; }
    public long Timestamp { get; set; }
}