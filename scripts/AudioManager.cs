using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Audio manager for SoreLosers game - handles background music and sound effects
/// Integrates with GameManager to play context-appropriate music during different game phases
/// Supports volume controls, looping, and cross-fade transitions
/// </summary>
public partial class AudioManager : Node
{
    // Singleton instance for global access
    public static AudioManager Instance { get; private set; }

    // Audio configuration
    [Export] public float MasterVolume { get; set; } = 0.8f;  // 80% default volume
    [Export] public float MusicVolume { get; set; } = 0.6f;   // 60% music volume (less intrusive)
    [Export] public float SFXVolume { get; set; } = 0.8f;     // 80% sound effects volume

    // Audio fade configuration
    [Export] public float FadeDuration = 2.0f;  // 2 second crossfades
    [Export] public bool EnableLooping = true;  // Enable music looping by default

    // Audio players - using multiple players for smooth crossfading
    private AudioStreamPlayer backgroundMusicPlayer;
    private AudioStreamPlayer backgroundMusicPlayer2; // For crossfading between tracks
    private AudioStreamPlayer sfxPlayer;
    private AudioStreamPlayer uiSfxPlayer; // Separate player for UI sounds

    // Current audio state
    private AudioStream currentBackgroundMusic;
    private bool isPlayingBackgroundMusic = false;
    private bool isFading = false;
    private AudioStreamPlayer activeMusicPlayer;
    private AudioStreamPlayer inactiveMusicPlayer;

    // Audio stream cache for performance
    private Dictionary<string, AudioStream> audioStreamCache = new();

    // Music tracks paths - using the existing Sneaky Snitch and placeholder system
    private readonly Dictionary<string, string> musicTracks = new()
    {
        ["sneaky_snitch"] = "res://audio/music/Sneaky Snitch.mp3",
        ["menu_music"] = "res://assets/audio/music/menu_background.ogg",
        ["gameplay_music"] = "res://assets/audio/music/gameplay_background.ogg"
    };

    // SFX paths - using existing audio assets
    private readonly Dictionary<string, string> sfxSounds = new()
    {
        ["button_click"] = "res://assets/audio/sfx/button_click.wav",
        ["card_place"] = "res://assets/audio/sfx/card_place.wav",
        ["card_shuffle"] = "res://assets/audio/sfx/card_shuffle.wav",
        ["egg_splat"] = "res://assets/audio/sfx/egg_splat.ogg",
        ["stink_bomb"] = "res://assets/audio/sfx/stink_bomb.ogg",
        ["footstep"] = "res://assets/audio/sfx/footstep.ogg"
    };

    // Events for audio system communication
    [Signal]
    public delegate void MusicStartedEventHandler(string trackName);

    [Signal]
    public delegate void MusicStoppedEventHandler();

    [Signal]
    public delegate void VolumeChangedEventHandler(float masterVolume, float musicVolume, float sfxVolume);

    public override void _Ready()
    {
        GD.Print("AudioManager: Initializing audio management system");

        // Set up singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GD.PrintErr("AudioManager: Multiple instances detected! Destroying duplicate.");
            QueueFree();
            return;
        }

        // Initialize audio players
        InitializeAudioPlayers();

        // Load audio streams into cache
        LoadAudioStreams();

        // Connect to GameManager for phase-based music changes
        ConnectToGameManager();

        // Start with Sneaky Snitch as default background music
        CallDeferred(nameof(StartDefaultBackgroundMusic));

