using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages multiplayer networking using ENet for host/client connections
/// Handles room codes, player connections, and network message routing
/// Implements 6-digit room code system for private matches (PRD F-1)
/// </summary>
public partial class NetworkManager : Node
{
    // Network configuration - direct server connection
    [Export]
    public int DefaultPort = 7778; // CRITICAL FIX: Use IPv4 bridge port, not IPv6 Godot port

    [Export]
    public int MaxClients = 4; // Up to 4 players from PRD

    // Connection timeout configuration
    [Export]
    public float ConnectionTimeoutSeconds = 10.0f; // 10 second timeout

    private Godot.Timer connectionTimeoutTimer;

    // Port management for multiple instances
    private int currentServerPort = 7778; // Updated to match DefaultPort

    // Room code system (6-digit codes from PRD)
    public string CurrentRoomCode { get; private set; } = "";

    // Network state
    public bool IsHost { get; private set; } = false;
    public bool IsClient { get; private set; } = false;
    public new bool IsConnected { get; private set; } = false;

    // Connected players tracking
    public Dictionary<int, PlayerNetworkData> ConnectedPlayers { get; private set; } = new();

    // Multiplayer API reference
    private MultiplayerApi multiplayerApi;

    // Pending connection data
    private string pendingPlayerName = "";
    private string pendingRoomCode = "";

    // Events
    [Signal]
    public delegate void PlayerConnectedEventHandler(int playerId);

    [Signal]
    public delegate void PlayerDisconnectedEventHandler(int playerId);

    [Signal]
    public delegate void ConnectionFailedEventHandler(string reason);

    [Signal]
    public delegate void ServerStartedEventHandler(string roomCode);

    [Signal]
    public delegate void ClientConnectedToServerEventHandler();

    [Signal]
    public delegate void GameCreatedEventHandler(string roomCode);

    [Signal]
    public delegate void PlayerDataReceivedEventHandler(int playerId, PlayerData playerData);



    public override void _Ready()
    {
        GD.Print("NetworkManager: _Ready() called - starting initialization");

        // Initialize multiplayer API properly
        multiplayerApi = GetTree().GetMultiplayer();
        if (multiplayerApi == null)
        {
            GD.PrintErr("NetworkManager: Failed to get multiplayer API!");
            return;
        }

        GD.Print("NetworkManager: Multiplayer API initialized successfully");

        // Connect multiplayer signals
        multiplayerApi.PeerConnected += OnPeerConnected;
        multiplayerApi.PeerDisconnected += OnPeerDisconnected;
        multiplayerApi.ConnectedToServer += OnConnectedToServer;
        multiplayerApi.ConnectionFailed += OnConnectionFailed;
        multiplayerApi.ServerDisconnected += OnServerDisconnected;

        // Initialize connection timeout timer
        connectionTimeoutTimer = new Godot.Timer();
        connectionTimeoutTimer.WaitTime = ConnectionTimeoutSeconds;
        connectionTimeoutTimer.OneShot = true;
        connectionTimeoutTimer.Timeout += OnConnectionTimeout;
        AddChild(connectionTimeoutTimer);

        GD.Print("NetworkManager: NetworkManager initialized successfully");
    }





