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

    // Match operation codes (replaces 29 RPC methods with 7 message types)
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
        CardsDealt = 9           // Card dealing synchronization
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

        // Connect to Nakama socket events
        if (nakama.Socket != null)
        {
            nakama.Socket.ReceivedMatchState += OnMatchStateReceived;
            nakama.Socket.ReceivedMatchPresence += OnMatchPresenceReceived;
            GD.Print("MatchManager: Connected to Nakama socket events");
        }
        else
        {
            GD.Print("MatchManager: Socket not ready yet, will connect events when socket is available");
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
    /// Set the current match (called when joining/creating a match)
    /// </summary>
    public void SetCurrentMatch(IMatch match)
    {
        currentMatch = match;
        GD.Print($"MatchManager: Set current match: {match.Id}");

        // 🔥 CRITICAL: Ensure nakama reference is properly set
        if (nakama == null)
        {
            nakama = NakamaManager.Instance;
            if (nakama == null)
            {
                GD.PrintErr("MatchManager: Failed to get NakamaManager instance!");
                return;
            }
            GD.Print("MatchManager: Successfully got NakamaManager reference");
        }

        // 🔥 CRITICAL: Initialize players from current match with enhanced debugging
        GD.Print($"MatchManager: SetCurrentMatch - Match has {match.Presences.Count()} presences");
        Players.Clear();

        foreach (var presence in match.Presences)
        {
            GD.Print($"MatchManager: Adding existing player from match: {presence.Username} (ID: {presence.UserId})");
            AddOrUpdatePlayer(presence.UserId, presence.Username);
        }

        // 🔥 CRITICAL: Ensure local player (self) is in the collection
        // Nakama doesn't send presence events for your own join, so add self explicitly
        if (nakama?.Session != null)
        {
            var localUserId = nakama.Session.UserId;

            // Use display name or fallback to user ID substring
            var localUsername = !string.IsNullOrEmpty(nakama.Session.Username)
                ? nakama.Session.Username
                : $"Player_{localUserId.Substring(0, 8)}";

            if (!Players.ContainsKey(localUserId))
            {
                GD.Print($"MatchManager: Adding local player (self) to collection: {localUsername} (ID: {localUserId})");
                AddOrUpdatePlayer(localUserId, localUsername);
            }
            else
            {
                GD.Print($"MatchManager: Local player {localUsername} already in collection");
            }
        }

        GD.Print($"MatchManager: After adding all players including self, Players.Count = {Players.Count}");

        // Connect socket events if not already connected
        if (nakama?.Socket != null)
        {
            nakama.Socket.ReceivedMatchState -= OnMatchStateReceived; // Prevent double connection
            nakama.Socket.ReceivedMatchPresence -= OnMatchPresenceReceived;
            nakama.Socket.ReceivedMatchState += OnMatchStateReceived;
            nakama.Socket.ReceivedMatchPresence += OnMatchPresenceReceived;
            GD.Print("MatchManager: Successfully connected to Nakama socket events");
        }
        else
        {
            GD.PrintErr("MatchManager: Nakama socket is null - cannot connect events!");
        }
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
    /// Emit PlayerLeftGame signal from main thread (fixes threading issues)
    /// </summary>
    private void EmitPlayerLeftSignal()
    {
        EmitSignal(SignalName.PlayerLeftGame);
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
        var count = Players.Count;
        GD.Print($"MatchManager: GetPlayerCount() returning {count} from Players.Count");
        return count;
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
    /// Check if local player is match owner (first to join)
    /// </summary>
    public bool IsLocalPlayerMatchOwner()
    {
        if (currentMatch?.Presences == null || currentMatch.Presences.Count() == 0)
            return false;

        // The first player in the match is the owner
        var firstPresence = currentMatch.Presences.First();
        return firstPresence.UserId == nakama?.Session?.UserId;
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
    /// Start the game (only match owner can do this)
    /// </summary>
    public async Task StartGame()
    {
        if (!IsLocalPlayerMatchOwner())
        {
            GD.PrintErr("MatchManager: Only match owner can start the game");
            return;
        }

        // 🔥 REMOVED: Ready check for manual starts - match owner button press is explicit authorization
        // When match owner clicks "Start Game", they're authorizing the game to begin regardless of other players' ready status
        // This enables the lobby system where match owner has full control

        GD.Print("MatchManager: Match owner starting game manually...");

        // 🔥 NEW: Force all players to ready state when match owner starts game
        foreach (var player in Players.Values)
        {
            player.IsReady = true;
        }
        GD.Print($"MatchManager: Force-readied {Players.Count} players for manual start");

        // Prepare game start data
        var playerList = Players.Values.ToList();
        var gameSeed = new Random().Next(); // Generate seed once for all instances

        var message = new GameStartMessage
        {
            Players = playerList.Select(p => new PlayerInfo
            {
                PlayerId = p.UserId,
                PlayerName = p.Username
            }).ToList(),
            Seed = gameSeed, // Use the same seed for all instances
            DealerId = playerList[0].UserId // First player is dealer
        };

        // 🔥 CRITICAL: Set GameSeed locally BEFORE sending message
        // The match owner doesn't receive their own message back from Nakama
        GameSeed = gameSeed;
        GD.Print($"MatchManager: Match owner setting GameSeed locally: {GameSeed}");

        await SendMatchMessage(MatchOpCode.GameStart, message);

        // Update local state
        IsGameInProgress = true;
        CurrentTurnPlayerId = playerList[0].UserId;
        CurrentTrick.Clear();
        TricksPlayed = 0;

        EmitSignal(SignalName.GameStarted);
        GD.Print("MatchManager: Game started successfully");
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
    /// Send turn change from CardManager to all players (used for syncing turn progression)
    /// </summary>
    public async Task SendTurnChange(int currentPlayerTurn, int tricksPlayed)
    {
        var turnChange = new TurnChangeMessage
        {
            CurrentPlayerTurn = currentPlayerTurn,
            TricksPlayed = tricksPlayed
        };

        await SendMatchMessage(MatchOpCode.TurnChange, turnChange);
        GD.Print($"MatchManager: Synced turn change - CurrentPlayerTurn: {currentPlayerTurn}, TricksPlayed: {tricksPlayed}");
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
            GD.Print($"MatchManager: Sending message to match {currentMatch.Id}");
            await nakama.Socket.SendMatchStateAsync(currentMatch.Id, (long)opCode, json);
            GD.Print($"MatchManager: Successfully sent message {opCode}");
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
            var opCode = (MatchOpCode)matchState.OpCode;
            var data = System.Text.Encoding.UTF8.GetString(matchState.State);

            GD.Print($"MatchManager: Received message {opCode} from {matchState.UserPresence.Username}");

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
    /// </summary>
    private void OnMatchPresenceReceived(IMatchPresenceEvent presenceEvent)
    {
        GD.Print($"MatchManager: Match presence event - Joins: {presenceEvent.Joins.Count()}, Leaves: {presenceEvent.Leaves.Count()}");

        // Handle players joining
        foreach (var presence in presenceEvent.Joins)
        {
            AddOrUpdatePlayer(presence.UserId, presence.Username);
        }

        // Handle players leaving  
        foreach (var presence in presenceEvent.Leaves)
        {
            RemovePlayer(presence.UserId);
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

            // Emit old signal for backwards compatibility
            EmitSignal(SignalName.TurnChanged, CurrentTurnPlayerId);
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

            // Emit simple signal to notify CardManager that cards were dealt
            EmitSignal(SignalName.CardsDealt);
            GD.Print($"MatchManager: CardsDealt signal emitted - CardManager can access via LastDealtCards property");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling dealt cards message: {ex.Message}");
        }
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
        public List<PlayerInfo> Players { get; set; }
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

    #endregion
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