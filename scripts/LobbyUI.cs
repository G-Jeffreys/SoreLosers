using Godot;

/// <summary>
/// Handles lobby UI for both host and client waiting states
/// Shows connected players, room code, and start game functionality
/// </summary>
public partial class LobbyUI : Control
{
    private Label roomCodeLabel;
    private Label playersLabel;
    private VBoxContainer playerList;
    private Button startGameButton;
    private Button backButton;

    public override void _Ready()
    {
        GD.Print("Lobby UI loaded");

        // Get references to UI elements
        roomCodeLabel = GetNode<Label>("VBoxContainer/RoomCodeLabel");
        playersLabel = GetNode<Label>("VBoxContainer/PlayersLabel");
        playerList = GetNode<VBoxContainer>("VBoxContainer/PlayerList");
        startGameButton = GetNode<Button>("VBoxContainer/ButtonContainer/StartGameButton");
        backButton = GetNode<Button>("VBoxContainer/ButtonContainer/BackButton");

        // Set initial state based on game phase
        UpdateLobbyState();

        // Connect to GameManager events
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerJoined += OnPlayerJoined;
            GameManager.Instance.PlayerLeft += OnPlayerLeft;
        }
    }

    /// <summary>
    /// Update lobby state based on current game phase
    /// </summary>
    private void UpdateLobbyState()
    {
        if (GameManager.Instance == null) return;

        // Show appropriate UI based on host vs client
        bool isHost = GameManager.Instance.CurrentPhase == GameManager.GamePhase.HostLobby;

        if (isHost)
        {
            GD.Print("LobbyUI: Showing host lobby state");
            startGameButton.Visible = true;
            startGameButton.Text = "Start Game";

            // Get room code from NetworkManager
            string roomCode = GameManager.Instance.NetworkManager?.CurrentRoomCode ?? "------";
            roomCodeLabel.Text = $"Room Code: {roomCode}";
        }
        else
        {
            GD.Print("LobbyUI: Showing client lobby state");
            startGameButton.Visible = false;
            roomCodeLabel.Text = "Waiting for host...";
        }

        // Update player list
        UpdatePlayerList();
    }

    /// <summary>
    /// Generate a random 6-digit room code
    /// </summary>
    /// <returns>6-digit room code string</returns>
    private string GenerateRoomCode()
    {
        var random = new System.Random();
        return random.Next(100000, 999999).ToString();
    }

    /// <summary>
    /// Update the list of connected players
    /// </summary>
    private void UpdatePlayerList()
    {
        // Clear existing player list
        foreach (Node child in playerList.GetChildren())
        {
            child.QueueFree();
        }

        if (GameManager.Instance == null) return;

        // Add connected players
        foreach (var player in GameManager.Instance.ConnectedPlayers.Values)
        {
            var playerLabel = new Label();
            playerLabel.Text = $"• {player.PlayerName}";
            playerLabel.HorizontalAlignment = HorizontalAlignment.Center;
            playerList.AddChild(playerLabel);
        }

        // Update player count
        int playerCount = GameManager.Instance.ConnectedPlayers.Count;
        playersLabel.Text = $"Connected Players ({playerCount}/4):";
    }

    /// <summary>
    /// Handle start game button press (host only)
    /// </summary>
    private void _on_start_game_button_pressed()
    {
        GD.Print("LobbyUI: Start game button pressed");

        if (GameManager.Instance == null) return;

        // Remove player count check - we automatically add AI players to fill the game
        // Let GameManager and CardManager handle the final validation

        // Start the card game phase
        GameManager.Instance.StartCardGame();
    }

    /// <summary>
    /// Handle back to menu button press
    /// </summary>
    private void _on_back_button_pressed()
    {
        GD.Print("LobbyUI: Back button pressed");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReturnToMainMenu();
        }
    }

    /// <summary>
    /// Handle player joined event
    /// </summary>
    /// <param name="playerId">Player ID that joined</param>
    /// <param name="playerData">Player data</param>
    private void OnPlayerJoined(int playerId, PlayerData playerData)
    {
        GD.Print($"LobbyUI: Player {playerData.PlayerName} joined");
        UpdatePlayerList();
    }

    /// <summary>
    /// Handle player left event
    /// </summary>
    /// <param name="playerId">Player ID that left</param>
    private void OnPlayerLeft(int playerId)
    {
        GD.Print($"LobbyUI: Player {playerId} left");
        UpdatePlayerList();
    }

    public override void _ExitTree()
    {
        // Disconnect from GameManager events
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerJoined -= OnPlayerJoined;
            GameManager.Instance.PlayerLeft -= OnPlayerLeft;
        }
    }
}