    /// <summary>
    /// Connect to server (either local or remote) using room code
    /// </summary>
    /// <param name="roomCode">6-digit room code</param>
    /// <param name="playerName">Player's display name</param>
    public void ConnectToServer(string roomCode, string playerName)
    {
        GD.Print("=== STARTING SERVER CONNECTION ===");
        GD.Print($"NetworkManager: ConnectToServer called - roomCode: '{roomCode}', playerName: '{playerName}'");

        // Ensure multiplayer API is ready
        if (multiplayerApi == null)
        {
            GD.PrintErr("NetworkManager: Multiplayer API not initialized!");
            EmitSignal(SignalName.ConnectionFailed, "Multiplayer API not ready");
            return;
        }
        GD.Print($"NetworkManager: Multiplayer API is available: {multiplayerApi != null}");

        // Validate room code format (empty means create new game)
        if (!string.IsNullOrEmpty(roomCode) && (roomCode.Length != 6 || !int.TryParse(roomCode, out _)))
        {
            GD.PrintErr("NetworkManager: Invalid room code format");
            EmitSignal(SignalName.ConnectionFailed, "Invalid room code format");
            return;
        }
        GD.Print($"NetworkManager: Room code validation passed");

        // Prevent multiple connection attempts
        if (IsConnected || IsClient)
        {
            GD.PrintErr("NetworkManager: Already connected or connecting!");
            EmitSignal(SignalName.ConnectionFailed, "Already connected or connecting");
            return;
        }

        CurrentRoomCode = roomCode;

        // Determine connection approach
        string serverAddress = GetServerAddress();
        int serverPort;
        bool isLocalConnection = (serverAddress == "127.0.0.1" || ShouldHostLocally());

        if (isLocalConnection)
        {
            // Connect to local host on standard ENet port
            serverPort = 7777;
            GD.Print($"NetworkManager: Connecting to LOCAL host at {serverAddress}:{serverPort}");
        }
        else
        {
            // Connect to remote AWS server via IPv4 bridge
            serverPort = DefaultPort; // 7778
            GD.Print($"NetworkManager: Connecting to REMOTE AWS server at {serverAddress}:{serverPort}");
            GD.Print($"NetworkManager: CRITICAL INFO - Using IPv4 bridge port {serverPort} (not IPv6 port 7777)");
        }

        // Check current peer state
        GD.Print($"NetworkManager: Current peer state - MultiplayerPeer: {multiplayerApi.MultiplayerPeer}, IsConnected: {IsConnected}");

        // Create ENet multiplayer peer
        var peer = new ENetMultiplayerPeer();
        GD.Print($"NetworkManager: Created ENet peer");

        // Start connection timeout timer
        GD.Print($"NetworkManager: Starting connection timeout timer ({ConnectionTimeoutSeconds} seconds)");
        connectionTimeoutTimer.Start();

        var error = peer.CreateClient(serverAddress, serverPort);
        GD.Print($"NetworkManager: CreateClient returned: {error}");

        if (error != Error.Ok)
        {
            GD.PrintErr($"NetworkManager: Failed to create client peer: {error}");
            connectionTimeoutTimer.Stop(); // Stop timer on immediate failure
            EmitSignal(SignalName.ConnectionFailed, $"Failed to connect to server: {error}");
            return;
        }

        // Set the multiplayer peer directly on the tree's multiplayer
        GD.Print($"NetworkManager: Setting multiplayer peer on API");
        multiplayerApi.MultiplayerPeer = peer;
        GD.Print($"NetworkManager: Multiplayer peer set successfully");

        // Update state
        IsClient = true;
        pendingPlayerName = playerName;
        pendingRoomCode = roomCode;

        GD.Print($"NetworkManager: CLIENT CONNECTION STARTED - Connecting to server");
        GD.Print($"NetworkManager: Will request room '{roomCode}' as '{playerName}'");
        GD.Print($"NetworkManager: State updated - IsClient: {IsClient}, pendingPlayerName: '{pendingPlayerName}', pendingRoomCode: '{pendingRoomCode}'");
        GD.Print("=== CONNECTION ATTEMPT IN PROGRESS ===");
    }

    /// <summary>
    /// Get the server address from configuration with development mode support
    /// </summary>
    private string GetServerAddress()
    {
        GD.Print("NetworkManager: Determining server address...");

        // Default AWS server IP
        string serverIP = "3.16.16.22";

        // DEVELOPMENT MODE DETECTION: Check various ways to enable localhost mode
        bool useLocalhost = false;
        string reason = "";

        // Method 1: Command line argument --localhost or --server=localhost
        if (OS.IsDebugBuild())
        {
            var args = OS.GetCmdlineArgs();
            foreach (string arg in args)
            {
                if (arg.StartsWith("--server="))
                {
                    serverIP = arg.Substring(9);
                    reason = $"command line argument: {arg}";
                    GD.Print($"NetworkManager: Server address overridden by {reason}");
                    return serverIP;
                }
                if (arg == "--localhost")
                {
                    useLocalhost = true;
                    reason = "command line argument: --localhost";
                    break;
                }
            }
        }

        // Method 2: Environment variable SORE_LOSERS_LOCAL=true
        string localEnv = OS.GetEnvironment("SORE_LOSERS_LOCAL");
        if (!string.IsNullOrEmpty(localEnv) && (localEnv.ToLower() == "true" || localEnv == "1"))
        {
            useLocalhost = true;
            reason = "environment variable: SORE_LOSERS_LOCAL=true";
        }

        // Method 3: Auto-detect if running multiple instances (development testing)
        if (OS.IsDebugBuild() && !useLocalhost)
        {
            // In development, if AWS server is unreachable, suggest localhost mode
            reason = "development build - consider using localhost mode";
        }

        if (useLocalhost)
        {
            serverIP = "127.0.0.1";
            GD.Print($"NetworkManager: LOCALHOST MODE ENABLED - Reason: {reason}");
            GD.Print("NetworkManager: Will connect to local server instead of AWS");

            // Show networking information for user
            ShowNetworkingInfo();
        }
        else
        {
            GD.Print($"NetworkManager: Using AWS server: {serverIP}");
            if (OS.IsDebugBuild())
            {
                GD.Print("NetworkManager: DEVELOPMENT TIP - Use --localhost flag or SORE_LOSERS_LOCAL=true for local testing");
            }
        }

        return serverIP;
    }

