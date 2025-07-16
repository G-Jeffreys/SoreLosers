using Godot;

public partial class MainMenuUI : Control
{
    private AcceptDialog joinDialog;
    private LineEdit roomCodeInput;

    public override void _Ready()
    {
        GD.Print("MainMenu loaded");
        CreateJoinDialog();
    }

    /// <summary>
    /// Create dialog for entering room code
    /// </summary>
    private void CreateJoinDialog()
    {
        // Create dialog
        joinDialog = new AcceptDialog();
        joinDialog.Title = "Join Game";
        joinDialog.Size = new Vector2I(300, 150);
        AddChild(joinDialog);

        // Create input field
        roomCodeInput = new LineEdit();
        roomCodeInput.Text = "";
        roomCodeInput.PlaceholderText = "Enter 6-digit room code";
        roomCodeInput.MaxLength = 6;
        roomCodeInput.Size = new Vector2(200, 30);
        roomCodeInput.Position = new Vector2(50, 50);

        joinDialog.AddChild(roomCodeInput);

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

        if (roomCode.Length == 6 && int.TryParse(roomCode, out _))
        {
            GameManager.Instance.JoinGame(roomCode);
        }
        else
        {
            GD.PrintErr("MainMenuUI: Invalid room code format");
            // TODO: Show error message
        }

        roomCodeInput.Text = "";
    }

    private void _on_host_button_pressed()
    {
        GD.Print("Host button pressed");
        GameManager.Instance.StartHosting();
    }

    private void _on_join_button_pressed()
    {
        GD.Print("Join button pressed - showing room code dialog");
        joinDialog.PopupCentered();
        roomCodeInput.GrabFocus();
    }

    /// <summary>
    /// Handle single-player test button press (for testing)
    /// </summary>
    private void _on_test_button_pressed()
    {
        GD.Print("Test button pressed - starting single-player game");
        GameManager.Instance.StartSinglePlayerGame();
    }

    private void _on_quit_button_pressed()
    {
        GD.Print("Quit button pressed");
        GetTree().Quit();
    }
}
