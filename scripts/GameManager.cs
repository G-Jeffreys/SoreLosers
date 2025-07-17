using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Main game manager that controls overall game state, phases, and system coordination
/// Handles transitions between menu, lobby, card game, and real-time phases
/// </summary>
public partial class GameManager : Node
{
    // Singleton instance for global access
    public static GameManager Instance { get; private set; }

    // Game state tracking
    public enum GamePhase
    {
        MainMenu,
        HostLobby,
        ClientLobby,
        CardPhase,
        RealTimePhase,
        Results
    }

    // Player location tracking for concurrent gameplay
    public enum PlayerLocation
    {
        AtTable,    // Player is at the card table and can play cards
        InKitchen   // Player is in the kitchen and can move/gather items
    }

    [Export]
    public GamePhase CurrentPhase { get; private set; } = GamePhase.MainMenu;

    // Player management
    public Dictionary<int, PlayerData> ConnectedPlayers { get; private set; } = new();
    public PlayerData LocalPlayer { get; private set; }

    // Player location tracking for concurrent gameplay
    public Dictionary<int, PlayerLocation> PlayerLocations { get; private set; } = new();

    // Game configuration
    [Export]
    public float TurnTimerDuration = 10.0f; // 10 second turn timer from PRD
    [Export]
    public int MaxPlayers = 4; // Up to 4 players from PRD
    [Export]
    public float SabotageEffectDuration = 30.0f; // 30 second sabotage duration from PRD
    [Export]
    public float StinkBombRadius = 160.0f; // 160px radius from PRD

    // System references
    public NetworkManager NetworkManager { get; private set; }
    public CardManager CardManager { get; private set; }
    public SabotageManager SabotageManager { get; private set; }
    public UIManager UIManager { get; private set; }

    // Events for system communication
    [Signal]
    public delegate void PhaseChangedEventHandler(int newPhase);

    [Signal]
    public delegate void PlayerJoinedEventHandler(int playerId, PlayerData playerData);

    [Signal]
    public delegate void PlayerLeftEventHandler(int playerId);

    [Signal]
    public delegate void TurnTimerExpiredEventHandler();

    // New events for player location changes
    [Signal]
    public delegate void PlayerLocationChangedEventHandler(int playerId, int newLocation);

    [Signal]
    public delegate void PlayerLeftTableEventHandler(int playerId);

    [Signal]
    public delegate void PlayerReturnedToTableEventHandler(int playerId);

    // Host lock file path for cleanup
    private string hostLockFilePath;