    /// <summary>
    /// Show networking information to help users understand connection options
    /// </summary>
    private void ShowNetworkingInfo()
    {
        GD.Print("=== NETWORKING INFORMATION ===");
        GD.Print("LOCAL P2P HOSTING ACTIVE");
        GD.Print("");
        GD.Print("🏠 WHAT WORKS:");
        GD.Print("  ✅ Same Computer: Multiple game instances");
        GD.Print("  ✅ Same WiFi/LAN: Other computers on your network");

        // Try to get local IP for LAN play
        string localIP = GetLocalIPAddress();
        if (!string.IsNullOrEmpty(localIP) && localIP != "127.0.0.1")
        {
            GD.Print($"  ✅ LAN IP: Other players can connect to {localIP}:7777");
        }

        GD.Print("");
        GD.Print("❌ WHAT DOESN'T WORK:");
        GD.Print("  ❌ Internet: Remote players (requires port forwarding)");
        GD.Print("  ❌ Mobile hotspots: Different internet connections");
        GD.Print("");
        GD.Print("🌐 FOR INTERNET PLAY:");
        GD.Print("  Use: godot -- --server=3.16.16.22 (AWS server)");
        GD.Print("  Or set up port forwarding on your router");
        GD.Print("===============================");
    }

    /// <summary>
    /// Get the local IP address for LAN connections
    /// </summary>
    private string GetLocalIPAddress()
    {
        try
        {
            // Use Godot's IP singleton to get local addresses
            var addresses = IP.GetLocalAddresses();
            foreach (string address in addresses)
            {
                // Look for IPv4 addresses that aren't localhost or link-local
                if (address.Contains(".") && !address.StartsWith("127.") && !address.StartsWith("169.254."))
                {
                    GD.Print($"NetworkManager: Found local IP: {address}");
                    return address;
                }
            }
        }
        catch (Exception ex)
        {
            GD.Print($"NetworkManager: Could not get local IP: {ex.Message}");
        }

        return "127.0.0.1";
    }

    /// <summary>
    /// Generate a 6-digit room code
    /// </summary>
    /// <returns>Random 6-digit room code</returns>
    public string GenerateRoomCode()
    {
        var random = new Random();
        int code = random.Next(100000, 999999);
        return code.ToString();
    }

    /// <summary>
    /// Disconnect from current game
    /// </summary>
    public void Disconnect()
    {
        // Close multiplayer connection
        if (multiplayerApi.MultiplayerPeer != null)
        {
            multiplayerApi.MultiplayerPeer.Close();
            multiplayerApi.MultiplayerPeer = null;
        }

        // Clear state
        IsHost = false;
        IsClient = false;
        IsConnected = false;
        CurrentRoomCode = "";
        ConnectedPlayers.Clear();
    }

    /// <summary>
    /// Send player data to all connected clients
    /// </summary>
    /// <param name="playerData">Player data to send</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    public void SendPlayerData(PlayerData playerData)
    {
        // Send player data as primitive values to avoid serialization issues
        Rpc(MethodName.ReceivePlayerData,
            playerData.PlayerId,
            playerData.PlayerName);
    }

    /// <summary>
    /// Receive player data from network
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <param name="playerName">Player name received</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    public void ReceivePlayerData(int playerId, string playerName)
    {
        // Update connected players
        if (ConnectedPlayers.ContainsKey(playerId))
        {
            ConnectedPlayers[playerId].PlayerName = playerName;
        }

        // Create PlayerData object for compatibility
        var receivedPlayerData = new PlayerData
        {
            PlayerId = playerId,
            PlayerName = playerName
        };

        // Notify GameManager
        GameManager.Instance?.AddPlayer(playerId, receivedPlayerData);

        EmitSignal(SignalName.PlayerDataReceived, playerId, receivedPlayerData);
    }

