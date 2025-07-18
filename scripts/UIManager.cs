using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages all UI systems including menus, HUD, chat, and sabotage overlays
/// Handles UI state transitions and player interface interactions
/// PRD F-4: Chat-Window Intimidation system with dynamic chat panel sizing
/// </summary>
public partial class UIManager : Node
{
    // UI state
    public enum UIState
    {
        MainMenu,
        HostLobby,
        ClientLobby,
        GameHUD,
        Results
    }

    [Export]
    public UIState CurrentUIState { get; private set; } = UIState.MainMenu;

    // Chat system (intimidation feature from PRD)
    [Export]
    public float ChatGrowthMultiplier = 1.5f; // How much chat grows when losing

    [Export]
    public float ChatShrinkDelay = 30.0f; // 30s delay before chat shrinks

    [Export]
    public Vector2 ChatBasePanelSize = new(300, 150); // Base chat panel size

    [Export]
    public Vector2 ChatMaxPanelSize = new(500, 300); // Maximum chat panel size

    [Export]
    public float ChatAnimationDuration = 0.5f; // Animation duration for resizing

    // Chat intimidation tracking
    private Dictionary<int, ChatIntimidationData> playerChatStates = new();
    private Dictionary<int, Godot.Timer> chatShrinkTimers = new();

    // UI node references
    private Control mainMenuPanel;
    private Control hostLobbyPanel;
    private Control clientLobbyPanel;
    private Control gameHUDPanel;
    private Control resultsPanel;

    // Chat panel references for each player
    private Dictionary<int, Control> playerChatPanels = new();

    // Events
    [Signal]
    public delegate void UIStateChangedEventHandler(UIState newState);

    [Signal]
    public delegate void HostGameRequestedEventHandler();

    [Signal]
    public delegate void JoinGameRequestedEventHandler(string roomCode);

    [Signal]
    public delegate void ChatIntimidationAppliedEventHandler(int playerId, float newSize);

    [Signal]
    public delegate void ChatIntimidationRemovedEventHandler(int playerId);

    [Signal]
    public delegate void ChatMessageSentEventHandler(int playerId, string message);

    public override void _Ready()
    {
        GD.Print("UIManager: Initializing UI system...");

        // Initialize UI panels
        InitializeUIPanels();

        // Initialize chat intimidation system
        InitializeChatIntimidation();

        // Connect to GameManager events
        ConnectGameManagerEvents();

        GD.Print("UIManager: UI system initialized successfully");
    }

    /// <summary>
    /// Initialize UI panels
    /// </summary>
    private void InitializeUIPanels()
    {
        GD.Print("UIManager: Initializing UI panels...");

        // TODO: In a real implementation, these would be loaded from scene files
        // For now, create placeholder panels

        // Main menu panel
        mainMenuPanel = new Control();
        mainMenuPanel.Name = "MainMenuPanel";
        mainMenuPanel.Visible = true;
        AddChild(mainMenuPanel);

        // Host lobby panel
        hostLobbyPanel = new Control();
        hostLobbyPanel.Name = "HostLobbyPanel";
        hostLobbyPanel.Visible = false;
        AddChild(hostLobbyPanel);

        // Client lobby panel
        clientLobbyPanel = new Control();
        clientLobbyPanel.Name = "ClientLobbyPanel";
        clientLobbyPanel.Visible = false;
        AddChild(clientLobbyPanel);

        // Game HUD panel
        gameHUDPanel = new Control();
        gameHUDPanel.Name = "GameHUDPanel";
        gameHUDPanel.Visible = false;
        AddChild(gameHUDPanel);

        // Results panel
        resultsPanel = new Control();
        resultsPanel.Name = "ResultsPanel";
        resultsPanel.Visible = false;
        AddChild(resultsPanel);

        GD.Print("UIManager: UI panels initialized");
    }

