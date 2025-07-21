using Godot;
using System;

/// <summary>
/// Represents player data including profile information, stats, and XP progression
/// Based on RPG stats system from PRD: ThrowPower, MoveSpeed, Composure (Levels 1-10)
/// </summary>
[System.Serializable]
public partial class PlayerData : Resource
{
    // Player identification
    [Export]
    public int PlayerId { get; set; }

    [Export]
    public string PlayerName { get; set; } = "Player";

    // RPG Stats (Levels 1-10) - from PRD Section 5.4
    [Export]
    public int ThrowPower { get; set; } = 1; // Affects egg splat coverage (20% -> 80%)

    [Export]
    public int MoveSpeed { get; set; } = 1; // Affects movement speed (110 -> 160 px/s)

    [Export]
    public int Composure { get; set; } = 1; // Affects blur strength (100% -> 50%)

    // XP System - from PRD Section 5.4
    [Export]
    public int TotalXP { get; set; } = 0;

    [Export]
    public int ThrowPowerXP { get; set; } = 0;

    [Export]
    public int MoveSpeedXP { get; set; } = 0;

    [Export]
    public int ComposureXP { get; set; } = 0;

    // Game statistics
    [Export]
    public int GamesWon { get; set; } = 0;

    [Export]
    public int GamesLost { get; set; } = 0;

    [Export]
    public int SuccessfulSabotages { get; set; } = 0;

    [Export]
    public int TimesEgged { get; set; } = 0;

    [Export]
    public int TimesStinkBombed { get; set; } = 0;

    // XP configuration
    [Export]
    public int ResilienceXPReward = 5; // XP awarded for surviving sabotage attacks

    // Constructors
    public PlayerData()
    {
        GD.Print("PlayerData: Default constructor called");
    }

    public PlayerData(int playerId, string playerName)
    {
        GD.Print($"PlayerData: Creating player {playerName} (ID: {playerId})");

        PlayerId = playerId;
        PlayerName = playerName;

        // Initialize with level 1 stats (PRD defaults)
        ThrowPower = 1;
        MoveSpeed = 1;
        Composure = 1;

        // Initialize XP values
        TotalXP = 0;
        ThrowPowerXP = 0;
        MoveSpeedXP = 0;
        ComposureXP = 0;

        // Initialize game statistics
        GamesWon = 0;
        GamesLost = 0;
        SuccessfulSabotages = 0;
        TimesEgged = 0;
        TimesStinkBombed = 0;

        GD.Print($"PlayerData: Player {playerName} initialized with default stats");
    }

    // Stat calculation methods based on PRD specifications

    /// <summary>
    /// Calculate actual throw power coverage percentage (20%/50%/80% for levels 1/2/3)
    /// </summary>
    /// <returns>Coverage percentage as float (0.2 to 0.8)</returns>
    public float GetThrowPowerCoverage()
    {
        // REDESIGNED: Levels 1-3 with much more impactful progression for shorter games
        // Level 1: 20%, Level 2: 50%, Level 3: 80% (+30% per level)
        float coverage = ThrowPower switch
        {
            1 => 0.2f,   // 20% coverage (baseline)
            2 => 0.5f,   // 50% coverage (+30% absolute)
            3 => 0.8f,   // 80% coverage (+30% absolute)
            _ => 0.8f + (ThrowPower - 3) * 0.02f  // Fallback for levels 4+ (much smaller gains)
        };

        return coverage;
    }

    /// <summary>
    /// Calculate actual movement speed in pixels per second (80/110/140 for levels 1/2/3)
    /// </summary>
    /// <returns>Movement speed in pixels per second</returns>
    public float GetMovementSpeed()
    {
        // REDESIGNED: Levels 1-3 with much more impactful progression for shorter games
        // Level 1: 80 px/s, Level 2: 110 px/s, Level 3: 140 px/s (+30 px/s per level)
        float speed = MoveSpeed switch
        {
            1 => 80.0f,   // Baseline
            2 => 110.0f,  // +30 px/s (37.5% increase)
            3 => 140.0f,  // +30 px/s (27% increase)
            _ => 140.0f + (MoveSpeed - 3) * 5.0f  // Fallback for levels 4+ (much smaller gains)
        };

        return speed;
    }

    /// <summary>
    /// Calculate blur strength multiplier (100%/75%/50% for levels 1/2/3)
    /// </summary>
    /// <returns>Blur strength multiplier (0.5 to 1.0)</returns>
    public float GetBlurStrength()
    {
        // REDESIGNED: Levels 1-3 with much more impactful progression for shorter games
        // Level 1: 100% blur, Level 2: 75% blur, Level 3: 50% blur (-25% per level)
        float blurStrength = Composure switch
        {
            1 => 1.0f,   // 100% blur (most vulnerable)
            2 => 0.75f,  // 75% blur (-25% absolute)
            3 => 0.5f,   // 50% blur (-25% absolute, strong resistance)
            _ => 0.5f - (Composure - 3) * 0.05f  // Fallback for levels 4+ (much smaller gains)
        };

        return Math.Max(blurStrength, 0.2f); // Minimum 20% blur to maintain some effect
    }

