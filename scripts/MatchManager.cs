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

    // Game state - using match-specific player data
    public Dictionary<string, MatchPlayerData> Players { get; private set; } = new();
    public bool IsGameInProgress { get; private set; } = false;
    public string CurrentTurnPlayerId { get; private set; } = "";
    public List<CardPlayData> CurrentTrick { get; private set; } = new();
    public int TricksPlayed { get; private set; } = 0;

    // Match operation codes (replaces 29 RPC methods with 6 message types)
    public enum MatchOpCode
    {
        PlayerJoined = 1,        // Player joins lobby
        PlayerReady = 2,         // Player ready to start
        GameStart = 3,           // Game initialization
        CardPlayed = 4,          // Card play action
        TurnChange = 5,          // Turn progression
        GameEnd = 6              // Game completion
    }

    // Events for UI and game systems
    [Signal] public delegate void PlayerJoinedGameEventHandler();
    [Signal] public delegate void PlayerLeftGameEventHandler();
    [Signal] public delegate void PlayerReadyChangedEventHandler(string playerId, bool isReady);
    [Signal] public delegate void GameStartedEventHandler();
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

        // Initialize players from current match
        Players.Clear();
        foreach (var presence in match.Presences)
        {
            AddOrUpdatePlayer(presence.UserId, presence.Username);
        }

        // Connect socket events if not already connected
        if (nakama?.Socket != null)
        {
            nakama.Socket.ReceivedMatchState -= OnMatchStateReceived; // Prevent double connection
            nakama.Socket.ReceivedMatchPresence -= OnMatchPresenceReceived;
            nakama.Socket.ReceivedMatchState += OnMatchStateReceived;
            nakama.Socket.ReceivedMatchPresence += OnMatchPresenceReceived;
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
            // Add new player
            var playerData = new MatchPlayerData
            {
                UserId = userId,
                Username = username,
                IsReady = isReady,
                Score = 0,
                CardCount = 0
            };

            Players[userId] = playerData;
            GD.Print($"MatchManager: Added player: {username} ({userId})");
            EmitSignal(SignalName.PlayerJoinedGame);
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
            EmitSignal(SignalName.PlayerLeftGame);
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
    public int GetPlayerCount() => Players.Count;

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

        if (!AreAllPlayersReady())
        {
            GD.PrintErr("MatchManager: Cannot start game - not all players are ready");
            return;
        }

        GD.Print("MatchManager: Starting game...");

        // Prepare game start data
        var playerList = Players.Values.ToList();
        var message = new GameStartMessage
        {
            Players = playerList.Select(p => new PlayerInfo
            {
                PlayerId = p.UserId,
                PlayerName = p.Username
            }).ToList(),
            Seed = new Random().Next(), // For deterministic card shuffling
            DealerId = playerList[0].UserId // First player is dealer
        };

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

    #endregion

    #region Message Handling

    /// <summary>
    /// Send a match message to all players
    /// </summary>
    private async Task SendMatchMessage<T>(MatchOpCode opCode, T messageData)
    {
        try
        {
            if (currentMatch == null || nakama?.Socket == null)
            {
                GD.PrintErr("MatchManager: Cannot send message - no active match or socket");
                return;
            }

            var json = JsonSerializer.Serialize(messageData);
            await nakama.Socket.SendMatchStateAsync(currentMatch.Id, (long)opCode, json);
            GD.Print($"MatchManager: Sent message {opCode}");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Failed to send message: {ex.Message}");
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
                default:
                    GD.Print($"MatchManager: Unknown message type: {opCode}");
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
                EmitSignal(SignalName.PlayerReadyChanged, message.PlayerId, message.IsReady);
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

            // Update local game state
            IsGameInProgress = true;
            CurrentTurnPlayerId = message.DealerId;
            CurrentTrick.Clear();
            TricksPlayed = 0;

            EmitSignal(SignalName.GameStarted);
            GD.Print("MatchManager: Game started!");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling game start message: {ex.Message}");
        }
    }

    private void HandleCardPlayedMessage(string data)
    {
        try
        {
            var cardPlay = JsonSerializer.Deserialize<CardPlayData>(data);
            CurrentTrick.Add(cardPlay);

            EmitSignal(SignalName.CardPlayed);
            GD.Print($"MatchManager: {cardPlay.PlayerName} played {cardPlay.Rank} of {cardPlay.Suit}");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling card played message: {ex.Message}");
        }
    }

    private void HandleTurnChangeMessage(string data)
    {
        try
        {
            var message = JsonSerializer.Deserialize<TurnChangeMessage>(data);
            CurrentTurnPlayerId = message.NextPlayerId;

            EmitSignal(SignalName.TurnChanged, CurrentTurnPlayerId);
            GD.Print($"MatchManager: Turn changed to {message.NextPlayerId}");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MatchManager: Error handling turn change message: {ex.Message}");
        }
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
    }

    /// <summary>
    /// Game end message
    /// </summary>
    private class GameEndMessage
    {
        public string WinnerId { get; set; }
        public Dictionary<string, int> FinalScores { get; set; }
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