using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages sabotage system including egg throwing, stink bombs, and overlay effects
/// Implements data-driven sabotage framework with ObstructionOverlay system
/// PRD F-3: Sabotage System with egg throw and stink bomb mechanics
/// </summary>
public partial class SabotageManager : Node
{
    // Sabotage configuration from PRD
    [Export]
    public float StinkBombRadius = 160.0f; // 160px radius from PRD

    [Export]
    public float SabotageEffectDuration = 30.0f; // 30 second duration from PRD

    [Export]
    public float StinkBombCooldown = 20.0f; // 20 second cooldown from PRD

    [Export]
    public float StinkBombWarningTime = 0.8f; // 0.8 second warning from PRD

    [Export]
    public int MaxEggsInInventory = 3; // DEPRECATED: Now level-based via PlayerData.GetMaxEggsInInventory() (Level 1: 3 eggs, Level N: 2+N eggs)

    [Export]
    public float EggProjectileSpeed = 500.0f; // Egg throwing speed

    // Active sabotage tracking
    public Dictionary<int, List<SabotageEffect>> ActiveEffects { get; private set; } = new();

    // Stink bomb cooldown tracking
    private Dictionary<int, float> stinkBombCooldowns = new();

    // Item spawn management
    private Dictionary<Vector2, ItemSpawn> itemSpawns = new();

    // Obstruction overlay system
    private Dictionary<int, ObstructionOverlay> playerOverlays = new();

    // Events
    [Signal]
    public delegate void SabotageAppliedEventHandler(int targetPlayerId, SabotageType sabotageType);

    [Signal]
    public delegate void SabotageExpiredEventHandler(int targetPlayerId, SabotageType sabotageType);

    [Signal]
    public delegate void SabotageCleanedEventHandler(int targetPlayerId, SabotageType sabotageType);

    [Signal]
    public delegate void ItemSpawnedEventHandler(Vector2 position, ItemType itemType);

    [Signal]
    public delegate void ItemPickedUpEventHandler(int playerId, ItemType itemType);

    [Signal]
    public delegate void EggThrownEventHandler(int sourcePlayerId, int targetPlayerId, Vector2 targetPosition);

    [Signal]
    public delegate void StinkBombDroppedEventHandler(int sourcePlayerId, Vector2 position);

    [Signal]
    public delegate void StinkBombExplodedEventHandler(Vector2 position);

    public override void _Ready()
    {
        GD.Print("SabotageManager: Initializing sabotage system...");

        // Initialize item spawns
        InitializeItemSpawns();

        // Initialize obstruction overlay system
        InitializeObstructionOverlays();

        // Set up timers and effects
        SetupSabotageTimers();

        GD.Print("SabotageManager: Sabotage system initialized successfully");
    }

    /// <summary>
    /// Initialize item spawn locations
    /// </summary>
    private void InitializeItemSpawns()
    {
        GD.Print("SabotageManager: Initializing item spawn locations...");

        // TODO: In a real implementation, these would be loaded from scene or config
        // For now, create some example spawn points

        // Egg tray spawn points
        itemSpawns[new Vector2(100, 100)] = new ItemSpawn(ItemType.Egg, 5.0f, 3); // 5s respawn, max 3 eggs
        itemSpawns[new Vector2(500, 100)] = new ItemSpawn(ItemType.Egg, 5.0f, 3);

        // Stink bomb spawn points
        itemSpawns[new Vector2(300, 200)] = new ItemSpawn(ItemType.StinkBomb, 20.0f, 1); // 20s respawn, max 1 bomb
        itemSpawns[new Vector2(600, 300)] = new ItemSpawn(ItemType.StinkBomb, 20.0f, 1);

        // Sink locations (for cleaning)
        itemSpawns[new Vector2(50, 300)] = new ItemSpawn(ItemType.Sink, 0.0f, 1); // Always available
        itemSpawns[new Vector2(750, 300)] = new ItemSpawn(ItemType.Sink, 0.0f, 1);

        GD.Print($"SabotageManager: Initialized {itemSpawns.Count} item spawn locations");
    }