    /// <summary>
    /// Calculate maximum number of eggs a player can carry based on ThrowPower level
    /// Level 1: 3 eggs, Level 2: 4 eggs, Level N: 2 + N eggs
    /// </summary>
    /// <returns>Maximum eggs in inventory</returns>
    public int GetMaxEggsInInventory()
    {
        // Formula: 2 + ThrowPower level
        // Level 1: 2 + 1 = 3 eggs (baseline)
        // Level 2: 2 + 2 = 4 eggs (+1 per level)
        // Level 3: 2 + 3 = 5 eggs (+1 per level)
        // Level 10: 2 + 10 = 12 eggs (max possible)
        int maxEggs = 2 + ThrowPower;

        GD.Print($"PlayerData: ThrowPower Level {ThrowPower} allows {maxEggs} max eggs in inventory");

        return maxEggs;
    }

    // XP and leveling system methods

    /// <summary>
    /// Calculate XP required to reach next level (PRD: 50 × level²)
    /// </summary>
    /// <param name="currentLevel">Current level of the stat</param>
    /// <returns>XP required to reach next level</returns>
    public static int GetXPRequiredForLevel(int currentLevel)
    {
        // PRD: XP_to_next = 50 × level²
        int xpRequired = 50 * currentLevel * currentLevel;

        // 🔥 REMOVED: Excessive logging that caused infinite spam
        return xpRequired;
    }

    /// <summary>
    /// Add XP to ThrowPower and check for level up
    /// </summary>
    /// <param name="xp">Amount of XP to add</param>
    /// <returns>True if leveled up, false otherwise</returns>
    public bool AddThrowPowerXP(int xp)
    {
        GD.Print($"PlayerData: Adding {xp} XP to ThrowPower (current: {ThrowPowerXP})");

        ThrowPowerXP += xp;
        TotalXP += xp;

        // Check for level up (capped at level 3 for shorter games)
        if (ThrowPower < 3 && ThrowPowerXP >= GetXPRequiredForLevel(ThrowPower))
        {
            int oldThrowPower = ThrowPower;
            int oldMaxEggs = GetMaxEggsInInventory(); // Calculate before leveling up

            ThrowPowerXP -= GetXPRequiredForLevel(ThrowPower);
            ThrowPower++;

            int newMaxEggs = GetMaxEggsInInventory(); // Calculate after leveling up

            GD.Print($"PlayerData: ThrowPower leveled up to {ThrowPower}!");
            GD.Print($"🥚 EGG CAPACITY INCREASED: You can now carry {newMaxEggs} eggs (was {oldMaxEggs})!");
            return true;
        }

        return false;
    }

    /// <summary>
    /// Add XP to MoveSpeed and check for level up
    /// </summary>
    /// <param name="xp">Amount of XP to add</param>
    /// <returns>True if leveled up, false otherwise</returns>
    public bool AddMoveSpeedXP(int xp)
    {
        GD.Print($"PlayerData: Adding {xp} XP to MoveSpeed (current: {MoveSpeedXP})");

        MoveSpeedXP += xp;
        TotalXP += xp;

        // Check for level up (capped at level 3 for shorter games)
        if (MoveSpeed < 3 && MoveSpeedXP >= GetXPRequiredForLevel(MoveSpeed))
        {
            MoveSpeedXP -= GetXPRequiredForLevel(MoveSpeed);
            MoveSpeed++;

            GD.Print($"PlayerData: MoveSpeed leveled up to {MoveSpeed}!");
            return true;
        }

        return false;
    }

    /// <summary>
    /// Add XP to Composure and check for level up
    /// </summary>
    /// <param name="xp">Amount of XP to add</param>
    /// <returns>True if leveled up, false otherwise</returns>
    public bool AddComposureXP(int xp)
    {
        GD.Print($"PlayerData: Adding {xp} XP to Composure (current: {ComposureXP})");

        ComposureXP += xp;
        TotalXP += xp;

        // Check for level up (capped at level 3 for shorter games)
        if (Composure < 3 && ComposureXP >= GetXPRequiredForLevel(Composure))
        {
            ComposureXP -= GetXPRequiredForLevel(Composure);
            Composure++;

            GD.Print($"PlayerData: Composure leveled up to {Composure}!");
            return true;
        }

        return false;
    }

