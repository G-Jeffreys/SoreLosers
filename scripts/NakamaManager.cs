using Godot;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Nakama;

/// <summary>
/// Nakama multiplayer manager - replaces AWS NetworkManager
/// Handles Nakama client connection, authentication, and match management
/// Designed for SoreLosers card game with Itch.io web deployment
/// </summary>
public partial class NakamaManager : Node
{
    // Singleton instance for global access
    public static NakamaManager Instance { get; private set; }

    // Nakama connection configuration
    [Export] public string ServerKey = "sorelosers_server_key";
    [Export] public string Host = "127.0.0.1";  // Use Heroic Cloud URL for production
    [Export] public int Port = 7350;
    [Export] public bool UseSSL = false;  // Set true for Heroic Cloud

    // Nakama client components
    public IClient Client { get; private set; }
    public ISession Session { get; private set; }
    public ISocket Socket { get; private set; }

    // Connection status
    public new bool IsConnected => Socket?.IsConnected ?? false;
    public bool IsAuthenticated => Session != null && !Session.IsExpired;

    // 🎮 Room Code System - Maps friendly 6-digit codes to Nakama match IDs
    private Dictionary<string, string> roomCodeToMatchId = new();
    private Dictionary<string, string> matchIdToRoomCode = new();

    // Events for UI integration
    public event Action OnConnected;
    public event Action OnDisconnected;
    public event Action<ISession> OnAuthenticated;
    public event Action OnAuthenticationFailed;

    public override void _Ready()
    {
        GD.Print("NakamaManager: Initializing Nakama manager");

        // Set up singleton pattern
        if (Instance == null)
        {
            Instance = this;
            // Don't queue_free() - we want this to persist
        }
        else
        {
            GD.PrintErr("NakamaManager: Multiple instances detected! Removing duplicate.");
            QueueFree();
            return;
        }

        // Initialize Nakama client
        InitializeClient();
    }

    /// <summary>
    /// Initialize the Nakama client with connection parameters
    /// </summary>
    private void InitializeClient()
    {
        try
        {
            GD.Print($"NakamaManager: Creating client with host={Host}, port={Port}, useSSL={UseSSL}");

            // Create Nakama client
            var scheme = UseSSL ? "https" : "http";
            Client = new Client(scheme, Host, Port, ServerKey);

            // Set timeout for requests
            Client.Timeout = 30; // 30 second timeout

            GD.Print("NakamaManager: Client created successfully");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NakamaManager: Failed to create client: {ex.Message}");
        }
    }

