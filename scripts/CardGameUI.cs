using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CardGameUI : Control
{
    // UI views for concurrent gameplay
    private Control cardTableView;
    private Control kitchenView;

    // Card table UI elements (now using scene nodes)
    private Label timerLabel;
    private Label scoreLabel;
    private VBoxContainer playerHand;
    private Control trickArea;
    private Panel chatPanel;
    private Label chatLabel;
    private LineEdit chatInput;
    private Button leaveTableButton;

    // Kitchen UI elements
    private CharacterBody2D player;
    private Label inventoryLabel;

    // Reference to game systems
    private CardManager cardManager;
    private GameManager gameManager;

    // Visual overlay system for sabotage effects
    private Control overlayLayer;
    private Dictionary<int, List<Control>> playerVisualOverlays = new();

    // Current player's cards
    private List<Card> currentPlayerCards = new();
    private List<TextureButton> cardButtons = new(); // CHANGED: Now using TextureButton for images

    // UI for showing other players' info (now using scene node)
    private VBoxContainer playersInfoContainer;
    private List<Label> playerInfoLabels = new();

    // Debug button for testing
    private Button debugEggButton;

    // Chat message history
    private List<string> chatHistory = new();
    private const int MaxChatHistoryLines = 5;

    // Card graphics system - NEW
    private Dictionary<string, Texture2D> cardTextures = new();
    private Texture2D cardBackTexture;
    private readonly Vector2 cardSize = new Vector2(100, 140); // PERFECT SIZE: Both hand and trick cards use same readable size
    private readonly Vector2 trickCardSize = new Vector2(100, 140); // TRICK CARDS: Same size as hand cards for consistency
    private readonly string cardAssetsPath = "res://assets/cards/faces/";
    private readonly string cardBackPath = "res://assets/cards/backs/card_back_blue.png";

    // Track last trick state to avoid unnecessary updates
    private int lastTrickCount = -1;
    private List<CardPlay> lastTrickState = new();

    public override void _Ready()
    {
        GD.Print("CardGame UI loaded");

        // Load card graphics first
        LoadCardGraphics();

        // Get references to UI views
        cardTableView = GetNode<Control>("CardTableView");
        kitchenView = GetNode<Control>("KitchenView");

        // Get references to card table UI elements
        timerLabel = GetNode<Label>("CardTableView/GameArea/ScoreTimerPanel/TimerLabel");
        scoreLabel = GetNode<Label>("CardTableView/GameArea/ScoreTimerPanel/ScoreLabel");
        playerHand = GetNode<VBoxContainer>("CardTableView/GameArea/PlayerHand");
        trickArea = GetNode<Control>("CardTableView/GameArea/CenterArea/TrickArea");

        // Configure trick area to not clip contents (allow cards to extend beyond bounds)
        if (trickArea != null)
        {
            trickArea.ClipContents = false;
            // Trick area configured successfully
        }
        chatPanel = GetNode<Panel>("ChatPanel");
        chatLabel = GetNode<Label>("ChatPanel/ChatVBox/ChatLabel");
        chatInput = GetNode<LineEdit>("ChatPanel/ChatVBox/ChatInput");
        leaveTableButton = GetNode<Button>("CardTableView/GameArea/LeaveTableButton");

        // Get references to kitchen UI elements
        player = GetNode<CharacterBody2D>("KitchenView/Player");
        inventoryLabel = GetNode<Label>("KitchenView/KitchenUI/InventoryLabel");

        // Get players info panel
        playersInfoContainer = GetNode<VBoxContainer>("PlayersInfoPanel");

        // Get debug button
        debugEggButton = GetNode<Button>("PlayersInfoPanel/DebugEggButton");

        // DEBUG: Verify chat panel properties on startup
        if (chatPanel != null)
        {
            // Chat panel found and configured

            // Style chat panel to be semi-transparent and visually "in front" but clickable behind
            var chatStyleBox = new StyleBoxFlat();
            chatStyleBox.BgColor = new Color(0.1f, 0.1f, 0.1f, 0.8f); // Dark with some transparency
            chatStyleBox.BorderColor = new Color(1.0f, 1.0f, 1.0f, 0.5f); // Semi-transparent white border
            chatStyleBox.BorderWidthLeft = 2;
            chatStyleBox.BorderWidthTop = 2;
            chatStyleBox.BorderWidthRight = 2;
            chatStyleBox.BorderWidthBottom = 2;
            chatStyleBox.CornerRadiusTopLeft = 5;
            chatStyleBox.CornerRadiusTopRight = 5;
            chatStyleBox.CornerRadiusBottomLeft = 5;
            chatStyleBox.CornerRadiusBottomRight = 5;
            chatPanel.AddThemeStyleboxOverride("panel", chatStyleBox);

            // Chat panel styled successfully
        }
        else
        {
            GD.PrintErr("DEBUG: Chat panel is NULL!");
        }

        // Create overlay layer for visual sabotage effects
        overlayLayer = new Control();
        overlayLayer.Name = "SabotageOverlayLayer";
        overlayLayer.MouseFilter = Control.MouseFilterEnum.Ignore; // Don't block mouse input
        // Set anchors to fill entire screen manually
        overlayLayer.AnchorLeft = 0.0f;
        overlayLayer.AnchorTop = 0.0f;
        overlayLayer.AnchorRight = 1.0f;
        overlayLayer.AnchorBottom = 1.0f;
        overlayLayer.OffsetLeft = 0.0f;
        overlayLayer.OffsetTop = 0.0f;
        overlayLayer.OffsetRight = 0.0f;
        overlayLayer.OffsetBottom = 0.0f;
        AddChild(overlayLayer);

        // Sabotage overlay layer created successfully

        // Initialize chat
        UpdateChatDisplay();

        // Connect to game systems
        ConnectToGameSystems();

        // Start at the table
        ShowCardTableView();

        GD.Print("CardGameUI: UI elements connected successfully");
        GD.Print($"CardGameUI: Debug button available: {debugEggButton != null}");
        GD.Print($"CardGameUI: Chat panel available: {chatPanel != null}");
        GD.Print($"CardGameUI: GameManager available: {gameManager != null}");
        GD.Print($"CardGameUI: SabotageManager available: {gameManager?.SabotageManager != null}");
    }

    /// <summary>
    /// Update UI every frame - especially timer and game state
    /// </summary>
    public override void _Process(double delta)
    {
        UpdateTimerDisplay();
        UpdateGameStateDisplay();
        UpdateTrickDisplayIfChanged(); // FIXED: Only update when trick changes
        UpdatePlayersInfo();
        UpdateLocationControls(); // Update location controls based on current player location
    }

    /// <summary>
    /// Update the trick area only when the trick state actually changes
    /// </summary>
    private void UpdateTrickDisplayIfChanged()
    {
        if (cardManager == null || !cardManager.GameInProgress)
        {
            // Clear trick display if game not in progress
            if (lastTrickCount != 0)
            {
                ClearTrickDisplay();
                lastTrickCount = 0;
                lastTrickState.Clear();
            }
            return;
        }

        var gameState = cardManager.GetGameState();

        // Only update if trick count or composition changed
        bool trickChanged = false;

        if (gameState.CurrentTrick.Count != lastTrickCount)
        {
            trickChanged = true;
        }
        else if (gameState.CurrentTrick.Count > 0)
        {
            // Check if any cards in the trick changed
            for (int i = 0; i < gameState.CurrentTrick.Count; i++)
            {
                if (i >= lastTrickState.Count ||
                    !gameState.CurrentTrick[i].Card.Equals(lastTrickState[i].Card) ||
                    gameState.CurrentTrick[i].PlayerId != lastTrickState[i].PlayerId)
                {
                    trickChanged = true;
                    break;
                }
            }
        }

        if (trickChanged)
        {
            UpdateTrickDisplay();
            lastTrickCount = gameState.CurrentTrick.Count;
            lastTrickState = new List<CardPlay>(gameState.CurrentTrick);
        }
    }

    /// <summary>
    /// Update the trick area to show cards played in current trick with actual card graphics
    /// </summary>
    private void UpdateTrickDisplay()
    {
        if (cardManager == null || !cardManager.GameInProgress) return;

        var gameState = cardManager.GetGameState();

        // Clear existing trick cards using the dedicated method
        ClearTrickDisplay();

        // Add actual card graphics for current trick
        if (gameState.CurrentTrick.Count > 0)
        {
            // Calculate positioning for trick cards in center area
            // TrickArea is 200x100, cards are 100x140, so we'll arrange them clearly separated
            float cardWidth = trickCardSize.X;
            float cardHeight = trickCardSize.Y;
            float overlapSpacing = 20; // TRICK CARDS: No overlap - clear separation between cards (20px gap)

            // Calculate total width needed and center position
            float totalWidthNeeded = (gameState.CurrentTrick.Count - 1) * overlapSpacing + cardWidth;
            Vector2 trickAreaSize = trickArea.Size; // Should be 200x100
            float startX = (trickAreaSize.X - totalWidthNeeded) / 2; // Center horizontally
            float cardY = (trickAreaSize.Y - cardHeight) / 2; // Center vertically (cards may extend above/below)

            for (int i = 0; i < gameState.CurrentTrick.Count; i++)
            {
                var cardPlay = gameState.CurrentTrick[i];
                var cardButton = CreateTrickCardButton(cardPlay.Card, cardPlay.PlayerId);
                cardButton.Name = $"TrickCard_{i}";

                // Position cards in a compact fan arrangement
                Vector2 cardPos = new Vector2(startX + i * overlapSpacing, cardY);
                cardButton.Position = cardPos;

                trickArea.AddChild(cardButton);

                // Force size after adding to scene tree (same as hand cards)
                cardButton.Size = trickCardSize;
                cardButton.CustomMinimumSize = trickCardSize;
                cardButton.CallDeferred(Control.MethodName.SetSize, trickCardSize);
            }

            // Only log when trick actually updates, not every frame
            GD.Print($"CardGameUI: Updated trick display with {gameState.CurrentTrick.Count} cards");
        }
    }

    /// <summary>
    /// Clear the trick display
    /// </summary>
    private void ClearTrickDisplay()
    {
        foreach (Node child in trickArea.GetChildren())
        {
            if (child.Name.ToString().StartsWith("TrickCard"))
            {
                child.QueueFree();
            }
        }
    }

    /// <summary>
    /// Update the players info panel to show each player's status
    /// </summary>
    private void UpdatePlayersInfo()
    {
        if (cardManager == null || gameManager == null) return;

        // CRITICAL FIX: Use PlayerOrder for consistent display, not ConnectedPlayers dictionary order
        var playerOrder = cardManager.PlayerOrder;
        if (playerOrder == null || playerOrder.Count == 0)
        {
            // If no PlayerOrder yet, fall back to sorted ConnectedPlayers for consistency
            var sortedPlayerIds = gameManager.ConnectedPlayers.Keys.ToList();
            sortedPlayerIds.Sort();
            playerOrder = sortedPlayerIds;
        }

        var gameState = cardManager.GetGameState();

        // Ensure we have enough labels for all players
        while (playerInfoLabels.Count < playerOrder.Count)
        {
            var label = new Label();
            label.Name = $"PlayerInfo_{playerInfoLabels.Count}";
            playersInfoContainer.AddChild(label);
            playerInfoLabels.Add(label);
        }

        // Hide excess labels if we have fewer players now
        for (int i = playerOrder.Count; i < playerInfoLabels.Count; i++)
        {
            playerInfoLabels[i].Visible = false;
        }

        // Update each player's info in PlayerOrder sequence
        for (int i = 0; i < playerOrder.Count; i++)
        {
            int playerId = playerOrder[i];
            var label = playerInfoLabels[i];
            label.Visible = true;

            if (!gameManager.ConnectedPlayers.ContainsKey(playerId))
            {
                label.Text = $"Player {playerId}: [DATA MISSING]";
                label.Modulate = Colors.Red;
                continue;
            }

            var playerData = gameManager.ConnectedPlayers[playerId];
            string playerName = playerData.PlayerName;

            // Get card count for this player
            int cardCount = 0;
            if (cardManager.GameInProgress)
            {
                var playerHand = cardManager.GetPlayerHand(playerId);
                cardCount = playerHand?.Count ?? 0;
            }

            // Get score for this player
            int score = 0;
            if (gameState.PlayerScores.ContainsKey(playerId))
            {
                score = gameState.PlayerScores[playerId];
            }

            // Check if it's this player's turn
            bool isCurrentTurn = cardManager.GameInProgress &&
                               gameState.CurrentPlayerTurn == playerId;

            // Format the display text
            string displayText = $"{playerName}: {cardCount} cards, {score} pts";
            if (isCurrentTurn)
            {
                displayText += " [TURN]";
                label.Modulate = Colors.Yellow; // Highlight current player
            }
            else
            {
                label.Modulate = Colors.White;
            }

            label.Text = displayText;
        }
    }

    /// <summary>
    /// Connect to game systems and their events
    /// </summary>
    private void ConnectToGameSystems()
    {
        GD.Print("CardGameUI: Connecting to game systems...");

        // Get references to game systems
        gameManager = GameManager.Instance;
        cardManager = gameManager?.CardManager; // CardManager is accessed through GameManager

        if (gameManager == null)
        {
            GD.PrintErr("CardGameUI: GameManager instance not found!");
            return;
        }

        if (cardManager == null)
        {
            GD.PrintErr("CardGameUI: CardManager instance not found!");
            return;
        }

        GD.Print($"CardGameUI: Found CardManager, GameInProgress: {cardManager.GameInProgress}");
        GD.Print($"CardGameUI: Connected players count: {gameManager.ConnectedPlayers.Count}");

        // Connect new location events
        gameManager.PlayerLocationChanged += OnPlayerLocationChanged;
        gameManager.PlayerLeftTable += OnPlayerLeftTable;
        gameManager.PlayerReturnedToTable += OnPlayerReturnedToTable;

        // Connect CardManager events
        cardManager.TurnStarted += OnTurnStarted;
        cardManager.TurnEnded += OnTurnEnded;
        cardManager.CardPlayed += OnCardPlayed;
        cardManager.TrickCompleted += OnTrickCompleted;
        cardManager.HandCompleted += OnHandCompleted;
        cardManager.HandDealt += OnHandDealt;
        cardManager.TurnTimerUpdated += OnTurnTimerUpdated;

        // Connect NetworkManager events if available
        if (gameManager.NetworkManager != null)
        {
            gameManager.NetworkManager.PlayerConnected += OnPlayerConnected;
            gameManager.NetworkManager.ClientConnectedToServer += OnClientConnectedToServer;
            // TODO: Add ChatMessageReceived event to NetworkManager for multiplayer chat
        }

        // Connect UIManager for chat intimidation if available
        if (gameManager.UIManager != null)
        {
            gameManager.UIManager.ChatIntimidationApplied += OnChatIntimidationApplied;
            gameManager.UIManager.ChatIntimidationRemoved += OnChatIntimidationRemoved;
        }

        // Connect SabotageManager for visual effects if available
        if (gameManager.SabotageManager != null)
        {
            gameManager.SabotageManager.EggThrown += OnEggThrown;
            gameManager.SabotageManager.StinkBombExploded += OnStinkBombExploded;

            // Connect sabotage cleaning events - FIXED: These signatures are correct
            gameManager.SabotageManager.SabotageCleaned += OnSabotageCleaned;

            // Connected to SabotageManager events successfully
        }
        else
        {
            // SabotageManager not available - visual effects disabled
        }

        // Initialize UI state
        UpdatePlayerHand();
        UpdateLocationControls();

        GD.Print("CardGameUI: Game systems connected successfully");
    }

    /// <summary>
    /// Handle leave table button pressed (now connected via scene)
    /// </summary>
    private void _on_leave_table_button_pressed()
    {
        GD.Print("Leave Table button pressed");
        if (gameManager?.LocalPlayer != null)
        {
            gameManager.PlayerLeaveTable(gameManager.LocalPlayer.PlayerId);
        }
    }



    /// <summary>
    /// Handle chat input text submitted
    /// </summary>
    private void _on_chat_input_text_submitted(string text)
    {
        if (string.IsNullOrEmpty(text.Trim())) return;

        GD.Print($"Chat message submitted: {text}");

        // Send chat message through network if connected
        if (gameManager?.NetworkManager != null && gameManager.NetworkManager.IsConnected)
        {
            gameManager.NetworkManager.SendChatMessage(text);
        }

        // Display message locally
        DisplayChatMessage(gameManager?.LocalPlayer?.PlayerName ?? "You", text);

        // Clear input
        chatInput.Text = "";
    }

    /// <summary>
    /// Handle debug egg button pressed - for testing egg throwing effects
    /// </summary>
    private void _on_debug_egg_button_pressed()
    {
        GD.Print("DEBUG: Egg button pressed - testing egg throwing effect");

        if (gameManager?.LocalPlayer == null)
        {
            GD.PrintErr("DEBUG: Cannot test egg effect - no local player");
            return;
        }

        if (gameManager.SabotageManager == null)
        {
            GD.PrintErr("DEBUG: Cannot test egg effect - no SabotageManager");
            return;
        }

        int localPlayerId = gameManager.LocalPlayer.PlayerId;

        // Apply egg effect to local player for testing - use local player as both source AND target
        Vector2 testPosition = CalculateRandomGridPosition(); // Use random grid position for testing
        gameManager.SabotageManager.ApplyEggThrow(localPlayerId, localPlayerId, testPosition); // This will create the visual via event system

        GD.Print($"DEBUG: Applied egg throwing effect from player {localPlayerId} to player {localPlayerId} for testing");

        // REMOVED: Direct CreateEggSplatVisual call that was causing duplicate eggs
        // The SabotageManager.ApplyEggThrow above will trigger the event system which creates the visual
        GD.Print("DEBUG: Egg visual will be created via SabotageManager event system");
    }

    /// <summary>
    /// DIRECT TEST: Apply chat growth immediately without waiting for hand completion
    /// </summary>
    private void _on_debug_chat_growth_button_pressed()
    {
        GD.Print("DEBUG: Chat growth button pressed - testing chat panel growth directly");

        // Apply 4x growth directly
        ApplyChatPanelGrowth(4.0f);

        GD.Print("DEBUG: Chat panel growth applied directly");
    }

    /// <summary>
    /// DEBUG TEST: Force trigger HandCompleted event to test the existing flow
    /// </summary>
    private void _on_debug_hand_completed_button_pressed()
    {
        GD.Print("DEBUG: Hand completed button pressed - simulating hand completion");

        // Directly call the OnHandCompleted method to test the existing flow
        OnHandCompleted();

        GD.Print("DEBUG: OnHandCompleted() called directly");
    }

    /// <summary>
    /// DEBUG TEST: Shrink chat panel back to normal size
    /// </summary>
    private void _on_debug_chat_shrink_button_pressed()
    {
        GD.Print("DEBUG: Chat shrink button pressed - testing chat panel shrink to normal size");

        // Apply 1.0x size (normal size)
        ApplyChatPanelGrowth(1.0f);

        GD.Print("DEBUG: Chat panel shrink to normal size applied");
    }

    /// <summary>
    /// DEBUG TEST: Clean egg visual effects
    /// </summary>
    private void _on_debug_clean_eggs_button_pressed()
    {
        GD.Print("DEBUG: Clean eggs button pressed - testing egg effect cleaning");

        // Clean egg visual effects
        CleanSabotageVisual(SabotageType.EggThrow);

        GD.Print("DEBUG: Egg effect cleaning applied");
    }

    /// <summary>
    /// Display a chat message in the chat panel
    /// </summary>
    private void DisplayChatMessage(string playerName, string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm");
        string fullMessage = $"[{timestamp}] {playerName}: {message}";

        // Add to chat history
        chatHistory.Add(fullMessage);

        // Keep only the last MaxChatHistoryLines messages
        while (chatHistory.Count > MaxChatHistoryLines)
        {
            chatHistory.RemoveAt(0);
        }

        // Update chat display
        UpdateChatDisplay();

        GD.Print($"Chat displayed: {fullMessage}");
    }

    /// <summary>
    /// Update the chat display with current message history
    /// </summary>
    private void UpdateChatDisplay()
    {
        if (chatHistory.Count == 0)
        {
            chatLabel.Text = "Welcome! Type a message below to chat with other players.";
            chatLabel.Modulate = Colors.LightGray;
        }
        else
        {
            chatLabel.Text = string.Join("\n", chatHistory);
            chatLabel.Modulate = Colors.White;
        }
    }

    /// <summary>
    /// Handle received chat message from network
    /// </summary>
    public void OnChatMessageReceived(string playerName, string message)
    {
        GD.Print($"CardGameUI: Received chat message from {playerName}: {message}");
        DisplayChatMessage(playerName, message);
    }

    /// <summary>
    /// Handle player location changed event
    /// </summary>
    /// <param name="playerId">Player whose location changed</param>
    /// <param name="newLocation">New location as integer</param>
    private void OnPlayerLocationChanged(int playerId, int newLocation)
    {
        GD.Print($"CardGameUI: Player {playerId} location changed to {(GameManager.PlayerLocation)newLocation}");

        // If it's the local player, update the location controls
        if (gameManager?.LocalPlayer?.PlayerId == playerId)
        {
            UpdateLocationControls();
        }
    }

    /// <summary>
    /// Handle player left table event
    /// </summary>
    /// <param name="playerId">Player who left the table</param>
    private void OnPlayerLeftTable(int playerId)
    {
        GD.Print($"CardGameUI: Player {playerId} left the table");

        // If it's the local player, switch to kitchen view
        if (gameManager?.LocalPlayer?.PlayerId == playerId)
        {
            GD.Print("CardGameUI: Local player left table - switching to kitchen view");
            ShowKitchenView();
        }
    }

    /// <summary>
    /// Handle player returned to table event
    /// </summary>
    /// <param name="playerId">Player who returned to table</param>
    private void OnPlayerReturnedToTable(int playerId)
    {
        GD.Print($"CardGameUI: Player {playerId} returned to table");

        // If it's the local player, switch back to card table view
        if (gameManager?.LocalPlayer?.PlayerId == playerId)
        {
            GD.Print("CardGameUI: Local player returned to table - switching to card table view");
            ShowCardTableView();
        }
    }

    /// <summary>
    /// Handle client connected to server
    /// </summary>
    private void OnClientConnectedToServer()
    {
        GD.Print("CardGameUI: Connected to server as client");

        // GameManager will handle the rest of the connection process
        // Just wait for game start command from host
    }

    /// <summary>
    /// Update location controls based on current player location
    /// </summary>
    private void UpdateLocationControls()
    {
        if (gameManager?.LocalPlayer == null) return;

        int localPlayerId = gameManager.LocalPlayer.PlayerId;
        var location = gameManager.GetPlayerLocation(localPlayerId);

        switch (location)
        {
            case GameManager.PlayerLocation.AtTable:
                leaveTableButton.Disabled = false;
                leaveTableButton.Visible = true;
                break;

            case GameManager.PlayerLocation.InKitchen:
                leaveTableButton.Disabled = true;
                leaveTableButton.Visible = false;
                break;
        }
    }

    /// <summary>
    /// Show the card table view
    /// </summary>
    private void ShowCardTableView()
    {
        cardTableView.Visible = true;
        kitchenView.Visible = false;

        // Show card game UI elements
        if (playersInfoContainer != null)
            playersInfoContainer.Visible = true;
        if (chatPanel != null)
            chatPanel.Visible = true;

        GD.Print("CardGameUI: Showing card table view with full UI");
    }

    /// <summary>
    /// Show the kitchen view (realtime phase)
    /// </summary>
    private void ShowKitchenView()
    {
        cardTableView.Visible = false;
        kitchenView.Visible = true;

        // Hide card game UI elements during realtime phase
        if (playersInfoContainer != null)
            playersInfoContainer.Visible = false;
        if (chatPanel != null)
            chatPanel.Visible = false;

        GD.Print("CardGameUI: Showing kitchen view (realtime phase) - card game UI hidden");
    }

    /// <summary>
    /// Update the player's hand display with actual cards from CardManager
    /// </summary>
    private async void UpdatePlayerHand()
    {
        GD.Print("CardGameUI: UpdatePlayerHand called");

        if (playerHand == null)
        {
            GD.PrintErr("DEBUG: PlayerHand container is NULL!");
            return;
        }

        if (cardManager == null)
        {
            GD.Print("CardGameUI: CardManager is null, cannot update hand");
            return;
        }

        if (gameManager == null)
        {
            GD.Print("CardGameUI: GameManager is null, cannot update hand");
            return;
        }

        GD.Print($"CardGameUI: CardManager.GameInProgress: {cardManager.GameInProgress}");

        // Clear existing cards
        ClearPlayerHand();

        // Get current player ID (for now, assume local player is ID 0)
        int localPlayerId = gameManager.LocalPlayer?.PlayerId ?? 0;
        GD.Print($"CardGameUI: Local player ID: {localPlayerId}");

        // Get player's actual cards from CardManager
        currentPlayerCards = cardManager.GetPlayerHand(localPlayerId);

        GD.Print($"CardGameUI: Player {localPlayerId} has {currentPlayerCards.Count} cards");

        if (currentPlayerCards.Count == 0)
        {
            GD.Print("CardGameUI: No cards found - game may not have started yet or cards not dealt");
            return;
        }

        // FIXED: Use Control container with manual positioning to avoid container constraints
        var cardContainer = new Control();
        cardContainer.Name = "ManualCardContainer";
        // Set to fill the parent container - FIXED for Godot 4.4 API
        cardContainer.AnchorLeft = 0;
        cardContainer.AnchorTop = 0;
        cardContainer.AnchorRight = 1;
        cardContainer.AnchorBottom = 1;
        cardContainer.OffsetLeft = 0;
        cardContainer.OffsetTop = 0;
        cardContainer.OffsetRight = 0;
        cardContainer.OffsetBottom = 0;
        // CRITICAL: Don't clip contents - allows cards to be larger than container bounds
        cardContainer.ClipContents = false;
        playerHand.AddChild(cardContainer);

        // Wait one frame for container to get proper size
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        // FIXED: Use PlayerHand container size instead of inner container
        float cardWidth = cardSize.X;
        float cardHeight = cardSize.Y;
        float overlapSpacing = -50; // HAND CARDS: Overlap nicely for compact fan effect (50% overlap for 100px wide cards)

        // Use the PlayerHand container's actual size (which we know has proper dimensions)
        Vector2 availableSize = playerHand.Size;

        if (currentPlayerCards.Count <= 7)
        {
            // Single row - manually positioned at bottom center
            float totalWidthNeeded = (currentPlayerCards.Count - 1) * overlapSpacing + cardWidth;
            float startX = (availableSize.X - totalWidthNeeded) / 2; // Center horizontally
            float cardY = availableSize.Y - cardHeight - 10; // ADJUSTED: Appropriate margin for 100x140 cards

            for (int i = 0; i < currentPlayerCards.Count; i++)
            {
                var card = currentPlayerCards[i];
                var cardButton = CreateCardButton(card, i);
                cardButtons.Add(cardButton);

                // DIRECT POSITION CONTROL - bypassing all container constraints
                Vector2 cardPos = new Vector2(startX + i * overlapSpacing, cardY);
                cardButton.Position = cardPos;
                cardContainer.AddChild(cardButton);

                // FORCE SIZE AFTER ADDING TO SCENE TREE - prevents container from overriding size
                cardButton.Size = cardSize;
                cardButton.CustomMinimumSize = cardSize;

                // FINAL ENFORCEMENT: Set size again after next frame processing
                cardButton.CallDeferred(Control.MethodName.SetSize, cardSize);
            }

            GD.Print($"CardGameUI: Created {cardButtons.Count} manually positioned cards in single row");
        }
        else
        {
            // Two-row layout - manually positioned
            int cardsPerRow = (currentPlayerCards.Count + 1) / 2;
            float totalWidthNeeded = (cardsPerRow - 1) * overlapSpacing + cardWidth;
            float startX = (availableSize.X - totalWidthNeeded) / 2;
            float topRowY = availableSize.Y - cardHeight * 2 - 20; // ADJUSTED: Appropriate space between rows for 100x140 cards
            float bottomRowY = availableSize.Y - cardHeight - 10; // ADJUSTED: Appropriate margin for 100x140 cards

            for (int i = 0; i < currentPlayerCards.Count; i++)
            {
                var card = currentPlayerCards[i];
                var cardButton = CreateCardButton(card, i);
                cardButtons.Add(cardButton);

                Vector2 cardPos;
                if (i < cardsPerRow)
                {
                    // Top row
                    cardPos = new Vector2(startX + i * overlapSpacing, topRowY);
                }
                else
                {
                    // Bottom row
                    int bottomRowIndex = i - cardsPerRow;
                    cardPos = new Vector2(startX + bottomRowIndex * overlapSpacing, bottomRowY);
                }

                cardButton.Position = cardPos;
                cardContainer.AddChild(cardButton);

                // FORCE SIZE AFTER ADDING TO SCENE TREE - prevents container from overriding size
                cardButton.Size = cardSize;
                cardButton.CustomMinimumSize = cardSize;

                // FINAL ENFORCEMENT: Set size again after next frame processing
                cardButton.CallDeferred(Control.MethodName.SetSize, cardSize);
            }

            GD.Print($"CardGameUI: Created {cardButtons.Count} manually positioned cards in two rows ({cardsPerRow} top, {currentPlayerCards.Count - cardsPerRow} bottom)");
        }
    }

    /// <summary>
    /// Load all card graphics into memory for fast access - NEW METHOD
    /// </summary>
    private void LoadCardGraphics()
    {
        GD.Print("CardGameUI: Loading card graphics...");

        // Load card back texture
        try
        {
            cardBackTexture = GD.Load<Texture2D>(cardBackPath);
            GD.Print($"CardGameUI: Loaded card back texture from {cardBackPath}");
        }
        catch (Exception e)
        {
            GD.PrintErr($"CardGameUI: Failed to load card back texture: {e.Message}");
        }

        // Load all card face textures using the naming convention: [suit]_[rank].png
        string[] suits = { "clubs", "diamonds", "hearts", "spades" };
        string[] ranks = { "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "jack", "queen", "king", "ace" };

        int loadedCards = 0;
        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                string filename = $"{suit}_{rank}.png";
                string fullPath = cardAssetsPath + filename;

                try
                {
                    var texture = GD.Load<Texture2D>(fullPath);
                    if (texture != null)
                    {
                        // Create a key based on Card object format for fast lookup
                        string cardKey = GetCardKey(suit, rank);
                        cardTextures[cardKey] = texture;
                        loadedCards++;
                    }
                    else
                    {
                        GD.PrintErr($"CardGameUI: Failed to load texture for {filename} - texture is null");
                    }
                }
                catch (Exception e)
                {
                    GD.PrintErr($"CardGameUI: Failed to load texture for {filename}: {e.Message}");
                }
            }
        }

        GD.Print($"CardGameUI: Successfully loaded {loadedCards}/52 card textures");

        if (loadedCards < 52)
        {
            GD.PrintErr($"CardGameUI: Warning - Only loaded {loadedCards} out of 52 expected card textures!");
        }
    }

    /// <summary>
    /// Generate card key for texture lookup - NEW METHOD
    /// </summary>
    private string GetCardKey(string suit, string rank)
    {
        // Convert rank names to match Card enum values
        string cardRank = rank switch
        {
            "two" => "Two",
            "three" => "Three",
            "four" => "Four",
            "five" => "Five",
            "six" => "Six",
            "seven" => "Seven",
            "eight" => "Eight",
            "nine" => "Nine",
            "ten" => "Ten",
            "jack" => "Jack",
            "queen" => "Queen",
            "king" => "King",
            "ace" => "Ace",
            _ => rank
        };

        string cardSuit = suit switch
        {
            "clubs" => "Clubs",
            "diamonds" => "Diamonds",
            "hearts" => "Hearts",
            "spades" => "Spades",
            _ => suit
        };

        return $"{cardRank} of {cardSuit}";
    }

    /// <summary>
    /// Get texture for a specific card - NEW METHOD
    /// </summary>
    private Texture2D GetCardTexture(Card card)
    {
        string cardKey = card.ToString();

        if (cardTextures.ContainsKey(cardKey))
        {
            return cardTextures[cardKey];
        }
        else
        {
            GD.PrintErr($"CardGameUI: No texture found for card: {cardKey}");
            return null;
        }
    }

    /// <summary>
    /// Create a button for a specific card - UPDATED FOR GRAPHICS
    /// </summary>
    /// <param name="card">The card to represent</param>
    /// <param name="index">Index in hand</param>
    /// <returns>TextureButton representing the card with actual graphics</returns>
    private TextureButton CreateCardButton(Card card, int index)
    {
        var cardButton = new TextureButton();

        // Set card texture
        var cardTexture = GetCardTexture(card);
        if (cardTexture != null)
        {
            cardButton.TextureNormal = cardTexture;
            cardButton.TexturePressed = cardTexture; // Same texture when pressed
            cardButton.TextureHover = cardTexture;   // Same texture on hover (could add highlight later)
            GD.Print($"CardGameUI: Created card button for {card} with texture");
        }
        else
        {
            // Fallback: Create a simple colored button if texture fails
            cardButton.TextureNormal = CreateFallbackCardTexture(card);
            GD.PrintErr($"CardGameUI: Using fallback texture for {card}");
        }

        // Configure texture scaling FIRST - Critical for Godot 4.4 TextureButton sizing
        cardButton.IgnoreTextureSize = true; // Allow custom sizing
        cardButton.StretchMode = TextureButton.StretchModeEnum.KeepAspectCentered; // Scale texture to fit button size while maintaining aspect ratio

        // Set size (scaled up as requested) - AFTER configuring texture scaling
        cardButton.Size = cardSize;
        cardButton.CustomMinimumSize = cardSize;

        // Card button configured successfully

        // Store card index in button metadata
        cardButton.SetMeta("card_index", index);

        // Add tooltip showing card name
        cardButton.TooltipText = card.ToString();

        // Connect click event
        cardButton.Pressed += () => OnCardClicked(cardButton);

        return cardButton;
    }

    /// <summary>
    /// Create a display-only card button for trick area - OPTIMIZED FOR TRICKS
    /// </summary>
    /// <param name="card">The card to represent</param>
    /// <param name="playerId">Player who played this card</param>
    /// <returns>TextureButton representing the card for display only</returns>
    private TextureButton CreateTrickCardButton(Card card, int playerId)
    {
        var cardButton = new TextureButton();

        // Configure as display-only button
        cardButton.Disabled = true; // No clicking needed for trick cards
        cardButton.MouseFilter = Control.MouseFilterEnum.Ignore; // Don't interfere with other UI

        // Set card texture
        var cardTexture = GetCardTexture(card);
        if (cardTexture != null)
        {
            cardButton.TextureNormal = cardTexture;
            cardButton.TextureDisabled = cardTexture; // Show texture even when disabled
            GD.Print($"CardGameUI: Created trick card button for {card} by player {playerId}");
        }
        else
        {
            // Fallback: Create a simple colored button if texture fails
            cardButton.TextureNormal = CreateFallbackCardTexture(card, trickCardSize);
            cardButton.TextureDisabled = CreateFallbackCardTexture(card, trickCardSize);
            GD.PrintErr($"CardGameUI: Using fallback texture for trick card {card}");
        }

        // Configure texture scaling for trick cards
        cardButton.IgnoreTextureSize = true;
        cardButton.StretchMode = TextureButton.StretchModeEnum.KeepAspectCentered;

        // Set smaller size for trick area
        cardButton.Size = trickCardSize;
        cardButton.CustomMinimumSize = trickCardSize;

        // Add tooltip showing card and player info
        cardButton.TooltipText = $"Player {playerId}: {card}";

        // Trick card button configured successfully

        return cardButton;
    }

    /// <summary>
    /// Create a fallback texture if card graphics fail to load - FIXED for Godot 4.4 API
    /// </summary>
    private Texture2D CreateFallbackCardTexture(Card card, Vector2? customSize = null)
    {
        // Use custom size if provided, otherwise use default card size
        Vector2 textureSize = customSize ?? cardSize;

        // Create a simple ImageTexture as fallback - FIXED: Use CreateEmpty instead of Create
        var image = Image.CreateEmpty((int)textureSize.X, (int)textureSize.Y, false, Image.Format.Rgb8);

        // Color based on suit
        Color cardColor = card.Suit switch
        {
            Suit.Hearts => Colors.Red,
            Suit.Diamonds => Colors.Red,
            Suit.Clubs => Colors.Black,
            Suit.Spades => Colors.Black,
            _ => Colors.Gray
        };

        image.Fill(cardColor);

        var texture = ImageTexture.CreateFromImage(image);
        return texture;
    }

    /// <summary>
    /// Clear all cards from player hand UI - UPDATED FOR TextureButton
    /// </summary>
    private void ClearPlayerHand()
    {
        foreach (var button in cardButtons)
        {
            button.QueueFree();
        }
        cardButtons.Clear();

        // Also clear any remaining children
        foreach (Node child in playerHand.GetChildren())
        {
            child.QueueFree();
        }
    }

    private void OnCardClicked(TextureButton cardButton) // UPDATED: Parameter type changed
    {
        // Get the card index from button metadata
        if (cardButton.HasMeta("card_index"))
        {
            int cardIndex = cardButton.GetMeta("card_index").AsInt32();

            // Validate index and get card from current hand
            if (cardIndex >= 0 && cardIndex < currentPlayerCards.Count)
            {
                var card = currentPlayerCards[cardIndex];
                GD.Print($"CardGameUI: Player clicked {card}");

                // Try to play the card through CardManager
                if (cardManager != null && gameManager != null)
                {
                    int localPlayerId = gameManager.LocalPlayer?.PlayerId ?? 0;
                    bool success = cardManager.PlayCard(localPlayerId, card);

                    if (success)
                    {
                        GD.Print($"CardGameUI: Successfully played {card}");
                        // Card will be removed from hand via CardManager events
                    }
                    else
                    {
                        GD.Print($"CardGameUI: Failed to play {card} - not valid move");
                    }
                }
            }
            else
            {
                GD.PrintErr($"CardGameUI: Invalid card index {cardIndex}");
            }
        }
    }

    /// <summary>
    /// Handle turn started event
    /// </summary>
    /// <param name="playerId">Player whose turn started</param>
    private void OnTurnStarted(int playerId)
    {
        GD.Print($"CardGameUI: Turn started for player {playerId}");

        // Update UI to show whose turn it is
        if (gameManager != null && gameManager.LocalPlayer?.PlayerId == playerId)
        {
            // It's our turn - enable card interactions
            SetCardsInteractable(true);
        }
        else
        {
            // Not our turn - disable card interactions
            SetCardsInteractable(false);
        }
    }

    /// <summary>
    /// Handle turn ended event
    /// </summary>
    /// <param name="playerId">Player whose turn ended</param>
    private void OnTurnEnded(int playerId)
    {
        GD.Print($"CardGameUI: Turn ended for player {playerId}");

        // Disable card interactions
        SetCardsInteractable(false);
    }

    /// <summary>
    /// Handle card played event
    /// </summary>
    /// <param name="playerId">Player who played card</param>
    /// <param name="cardString">String representation of card</param>
    private void OnCardPlayed(int playerId, string cardString)
    {
        GD.Print($"CardGameUI: Player {playerId} played {cardString}");

        // If it was our card, remove it from hand
        if (gameManager != null && gameManager.LocalPlayer?.PlayerId == playerId)
        {
            UpdatePlayerHand(); // Refresh hand display
        }
    }

    /// <summary>
    /// Handle trick completed event
    /// </summary>
    /// <param name="winnerId">Player who won the trick</param>
    private void OnTrickCompleted(int winnerId)
    {
        GD.Print($"CardGameUI: Player {winnerId} won the trick");

        // Clear trick area using the dedicated method
        ClearTrickDisplay();

        // Reset trick tracking state
        lastTrickCount = 0;
        lastTrickState.Clear();

        // Update scores if needed
        UpdateScoreDisplay();
    }

    /// <summary>
    /// Handle hand completed event - this triggers chat intimidation for losers
    /// </summary>
    private void OnHandCompleted()
    {
        GD.Print("CardGameUI: Hand completed");

        // FOR DEBUGGING: Always apply chat intimidation regardless of win/lose
        GD.Print("CardGameUI: DEBUG MODE - Applying chat intimidation regardless of game outcome");

        // Apply chat intimidation directly (4x growth for debugging)
        ApplyChatPanelGrowth(4.0f); // 4x growth (quadruple size)
        GD.Print("CardGameUI: Chat intimidation applied directly - 4x growth for debugging");

        // TODO: Remove this debug behavior and restore win/lose logic
        // Get game state to determine losers (COMMENTED OUT FOR DEBUGGING)
        /*
        if (cardManager != null && gameManager != null)
        {
            var gameState = cardManager.GetGameState();
            int localPlayerId = gameManager.LocalPlayer?.PlayerId ?? 0;

            // Check if local player is a loser
            if (gameState.PlayerScores.ContainsKey(localPlayerId))
            {
                int localScore = gameState.PlayerScores[localPlayerId];
                int maxScore = gameState.PlayerScores.Values.Max();

                // If local player didn't get the highest score, they lost
                if (localScore < maxScore)
                {
                    GD.Print($"CardGameUI: Local player lost with score {localScore} vs max {maxScore} - applying chat intimidation");

                    // Apply chat intimidation directly (simplified approach)
                    ApplyChatPanelGrowth(4.0f); // 4x growth (quadruple size)
                    GD.Print("CardGameUI: Chat intimidation applied directly");
                }
                else if (localScore == maxScore)
                {
                    GD.Print($"CardGameUI: Local player won with score {localScore} - removing chat intimidation");

                    // Remove chat intimidation for winners (back to normal size)
                    ApplyChatPanelGrowth(1.0f); // Normal size
                    GD.Print("CardGameUI: Chat intimidation removed - back to normal size");
                }
            }
        }
        */

        // Clear hand display
        ClearPlayerHand();

        // Wait for new hand to be dealt
        // UpdatePlayerHand will be called when new hand starts
    }

    /// <summary>
    /// Handle hand dealt event - cards have been distributed to players
    /// </summary>
    private void OnHandDealt()
    {
        GD.Print("CardGameUI: Cards dealt - updating hand display");
        UpdatePlayerHand();
    }

    /// <summary>
    /// Handle turn timer update from host (for network synchronization)
    /// </summary>
    /// <param name="timeRemaining">Time remaining in seconds</param>
    private void OnTurnTimerUpdated(float timeRemaining)
    {
        // This allows clients to receive timer updates from the host
        // The actual timer display is handled in UpdateTimerDisplay()
        // which now gets the synchronized timer value
    }

    /// <summary>
    /// Handle player connected to network
    /// </summary>
    /// <param name="playerId">ID of connected player</param>
    private void OnPlayerConnected(int playerId)
    {
        GD.Print($"CardGameUI: Player {playerId} connected");

        // GameManager now handles starting the card game automatically
        // No need for CardGameUI to manage this anymore
    }

    /// <summary>
    /// Handle chat intimidation applied event
    /// </summary>
    private void OnChatIntimidationApplied(int playerId, float newSize)
    {
        // If it's the local player, grow the chat panel
        if (gameManager?.LocalPlayer?.PlayerId == playerId)
        {
            GD.Print($"CardGameUI: Applying chat intimidation - growing chat panel to {newSize}");
            ApplyChatPanelGrowth(newSize);
        }
    }

    /// <summary>
    /// Handle chat intimidation removed event
    /// </summary>
    private void OnChatIntimidationRemoved(int playerId)
    {
        // If it's the local player, shrink the chat panel back to normal
        if (gameManager?.LocalPlayer?.PlayerId == playerId)
        {
            GD.Print("CardGameUI: Removing chat intimidation - shrinking chat panel to normal size");
            ApplyChatPanelGrowth(1.0f); // Normal size
        }
    }

    /// <summary>
    /// Handle egg thrown event - creates visual effect for target player
    /// </summary>
    private void OnEggThrown(int sourcePlayerId, int targetPlayerId, Vector2 targetPosition)
    {
        GD.Print($"DEBUG: Egg thrown event - source: {sourcePlayerId}, target: {targetPlayerId}");

        // Only show visual effect if local player is the target
        if (gameManager?.LocalPlayer?.PlayerId == targetPlayerId)
        {
            GD.Print("DEBUG: Local player is the target - creating egg splat visual effect");
            CreateEggSplatVisual(targetPosition);
        }
    }

    /// <summary>
    /// Handle sabotage applied event - for general sabotage effects
    /// </summary>
    private void OnSabotageApplied(int playerId, int sabotageTypeInt)
    {
        var sabotageType = (SabotageType)sabotageTypeInt;
        GD.Print($"DEBUG: Sabotage applied - player: {playerId}, type: {sabotageType}");

        // Only show visual effect if local player is affected
        if (gameManager?.LocalPlayer?.PlayerId == playerId)
        {
            switch (sabotageType)
            {
                case SabotageType.StinkBomb:
                    CreateStinkFogVisual();
                    break;
                    // EggThrow is handled by OnEggThrown event
            }
        }
    }

    /// <summary>
    /// Handle sabotage cleaned event - removes visual effects
    /// </summary>
    private void OnSabotageCleaned(int targetPlayerId, SabotageType sabotageType)
    {
        GD.Print($"DEBUG: Sabotage cleaned event - player: {targetPlayerId}, type: {sabotageType}");

        // Only clean visual effect if local player cleaned it
        if (gameManager?.LocalPlayer?.PlayerId == targetPlayerId)
        {
            GD.Print($"DEBUG: Local player cleaned {sabotageType} - triggering comprehensive visual cleanup");
            CleanSabotageVisual(sabotageType);
        }
        else
        {
            GD.Print($"DEBUG: Player {targetPlayerId} cleaned {sabotageType} but not local player - no visual cleanup needed");
        }
    }

    /// <summary>
    /// Handle stink bomb exploded event - creates area effect visual
    /// </summary>
    private void OnStinkBombExploded(Vector2 bombPosition)
    {
        GD.Print($"DEBUG: Stink bomb exploded at {bombPosition}");
        // This event is more for showing explosion animation
        // The actual fog effect is handled by OnSabotageApplied
    }

    /// <summary>
    /// Apply chat panel growth effect
    /// </summary>
    private void ApplyChatPanelGrowth(float multiplier)
    {
        if (chatPanel == null)
        {
            GD.PrintErr("CardGameUI: Cannot apply chat growth - chatPanel is null!");
            return;
        }

        Vector2 baseSize = new Vector2(260, 160); // Base chat panel size (from scene: 280x160, but using 260x160 for growth calc)
        Vector2 newSize = baseSize * multiplier;

        // Calculate the bottom-right corner position (this should stay fixed)
        Vector2 currentBottomRight = chatPanel.Position + chatPanel.Size;

        // Calculate new position to keep bottom-right corner fixed
        Vector2 newPosition = currentBottomRight - newSize;

        // Animate both size and position changes
        var tween = CreateTween();
        if (tween != null)
        {
            // Animate both size and position in parallel
            tween.Parallel().TweenProperty(chatPanel, "size", newSize, 0.5f);
            tween.Parallel().TweenProperty(chatPanel, "position", newPosition, 0.5f);
        }
        else
        {
            // Fallback: set size and position directly if tween fails
            chatPanel.Size = newSize;
            chatPanel.Position = newPosition;
        }

        // Update chat label to show intimidation status
        if (chatLabel != null)
        {
            if (multiplier > 1.0f)
            {
                chatLabel.Text = $"DEBUG: Chat panel grew {multiplier:F1}x! Growing UP and LEFT, bottom-right corner should stay fixed.";
                chatLabel.Modulate = Colors.Orange;
            }
            else
            {
                chatLabel.Text = "Chat panel back to normal size.";
                chatLabel.Modulate = Colors.White;
            }
            GD.Print($"CardGameUI: Chat label updated: {chatLabel.Text}");
        }
        else
        {
            GD.PrintErr("CardGameUI: Cannot update chat label - chatLabel is null!");
        }

        GD.Print($"CardGameUI: === END CHAT PANEL GROWTH DEBUG ===");
    }

    /// <summary>
    /// Enable/disable card button interactions - UPDATED FOR TextureButton
    /// </summary>
    /// <param name="interactable">Whether cards should be clickable</param>
    private void SetCardsInteractable(bool interactable)
    {
        if (cardManager == null || gameManager == null) return;

        int localPlayerId = gameManager.LocalPlayer?.PlayerId ?? 0;

        for (int i = 0; i < cardButtons.Count; i++)
        {
            var button = cardButtons[i];

            if (interactable && i < currentPlayerCards.Count)
            {
                // Check if this specific card is valid to play
                var card = currentPlayerCards[i];
                bool isValidCard = cardManager.IsValidCardPlay(localPlayerId, card);

                button.Disabled = !isValidCard;

                // Visual feedback for valid/invalid cards using modulation only
                if (isValidCard)
                {
                    button.Modulate = Colors.White; // Normal color for valid cards
                    // Could add a green tint: button.Modulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    button.Modulate = new Color(0.5f, 0.5f, 0.5f, 1.0f); // Grayed out for invalid cards
                }
            }
            else
            {
                button.Disabled = true;
                button.Modulate = new Color(0.5f, 0.5f, 0.5f, 1.0f); // Grayed out when not interactive
            }
        }
    }

    /// <summary>
    /// Create a styled card background
    /// </summary>
    /// <param name="color">Background color</param>
    /// <returns>StyleBox for card</returns>
    private StyleBoxFlat CreateCardStyle(Color color)
    {
        var style = new StyleBoxFlat();
        style.BgColor = color;
        style.BorderColor = Colors.Black;
        style.BorderWidthTop = 2;
        style.BorderWidthBottom = 2;
        style.BorderWidthLeft = 2;
        style.BorderWidthRight = 2;
        style.CornerRadiusTopLeft = 5;
        style.CornerRadiusTopRight = 5;
        style.CornerRadiusBottomLeft = 5;
        style.CornerRadiusBottomRight = 5;
        return style;
    }

    /// <summary>
    /// Update score display
    /// </summary>
    private void UpdateScoreDisplay()
    {
        if (cardManager == null || gameManager == null) return;

        var gameState = cardManager.GetGameState();
        int localPlayerId = gameManager.LocalPlayer?.PlayerId ?? 0;

        if (gameState.PlayerScores.ContainsKey(localPlayerId))
        {
            int score = gameState.PlayerScores[localPlayerId];
            scoreLabel.Text = $"Score: {score}";
        }
    }

    /// <summary>
    /// Update timer display with float
    /// </summary>
    /// <param name="seconds">Seconds remaining</param>
    public void UpdateTimer(float seconds)
    {
        timerLabel.Text = ((int)seconds).ToString();
    }

    /// <summary>
    /// Continuously update timer display from CardManager
    /// </summary>
    private void UpdateTimerDisplay()
    {
        if (cardManager == null || !cardManager.GameInProgress)
        {
            timerLabel.Text = "10";
            return;
        }

        var gameState = cardManager.GetGameState();
        float timeRemaining = gameState.TurnTimeRemaining;
        timerLabel.Text = ((int)timeRemaining).ToString();
    }

    /// <summary>
    /// Update game state display with player turn and other info
    /// </summary>
    private void UpdateGameStateDisplay()
    {
        if (cardManager == null || gameManager == null || !cardManager.GameInProgress)
        {
            return;
        }

        var gameState = cardManager.GetGameState();
        int localPlayerId = gameManager.LocalPlayer?.PlayerId ?? 0;

        // Update score display
        if (gameState.PlayerScores.ContainsKey(localPlayerId))
        {
            int score = gameState.PlayerScores[localPlayerId];
            scoreLabel.Text = $"Score: {score}";
        }
    }

    public void UpdateScore(int score)
    {
        scoreLabel.Text = $"Score: {score}";
    }

    /// <summary>
    /// Handle chat panel resized (for chat intimidation feature)
    /// </summary>
    private void _on_chat_panel_resized()
    {
        // This method is called when the chat panel is resized
        // We can use this for the chat intimidation feature
        GD.Print($"Chat panel resized to: {chatPanel.Size}");
    }

    /// <summary>
    /// Create visual egg splat effect on screen
    /// </summary>
    private void CreateEggSplatVisual(Vector2 position)
    {
        GD.Print($"DEBUG: Creating egg splat visual at {position}");

        if (overlayLayer == null)
        {
            GD.PrintErr("DEBUG: Cannot create egg splat - overlay layer is null");
            return;
        }

        // Get player's throw power for coverage calculation
        float coverage = 0.5f; // Default coverage
        if (gameManager?.LocalPlayer != null)
        {
            coverage = gameManager.LocalPlayer.GetThrowPowerCoverage();
        }

        // Create egg splat visual using actual PNG asset
        var splatTextureRect = new TextureRect();
        splatTextureRect.Name = "EggSplat"; // Set name first

        // CRITICAL: Add metadata to identify as egg splat (names can get auto-generated by Godot)
        splatTextureRect.SetMeta("IsEggSplat", true);
        splatTextureRect.SetMeta("CreatedAt", Time.GetUnixTimeFromSystem());

        GD.Print($"DEBUG: Created TextureRect with name '{splatTextureRect.Name}' and EggSplat metadata");

        // Load the egg splat texture asset
        var splatTexture = GD.Load<Texture2D>("res://assets/sabotage/Raw_egg_splatter_on_...-1106652873-0.png");
        if (splatTexture == null)
        {
            GD.PrintErr("DEBUG: Failed to load Raw_egg_splatter PNG - falling back to colored rectangle");
            // Fallback to colored rectangle if texture fails to load
            var fallbackPanel = new Panel();
            fallbackPanel.Name = "EggSplat";
            fallbackPanel.SetMeta("IsEggSplat", true);
            fallbackPanel.SetMeta("CreatedAt", Time.GetUnixTimeFromSystem());

            float fallbackSize = 3000.0f * coverage;
            fallbackPanel.Size = new Vector2(fallbackSize, fallbackSize);
            Vector2 fallbackPosition = position == Vector2.Zero ?
                CalculateRandomGridPosition() - new Vector2(fallbackSize, fallbackSize) * 0.5f :
                position - new Vector2(fallbackSize, fallbackSize) * 0.5f;
            fallbackPanel.Position = fallbackPosition;

            var styleBox = new StyleBoxFlat();
            styleBox.BgColor = new Color(1.0f, 0.8f, 0.2f, 0.95f); // Less transparent fallback
            fallbackPanel.AddThemeStyleboxOverride("panel", styleBox);
            fallbackPanel.MouseFilter = Control.MouseFilterEnum.Ignore; // Allow click-through
            overlayLayer.AddChild(fallbackPanel);
            return;
        }

        splatTextureRect.Texture = splatTexture;

        // Calculate size based on coverage - MADE 15X LARGER TOTAL (was 200, now 3000)
        float baseSize = 3000.0f; // 15x larger than original 200px
        Vector2 splatSize = new Vector2(baseSize * coverage, baseSize * coverage);
        splatTextureRect.Size = splatSize;

        // Position randomly in one of 6 grid sections if no specific position given
        Vector2 splatPosition = position == Vector2.Zero ?
            CalculateRandomGridPosition() - splatSize * 0.5f :
            position - splatSize * 0.5f;

        splatTextureRect.Position = splatPosition;

        // Configure texture rendering
        splatTextureRect.ExpandMode = TextureRect.ExpandModeEnum.FitWidthProportional;
        splatTextureRect.StretchMode = TextureRect.StretchModeEnum.KeepAspectCovered;

        // Make egg splat less transparent and allow click-through
        splatTextureRect.Modulate = new Color(1.0f, 1.0f, 1.0f, 0.95f); // Minimal transparency
        splatTextureRect.MouseFilter = Control.MouseFilterEnum.Ignore; // Allow clicking buttons underneath

        // Add to overlay layer
        overlayLayer.AddChild(splatTextureRect);

        // Verify name after adding to scene tree
        GD.Print($"DEBUG: After adding to scene tree, TextureRect name is: '{splatTextureRect.Name}'");
        GD.Print($"DEBUG: TextureRect metadata IsEggSplat: {splatTextureRect.GetMeta("IsEggSplat", false)}");

        // Add to tracking
        int localPlayerId = gameManager?.LocalPlayer?.PlayerId ?? 0;
        if (!playerVisualOverlays.ContainsKey(localPlayerId))
        {
            playerVisualOverlays[localPlayerId] = new List<Control>();
        }
        playerVisualOverlays[localPlayerId].Add(splatTextureRect);

        GD.Print($"DEBUG: Egg splat visual created with coverage {coverage:P1}, size {splatSize} (15x larger than original!)");
    }

    /// <summary>
    /// Create visual stink fog effect on screen
    /// </summary>
    private void CreateStinkFogVisual()
    {
        GD.Print("DEBUG: Creating stink fog visual");

        if (overlayLayer == null)
        {
            GD.PrintErr("DEBUG: Cannot create stink fog - overlay layer is null");
            return;
        }

        // Get player's composure for blur intensity calculation
        float blurIntensity = 0.5f; // Default intensity
        if (gameManager?.LocalPlayer != null)
        {
            blurIntensity = gameManager.LocalPlayer.GetBlurStrength();
        }

        // Create fog visual (full screen overlay)
        var fogPanel = new Panel();
        fogPanel.Name = "StinkFog";
        // Set anchors to fill entire screen manually
        fogPanel.AnchorLeft = 0.0f;
        fogPanel.AnchorTop = 0.0f;
        fogPanel.AnchorRight = 1.0f;
        fogPanel.AnchorBottom = 1.0f;
        fogPanel.OffsetLeft = 0.0f;
        fogPanel.OffsetTop = 0.0f;
        fogPanel.OffsetRight = 0.0f;
        fogPanel.OffsetBottom = 0.0f;

        // Style the fog (green color with varying transparency based on composure)
        var styleBox = new StyleBoxFlat();
        styleBox.BgColor = new Color(0.0f, 0.8f, 0.2f, blurIntensity * 0.3f); // Green with variable transparency
        fogPanel.AddThemeStyleboxOverride("panel", styleBox);

        // Add to overlay layer
        overlayLayer.AddChild(fogPanel);

        // Add to tracking
        int localPlayerId = gameManager?.LocalPlayer?.PlayerId ?? 0;
        if (!playerVisualOverlays.ContainsKey(localPlayerId))
        {
            playerVisualOverlays[localPlayerId] = new List<Control>();
        }
        playerVisualOverlays[localPlayerId].Add(fogPanel);

        // Auto-remove after 30 seconds (PRD requirement)
        GetTree().CreateTimer(30.0).Timeout += () =>
        {
            if (IsInstanceValid(fogPanel))
            {
                fogPanel.QueueFree();
                if (playerVisualOverlays.ContainsKey(localPlayerId))
                {
                    playerVisualOverlays[localPlayerId].Remove(fogPanel);
                }
                GD.Print("DEBUG: Stink fog visual auto-removed after 30 seconds");
            }
        };

        GD.Print($"DEBUG: Stink fog visual created with intensity {blurIntensity:P1}");
    }

    /// <summary>
    /// Clean sabotage visual effects - COMPREHENSIVE VERSION with ROBUST identification
    /// </summary>
    private void CleanSabotageVisual(SabotageType sabotageType)
    {
        int localPlayerId = gameManager?.LocalPlayer?.PlayerId ?? 0;

        var allRemovedEffects = new List<string>();
        int totalFoundEffects = 0;

        // STEP 1: Clean tracked overlays with SAFE iteration
        if (playerVisualOverlays.ContainsKey(localPlayerId))
        {
            var overlaysList = playerVisualOverlays[localPlayerId];

            // Create a COPY of the list to safely iterate (avoid collection modification during iteration)
            var overlaysListCopy = new List<Control>(overlaysList);

            foreach (var overlay in overlaysListCopy)
            {
                if (overlay != null && IsInstanceValid(overlay))
                {
                    string overlayName = overlay.Name;

                    // Check both name AND metadata for identification
                    bool isEggSplat = (overlayName == "EggSplat") || overlay.GetMeta("IsEggSplat", false).AsBool();
                    bool isStinkFog = (overlayName == "StinkFog") || overlay.GetMeta("IsStinkFog", false).AsBool();

                    bool shouldRemove = false;
                    switch (sabotageType)
                    {
                        case SabotageType.EggThrow:
                            if (isEggSplat)
                            {
                                shouldRemove = true;
                            }
                            break;
                        case SabotageType.StinkBomb:
                            if (isStinkFog)
                            {
                                shouldRemove = true;
                            }
                            break;
                    }

                    if (shouldRemove)
                    {
                        totalFoundEffects++;

                        // Remove from parent and tracking FIRST, then free
                        if (overlay.GetParent() != null)
                        {
                            overlay.GetParent().RemoveChild(overlay);
                        }
                        playerVisualOverlays[localPlayerId].Remove(overlay);
                        overlay.QueueFree(); // Use QueueFree for safer removal

                        allRemovedEffects.Add($"tracked {overlayName}");
                    }
                }
                else
                {
                    // Clean up invalid references from tracking
                    if (playerVisualOverlays[localPlayerId].Contains(overlay))
                    {
                        playerVisualOverlays[localPlayerId].Remove(overlay);
                    }
                }
            }
        }

        // STEP 2: COMPREHENSIVE CLEANUP - Search entire overlay layer multiple times until no more found
        int searchRounds = 0;
        bool foundEffectsThisRound = true;

        while (foundEffectsThisRound && searchRounds < 10) // Max 10 rounds to prevent infinite loop
        {
            searchRounds++;
            foundEffectsThisRound = false;

            if (overlayLayer != null)
            {
                var allChildren = overlayLayer.GetChildren();

                // Create a copy of children list for safe iteration
                var childrenCopy = new List<Node>();
                foreach (Node child in allChildren)
                {
                    childrenCopy.Add(child);
                }

                foreach (Node child in childrenCopy)
                {
                    if (child != null && IsInstanceValid(child))
                    {
                        string childName = child.Name;

                        // Enhanced identification using metadata
                        bool isEggSplat = false;
                        bool isStinkFog = false;

                        if (child is Control control)
                        {
                            isEggSplat = (childName == "EggSplat") || control.GetMeta("IsEggSplat", false).AsBool();
                            isStinkFog = (childName == "StinkFog") || control.GetMeta("IsStinkFog", false).AsBool();
                        }

                        bool shouldRemove = false;
                        switch (sabotageType)
                        {
                            case SabotageType.EggThrow:
                                if (isEggSplat)
                                {
                                    shouldRemove = true;
                                }
                                break;
                            case SabotageType.StinkBomb:
                                if (isStinkFog)
                                {
                                    shouldRemove = true;
                                }
                                break;
                        }

                        if (shouldRemove)
                        {
                            totalFoundEffects++;
                            foundEffectsThisRound = true;

                            // Safe removal
                            if (child.GetParent() != null)
                            {
                                child.GetParent().RemoveChild(child);
                            }
                            child.QueueFree();

                            allRemovedEffects.Add($"untracked {childName} (round {searchRounds})");
                        }
                    }
                }
            }
            else
            {
                GD.PrintErr("DEBUG: Overlay layer is null - cannot perform comprehensive cleanup");
                break;
            }
        }

        // STEP 3: Final verification after a short delay to let QueueFree take effect
        GetTree().CreateTimer(0.1).Timeout += () =>
        {
            VerifyCleanupComplete(sabotageType);
        };

    }

    /// <summary>
    /// Verify that cleanup was successful
    /// </summary>
    private void VerifyCleanupComplete(SabotageType sabotageType)
    {
        if (overlayLayer != null)
        {
            var remainingChildren = overlayLayer.GetChildren();
            var remainingTargetEffects = new List<string>();

            foreach (Node child in remainingChildren)
            {
                if (child != null && IsInstanceValid(child))
                {
                    string childName = child.Name;

                    // Check using metadata-based identification
                    bool isTargetEffect = false;

                    if (child is Control control)
                    {
                        switch (sabotageType)
                        {
                            case SabotageType.EggThrow:
                                isTargetEffect = (childName == "EggSplat") || control.GetMeta("IsEggSplat", false).AsBool();
                                break;
                            case SabotageType.StinkBomb:
                                isTargetEffect = (childName == "StinkFog") || control.GetMeta("IsStinkFog", false).AsBool();
                                break;
                        }
                    }

                    if (isTargetEffect)
                    {
                        remainingTargetEffects.Add($"{childName} (metadata check)");
                    }
                }
            }

            if (remainingTargetEffects.Count > 0)
            {
                GD.PrintErr($"WARNING - {remainingTargetEffects.Count} {sabotageType} effects still remain: {string.Join(", ", remainingTargetEffects)}");
            }
        }
    }

    public override void _ExitTree()
    {
        // Disconnect from CardManager events
        if (cardManager != null)
        {
            cardManager.TurnStarted -= OnTurnStarted;
            cardManager.TurnEnded -= OnTurnEnded;
            cardManager.CardPlayed -= OnCardPlayed;
            cardManager.TrickCompleted -= OnTrickCompleted;
            cardManager.HandCompleted -= OnHandCompleted;
        }
    }

    /// <summary>
    /// Calculate a random position based on 3x2 grid system (6 sections total)
    /// Each egg throw randomly selects one of the 6 sections and centers the splat there
    /// </summary>
    /// <returns>Random position centered in one of the 6 grid sections</returns>
    private Vector2 CalculateRandomGridPosition()
    {
        // Get screen/viewport size
        var viewport = GetViewport();
        if (viewport == null)
        {
            GD.PrintErr("CardGameUI: Cannot get viewport for random position calculation");
            return new Vector2(640, 360); // Default fallback position
        }

        Vector2 screenSize = viewport.GetVisibleRect().Size;

        // Divide screen into 3x2 grid (3 columns, 2 rows = 6 sections total)
        float sectionWidth = screenSize.X / 3.0f;
        float sectionHeight = screenSize.Y / 2.0f;

        // Randomly select one of the 6 sections (0-5)
        int randomSection = GD.RandRange(0, 5);

        // Calculate grid coordinates (column, row)
        int column = randomSection % 3;  // 0, 1, or 2
        int row = randomSection / 3;     // 0 or 1

        // Calculate center position of the selected section
        float centerX = (column + 0.5f) * sectionWidth;
        float centerY = (row + 0.5f) * sectionHeight;

        Vector2 randomPosition = new Vector2(centerX, centerY);

        GD.Print($"CardGameUI: Random egg position - Section {randomSection} (col:{column}, row:{row}) = {randomPosition}");

        return randomPosition;
    }
}



