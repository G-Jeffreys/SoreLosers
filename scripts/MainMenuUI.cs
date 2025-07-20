using Godot;
using System;
using System.Threading.Tasks;

/// <summary>
/// Main menu UI updated for Nakama multiplayer
/// Replaces AWS NetworkManager integration with NakamaManager
/// </summary>
public partial class MainMenuUI : Control
{
    private AcceptDialog joinDialog;
    private LineEdit roomCodeInput;
    private AcceptDialog connectionDialog;
    private Label connectionStatus;
    private NakamaManager nakama;

    public override void _Ready()
    {
        GD.Print("MainMenu loaded - Nakama version");
        CreateJoinDialog();
        CreateConnectionDialog();

        // Get NakamaManager instance (autoloaded)
        nakama = NakamaManager.Instance;
        if (nakama == null)
        {
            GD.PrintErr("MainMenuUI: NakamaManager autoload not found!");
            return;
        }

        SetupNakamaConnection();

        // MatchManager is now autoloaded, so no need to create it manually
        if (MatchManager.Instance == null)
        {
            GD.PrintErr("MainMenuUI: MatchManager autoload not found!");
        }
        else
        {
            GD.Print("MainMenuUI: MatchManager autoload found successfully");
        }
    }

    /// <summary>
    /// Setup Nakama connection and events (called deferred if needed)
    /// </summary>
    private void SetupNakamaConnection()
    {
        // Get the NakamaManager instance (should be available now)
        nakama = NakamaManager.Instance;

        // Connect to Nakama events
        if (nakama != null)
        {
            nakama.OnAuthenticated += OnAuthenticated;
            nakama.OnAuthenticationFailed += OnAuthenticationFailed;
            nakama.OnConnected += OnNakamaConnected;
            nakama.OnDisconnected += OnNakamaDisconnected;
            GD.Print("MainMenuUI: Connected to NakamaManager events");
        }
        else
        {
            GD.PrintErr("MainMenuUI: Failed to get NakamaManager instance!");
        }
    }



    /// <summary>
    /// Handle successful authentication
    /// </summary>
    private void OnAuthenticated(Nakama.ISession session)
    {
        GD.Print($"MainMenuUI: Authenticated as {session.Username} ({session.UserId})");

        // Update connection dialog
        if (connectionStatus != null)
        {
            connectionStatus.Text = $"✅ Connected as: {session.Username}\nReady to play!";
        }

        // Auto-close connection dialog after successful auth
        CallDeferred(nameof(CloseConnectionDialog));
    }

    /// <summary>
    /// Handle authentication failure
    /// </summary>
    private void OnAuthenticationFailed()
    {
        GD.PrintErr("MainMenuUI: Authentication failed");

        if (connectionStatus != null)
        {
            connectionStatus.Text = "❌ Authentication failed\nPlease try again";
        }

        ShowErrorDialog("Authentication Failed", "Could not connect to game servers.\nPlease check your internet connection and try again.");
    }

    /// <summary>
    /// Handle socket connection
    /// </summary>
    private void OnNakamaConnected()
    {
        GD.Print("MainMenuUI: Socket connected to Nakama");
    }

    /// <summary>
    /// Handle socket disconnection
    /// </summary>
    private void OnNakamaDisconnected()
    {
        GD.Print("MainMenuUI: Socket disconnected from Nakama");
    }

    /// <summary>
    /// Create connection status dialog
    /// </summary>
    private void CreateConnectionDialog()
    {
        connectionDialog = new AcceptDialog();
        connectionDialog.Title = "Connecting to Game Servers";
        connectionDialog.Size = new Vector2I(400, 200);

        connectionStatus = new Label();
        connectionStatus.Text = "🔄 Connecting to Nakama servers...\nPlease wait...";
        connectionStatus.AutowrapMode = TextServer.AutowrapMode.WordSmart;
        connectionStatus.Size = new Vector2(350, 100);
        connectionStatus.Position = new Vector2(25, 50);

        connectionDialog.AddChild(connectionStatus);
        AddChild(connectionDialog);

        GD.Print("MainMenuUI: Connection dialog created");
    }