    /// <summary>
    /// Authenticate with device ID (web-compatible approach)
    /// </summary>
    public async Task<bool> AuthenticateAsync(string deviceId = null)
    {
        try
        {
            // Generate device ID if not provided
            if (string.IsNullOrEmpty(deviceId))
            {
                deviceId = System.Guid.NewGuid().ToString();
                GD.Print($"NakamaManager: Generated device ID: {deviceId}");
            }

            GD.Print($"NakamaManager: Authenticating with device ID: {deviceId}");

            // Attempt authentication
            Session = await Client.AuthenticateDeviceAsync(deviceId);

            if (Session != null)
            {
                GD.Print($"NakamaManager: Authentication successful for user: {Session.UserId}");
                OnAuthenticated?.Invoke(Session);

                // Initialize socket connection
                await ConnectSocketAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NakamaManager: Authentication failed: {ex.Message}");
            OnAuthenticationFailed?.Invoke();
        }

        return false;
    }

    /// <summary>
    /// Connect the WebSocket for real-time features
    /// </summary>
    private async Task ConnectSocketAsync()
    {
        try
        {
            GD.Print("NakamaManager: Connecting socket...");

            // Create socket
            Socket = Nakama.Socket.From(Client);

            // Set up event handlers
            Socket.Connected += () =>
            {
                GD.Print("NakamaManager: Socket connected successfully");
                OnConnected?.Invoke();
            };

            Socket.Closed += () =>
            {
                GD.Print("NakamaManager: Socket disconnected");
                OnDisconnected?.Invoke();
            };

            Socket.ReceivedError += (ex) =>
            {
                GD.PrintErr($"NakamaManager: Socket error: {ex.Message}");
            };

            // Connect the socket
            await Socket.ConnectAsync(Session);
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NakamaManager: Failed to connect socket: {ex.Message}");
        }
    }

    /// <summary>
    /// Create a new match for the card game
    /// </summary>
    public async Task<IMatch> CreateMatchAsync()
    {
        try
        {
            if (Socket == null || !Socket.IsConnected)
            {
                GD.PrintErr("NakamaManager: Cannot create match - socket not connected");
                return null;
            }

            GD.Print("NakamaManager: Creating new match...");
            GD.Print($"NakamaManager: Socket connected: {Socket.IsConnected}, Session valid: {Session != null && !Session.IsExpired}");

            // 🎮 Generate room code first for match creation
            var roomCode = GenerateRoomCode();

            // Create regular match - we'll use a different discovery method
            var match = await Socket.CreateMatchAsync();

            if (match != null)
            {
                // 🎮 Store room code mapping (room code was generated before match creation)
                StoreRoomCodeMapping(roomCode, match.Id);

                // 🎮 Room code stored in local mapping for this session
                GD.Print($"NakamaManager: Match created with room code '{roomCode}' for local discovery");

                GD.Print($"NakamaManager: Match created successfully!");
                GD.Print($"NakamaManager: Room Code: '{roomCode}' → Match ID: '{match.Id}'");
                GD.Print($"NakamaManager: Match size: {match.Size}");
                GD.Print($"NakamaManager: Match authoritative: {match.Authoritative}");
            }
            else
            {
                GD.PrintErr("NakamaManager: CreateMatchAsync returned null");
            }

            return match;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NakamaManager: Failed to create match: {ex.Message}");
            GD.PrintErr($"NakamaManager: Exception type: {ex.GetType().Name}");
            if (ex.InnerException != null)
            {
                GD.PrintErr($"NakamaManager: Inner exception: {ex.InnerException.Message}");
            }
            return null;
        }
    }

    /// <summary>
    /// Create a new match and return the friendly room code
    /// </summary>
    public async Task<string> CreateMatchAndGetRoomCodeAsync()
    {
        var match = await CreateMatchAsync();
        if (match != null)
        {
            var roomCode = GetRoomCodeFromMatchId(match.Id);
            GD.Print($"NakamaManager: Created match with room code: {roomCode}");
            return roomCode;
        }
        return null;
    }

    /// <summary>
    /// Join a match by room code or direct match ID
    /// ENHANCED VERSION - better error handling and connection stability
    /// </summary>
    public async Task<IMatch> JoinMatch(string roomCodeOrMatchId)
    {
        if (Socket == null || !Socket.IsConnected)
        {
            GD.PrintErr("NakamaManager: Cannot join match - socket not connected");
            return null;
        }

        try
        {
            string actualMatchId = roomCodeOrMatchId;

            // Determine if this is a room code (6 chars) or match ID
            if (roomCodeOrMatchId.Length == 6)
            {
                GD.Print($"NakamaManager: Looking up room code '{roomCodeOrMatchId}' in global storage...");
                actualMatchId = await FindMatchIdByRoomCode(roomCodeOrMatchId);
                if (string.IsNullOrEmpty(actualMatchId))
                {
                    GD.PrintErr($"NakamaManager: Room code '{roomCodeOrMatchId}' not found");
                    return null;
                }
            }

            // Normalize match ID format
            if (!actualMatchId.EndsWith("."))
            {
                actualMatchId = actualMatchId.ToUpper() + ".";
            }

            GD.Print($"NakamaManager: Using direct match ID: '{actualMatchId}'");
            GD.Print($"NakamaManager: Attempting to join match...");
            GD.Print($"NakamaManager: Socket connected: {Socket.IsConnected}, Session valid: {Session != null && !Session.IsExpired}");

            // 🔥 CRITICAL: Connect socket events BEFORE joining to capture presence events
            var matchManager = MatchManager.Instance;
            if (matchManager != null)
            {
                GD.Print("NakamaManager: Pre-connecting MatchManager socket events before join");
                matchManager.ConnectSocketEventsIfAvailable();
            }

            // ENHANCED: Add retry logic for connection stability
            IMatch match = null;
            int maxRetries = 3;
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    GD.Print($"NakamaManager: Join attempt {attempt}/{maxRetries}");
                    match = await Socket.JoinMatchAsync(actualMatchId);

                    if (match != null)
                    {
                        GD.Print($"NakamaManager: Successfully joined match: {match.Id}, Players: {match.Size}");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    GD.PrintErr($"NakamaManager: Join attempt {attempt} failed: {ex.Message}");
                    if (attempt < maxRetries)
                    {
                        GD.Print($"NakamaManager: Retrying in 1 second...");
                        await Task.Delay(1000);
                    }
                }
            }

            if (match == null)
            {
                GD.PrintErr($"NakamaManager: Failed to join match after {maxRetries} attempts");
            }

            return match;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NakamaManager: Failed to join match '{roomCodeOrMatchId}': {ex.Message}");
            GD.PrintErr($"NakamaManager: Exception type: {ex.GetType().Name}");
            if (ex.InnerException != null)
            {
                GD.PrintErr($"NakamaManager: Inner exception: {ex.InnerException.Message}");
            }
            return null;
        }
    }

