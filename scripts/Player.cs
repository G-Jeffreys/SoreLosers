using Godot;
using System;

/// <summary>
/// Player controller for the real-time phase of the game
/// Handles top-down movement, interaction with items, and sabotage actions
/// Movement speed is affected by player stats from PlayerData
/// </summary>
public partial class Player : CharacterBody2D
{
    // Sprite direction system for character sprite sheet
    public enum FacingDirection
    {
        Up = 0,    // Top row of sprite sheet
        Right = 1, // Second row of sprite sheet
        Down = 2,  // Third row of sprite sheet
        Left = 3   // Bottom row of sprite sheet
    }

    // Sprite system references
    private AnimatedSprite2D playerSprite;
    private FacingDirection currentFacing = FacingDirection.Up; // Default to facing up

    // Sprite sheet configuration
    [Export]
    public int SpriteFramesPerDirection = 4; // Number of animation frames per direction
    [Export]
    public float AnimationSpeed = 8.0f; // Animation playback speed

    // Player data reference
    public PlayerData PlayerData { get; private set; }

    // Movement configuration (overridden by player stats)
    [Export]
    public float BaseSpeed = 80.0f; // Reduced from 110.0f for slower movement

    // Current movement speed (calculated from player stats)
    public float CurrentSpeed { get; private set; } = 80.0f;

    // Interaction system
    [Export]
    public float InteractionRadius = 75.0f; // Radius for item interaction

    // Inventory system
    public int EggsInInventory { get; private set; } = 0;
    public bool HasStinkBomb { get; private set; } = false;

    // Visual indicators
    [Export]
    public bool ShowDebugInfo = true;

    // UI reference for inventory updates
    private Label inventoryLabel;

    // Movement XP tracking
    private Vector2 lastPosition = Vector2.Zero;
    private float accumulatedDistance = 0.0f;

    [Export]
    public float XPDistanceThreshold = 500.0f; // Award XP per this many pixels moved

    [Export]
    public int MovementXPReward = 10; // Increased 10x to balance slower movement speed

    // Events
    [Signal]
    public delegate void PlayerMovedEventHandler(Vector2 newPosition);

    [Signal]
    public delegate void ItemInteractedEventHandler(string itemType);

    [Signal]
    public delegate void SabotageUsedEventHandler(SabotageType sabotageType);

    [Signal]
    public delegate void InventoryUpdatedEventHandler(int eggs, bool hasStinkBomb);

    public override void _Ready()
    {
        GD.Print("Player: Initializing player controller...");

        // Get inventory label reference - with error handling
        try
        {
            inventoryLabel = GetNode<Label>("../KitchenUI/InventoryLabel");
            if (inventoryLabel != null)
            {
                GD.Print("Player: Successfully found inventory label");
            }
            else
            {
                GD.PrintErr("Player: Inventory label found but is null");
            }
        }
        catch (System.Exception e)
        {
            GD.PrintErr($"Player: Failed to find inventory label: {e.Message}");
            // Try alternative path - maybe the structure is different
            try
            {
                inventoryLabel = GetNode<Label>("../../KitchenView/KitchenUI/InventoryLabel");
                GD.Print("Player: Found inventory label using alternative path");
            }
            catch
            {
                GD.PrintErr("Player: Alternative path also failed - inventory updates will not work");
            }
        }

        // Get player data from GameManager
        if (GameManager.Instance != null)
        {
            PlayerData = GameManager.Instance.LocalPlayer;
            if (PlayerData != null)
            {
                UpdateMovementSpeed();
                GD.Print($"Player: Loaded player data for {PlayerData.PlayerName}");
            }
        }
        else
        {
            GD.PrintErr("Player: GameManager instance not found!");
        }

        // Update inventory display
        UpdateInventoryDisplay();

        // Initialize movement tracking
        lastPosition = GlobalPosition;

        // Initialize sprite system
        InitializePlayerSprite();

        GD.Print($"Player: Player controller initialized with speed {CurrentSpeed}");
    }

    /// <summary>
    /// Update movement speed based on player stats
    /// </summary>
    private void UpdateMovementSpeed()
    {
        if (PlayerData != null)
        {
            CurrentSpeed = PlayerData.GetMovementSpeed();
            GD.Print($"Player: Movement speed updated to {CurrentSpeed} px/s (Level {PlayerData.MoveSpeed})");
        }
        else
        {
            CurrentSpeed = BaseSpeed;
            GD.Print($"Player: Using base movement speed {BaseSpeed} px/s");
        }
    }

