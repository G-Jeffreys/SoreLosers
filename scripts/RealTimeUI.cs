using Godot;

public partial class RealTimeUI : Control
{
    private CharacterBody2D player;
    private Label inventoryLabel;
    private Control overlayLayer;
    private int eggCount = 0;
    private bool hasBomb = false;

    public override void _Ready()
    {
        GD.Print("RealTime UI loaded");

        // Get references
        player = GetNode<CharacterBody2D>("GameArea/Player");
        inventoryLabel = GetNode<Label>("UI/InventoryLabel");
        overlayLayer = GetNode<Control>("OverlayLayer");

        UpdateInventoryDisplay();
    }

    public override void _Process(double delta)
    {
        HandlePlayerMovement();
        HandleInteraction();
    }

    private void HandlePlayerMovement()
    {
        var velocity = Vector2.Zero;

        if (Input.IsActionPressed("move_left"))
            velocity.X -= 1;
        if (Input.IsActionPressed("move_right"))
            velocity.X += 1;
        if (Input.IsActionPressed("move_up"))
            velocity.Y -= 1;
        if (Input.IsActionPressed("move_down"))
            velocity.Y += 1;

        velocity = velocity.Normalized() * 150; // Base speed
        player.Velocity = velocity;
        player.MoveAndSlide();
    }

    private void HandleInteraction()
    {
        if (Input.IsActionJustPressed("interact"))
        {
            GD.Print("Interact pressed");
            CheckInteraction();
        }
    }

    private void CheckInteraction()
    {
        var spaceState = GetViewport().World2D.DirectSpaceState;
        var query = new PhysicsPointQueryParameters2D();
        query.Position = player.GlobalPosition;
        query.CollisionMask = 1;

        var result = spaceState.IntersectPoint(query);
        if (result.Count > 0)
        {
            var body = ((Godot.Collections.Dictionary)result[0])["collider"].AsGodotObject();
            if (body is Node node)
            {
                if (node.Name == "EggTray")
                {
                    PickupEgg();
                }
                else if (node.Name == "Sink")
                {
                    WashEgg();
                }
            }
        }
    }

    private void PickupEgg()
    {
        if (eggCount < 3)
        {
            eggCount++;
            GD.Print($"Picked up egg! Count: {eggCount}");
            UpdateInventoryDisplay();
        }
    }

    private void WashEgg()
    {
        GD.Print("Washing at sink!");
        // Clear any egg overlays
        foreach (Node child in overlayLayer.GetChildren())
        {
            if (child.Name.ToString().Contains("EggSplat"))
            {
                child.QueueFree();
            }
        }
    }

    private void UpdateInventoryDisplay()
    {
        var items = new System.Collections.Generic.List<string>();
        if (eggCount > 0) items.Add($"Eggs: {eggCount}");
        if (hasBomb) items.Add("Stink Bomb");

        inventoryLabel.Text = items.Count > 0 ?
            $"Inventory: {string.Join(", ", items)}" :
            "Inventory: Empty";
    }

    public void CreateEggSplat(Vector2 position)
    {
        var splat = new ColorRect();
        splat.Name = "EggSplat";
        splat.Color = new Color(1, 1, 0, 0.7f);
        splat.Size = new Vector2(100, 100);
        splat.Position = position - splat.Size / 2;
        overlayLayer.AddChild(splat);

        GD.Print($"Created egg splat at {position}");
    }

    public void CreateStinkBomb(Vector2 position)
    {
        var fog = new ColorRect();
        fog.Name = "StinkFog";
        fog.Color = new Color(0, 1, 0, 0.5f);
        fog.Size = new Vector2(160, 160);
        fog.Position = position - fog.Size / 2;
        overlayLayer.AddChild(fog);

        // Remove after 30 seconds
        GetTree().CreateTimer(30.0).Timeout += () =>
        {
            if (IsInstanceValid(fog))
                fog.QueueFree();
        };

        GD.Print($"Created stink bomb at {position}");
    }
}