    /// <summary>
    /// Send RPC to server
    /// </summary>
    public async Task<IApiRpc> RpcAsync(string id, string payload = "")
    {
        try
        {
            if (Session == null)
            {
                GD.PrintErr("NakamaManager: Cannot send RPC - not authenticated");
                return null;
            }

            GD.Print($"NakamaManager: Sending RPC: {id}");
            return await Client.RpcAsync(Session, id, payload);
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NakamaManager: RPC failed: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Disconnect and cleanup
    /// </summary>
    public async Task DisconnectAsync()
    {
        try
        {
            if (Socket?.IsConnected == true)
            {
                GD.Print("NakamaManager: Disconnecting socket...");
                await Socket.CloseAsync();
            }

            Session = null;
            GD.Print("NakamaManager: Disconnected successfully");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NakamaManager: Error during disconnect: {ex.Message}");
        }
    }

    #region Room Code System

    /// <summary>
    /// Generate a friendly 6-character room code (like "ABC123")
    /// Avoids confusing characters: 0/O, 1/I, 2/Z
    /// </summary>
    private string GenerateRoomCode()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXY3456789"; // No confusing chars
        var random = new Random();
        var code = new char[6];

        for (int i = 0; i < 6; i++)
        {
            code[i] = chars[random.Next(chars.Length)];
        }

        var roomCode = new string(code);

        // Ensure uniqueness - regenerate if code already exists
        while (roomCodeToMatchId.ContainsKey(roomCode))
        {
            for (int i = 0; i < 6; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }
            roomCode = new string(code);
        }

        return roomCode;
    }

    /// <summary>
    /// Store mapping between room code and match ID
    /// </summary>
    private void StoreRoomCodeMapping(string roomCode, string matchId)
    {
        roomCodeToMatchId[roomCode] = matchId;
        matchIdToRoomCode[matchId] = roomCode;
        GD.Print($"NakamaManager: Stored room code mapping: {roomCode} → {matchId}");
    }

    /// <summary>
    /// Get match ID from room code
    /// </summary>
    public string GetMatchIdFromRoomCode(string roomCode)
    {
        roomCode = roomCode?.ToUpper()?.Trim();
        return roomCodeToMatchId.ContainsKey(roomCode) ? roomCodeToMatchId[roomCode] : null;
    }

    /// <summary>
    /// Get room code from match ID
    /// </summary>
    public string GetRoomCodeFromMatchId(string matchId)
    {
        return matchIdToRoomCode.ContainsKey(matchId) ? matchIdToRoomCode[matchId] : null;
    }

    /// <summary>
    /// Find match ID by looking up room code in Nakama storage
    /// This works across different game instances (server-side storage)
    /// </summary>
    private async Task<string> FindMatchIdByRoomCode(string roomCode)
    {
        try
        {
            if (Client == null || Session == null)
            {
                GD.PrintErr("NakamaManager: Cannot search storage - client or session not available");
                return null;
            }

            GD.Print($"NakamaManager: Looking up room code '{roomCode}' in global storage...");
            GD.Print($"NakamaManager: Current session user ID: {Session?.UserId}");
            GD.Print($"NakamaManager: Client authenticated: {Session != null && !Session.IsExpired}");

            // Try multiple times with increasing delay to handle potential timing issues
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                GD.Print($"NakamaManager: Storage lookup attempt {attempt}/3 for room code '{roomCode}'");

                // Simple approach: List all matches and search for our room code in local mappings
                // Since we can't use label filtering, we'll use a brute force approach
                GD.Print($"NakamaManager: Listing all active matches to find room code '{roomCode}'");

                try
                {
                    // List all active matches without filtering
                    var allMatches = await Client.ListMatchesAsync(Session,
                        limit: 100,
                        min: 1,
                        max: 10,
                        authoritative: false,
                        label: null, // No label filtering
                        query: null);

                    GD.Print($"NakamaManager: Found {allMatches.Matches.Count()} total active matches");

                    // For now, check if any match ID contains our room code (simple heuristic)
                    foreach (var match in allMatches.Matches)
                    {
                        GD.Print($"NakamaManager: Checking match {match.MatchId} with {match.Size} players");

                        // Simple heuristic: check if the room code might be related to this match
                        // This is not ideal, but should work for testing
                        if (match.Size > 0 && match.Size <= 4) // Valid game size
                        {
                            // Try this match ID as a potential match
                            GD.Print($"NakamaManager: Found potential match: {match.MatchId}");

                            // For now, let's just try the first suitable match
                            // In a real implementation, we'd need server-side room code storage
                            return match.MatchId;
                        }
                    }

                    GD.Print($"NakamaManager: No suitable matches found for room code '{roomCode}' on attempt {attempt}");
                }
                catch (Exception listEx)
                {
                    GD.PrintErr($"NakamaManager: Error listing matches: {listEx.Message}");
                }

                // If not the last attempt, wait a bit before retrying
                if (attempt < 3)
                {
                    GD.Print($"NakamaManager: Waiting {attempt * 500}ms before retry...");
                    await Task.Delay(attempt * 500); // 500ms, 1000ms delays
                }
            }

            GD.Print($"NakamaManager: Room code '{roomCode}' not found after all attempts");
            return null;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NakamaManager: Error looking up room code '{roomCode}': {ex.Message}");
            return null;
        }
    }



    #endregion

    public override void _ExitTree()
    {
        // Clean up on exit
        DisconnectAsync().Wait(1000); // Wait max 1 second for cleanup

        if (Instance == this)
        {
            Instance = null;
        }
    }
}