    /// <summary>
    /// Initialize the player sprite system with sprite sheet texture and animations
    /// </summary>
    private void InitializePlayerSprite()
    {
        // Get reference to the AnimatedSprite2D node
        playerSprite = GetNode<AnimatedSprite2D>("PlayerSprite");
        if (playerSprite == null)
        {
            GD.PrintErr("Player: Failed to find PlayerSprite (AnimatedSprite2D) node!");
            return;
        }

        GD.Print("Player: Setting up sprite sheet system...");

        // Load the sprite sheet texture
        var spriteTexture = GD.Load<Texture2D>("res://assets/environment/players/Blonde_guy_with_eypa...-694987079-0.png");
        if (spriteTexture == null)
        {
            GD.PrintErr("Player: Failed to load sprite sheet texture!");
            return;
        }

        // Create SpriteFrames resource for AnimatedSprite2D
        var spriteFrames = new SpriteFrames();

        // Calculate frame dimensions (assuming 4 directions with multiple frames each)
        int textureWidth = spriteTexture.GetWidth();
        int textureHeight = spriteTexture.GetHeight();
        int frameWidth = textureWidth / SpriteFramesPerDirection; // Assuming 4 frames per row
        int frameHeight = textureHeight / 4; // Assuming 4 rows (up, down, left, right)

        GD.Print($"Player: Sprite sheet dimensions: {textureWidth}x{textureHeight}");
        GD.Print($"Player: Frame dimensions: {frameWidth}x{frameHeight}");
        GD.Print($"Player: Setting up {SpriteFramesPerDirection} frames per direction");

        // Create animations for each direction
        foreach (FacingDirection direction in System.Enum.GetValues<FacingDirection>())
        {
            string animationName = direction.ToString().ToLower();
            spriteFrames.AddAnimation(animationName);
            spriteFrames.SetAnimationSpeed(animationName, AnimationSpeed);
            spriteFrames.SetAnimationLoop(animationName, true);

            int row = (int)direction;
            GD.Print($"Player: Creating animation '{animationName}' for row {row}");

            // Add frames for this direction
            for (int frame = 0; frame < SpriteFramesPerDirection; frame++)
            {
                // Create AtlasTexture for this frame
                var atlasTexture = new AtlasTexture();
                atlasTexture.Atlas = spriteTexture;
                atlasTexture.Region = new Rect2(
                    frame * frameWidth,     // X position in sprite sheet
                    row * frameHeight,      // Y position in sprite sheet
                    frameWidth,             // Frame width
                    frameHeight             // Frame height
                );

                spriteFrames.AddFrame(animationName, atlasTexture);
                GD.Print($"Player: Added frame {frame} for {animationName} at region ({frame * frameWidth}, {row * frameHeight}, {frameWidth}, {frameHeight})");
            }
        }

        // Apply the sprite frames to the AnimatedSprite2D
        playerSprite.SpriteFrames = spriteFrames;

        // Set initial animation to facing up and make sure it's visible
        SetSpriteDirection(FacingDirection.Up);

        // Ensure the sprite is visible and playing the idle animation
        playerSprite.Play();

        GD.Print("Player: Sprite sheet system initialized successfully!");
    }

    /// <summary>
    /// Set the sprite direction and play appropriate animation
    /// </summary>
    /// <param name="direction">Direction the player is facing</param>
    private void SetSpriteDirection(FacingDirection direction)
    {
        if (playerSprite == null) return;

        // Always update direction and ensure sprite is playing
        currentFacing = direction;
        string animationName = direction.ToString().ToLower();

        GD.Print($"Player: Setting sprite direction to {direction} (animation: {animationName})");

        playerSprite.Animation = animationName;
        playerSprite.Play(); // Always play to ensure visibility
    }

