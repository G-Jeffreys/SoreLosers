using Godot;

public partial class ResultsUI : Control
{
    private Label winnerLabel;
    private VBoxContainer scoresList;
    private Label xpLabel;
    private Label statsLabel;

    public override void _Ready()
    {
        GD.Print("Results UI loaded");
        
        // Get references
        winnerLabel = GetNode<Label>("VBoxContainer/WinnerLabel");
        scoresList = GetNode<VBoxContainer>("VBoxContainer/ScoresList");
        xpLabel = GetNode<Label>("VBoxContainer/XPLabel");
        statsLabel = GetNode<Label>("VBoxContainer/StatsLabel");
        
        // Show placeholder results
        ShowResults();
    }

    private void ShowResults()
    {
        // Clear existing scores
        foreach (Node child in scoresList.GetChildren())
        {
            child.QueueFree();
        }
        
        // Add player scores
        var players = new[] { "Player 1", "Player 2", "Player 3", "Player 4" };
        var scores = new[] { 120, 95, 80, 65 };
        
        for (int i = 0; i < players.Length; i++)
        {
            var scoreLabel = new Label();
            scoreLabel.Text = $"{players[i]}: {scores[i]} points";
            scoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
            scoresList.AddChild(scoreLabel);
        }
        
        // Set winner
        winnerLabel.Text = $"Winner: {players[0]}";
        
        // Show XP and stat progression
        xpLabel.Text = "XP Gained: +40 (Win bonus)";
        statsLabel.Text = "ThrowPower: Level 2 → Level 3\nMoveSpeed: 45/200 XP to next level";
    }

    private void _on_play_again_button_pressed()
    {
        GD.Print("Play Again clicked");
        GameManager.Instance.StartNewMatch();
    }

    private void _on_main_menu_button_pressed()
    {
        GD.Print("Main Menu clicked");
        GameManager.Instance.ReturnToMainMenu();
    }
}