    /// <summary>
    /// Send complete player list (including AI players) from host to all clients
    /// CRITICAL FIX: Ensures all instances have identical player lists
    /// </summary>
    /// <param name="playerIds">Array of all player IDs in the game</param>
    /// <param name="playerNames">Array of corresponding player names</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    public void NetworkSyncPlayers(int[] playerIds, string[] playerNames)
    {
        GD.Print("=== CRITICAL DEBUG: NetworkSyncPlayers RPC RECEIVED ===");
        GD.Print($"NetworkManager: CLIENT received NetworkSyncPlayers RPC");
        GD.Print($"NetworkManager: Sender ID: {GetTree().GetMultiplayer().GetRemoteSenderId()}");
        GD.Print($"NetworkManager: My ID: {GetTree().GetMultiplayer().GetUniqueId()}");
        GD.Print($"NetworkManager: IsHost: {IsHost}, IsClient: {IsClient}, IsConnected: {IsConnected}");

        if (playerIds.Length != playerNames.Length)
        {
            GD.PrintErr($"NetworkManager: Player sync arrays mismatch - IDs: {playerIds.Length}, Names: {playerNames.Length}");
            return;
        }

        GD.Print($"NetworkManager: CLIENT received player sync with {playerIds.Length} players");
        for (int i = 0; i < playerIds.Length; i++)
        {
            GD.Print($"  Player {i}: ID={playerIds[i]}, Name={playerNames[i]}");
        }

        GD.Print($"NetworkManager: CLIENT local player ID before sync: {GameManager.Instance?.LocalPlayer?.PlayerId}");

        // CRITICAL FIX: Clear ALL existing players and rebuild in the exact order from host
        var localPlayerId = GameManager.Instance?.LocalPlayer?.PlayerId ?? -1;

        if (GameManager.Instance?.ConnectedPlayers != null)
        {
            GD.Print($"NetworkManager: CLIENT current ConnectedPlayers before sync: [{string.Join(", ", GameManager.Instance.ConnectedPlayers.Keys)}]");

            // Clear the entire ConnectedPlayers dictionary to rebuild it in host order
            GameManager.Instance.ConnectedPlayers.Clear();
            ConnectedPlayers.Clear();

            GD.Print("NetworkManager: CLIENT cleared all players to rebuild in host order");
        }

        // Add all synchronized players in the exact order from host
        for (int i = 0; i < playerIds.Length; i++)
        {
            int playerId = playerIds[i];
            string playerName = playerNames[i];

            // Create PlayerData for this synchronized player
            var playerData = new PlayerData
            {
                PlayerId = playerId,
                PlayerName = playerName,
                ThrowPower = 1,
                MoveSpeed = 1,
                Composure = 1,
                TotalXP = 0
            };

            // CRITICAL FIX: Update local player data if this is our player
            if (playerId == localPlayerId)
            {
                GD.Print($"NetworkManager: CLIENT updating local player data for {playerId} ({playerName})");
                if (GameManager.Instance?.LocalPlayer != null)
                {
                    GameManager.Instance.LocalPlayer.PlayerName = playerName; // Use host's name
                }
            }

            // Add to GameManager in host order
            GameManager.Instance?.AddPlayer(playerId, playerData);

            // Update NetworkManager's connected players list
            ConnectedPlayers[playerId] = new PlayerNetworkData
            {
                PlayerId = playerId,
                PlayerName = playerName,
                IsHost = (playerId == 1) // Host is always ID 1
            };

            GD.Print($"NetworkManager: CLIENT synchronized player {playerName} (ID: {playerId}) in position {i}");
        }

        GD.Print($"NetworkManager: CLIENT player synchronization complete");
        var finalPlayerIds = GameManager.Instance?.ConnectedPlayers?.Keys.ToArray() ?? new int[0];
        GD.Print($"NetworkManager: CLIENT final ConnectedPlayers: [{string.Join(", ", finalPlayerIds)}]");
        GD.Print($"NetworkManager: CLIENT final player count: {GameManager.Instance?.ConnectedPlayers?.Count ?? 0}");
        GD.Print("=== END NetworkSyncPlayers RPC ===");
    }

    /// <summary>
    /// Send chat message to all players
    /// </summary>
    /// <param name="message">Chat message to send</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    public void SendChatMessage(string message)
    {
        // TODO: Implement chat message handling
        // This would integrate with the chat intimidation system
    }