    /// <summary>
    /// Update sprite direction based on movement input
    /// </summary>
    /// <param name="inputDirection">Current movement input vector</param>
    private void UpdateSpriteDirection(Vector2 inputDirection)
    {
        if (inputDirection == Vector2.Zero) return; // Don't change direction when not moving

        // Determine primary direction based on input (prioritize vertical movement)
        if (Mathf.Abs(inputDirection.Y) > Mathf.Abs(inputDirection.X))
        {
            // Vertical movement takes priority
            if (inputDirection.Y < 0) // Moving up
            {
                SetSpriteDirection(FacingDirection.Up);
            }
            else // Moving down
            {
                SetSpriteDirection(FacingDirection.Down);
            }
        }
        else
        {
            // Horizontal movement
            if (inputDirection.X < 0) // Moving left
            {
                SetSpriteDirection(FacingDirection.Left);
            }
            else // Moving right
            {
                SetSpriteDirection(FacingDirection.Right);
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleMovement(delta);
        HandleInteraction();
        HandleSabotageActions();
    }

    /// <summary>
    /// Handle arrow key movement input for top-down gameplay
    /// </summary>
    /// <param name="delta">Frame delta time</param>
    private void HandleMovement(double delta)
    {
        Vector2 velocity = Velocity;

        // Get input direction (arrow keys)
        Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        // Apply movement based on input
        if (inputDirection != Vector2.Zero)
        {
            velocity = inputDirection * CurrentSpeed;

            // Update sprite direction based on movement
            UpdateSpriteDirection(inputDirection);

            // Reset animation speed to normal when moving
            if (playerSprite != null)
            {
                playerSprite.SpeedScale = 1.0f; // Normal animation speed
            }

            // Emit movement signal for networking
            EmitSignal(SignalName.PlayerMoved, GlobalPosition);
        }
        else
        {
            // Apply friction when no input
            velocity = velocity.MoveToward(Vector2.Zero, CurrentSpeed * 3.0f * (float)delta);

            // Keep sprite visible but slow down animation when not moving
            if (playerSprite != null)
            {
                // Keep the sprite playing but at a slower speed for idle animation
                if (playerSprite.IsPlaying())
                {
                    playerSprite.SpeedScale = 0.3f; // Slow idle animation
                }
            }
        }

        // Apply movement
        Velocity = velocity;
        MoveAndSlide();

        // Track movement for XP (only in kitchen)
        TrackMovementForXP();

        // Debug visualization
        if (ShowDebugInfo)
        {
            DebugDrawMovement();
        }
    }

    /// <summary>
    /// Track movement distance and award MoveSpeed XP for natural movement
    /// Only awards XP when player is in kitchen phase to encourage exploration
    /// </summary>
    private void TrackMovementForXP()
    {
        // Only track movement when player is in kitchen
        if (GameManager.Instance != null && PlayerData != null)
        {
            var playerLocation = GameManager.Instance.GetPlayerLocation(PlayerData.PlayerId);
            if (playerLocation != GameManager.PlayerLocation.InKitchen)
            {
                return; // Don't track movement at card table
            }
        }
        else
        {
            return; // Can't determine location, skip XP tracking
        }

        // Calculate distance moved this frame
        Vector2 currentPosition = GlobalPosition;
        float distanceMoved = lastPosition.DistanceTo(currentPosition);

        // Accumulate distance
        accumulatedDistance += distanceMoved;

        // Award XP when threshold is reached
        if (accumulatedDistance >= XPDistanceThreshold)
        {
            if (PlayerData != null)
            {
                bool leveledUp = PlayerData.AddMoveSpeedXP(MovementXPReward);

                GD.Print($"Player: Awarded {MovementXPReward} MoveSpeed XP for moving {accumulatedDistance:F0} pixels");

                if (leveledUp)
                {
                    GD.Print($"Player: MoveSpeed leveled up to {PlayerData.MoveSpeed}!");
                    UpdateMovementSpeed(); // Update speed immediately after level up
                }

                // 🔥 NEW: Notify UI to refresh stats when movement XP is gained
                var cardGameUI = GetTree().GetFirstNodeInGroup("card_game_ui") as CardGameUI;
                if (cardGameUI != null)
                {
                    GD.Print($"🏃 Movement XP - refreshing stats");
                    cardGameUI.ForceStatsRefresh();
                }
            }

            // Reset distance counter
            accumulatedDistance = 0.0f;
        }

        // Update last position for next frame
        lastPosition = currentPosition;
    }

    /// <summary>
    /// Handle interaction with items (Space key) - only in kitchen phase
    /// </summary>
    private void HandleInteraction()
    {
        if (Input.IsActionJustPressed("ui_accept")) // Space key
        {
            // Check if player is in kitchen phase
            if (GameManager.Instance != null && PlayerData != null)
            {
                var playerLocation = GameManager.Instance.GetPlayerLocation(PlayerData.PlayerId);

                if (playerLocation == GameManager.PlayerLocation.InKitchen)
                {
                    GD.Print("Player: Space pressed in kitchen - checking for nearby items");
                    CheckForNearbyItemsPhysics();
                }
                else if (playerLocation == GameManager.PlayerLocation.AtTable)
                {
                    GD.Print("Player: Space pressed at table - throwing egg at active player");
                    ThrowEggAtActivePlayer();
                }
            }
        }
    }

    /// <summary>
    /// Handle sabotage actions (mouse clicks, additional keys)
    /// </summary>
    private void HandleSabotageActions()
    {
        // Right click - drop stink bomb
        if (Input.IsActionJustPressed("ui_cancel") && HasStinkBomb)
        {
            DropStinkBomb();
        }
    }

    /// <summary>
    /// Check for nearby items using physics queries - this replaces the old simulation
    /// </summary>
    private void CheckForNearbyItemsPhysics()
    {
        var spaceState = GetWorld2D().DirectSpaceState;

        // Create a physics query to find objects within interaction radius
        var query = new PhysicsShapeQueryParameters2D();

        // Create a circle shape for the interaction area
        var circleShape = new CircleShape2D();
        circleShape.Radius = InteractionRadius;
        query.Shape = circleShape;
        query.Transform = new Transform2D(0, GlobalPosition);
        query.CollisionMask = 1; // Default physics layer

        // Execute the query
        var results = spaceState.IntersectShape(query);

        GD.Print($"Player: Found {results.Count} potential interaction targets");

        foreach (var result in results)
        {
            var collider = result["collider"].AsGodotObject();

            if (collider is StaticBody2D body)
            {
                string objectName = body.Name;
                GD.Print($"Player: Found interactive object: {objectName}");

                switch (objectName)
                {
                    case "EggTray":
                        int maxEggs = PlayerData?.GetMaxEggsInInventory() ?? 3; // Level-based max eggs or fallback to 3
                        if (EggsInInventory < maxEggs)
                        {
                            PickupEgg();
                        }
                        else
                        {
                            GD.Print($"Player: Cannot pick up egg - inventory full ({EggsInInventory}/{maxEggs})");
                        }
                        break;

                    case "Sink":
                        CleanAtSink();
                        break;

                    case "CardTable":
                        ReturnToCardTable();
                        break;

                    default:
                        GD.Print($"Player: Unknown interactive object: {objectName}");
                        break;
                }
            }
        }

        if (results.Count == 0)
        {
            GD.Print("Player: No interactive objects within range");
        }
    }

    /// <summary>
    /// Pick up an egg from egg tray
    /// </summary>
    private void PickupEgg()
    {
        int maxEggs = PlayerData?.GetMaxEggsInInventory() ?? 3; // Level-based max eggs or fallback to 3
        if (EggsInInventory < maxEggs)
        {
            EggsInInventory++;
            GD.Print($"Player: Picked up egg! Total eggs: {EggsInInventory}/{maxEggs} (ThrowPower Level {PlayerData?.ThrowPower ?? 1})");

            // Update inventory display
            UpdateInventoryDisplay();

            // Emit signals for tracking
            EmitSignal(SignalName.ItemInteracted, "egg");
            EmitSignal(SignalName.InventoryUpdated, EggsInInventory, HasStinkBomb);
        }
        else
        {
            GD.Print($"Player: Cannot pick up egg - inventory full ({EggsInInventory}/{maxEggs})");
        }
    }

    /// <summary>
    /// Pick up a stink bomb
    /// </summary>
    private void PickupStinkBomb()
    {
        if (!HasStinkBomb)
        {
            HasStinkBomb = true;
            GD.Print("Player: Picked up stink bomb");

            // Update inventory display
            UpdateInventoryDisplay();

            // Emit signals for tracking
            EmitSignal(SignalName.ItemInteracted, "stink_bomb");
            EmitSignal(SignalName.InventoryUpdated, EggsInInventory, HasStinkBomb);
        }
        else
        {
            GD.Print("Player: Cannot pick up stink bomb - already have one");
        }
    }

    /// <summary>
    /// Clean egg effects at sink
    /// </summary>
    private void CleanAtSink()
    {
        GD.Print("Player: Washing at sink - cleaning egg effects");

        // Connect with SabotageManager to remove egg effects
        if (GameManager.Instance?.SabotageManager != null && PlayerData != null)
        {
            GameManager.Instance.SabotageManager.CleanEggEffect(PlayerData.PlayerId);
        }

        // Emit signal for tracking
        EmitSignal(SignalName.ItemInteracted, "sink");
    }

    /// <summary>
    /// Return to card table - rejoins the card game from kitchen
    /// </summary>
    private void ReturnToCardTable()
    {
        GD.Print("Player: Interacting with card table - returning to card game");

        // Use GameManager to return player to table
        if (GameManager.Instance != null && PlayerData != null)
        {
            GameManager.Instance.PlayerReturnToTable(PlayerData.PlayerId);
            GD.Print($"Player: Player {PlayerData.PlayerId} returned to card table");
        }
        else
        {
            GD.PrintErr("Player: Cannot return to table - GameManager or PlayerData is null");
        }

        // Emit signal for tracking
        EmitSignal(SignalName.ItemInteracted, "card_table");
    }

    /// <summary>
    /// Update the inventory display UI
    /// </summary>
    private void UpdateInventoryDisplay()
    {
        GD.Print($"Player: UpdateInventoryDisplay called - Eggs: {EggsInInventory}, Stink Bomb: {HasStinkBomb}");

        if (inventoryLabel != null)
        {
            var items = new System.Collections.Generic.List<string>();
            int maxEggs = PlayerData?.GetMaxEggsInInventory() ?? 3; // Level-based max eggs or fallback to 3
            if (EggsInInventory > 0) items.Add($"Eggs: {EggsInInventory}/{maxEggs}");
            if (HasStinkBomb) items.Add("Stink Bomb");

            string newText = items.Count > 0 ?
                $"Inventory: {string.Join(", ", items)}" :
                "Inventory: Empty";

            inventoryLabel.Text = newText;

            GD.Print($"Player: Inventory display updated successfully - {newText}");
        }
        else
        {
            GD.PrintErr("Player: Cannot update inventory display - inventoryLabel is null!");

            // Try to find the label again
            try
            {
                inventoryLabel = GetNode<Label>("../KitchenUI/InventoryLabel");
                if (inventoryLabel != null)
                {
                    GD.Print("Player: Successfully re-found inventory label, trying update again");
                    UpdateInventoryDisplay(); // Recursive call now that we found it
                    return;
                }
            }
            catch
            {
                GD.PrintErr("Player: Failed to re-find inventory label");
            }
        }
    }

    /// <summary>
    /// Throw an egg at target location
    /// </summary>
    private void ThrowEgg()
    {
        if (EggsInInventory > 0)
        {
            EggsInInventory--;

            // Get mouse position for targeting
            Vector2 mousePosition = GetGlobalMousePosition();
            Vector2 throwDirection = (mousePosition - GlobalPosition).Normalized();

            GD.Print($"Player: Throwing egg towards {mousePosition}. Eggs remaining: {EggsInInventory}");

            // Update inventory display
            UpdateInventoryDisplay();

            // TODO: Create egg projectile
            // TODO: Apply egg effect on impact

            EmitSignal(SignalName.SabotageUsed, (int)SabotageType.EggThrow);
            EmitSignal(SignalName.InventoryUpdated, EggsInInventory, HasStinkBomb);

            // Add XP for sabotage
            if (PlayerData != null)
            {
                PlayerData.AddSabotageXP(SabotageType.EggThrow);
                UpdateMovementSpeed(); // Update in case of level up
            }
        }
        else
        {
            GD.Print("Player: No eggs to throw");
        }
    }

    /// <summary>
    /// Throw an egg at the active player (whose turn it is) - only affects human players
    /// </summary>
    private void ThrowEggAtActivePlayer()
    {
        if (EggsInInventory <= 0)
        {
            GD.Print("Player: No eggs to throw at active player");
            return;
        }

        // Get the active player from CardManager
        if (GameManager.Instance?.CardManager == null)
        {
            GD.PrintErr("Player: Cannot throw egg - CardManager not available");
            return;
        }

        var gameState = GameManager.Instance.CardManager.GetGameState();
        if (!gameState.GameInProgress)
        {
            GD.Print("Player: Cannot throw egg - no game in progress");
            return;
        }

        int activePlayerId = gameState.CurrentPlayerTurn;

        // Check if target is AI player (no effect on AI)
        if (GameManager.Instance.ConnectedPlayers.ContainsKey(activePlayerId))
        {
            var targetPlayerData = GameManager.Instance.ConnectedPlayers[activePlayerId];
            if (targetPlayerData.PlayerName.StartsWith("AI_"))
            {
                GD.Print($"Player: Egg thrown at AI player {activePlayerId} - no effect");
                // Still consume the egg but no effect
                EggsInInventory--;
                UpdateInventoryDisplay();
                return;
            }
        }

        // Throw egg at human player
        EggsInInventory--;
        GD.Print($"Player: Throwing egg at active player {activePlayerId}. Eggs remaining: {EggsInInventory}");

        // Update inventory display
        UpdateInventoryDisplay();

        // Apply egg effect to target player
        if (GameManager.Instance.SabotageManager != null && PlayerData != null)
        {
            // Calculate random position using 3x2 grid system (6 sections total)
            Vector2 targetPosition = CalculateRandomGridPosition();
            GameManager.Instance.SabotageManager.ApplyEggThrow(PlayerData.PlayerId, activePlayerId, targetPosition);
            GD.Print($"Player: Applied egg throw effect from player {PlayerData.PlayerId} to player {activePlayerId} at position {targetPosition}");
        }

        // Emit signals for tracking
        EmitSignal(SignalName.SabotageUsed, (int)SabotageType.EggThrow);
        EmitSignal(SignalName.InventoryUpdated, EggsInInventory, HasStinkBomb);

        // Add XP for sabotage
        if (PlayerData != null)
        {
            PlayerData.AddSabotageXP(SabotageType.EggThrow);
            UpdateMovementSpeed(); // Update in case of level up
        }
    }

    /// <summary>
    /// Drop a stink bomb at current location
    /// </summary>
    private void DropStinkBomb()
    {
        if (HasStinkBomb)
        {
            HasStinkBomb = false;

            GD.Print($"Player: Dropping stink bomb at {GlobalPosition}");

            // Update inventory display
            UpdateInventoryDisplay();

            // TODO: Create stink bomb with 0.8s timer
            // TODO: Apply stink effect to players in radius

            EmitSignal(SignalName.SabotageUsed, (int)SabotageType.StinkBomb);
            EmitSignal(SignalName.InventoryUpdated, EggsInInventory, HasStinkBomb);

            // Add XP for sabotage
            if (PlayerData != null)
            {
                PlayerData.AddSabotageXP(SabotageType.StinkBomb);
                UpdateMovementSpeed(); // Update in case of level up
            }
        }
        else
        {
            GD.Print("Player: No stink bomb to drop");
        }
    }

    /// <summary>
    /// Update player data (called when stats change)
    /// </summary>
    /// <param name="newPlayerData">Updated player data</param>
    public void UpdatePlayerData(PlayerData newPlayerData)
    {
        GD.Print($"Player: Updating player data for {newPlayerData.PlayerName}");

        PlayerData = newPlayerData;
        UpdateMovementSpeed();

        GD.Print($"Player: Player data updated successfully");
    }

    /// <summary>
    /// Get current player status for UI display
    /// </summary>
    /// <returns>Formatted status string</returns>
    public string GetPlayerStatus()
    {
        int maxEggs = PlayerData?.GetMaxEggsInInventory() ?? 3; // Level-based max eggs or fallback to 3
        return $"Speed: {CurrentSpeed:F1} px/s | Eggs: {EggsInInventory}/{maxEggs} | Stink Bomb: {(HasStinkBomb ? "Yes" : "No")}";
    }

    /// <summary>
    /// Debug visualization for movement and interaction
    /// </summary>
    private void DebugDrawMovement()
    {
        // TODO: Add debug drawing for movement direction, interaction radius, etc.
        // This would be useful for development and testing
    }

    /// <summary>
    /// Handle being hit by sabotage effects
    /// </summary>
    /// <param name="sabotageType">Type of sabotage received</param>
    /// <param name="intensity">Effect intensity</param>
    public void OnSabotageReceived(SabotageType sabotageType, float intensity)
    {
        GD.Print($"Player: Received {sabotageType} sabotage with intensity {intensity}");

        // Record the sabotage hit in player data
        if (PlayerData != null)
        {
            PlayerData.RecordSabotageHit(sabotageType);
        }

        // TODO: Apply visual effects based on sabotage type and player composure
        // TODO: Reduce effect intensity based on Composure stat

        switch (sabotageType)
        {
            case SabotageType.EggThrow:
                GD.Print("Player: Egg splat effect applied");
                break;
            case SabotageType.StinkBomb:
                GD.Print("Player: Stink bomb fog effect applied");
                break;
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
            GD.PrintErr("Player: Cannot get viewport for random position calculation");
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

        GD.Print($"Player: Random egg position - Section {randomSection} (col:{column}, row:{row}) = {randomPosition}");

        return randomPosition;
    }

    public override void _ExitTree()
    {
        GD.Print("Player: Player controller shutting down");
    }
}