using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages multiplayer networking using ENet for host/client connections
/// Handles room codes, player connections, and network message routing
/// Implements 6-digit room code system for private matches (PRD F-1)
/// </summary>
public partial class NetworkManager : Node
{
    // Network configuration
    [Export]
    public int DefaultPort = 7777;

    [Export]
    public int MaxClients = 4; // Up to 4 players from PRD

    // Port management for multiple instances
    private int currentServerPort = 7777;

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
    public delegate void PlayerDataReceivedEventHandler(int playerId, PlayerData playerData);

    public override void _Ready()
    {
        GD.Print("NetworkManager: Initializing network manager...");

        // Initialize multiplayer API properly
        multiplayerApi = GetTree().GetMultiplayer();
        if (multiplayerApi == null)
        {
            GD.PrintErr("NetworkManager: Failed to get multiplayer API!");
            return;
        }

        // Connect multiplayer signals
        multiplayerApi.PeerConnected += OnPeerConnected;
        multiplayerApi.PeerDisconnected += OnPeerDisconnected;
        multiplayerApi.ConnectedToServer += OnConnectedToServer;
        multiplayerApi.ConnectionFailed += OnConnectionFailed;
        multiplayerApi.ServerDisconnected += OnServerDisconnected;

        GD.Print("NetworkManager: Network manager initialized successfully");
    }

