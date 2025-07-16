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
        GD.Print("GameManager: Initializing game systems...");

        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
            GD.Print("GameManager: Singleton instance created");
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

        GD.Print($"GameManager: Initialization complete. Current phase: {CurrentPhase}");
    }



    /// <summary>
    /// Initialize all core game systems
    /// </summary>
    private void InitializeSystems()
    {
        GD.Print("GameManager: Initializing core systems...");

        // Get autoload system references (managers are autoloads in project.godot)
        NetworkManager = GetNode<NetworkManager>("/root/NetworkManager");
        CardManager = GetNode<CardManager>("/root/CardManager");
        SabotageManager = GetNode<SabotageManager>("/root/SabotageManager");
        UIManager = GetNode<UIManager>("/root/UIManager");

        // Connect system signals
        ConnectSystemSignals();

        GD.Print("GameManager: All systems initialized successfully");
    }

    /// <summary>
    /// Connect signals between systems for communication
    /// </summary>
    private void ConnectSystemSignals()
    {
        GD.Print("GameManager: Connecting system signals...");

        // Connect phase change signals to all systems
        PhaseChanged += OnPhaseChanged;

        // Connect player management signals
        PlayerJoined += OnPlayerJoined;
        PlayerLeft += OnPlayerLeft;

        GD.Print("GameManager: System signals connected");
    }

    /// <summary>
    /// Initialize the local player with default stats
    /// </summary>
    private void InitializeLocalPlayer()
    {
        GD.Print("GameManager: Initializing local player...");

        // Generate unique player ID for testing multiple instances
        // In debug mode, use a random ID to simulate different players
        int playerId = 0; // Default host ID
        string playerName = "Player";

        if (OS.IsDebugBuild())
        {
            // For debug mode, we'll set the player ID after networking is established
            // For now, use temporary IDs that will be updated later
            ushort testPort = 12345;
            var udp = new UdpServer();
            bool isFirstInstance = udp.Listen(testPort) == Error.Ok;
            udp.Stop();

            if (isFirstInstance)
            {
                playerId = 0; // Host is always Player 0
                playerName = "Host_Player";
                GD.Print("GameManager: DEBUG MODE - First instance (Host) -> Player ID 0");
            }
            else
            {
                playerId = 1; // Client gets Player 1
                playerName = "Client_Player";
                GD.Print("GameManager: DEBUG MODE - Second instance (Client) -> Player ID 1");
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

        GD.Print($"GameManager: Local player initialized - {LocalPlayer.PlayerName} (ID: {LocalPlayer.PlayerId})");
        GD.Print($"GameManager: Stats - ThrowPower: {LocalPlayer.ThrowPower}, MoveSpeed: {LocalPlayer.MoveSpeed}, Composure: {LocalPlayer.Composure}");
    }

    /// <summary>
    /// Set up automatic networking for debug mode - first instance hosts, second connects
    /// </summary>
    private void SetupDebugNetworking()
    {
        GD.Print("GameManager: Setting up debug networking...");

        // Use a more reliable method to detect if we should host or connect
        // Check if there's already a host running by trying to connect to the default port
        bool hostExists = CheckIfHostExists();

        if (!hostExists)
        {
            // No host found - start hosting
            GD.Print("GameManager: No existing host found - starting as host");
            CallDeferred(MethodName.StartHosting);
        }
        else
        {
            // Host exists - connect as client
            GD.Print("GameManager: Host exists - connecting as client");
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
                GD.Print("GameManager: Host lock file exists - another instance is hosting");
                return true;
            }

            // We can create the file, so we should be the host
            file.StoreString($"Host started at {Time.GetUnixTimeFromSystem()}");
            file.Close();

            // Store path for cleanup
            hostLockFilePath = lockFilePath;

            GD.Print("GameManager: Created host lock file - this instance will host");
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
            GD.Print("GameManager: Attempting to join host as client");

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
            GD.Print($"GameManager: Switching to scene: {scenePath}");

            // Use call_deferred to ensure scene switching happens safely
            GetTree().CallDeferred("change_scene_to_file", scenePath);
        }
        else
        {
            GD.Print($"GameManager: No scene mapping for phase {newPhase}, staying in current scene");
        }

        GD.Print($"GameManager: Phase changed successfully to {newPhase}");
    }

    /// <summary>
    /// Add a player to the game
    /// </summary>
    /// <param name="playerId">Unique player ID</param>
    /// <param name="playerData">Player data including stats and profile</param>
    public void AddPlayer(int playerId, PlayerData playerData)
    {
        GD.Print($"GameManager: Adding player {playerData.PlayerName} (ID: {playerId})");

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
            GD.Print($"GameManager: Player {playerId} initialized at table");
        }

        // Only emit player joined signal for truly new players
        if (isNewPlayer)
        {
            EmitSignal(SignalName.PlayerJoined, playerId, playerData);
        }

        GD.Print($"GameManager: Player {playerData.PlayerName} added successfully. Total players: {ConnectedPlayers.Count}");
    }

    /// <summary>
    /// Remove a player from the game
    /// </summary>
    /// <param name="playerId">ID of player to remove</param>
    public void RemovePlayer(int playerId)
    {
        GD.Print($"GameManager: Removing player ID {playerId}");

        if (ConnectedPlayers.ContainsKey(playerId))
        {
            var playerName = ConnectedPlayers[playerId].PlayerName;
            ConnectedPlayers.Remove(playerId);

            // Remove player location tracking
            PlayerLocations.Remove(playerId);

            // Emit player left signal
            EmitSignal(SignalName.PlayerLeft, playerId);

            GD.Print($"GameManager: Player {playerName} removed successfully. Remaining players: {ConnectedPlayers.Count}");
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
            GD.Print($"GameManager: Player {playerId} already at {newLocation}");
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
        GD.Print($"GameManager: Player {playerId} leaving table for kitchen");
        SetPlayerLocation(playerId, PlayerLocation.InKitchen);
    }

    /// <summary>
    /// Player returns to the table from the kitchen
    /// </summary>
    /// <param name="playerId">Player ID</param>
    public void PlayerReturnToTable(int playerId)
    {
        GD.Print($"GameManager: Player {playerId} returning to table from kitchen");
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
        GD.Print("GameManager: Starting to host game...");

        // Start hosting through NetworkManager
        if (NetworkManager != null)
        {
            string roomCode = NetworkManager.StartHosting();
            if (string.IsNullOrEmpty(roomCode))
            {
                GD.PrintErr("GameManager: Failed to start hosting");
                return;
            }
            GD.Print($"GameManager: Hosting started with room code: {roomCode}");
        }

        // Transition to host lobby phase
        ChangePhase(GamePhase.HostLobby);

        // Add local player as host
        AddPlayer(LocalPlayer.PlayerId, LocalPlayer);

        GD.Print("GameManager: Host game started successfully");
    }

    /// <summary>
    /// Join a game as client
    /// </summary>
    /// <param name="roomCode">6-digit room code from PRD</param>
    public void JoinGame(string roomCode)
    {
        GD.Print($"GameManager: Attempting to join game with room code: {roomCode}");

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

        GD.Print("GameManager: Join game process initiated");
    }

    /// <summary>
    /// Start the card game phase
    /// </summary>
    public void StartCardGame()
    {
        GD.Print("GameManager: Starting card game phase...");

        // Remove the premature player count check - we'll add AI players automatically
        // The actual player count validation happens in CardManager.ExecuteGameStart()

        // Transition to card phase locally
        ChangePhase(GamePhase.CardPhase);

        // CRITICAL FIX: Actually start the CardManager game with connected players
        var playerIds = new List<int>(ConnectedPlayers.Keys);
        GD.Print($"GameManager: Starting CardManager with players: {string.Join(", ", playerIds)}");

        // Use CallDeferred to ensure CardManager is ready after scene change
        CallDeferred(MethodName.StartCardManagerGame, playerIds.ToArray());

        // Notify all clients about game start via RPC
        if (NetworkManager != null && NetworkManager.IsHost)
        {
            GD.Print("GameManager: Notifying all clients to start card game...");
            Rpc(MethodName.OnGameStarted);
        }

        GD.Print("GameManager: Card game phase started successfully");
    }

    /// <summary>
    /// Deferred method to start CardManager game after scene change
    /// </summary>
    /// <param name="playerIds">Array of player IDs</param>
    private void StartCardManagerGame(int[] playerIds)
    {
        GD.Print($"GameManager: Starting CardManager game with {playerIds.Length} human players");

        if (CardManager == null)
        {
            GD.PrintErr("GameManager: CardManager not available - cannot start game");
            return;
        }

        // Create final player list and add AI players to fill up to 4 total players
        var finalPlayerIds = new List<int>(playerIds);

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
            GD.Print($"GameManager: Created AI player {nextAiId}: {aiPlayerData.PlayerName}");

            nextAiId++;
        }

        GD.Print($"GameManager: Final player list for CardManager: {string.Join(", ", finalPlayerIds)}");

        // Now start the CardManager with the complete player list
        CardManager.StartGame(finalPlayerIds);
        GD.Print("GameManager: CardManager game started successfully with AI players");
    }

    /// <summary>
    /// RPC method to notify clients that the game has started
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void OnGameStarted()
    {
        GD.Print("GameManager: Received game start notification from host");

        // Client transitions to card phase
        ChangePhase(GamePhase.CardPhase);

        // CRITICAL FIX: Clients also need to start their CardManager game with AI players
        var playerIds = new List<int>(ConnectedPlayers.Keys);
        GD.Print($"GameManager: Client starting CardManager with players: {string.Join(", ", playerIds)}");

        // Use CallDeferred to ensure CardManager is ready after scene change
        CallDeferred(MethodName.StartCardManagerGame, playerIds.ToArray());

        GD.Print("GameManager: Client successfully transitioned to card game phase");
    }

    /// <summary>
    /// Start the real-time phase for sabotage
    /// </summary>
    public void StartRealTimePhase()
    {
        GD.Print("GameManager: Starting real-time phase...");

        // Transition to real-time phase
        ChangePhase(GamePhase.RealTimePhase);

        GD.Print("GameManager: Real-time phase started successfully");
    }

    /// <summary>
    /// Start the process of joining a game (called from main menu)
    /// </summary>
    public void StartJoining()
    {
        GD.Print("GameManager: Starting join game process...");

        // Transition to client lobby phase to show join UI
        ChangePhase(GamePhase.ClientLobby);

        GD.Print("GameManager: Join game process initiated");
    }

    /// <summary>
    /// Start a new match (called from results screen)
    /// </summary>
    public void StartNewMatch()
    {
        GD.Print("GameManager: Starting new match...");

        // Remove the player count check - we add AI players automatically
        // Let CardManager handle the validation after AI players are added

        // Transition back to card phase for new game
        ChangePhase(GamePhase.CardPhase);

        GD.Print("GameManager: New match started successfully");
    }

    /// <summary>
    /// Return to main menu (called from results screen)
    /// </summary>
    public void ReturnToMainMenu()
    {
        GD.Print("GameManager: Returning to main menu...");

        // Clear connected players except local player
        ConnectedPlayers.Clear();
        AddPlayer(LocalPlayer.PlayerId, LocalPlayer);

        // Transition back to main menu
        ChangePhase(GamePhase.MainMenu);

        GD.Print("GameManager: Returned to main menu successfully");
    }

    /// <summary>
    /// Start a single-player game with AI opponents (for testing)
    /// </summary>
    public void StartSinglePlayerGame()
    {
        GD.Print("GameManager: Starting single-player game with AI opponents...");

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

        GD.Print("GameManager: Single-player game started successfully");
    }

    /// <summary>
    /// Handle phase change events
    /// </summary>
    /// <param name="newPhase">New phase as integer</param>
    private void OnPhaseChanged(int newPhase)
    {
        GD.Print($"GameManager: Phase change event received: {(GamePhase)newPhase}");

        // Phase-specific logic can be added here
        switch ((GamePhase)newPhase)
        {
            case GamePhase.MainMenu:
                GD.Print("GameManager: Entered main menu phase");
                break;
            case GamePhase.HostLobby:
                GD.Print("GameManager: Entered host lobby phase");
                break;
            case GamePhase.ClientLobby:
                GD.Print("GameManager: Entered client lobby phase");
                break;
            case GamePhase.CardPhase:
                GD.Print("GameManager: Entered card phase");
                break;
            case GamePhase.RealTimePhase:
                GD.Print("GameManager: Entered real-time phase");
                break;
            case GamePhase.Results:
                GD.Print("GameManager: Entered results phase");
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
        GD.Print($"GameManager: Player joined event - {playerData.PlayerName} (ID: {playerId})");

        // Update UI, notify other systems, etc.
    }

    /// <summary>
    /// Handle player left events
    /// </summary>
    /// <param name="playerId">Player ID that left</param>
    private void OnPlayerLeft(int playerId)
    {
        GD.Print($"GameManager: Player left event - ID: {playerId}");

        // Handle player disconnection logic
        if (ConnectedPlayers.Count == 0)
        {
            GD.Print("GameManager: All players disconnected, returning to main menu");
            ChangePhase(GamePhase.MainMenu);
        }
    }

    public override void _ExitTree()
    {
        GD.Print("GameManager: Shutting down...");

        // Clean up host lock file if we created it
        if (!string.IsNullOrEmpty(hostLockFilePath) && FileAccess.FileExists(hostLockFilePath))
        {
            DirAccess.RemoveAbsolute(hostLockFilePath);
            GD.Print("GameManager: Cleaned up host lock file");
        }

        // Clear singleton
        if (Instance == this)
        {
            Instance = null;
        }

        GD.Print("GameManager: Shutdown complete");
    }
}