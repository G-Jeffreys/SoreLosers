using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Dedicated server manager for AWS hosting
/// Handles multiple concurrent game sessions with room codes
/// Runs headless without UI dependencies
/// </summary>
public partial class DedicatedServer : Node
{
    // Server configuration
    [Export] public int ServerPort = 7777;
    [Export] public int MaxConcurrentSessions = 10;
    [Export] public int MaxPlayersPerSession = 4;
    [Export] public float SessionTimeoutMinutes = 30.0f;

    // Active game sessions
    private Dictionary<string, GameSession> activeSessions = new();
    private Dictionary<int, string> playerToSession = new(); // Player ID -> Room Code mapping

    // Network components
    private ENetMultiplayerPeer serverPeer;
    private MultiplayerApi multiplayerApi;

    // Room code generation
    private Random roomCodeGenerator = new();

    [Signal] public delegate void SessionCreatedEventHandler(string roomCode);
    [Signal] public delegate void SessionEndedEventHandler(string roomCode);
    [Signal] public delegate void PlayerJoinedSessionEventHandler(int playerId, string roomCode);

    public override void _Ready()
    {
        // CRITICAL: Remove NetworkManager autoload to prevent RPC conflicts
        var networkManager = GetTree().GetFirstNodeInGroup("NetworkManager");
        if (networkManager == null)
        {
            // Try getting by autoload name (use GetNodeOrNull to avoid crash if already removed)
            networkManager = GetNodeOrNull<Node>("/root/NetworkManager");
        }

        if (networkManager != null)
        {
            GD.Print("DedicatedServer: REMOVING NetworkManager to prevent RPC conflicts");
            networkManager.QueueFree();
        }
        else
        {
            GD.Print("DedicatedServer: NetworkManager not found (already removed or not loaded)");
        }

        // Add to group so other systems can detect server mode
        AddToGroup("dedicated_server");

        GD.Print("=== DEDICATED SERVER STARTING ===");
        GD.Print($"DedicatedServer: Initializing on port {ServerPort}");
        GD.Print($"DedicatedServer: Max sessions: {MaxConcurrentSessions}");
        GD.Print($"DedicatedServer: Max players per session: {MaxPlayersPerSession}");

        // Initialize network
        multiplayerApi = GetTree().GetMultiplayer();

        // Start server
        if (StartServer())
        {
            GD.Print("DedicatedServer: Server started successfully!");
            GD.Print("DedicatedServer: Ready to accept connections");
        }
        else
        {
            GD.PrintErr("DedicatedServer: Failed to start server!");
            GetTree().Quit(1);
        }

        // Set up cleanup timer
        var cleanupTimer = new Timer();
        cleanupTimer.WaitTime = 60.0f; // Check every minute
        cleanupTimer.Timeout += CleanupExpiredSessions;
        cleanupTimer.Autostart = true;
        AddChild(cleanupTimer);
    }