    /// <summary>
    /// Initialize obstruction overlay system for all players
    /// </summary>
    private void InitializeObstructionOverlays()
    {
        GD.Print("SabotageManager: Initializing obstruction overlays...");

        // TODO: Create overlay nodes for each player
        // For now, just initialize the tracking dictionary
        playerOverlays.Clear();

        // Get connected players from GameManager
        if (GameManager.Instance != null)
        {
            foreach (var playerId in GameManager.Instance.ConnectedPlayers.Keys)
            {
                playerOverlays[playerId] = new ObstructionOverlay(playerId);
                GD.Print($"SabotageManager: Created obstruction overlay for player {playerId}");
            }
        }

        GD.Print("SabotageManager: Obstruction overlays initialized");
    }

    /// <summary>
    /// Set up sabotage effect timers
    /// </summary>
    private void SetupSabotageTimers()
    {
        GD.Print("SabotageManager: Setting up sabotage timers...");

        // TODO: Set up effect expiration timers
        // For now, effects will be manually managed

        GD.Print("SabotageManager: Sabotage timers configured");
    }

    /// <summary>
    /// Update sabotage effects and timers
    /// </summary>
    public override void _Process(double delta)
    {
        UpdateSabotageEffects((float)delta);
        UpdateStinkBombCooldowns((float)delta);
        UpdateItemSpawns((float)delta);
    }

    /// <summary>
    /// Update active sabotage effects
    /// </summary>
    /// <param name="delta">Frame delta time</param>
    private void UpdateSabotageEffects(float delta)
    {
        var playersToUpdate = new List<int>(ActiveEffects.Keys);

        foreach (int playerId in playersToUpdate)
        {
            var effects = ActiveEffects[playerId];
            var effectsToRemove = new List<SabotageEffect>();

            foreach (var effect in effects)
            {
                effect.Duration -= delta;

                if (effect.Duration <= 0)
                {
                    effectsToRemove.Add(effect);

                    GD.Print($"SabotageManager: {effect.Type} effect expired for player {playerId}");
                    EmitSignal(SignalName.SabotageExpired, playerId, (int)effect.Type);

                    // Remove overlay effect
                    if (playerOverlays.ContainsKey(playerId))
                    {
                        playerOverlays[playerId].RemoveEffect(effect.Type);
                    }
                }
            }

            // Remove expired effects
            foreach (var effect in effectsToRemove)
            {
                effects.Remove(effect);
            }

            // Remove player entry if no effects remain
            if (effects.Count == 0)
            {
                ActiveEffects.Remove(playerId);
            }
        }
    }

    /// <summary>
    /// Update stink bomb cooldowns
    /// </summary>
    /// <param name="delta">Frame delta time</param>
    private void UpdateStinkBombCooldowns(float delta)
    {
        var playersToUpdate = new List<int>(stinkBombCooldowns.Keys);

        foreach (int playerId in playersToUpdate)
        {
            stinkBombCooldowns[playerId] -= delta;

            if (stinkBombCooldowns[playerId] <= 0)
            {
                stinkBombCooldowns.Remove(playerId);
                GD.Print($"SabotageManager: Stink bomb cooldown expired for player {playerId}");
            }
        }
    }

    /// <summary>
    /// Update item spawn timers
    /// </summary>
    /// <param name="delta">Frame delta time</param>
    private void UpdateItemSpawns(float delta)
    {
        foreach (var spawn in itemSpawns.Values)
        {
            spawn.Update(delta);
        }
    }

    /// <summary>
    /// Apply egg throw sabotage to target player
    /// </summary>
    /// <param name="sourcePlayerId">Player throwing the egg</param>
    /// <param name="targetPlayerId">Player being targeted</param>
    /// <param name="targetPosition">Position where egg hits</param>
    public void ApplyEggThrow(int sourcePlayerId, int targetPlayerId, Vector2 targetPosition)
    {
        GD.Print($"SabotageManager: Player {sourcePlayerId} throwing egg at player {targetPlayerId}");

        // Get source player's throw power
        var sourcePlayerData = GameManager.Instance?.GetPlayer(sourcePlayerId);
        if (sourcePlayerData == null)
        {
            GD.PrintErr("SabotageManager: Could not find source player data");
            return;
        }

        // Calculate coverage based on throw power (20% to 80% from PRD)
        float coverage = sourcePlayerData.GetThrowPowerCoverage();

        // 🔥 NEW: Check if this is a Nakama multiplayer game
        var matchManager = MatchManager.Instance;
        if (matchManager?.HasActiveMatch == true)
        {
            GD.Print("SabotageManager: Nakama game detected - sending egg throw via network");

            // Send to Nakama for multiplayer synchronization
            _ = matchManager.SendEggThrow(sourcePlayerId, targetPlayerId, targetPosition, coverage);

            // Record XP for the throwing player immediately (local action)
            sourcePlayerData.AddSabotageXP(SabotageType.EggThrow);

            GD.Print($"SabotageManager: Egg throw sent via Nakama - Player {sourcePlayerId} -> Player {targetPlayerId}");
        }
        else
        {
            // Fallback: Local-only application (single player or ENet)
            GD.Print("SabotageManager: Local game detected - applying egg effect locally");
            ApplyLocalEggEffect(sourcePlayerId, targetPlayerId, targetPosition, coverage);
        }
    }