    /// <summary>
    /// Close connection dialog
    /// </summary>
    private void CloseConnectionDialog()
    {
        if (connectionDialog != null && connectionDialog.Visible)
        {
            connectionDialog.Hide();
        }
    }

    /// <summary>
    /// Create dialog for entering match ID
    /// </summary>
    private void CreateJoinDialog()
    {
        // Create dialog
        joinDialog = new AcceptDialog();
        joinDialog.Title = "Join Game";
        joinDialog.Size = new Vector2I(400, 200);
        AddChild(joinDialog);

        // Create input field for room code
        roomCodeInput = new LineEdit();
        roomCodeInput.Text = "";
        roomCodeInput.PlaceholderText = "Enter room code (e.g. ABC123)";
        roomCodeInput.Size = new Vector2(350, 30);
        roomCodeInput.Position = new Vector2(25, 50);

        joinDialog.AddChild(roomCodeInput);

        // Create info label
        var infoLabel = new Label();
        infoLabel.Text = "Enter the 6-character room code provided by the host player.";
        infoLabel.AutowrapMode = TextServer.AutowrapMode.WordSmart;
        infoLabel.Size = new Vector2(350, 40);
        infoLabel.Position = new Vector2(25, 90);
        joinDialog.AddChild(infoLabel);

        // Connect dialog accepted signal
        joinDialog.Confirmed += OnJoinDialogConfirmed;

        GD.Print("MainMenuUI: Join dialog created");
    }

    /// <summary>
    /// Handle join dialog confirmation
    /// </summary>
    private void OnJoinDialogConfirmed()
    {
        string roomCode = roomCodeInput.Text.Trim();
        GD.Print($"MainMenuUI: Attempting to join with room code: {roomCode}");

        if (!string.IsNullOrEmpty(roomCode))
        {
            JoinMatch(roomCode);
        }
        else
        {
            GD.PrintErr("MainMenuUI: Invalid room code");
            ShowErrorDialog("Invalid Room Code", "Please enter a valid 6-character room code.");
        }

        roomCodeInput.Text = "";
    }

