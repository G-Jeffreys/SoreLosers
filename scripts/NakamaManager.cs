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

            var match = await Socket.CreateMatchAsync();

            if (match != null)
            {
                // 🎮 Generate friendly room code and store mapping
                var roomCode = GenerateRoomCode();
                StoreRoomCodeMapping(roomCode, match.Id);

                // 🌐 Store room code in Nakama's global storage for cross-instance access
                var storageSuccess = await StoreRoomCodeInStorage(roomCode, match.Id);
                if (!storageSuccess)
                {
                    GD.PrintErr($"NakamaManager: Warning - Failed to store room code '{roomCode}' in global storage. Room may not be discoverable by other players.");
                }

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
    /// Join an existing match by room code or match ID
    /// </summary>
    public async Task<IMatch> JoinMatchAsync(string roomCodeOrMatchId)
    {
        try
        {
            if (Socket == null || !Socket.IsConnected)
            {
                GD.PrintErr("NakamaManager: Cannot join match - socket not connected");
                return null;
            }

            // Validate input
            if (string.IsNullOrWhiteSpace(roomCodeOrMatchId))
            {
                GD.PrintErr("NakamaManager: Cannot join match - room code or match ID is null or empty");
                return null;
            }

            var input = roomCodeOrMatchId.Trim().ToUpper();
            string actualMatchId;

            // 🎮 Check if input is a room code (6 characters) or full match ID
            if (input.Length == 6)
            {
                // Treat as room code - search using match listing
                actualMatchId = await FindMatchIdByRoomCode(input);
                if (actualMatchId == null)
                {
                    GD.PrintErr($"NakamaManager: Room code '{input}' not found or expired");
                    return null;
                }
                GD.Print($"NakamaManager: Found room code '{input}' → Match ID: '{actualMatchId}'");
            }
            else
            {
                // Treat as full match ID
                actualMatchId = input;
                GD.Print($"NakamaManager: Using direct match ID: '{actualMatchId}'");
            }

            GD.Print($"NakamaManager: Attempting to join match...");
            GD.Print($"NakamaManager: Socket connected: {Socket.IsConnected}, Session valid: {Session != null && !Session.IsExpired}");

            var match = await Socket.JoinMatchAsync(actualMatchId);

            if (match != null)
            {
                GD.Print($"NakamaManager: Successfully joined match: {match.Id}, Players: {match.Size}");
            }
            else
            {
                GD.PrintErr("NakamaManager: JoinMatchAsync returned null");
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

                // Read from global storage collection "room_codes"
                var storageRequest = new StorageObjectId
                {
                    Collection = "room_codes",
                    Key = roomCode,
                    UserId = null // null = global storage
                };

                GD.Print($"NakamaManager: Storage request - Collection: '{storageRequest.Collection}', Key: '{storageRequest.Key}', UserId: '{storageRequest.UserId}'");

                var result = await Client.ReadStorageObjectsAsync(Session, new[] { storageRequest });

                GD.Print($"NakamaManager: Storage read result - Found {result.Objects.Count()} objects");

                if (result.Objects.Count() > 0)
                {
                    var storageObject = result.Objects.FirstOrDefault();
                    if (storageObject != null)
                    {
                        try
                        {
                            // Parse JSON value to extract match ID
                            var jsonData = JsonSerializer.Deserialize<JsonElement>(storageObject.Value);
                            var matchId = jsonData.GetProperty("matchId").GetString();

                            GD.Print($"NakamaManager: Found match ID '{matchId}' for room code '{roomCode}' on attempt {attempt}");

                            // Store the mapping for future use
                            StoreRoomCodeMapping(roomCode, matchId);

                            return matchId;
                        }
                        catch (Exception parseEx)
                        {
                            GD.PrintErr($"NakamaManager: Error parsing storage value for room code '{roomCode}': {parseEx.Message}");
                            return null;
                        }
                    }
                }
                else
                {
                    GD.Print($"NakamaManager: Room code '{roomCode}' not found in storage on attempt {attempt}");

                    // If not the last attempt, wait a bit before retrying
                    if (attempt < 3)
                    {
                        GD.Print($"NakamaManager: Waiting {attempt * 500}ms before retry...");
                        await Task.Delay(attempt * 500); // 500ms, 1000ms delays
                    }
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

    /// <summary>
    /// Store room code mapping in Nakama's global storage
    /// This allows other players to find the match using the room code
    /// </summary>
    private async Task<bool> StoreRoomCodeInStorage(string roomCode, string matchId)
    {
        try
        {
            if (Client == null || Session == null)
            {
                GD.PrintErr("NakamaManager: Cannot store room code - client or session not available");
                return false;
            }

            GD.Print($"NakamaManager: Storing room code '{roomCode}' → '{matchId}' in global storage...");
            GD.Print($"NakamaManager: Current session user ID for storage: {Session?.UserId}");

            // Write to global storage collection "room_codes"
            // Nakama requires Value to be JSON, so wrap match ID in a JSON object
            var storageData = new
            {
                matchId = matchId,
                createdAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                hostUserId = Session.UserId
            };
            var storageValue = JsonSerializer.Serialize(storageData);

            GD.Print($"NakamaManager: Storage data to write: {storageValue}");

            var writeRequest = new WriteStorageObject
            {
                Collection = "room_codes",
                Key = roomCode,
                Value = storageValue,
                PermissionRead = 2, // 2 = PublicRead (anyone can read)
                PermissionWrite = 0, // 0 = NoWrite (only creator can write)
            };

            GD.Print($"NakamaManager: Write request - Collection: '{writeRequest.Collection}', Key: '{writeRequest.Key}', PermissionRead: {writeRequest.PermissionRead}");

            await Client.WriteStorageObjectsAsync(Session, new[] { writeRequest });

            GD.Print($"NakamaManager: Successfully stored room code '{roomCode}' in storage");
            return true;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"NakamaManager: Error storing room code '{roomCode}': {ex.Message}");
            return false;
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