    /// <summary>
    /// Initialize chat intimidation system
    /// </summary>
    private void InitializeChatIntimidation()
    {
        GD.Print("UIManager: Initializing chat intimidation system...");

        // Clear existing data
        playerChatStates.Clear();
        chatShrinkTimers.Clear();
        playerChatPanels.Clear();

        // Initialize for connected players
        if (GameManager.Instance != null)
        {
            foreach (var playerId in GameManager.Instance.ConnectedPlayers.Keys)
            {
                InitializePlayerChatPanel(playerId);
            }
        }

        GD.Print("UIManager: Chat intimidation system initialized");
    }

    /// <summary>
    /// Initialize chat panel for a specific player
    /// </summary>
    /// <param name="playerId">Player ID</param>
    private void InitializePlayerChatPanel(int playerId)
    {
        GD.Print($"UIManager: Initializing chat panel for player {playerId}");

        // Create chat panel
        var chatPanel = new Panel();
        chatPanel.Name = $"ChatPanel_{playerId}";
        chatPanel.Size = ChatBasePanelSize;
        chatPanel.Position = new Vector2(10, 10 + playerId * 160); // Stack panels vertically

        // TODO: Add chat input field, message history, etc.

        // Add to game HUD
        if (gameHUDPanel != null)
        {
            gameHUDPanel.AddChild(chatPanel);
        }

        // Track panel and state
        playerChatPanels[playerId] = chatPanel;
        playerChatStates[playerId] = new ChatIntimidationData
        {
            PlayerId = playerId,
            CurrentSize = ChatBasePanelSize,
            BaseSize = ChatBasePanelSize,
            IsIntimidated = false,
            IntimidationLevel = 0,
            LastLossTime = DateTime.MinValue
        };

        GD.Print($"UIManager: Chat panel created for player {playerId}");
    }

    /// <summary>
    /// Connect to GameManager events
    /// </summary>
    private void ConnectGameManagerEvents()
    {
        GD.Print("UIManager: Connecting to GameManager events...");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.PhaseChanged += OnGamePhaseChanged;
            GameManager.Instance.PlayerJoined += OnPlayerJoined;
            GameManager.Instance.PlayerLeft += OnPlayerLeft;
        }