    /// <summary>
    /// Join a Nakama match using room code
    /// </summary>
    private async void JoinMatch(string roomCode)
    {
        try
        {
            if (nakama == null || !nakama.IsAuthenticated)
            {
                ShowErrorDialog("Not Connected", "Please wait for connection to complete before joining a game.");
                return;
            }

            // Validate and debug the room code
            if (string.IsNullOrWhiteSpace(roomCode))
            {
                ShowErrorDialog("Invalid Room Code", "Please enter a valid 6-character room code.");
                return;
            }

            GD.Print($"MainMenuUI: Attempting to join using room code...");
            GD.Print($"MainMenuUI: Raw Room Code: '{roomCode}' (length: {roomCode.Length})");
            GD.Print($"MainMenuUI: Trimmed Room Code: '{roomCode.Trim()}' (length: {roomCode.Trim().Length})");
            GD.Print($"MainMenuUI: Nakama authenticated: {nakama.IsAuthenticated}, connected: {nakama.IsConnected}");

            // Trim whitespace from room code - NakamaManager will handle conversion
            var cleanRoomCode = roomCode.Trim();
            var match = await nakama.JoinMatch(cleanRoomCode);

            if (match != null)
            {
                GD.Print($"MainMenuUI: Successfully joined match {match.Id}");

                // Set the match in MatchManager
                var matchManager = MatchManager.Instance;
                if (matchManager != null)
                {
                    matchManager.SetCurrentMatch(match);
                    GD.Print("MainMenuUI: Match set in MatchManager for joined match");
                }

                // Transition to game scene
                GD.Print($"MainMenuUI: Successfully joined match {match.Id} with {match.Size} players - transitioning to game scene");
                GetTree().CallDeferred("change_scene_to_file", "res://scenes/CardGame.tscn");
            }
            else
            {
                GD.PrintErr($"MainMenuUI: Failed to join room - received null response for room code: '{cleanRoomCode}'");
                ShowErrorDialog("Join Failed", $"Could not join room: {cleanRoomCode}\n\nPossible reasons:\n• Room code does not exist or expired\n• Room is full (4 players max)\n• Connection issue\n\nTry creating a new match instead.");
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MainMenuUI: Error joining match: {ex.Message}");
            ShowErrorDialog("Join Error", $"An error occurred while joining the match:\n{ex.Message}");
        }
    }

    /// <summary>
    /// Create a new Nakama match with friendly room code
    /// </summary>
    private async void CreateMatch()
    {
        try
        {
            if (nakama == null || !nakama.IsAuthenticated)
            {
                ShowErrorDialog("Not Connected", "Please wait for connection to complete before creating a game.");
                return;
            }

            GD.Print("MainMenuUI: Creating new match with room code...");

            // 🎮 Use new room code system
            var roomCode = await nakama.CreateMatchAndGetRoomCodeAsync();

            if (!string.IsNullOrEmpty(roomCode))
            {
                GD.Print($"MainMenuUI: Successfully created match with room code: {roomCode}");

                // Get the actual match object for MatchManager
                var matchId = nakama.GetMatchIdFromRoomCode(roomCode);
                if (!string.IsNullOrEmpty(matchId))
                {
                    var match = await nakama.JoinMatch(matchId); // Join our own match to get IMatch object

                    if (match != null)
                    {
                        // Set the match in MatchManager
                        var matchManager = MatchManager.Instance;
                        if (matchManager != null)
                        {
                            matchManager.SetCurrentMatch(match);
                            GD.Print("MainMenuUI: Match set in MatchManager");
                        }
                    }
                }

                // Show room code with longer display time, then transition to game scene
                GD.Print($"MainMenuUI: Match created successfully! Room Code: {roomCode} - showing to user");
                ShowBriefRoomCodeAndTransition(roomCode);
            }
            else
            {
                ShowErrorDialog("Create Failed", "Could not create a new match. Please try again.");
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MainMenuUI: Error creating match: {ex.Message}");
            ShowErrorDialog("Create Error", $"An error occurred while creating the match:\n{ex.Message}");
        }
    }

    // Active dialog reference to prevent multiple exclusive dialogs
    private AcceptDialog activeDialog;

    /// <summary>
    /// Close any existing dialog and show a new one
    /// </summary>
    private void ShowDialog(AcceptDialog dialog)
    {
        // Close and remove any existing dialog immediately
        CloseActiveDialog();

        activeDialog = dialog;
        AddChild(dialog);
        dialog.PopupCentered();

        // Auto-remove dialog after it's closed
        dialog.Connect("close_requested", new Callable(this, nameof(OnDialogClosed)));
    }

    /// <summary>
    /// Close the active dialog immediately
    /// </summary>
    private void CloseActiveDialog()
    {
        if (activeDialog != null && IsInstanceValid(activeDialog))
        {
            activeDialog.Hide();
            activeDialog.QueueFree();
            activeDialog = null;
        }
    }

    /// <summary>
    /// Handle dialog close event
    /// </summary>
    private void OnDialogClosed()
    {
        CloseActiveDialog();
    }

    /// <summary>
    /// Show room code with extended time for copying, then transition to game scene
    /// FIXED VERSION - prevents ObjectDisposedException during async operations
    /// </summary>
    private async void ShowBriefRoomCodeAndTransition(string roomCode)
    {
        var dialog = new AcceptDialog();
        dialog.Title = "🎉 Match Created!";
        dialog.DialogText = $"Your game room is ready!\n\n" +
                           $"🏷️  Room Code: {roomCode}\n\n" +
                           $"📋 Copy this code and share it with friends!\n" +
                           $"📞 Easy to say over voice chat or text\n\n" +
                           $"⏱️  Starting game in 8 seconds...\n" +
                           $"(Click OK to start immediately)";
        dialog.Size = new Vector2I(450, 250);
        dialog.GetOkButtonText();

        ShowDialog(dialog);

        // 🔥 CRITICAL FIX: Add disposal checks and early exit handling
        bool dialogClosed = false;

        // Connect to dialog confirmed signal to detect early closure
        dialog.Confirmed += () =>
        {
            dialogClosed = true;
            GD.Print("MainMenuUI: User clicked OK - starting game immediately");
        };

        // Wait 8 seconds with disposal checks
        for (int i = 0; i < 80; i++) // 80 * 100ms = 8000ms
        {
            // 🔥 CRITICAL: Check if this object has been disposed/freed
            if (!IsInstanceValid(this) || IsQueuedForDeletion())
            {
                GD.Print("MainMenuUI: Object disposed during room code display - cancelling transition");
                return;
            }

            // Check if dialog was closed early
            if (dialogClosed)
            {
                break;
            }

            await Task.Delay(100);
        }

        // 🔥 CRITICAL: Final disposal check before accessing GetTree()
        if (!IsInstanceValid(this) || IsQueuedForDeletion())
        {
            GD.Print("MainMenuUI: Object disposed after delay - cancelling scene transition");
            return;
        }

        // Close dialog and transition
        CloseActiveDialog();
        GD.Print($"MainMenuUI: Transitioning to game scene after showing room code: {roomCode}");

        // 🔥 CRITICAL: Check tree availability before calling scene change
        var tree = GetTree();
        if (tree != null && IsInstanceValid(tree))
        {
            tree.CallDeferred("change_scene_to_file", "res://scenes/CardGame.tscn");
        }
        else
        {
            GD.PrintErr("MainMenuUI: Cannot transition scene - tree not available");
        }
    }

    /// <summary>
    /// Show match created dialog with match ID
    /// </summary>
    private void ShowMatchCreatedDialog(string matchId)
    {
        var dialog = new AcceptDialog();
        dialog.Title = "Match Created!";
        dialog.Size = new Vector2I(500, 300);

        dialog.DialogText = $"🎉 Your match has been created!\n\n" +
                           $"Match ID: {matchId}\n\n" +
                           $"📤 Share this Match ID with other players so they can join your game.\n\n" +
                           $"💡 Tip: Copy this ID and send it to your friends via chat, email, or any messaging app.\n\n" +
                           $"The game will start automatically when players join!";

        ShowDialog(dialog);
    }

    /// <summary>
    /// Show error dialog
    /// </summary>
    private void ShowErrorDialog(string title, string message)
    {
        var dialog = new AcceptDialog();
        dialog.Title = title;
        dialog.DialogText = message;
        dialog.Size = new Vector2I(400, 200);

        ShowDialog(dialog);
    }

    /// <summary>
    /// Show success dialog
    /// </summary>
    private void ShowSuccessDialog(string title, string message)
    {
        var dialog = new AcceptDialog();
        dialog.Title = title;
        dialog.DialogText = message;
        dialog.Size = new Vector2I(400, 200);

        ShowDialog(dialog);
    }

    /// <summary>
    /// Connect to Nakama servers
    /// </summary>
    private async void ConnectToNakama()
    {
        if (nakama == null)
        {
            ShowErrorDialog("Error", "NakamaManager not available");
            return;
        }

        // Close any existing dialogs and show connection dialog
        CloseActiveDialog();

        // Show connection dialog using proper dialog management
        activeDialog = connectionDialog;
        if (!connectionDialog.IsInsideTree())
        {
            AddChild(connectionDialog);
        }
        connectionDialog.PopupCentered();
        connectionStatus.Text = "🔄 Connecting to Nakama servers...\nPlease wait...";

        try
        {
            // Generate a unique device ID for this session
            var deviceId = System.Guid.NewGuid().ToString();
            GD.Print($"MainMenuUI: Attempting authentication with device ID: {deviceId}");

            var success = await nakama.AuthenticateAsync(deviceId);

            if (!success)
            {
                connectionStatus.Text = "❌ Connection failed\nPlease try again";
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"MainMenuUI: Connection error: {ex.Message}");
            connectionStatus.Text = $"❌ Connection error:\n{ex.Message}";
        }
    }

    // Button event handlers
    private void _on_host_button_pressed()
    {
        GD.Print("Host button pressed - creating Nakama match");

        if (nakama == null || !nakama.IsAuthenticated)
        {
            ConnectToNakama();
            // Wait for connection, then create match
            CallDeferred(nameof(DelayedCreateMatch));
        }
        else
        {
            CreateMatch();
        }
    }

    private async void DelayedCreateMatch()
    {
        // Wait for authentication to complete with disposal checks
        for (int i = 0; i < 50; i++) // Wait up to 5 seconds
        {
            // 🔥 CRITICAL: Check if this object has been disposed/freed
            if (!IsInstanceValid(this) || IsQueuedForDeletion())
            {
                GD.Print("MainMenuUI: Object disposed during DelayedCreateMatch - cancelling");
                return;
            }

            if (nakama?.IsAuthenticated == true)
            {
                CreateMatch();
                return;
            }
            await Task.Delay(100);
        }

        // 🔥 CRITICAL: Final disposal check before showing error
        if (!IsInstanceValid(this) || IsQueuedForDeletion())
        {
            GD.Print("MainMenuUI: Object disposed during DelayedCreateMatch timeout - cancelling");
            return;
        }

        ShowErrorDialog("Connection Timeout", "Could not connect to servers in time. Please try again.");
    }

    private void _on_join_button_pressed()
    {
        GD.Print("Join button pressed - showing room code dialog");

        if (nakama == null || !nakama.IsAuthenticated)
        {
            ConnectToNakama();
            // Show join dialog after connection completes
            CallDeferred(nameof(DelayedShowJoinDialog));
        }
        else
        {
            // Already connected, show join dialog immediately
            ShowJoinDialog();
        }
    }

    /// <summary>
    /// Show join dialog using proper dialog management
    /// </summary>
    private void ShowJoinDialog()
    {
        // Close any existing dialogs first, then show join dialog
        CloseActiveDialog();

        // Use deferred call to ensure any previous dialog is fully closed
        CallDeferred(nameof(DelayedPopupJoinDialog));
    }

    /// <summary>
    /// Actually popup the join dialog (called deferred)
    /// </summary>
    private void DelayedPopupJoinDialog()
    {
        joinDialog.PopupCentered();
        roomCodeInput.GrabFocus();
    }

    /// <summary>
    /// Show join dialog after authentication completes
    /// FIXED VERSION - prevents ObjectDisposedException during async operations
    /// </summary>
    private async void DelayedShowJoinDialog()
    {
        // Wait for authentication to complete with disposal checks
        for (int i = 0; i < 50; i++) // Wait up to 5 seconds
        {
            // 🔥 CRITICAL: Check if this object has been disposed/freed
            if (!IsInstanceValid(this) || IsQueuedForDeletion())
            {
                GD.Print("MainMenuUI: Object disposed during DelayedShowJoinDialog - cancelling");
                return;
            }

            if (nakama?.IsAuthenticated == true)
            {
                ShowJoinDialog();
                return;
            }
            await Task.Delay(100);
        }

        // 🔥 CRITICAL: Final disposal check before showing error
        if (!IsInstanceValid(this) || IsQueuedForDeletion())
        {
            GD.Print("MainMenuUI: Object disposed during DelayedShowJoinDialog timeout - cancelling");
            return;
        }

        // If authentication failed, show error and still allow join attempt
        ShowErrorDialog("Connection Timeout", "Could not connect to servers in time. You can still try entering a room code.");
        CallDeferred(nameof(ShowJoinDialog));
    }

    private void _on_test_button_pressed()
    {
        GD.Print("Test button pressed - testing Nakama connection");
        ConnectToNakama();
    }

    private void _on_quit_button_pressed()
    {
        GD.Print("Quit button pressed");
        GetTree().Quit();
    }

    public override void _ExitTree()
    {
        // Clean up event connections
        if (nakama != null)
        {
            nakama.OnAuthenticated -= OnAuthenticated;
            nakama.OnAuthenticationFailed -= OnAuthenticationFailed;
            nakama.OnConnected -= OnNakamaConnected;
            nakama.OnDisconnected -= OnNakamaDisconnected;
        }
    }
}