    /// <summary>
    /// Add XP based on game outcome (PRD: Win hand +40 XP, Lose hand +10 XP)
    /// </summary>
    /// <param name="won">True if player won the hand</param>
    public void AddGameXP(bool won)
    {
        int xpToAdd = won ? 40 : 10; // PRD specifications

        GD.Print($"PlayerData: Adding {xpToAdd} XP for game outcome (won: {won})");

        // Split XP evenly across all stats (40 XP = ~13.33 per stat)
        int xpPerStat = xpToAdd / 3;
        int remainder = xpToAdd % 3;

        // Add base XP to each stat
        bool throwPowerLevelUp = AddThrowPowerXP(xpPerStat);
        bool moveSpeedLevelUp = AddMoveSpeedXP(xpPerStat);
        bool composureLevelUp = AddComposureXP(xpPerStat);

        // Add remainder XP to ThrowPower
        if (remainder > 0)
        {
            AddThrowPowerXP(remainder);
        }

        // Update game statistics
        if (won)
        {
            GamesWon++;
        }
        else
        {
            GamesLost++;
        }

        // Log level ups
        if (throwPowerLevelUp || moveSpeedLevelUp || composureLevelUp)
        {
            GD.Print($"PlayerData: Level up! ThrowPower: {ThrowPower}, MoveSpeed: {MoveSpeed}, Composure: {Composure}");
        }
    }

    /// <summary>
    /// Add XP for successful sabotage (PRD: +20 XP to related stat)
    /// </summary>
    /// <param name="sabotageType">Type of sabotage performed</param>
    public void AddSabotageXP(SabotageType sabotageType)
    {
        GD.Print($"PlayerData: Adding sabotage XP for {sabotageType}");

        const int sabotageXP = 20; // PRD specification

        switch (sabotageType)
        {
            case SabotageType.EggThrow:
                AddThrowPowerXP(sabotageXP);
                break;
            case SabotageType.StinkBomb:
                AddMoveSpeedXP(sabotageXP); // Speed helps with bomb placement/escape
                break;
            default:
                GD.PrintErr($"PlayerData: Unknown sabotage type: {sabotageType}");
                break;
        }

        SuccessfulSabotages++;
        GD.Print($"PlayerData: Successful sabotages: {SuccessfulSabotages}");
    }

    /// <summary>
    /// Record being hit by sabotage and award Composure XP for building resilience
    /// </summary>
    /// <param name="sabotageType">Type of sabotage received</param>
    public void RecordSabotageHit(SabotageType sabotageType)
    {
        GD.Print($"PlayerData: Recording sabotage hit: {sabotageType}");

        switch (sabotageType)
        {
            case SabotageType.EggThrow:
                TimesEgged++;
                // Award Composure XP for getting egged (builds resilience)
                bool eggComposureLevelUp = AddComposureXP(ResilienceXPReward);
                GD.Print($"PlayerData: Awarded {ResilienceXPReward} Composure XP for surviving egg throw");

                if (eggComposureLevelUp)
                {
                    GD.Print($"PlayerData: Composure leveled up to {Composure} from surviving egg throws!");
                }
                break;

            case SabotageType.StinkBomb:
                TimesStinkBombed++;
                // Award Composure XP for getting stink bombed (builds resilience)
                bool stinkComposureLevelUp = AddComposureXP(ResilienceXPReward);
                GD.Print($"PlayerData: Awarded {ResilienceXPReward} Composure XP for surviving stink bomb");

                if (stinkComposureLevelUp)
                {
                    GD.Print($"PlayerData: Composure leveled up to {Composure} from surviving stink bombs!");
                }
                break;

            default:
                GD.PrintErr($"PlayerData: Unknown sabotage type: {sabotageType}");
                break;
        }
    }

    /// <summary>
    /// Get a formatted string representation of the player data
    /// </summary>
    /// <returns>Formatted player information</returns>
    /// <summary>
    /// Calculate level from XP amount
    /// </summary>
    /// <param name="xp">XP amount</param>
    /// <returns>Level (1-10)</returns>
    public int GetLevel(int xp)
    {
        if (xp <= 0) return 1;

        for (int level = 1; level <= 10; level++)
        {
            int required = GetXPForLevel(level);
            if (xp < required)
            {
                return level - 1;
            }
        }

        return 10; // Max level
    }

    /// <summary>
    /// Get total XP required to reach specific level
    /// PRD: XP_to_next = 50 × level² for going from level N to N+1
    /// Total XP to reach level N = sum of all XP_to_next from 1 to N-1
    /// </summary>
    /// <param name="level">Target level</param>
    /// <returns>Total XP required to reach this level</returns>
    public int GetXPForLevel(int level)
    {
        if (level <= 1) return 0;

        // Calculate cumulative XP: sum of 50 × i² for i from 1 to (level-1)
        int totalXP = 0;
        for (int i = 1; i < level; i++)
        {
            totalXP += 50 * i * i;
        }
        return totalXP;
    }

    public override string ToString()
    {
        return $"Player {PlayerName} (ID: {PlayerId}) - " +
               $"ThrowPower: {ThrowPower}, MoveSpeed: {MoveSpeed}, Composure: {Composure} - " +
               $"Total XP: {TotalXP}, Games Won: {GamesWon}, Games Lost: {GamesLost}";
    }
}

/// <summary>
/// Enum for different types of sabotage actions
/// </summary>
public enum SabotageType
{
    EggThrow,
    StinkBomb
}