    /// <summary>
    /// Apply egg effect locally (for single player, ENet, or when receiving from Nakama)
    /// </summary>
    /// <param name="sourcePlayerId">Player throwing the egg</param>
    /// <param name="targetPlayerId">Player being targeted</param>
    /// <param name="targetPosition">Position where egg hits</param>
    /// <param name="coverage">Coverage percentage based on throw power</param>
    public void ApplyLocalEggEffect(int sourcePlayerId, int targetPlayerId, Vector2 targetPosition, float coverage)
    {
        GD.Print($"SabotageManager: Applying local egg effect - Player {sourcePlayerId} -> Player {targetPlayerId} with {coverage:P1} coverage");

        // Create egg effect
        var eggEffect = new SabotageEffect(SabotageType.EggThrow, float.MaxValue, coverage); // Egg lasts until cleaned

        // Add to active effects
        if (!ActiveEffects.ContainsKey(targetPlayerId))
        {
            ActiveEffects[targetPlayerId] = new List<SabotageEffect>();
        }
        ActiveEffects[targetPlayerId].Add(eggEffect);

        // Apply egg splat overlay
        if (playerOverlays.ContainsKey(targetPlayerId))
        {
            playerOverlays[targetPlayerId].ApplyEggSplat(coverage, targetPosition);
        }

        // 🔥 ENHANCED: Record being hit with concise logging to avoid overflow
        var targetPlayerData = GameManager.Instance?.GetPlayer(targetPlayerId);
        if (targetPlayerData != null)
        {
            // 🔥 CONCISE: Log essential stats before/after
            int timesEggedBefore = targetPlayerData.TimesEgged;
            int composureXPBefore = targetPlayerData.ComposureXP;

            // Record the sabotage hit - this should increment TimesEgged and add Composure XP
            targetPlayerData.RecordSabotageHit(SabotageType.EggThrow);

            // 🔥 CONCISE: Log essential changes only
            int timesEggedAfter = targetPlayerData.TimesEgged;
            int composureXPAfter = targetPlayerData.ComposureXP;

            GD.Print($"🥚 EGG HIT P{targetPlayerId}: Egged {timesEggedBefore}→{timesEggedAfter}, XP {composureXPBefore}→{composureXPAfter}");

            if (timesEggedAfter == timesEggedBefore || composureXPAfter == composureXPBefore)
            {
                GD.PrintErr($"❌ STATS NOT UPDATED for P{targetPlayerId}!");
            }
        }
        else
        {
            GD.PrintErr($"❌ Player {targetPlayerId} data not found!");
        }

        GD.Print($"SabotageManager: Local egg effect applied with {coverage:P1} coverage");

        EmitSignal(SignalName.SabotageApplied, targetPlayerId, (int)SabotageType.EggThrow);
        EmitSignal(SignalName.EggThrown, sourcePlayerId, targetPlayerId, targetPosition);
    }

