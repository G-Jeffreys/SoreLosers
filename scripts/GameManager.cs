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
        // Check if we're running on a dedicated server
        if (IsRunningOnDedicatedServer())
        {
            GD.Print("GameManager: Running on dedicated server - disabling client functionality");
            SetProcess(false);
            SetProcessInput(false);
            return;
        }

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
    /// Check if we're running on a dedicated server
    /// </summary>
    private bool IsRunningOnDedicatedServer()
    {
        // Multiple checks for dedicated server detection

        // Check 1: DedicatedServer node exists in scene tree
        if (GetTree().GetFirstNodeInGroup("dedicated_server") != null)
            return true;

        // Check 2: Running headless (no display server)
        if (DisplayServer.GetName() == "headless")
            return true;

        // Check 3: Check main scene path
        var currentScene = GetTree().CurrentScene;
        if (currentScene != null && currentScene.SceneFilePath.Contains("DedicatedServer"))
            return true;

        // Check 4: Command line arguments
        var args = OS.GetCmdlineArgs();
        foreach (string arg in args)
        {
            if (arg.Contains("DedicatedServer") || arg.Contains("--headless"))
                return true;
        }

        return false;
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

            // Use file-based lock for reliable instance detection
            hostLockFilePath = OS.GetUserDataDir() + "/instance.lock";
            bool isFirstInstance = false;

            try
            {
                // CRITICAL FIX: Check if lock file already exists first
                if (FileAccess.FileExists(hostLockFilePath))
                {
                    GD.Print($"GameManager: Lock file already exists at {hostLockFilePath} - this is NOT the first instance");
                    isFirstInstance = false;
                }
                else
                {
                    // File doesn't exist - try to create it (we are first instance)
                    var lockFile = FileAccess.Open(hostLockFilePath, FileAccess.ModeFlags.Write);
                    if (lockFile != null)
                    {
                        lockFile.StoreString($"host_pid_{processId}_{timestamp}");
                        lockFile.Close();
                        isFirstInstance = true;
                        GD.Print($"GameManager: Created host lock file at {hostLockFilePath}");
                    }
                    else
                    {
                        GD.Print($"GameManager: Failed to create lock file - not first instance");
                        isFirstInstance = false;
                    }
                }
            }
            catch (Exception ex)
            {
                GD.Print($"GameManager: Lock file exception: {ex.Message} - assuming not first instance");
                isFirstInstance = false;
            }

            GD.Print($"GameManager: Instance detection - Process ID: {processId}, Timestamp: {timestamp}, File lock: {isFirstInstance}");

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
        GD.Print("GameManager: Setting up debug networking...");

        // Add a small delay to ensure the first instance has time to start hosting
        GetTree().CreateTimer(0.2f).Timeout += () =>
        {
            // Use a more reliable method to detect if we should host or connect
            bool hostExists = CheckIfHostExists();

            GD.Print($"GameManager: Host detection result: {hostExists}");

            if (!hostExists)
            {
                // No host found - start hosting
                GD.Print("GameManager: Starting as HOST");
                CallDeferred(MethodName.StartHosting);
            }
            else
            {
                // Host exists - connect as client
                GD.Print("GameManager: Starting as CLIENT");
                CallDeferred(MethodName.JoinGameAsClient);
            }
        };
    }

    /// <summary>
    /// Check if a host is already running using a more reliable method
    /// </summary>
    /// <returns>True if host exists</returns>
    private bool CheckIfHostExists()
    {
        // CRITICAL FIX: Use simple ENet port test instead of complex async approach
        // Try to create an ENet server on the default port to see if it's already in use
        var testPeer = new ENetMultiplayerPeer();
        try
        {
            var result = testPeer.CreateServer(7777, 1, 0, 0, 0);
            if (result == Error.Ok)
            {
                // We successfully bound to the port, so no host exists
                GD.Print("GameManager: No host detected via ENet port test - will become host");
                testPeer.Close();
                return false;
            }
            else
            {
                // Failed to bind, likely because port is in use by existing host
                GD.Print("GameManager: Host detected via ENet port test - will join as client");
                testPeer.Close();
                return true;
            }
        }
        catch (Exception ex)
        {
            GD.Print($"GameManager: ENet port test failed: {ex.Message} - defaulting to client");
            testPeer?.Close();
            return true; // Default to client if test fails
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

        GD.Print($"GameManager: AddPlayer called - ID: {playerId}, Name: {playerData.PlayerName}, IsNew: {isNewPlayer}");
        GD.Print($"GameManager: Current ConnectedPlayers before add: [{string.Join(", ", ConnectedPlayers.Keys)}]");
        GD.Print($"GameManager: LocalPlayer ID: {LocalPlayer?.PlayerId}, Name: {LocalPlayer?.PlayerName}");

        if (!isNewPlayer)
        {
            GD.Print($"GameManager: Player ID {playerId} already exists! Updating data.");
        }

        ConnectedPlayers[playerId] = playerData;

        // Initialize player location - new players start at table
        if (isNewPlayer)
        {
            PlayerLocations[playerId] = PlayerLocation.AtTable;
            GD.Print($"GameManager: Set new player {playerId} location to AtTable");
        }

        // Only emit player joined signal for truly new players
        if (isNewPlayer)
        {
            EmitSignal(SignalName.PlayerJoined, playerId, playerData);
            GD.Print($"GameManager: Emitted PlayerJoined signal for new player {playerId}");
        }

        GD.Print($"GameManager: Final ConnectedPlayers after add: [{string.Join(", ", ConnectedPlayers.Keys)}]");
        GD.Print($"GameManager: Final ConnectedPlayers count: {ConnectedPlayers.Count}");
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
    /// Start hosting a game - Now creates game on AWS server
    /// </summary>
    public void StartHosting()
    {
        GD.Print("=== GAMEMANAGER: STARTING HOST GAME PROCESS ===");
        GD.Print($"GameManager: StartHosting called - LocalPlayer: {LocalPlayer?.PlayerName} (ID: {LocalPlayer?.PlayerId})");
        GD.Print($"GameManager: Current phase: {CurrentPhase}");
        GD.Print($"GameManager: Current players count: {ConnectedPlayers.Count}");

        // Validate prerequisites
        if (LocalPlayer == null)
        {
            GD.PrintErr("GameManager: LocalPlayer is null - cannot start hosting!");
            return;
        }

        // Start hosting through NetworkManager (connects to AWS server)
        if (NetworkManager != null)
        {
            GD.Print($"GameManager: NetworkManager found - IsHost: {NetworkManager.IsHost}, IsClient: {NetworkManager.IsClient}, IsConnected: {NetworkManager.IsConnected}");

            // Connect signal to handle server response
            if (!NetworkManager.IsConnected(nameof(NetworkManager.ClientConnectedToServer), new Callable(this, nameof(OnHostingServerConnected))))
            {
                NetworkManager.ClientConnectedToServer += OnHostingServerConnected;
                GD.Print("GameManager: Connected to ClientConnectedToServer signal");
            }
            else
            {
                GD.Print("GameManager: Already connected to ClientConnectedToServer signal");
            }

            // Also connect to connection failed signal for error handling
            if (!NetworkManager.IsConnected(nameof(NetworkManager.ConnectionFailed), new Callable(this, nameof(OnHostingConnectionFailed))))
            {
                NetworkManager.ConnectionFailed += OnHostingConnectionFailed;
                GD.Print("GameManager: Connected to ConnectionFailed signal");
            }

            GD.Print("GameManager: About to call NetworkManager.StartHosting()...");
            NetworkManager.StartHosting();
            GD.Print("GameManager: NetworkManager.StartHosting() call completed - waiting for connection...");
        }
        else
        {
            GD.PrintErr("GameManager: NetworkManager not available - cannot start hosting!");
            GD.PrintErr("GameManager: Check if NetworkManager is properly configured as autoload");
        }

        GD.Print("=== GAMEMANAGER: HOST GAME PROCESS INITIATED ===");
    }

    /// <summary>
    /// Handle successful connection to server when hosting
    /// </summary>
    private void OnHostingServerConnected()
    {
        GD.Print("=== GAMEMANAGER: HOST CONNECTION SUCCESS ===");
        GD.Print($"GameManager: Successfully connected to server - Room code: {NetworkManager.CurrentRoomCode}");

        // Disconnect the signals since we only need them once for this hosting attempt
        NetworkManager.ClientConnectedToServer -= OnHostingServerConnected;
        NetworkManager.ConnectionFailed -= OnHostingConnectionFailed;
        GD.Print("GameManager: Disconnected hosting connection signals");

        // Now continue with hosting setup
        GD.Print("GameManager: Transitioning to host lobby phase...");

        // Transition to host lobby phase
        ChangePhase(GamePhase.HostLobby);

        // Add local player as host
        GD.Print($"GameManager: Adding local player as host: {LocalPlayer.PlayerName} (ID: {LocalPlayer.PlayerId})");
        AddPlayer(LocalPlayer.PlayerId, LocalPlayer);

        GD.Print($"GameManager: Host lobby setup complete - Phase: {CurrentPhase}");
        GD.Print("=== GAMEMANAGER: HOST LOBBY READY ===");
    }

    /// <summary>
    /// Handle connection failure when hosting
    /// </summary>
    private void OnHostingConnectionFailed(string reason)
    {
        GD.PrintErr("=== GAMEMANAGER: HOST CONNECTION FAILED ===");
        GD.PrintErr($"GameManager: Failed to connect to server when hosting - Reason: {reason}");

        // Disconnect the signals to clean up
        NetworkManager.ClientConnectedToServer -= OnHostingServerConnected;
        NetworkManager.ConnectionFailed -= OnHostingConnectionFailed;
        GD.Print("GameManager: Disconnected hosting connection signals after failure");

        // Stay in main menu - user can try again or see the error
        GD.Print("GameManager: Staying in main menu after hosting failure");
        GD.PrintErr("=== GAMEMANAGER: HOST CONNECTION FAILED - USER SHOULD SEE ERROR ===");
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
    /// Start the card game phase (HOST ONLY)
    /// </summary>
    public void StartCardGame()
    {
        // CRITICAL FIX: Only host should start the actual game
        if (NetworkManager == null || !NetworkManager.IsHost)
        {
            GD.PrintErr("GameManager: StartCardGame called on non-host - only host can start games!");
            return;
        }

        GD.Print("GameManager: HOST starting card game");

        // Remove the premature player count check - we'll add AI players automatically
        // The actual player count validation happens in CardManager.ExecuteGameStart()

        // Transition to card phase locally
        ChangePhase(GamePhase.CardPhase);

        // CRITICAL FIX: Actually start the CardManager game with connected players
        var playerIds = new List<int>(ConnectedPlayers.Keys);

        // Use CallDeferred to ensure CardManager is ready after scene change
        CallDeferred(MethodName.StartCardManagerGame, playerIds.ToArray());

        // Notify all clients about game start via RPC
        Rpc(MethodName.OnGameStarted);
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

        // CRITICAL FIX: Create deterministic player list sorted by ID to ensure consistent order
        var finalPlayerIds = new List<int>(playerIds);
        finalPlayerIds.Sort(); // Sort to ensure consistent order across instances

        GD.Print($"GameManager: StartCardManagerGame with sorted players: [{string.Join(", ", finalPlayerIds)}]");

        // CRITICAL FIX: Only HOST creates and manages AI players
        if (NetworkManager != null && NetworkManager.IsConnected)
        {
            if (NetworkManager.IsHost)
            {
                // Host: Create AI players and synchronize complete player list to clients
                CreateAndSyncAIPlayers(finalPlayerIds);

                // CRITICAL FIX: Add delay to ensure NetworkSyncPlayers RPC completes before NetworkStartGame
                GetTree().CreateTimer(0.5f).Timeout += () => // Increased from 0.1f to 0.5f
                {
                    GD.Print("=== CRITICAL DEBUG: STARTING CARD GAME AFTER SYNC DELAY ===");
                    GD.Print("GameManager: Starting card game after player sync delay");
                    GD.Print($"GameManager: Final ConnectedPlayers count: {ConnectedPlayers.Count}");
                    GD.Print($"GameManager: Final ConnectedPlayers: [{string.Join(", ", ConnectedPlayers.Keys)}]");
                    CardManager.StartGame(finalPlayerIds);
                    GD.Print("=== END STARTING CARD GAME ===");
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
            // Single-player game: Create AI players locally
            CreateAIPlayersLocally(finalPlayerIds);
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
        GD.Print($"GameManager: HOST starting with {finalPlayerIds.Count} real players: [{string.Join(", ", finalPlayerIds)}]");

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

            GD.Print($"GameManager: HOST created AI player {aiPlayerData.PlayerName} (ID: {nextAiId})");
            nextAiId++;
        }

        // CRITICAL FIX: Sort the final list to ensure deterministic order
        finalPlayerIds.Sort();
        GD.Print($"GameManager: HOST final sorted player list: [{string.Join(", ", finalPlayerIds)}]");

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
            GD.Print("GameManager: Skipping SyncPlayersToClients - not host or not connected");
            return;
        }

        // CRITICAL FIX: Prepare arrays in sorted order for deterministic sync
        var sortedPlayerIds = ConnectedPlayers.Keys.ToList();
        sortedPlayerIds.Sort(); // Ensure consistent order

        var playerIds = new List<int>();
        var playerNames = new List<string>();

        foreach (var playerId in sortedPlayerIds)
        {
            playerIds.Add(playerId);
            playerNames.Add(ConnectedPlayers[playerId].PlayerName);
        }

        GD.Print($"GameManager: HOST syncing {playerIds.Count} players to clients in sorted order:");
        for (int i = 0; i < playerIds.Count; i++)
        {
            GD.Print($"  - Player {playerIds[i]}: {playerNames[i]}");
        }

        GD.Print("=== CRITICAL DEBUG: SENDING NetworkSyncPlayers RPC ===");
        GD.Print($"GameManager: HOST about to send NetworkSyncPlayers RPC to all clients");
        GD.Print($"GameManager: HOST NetworkManager.IsHost: {NetworkManager.IsHost}, IsConnected: {NetworkManager.IsConnected}");

        // Send player sync to all clients
        NetworkManager.Rpc(NetworkManager.MethodName.NetworkSyncPlayers,
            playerIds.ToArray(),
            playerNames.ToArray());

        GD.Print("GameManager: HOST NetworkSyncPlayers RPC sent to all clients");
        GD.Print("=== END SENDING NetworkSyncPlayers RPC ===");
    }

    /// <summary>
    /// RPC method to notify clients that the game has started
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
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