    /// <summary>
    /// Start hosting a game with generated room code
    /// </summary>
    /// <returns>6-digit room code for other players to join</returns>
    public string StartHosting()
    {
        GD.Print("NetworkManager: Starting to host game...");

        // Ensure multiplayer API is ready
        if (multiplayerApi == null)
        {
            GD.PrintErr("NetworkManager: Multiplayer API not initialized!");
            EmitSignal(SignalName.ConnectionFailed, "Multiplayer API not ready");
            return "";
        }

        // Generate 6-digit room code
        CurrentRoomCode = GenerateRoomCode();

        // Create ENet multiplayer peer with enhanced initialization
        var peer = new ENetMultiplayerPeer();
        Error error = Error.Failed;

        GD.Print("NetworkManager: Initializing ENet multiplayer peer...");

        // Try to initialize ENet more explicitly
        try
        {
            // Try ports starting from DefaultPort
            for (int port = DefaultPort; port < DefaultPort + 10; port++)
            {
                GD.Print($"NetworkManager: Attempting to create server on port {port}...");

                // Create a fresh peer for each attempt to avoid state issues
                peer = new ENetMultiplayerPeer();

                // Try creating server with minimal parameters first
                error = peer.CreateServer(port, MaxClients, 0, 0, 0);

                if (error == Error.Ok)
                {
                    currentServerPort = port;
                    GD.Print($"NetworkManager: Server created successfully on port {port}");
                    break;
                }
                else
                {
                    GD.Print($"NetworkManager: Port {port} failed with error: {error}");
                    peer?.Close(); // Clean up failed peer
                }
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NetworkManager: Exception during server creation: {ex.Message}");
            peer?.Close();
            EmitSignal(SignalName.ConnectionFailed, $"Server creation exception: {ex.Message}");
            return "";
        }

        if (error != Error.Ok)
        {
            GD.PrintErr($"NetworkManager: Failed to create server peer on any port. Final error: {error}");
            peer?.Close();
            EmitSignal(SignalName.ConnectionFailed, $"Failed to create server: {error}");
            return "";
        }

        // Set the multiplayer peer directly on the tree's multiplayer
        multiplayerApi.MultiplayerPeer = peer;
        GD.Print($"NetworkManager: Host multiplayer peer set successfully");

        // Update state
        IsHost = true;
        IsConnected = true;

        // Add host to connected players
        int hostId = multiplayerApi.GetUniqueId();
        ConnectedPlayers[hostId] = new PlayerNetworkData
        {
            PlayerId = hostId,
            PlayerName = GameManager.Instance?.LocalPlayer?.PlayerName ?? "Host",
            IsHost = true
        };

        // Update GameManager's local player with the network ID
        if (GameManager.Instance?.LocalPlayer != null)
        {
            GD.Print($"NetworkManager: Host assigned network player ID: {hostId}");
            int oldId = GameManager.Instance.LocalPlayer.PlayerId;

            // Update the local player with the network-assigned ID
            GameManager.Instance.LocalPlayer.PlayerId = hostId;
            GameManager.Instance.LocalPlayer.PlayerName = $"Host_Player_{hostId}";

            // Re-add to connected players with correct ID
            if (oldId != hostId)
            {
                GameManager.Instance.ConnectedPlayers.Remove(oldId);
                GameManager.Instance.AddPlayer(hostId, GameManager.Instance.LocalPlayer);
                GD.Print($"NetworkManager: Updated player ID from {oldId} to {hostId}");
            }
            else
            {
                GD.Print($"NetworkManager: Player ID unchanged ({hostId}), no need to re-add");
            }
        }

        GD.Print($"NetworkManager: Hosting started successfully with room code: {CurrentRoomCode}");
        GD.Print($"NetworkManager: Host ID: {hostId}, Port: {currentServerPort}");

        EmitSignal(SignalName.ServerStarted, CurrentRoomCode);
        return CurrentRoomCode;
    }

    /// <summary>
    /// Connect to a hosted game using room code
    /// </summary>
    /// <param name="roomCode">6-digit room code</param>
    public void ConnectToGame(string roomCode)
    {
        GD.Print($"NetworkManager: Connecting to game with room code: {roomCode}");

        // Ensure multiplayer API is ready
        if (multiplayerApi == null)
        {
            GD.PrintErr("NetworkManager: Multiplayer API not initialized!");
            EmitSignal(SignalName.ConnectionFailed, "Multiplayer API not ready");
            return;
        }

        // Validate room code format
        if (roomCode.Length != 6 || !int.TryParse(roomCode, out _))
        {
            GD.PrintErr("NetworkManager: Invalid room code format");
            EmitSignal(SignalName.ConnectionFailed, "Invalid room code format");
            return;
        }

        CurrentRoomCode = roomCode;

        // TODO: In a real implementation, you'd look up the host's IP address
        // For now, we'll assume localhost for testing
        string hostAddress = "127.0.0.1";

        // Create ENet multiplayer peer
        var peer = new ENetMultiplayerPeer();
        var error = peer.CreateClient(hostAddress, DefaultPort);
        if (error != Error.Ok)
        {
            GD.PrintErr($"NetworkManager: Failed to create client peer: {error}");
            EmitSignal(SignalName.ConnectionFailed, $"Failed to create client: {error}");
            return;
        }

        // Set the multiplayer peer directly on the tree's multiplayer
        multiplayerApi.MultiplayerPeer = peer;
        GD.Print($"NetworkManager: Client multiplayer peer set successfully");

        // Update state
        IsClient = true;

        GD.Print($"NetworkManager: Connection attempt initiated to {hostAddress}:{DefaultPort}");
    }

    /// <summary>
    /// Generate a 6-digit room code
    /// </summary>
    /// <returns>Random 6-digit room code</returns>
    public string GenerateRoomCode()
    {
        var random = new Random();
        int code = random.Next(100000, 999999);

        GD.Print($"NetworkManager: Generated room code: {code}");
        return code.ToString();
    }

    /// <summary>
    /// Disconnect from current game
    /// </summary>
    public void Disconnect()
    {
        GD.Print("NetworkManager: Disconnecting from game...");

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

        GD.Print("NetworkManager: Disconnected successfully");
    }

    /// <summary>
    /// Send player data to all connected clients
    /// </summary>
    /// <param name="playerData">Player data to send</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void SendPlayerData(PlayerData playerData)
    {
        GD.Print($"NetworkManager: Sending player data for {playerData.PlayerName}");

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
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void ReceivePlayerData(int playerId, string playerName)
    {
        GD.Print($"NetworkManager: Received player data for {playerName} (ID: {playerId})");

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
    /// Send chat message to all players
    /// </summary>
    /// <param name="message">Chat message to send</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void SendChatMessage(string message)
    {
        GD.Print($"NetworkManager: Sending chat message: {message}");

        // TODO: Implement chat message handling
        // This would integrate with the chat intimidation system
    }

    /// <summary>
    /// Send sabotage action to all players
    /// </summary>
    /// <param name="sourcePlayerId">Player performing sabotage</param>
    /// <param name="sabotageType">Type of sabotage</param>
    /// <param name="targetPosition">Target position for sabotage</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void SendSabotageAction(int sourcePlayerId, SabotageType sabotageType, Vector2 targetPosition)
    {
        GD.Print($"NetworkManager: Sending sabotage action: {sabotageType} from player {sourcePlayerId}");

        // TODO: Implement sabotage synchronization
        // This would trigger sabotage effects on all clients
    }

    /// <summary>
    /// Send card play action to all players
    /// </summary>
    /// <param name="playerId">Player playing card</param>
    /// <param name="cardId">Card being played</param>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void SendCardPlay(int playerId, int cardId)
    {
        GD.Print($"NetworkManager: Sending card play: Player {playerId} plays card {cardId}");

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
        GD.Print($"NetworkManager: Peer connected - ID: {playerId}");

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
            GD.Print($"NetworkManager: Added player {playerId} to GameManager");

            // If we're host and this is the second player, move to card game phase
            if (IsHost && ConnectedPlayers.Count >= 2)
            {
                GD.Print("NetworkManager: Enough players connected, transitioning to card phase");
                GameManager.Instance.ChangePhase(GameManager.GamePhase.CardPhase);
            }
        }

        // If we're the host, send welcome message
        if (IsHost)
        {
            GD.Print($"NetworkManager: Welcoming new player {playerId}");

            // Send current game state to new player
            if (GameManager.Instance?.LocalPlayer != null)
            {
                RpcId(playerId, MethodName.ReceivePlayerData,
                    GameManager.Instance.LocalPlayer.PlayerId,
                    GameManager.Instance.LocalPlayer);
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
        GD.Print($"NetworkManager: Peer disconnected - ID: {playerId}");

        // Remove from connected players
        if (ConnectedPlayers.ContainsKey(playerId))
        {
            string playerName = ConnectedPlayers[playerId].PlayerName;
            ConnectedPlayers.Remove(playerId);

            GD.Print($"NetworkManager: Player {playerName} removed from connected players");
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
        GD.Print("NetworkManager: Successfully connected to server");

        IsConnected = true;

        // Update local player ID to use the multiplayer unique ID
        if (GameManager.Instance?.LocalPlayer != null)
        {
            int networkPlayerId = GetTree().GetMultiplayer().GetUniqueId();
            GD.Print($"NetworkManager: Client assigned network player ID: {networkPlayerId}");

            int oldId = GameManager.Instance.LocalPlayer.PlayerId;

            // Update the local player with the network-assigned ID
            GameManager.Instance.LocalPlayer.PlayerId = networkPlayerId;
            GameManager.Instance.LocalPlayer.PlayerName = $"Client_Player_{networkPlayerId}";

            // Re-add to connected players with correct ID
            if (oldId != networkPlayerId)
            {
                GameManager.Instance.ConnectedPlayers.Remove(oldId); // Remove old temp ID
                GameManager.Instance.AddPlayer(networkPlayerId, GameManager.Instance.LocalPlayer);
                GD.Print($"NetworkManager: Updated client ID from {oldId} to {networkPlayerId}");
            }
            else
            {
                GD.Print($"NetworkManager: Client ID unchanged ({networkPlayerId}), no need to re-add");
            }

            // Send our updated player data to the server
            SendPlayerData(GameManager.Instance.LocalPlayer);
        }

        // Transition to card game phase
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangePhase(GameManager.GamePhase.CardPhase);
        }

        EmitSignal(SignalName.ClientConnectedToServer);
    }

    /// <summary>
    /// Handle connection failed event
    /// </summary>
    private void OnConnectionFailed()
    {
        GD.PrintErr("NetworkManager: Connection to server failed");

        IsClient = false;
        IsConnected = false;
        CurrentRoomCode = "";

        EmitSignal(SignalName.ConnectionFailed, "Connection to server failed");
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

    public override void _ExitTree()
    {
        GD.Print("NetworkManager: Cleaning up network manager...");

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