    /// <summary>
    /// Apply visual-only egg effect (for non-victim clients in multiplayer)
    /// This applies visual effects but does NOT update victim's stats to prevent conflicts
    /// </summary>
    /// <param name="sourcePlayerId">Player throwing the egg</param>
    /// <param name="targetPlayerId">Player being targeted</param>
    /// <param name="targetPosition">Position where egg hits</param>
    /// <param name="coverage">Coverage percentage based on throw power</param>
    public void ApplyVisualOnlyEggEffect(int sourcePlayerId, int targetPlayerId, Vector2 targetPosition, float coverage)
    {
        GD.Print($"SabotageManager: Applying VISUAL-ONLY egg effect - Player {sourcePlayerId} -> Player {targetPlayerId} with {coverage:P1} coverage (NO STATS UPDATE)");

        // Create egg effect for visual tracking only
        var eggEffect = new SabotageEffect(SabotageType.EggThrow, float.MaxValue, coverage); // Egg lasts until cleaned

        // Add to active effects for visual consistency
        if (!ActiveEffects.ContainsKey(targetPlayerId))
        {
            ActiveEffects[targetPlayerId] = new List<SabotageEffect>();
        }
        ActiveEffects[targetPlayerId].Add(eggEffect);

        // Apply egg splat overlay
        if (playerOverlays.ContainsKey(targetPlayerId))
        {
            playerOverlays[targetPlayerId].ApplyEggSplat(coverage, targetPosition);
        }

        // 🔥 CRITICAL: DO NOT update victim's stats here - only the victim's own client should do that
        GD.Print($"SabotageManager: 👀 VISUAL-ONLY effect applied - victim's stats will be updated on their own client");

        GD.Print($"SabotageManager: Visual-only egg effect applied with {coverage:P1} coverage");

        EmitSignal(SignalName.SabotageApplied, targetPlayerId, (int)SabotageType.EggThrow);
        EmitSignal(SignalName.EggThrown, sourcePlayerId, targetPlayerId, targetPosition);
    }

    /// <summary>
    /// Apply stink bomb sabotage to players in radius
    /// </summary>
    /// <param name="sourcePlayerId">Player dropping the bomb</param>
    /// <param name="bombPosition">Position where bomb is dropped</param>
    /// <param name="playerPositions">Current positions of all players</param>
    public void ApplyStinkBomb(int sourcePlayerId, Vector2 bombPosition, Dictionary<int, Vector2> playerPositions)
    {
        GD.Print($"SabotageManager: Player {sourcePlayerId} dropping stink bomb at {bombPosition}");

        // Check cooldown
        if (stinkBombCooldowns.ContainsKey(sourcePlayerId))
        {
            GD.Print($"SabotageManager: Player {sourcePlayerId} stink bomb on cooldown");
            return;
        }

        // Start cooldown
        stinkBombCooldowns[sourcePlayerId] = StinkBombCooldown;

        // Create stink bomb with timer
        CreateStinkBombTimer(sourcePlayerId, bombPosition, playerPositions);

        EmitSignal(SignalName.StinkBombDropped, sourcePlayerId, bombPosition);

        GD.Print("SabotageManager: Stink bomb timer started");
    }

    /// <summary>
    /// Create stink bomb timer and handle explosion
    /// </summary>
    /// <param name="sourcePlayerId">Player who dropped the bomb</param>
    /// <param name="bombPosition">Bomb position</param>
    /// <param name="playerPositions">Player positions when bomb was dropped</param>
    private void CreateStinkBombTimer(int sourcePlayerId, Vector2 bombPosition, Dictionary<int, Vector2> playerPositions)
    {
        var timer = new Timer();
        timer.WaitTime = StinkBombWarningTime;
        timer.OneShot = true;
        AddChild(timer);

        timer.Timeout += () =>
        {
            ExplodeStinkBomb(sourcePlayerId, bombPosition, playerPositions);
            timer.QueueFree();
        };

        timer.Start();
    }