    /// <summary>
    /// Start the dedicated server
    /// </summary>
    /// <returns>True if started successfully</returns>
    private bool StartServer()
    {
        try
        {
            serverPeer = new ENetMultiplayerPeer();
            var result = serverPeer.CreateServer(ServerPort, MaxConcurrentSessions * MaxPlayersPerSession);

            if (result != Error.Ok)
            {
                GD.PrintErr($"DedicatedServer: Failed to create server: {result}");
                return false;
            }

            multiplayerApi.MultiplayerPeer = serverPeer;

            // Connect network events
            multiplayerApi.PeerConnected += OnPeerConnected;
            multiplayerApi.PeerDisconnected += OnPeerDisconnected;

            GD.Print($"DedicatedServer: Server bound to port {ServerPort}");
            return true;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"DedicatedServer: Exception starting server: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Handle new client connection
    /// </summary>
    private void OnPeerConnected(long peerId)
    {
        int playerId = (int)peerId;
        GD.Print($"DedicatedServer: Client {playerId} connected");

        // Client will send join request with room code
        // We wait for that before assigning to session
    }

    /// <summary>
    /// Handle client disconnection
    /// </summary>
    private void OnPeerDisconnected(long peerId)
    {
        int playerId = (int)peerId;
        GD.Print($"DedicatedServer: Client {playerId} disconnected");

        // Remove player from their session
        if (playerToSession.ContainsKey(playerId))
        {
            string roomCode = playerToSession[playerId];
            RemovePlayerFromSession(playerId, roomCode);
        }
    }

    /// <summary>
    /// NetworkManager RPC compatibility layer - handles RPCs from client NetworkManager
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    public void RequestJoinGame(string roomCode, string playerName)
    {
        // Forward to internal method
        HandleJoinRequest(roomCode, playerName);
    }



    /// <summary>
    /// Internal method: Client requests to join or create a game
    /// </summary>
    private void HandleJoinRequest(string roomCode, string playerName)
    {
        int senderId = multiplayerApi.GetRemoteSenderId();
        GD.Print($"DedicatedServer: Join request from {senderId} for room '{roomCode}' as '{playerName}'");

        if (string.IsNullOrEmpty(roomCode))
        {
            // Create new session
            roomCode = CreateNewSession();
            if (roomCode != null)
            {
                AddPlayerToSession(senderId, roomCode, playerName);
                RpcId(senderId, "JoinGameResponse", true, roomCode, "Game created!");
            }
            else
            {
                RpcId(senderId, "JoinGameResponse", false, "", "Server full");
            }
        }
        else
        {
            // Join existing session
            if (activeSessions.ContainsKey(roomCode) && !activeSessions[roomCode].IsFull())
            {
                AddPlayerToSession(senderId, roomCode, playerName);
                RpcId(senderId, "JoinGameResponse", true, roomCode, "Joined game!");
            }
            else
            {
                string error = activeSessions.ContainsKey(roomCode) ? "Game full" : "Game not found";
                RpcId(senderId, "JoinGameResponse", false, roomCode, error);
            }
        }
    }

    /// <summary>
    /// RPC: Send join response to client
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void JoinGameResponse(bool success, string roomCode, string message)
    {
        // This is sent TO clients, not received by server
    }

    /// <summary>
    /// Create a new game session
    /// </summary>
    private string CreateNewSession()
    {
        if (activeSessions.Count >= MaxConcurrentSessions)
        {
            GD.PrintErr("DedicatedServer: Cannot create session - server at capacity");
            return null;
        }

        string roomCode = GenerateRoomCode();
        var session = new GameSession(roomCode, MaxPlayersPerSession);

        activeSessions[roomCode] = session;
        EmitSignal(SignalName.SessionCreated, roomCode);

        GD.Print($"DedicatedServer: Created session {roomCode}");
        return roomCode;
    }

    /// <summary>
    /// Add player to a game session
    /// </summary>
    private void AddPlayerToSession(int playerId, string roomCode, string playerName)
    {
        if (!activeSessions.ContainsKey(roomCode))
        {
            GD.PrintErr($"DedicatedServer: Cannot add player to non-existent session {roomCode}");
            return;
        }

        var session = activeSessions[roomCode];
        session.AddPlayer(playerId, playerName);
        playerToSession[playerId] = roomCode;

        EmitSignal(SignalName.PlayerJoinedSession, playerId, roomCode);
        GD.Print($"DedicatedServer: Added player {playerId} ({playerName}) to session {roomCode}");

        // Notify all players in session about new player
        BroadcastToSession(roomCode, MethodName.PlayerJoinedGame, playerId, playerName);

        // Start game if enough players
        if (session.Players.Count >= 2) // Minimum 2 players to start
        {
            StartGameSession(roomCode);
        }
    }

    /// <summary>
    /// Remove player from their session
    /// </summary>
    private void RemovePlayerFromSession(int playerId, string roomCode)
    {
        if (!activeSessions.ContainsKey(roomCode))
            return;

        var session = activeSessions[roomCode];
        session.RemovePlayer(playerId);
        playerToSession.Remove(playerId);

        // Notify remaining players
        BroadcastToSession(roomCode, MethodName.PlayerLeftGame, playerId);

        // End session if no players left
        if (session.Players.Count == 0)
        {
            EndSession(roomCode);
        }

        GD.Print($"DedicatedServer: Removed player {playerId} from session {roomCode}");
    }

    /// <summary>
    /// Start the card game for a session
    /// </summary>
    private void StartGameSession(string roomCode)
    {
        if (!activeSessions.ContainsKey(roomCode))
            return;

        var session = activeSessions[roomCode];
        if (session.IsGameActive)
            return; // Already started

        session.StartGame();

        // Broadcast game start to all players in session
        var playerIds = session.Players.Keys.ToArray();
        BroadcastToSession(roomCode, MethodName.GameStarted, playerIds);

        GD.Print($"DedicatedServer: Started game for session {roomCode} with {playerIds.Length} players");
    }

    /// <summary>
    /// Broadcast message to all players in a session
    /// </summary>
    private void BroadcastToSession(string roomCode, StringName method, params Variant[] args)
    {
        if (!activeSessions.ContainsKey(roomCode))
            return;

        var session = activeSessions[roomCode];
        foreach (int playerId in session.Players.Keys)
        {
            RpcId(playerId, method, args);
        }
    }

    /// <summary>
    /// RPC: Client-to-server game actions
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void PlayerAction(string actionType, Variant[] actionData)
    {
        int senderId = multiplayerApi.GetRemoteSenderId();

        if (!playerToSession.ContainsKey(senderId))
        {
            GD.PrintErr($"DedicatedServer: Received action from player {senderId} not in any session");
            return;
        }

        string roomCode = playerToSession[senderId];
        var session = activeSessions[roomCode];

        // Process action based on type
        switch (actionType)
        {
            case "play_card":
                ProcessCardPlay(session, senderId, actionData);
                break;
            case "sabotage":
                ProcessSabotage(session, senderId, actionData);
                break;
            default:
                GD.PrintErr($"DedicatedServer: Unknown action type: {actionType}");
                break;
        }
    }

    /// <summary>
    /// Process card play action
    /// </summary>
    private void ProcessCardPlay(GameSession session, int playerId, Variant[] actionData)
    {
        // TODO: Implement card game logic
        // For now, just broadcast to other players
        string roomCode = session.RoomCode;
        var args = new Variant[actionData.Length + 1];
        args[0] = playerId;
        for (int i = 0; i < actionData.Length; i++)
        {
            args[i + 1] = actionData[i];
        }
        BroadcastToSession(roomCode, nameof(CardPlayed), args);
    }

    /// <summary>
    /// Process sabotage action
    /// </summary>
    private void ProcessSabotage(GameSession session, int playerId, Variant[] actionData)
    {
        // TODO: Implement sabotage logic
        // For now, just broadcast to other players
        string roomCode = session.RoomCode;
        var args = new Variant[actionData.Length + 1];
        args[0] = playerId;
        for (int i = 0; i < actionData.Length; i++)
        {
            args[i + 1] = actionData[i];
        }
        BroadcastToSession(roomCode, nameof(SabotageAction), args);
    }

    /// <summary>
    /// RPC stubs for client communication
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void PlayerJoinedGame(int playerId, string playerName) { }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void PlayerLeftGame(int playerId) { }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void GameStarted(int[] playerIds) { }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void CardPlayed(int playerId, Variant[] cardData) { }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void SabotageAction(int playerId, Variant[] sabotageData) { }

    /// <summary>
    /// Generate unique 6-digit room code
    /// </summary>
    private string GenerateRoomCode()
    {
        string roomCode;
        int attempts = 0;

        do
        {
            roomCode = roomCodeGenerator.Next(100000, 999999).ToString();
            attempts++;
        } while (activeSessions.ContainsKey(roomCode) && attempts < 100);

        return roomCode;
    }

    /// <summary>
    /// End a game session
    /// </summary>
    private void EndSession(string roomCode)
    {
        if (!activeSessions.ContainsKey(roomCode))
            return;

        var session = activeSessions[roomCode];

        // Remove all players from mapping
        foreach (int playerId in session.Players.Keys)
        {
            playerToSession.Remove(playerId);
        }

        activeSessions.Remove(roomCode);
        EmitSignal(SignalName.SessionEnded, roomCode);

        GD.Print($"DedicatedServer: Ended session {roomCode}");
    }

    /// <summary>
    /// Clean up expired sessions
    /// </summary>
    private void CleanupExpiredSessions()
    {
        var expiredSessions = new List<string>();
        var cutoffTime = DateTime.Now.AddMinutes(-SessionTimeoutMinutes);

        foreach (var kvp in activeSessions)
        {
            if (kvp.Value.CreatedAt < cutoffTime)
            {
                expiredSessions.Add(kvp.Key);
            }
        }

        foreach (string roomCode in expiredSessions)
        {
            GD.Print($"DedicatedServer: Cleaning up expired session {roomCode}");
            EndSession(roomCode);
        }
    }

    /// <summary>
    /// Get server status for monitoring
    /// </summary>
    public Dictionary<string, Variant> GetServerStatus()
    {
        return new Dictionary<string, Variant>
        {
            ["active_sessions"] = activeSessions.Count,
            ["connected_players"] = playerToSession.Count,
            ["max_sessions"] = MaxConcurrentSessions,
            ["server_port"] = ServerPort,
            ["uptime_seconds"] = Time.GetUnixTimeFromSystem()
        };
    }

    public override void _ExitTree()
    {
        if (serverPeer != null)
        {
            serverPeer.Close();
        }
    }
}

/// <summary>
/// Represents a single game session
/// </summary>
public class GameSession
{
    public string RoomCode { get; private set; }
    public Dictionary<int, PlayerSessionData> Players { get; private set; } = new();
    public bool IsGameActive { get; private set; } = false;
    public DateTime CreatedAt { get; private set; }
    public int MaxPlayers { get; private set; }

    public GameSession(string roomCode, int maxPlayers)
    {
        RoomCode = roomCode;
        MaxPlayers = maxPlayers;
        CreatedAt = DateTime.Now;
    }

    public void AddPlayer(int playerId, string playerName)
    {
        Players[playerId] = new PlayerSessionData
        {
            PlayerId = playerId,
            PlayerName = playerName,
            JoinedAt = DateTime.Now
        };
    }

    public void RemovePlayer(int playerId)
    {
        Players.Remove(playerId);
    }

    public bool IsFull()
    {
        return Players.Count >= MaxPlayers;
    }

    public void StartGame()
    {
        IsGameActive = true;
    }

    public void EndGame()
    {
        IsGameActive = false;
    }
}

/// <summary>
/// Player data within a session
/// </summary>
public class PlayerSessionData
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public DateTime JoinedAt { get; set; }
}