        GD.Print("UIManager: GameManager events connected");
    }

    /// <summary>
    /// Change the current UI state
    /// </summary>
    /// <param name="newState">New UI state to transition to</param>
    public void ChangeUIState(UIState newState)
    {
        GD.Print($"UIManager: UI state transition: {CurrentUIState} -> {newState}");

        // Hide current UI
        HideAllPanels();

        // Show new UI
        ShowPanelForState(newState);

        CurrentUIState = newState;

        EmitSignal(SignalName.UIStateChanged, (int)newState);

        GD.Print($"UIManager: UI state changed to {newState}");
    }

    /// <summary>
    /// Hide all UI panels
    /// </summary>
    private void HideAllPanels()
    {
        mainMenuPanel.Visible = false;
        hostLobbyPanel.Visible = false;
        clientLobbyPanel.Visible = false;
        gameHUDPanel.Visible = false;
        resultsPanel.Visible = false;
    }

    /// <summary>
    /// Show panel for specific UI state
    /// </summary>
    /// <param name="state">UI state</param>
    private void ShowPanelForState(UIState state)
    {
        switch (state)
        {
            case UIState.MainMenu:
                mainMenuPanel.Visible = true;
                break;
            case UIState.HostLobby:
                hostLobbyPanel.Visible = true;
                break;
            case UIState.ClientLobby:
                clientLobbyPanel.Visible = true;
                break;
            case UIState.GameHUD:
                gameHUDPanel.Visible = true;
                break;
            case UIState.Results:
                resultsPanel.Visible = true;
                break;
        }
    }

    /// <summary>
    /// Show host lobby UI
    /// </summary>
    /// <param name="roomCode">Room code to display</param>
    public void ShowHostLobby(string roomCode)
    {
        GD.Print($"UIManager: Showing host lobby with room code: {roomCode}");

        ChangeUIState(UIState.HostLobby);

        // TODO: Update room code display
        // TODO: Update player list
        // TODO: Show start game button

        GD.Print("UIManager: Host lobby displayed");
    }

    /// <summary>
    /// Show client lobby UI
    /// </summary>
    public void ShowClientLobby()
    {
        GD.Print("UIManager: Showing client lobby");

        ChangeUIState(UIState.ClientLobby);

        // TODO: Show waiting message
        // TODO: Show player list
        // TODO: Show ready status

        GD.Print("UIManager: Client lobby displayed");
    }

    /// <summary>
    /// Show main game HUD
    /// </summary>
    public void ShowGameHUD()
    {
        GD.Print("UIManager: Showing game HUD");

        ChangeUIState(UIState.GameHUD);

        // TODO: Show card hand
        // TODO: Show turn timer
        // TODO: Show player stats

        GD.Print("UIManager: Game HUD displayed");
    }

    /// <summary>
    /// Apply chat intimidation effect when player loses
    /// </summary>
    /// <param name="playerId">Player who lost the round</param>
    public void ApplyChatIntimidation(int playerId)
    {
        GD.Print($"UIManager: Applying chat intimidation to player {playerId}");

        if (!playerChatStates.ContainsKey(playerId))
        {
            GD.PrintErr($"UIManager: No chat state found for player {playerId}");
            return;
        }

        var chatState = playerChatStates[playerId];

        // Increase intimidation level
        chatState.IntimidationLevel++;
        chatState.LastLossTime = DateTime.Now;

        // Calculate new size based on intimidation level
        Vector2 newSize = CalculateChatSize(chatState.BaseSize, chatState.IntimidationLevel);

        // Apply intimidation
        ApplyChatResize(playerId, newSize);

        // Cancel existing shrink timer
        if (chatShrinkTimers.ContainsKey(playerId))
        {
            chatShrinkTimers[playerId].Stop();
            chatShrinkTimers[playerId].QueueFree();
            chatShrinkTimers.Remove(playerId);
        }

        // Start new shrink timer (30s delay from PRD)
        StartChatShrinkTimer(playerId);

        GD.Print($"UIManager: Chat intimidation applied to player {playerId} (level {chatState.IntimidationLevel})");
    }

    /// <summary>
    /// Remove chat intimidation effect when player wins
    /// </summary>
    /// <param name="playerId">Player who won the round</param>
    public void RemoveChatIntimidation(int playerId)
    {
        GD.Print($"UIManager: Removing chat intimidation from player {playerId}");

        if (!playerChatStates.ContainsKey(playerId))
        {
            GD.PrintErr($"UIManager: No chat state found for player {playerId}");
            return;
        }

        var chatState = playerChatStates[playerId];

        // Reset intimidation
        chatState.IntimidationLevel = 0;
        chatState.IsIntimidated = false;

        // Restore original size
        ApplyChatResize(playerId, chatState.BaseSize);

        // Cancel shrink timer
        if (chatShrinkTimers.ContainsKey(playerId))
        {
            chatShrinkTimers[playerId].Stop();
            chatShrinkTimers[playerId].QueueFree();
            chatShrinkTimers.Remove(playerId);
        }

        EmitSignal(SignalName.ChatIntimidationRemoved, playerId);

        GD.Print($"UIManager: Chat intimidation removed from player {playerId}");
    }

    /// <summary>
    /// Calculate chat size based on intimidation level
    /// </summary>
    /// <param name="baseSize">Base chat size</param>
    /// <param name="intimidationLevel">Current intimidation level</param>
    /// <returns>New chat size</returns>
    private Vector2 CalculateChatSize(Vector2 baseSize, int intimidationLevel)
    {
        // Apply growth multiplier for each intimidation level
        float growthFactor = 1.0f + (ChatGrowthMultiplier - 1.0f) * intimidationLevel;

        Vector2 newSize = baseSize * growthFactor;

        // Clamp to maximum size
        newSize.X = Mathf.Min(newSize.X, ChatMaxPanelSize.X);
        newSize.Y = Mathf.Min(newSize.Y, ChatMaxPanelSize.Y);

        GD.Print($"UIManager: Calculated chat size: {newSize} (level {intimidationLevel}, factor {growthFactor})");

        return newSize;
    }

    /// <summary>
    /// Apply chat panel resize with animation
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <param name="newSize">New panel size</param>
    private void ApplyChatResize(int playerId, Vector2 newSize)
    {
        GD.Print($"UIManager: Resizing chat panel for player {playerId} to {newSize}");

        if (!playerChatPanels.ContainsKey(playerId))
        {
            GD.PrintErr($"UIManager: No chat panel found for player {playerId}");
            return;
        }

        var chatPanel = playerChatPanels[playerId];
        var chatState = playerChatStates[playerId];

        // Update state
        chatState.CurrentSize = newSize;
        chatState.IsIntimidated = (newSize != chatState.BaseSize);

        // Create tween for smooth resizing
        var tween = CreateTween();
        tween.TweenProperty(chatPanel, "size", newSize, ChatAnimationDuration);

        // Visual feedback
        if (chatState.IsIntimidated)
        {
            // Add visual intimidation effects (red border, pulsing, etc.)
            // TODO: Implement visual intimidation effects
            GD.Print($"UIManager: Chat panel intimidation effect applied");
        }
        else
        {
            // Remove visual intimidation effects
            // TODO: Remove visual intimidation effects
            GD.Print($"UIManager: Chat panel intimidation effect removed");
        }

        EmitSignal(SignalName.ChatIntimidationApplied, playerId, newSize.Length());
    }

    /// <summary>
    /// Start timer for chat shrink delay
    /// </summary>
    /// <param name="playerId">Player ID</param>
    private void StartChatShrinkTimer(int playerId)
    {
        GD.Print($"UIManager: Starting chat shrink timer for player {playerId} ({ChatShrinkDelay}s)");

        var timer = new Godot.Timer();
        timer.WaitTime = ChatShrinkDelay;
        timer.OneShot = true;
        AddChild(timer);

        timer.Timeout += () =>
        {
            OnChatShrinkTimerExpired(playerId);
            timer.QueueFree();
        };

        chatShrinkTimers[playerId] = timer;
        timer.Start();
    }

    /// <summary>
    /// Handle chat shrink timer expiration (30s AFK)
    /// </summary>
    /// <param name="playerId">Player ID</param>
    private void OnChatShrinkTimerExpired(int playerId)
    {
        GD.Print($"UIManager: Chat shrink timer expired for player {playerId}");

        // Remove intimidation after 30s delay
        RemoveChatIntimidation(playerId);

        // Remove from timer tracking
        if (chatShrinkTimers.ContainsKey(playerId))
        {
            chatShrinkTimers.Remove(playerId);
        }
    }

    /// <summary>
    /// Send chat message
    /// </summary>
    /// <param name="playerId">Player sending message</param>
    /// <param name="message">Message content</param>
    public void SendChatMessage(int playerId, string message)
    {
        GD.Print($"UIManager: Player {playerId} sending chat message: {message}");

        // TODO: Add message to chat history
        // TODO: Update UI with new message

        EmitSignal(SignalName.ChatMessageSent, playerId, message);

        // Reset shrink timer on activity
        if (chatShrinkTimers.ContainsKey(playerId))
        {
            chatShrinkTimers[playerId].Stop();
            chatShrinkTimers[playerId].QueueFree();
            chatShrinkTimers.Remove(playerId);

            // Start new timer
            StartChatShrinkTimer(playerId);
        }
    }

    /// <summary>
    /// Get chat intimidation status for player
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <returns>Chat intimidation data</returns>
    public ChatIntimidationData GetChatIntimidationStatus(int playerId)
    {
        return playerChatStates.GetValueOrDefault(playerId);
    }

    /// <summary>
    /// Show sabotage overlay effect
    /// </summary>
    /// <param name="playerId">Player receiving the effect</param>
    /// <param name="sabotageType">Type of sabotage</param>
    /// <param name="intensity">Effect intensity</param>
    public void ShowSabotageOverlay(int playerId, SabotageType sabotageType, float intensity)
    {
        GD.Print($"UIManager: Showing {sabotageType} overlay for player {playerId} with intensity {intensity}");

        // TODO: Apply appropriate overlay effect
        // TODO: Set intensity based on player stats

        GD.Print("UIManager: Sabotage overlay applied");
    }

    /// <summary>
    /// Show results screen
    /// </summary>
    /// <param name="winnerId">Winner player ID</param>
    /// <param name="finalScores">Final scores for all players</param>
    public void ShowResults(int winnerId, Dictionary<int, int> finalScores)
    {
        GD.Print($"UIManager: Showing results - Winner: {winnerId}");

        ChangeUIState(UIState.Results);

        // TODO: Display winner
        // TODO: Display final scores
        // TODO: Display XP gained
        // TODO: Display stat increases

        GD.Print("UIManager: Results screen displayed");
    }

    // Event handlers

    /// <summary>
    /// Handle game phase change
    /// </summary>
    /// <param name="newPhase">New game phase as integer</param>
    private void OnGamePhaseChanged(int newPhase)
    {
        var phase = (GameManager.GamePhase)newPhase;
        GD.Print($"UIManager: Game phase changed to {phase}");

        switch (phase)
        {
            case GameManager.GamePhase.MainMenu:
                ChangeUIState(UIState.MainMenu);
                break;
            case GameManager.GamePhase.HostLobby:
                ChangeUIState(UIState.HostLobby);
                break;
            case GameManager.GamePhase.ClientLobby:
                ChangeUIState(UIState.ClientLobby);
                break;
            case GameManager.GamePhase.CardPhase:
            case GameManager.GamePhase.RealTimePhase:
                ChangeUIState(UIState.GameHUD);
                break;
            case GameManager.GamePhase.Results:
                ChangeUIState(UIState.Results);
                break;
        }
    }

    /// <summary>
    /// Handle player joined
    /// </summary>
    /// <param name="playerId">Player ID that joined</param>
    /// <param name="playerData">Player data</param>
    private void OnPlayerJoined(int playerId, PlayerData playerData)
    {
        GD.Print($"UIManager: Player {playerData.PlayerName} joined (ID: {playerId})");

        // Initialize chat panel for new player
        InitializePlayerChatPanel(playerId);

        // TODO: Update lobby UI with new player
        // TODO: Update game UI with new player
    }

    /// <summary>
    /// Handle player left
    /// </summary>
    /// <param name="playerId">Player ID that left</param>
    private void OnPlayerLeft(int playerId)
    {
        GD.Print($"UIManager: Player {playerId} left");

        // Clean up chat panel
        if (playerChatPanels.ContainsKey(playerId))
        {
            playerChatPanels[playerId].QueueFree();
            playerChatPanels.Remove(playerId);
        }

        // Clean up chat state
        if (playerChatStates.ContainsKey(playerId))
        {
            playerChatStates.Remove(playerId);
        }

        // Clean up timer
        if (chatShrinkTimers.ContainsKey(playerId))
        {
            chatShrinkTimers[playerId].Stop();
            chatShrinkTimers[playerId].QueueFree();
            chatShrinkTimers.Remove(playerId);
        }

        // TODO: Update lobby UI
        // TODO: Update game UI
    }

    public override void _ExitTree()
    {
        GD.Print("UIManager: UI system shutting down");

        // Clean up chat timers
        foreach (var timer in chatShrinkTimers.Values)
        {
            timer.Stop();
            timer.QueueFree();
        }
        chatShrinkTimers.Clear();

        // Clean up state
        playerChatStates.Clear();
        playerChatPanels.Clear();

        // Disconnect from GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PhaseChanged -= OnGamePhaseChanged;
            GameManager.Instance.PlayerJoined -= OnPlayerJoined;
            GameManager.Instance.PlayerLeft -= OnPlayerLeft;
        }

        GD.Print("UIManager: UI system shutdown complete");
    }
}

/// <summary>
/// Chat intimidation data for a player
/// </summary>
public class ChatIntimidationData
{
    public int PlayerId { get; set; }
    public Vector2 CurrentSize { get; set; }
    public Vector2 BaseSize { get; set; }
    public bool IsIntimidated { get; set; }
    public int IntimidationLevel { get; set; }
    public DateTime LastLossTime { get; set; }

    public override string ToString()
    {
        return $"Player {PlayerId}: Size {CurrentSize}, Level {IntimidationLevel}, Intimidated: {IsIntimidated}";
    }
}