    /// <summary>
    /// Explode stink bomb and affect players in radius
    /// </summary>
    /// <param name="sourcePlayerId">Player who dropped the bomb</param>
    /// <param name="bombPosition">Bomb position</param>
    /// <param name="playerPositions">Player positions at explosion time</param>
    private void ExplodeStinkBomb(int sourcePlayerId, Vector2 bombPosition, Dictionary<int, Vector2> playerPositions)
    {
        GD.Print($"SabotageManager: Stink bomb exploding at {bombPosition}");

        var affectedPlayers = new List<int>();

        // Get source player data for XP
        var sourcePlayerData = GameManager.Instance?.GetPlayer(sourcePlayerId);

        // Check each player's position
        foreach (var kvp in playerPositions)
        {
            int playerId = kvp.Key;
            Vector2 playerPosition = kvp.Value;

            float distance = bombPosition.DistanceTo(playerPosition);

            if (distance <= StinkBombRadius)
            {
                affectedPlayers.Add(playerId);

                // Get target player data for composure calculation
                var targetPlayerData = GameManager.Instance?.GetPlayer(playerId);
                if (targetPlayerData != null)
                {
                    // Calculate blur intensity based on composure (100% at level 1, 50% at level 10)
                    float blurIntensity = targetPlayerData.GetBlurStrength();

                    // Create stink bomb effect
                    var stinkEffect = new SabotageEffect(SabotageType.StinkBomb, SabotageEffectDuration, blurIntensity);

                    // Add to active effects
                    if (!ActiveEffects.ContainsKey(playerId))
                    {
                        ActiveEffects[playerId] = new List<SabotageEffect>();
                    }
                    ActiveEffects[playerId].Add(stinkEffect);

                    // Apply fog overlay
                    if (playerOverlays.ContainsKey(playerId))
                    {
                        playerOverlays[playerId].ApplyStinkFog(blurIntensity, SabotageEffectDuration);
                    }

                    // 🔥 CONCISE: Record stink bomb hit with essential logging
                    int timesStinkBombedBefore = targetPlayerData.TimesStinkBombed;
                    int composureXPBefore = targetPlayerData.ComposureXP;

                    // Record the sabotage hit - this should increment TimesStinkBombed and add Composure XP
                    targetPlayerData.RecordSabotageHit(SabotageType.StinkBomb);

                    // Log essential changes only
                    int timesStinkBombedAfter = targetPlayerData.TimesStinkBombed;
                    int composureXPAfter = targetPlayerData.ComposureXP;

                    GD.Print($"💨 STINK HIT P{playerId}: Bombed {timesStinkBombedBefore}→{timesStinkBombedAfter}, XP {composureXPBefore}→{composureXPAfter}");

                    if (timesStinkBombedAfter == timesStinkBombedBefore || composureXPAfter == composureXPBefore)
                    {
                        GD.PrintErr($"❌ STINK STATS NOT UPDATED for P{playerId}!");
                    }

                    GD.Print($"SabotageManager: Player {playerId} affected by stink bomb (distance: {distance:F1}, blur: {blurIntensity:P1})");

                    EmitSignal(SignalName.SabotageApplied, playerId, (int)SabotageType.StinkBomb);
                }
            }
        }

        // Award XP to source player if bomb affected someone
        if (affectedPlayers.Count > 0 && sourcePlayerData != null)
        {
            sourcePlayerData.AddSabotageXP(SabotageType.StinkBomb);
        }

        EmitSignal(SignalName.StinkBombExploded, bombPosition);

        GD.Print($"SabotageManager: Stink bomb affected {affectedPlayers.Count} players");
    }

    /// <summary>
    /// Clean egg sabotage effect (washable at sink)
    /// </summary>
    /// <param name="playerId">Player cleaning the effect</param>
    public void CleanEggEffect(int playerId)
    {
        GD.Print($"SabotageManager: Player {playerId} cleaning egg effect");

        if (!ActiveEffects.ContainsKey(playerId))
        {
            GD.Print($"SabotageManager: Player {playerId} has no active effects to clean");
            return;
        }

        // Remove egg effects
        var effects = ActiveEffects[playerId];
        var eggsToRemove = effects.Where(e => e.Type == SabotageType.EggThrow).ToList();

        foreach (var eggEffect in eggsToRemove)
        {
            effects.Remove(eggEffect);
            GD.Print($"SabotageManager: Removed egg effect for player {playerId}");
        }

        // Remove overlay
        if (playerOverlays.ContainsKey(playerId))
        {
            playerOverlays[playerId].RemoveEffect(SabotageType.EggThrow);
        }

        // Clean up empty effects list
        if (effects.Count == 0)
        {
            ActiveEffects.Remove(playerId);
        }

        EmitSignal(SignalName.SabotageCleaned, playerId, (int)SabotageType.EggThrow);

        GD.Print("SabotageManager: Egg effect cleaned successfully");
    }