    public override void _Ready()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GD.PrintErr("GameManager: Multiple instances detected! Destroying duplicate.");
            QueueFree();
            return;
        }

        // Initialize core systems
        InitializeSystems();

        // Set up initial local player
        InitializeLocalPlayer();

        // Note: Removed automatic debug networking - users should manually choose host/join
        // This prevents both instances from trying to auto-host and causing conflicts
    }



    /// <summary>
    /// Initialize all core game systems
    /// </summary>
    private void InitializeSystems()
    {
        // Get autoload system references (managers are autoloads in project.godot)
        NetworkManager = GetNode<NetworkManager>("/root/NetworkManager");
        CardManager = GetNode<CardManager>("/root/CardManager");
        SabotageManager = GetNode<SabotageManager>("/root/SabotageManager");
        UIManager = GetNode<UIManager>("/root/UIManager");

        // Connect system signals
        ConnectSystemSignals();
    }

    /// <summary>
    /// Connect signals between systems for communication
    /// </summary>
    private void ConnectSystemSignals()
    {
        // Connect phase change signals to all systems
        PhaseChanged += OnPhaseChanged;

        // Connect player management signals
        PlayerJoined += OnPlayerJoined;
        PlayerLeft += OnPlayerLeft;
    }

    /// <summary>
    /// Initialize the local player with default stats
    /// </summary>
    private void InitializeLocalPlayer()
    {
        // Generate unique player ID for testing multiple instances
        // In debug mode, use a random ID to simulate different players
        int playerId = 0; // Default host ID
        string playerName = "Player";

        if (OS.IsDebugBuild())
        {
            // For debug mode, use a more reliable instance detection method
            // Use process ID + timestamp to create unique temporary player names
            int processId = OS.GetProcessId();
            long timestamp = (long)Time.GetUnixTimeFromSystem();
            
            // Create a more unique identifier combining PID and timestamp
            string instanceId = $"{processId}_{timestamp % 10000}";
            
            // Test if we can bind to the detection port (more reliable than UDP test)
            ushort testPort = 12345;
            var tcp = new TcpServer();
            bool isFirstInstance = tcp.Listen(testPort) == Error.Ok;
            tcp.Stop();
            
            GD.Print($"GameManager: Instance detection - Process ID: {processId}, Timestamp: {timestamp}, Port test: {isFirstInstance}");

            if (isFirstInstance)
            {
                playerId = 0; // Host is always Player 0
                playerName = $"Host_Player_{instanceId}";
                GD.Print("GameManager: Detected as FIRST instance (potential host)");
            }
            else
            {
                playerId = 1000 + (int)(timestamp % 1000); // Unique client ID
                playerName = $"Client_Player_{instanceId}";
                GD.Print("GameManager: Detected as SECOND instance (potential client)");
            }
        }

        // Create local player with default stats from PRD
        LocalPlayer = new PlayerData
        {
            PlayerId = playerId,
            PlayerName = playerName,
            ThrowPower = 1, // Level 1 (20% coverage)
            MoveSpeed = 1, // Level 1 (110 px/s)
            Composure = 1, // Level 1 (100% blur strength)
            TotalXP = 0
        };
    }

    /// <summary>
    /// Set up automatic networking for debug mode - first instance hosts, second connects
    /// </summary>
    private void SetupDebugNetworking()
    {
        // Use a more reliable method to detect if we should host or connect
        // Check if there's already a host running by trying to connect to the default port
        bool hostExists = CheckIfHostExists();

        if (!hostExists)
        {
            // No host found - start hosting
            CallDeferred(MethodName.StartHosting);
        }
        else
        {
            // Host exists - connect as client
            CallDeferred(MethodName.JoinGameAsClient);
        }
    }

    /// <summary>
    /// Check if a host is already running using a more reliable method
    /// </summary>
    /// <returns>True if host exists</returns>
    private bool CheckIfHostExists()
    {
        // Use a lock file approach for more reliable host detection
        string lockFilePath = OS.GetUserDataDir() + "/host.lock";

        try
        {
            // Try to create/open the lock file exclusively
            using var file = FileAccess.Open(lockFilePath, FileAccess.ModeFlags.Write);
            if (file == null)
            {
                // File already exists and locked by another instance
                return true;
            }

            // We can create the file, so we should be the host
            file.StoreString($"Host started at {Time.GetUnixTimeFromSystem()}");
            file.Close();

            // Store path for cleanup
            hostLockFilePath = lockFilePath;

            return false;
        }
        catch (Exception ex)
        {
            GD.Print($"GameManager: Error checking host lock: {ex.Message} - defaulting to client");
            return true; // Default to client if there's an error
        }
    }

    /// <summary>
    /// Connect as client to the host for debug testing
    /// </summary>
    private void JoinGameAsClient()
    {
        // Short delay to ensure host is ready
        GetTree().CreateTimer(0.5f).Timeout += () =>
        {
            if (NetworkManager != null)
            {
                // For localhost testing, we know the host is on default port
                NetworkManager.ConnectToGame("123456"); // Use dummy room code for localhost
            }
        };
    }

    // Scene paths for different game phases
    private readonly Dictionary<GamePhase, string> sceneMap = new()
    {
        { GamePhase.MainMenu, "res://scenes/MainMenu.tscn" },
        { GamePhase.HostLobby, "res://scenes/Lobby.tscn" },
        { GamePhase.ClientLobby, "res://scenes/Lobby.tscn" },
        { GamePhase.CardPhase, "res://scenes/CardGame.tscn" },
        { GamePhase.RealTimePhase, "res://scenes/RealTime.tscn" },
        { GamePhase.Results, "res://scenes/Results.tscn" }
    };

    /// <summary>
    /// Change the current game phase and switch to appropriate scene
    /// </summary>
    /// <param name="newPhase">The new phase to transition to</param>
    public void ChangePhase(GamePhase newPhase)
    {
        GD.Print($"GameManager: Phase transition: {CurrentPhase} -> {newPhase}");

        var previousPhase = CurrentPhase;
        CurrentPhase = newPhase;

        // Emit phase change signal first so systems can prepare
        EmitSignal(SignalName.PhaseChanged, (int)newPhase);

        // Switch scene if we have a mapping for this phase
        if (sceneMap.ContainsKey(newPhase))
        {
            string scenePath = sceneMap[newPhase];

            // Use call_deferred to ensure scene switching happens safely
            GetTree().CallDeferred("change_scene_to_file", scenePath);
        }
    }

    /// <summary>
    /// Add a player to the game
    /// </summary>
    /// <param name="playerId">Unique player ID</param>
    /// <param name="playerData">Player data including stats and profile</param>
    public void AddPlayer(int playerId, PlayerData playerData)
    {
        bool isNewPlayer = !ConnectedPlayers.ContainsKey(playerId);
        if (!isNewPlayer)
        {
            GD.Print($"GameManager: Player ID {playerId} already exists! Updating data.");
        }

        ConnectedPlayers[playerId] = playerData;

        // Initialize player location - new players start at table
        if (isNewPlayer)
        {
            PlayerLocations[playerId] = PlayerLocation.AtTable;
        }

        // Only emit player joined signal for truly new players
        if (isNewPlayer)
        {
            EmitSignal(SignalName.PlayerJoined, playerId, playerData);
        }
    }

    /// <summary>
    /// Remove a player from the game
    /// </summary>
    /// <param name="playerId">ID of player to remove</param>
    public void RemovePlayer(int playerId)
    {
        if (ConnectedPlayers.ContainsKey(playerId))
        {
            var playerName = ConnectedPlayers[playerId].PlayerName;
            ConnectedPlayers.Remove(playerId);

            // Remove player location tracking
            PlayerLocations.Remove(playerId);

            // Emit player left signal
            EmitSignal(SignalName.PlayerLeft, playerId);

            GD.Print($"GameManager: Player {playerName} removed. Remaining players: {ConnectedPlayers.Count}");
        }
        else
        {
            GD.PrintErr($"GameManager: Cannot remove player ID {playerId} - not found in connected players");
        }
    }

    /// <summary>
    /// Get player data by ID
    /// </summary>
    /// <param name="playerId">Player ID to look up</param>
    /// <returns>Player data if found, null otherwise</returns>
    public PlayerData GetPlayer(int playerId)
    {
        ConnectedPlayers.TryGetValue(playerId, out var playerData);
        return playerData;
    }

    /// <summary>
    /// Get player's current location (AtTable or InKitchen)
    /// </summary>
    /// <param name="playerId">Player ID to check</param>
    /// <returns>Player location, defaults to AtTable if not found</returns>
    public PlayerLocation GetPlayerLocation(int playerId)
    {
        return PlayerLocations.GetValueOrDefault(playerId, PlayerLocation.AtTable);
    }

    /// <summary>
    /// Set a player's location (AtTable or InKitchen)
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <param name="newLocation">New location</param>
    public void SetPlayerLocation(int playerId, PlayerLocation newLocation)
    {
        if (!ConnectedPlayers.ContainsKey(playerId))
        {
            GD.PrintErr($"GameManager: Cannot set location for unknown player {playerId}");
            return;
        }

        var oldLocation = GetPlayerLocation(playerId);
        if (oldLocation == newLocation)
        {
            return;
        }

        PlayerLocations[playerId] = newLocation;
        GD.Print($"GameManager: Player {playerId} moved from {oldLocation} to {newLocation}");

        // Emit appropriate signals
        EmitSignal(SignalName.PlayerLocationChanged, playerId, (int)newLocation);

        if (newLocation == PlayerLocation.InKitchen)
        {
            EmitSignal(SignalName.PlayerLeftTable, playerId);
        }
        else if (newLocation == PlayerLocation.AtTable)
        {
            EmitSignal(SignalName.PlayerReturnedToTable, playerId);
        }
    }

    /// <summary>
    /// Player leaves the table to go to the kitchen
    /// </summary>
    /// <param name="playerId">Player ID</param>
    public void PlayerLeaveTable(int playerId)
    {
        SetPlayerLocation(playerId, PlayerLocation.InKitchen);
    }

    /// <summary>
    /// Player returns to the table from the kitchen
    /// </summary>
    /// <param name="playerId">Player ID</param>
    public void PlayerReturnToTable(int playerId)
    {
        SetPlayerLocation(playerId, PlayerLocation.AtTable);
    }

    /// <summary>
    /// Check if a player is currently at the table and can play cards
    /// </summary>
    /// <param name="playerId">Player ID to check</param>
    /// <returns>True if player is at table</returns>
    public bool IsPlayerAtTable(int playerId)
    {
        return GetPlayerLocation(playerId) == PlayerLocation.AtTable;
    }

    /// <summary>
    /// Get list of players currently at the table
    /// </summary>
    /// <returns>List of player IDs at table</returns>
    public List<int> GetPlayersAtTable()
    {
        var playersAtTable = new List<int>();
        foreach (var kvp in PlayerLocations)
        {
            if (kvp.Value == PlayerLocation.AtTable)
            {
                playersAtTable.Add(kvp.Key);
            }
        }
        return playersAtTable;
    }

    /// <summary>
    /// Get list of players currently in the kitchen
    /// </summary>
    /// <returns>List of player IDs in kitchen</returns>
    public List<int> GetPlayersInKitchen()
    {
        var playersInKitchen = new List<int>();
        foreach (var kvp in PlayerLocations)
        {
            if (kvp.Value == PlayerLocation.InKitchen)
            {
                playersInKitchen.Add(kvp.Key);
            }
        }
        return playersInKitchen;
    }

    /// <summary>
    /// Start hosting a game
    /// </summary>
    public void StartHosting()
    {
        GD.Print($"GameManager: StartHosting called - LocalPlayer: {LocalPlayer?.PlayerName} (ID: {LocalPlayer?.PlayerId})");
        
        // Start hosting through NetworkManager
        if (NetworkManager != null)
        {
            string roomCode = NetworkManager.StartHosting();
            if (string.IsNullOrEmpty(roomCode))
            {
                GD.PrintErr("GameManager: Failed to start hosting");
                return;
            }
            
            GD.Print($"GameManager: Hosting started successfully - Room code: {roomCode}");
        }

        // Transition to host lobby phase
        ChangePhase(GamePhase.HostLobby);

        // Add local player as host
        AddPlayer(LocalPlayer.PlayerId, LocalPlayer);
        
        GD.Print($"GameManager: Host lobby setup complete - Phase: {CurrentPhase}");
    }

    /// <summary>
    /// Join a game as client
    /// </summary>
    /// <param name="roomCode">6-digit room code from PRD</param>
    public void JoinGame(string roomCode)
    {
        GD.Print($"GameManager: JoinGame called - LocalPlayer: {LocalPlayer?.PlayerName} (ID: {LocalPlayer?.PlayerId}), RoomCode: {roomCode}");
        
        // Validate room code
        if (NetworkManager == null || !NetworkManager.IsValidRoomCode(roomCode))
        {
            GD.PrintErr("GameManager: Invalid room code or NetworkManager not available");
            return;
        }

        // Transition to client lobby phase
        ChangePhase(GamePhase.ClientLobby);

        // Connect to the game using NetworkManager
        NetworkManager.ConnectToGame(roomCode);
        
        GD.Print($"GameManager: Client connection initiated - Phase: {CurrentPhase}");
    }

    /// <summary>
    /// Start the card game phase
    /// </summary>
    public void StartCardGame()
    {
        // Remove the premature player count check - we'll add AI players automatically
        // The actual player count validation happens in CardManager.ExecuteGameStart()

        // Transition to card phase locally
        ChangePhase(GamePhase.CardPhase);

        // CRITICAL FIX: Actually start the CardManager game with connected players
        var playerIds = new List<int>(ConnectedPlayers.Keys);

        // Use CallDeferred to ensure CardManager is ready after scene change
        CallDeferred(MethodName.StartCardManagerGame, playerIds.ToArray());

        // Notify all clients about game start via RPC
        if (NetworkManager != null && NetworkManager.IsHost)
        {
            Rpc(MethodName.OnGameStarted);
        }
    }

    /// <summary>
    /// Deferred method to start CardManager game after scene change
    /// </summary>
    /// <param name="playerIds">Array of player IDs</param>
    private void StartCardManagerGame(int[] playerIds)
    {
        if (CardManager == null)
        {
            GD.PrintErr("GameManager: CardManager not available - cannot start game");
            return;
        }

        // CRITICAL DEBUG: Log initial state
        GD.Print($"GameManager: StartCardManagerGame called with {playerIds.Length} players");
        GD.Print($"GameManager: Network state - IsConnected: {NetworkManager?.IsConnected}, IsHost: {NetworkManager?.IsHost}");
        GD.Print($"GameManager: Local player: {LocalPlayer?.PlayerId} ({LocalPlayer?.PlayerName})");
        
        // Log incoming player IDs
        for (int i = 0; i < playerIds.Length; i++)
        {
            var playerData = GetPlayer(playerIds[i]);
            var isLocal = LocalPlayer?.PlayerId == playerIds[i];
            GD.Print($"  - Input Player {i}: {playerIds[i]} ({playerData?.PlayerName ?? "Unknown"}) (Local: {isLocal})");
        }

        // Create final player list and add AI players to fill up to 4 total players
        var finalPlayerIds = new List<int>(playerIds);

        // CRITICAL FIX: Only HOST creates and manages AI players
        if (NetworkManager != null && NetworkManager.IsConnected)
        {
            if (NetworkManager.IsHost)
            {
                GD.Print($"GameManager: HOST - Creating AI players and syncing to clients");
                
                // Host: Create AI players and synchronize complete player list to clients
                CreateAndSyncAIPlayers(finalPlayerIds);
                
                // CRITICAL DEBUG: Log final player list before starting game
                GD.Print($"GameManager: HOST - Final player list before CardManager.StartGame:");
                for (int i = 0; i < finalPlayerIds.Count; i++)
                {
                    var playerData = GetPlayer(finalPlayerIds[i]);
                    var isLocal = LocalPlayer?.PlayerId == finalPlayerIds[i];
                    GD.Print($"  - Final Player {i}: {finalPlayerIds[i]} ({playerData?.PlayerName ?? "Unknown"}) (Local: {isLocal})");
                }
                
                // CRITICAL FIX: Add delay to ensure NetworkSyncPlayers RPC completes before NetworkStartGame
                GetTree().CreateTimer(0.1f).Timeout += () => 
                {
                    GD.Print("GameManager: HOST - Starting card game after player sync delay");
                    CardManager.StartGame(finalPlayerIds);
                };
            }
            else
            {
                // Clients should never reach this point - they wait for NetworkStartGame RPC
                GD.PrintErr("GameManager: ERROR - Client should not call StartCardManagerGame!");
                return;
            }
        }
        else
        {
            GD.Print($"GameManager: SINGLE PLAYER - Creating AI players locally");
            
            // Single-player game: Create AI players locally
            CreateAIPlayersLocally(finalPlayerIds);
            
            // CRITICAL DEBUG: Log final player list for single-player
            GD.Print($"GameManager: SINGLE PLAYER - Final player list:");
            for (int i = 0; i < finalPlayerIds.Count; i++)
            {
                var playerData = GetPlayer(finalPlayerIds[i]);
                var isLocal = LocalPlayer?.PlayerId == finalPlayerIds[i];
                GD.Print($"  - Final Player {i}: {finalPlayerIds[i]} ({playerData?.PlayerName ?? "Unknown"}) (Local: {isLocal})");
            }
            
            CardManager.StartGame(finalPlayerIds);
        }
    }

    /// <summary>
    /// Create AI players and synchronize them to all clients (HOST ONLY)
    /// </summary>
    /// <param name="finalPlayerIds">Final player ID list to populate</param>
    private void CreateAndSyncAIPlayers(List<int> finalPlayerIds)
    {
        GD.Print($"GameManager: HOST creating AI players to fill {4 - finalPlayerIds.Count} slots");
        GD.Print($"GameManager: HOST current player list before AI creation:");
        for (int i = 0; i < finalPlayerIds.Count; i++)
        {
            var playerData = GetPlayer(finalPlayerIds[i]);
            var isLocal = LocalPlayer?.PlayerId == finalPlayerIds[i];
            GD.Print($"  - Existing Player {i}: {finalPlayerIds[i]} ({playerData?.PlayerName ?? "Unknown"}) (Local: {isLocal})");
        }

        // Add AI players with IDs that don't conflict with real players
        int nextAiId = 100; // Start AI IDs at 100 to avoid conflicts
        int aiCount = 0;
        while (finalPlayerIds.Count < 4)
        {
            // Find next available AI ID
            while (finalPlayerIds.Contains(nextAiId) || ConnectedPlayers.ContainsKey(nextAiId))
            {
                nextAiId++;
            }

            finalPlayerIds.Add(nextAiId);

            // Create AI player data
            var aiPlayerData = new PlayerData
            {
                PlayerId = nextAiId,
                PlayerName = $"AI_Player_{nextAiId}",
                ThrowPower = 1,
                MoveSpeed = 1,
                Composure = 1
            };

            // Add AI player to ConnectedPlayers so it's tracked properly
            AddPlayer(nextAiId, aiPlayerData);

            GD.Print($"GameManager: HOST created AI player {aiPlayerData.PlayerName} (ID: {nextAiId}) - Position {finalPlayerIds.Count - 1}");
            nextAiId++;
            aiCount++;
        }

        GD.Print($"GameManager: HOST created {aiCount} AI players. Final player count: {finalPlayerIds.Count}");

        // Synchronize complete player list to all clients
        SyncPlayersToClients();
    }

    /// <summary>
    /// Create AI players locally for single-player games
    /// </summary>
    /// <param name="finalPlayerIds">Final player ID list to populate</param>
    private void CreateAIPlayersLocally(List<int> finalPlayerIds)
    {
        // Add AI players with IDs that don't conflict with real players
        int nextAiId = 100; // Start AI IDs at 100 to avoid conflicts
        while (finalPlayerIds.Count < 4)
        {
            // Find next available AI ID
            while (finalPlayerIds.Contains(nextAiId) || ConnectedPlayers.ContainsKey(nextAiId))
            {
                nextAiId++;
            }

            finalPlayerIds.Add(nextAiId);

            // Create AI player data
            var aiPlayerData = new PlayerData
            {
                PlayerId = nextAiId,
                PlayerName = $"AI_Player_{nextAiId}",
                ThrowPower = 1,
                MoveSpeed = 1,
                Composure = 1
            };

            // Add AI player to ConnectedPlayers so it's tracked properly
            AddPlayer(nextAiId, aiPlayerData);

            nextAiId++;
        }
    }

    /// <summary>
    /// Send complete player list to all clients for synchronization
    /// </summary>
    private void SyncPlayersToClients()
    {
        if (NetworkManager == null || !NetworkManager.IsConnected || !NetworkManager.IsHost)
        {
            return;
        }

        // Prepare arrays for RPC
        var playerIds = new List<int>();
        var playerNames = new List<string>();

        foreach (var kvp in ConnectedPlayers)
        {
            playerIds.Add(kvp.Key);
            playerNames.Add(kvp.Value.PlayerName);
        }

        GD.Print($"GameManager: Syncing {playerIds.Count} players to clients: [{string.Join(", ", playerNames)}]");

        // Send player sync to all clients
        NetworkManager.Rpc(NetworkManager.MethodName.NetworkSyncPlayers, 
            playerIds.ToArray(), 
            playerNames.ToArray());
    }

    /// <summary>
    /// RPC method to notify clients that the game has started
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void OnGameStarted()
    {
        // Client transitions to card phase
        ChangePhase(GamePhase.CardPhase);

        // CRITICAL FIX: Clients should NOT create their own games!
        // They should wait for the host to send the synchronized game state via CardManager.NetworkStartGame()
    }

    /// <summary>
    /// Start the real-time phase for sabotage
    /// </summary>
    public void StartRealTimePhase()
    {
        // Transition to real-time phase
        ChangePhase(GamePhase.RealTimePhase);
    }

    /// <summary>
    /// Start the process of joining a game (called from main menu)
    /// </summary>
    public void StartJoining()
    {
        // Transition to client lobby phase to show join UI
        ChangePhase(GamePhase.ClientLobby);
    }

    /// <summary>
    /// Start a new match (called from results screen)
    /// </summary>
    public void StartNewMatch()
    {
        // Remove the player count check - we add AI players automatically
        // Let CardManager handle the validation after AI players are added

        // Transition back to card phase for new game
        ChangePhase(GamePhase.CardPhase);
    }

    /// <summary>
    /// Return to main menu (called from results screen)
    /// </summary>
    public void ReturnToMainMenu()
    {
        // Clear connected players except local player
        ConnectedPlayers.Clear();
        AddPlayer(LocalPlayer.PlayerId, LocalPlayer);

        // Transition back to main menu
        ChangePhase(GamePhase.MainMenu);
    }

    /// <summary>
    /// Start a single-player game with AI opponents (for testing)
    /// </summary>
    public void StartSinglePlayerGame()
    {
        // Ensure local player is added
        if (!ConnectedPlayers.ContainsKey(LocalPlayer.PlayerId))
        {
            AddPlayer(LocalPlayer.PlayerId, LocalPlayer);
        }

        // Transition directly to card phase
        ChangePhase(GamePhase.CardPhase);

        // Start CardManager with just the local player (AI will be added automatically)
        var playerIds = new List<int> { LocalPlayer.PlayerId };
        CallDeferred(MethodName.StartCardManagerGame, playerIds.ToArray());
    }

    /// <summary>
    /// Handle phase change events
    /// </summary>
    /// <param name="newPhase">New phase as integer</param>
    private void OnPhaseChanged(int newPhase)
    {
        // Phase-specific logic can be added here
        switch ((GamePhase)newPhase)
        {
            case GamePhase.MainMenu:
                break;
            case GamePhase.HostLobby:
                break;
            case GamePhase.ClientLobby:
                break;
            case GamePhase.CardPhase:
                break;
            case GamePhase.RealTimePhase:
                break;
            case GamePhase.Results:
                break;
        }
    }

    /// <summary>
    /// Handle player joined events
    /// </summary>
    /// <param name="playerId">Player ID that joined</param>
    /// <param name="playerData">Data of the player that joined</param>
    private void OnPlayerJoined(int playerId, PlayerData playerData)
    {
        // Update UI, notify other systems, etc.
    }

    /// <summary>
    /// Handle player left events
    /// </summary>
    /// <param name="playerId">Player ID that left</param>
    private void OnPlayerLeft(int playerId)
    {
        // Handle player disconnection logic
        if (ConnectedPlayers.Count == 0)
        {
            ChangePhase(GamePhase.MainMenu);
        }
    }

    public override void _ExitTree()
    {
        // Clean up host lock file if we created it
        if (!string.IsNullOrEmpty(hostLockFilePath) && FileAccess.FileExists(hostLockFilePath))
        {
            DirAccess.RemoveAbsolute(hostLockFilePath);
        }

        // Clear singleton
        if (Instance == this)
        {
            Instance = null;
        }
    }
}