    /// <summary>
    /// Send sabotage action to all players
    /// </summary>
    /// <param name="sourcePlayerId">Player performing sabotage</param>
    /// <param name="sabotageType">Type of sabotage</param>
    /// <param name="targetPosition">Target position for sabotage</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    public void SendSabotageAction(int sourcePlayerId, SabotageType sabotageType, Vector2 targetPosition)
    {
        // TODO: Implement sabotage synchronization
        // This would trigger sabotage effects on all clients
    }

    /// <summary>
    /// Send card play action to all players
    /// </summary>
    /// <param name="playerId">Player playing card</param>
    /// <param name="cardId">Card being played</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    public void SendCardPlay(int playerId, int cardId)
    {
        // TODO: Implement card game synchronization
        // This would update the card game state on all clients
    }

    /// <summary>
    /// Get list of connected player IDs
    /// </summary>
    /// <returns>List of connected player IDs</returns>
    public List<int> GetConnectedPlayerIds()
    {
        return new List<int>(ConnectedPlayers.Keys);
    }

    /// <summary>
    /// Get player count
    /// </summary>
    /// <returns>Number of connected players</returns>
    public int GetPlayerCount()
    {
        return ConnectedPlayers.Count;
    }

    /// <summary>
    /// Check if room is full
    /// </summary>
    /// <returns>True if room is at maximum capacity</returns>
    public bool IsRoomFull()
    {
        return ConnectedPlayers.Count >= MaxClients;
    }

    // Network event handlers

    /// <summary>
    /// Handle peer connected event
    /// </summary>
    /// <param name="id">Peer ID that connected</param>
    private void OnPeerConnected(long id)
    {
        int playerId = (int)id;
        GD.Print($"NetworkManager: Peer {playerId} connected");

        // Add to connected players
        ConnectedPlayers[playerId] = new PlayerNetworkData
        {
            PlayerId = playerId,
            PlayerName = $"Player_{playerId}",
            IsHost = false
        };

        // Create PlayerData and add to GameManager
        var playerData = new PlayerData
        {
            PlayerId = playerId,
            PlayerName = $"Player {playerId}",
            ThrowPower = 1,
            MoveSpeed = 1,
            Composure = 1,
            TotalXP = 0
        };

        // Notify GameManager of new player
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddPlayer(playerId, playerData);

            // REMOVED: Automatic transition to card phase when 2+ players connect
            // Let the host manually control when the game starts via the lobby UI
        }

        // If we're the host, send welcome message
        if (IsHost)
        {
            // Send current game state to new player
            if (GameManager.Instance?.LocalPlayer != null)
            {
                // CRITICAL FIX: Send primitive values, not complex objects
                RpcId(playerId, MethodName.ReceivePlayerData,
                    GameManager.Instance.LocalPlayer.PlayerId,
                    GameManager.Instance.LocalPlayer.PlayerName);
            }
        }

