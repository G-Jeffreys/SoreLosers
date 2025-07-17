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
    }

    /// <summary>
    /// Start hosting a game with generated room code
    /// </summary>
    /// <returns>6-digit room code for other players to join</returns>
    public string StartHosting()
    {
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

        // Try to initialize ENet more explicitly
        try
        {
            // Try ports starting from DefaultPort
            for (int port = DefaultPort; port < DefaultPort + 10; port++)
            {
                // Create a fresh peer for each attempt to avoid state issues
                peer = new ENetMultiplayerPeer();

                // Try creating server with minimal parameters first
                error = peer.CreateServer(port, MaxClients, 0, 0, 0);

                if (error == Error.Ok)
                {
                    currentServerPort = port;
                    GD.Print($"NetworkManager: Server created on port {port}");
                    break;
                }
                else
                {
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

        // CRITICAL FIX: Set up multiplayer authority for host
        int hostId = multiplayerApi.GetUniqueId();

        // Set host as authority for CardManager and other critical nodes
        SetupMultiplayerAuthority(hostId);

        // Update state
        IsHost = true;
        IsConnected = true;

        GD.Print($"NetworkManager: HOST STATUS SET - IsHost: {IsHost}, IsClient: {IsClient}, IsConnected: {IsConnected}");

        // Add host to connected players
        ConnectedPlayers[hostId] = new PlayerNetworkData
        {
            PlayerId = hostId,
            PlayerName = "Host", // Use clear host designation
            IsHost = true
        };

        // CRITICAL FIX: Update GameManager's local player with the network ID but preserve the original name
        if (GameManager.Instance?.LocalPlayer != null)
        {
            int oldId = GameManager.Instance.LocalPlayer.PlayerId;
            string originalName = GameManager.Instance.LocalPlayer.PlayerName;

            // IMPORTANT: Update both ID and name to reflect network role
            GameManager.Instance.LocalPlayer.PlayerId = hostId;
            GameManager.Instance.LocalPlayer.PlayerName = "Host"; // Clear host identification

            // Re-add to connected players with correct ID if changed
            if (oldId != hostId)
            {
                GD.Print($"NetworkManager: HOST ID changed from {oldId} to {hostId}, updating ConnectedPlayers");
                GameManager.Instance.ConnectedPlayers.Remove(oldId);
                GameManager.Instance.AddPlayer(hostId, GameManager.Instance.LocalPlayer);
            }
            else
            {
                GD.Print($"NetworkManager: HOST ID unchanged ({hostId}), updating existing player");
                GameManager.Instance.AddPlayer(hostId, GameManager.Instance.LocalPlayer);
            }
        }

        GD.Print($"NetworkManager: Hosting started - Room code: {CurrentRoomCode}, Host ID: {hostId}, Port: {currentServerPort}");

        EmitSignal(SignalName.ServerStarted, CurrentRoomCode);
        return CurrentRoomCode;
    }

    /// <summary>
    /// Connect to a hosted game using room code
    /// </summary>
    /// <param name="roomCode">6-digit room code</param>
    public void ConnectToGame(string roomCode)
    {
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

        // Update state
        IsClient = true;

        GD.Print($"NetworkManager: CLIENT CONNECTION STARTED - IsHost: {IsHost}, IsClient: {IsClient}, IsConnected: {IsConnected}");
        GD.Print($"NetworkManager: Connecting to {hostAddress}:{DefaultPort} with room code {roomCode}");
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
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
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
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
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
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
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
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
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
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
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
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
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
        GD.Print("NetworkManager: Connected to server");

        IsConnected = true;
        GD.Print($"NetworkManager: CLIENT CONNECTED TO SERVER - IsHost: {IsHost}, IsClient: {IsClient}, IsConnected: {IsConnected}");

        // CRITICAL FIX: Update local player ID to use the multiplayer unique ID but preserve name for debugging
        if (GameManager.Instance?.LocalPlayer != null)
        {
            int networkPlayerId = GetTree().GetMultiplayer().GetUniqueId();
            int oldId = GameManager.Instance.LocalPlayer.PlayerId;
            string originalName = GameManager.Instance.LocalPlayer.PlayerName;

            GD.Print($"NetworkManager: CLIENT updating player ID from {oldId} to {networkPlayerId}");

            // IMPORTANT: Only update the ID, keep original distinguishable name
            GameManager.Instance.LocalPlayer.PlayerId = networkPlayerId;
            GameManager.Instance.LocalPlayer.PlayerName = "Client"; // Clear client identification

            // Re-add to connected players with correct ID if changed
            if (oldId != networkPlayerId)
            {
                GD.Print($"NetworkManager: CLIENT ID changed from {oldId} to {networkPlayerId}, updating ConnectedPlayers");
                GameManager.Instance.ConnectedPlayers.Remove(oldId); // Remove old temp ID
                GameManager.Instance.AddPlayer(networkPlayerId, GameManager.Instance.LocalPlayer);
            }
            else
            {
                GD.Print($"NetworkManager: CLIENT ID unchanged ({networkPlayerId}), updating existing player");
                GameManager.Instance.AddPlayer(networkPlayerId, GameManager.Instance.LocalPlayer);
            }

            // Send our updated player data to the server
            SendPlayerData(GameManager.Instance.LocalPlayer);
        }

        // CRITICAL FIX: Don't auto-transition to card phase
        // Let the host control when the game actually begins via StartCardGame()

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