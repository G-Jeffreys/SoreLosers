using Godot;
using System;

/// <summary>
/// Handles lobby UI for both host and client waiting states
/// Shows connected players, room code, and start game functionality
/// Displays comprehensive game instructions in the left panel
/// </summary>
public partial class LobbyUI : Control
{
    private Label roomCodeLabel;
    private Label playersLabel;
    private VBoxContainer playerList;
    private Button startGameButton;
    private Button backButton;

    // Game instructions UI elements
    private RichTextLabel instructionsText;

    public override void _Ready()
    {
        // Get references to UI elements
        roomCodeLabel = GetNode<Label>("VBoxContainer/RoomCodeLabel");
        playersLabel = GetNode<Label>("VBoxContainer/PlayersLabel");
        playerList = GetNode<VBoxContainer>("VBoxContainer/PlayerList");
        startGameButton = GetNode<Button>("VBoxContainer/ButtonContainer/StartGameButton");
        backButton = GetNode<Button>("VBoxContainer/ButtonContainer/BackButton");

        // Get reference to game instructions panel
        instructionsText = GetNode<RichTextLabel>("GameInstructionsPanel/InstructionsVBox/InstructionsText");

        // Initialize game instructions
        SetupGameInstructions();

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
    /// Setup the comprehensive game instructions text
    /// </summary>
    private void SetupGameInstructions()
    {
        GD.Print("LobbyUI: Setting up game instructions");

        // Comprehensive game rules based on the actual implementation
        string instructions = @"[font_size=12][b]OBJECTIVE:[/b]
Win tricks to score points. First player to reach 10 points wins!

[b]SETUP:[/b]
• 2-4 players (AI fills empty slots)
• Standard 52-card deck
• 13 cards dealt per player

[b]TRICK-TAKING RULES:[/b]
1. [b]Follow Suit:[/b] Must play same suit as first card if you have it
2. [b]Highest Wins:[/b] Highest card of the led suit wins the trick
3. [b]Turn Timer:[/b] 10 seconds per turn - play fast!
4. [b]Scoring:[/b] Win a trick = 1 point

[b]CONCURRENT GAMEPLAY:[/b]
5. [b]Leave Table:[/b] Click ""Leave Table"" to gather sabotage items
6. [b]Miss Turns:[/b] Away from table? You'll miss your turn (no penalty)
7. [b]Return Anytime:[/b] Click ""Return to Table"" to rejoin card play

[b]SABOTAGE SYSTEM:[/b]
8. [b]Egg Throwing:[/b] Collect eggs, throw at opponents to obstruct their view
9. [b]Wash Off:[/b] Use sink to clean egg splatter effects

[b]VICTORY CONDITIONS:[/b]
• [b]Win Game:[/b] First to 10 points wins overall
• [b]XP Rewards:[/b] Gain experience for winning tricks and rounds
• [b]Chat Intimidation:[/b] Losing players get enlarged chat panels

[b]STRATEGY TIPS:[/b]
• Balance card play with sabotage gathering
• Time your table departures carefully
• Use movement to disrupt opponents during their turns[/font_size]";

        if (instructionsText != null)
        {
            instructionsText.Text = instructions;
            GD.Print("LobbyUI: Game instructions populated successfully");
        }
        else
        {
            GD.PrintErr("LobbyUI: Failed to find InstructionsText node!");
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

        GD.Print($"LobbyUI: Phase check - CurrentPhase: {GameManager.Instance.CurrentPhase}, IsHost phase: {isHost}");
        GD.Print($"LobbyUI: NetworkManager status - IsHost: {GameManager.Instance.NetworkManager?.IsHost}, IsClient: {GameManager.Instance.NetworkManager?.IsClient}, IsConnected: {GameManager.Instance.NetworkManager?.IsConnected}");

        if (isHost)
        {
            startGameButton.Visible = true;
            startGameButton.Text = "Start Game";

            // Get room code from NetworkManager
            string roomCode = GameManager.Instance.NetworkManager?.CurrentRoomCode ?? "------";
            roomCodeLabel.Text = $"Room Code: {roomCode}";
        }
        else
        {
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

        if (GameManager.Instance == null)
        {
            GD.PrintErr("LobbyUI: GameManager.Instance is null!");
            return;
        }

        // Check if we're actually the host
        if (GameManager.Instance.NetworkManager == null)
        {
            GD.PrintErr("LobbyUI: NetworkManager is null!");
            return;
        }

        if (!GameManager.Instance.NetworkManager.IsHost)
        {
            GD.PrintErr("LobbyUI: Only host can start the game!");
            return;
        }

        // Validate minimum players (we'll add AI players automatically)
        int playerCount = GameManager.Instance.ConnectedPlayers.Count;
        if (playerCount < 1)
        {
            GD.PrintErr("LobbyUI: Need at least 1 human player to start game");
            return;
        }

        // Disable button to prevent double-clicking
        startGameButton.Disabled = true;
        startGameButton.Text = "Starting Game...";

        try
        {
            // Start the card game phase
            GameManager.Instance.StartCardGame();
        }
        catch (Exception ex)
        {
            GD.PrintErr($"LobbyUI: Failed to start card game: {ex.Message}");

            // Re-enable button on failure
            startGameButton.Disabled = false;
            startGameButton.Text = "Start Game";
        }
    }

    /// <summary>
    /// Handle back to menu button press
    /// </summary>
    private void _on_back_button_pressed()
    {
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
        UpdatePlayerList();
    }

    /// <summary>
    /// Handle player left event
    /// </summary>
    /// <param name="playerId">Player ID that left</param>
    private void OnPlayerLeft(int playerId)
    {
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