        EmitSignal(SignalName.PlayerConnected, playerId);
    }

    /// <summary>
    /// Handle peer disconnected event
    /// </summary>
    /// <param name="id">Peer ID that disconnected</param>
    private void OnPeerDisconnected(long id)
    {
        int playerId = (int)id;
        GD.Print($"NetworkManager: Peer {playerId} disconnected");

        // Remove from connected players
        if (ConnectedPlayers.ContainsKey(playerId))
        {
            ConnectedPlayers.Remove(playerId);
        }

        // Notify GameManager
        GameManager.Instance?.RemovePlayer(playerId);

        EmitSignal(SignalName.PlayerDisconnected, playerId);
    }

    /// <summary>
    /// Handle successful connection to server
    /// </summary>
    private void OnConnectedToServer()
    {
        GD.Print("=== CONNECTION SUCCESS ===");
        GD.Print("NetworkManager: Connected to AWS server successfully!");

        // Stop the connection timeout timer since we connected successfully
        connectionTimeoutTimer.Stop();
        GD.Print("NetworkManager: Connection timeout timer stopped (successful connection)");

        IsConnected = true;
        GD.Print($"NetworkManager: CLIENT CONNECTED TO SERVER - IsClient: {IsClient}, IsConnected: {IsConnected}");

        // Send join request to server
        GD.Print($"NetworkManager: Sending join request for room '{pendingRoomCode}' as '{pendingPlayerName}'");
        SendJoinRequest(pendingRoomCode, pendingPlayerName);
    }

    /// <summary>
    /// Send join request to dedicated server (calls DedicatedServer's RequestJoinGame)
    /// </summary>
    private void SendJoinRequest(string roomCode, string playerName)
    {
        GD.Print($"NetworkManager: Sending join request to DedicatedServer - Room: '{roomCode}', Player: '{playerName}'");

        // Check if we're running on the dedicated server
        var dedicatedServer = GetTree().GetFirstNodeInGroup("dedicated_server");
        if (dedicatedServer != null)
        {
            // We're on the server - call DedicatedServer method directly
            GD.Print("NetworkManager: Calling DedicatedServer RequestJoinGame directly (server-side)");
            dedicatedServer.Call("RequestJoinGame", roomCode, playerName);
        }
        else
        {
            // We're on the client - send RPC to remote server
            GD.Print("NetworkManager: Sending RequestJoinGame RPC to remote server (client-side)");
            RpcId(1, "RequestJoinGame", roomCode, playerName);
        }
    }

    /// <summary>
    /// RPC stub for RequestJoinGame - this is called on the server by clients
    /// The actual implementation is in DedicatedServer.cs
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void RequestJoinGame(string roomCode, string playerName)
    {
        // This is a server-side RPC stub
        // The actual implementation is handled by DedicatedServer
        GD.Print($"NetworkManager: RequestJoinGame RPC received - forwarding to DedicatedServer");

        var dedicatedServer = GetTree().GetFirstNodeInGroup("dedicated_server");
        if (dedicatedServer != null)
        {
            dedicatedServer.Call("RequestJoinGame", roomCode, playerName);
        }
        else
        {
            GD.PrintErr("NetworkManager: No DedicatedServer found to handle RequestJoinGame!");
        }
    }

    /// <summary>
    /// RPC: Receive join response from server
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void JoinGameResponse(bool success, string roomCode, string message)
    {
        GD.Print($"NetworkManager: Received join response - Success: {success}, Room: {roomCode}, Message: {message}");

        if (success)
        {
            CurrentRoomCode = roomCode;

            // Check if this was a game creation (empty pendingRoomCode) or joining existing game
            if (string.IsNullOrEmpty(pendingRoomCode))
            {
                GD.Print($"NetworkManager: Successfully created new game {roomCode}");
                EmitSignal(SignalName.GameCreated, roomCode);
            }
            else
            {
                GD.Print($"NetworkManager: Successfully joined existing game {roomCode}");
            }

            EmitSignal(SignalName.ClientConnectedToServer);
        }
        else
        {
            EmitSignal(SignalName.ConnectionFailed, message);
            GD.PrintErr($"NetworkManager: Failed to join game: {message}");
        }
    }

    /// <summary>
    /// RPC: Receive player joined notification
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void PlayerJoinedGame(int playerId, string playerName)
    {
        GD.Print($"NetworkManager: Player {playerId} ({playerName}) joined the game");
        EmitSignal(SignalName.PlayerConnected, playerId);
    }

    /// <summary>
    /// RPC: Receive player left notification
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void PlayerLeftGame(int playerId)
    {
        GD.Print($"NetworkManager: Player {playerId} left the game");
        EmitSignal(SignalName.PlayerDisconnected, playerId);
    }

    /// <summary>
    /// RPC: Receive game started notification
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void GameStarted(int[] playerIds)
    {
        GD.Print($"NetworkManager: Game started with players: [{string.Join(", ", playerIds)}]");
        // GameManager will handle this via CardManager
    }

    /// <summary>
    /// RPC: Player action sent to server
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void PlayerAction(string actionType, Variant[] actionData)
    {
        // This method is called by clients to send actions to the server
        // The actual implementation is on the DedicatedServer - this is just the RPC declaration
        GD.Print($"NetworkManager: PlayerAction RPC called - Action: '{actionType}', Data count: {actionData.Length}");
    }

    /// <summary>
    /// RPC: Send player action to server
    /// </summary>
    public void SendPlayerAction(string actionType, params Variant[] actionData)
    {
        if (!IsConnected)
        {
            GD.PrintErr("NetworkManager: Cannot send action - not connected to server");
            return;
        }

        var args = new Variant[actionData.Length + 1];
        args[0] = actionType;
        for (int i = 0; i < actionData.Length; i++)
        {
            args[i + 1] = actionData[i];
        }
        Rpc("PlayerAction", args);
    }

    /// <summary>
    /// Create a new game - either on AWS server or locally
    /// </summary>
    public string StartHosting()
    {
        GD.Print("NetworkManager: StartHosting - Determining hosting mode...");

        string serverAddress = GetServerAddress();

        // If localhost mode or server unreachable, host locally
        if (serverAddress == "127.0.0.1" || ShouldHostLocally())
        {
            return StartLocalHosting();
        }
        else
        {
            return StartRemoteHosting();
        }
    }

    /// <summary>
    /// Check if we should host locally instead of connecting to remote server
    /// </summary>
    private bool ShouldHostLocally()
    {
        // Always host locally in debug builds unless specifically connecting to remote server
        if (OS.IsDebugBuild())
        {
            var args = OS.GetCmdlineArgs();
            foreach (string arg in args)
            {
                if (arg.StartsWith("--server=") && !arg.Contains("127.0.0.1") && !arg.Contains("localhost"))
                {
                    return false; // User specified remote server
                }
            }
            return true; // Default to local hosting in debug
        }
        return false;
    }

    /// <summary>
    /// Start local peer-to-peer hosting (creates ENet server)
    /// </summary>
    private string StartLocalHosting()
    {
        GD.Print("=== STARTING LOCAL P2P HOSTING ===");
        GD.Print("NetworkManager: Creating local ENet server for peer-to-peer multiplayer");

        // Generate room code for this local game
        CurrentRoomCode = GenerateRoomCode();

        // Create ENet server on standard ENet port (not bridge port)
        int localServerPort = 7777; // Use standard ENet port for local hosting
        var peer = new ENetMultiplayerPeer();
        var result = peer.CreateServer(localServerPort, MaxClients);

        if (result != Error.Ok)
        {
            GD.PrintErr($"NetworkManager: Failed to create local server on port {localServerPort}: {result}");
            EmitSignal(SignalName.ConnectionFailed, $"Failed to create local server: {result}");
            return "";
        }

        // Set the multiplayer peer
        multiplayerApi.MultiplayerPeer = peer;

        // Update state
        IsHost = true;
        IsClient = false;
        IsConnected = true;

        GD.Print($"NetworkManager: Local server created successfully on port {localServerPort}");
        GD.Print($"NetworkManager: Room code: {CurrentRoomCode}");
        GD.Print($"NetworkManager: State - IsHost: {IsHost}, IsConnected: {IsConnected}");

        // Emit signals to notify game systems
        EmitSignal(SignalName.ServerStarted, CurrentRoomCode);
        EmitSignal(SignalName.GameCreated, CurrentRoomCode);
        EmitSignal(SignalName.ClientConnectedToServer); // This triggers the host lobby transition

        GD.Print("=== LOCAL P2P SERVER READY ===");
        return CurrentRoomCode;
    }

    /// <summary>
    /// Start remote hosting (connect to dedicated server) - original AWS approach
    /// </summary>
    private string StartRemoteHosting()
    {
        GD.Print("NetworkManager: StartHosting - Creating new game on AWS server");

        // Connect to server with empty room code to create new game
        ConnectToServer("", "Host");

        // Return empty for now - actual room code will come from server response
        return "";
    }

    /// <summary>
    /// REMOVED: Old ConnectToGame method
    /// Use ConnectToServer instead  
    /// </summary>
    [System.Obsolete("Use ConnectToServer instead")]
    public void ConnectToGame(string roomCode)
    {
        GD.PrintErr("NetworkManager: ConnectToGame is deprecated - use ConnectToServer instead");
        // For backward compatibility, call new method with default name
        ConnectToServer(roomCode, "Player");
    }

    /// <summary>
    /// Update local player based on server connection
    /// Note: In AWS server mode, player management is handled server-side
    /// </summary>
    private void UpdateLocalPlayerFromServer()
    {
        // In dedicated server mode, we don't manage local players
        // Player data is handled entirely server-side
        GD.Print("NetworkManager: Player management handled by server - local player management skipped");

        // Still emit the connection signal for UI updates
        EmitSignal(SignalName.ClientConnectedToServer);
    }

    /// <summary>
    /// Handle connection failed event
    /// </summary>
    private void OnConnectionFailed()
    {
        GD.PrintErr("=== CONNECTION FAILED ===");
        GD.PrintErr("NetworkManager: Connection to server failed");

        // Stop the connection timeout timer since connection attempt ended
        connectionTimeoutTimer.Stop();
        GD.Print("NetworkManager: Connection timeout timer stopped (connection failed)");

        // Clean up state
        IsClient = false;
        IsConnected = false;
        CurrentRoomCode = "";

        // Disconnect multiplayer peer to clean up properly
        if (multiplayerApi?.MultiplayerPeer != null)
        {
            GD.Print("NetworkManager: Cleaning up multiplayer peer");
            multiplayerApi.MultiplayerPeer.Close();
            multiplayerApi.MultiplayerPeer = null;
        }

        EmitSignal(SignalName.ConnectionFailed, "Connection to server failed");
        GD.PrintErr("=== CONNECTION FAILED - USER SHOULD SEE ERROR MESSAGE ===");
    }

    /// <summary>
    /// Handle server disconnected event
    /// </summary>
    private void OnServerDisconnected()
    {
        GD.Print("NetworkManager: Server disconnected");

        IsClient = false;
        IsConnected = false;
        CurrentRoomCode = "";
        ConnectedPlayers.Clear();

        // TODO: Return to main menu or show reconnection dialog
        GameManager.Instance?.ChangePhase(GameManager.GamePhase.MainMenu);
    }

    /// <summary>
    /// Handle connection timeout event
    /// </summary>
    private void OnConnectionTimeout()
    {
        GD.PrintErr("=== CONNECTION TIMEOUT ===");
        GD.PrintErr($"NetworkManager: Connection to server timed out after {ConnectionTimeoutSeconds} seconds");

        // Clean up state
        IsClient = false;
        IsConnected = false;
        CurrentRoomCode = "";

        // Disconnect multiplayer peer to clean up properly
        if (multiplayerApi?.MultiplayerPeer != null)
        {
            GD.Print("NetworkManager: Cleaning up multiplayer peer due to timeout");
            multiplayerApi.MultiplayerPeer.Close();
            multiplayerApi.MultiplayerPeer = null;
        }

        EmitSignal(SignalName.ConnectionFailed, $"Connection timed out after {ConnectionTimeoutSeconds} seconds. Server may be unreachable.");
        GD.PrintErr("=== CONNECTION TIMEOUT - USER SHOULD SEE ERROR MESSAGE ===");
    }

    public override void _ExitTree()
    {
        // Disconnect multiplayer signals
        if (multiplayerApi != null)
        {
            multiplayerApi.PeerConnected -= OnPeerConnected;
            multiplayerApi.PeerDisconnected -= OnPeerDisconnected;
            multiplayerApi.ConnectedToServer -= OnConnectedToServer;
            multiplayerApi.ConnectionFailed -= OnConnectionFailed;
            multiplayerApi.ServerDisconnected -= OnServerDisconnected;
        }

        // Clean up network connections
        if (IsHost || IsClient)
        {
            Disconnect();
        }

        // Stop the connection timeout timer
        if (connectionTimeoutTimer != null)
        {
            connectionTimeoutTimer.QueueFree();
        }
    }

    /// <summary>
    /// Validate room code format (6 digits)
    /// </summary>
    /// <param name="roomCode">Room code to validate</param>
    /// <returns>True if valid format</returns>
    public bool IsValidRoomCode(string roomCode)
    {
        return roomCode != null &&
               roomCode.Length == 6 &&
               int.TryParse(roomCode, out _);
    }

    /// <summary>
    /// Sets up multiplayer authority for the given player ID.
    /// This ensures that the CardManager and other critical nodes are
    /// controlled by the host and not the clients.
    /// </summary>
    /// <param name="playerId">The player ID of the host.</param>
    private void SetupMultiplayerAuthority(int playerId)
    {
        // Set authority for CardManager
        if (GetNode<CardManager>("/root/CardManager") is CardManager cardManager)
        {
            cardManager.SetMultiplayerAuthority(playerId);
        }
        else
        {
            GD.PrintErr("NetworkManager: CardManager node not found in scene.");
        }

        // Set authority for GameManager
        if (GetNode<GameManager>("/root/GameManager") is GameManager gameManager)
        {
            gameManager.SetMultiplayerAuthority(playerId);
        }
        else
        {
            GD.PrintErr("NetworkManager: GameManager node not found in scene.");
        }

        // Set authority for NetworkManager
        SetMultiplayerAuthority(playerId);
    }
}

/// <summary>
/// Network data for connected players
/// </summary>
public class PlayerNetworkData
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = "";
    public bool IsHost { get; set; } = false;
    public DateTime ConnectedTime { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"Player {PlayerName} (ID: {PlayerId}) - Host: {IsHost}";
    }
}