    /// <summary>
    /// Check if player can pick up item at position
    /// </summary>
    /// <param name="playerId">Player attempting pickup</param>
    /// <param name="position">Position to check</param>
    /// <param name="interactionRadius">Interaction radius</param>
    /// <returns>Item type if available, null otherwise</returns>
    public ItemType? CheckItemPickup(int playerId, Vector2 position, float interactionRadius)
    {
        foreach (var kvp in itemSpawns)
        {
            Vector2 spawnPosition = kvp.Key;
            ItemSpawn spawn = kvp.Value;

            if (position.DistanceTo(spawnPosition) <= interactionRadius)
            {
                if (spawn.CanPickup())
                {
                    return spawn.ItemType;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Pick up item at position
    /// </summary>
    /// <param name="playerId">Player picking up item</param>
    /// <param name="position">Position to pick up from</param>
    /// <param name="interactionRadius">Interaction radius</param>
    /// <returns>True if item was picked up</returns>
    public bool PickupItem(int playerId, Vector2 position, float interactionRadius)
    {
        foreach (var kvp in itemSpawns)
        {
            Vector2 spawnPosition = kvp.Key;
            ItemSpawn spawn = kvp.Value;

            if (position.DistanceTo(spawnPosition) <= interactionRadius)
            {
                if (spawn.CanPickup())
                {
                    spawn.PickupItem();

                    GD.Print($"SabotageManager: Player {playerId} picked up {spawn.ItemType} at {spawnPosition}");

                    EmitSignal(SignalName.ItemPickedUp, playerId, (int)spawn.ItemType);

                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Get active sabotage effects for a player
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <returns>List of active effects</returns>
    public List<SabotageEffect> GetActiveEffects(int playerId)
    {
        return ActiveEffects.GetValueOrDefault(playerId, new List<SabotageEffect>());
    }

    /// <summary>
    /// Check if player has specific sabotage effect
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <param name="sabotageType">Sabotage type to check</param>
    /// <returns>True if player has the effect</returns>
    public bool HasSabotageEffect(int playerId, SabotageType sabotageType)
    {
        if (!ActiveEffects.ContainsKey(playerId))
            return false;

        return ActiveEffects[playerId].Any(e => e.Type == sabotageType);
    }

    /// <summary>
    /// Get stink bomb cooldown remaining for player
    /// </summary>
    /// <param name="playerId">Player ID</param>
    /// <returns>Cooldown remaining in seconds</returns>
    public float GetStinkBombCooldownRemaining(int playerId)
    {
        return stinkBombCooldowns.GetValueOrDefault(playerId, 0.0f);
    }

    /// <summary>
    /// Get available item spawns near position
    /// </summary>
    /// <param name="position">Position to check</param>
    /// <param name="radius">Search radius</param>
    /// <returns>List of available items</returns>
    public List<ItemAvailability> GetNearbyItems(Vector2 position, float radius)
    {
        var nearbyItems = new List<ItemAvailability>();

        foreach (var kvp in itemSpawns)
        {
            Vector2 spawnPosition = kvp.Key;
            ItemSpawn spawn = kvp.Value;

            if (position.DistanceTo(spawnPosition) <= radius)
            {
                nearbyItems.Add(new ItemAvailability
                {
                    ItemType = spawn.ItemType,
                    Position = spawnPosition,
                    Available = spawn.CanPickup(),
                    RespawnTime = spawn.RespawnTimeRemaining
                });
            }
        }

        return nearbyItems;
    }

    public override void _ExitTree()
    {
        GD.Print("SabotageManager: Sabotage system shutting down");

        // Clean up overlays
        foreach (var overlay in playerOverlays.Values)
        {
            overlay.Dispose();
        }
        playerOverlays.Clear();

        // Clear active effects
        ActiveEffects.Clear();
        stinkBombCooldowns.Clear();

        GD.Print("SabotageManager: Sabotage system shutdown complete");
    }
}

/// <summary>
/// Represents an active sabotage effect on a player
/// </summary>
public class SabotageEffect
{
    public SabotageType Type { get; set; }
    public float Duration { get; set; }
    public float Intensity { get; set; }
    public DateTime StartTime { get; set; }

    public SabotageEffect(SabotageType type, float duration, float intensity)
    {
        Type = type;
        Duration = duration;
        Intensity = intensity;
        StartTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Type} - Duration: {Duration:F1}s, Intensity: {Intensity:P1}";
    }
}

/// <summary>
/// Manages obstruction overlays for a player
/// </summary>
public class ObstructionOverlay
{
    public int PlayerId { get; private set; }
    private Dictionary<SabotageType, OverlayEffect> activeOverlays = new();

    public ObstructionOverlay(int playerId)
    {
        PlayerId = playerId;
        GD.Print($"ObstructionOverlay: Created for player {playerId}");
    }

    /// <summary>
    /// Apply egg splat overlay
    /// </summary>
    /// <param name="coverage">Coverage percentage (0.0 to 1.0)</param>
    /// <param name="position">Impact position</param>
    public void ApplyEggSplat(float coverage, Vector2 position)
    {
        GD.Print($"ObstructionOverlay: Applying egg splat with {coverage:P1} coverage at {position}");

        activeOverlays[SabotageType.EggThrow] = new OverlayEffect
        {
            Type = SabotageType.EggThrow,
            Intensity = coverage,
            Position = position,
            Duration = float.MaxValue // Egg lasts until cleaned
        };

        // TODO: Apply actual visual overlay
    }

    /// <summary>
    /// Apply stink fog overlay
    /// </summary>
    /// <param name="blurIntensity">Blur intensity (0.0 to 1.0)</param>
    /// <param name="duration">Effect duration</param>
    public void ApplyStinkFog(float blurIntensity, float duration)
    {
        GD.Print($"ObstructionOverlay: Applying stink fog with {blurIntensity:P1} blur for {duration}s");

        activeOverlays[SabotageType.StinkBomb] = new OverlayEffect
        {
            Type = SabotageType.StinkBomb,
            Intensity = blurIntensity,
            Duration = duration
        };

        // TODO: Apply actual visual overlay
    }

    /// <summary>
    /// Remove overlay effect
    /// </summary>
    /// <param name="sabotageType">Type of effect to remove</param>
    public void RemoveEffect(SabotageType sabotageType)
    {
        if (activeOverlays.ContainsKey(sabotageType))
        {
            activeOverlays.Remove(sabotageType);
            GD.Print($"ObstructionOverlay: Removed {sabotageType} overlay for player {PlayerId}");

            // TODO: Remove actual visual overlay
        }
    }

    /// <summary>
    /// Get active overlay effects
    /// </summary>
    /// <returns>List of active effects</returns>
    public List<OverlayEffect> GetActiveEffects()
    {
        return activeOverlays.Values.ToList();
    }

    /// <summary>
    /// Dispose of overlay resources
    /// </summary>
    public void Dispose()
    {
        activeOverlays.Clear();
        GD.Print($"ObstructionOverlay: Disposed for player {PlayerId}");
    }
}

/// <summary>
/// Manages item spawning at specific locations
/// </summary>
public class ItemSpawn
{
    public ItemType ItemType { get; private set; }
    public float RespawnTime { get; private set; }
    public int MaxItems { get; private set; }
    public int CurrentItems { get; private set; }
    public float RespawnTimeRemaining { get; private set; }

    public ItemSpawn(ItemType itemType, float respawnTime, int maxItems)
    {
        ItemType = itemType;
        RespawnTime = respawnTime;
        MaxItems = maxItems;
        CurrentItems = maxItems; // Start with full items
        RespawnTimeRemaining = 0.0f;
    }

    /// <summary>
    /// Check if item can be picked up
    /// </summary>
    /// <returns>True if item is available</returns>
    public bool CanPickup()
    {
        return CurrentItems > 0;
    }

    /// <summary>
    /// Pick up an item
    /// </summary>
    public void PickupItem()
    {
        if (CurrentItems > 0)
        {
            CurrentItems--;

            if (CurrentItems == 0 && RespawnTime > 0)
            {
                RespawnTimeRemaining = RespawnTime;
            }
        }
    }

    /// <summary>
    /// Update spawn timer
    /// </summary>
    /// <param name="delta">Frame delta time</param>
    public void Update(float delta)
    {
        if (RespawnTimeRemaining > 0)
        {
            RespawnTimeRemaining -= delta;

            if (RespawnTimeRemaining <= 0)
            {
                CurrentItems = MaxItems;
                RespawnTimeRemaining = 0.0f;
            }
        }
    }
}

/// <summary>
/// Item types available for pickup
/// </summary>
public enum ItemType
{
    Egg,
    StinkBomb,
    Sink
}

/// <summary>
/// Overlay effect data
/// </summary>
public class OverlayEffect
{
    public SabotageType Type { get; set; }
    public float Intensity { get; set; }
    public Vector2 Position { get; set; }
    public float Duration { get; set; }
}

/// <summary>
/// Item availability information
/// </summary>
public class ItemAvailability
{
    public ItemType ItemType { get; set; }
    public Vector2 Position { get; set; }
    public bool Available { get; set; }
    public float RespawnTime { get; set; }
}