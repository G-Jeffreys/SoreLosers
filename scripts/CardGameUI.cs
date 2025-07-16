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
    private Button returnTableButton;

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
    private readonly Vector2 cardSize = new Vector2(140, 190); // LARGER: Bigger cards that are more readable
    private readonly string cardAssetsPath = "res://assets/cards/faces/";
    private readonly string cardBackPath = "res://assets/cards/backs/card_back_blue.png";

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
        chatPanel = GetNode<Panel>("ChatPanel");
        chatLabel = GetNode<Label>("ChatPanel/ChatVBox/ChatLabel");
        chatInput = GetNode<LineEdit>("ChatPanel/ChatVBox/ChatInput");
        leaveTableButton = GetNode<Button>("CardTableView/GameArea/LeaveTableButton");

        // Get references to kitchen UI elements
        player = GetNode<CharacterBody2D>("KitchenView/Player");
        inventoryLabel = GetNode<Label>("KitchenView/KitchenUI/InventoryLabel");
        returnTableButton = GetNode<Button>("KitchenView/ReturnTableButton");

        // Get players info panel
        playersInfoContainer = GetNode<VBoxContainer>("PlayersInfoPanel");

        // Get debug button
        debugEggButton = GetNode<Button>("PlayersInfoPanel/DebugEggButton");

        // DEBUG: Verify chat panel properties on startup
        if (chatPanel != null)
        {
            GD.Print($"DEBUG: Chat panel found! Initial size: {chatPanel.Size}");
            GD.Print($"DEBUG: Chat panel position: {chatPanel.Position}");
            GD.Print($"DEBUG: Chat panel anchors: L={chatPanel.AnchorLeft}, T={chatPanel.AnchorTop}, R={chatPanel.AnchorRight}, B={chatPanel.AnchorBottom}");
            GD.Print($"DEBUG: Chat panel offsets: L={chatPanel.OffsetLeft}, T={chatPanel.OffsetTop}, R={chatPanel.OffsetRight}, B={chatPanel.OffsetBottom}");

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

            GD.Print("DEBUG: Chat panel styled to be semi-transparent and visually in front");
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

        GD.Print("DEBUG: Sabotage overlay layer created");

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
        UpdateTrickDisplay();
        UpdatePlayersInfo();
        UpdateLocationControls(); // Update location controls based on current player location
    }

    /// <summary>
    /// Update the trick area to show cards played in current trick
    /// </summary>
    private void UpdateTrickDisplay()
    {
        if (cardManager == null || !cardManager.GameInProgress) return;

        var gameState = cardManager.GetGameState();

        // Clear existing trick cards
        foreach (Node child in trickArea.GetChildren())
        {
            if (child.Name.ToString().StartsWith("TrickCard"))
            {
                child.QueueFree();
            }
        }

        // Add cards from current trick
        for (int i = 0; i < gameState.CurrentTrick.Count; i++)
        {
            var cardPlay = gameState.CurrentTrick[i];
            var cardLabel = new Label();
            cardLabel.Name = $"TrickCard_{i}";
            cardLabel.Text = $"P{cardPlay.PlayerId}: {cardPlay.Card}";
            cardLabel.Position = new Vector2(i * 60, i * 20);
            cardLabel.Size = new Vector2(150, 30);
            cardLabel.AddThemeStyleboxOverride("normal", CreateCardStyle(Colors.White));
            trickArea.AddChild(cardLabel);
        }
    }

    /// <summary>
    /// Update the players info panel to show each player's status
    /// </summary>
    private void UpdatePlayersInfo()
    {
        if (cardManager == null || gameManager == null || !cardManager.GameInProgress) return;

        var gameState = cardManager.GetGameState();

        // Clear existing player info labels (but keep the title)
        foreach (var label in playerInfoLabels)
        {
            label?.QueueFree();
        }
        playerInfoLabels.Clear();

        // Add info for each connected player
        foreach (var playerKvp in gameManager.ConnectedPlayers)
        {
            int playerId = playerKvp.Key;
            var playerData = playerKvp.Value;

            var playerInfoLabel = new Label();

            // Get hand size for this player
            var playerHand = cardManager.GetPlayerHand(playerId);
            int handSize = playerHand.Count;

            // Get score
            int score = gameState.PlayerScores.GetValueOrDefault(playerId, 0);

            // Check if it's this player's turn
            bool isCurrentTurn = gameState.CurrentPlayerTurn == playerId;
            string turnIndicator = isCurrentTurn ? " [TURN]" : "";

            // Get player location
            var location = gameManager.GetPlayerLocation(playerId);
            string locationText = location == GameManager.PlayerLocation.AtTable ? "📍" : "🍳";

            // Format player info
            playerInfoLabel.Text = $"{locationText} {playerData.PlayerName}: {handSize} cards, {score} pts{turnIndicator}";

            // Highlight current turn player
            if (isCurrentTurn)
            {
                playerInfoLabel.Modulate = Colors.Yellow;
            }
            else
            {
                playerInfoLabel.Modulate = Colors.White;
            }

            playersInfoContainer.AddChild(playerInfoLabel);
            playerInfoLabels.Add(playerInfoLabel);
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

            GD.Print("DEBUG: Connected to SabotageManager events including SabotageCleaned");
        }
        else
        {
            GD.PrintErr("DEBUG: SabotageManager not available for visual effects");
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
    /// Handle return table button pressed (now connected via scene)
    /// </summary>
    private void _on_return_table_button_pressed()
    {
        GD.Print("Return to Table button pressed");
        if (gameManager?.LocalPlayer != null)
        {
            gameManager.PlayerReturnToTable(gameManager.LocalPlayer.PlayerId);
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
        Vector2 testPosition = new Vector2(400, 300); // Use specific position for testing
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
                returnTableButton.Disabled = true;
                leaveTableButton.Visible = true;
                returnTableButton.Visible = false;
                break;

            case GameManager.PlayerLocation.InKitchen:
                leaveTableButton.Disabled = true;
                returnTableButton.Disabled = false;
                leaveTableButton.Visible = false;
                returnTableButton.Visible = true;
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

        // DEBUG: Log PlayerHand container properties
        if (playerHand != null)
        {
            GD.Print($"DEBUG: PlayerHand container properties:");
            GD.Print($"DEBUG: - Size: {playerHand.Size}");
            GD.Print($"DEBUG: - Position: {playerHand.Position}");
            GD.Print($"DEBUG: - OffsetLeft: {playerHand.OffsetLeft}, OffsetTop: {playerHand.OffsetTop}");
            GD.Print($"DEBUG: - OffsetRight: {playerHand.OffsetRight}, OffsetBottom: {playerHand.OffsetBottom}");
            GD.Print($"DEBUG: - AnchorLeft: {playerHand.AnchorLeft}, AnchorTop: {playerHand.AnchorTop}");
            GD.Print($"DEBUG: - AnchorRight: {playerHand.AnchorRight}, AnchorBottom: {playerHand.AnchorBottom}");
        }
        else
        {
            GD.PrintErr("DEBUG: PlayerHand container is NULL!");
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
        playerHand.AddChild(cardContainer);
        
        // Wait one frame for container to get proper size
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        
        // FIXED: Use PlayerHand container size instead of inner container
        float cardWidth = cardSize.X;
        float cardHeight = cardSize.Y;
        float overlapSpacing = -60; // Tighter overlap - cards overlap by about half for fan effect
        
        // Use the PlayerHand container's actual size (which we know has proper dimensions)
        Vector2 availableSize = playerHand.Size;
        
        GD.Print($"DEBUG: Manual positioning setup:");
        GD.Print($"DEBUG: - Card size: {cardSize}");
        GD.Print($"DEBUG: - PlayerHand container size: {availableSize}");
        GD.Print($"DEBUG: - Card container size: {cardContainer.Size}");
        GD.Print($"DEBUG: - Overlap spacing: {overlapSpacing}");
        
        if (currentPlayerCards.Count <= 7)
        {
            // Single row - manually positioned at bottom center
            float totalWidthNeeded = (currentPlayerCards.Count - 1) * overlapSpacing + cardWidth;
            float startX = (availableSize.X - totalWidthNeeded) / 2; // Center horizontally
            float cardY = availableSize.Y - cardHeight - 10; // 10px from bottom
            
            GD.Print($"DEBUG: Single row positioning:");
            GD.Print($"DEBUG: - Total width needed: {totalWidthNeeded}");
            GD.Print($"DEBUG: - Start X: {startX}, Card Y: {cardY}");

            for (int i = 0; i < currentPlayerCards.Count; i++)
            {
                var card = currentPlayerCards[i];
                var cardButton = CreateCardButton(card, i);
                cardButtons.Add(cardButton);
                
                // DIRECT POSITION CONTROL - bypassing all container constraints
                Vector2 cardPos = new Vector2(startX + i * overlapSpacing, cardY);
                cardButton.Position = cardPos;
                cardContainer.AddChild(cardButton);
                
                GD.Print($"DEBUG: Card {i} ({card}) positioned at: {cardPos}");
            }

            GD.Print($"CardGameUI: Created {cardButtons.Count} manually positioned cards in single row");
        }
        else
        {
            // Two-row layout - manually positioned
            int cardsPerRow = (currentPlayerCards.Count + 1) / 2;
            float totalWidthNeeded = (cardsPerRow - 1) * overlapSpacing + cardWidth;
            float startX = (availableSize.X - totalWidthNeeded) / 2;
            float topRowY = availableSize.Y - cardHeight * 2 - 20; // Leave space for 2 rows
            float bottomRowY = availableSize.Y - cardHeight - 10;
            
            GD.Print($"DEBUG: Two row positioning:");
            GD.Print($"DEBUG: - Cards per row: {cardsPerRow}");
            GD.Print($"DEBUG: - Top row Y: {topRowY}, Bottom row Y: {bottomRowY}");

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
                
                GD.Print($"DEBUG: Card {i} ({card}) positioned at: {cardPos}");
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

        // Set size (scaled up as requested)
        cardButton.Size = cardSize;
        cardButton.CustomMinimumSize = cardSize;

        // Configure texture scaling - Simplified for Godot 4.4 API
        cardButton.IgnoreTextureSize = true; // Allow custom sizing

        // DEBUG: Log the actual card button properties
        GD.Print($"DEBUG: Created card button for {card}");
        GD.Print($"DEBUG: - Size set to: {cardSize}");
        GD.Print($"DEBUG: - Actual Size after creation: {cardButton.Size}");
        GD.Print($"DEBUG: - CustomMinimumSize: {cardButton.CustomMinimumSize}");
        GD.Print($"DEBUG: - IgnoreTextureSize: {cardButton.IgnoreTextureSize}");

        // Store card index in button metadata
        cardButton.SetMeta("card_index", index);

        // Add tooltip showing card name
        cardButton.TooltipText = card.ToString();

        // Connect click event
        cardButton.Pressed += () => OnCardClicked(cardButton);

        return cardButton;
    }

    /// <summary>
    /// Create a fallback texture if card graphics fail to load - FIXED for Godot 4.4 API
    /// </summary>
    private Texture2D CreateFallbackCardTexture(Card card)
    {
        // Create a simple ImageTexture as fallback - FIXED: Use CreateEmpty instead of Create
        var image = Image.CreateEmpty(140, 190, false, Image.Format.Rgb8);
        
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

        // Clear trick area
        foreach (Node child in trickArea.GetChildren())
        {
            child.QueueFree();
        }

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

        GD.Print($"CardGameUI: === CHAT PANEL GROWTH DEBUG ===");
        GD.Print($"CardGameUI: Current chat panel size: {chatPanel.Size}");
        GD.Print($"CardGameUI: Current chat panel position: {chatPanel.Position}");
        GD.Print($"CardGameUI: Base size for calculation: {baseSize}");
        GD.Print($"CardGameUI: Multiplier: {multiplier}");
        GD.Print($"CardGameUI: Target size: {newSize}");

        // Calculate the bottom-right corner position (this should stay fixed)
        Vector2 currentBottomRight = chatPanel.Position + chatPanel.Size;
        GD.Print($"CardGameUI: Current bottom-right corner: {currentBottomRight}");

        // Calculate new position to keep bottom-right corner fixed
        Vector2 newPosition = currentBottomRight - newSize;
        GD.Print($"CardGameUI: New position to keep bottom-right fixed: {newPosition}");

        // Animate both size and position changes
        var tween = CreateTween();
        if (tween != null)
        {
            GD.Print("CardGameUI: Starting tween animation for size AND position...");

            // Animate both size and position in parallel
            tween.Parallel().TweenProperty(chatPanel, "size", newSize, 0.5f);
            tween.Parallel().TweenProperty(chatPanel, "position", newPosition, 0.5f);

            GD.Print("CardGameUI: Tween animation started for chat panel resize with position adjustment");

            // Add completion callback to confirm resize
            tween.TweenCallback(Callable.From(() =>
            {
                GD.Print($"CardGameUI: === TWEEN COMPLETED ===");
                GD.Print($"CardGameUI: Final chat panel size: {chatPanel.Size}");
                GD.Print($"CardGameUI: Final chat panel position: {chatPanel.Position}");
                Vector2 finalBottomRight = chatPanel.Position + chatPanel.Size;
                GD.Print($"CardGameUI: Final bottom-right corner: {finalBottomRight}");
                GD.Print($"CardGameUI: Bottom-right corner moved by: {finalBottomRight - currentBottomRight}");
            }));
        }
        else
        {
            // Fallback: set size and position directly if tween fails
            GD.Print("CardGameUI: Tween failed, setting chat panel size and position directly");
            chatPanel.Size = newSize;
            chatPanel.Position = newPosition;
            GD.Print($"CardGameUI: Size set directly to: {chatPanel.Size}");
            GD.Print($"CardGameUI: Position set directly to: {chatPanel.Position}");
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

        // Create egg splat visual
        var splatPanel = new Panel();
        splatPanel.Name = "EggSplat"; // Set name first

        // CRITICAL: Add metadata to identify as egg splat (names can get auto-generated by Godot)
        splatPanel.SetMeta("IsEggSplat", true);
        splatPanel.SetMeta("CreatedAt", Time.GetUnixTimeFromSystem());

        GD.Print($"DEBUG: Created panel with name '{splatPanel.Name}' and EggSplat metadata");

        // Calculate size based on coverage - MADE 15X LARGER TOTAL (was 200, now 3000)
        float baseSize = 3000.0f; // 15x larger than original 200px
        Vector2 splatSize = new Vector2(baseSize * coverage, baseSize * coverage);
        splatPanel.Size = splatSize;

        // Position randomly around screen center if no specific position given
        Vector2 splatPosition = position == Vector2.Zero ?
            new Vector2(GetViewport().GetVisibleRect().Size.X * 0.5f - splatSize.X * 0.5f,
                       GetViewport().GetVisibleRect().Size.Y * 0.5f - splatSize.Y * 0.5f) :
            position - splatSize * 0.5f;

        splatPanel.Position = splatPosition;

        // Style the splat (yellow/orange color with transparency)
        var styleBox = new StyleBoxFlat();
        styleBox.BgColor = new Color(1.0f, 0.8f, 0.2f, 0.7f); // Yellow with transparency
        styleBox.CornerRadiusTopLeft = 10;
        styleBox.CornerRadiusTopRight = 15;
        styleBox.CornerRadiusBottomLeft = 12;
        styleBox.CornerRadiusBottomRight = 8;
        splatPanel.AddThemeStyleboxOverride("panel", styleBox);

        // Add to overlay layer
        overlayLayer.AddChild(splatPanel);

        // Verify name after adding to scene tree
        GD.Print($"DEBUG: After adding to scene tree, panel name is: '{splatPanel.Name}'");
        GD.Print($"DEBUG: Panel metadata IsEggSplat: {splatPanel.GetMeta("IsEggSplat", false)}");

        // Add to tracking
        int localPlayerId = gameManager?.LocalPlayer?.PlayerId ?? 0;
        if (!playerVisualOverlays.ContainsKey(localPlayerId))
        {
            playerVisualOverlays[localPlayerId] = new List<Control>();
        }
        playerVisualOverlays[localPlayerId].Add(splatPanel);

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
        GD.Print($"DEBUG: ========== STARTING COMPREHENSIVE CLEANING OF {sabotageType} ==========");

        int localPlayerId = gameManager?.LocalPlayer?.PlayerId ?? 0;
        GD.Print($"DEBUG: Local player ID: {localPlayerId}");

        var allRemovedEffects = new List<string>();
        int totalFoundEffects = 0;

        // STEP 1: Clean tracked overlays with SAFE iteration
        if (playerVisualOverlays.ContainsKey(localPlayerId))
        {
            var overlaysList = playerVisualOverlays[localPlayerId];
            GD.Print($"DEBUG: Found {overlaysList.Count} tracked visual overlays for player {localPlayerId}");

            // Create a COPY of the list to safely iterate (avoid collection modification during iteration)
            var overlaysListCopy = new List<Control>(overlaysList);
            GD.Print($"DEBUG: Created copy of {overlaysListCopy.Count} tracked overlays for safe iteration");

            foreach (var overlay in overlaysListCopy)
            {
                if (overlay != null && IsInstanceValid(overlay))
                {
                    string overlayName = overlay.Name;

                    // Check both name AND metadata for identification
                    bool isEggSplat = (overlayName == "EggSplat") || overlay.GetMeta("IsEggSplat", false).AsBool();
                    bool isStinkFog = (overlayName == "StinkFog") || overlay.GetMeta("IsStinkFog", false).AsBool();

                    GD.Print($"DEBUG: Checking tracked overlay: '{overlayName}' - IsEggSplat: {isEggSplat}, IsStinkFog: {isStinkFog}");

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
                        GD.Print($"DEBUG: Removing tracked {overlayName} overlay (identified as {sabotageType})");

                        // Remove from parent and tracking FIRST, then free
                        if (overlay.GetParent() != null)
                        {
                            overlay.GetParent().RemoveChild(overlay);
                        }
                        playerVisualOverlays[localPlayerId].Remove(overlay);
                        overlay.QueueFree(); // Use QueueFree for safer removal

                        allRemovedEffects.Add($"tracked {overlayName}");
                        GD.Print($"DEBUG: Successfully removed tracked {overlayName}");
                    }
                }
                else
                {
                    GD.Print($"DEBUG: Skipping invalid/disposed tracked overlay");
                    // Clean up invalid references from tracking
                    if (playerVisualOverlays[localPlayerId].Contains(overlay))
                    {
                        playerVisualOverlays[localPlayerId].Remove(overlay);
                        GD.Print($"DEBUG: Cleaned up invalid reference from tracking");
                    }
                }
            }

            GD.Print($"DEBUG: Tracked cleaning complete. Remaining tracked overlays: {playerVisualOverlays[localPlayerId].Count}");
        }
        else
        {
            GD.Print("DEBUG: No tracked visual overlays dictionary entry for local player");
            GD.Print($"DEBUG: Available player overlay keys: {string.Join(", ", playerVisualOverlays.Keys)}");
        }

        // STEP 2: COMPREHENSIVE CLEANUP - Search entire overlay layer multiple times until no more found
        GD.Print("DEBUG: Starting comprehensive cleanup - searching entire overlay layer");

        int searchRounds = 0;
        bool foundEffectsThisRound = true;

        while (foundEffectsThisRound && searchRounds < 10) // Max 10 rounds to prevent infinite loop
        {
            searchRounds++;
            foundEffectsThisRound = false;
            GD.Print($"DEBUG: === SEARCH ROUND {searchRounds} ===");

            if (overlayLayer != null)
            {
                var allChildren = overlayLayer.GetChildren();
                GD.Print($"DEBUG: Found {allChildren.Count} total children in overlay layer");

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

                        GD.Print($"DEBUG: Checking overlay layer child: '{childName}' (Type: {child.GetType().Name}) - IsEggSplat: {isEggSplat}, IsStinkFog: {isStinkFog}");

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
                            GD.Print($"DEBUG: Found and removing {childName} (identified as {sabotageType})!");

                            // Safe removal
                            if (child.GetParent() != null)
                            {
                                child.GetParent().RemoveChild(child);
                            }
                            child.QueueFree();

                            allRemovedEffects.Add($"untracked {childName} (round {searchRounds})");
                            GD.Print($"DEBUG: Successfully removed untracked {childName}");
                        }
                    }
                    else
                    {
                        GD.Print($"DEBUG: Skipping invalid/disposed overlay layer child");
                    }
                }

                if (foundEffectsThisRound)
                {
                    GD.Print($"DEBUG: Found effects in round {searchRounds}, will search again...");
                }
                else
                {
                    GD.Print($"DEBUG: No more effects found in round {searchRounds}, cleanup complete!");
                }
            }
            else
            {
                GD.PrintErr("DEBUG: Overlay layer is null - cannot perform comprehensive cleanup");
                break;
            }
        }

        GD.Print($"DEBUG: COMPREHENSIVE ROBUST cleaning complete for {sabotageType}!");
        GD.Print($"DEBUG: Total effects found and removed: {totalFoundEffects}");
        GD.Print($"DEBUG: Search rounds completed: {searchRounds}");
        GD.Print($"DEBUG: Detailed removal list: {string.Join(", ", allRemovedEffects)}");

        // STEP 3: Final verification after a short delay to let QueueFree take effect
        GetTree().CreateTimer(0.1).Timeout += () =>
        {
            VerifyCleanupComplete(sabotageType);
        };

        GD.Print($"DEBUG: ========== CLEANING COMPLETE FOR {sabotageType} ==========");
    }

    /// <summary>
    /// Verify that cleanup was successful
    /// </summary>
    private void VerifyCleanupComplete(SabotageType sabotageType)
    {
        GD.Print($"DEBUG: === VERIFYING CLEANUP COMPLETE FOR {sabotageType} ===");

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
                GD.PrintErr($"DEBUG: ⚠️  WARNING - {remainingTargetEffects.Count} {sabotageType} effects still remain: {string.Join(", ", remainingTargetEffects)}");
                GD.PrintErr($"DEBUG: Cleanup may have failed or new effects were created during cleanup");
            }
            else
            {
                GD.Print($"DEBUG: ✅ SUCCESS - All {sabotageType} effects completely removed!");
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
}