        GD.Print("AudioManager: Audio system initialized successfully");
    }

    /// <summary>
    /// Initialize all audio stream players with proper configuration
    /// </summary>
    private void InitializeAudioPlayers()
    {
        GD.Print("AudioManager: Setting up audio stream players");

        // Background music player 1 (primary)
        backgroundMusicPlayer = new AudioStreamPlayer();
        backgroundMusicPlayer.Name = "BackgroundMusicPlayer";
        backgroundMusicPlayer.Bus = "Music"; // Use Music audio bus for separate volume control
        AddChild(backgroundMusicPlayer);

        // Background music player 2 (for crossfading)
        backgroundMusicPlayer2 = new AudioStreamPlayer();
        backgroundMusicPlayer2.Name = "BackgroundMusicPlayer2";
        backgroundMusicPlayer2.Bus = "Music";
        AddChild(backgroundMusicPlayer2);

        // Sound effects player
        sfxPlayer = new AudioStreamPlayer();
        sfxPlayer.Name = "SFXPlayer";
        sfxPlayer.Bus = "SFX"; // Use SFX audio bus for separate volume control
        AddChild(sfxPlayer);

        // UI sound effects player (for menu clicks, etc.)
        uiSfxPlayer = new AudioStreamPlayer();
        uiSfxPlayer.Name = "UISFXPlayer";
        uiSfxPlayer.Bus = "UI"; // Use UI audio bus
        AddChild(uiSfxPlayer);

        // Set initial active music player
        activeMusicPlayer = backgroundMusicPlayer;
        inactiveMusicPlayer = backgroundMusicPlayer2;

        // Apply initial volume settings
        UpdateVolumes();

        GD.Print("AudioManager: Audio players configured successfully");
    }

    /// <summary>
    /// Load all audio streams into cache for performance
    /// </summary>
    private void LoadAudioStreams()
    {
        GD.Print("AudioManager: Loading audio streams into cache");

        // Load music tracks
        foreach (var track in musicTracks)
        {
            LoadAudioStreamToCache(track.Key, track.Value);
        }

        // Load SFX sounds  
        foreach (var sound in sfxSounds)
        {
            LoadAudioStreamToCache(sound.Key, sound.Value);
        }

        GD.Print($"AudioManager: Loaded {audioStreamCache.Count} audio streams into cache");
    }

    /// <summary>
    /// Load individual audio stream to cache with error handling
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="path">Resource path</param>
    private void LoadAudioStreamToCache(string key, string path)
    {
        try
        {
            // Check if file exists first (handles .placeholder files gracefully)
            if (!FileAccess.FileExists(path))
            {
                GD.Print($"AudioManager: Audio file not found, skipping: {path}");
                return;
            }

            var audioStream = GD.Load<AudioStream>(path);
            if (audioStream != null)
            {
                audioStreamCache[key] = audioStream;
                GD.Print($"AudioManager: ✅ Loaded audio: {key} -> {path}");
            }
            else
            {
                GD.PrintErr($"AudioManager: ❌ Failed to load audio stream: {path}");
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"AudioManager: ❌ Exception loading audio {path}: {ex.Message}");
        }
    }

    /// <summary>
    /// Connect to GameManager for phase-based music management
    /// </summary>
    private void ConnectToGameManager()
    {
        var gameManager = GameManager.Instance;
        if (gameManager != null)
        {
            gameManager.PhaseChanged += OnGamePhaseChanged;
            GD.Print("AudioManager: Connected to GameManager phase changes");
        }
        else
        {
            GD.Print("AudioManager: GameManager not available yet - will connect later");
            // Try connecting again after a delay
            GetTree().CreateTimer(1.0f).Timeout += ConnectToGameManager;
        }
    }

    /// <summary>
    /// Handle game phase changes to play appropriate music
    /// </summary>
    /// <param name="newPhase">New game phase</param>
    private void OnGamePhaseChanged(int newPhase)
    {
        var phase = (GameManager.GamePhase)newPhase;
        GD.Print($"AudioManager: Game phase changed to {phase} - updating background music");

        switch (phase)
        {
            case GameManager.GamePhase.MainMenu:
                PlayBackgroundMusic("sneaky_snitch"); // Use Sneaky Snitch for main menu
                break;
            case GameManager.GamePhase.HostLobby:
            case GameManager.GamePhase.ClientLobby:
                PlayBackgroundMusic("sneaky_snitch"); // Continue Sneaky Snitch in lobby
                break;
            case GameManager.GamePhase.CardPhase:
            case GameManager.GamePhase.RealTimePhase:
                PlayBackgroundMusic("sneaky_snitch"); // Sneaky Snitch during gameplay
                break;
            case GameManager.GamePhase.Results:
                // Could add victory/defeat music here in the future
                PlayBackgroundMusic("sneaky_snitch");
                break;
        }
    }

    /// <summary>
    /// Start default background music (Sneaky Snitch)
    /// </summary>
    private void StartDefaultBackgroundMusic()
    {
        GD.Print("AudioManager: Starting default background music (Sneaky Snitch)");
        PlayBackgroundMusic("sneaky_snitch");
    }

    /// <summary>
    /// Play background music with optional crossfading
    /// </summary>
    /// <param name="trackName">Music track identifier</param>
    /// <param name="fadeDuration">Override fade duration (optional)</param>
    public void PlayBackgroundMusic(string trackName, float? fadeDuration = null)
    {
        if (!audioStreamCache.ContainsKey(trackName))
        {
            GD.PrintErr($"AudioManager: Music track '{trackName}' not found in cache");
            return;
        }

        var newTrack = audioStreamCache[trackName];

        // Don't restart if already playing the same track
        if (currentBackgroundMusic == newTrack && isPlayingBackgroundMusic)
        {
            GD.Print($"AudioManager: Already playing {trackName} - skipping");
            return;
        }

        GD.Print($"AudioManager: Playing background music: {trackName}");

        var fadeTime = fadeDuration ?? FadeDuration;

        if (isPlayingBackgroundMusic && !isFading && fadeTime > 0)
        {
            // Crossfade to new track
            CrossfadeToNewTrack(newTrack, fadeTime);
        }
        else
        {
            // Direct play (no current music or fading disabled)
            PlayTrackDirectly(newTrack);
        }

        currentBackgroundMusic = newTrack;
        isPlayingBackgroundMusic = true;
        EmitSignal(SignalName.MusicStarted, trackName);
    }

    /// <summary>
    /// Crossfade between current and new background music track
    /// </summary>
    /// <param name="newTrack">New audio stream to play</param>
    /// <param name="fadeTime">Crossfade duration</param>
    private void CrossfadeToNewTrack(AudioStream newTrack, float fadeTime)
    {
        if (isFading) return; // Prevent multiple simultaneous fades

        isFading = true;
        GD.Print($"AudioManager: Crossfading to new track over {fadeTime} seconds");

        // Start new track on inactive player
        inactiveMusicPlayer.Stream = newTrack;
        inactiveMusicPlayer.VolumeDb = -80.0f; // Start silent
        inactiveMusicPlayer.Play();

        // Create tweener for crossfade
        var tween = CreateTween();
        tween.SetParallel(true); // Allow parallel tweening

        // Fade out current track
        tween.TweenMethod(Callable.From<float>(SetActiveMusicVolume), 0.0f, -80.0f, fadeTime);

        // Fade in new track
        tween.TweenMethod(Callable.From<float>(SetInactiveMusicVolume), -80.0f, 0.0f, fadeTime);

        // Complete crossfade
        tween.TweenCallback(Callable.From(CompleteCrossfade)).SetDelay(fadeTime);
    }

    /// <summary>
    /// Play track directly without crossfading
    /// </summary>
    /// <param name="track">Audio stream to play</param>
    private void PlayTrackDirectly(AudioStream track)
    {
        StopBackgroundMusic(false); // Stop current music without fade

        activeMusicPlayer.Stream = track;
        activeMusicPlayer.VolumeDb = 0.0f;
        activeMusicPlayer.Play();

        GD.Print("AudioManager: Started background music directly");
    }

    /// <summary>
    /// Complete crossfade transition
    /// </summary>
    private void CompleteCrossfade()
    {
        // Stop the old track
        activeMusicPlayer.Stop();

        // Swap active/inactive players
        (activeMusicPlayer, inactiveMusicPlayer) = (inactiveMusicPlayer, activeMusicPlayer);

        isFading = false;
        GD.Print("AudioManager: Crossfade completed successfully");
    }

    /// <summary>
    /// Set volume for active music player (used during crossfade)
    /// </summary>
    /// <param name="volumeDb">Volume in decibels</param>
    private void SetActiveMusicVolume(float volumeDb)
    {
        if (activeMusicPlayer != null)
            activeMusicPlayer.VolumeDb = volumeDb;
    }

    /// <summary>
    /// Set volume for inactive music player (used during crossfade)
    /// </summary>
    /// <param name="volumeDb">Volume in decibels</param>
    private void SetInactiveMusicVolume(float volumeDb)
    {
        if (inactiveMusicPlayer != null)
            inactiveMusicPlayer.VolumeDb = volumeDb;
    }

    /// <summary>
    /// Stop background music with optional fade out
    /// </summary>
    /// <param name="fadeOut">Whether to fade out (default: true)</param>
    public void StopBackgroundMusic(bool fadeOut = true)
    {
        if (!isPlayingBackgroundMusic) return;

        GD.Print($"AudioManager: Stopping background music (fade: {fadeOut})");

        if (fadeOut && FadeDuration > 0)
        {
            // Fade out current music
            var tween = CreateTween();
            tween.TweenMethod(Callable.From<float>(SetActiveMusicVolume), 0.0f, -80.0f, FadeDuration);
            tween.TweenCallback(Callable.From(CompleteStop)).SetDelay(FadeDuration);
        }
        else
        {
            // Stop immediately
            CompleteStop();
        }
    }

    /// <summary>
    /// Complete music stop
    /// </summary>
    private void CompleteStop()
    {
        activeMusicPlayer.Stop();
        inactiveMusicPlayer.Stop();
        isPlayingBackgroundMusic = false;
        currentBackgroundMusic = null;
        EmitSignal(SignalName.MusicStopped);
        GD.Print("AudioManager: Background music stopped");
    }

    /// <summary>
    /// Play sound effect
    /// </summary>
    /// <param name="soundName">Sound effect identifier</param>
    /// <param name="volumeOverride">Override volume (optional)</param>
    public void PlaySFX(string soundName, float? volumeOverride = null)
    {
        if (!audioStreamCache.ContainsKey(soundName))
        {
            GD.Print($"AudioManager: SFX '{soundName}' not found in cache (might be placeholder)");
            return;
        }

        var stream = audioStreamCache[soundName];
        var player = soundName.Contains("button") || soundName.Contains("ui") ? uiSfxPlayer : sfxPlayer;

        player.Stream = stream;

        // Apply volume override if specified
        if (volumeOverride.HasValue)
        {
            player.VolumeDb = Mathf.LinearToDb(volumeOverride.Value);
        }
        else
        {
            player.VolumeDb = 0.0f; // Default volume
        }

        player.Play();

        GD.Print($"AudioManager: Played SFX: {soundName}");
    }

    /// <summary>
    /// Update all audio volumes based on current settings
    /// </summary>
    public void UpdateVolumes()
    {
        // Convert linear volume to decibel
        var masterDb = Mathf.LinearToDb(MasterVolume);
        var musicDb = Mathf.LinearToDb(MusicVolume);
        var sfxDb = Mathf.LinearToDb(SFXVolume);

        // Update audio bus volumes - AudioServer is static in Godot 4

        // Get bus indices
        var masterBusIdx = AudioServer.GetBusIndex("Master");
        var musicBusIdx = AudioServer.GetBusIndex("Music");
        var sfxBusIdx = AudioServer.GetBusIndex("SFX");
        var uiBusIdx = AudioServer.GetBusIndex("UI");

        // Set bus volumes
        AudioServer.SetBusVolumeDb(masterBusIdx, masterDb);
        AudioServer.SetBusVolumeDb(musicBusIdx, musicDb);
        AudioServer.SetBusVolumeDb(sfxBusIdx, sfxDb);
        AudioServer.SetBusVolumeDb(uiBusIdx, sfxDb); // UI uses same as SFX

        EmitSignal(SignalName.VolumeChanged, MasterVolume, MusicVolume, SFXVolume);

        GD.Print($"AudioManager: Updated volumes - Master: {MasterVolume:F2}, Music: {MusicVolume:F2}, SFX: {SFXVolume:F2}");
    }

    /// <summary>
    /// Set master volume
    /// </summary>
    /// <param name="volume">Volume (0.0 to 1.0)</param>
    public void SetMasterVolume(float volume)
    {
        MasterVolume = Mathf.Clamp(volume, 0.0f, 1.0f);
        UpdateVolumes();
    }

    /// <summary>
    /// Set music volume
    /// </summary>
    /// <param name="volume">Volume (0.0 to 1.0)</param>
    public void SetMusicVolume(float volume)
    {
        MusicVolume = Mathf.Clamp(volume, 0.0f, 1.0f);
        UpdateVolumes();
    }

    /// <summary>
    /// Set SFX volume
    /// </summary>
    /// <param name="volume">Volume (0.0 to 1.0)</param>
    public void SetSFXVolume(float volume)
    {
        SFXVolume = Mathf.Clamp(volume, 0.0f, 1.0f);
        UpdateVolumes();
    }

    /// <summary>
    /// Toggle background music on/off
    /// </summary>
    public void ToggleBackgroundMusic()
    {
        if (isPlayingBackgroundMusic)
        {
            StopBackgroundMusic();
        }
        else
        {
            StartDefaultBackgroundMusic();
        }
    }

    /// <summary>
    /// Get current music status
    /// </summary>
    /// <returns>True if background music is playing</returns>
    public bool IsPlayingBackgroundMusic()
    {
        return isPlayingBackgroundMusic;
    }

    /// <summary>
    /// Get current background music name
    /// </summary>
    /// <returns>Current track name or null</returns>
    public string GetCurrentMusicTrack()
    {
        if (currentBackgroundMusic == null) return null;

        // Find track name by comparing audio stream
        foreach (var track in musicTracks)
        {
            if (audioStreamCache.ContainsKey(track.Key) && audioStreamCache[track.Key] == currentBackgroundMusic)
            {
                return track.Key;
            }
        }

        return "unknown";
    }

    /// <summary>
    /// Cleanup on exit
    /// </summary>
    public override void _ExitTree()
    {
        StopBackgroundMusic(false); // Stop without fade on exit
        GD.Print("AudioManager: Audio system cleaned up");
    }
}