# AWS to Nakama Migration Plan
**SoreLosers Multiplayer Card Game - Complete Architecture Migration**  
*Created: 2025-01-18*

---

## 🚨 **MIGRATION OVERVIEW**

**Current State**: Failed AWS dedicated server with persistent RPC conflicts  
**Target State**: Nakama-powered multiplayer with web deployment capability  
**Migration Goal**: Replace complex AWS infrastructure with Nakama's hosted solution  
**Timeline**: 3-5 development sessions  

### **Why Migrate to Nakama?**

| Current AWS Issues | Nakama Solutions |
|-------------------|------------------|
| ❌ RPC checksum failures | ✅ Built-in message handling |
| ❌ Complex dual architecture | ✅ Single client architecture |
| ❌ EC2 costs & maintenance | ✅ Free tier (100 concurrent users) |
| ❌ Manual deployment process | ✅ Simple Docker deployment |
| ❌ Web export limitations | ✅ Perfect WebSocket support |
| ❌ Custom room code system | ✅ Built-in matchmaking |

---

## 📋 **PHASE 1: INFRASTRUCTURE ASSESSMENT**

### **Current AWS Architecture to Remove**
```
[Client] → IPv4 Bridge (7778) → IPv6 Server (7777) → [DedicatedServer.cs]
                ↓
        [GameSession Management]
        [Room Code Generation]  
        [RPC Conflict Resolution]
```

### **Target Nakama Architecture**
```
[Client] → WebSocket → [Nakama Server] → [Match Management]
                              ↓
                    [Built-in Matchmaking]
                    [Real-time Multiplayer]
                    [User Authentication]
```

### **Files to Remove/Archive**
- ✅ `scripts/DedicatedServer.cs` - Replace with Nakama client calls
- ✅ `scenes/DedicatedServer.tscn` - No longer needed
- ✅ `AWS_MIGRATION.md` - Archive as historical document
- ✅ `build_server.sh` - Replace with client-only builds
- ✅ `deploy_to_aws.sh` - Remove AWS deployment
- ✅ `configure_security_group.sh` - No longer needed
- ✅ `server_config.json` - Replace with Nakama config
- ✅ `test_server.sh` - No longer needed
- ✅ `project_server.godot` - Single project configuration

### **Current RPC Methods to Migrate**
**29 Total RPC Methods Identified:**
- NetworkManager.cs: 12 methods
- DedicatedServer.cs: 8 methods  
- CardManager.cs: 8 methods
- GameManager.cs: 1 method

---

## 📋 **PHASE 2: NAKAMA SETUP & CONFIGURATION**

### **Step 1: Nakama Server Setup**

#### **Option A: Local Development (Recommended for testing)**
```bash
# Create docker-compose.yml
version: '3.8'
services:
  nakama:
    image: heroiclabs/nakama:3.18.0
    entrypoint:
      - "/bin/sh"
      - "-ecx"
      - >
          /nakama/nakama --name nakama1 --database.address root@cockroachdb:26257 --logger.level INFO --session.token_expiry_sec 7200 --socket.server_key "sorelosers_server_key"
    restart: unless-stopped
    links:
      - "cockroachdb:db"
    depends_on:
      cockroachdb:
        condition: service_healthy
    ports:
      - "7349:7349"
      - "7350:7350"
      - "7351:7351"
    healthcheck:
      test: ["CMD", "/nakama/nakama", "healthcheck"]
      interval: 10s
      timeout: 5s
      retries: 5
  cockroachdb:
    image: cockroachdb/cockroach:latest-v23.1
    command: start-single-node --insecure --store=attrs=ssd,path=/var/lib/cockroach/
    restart: unless-stopped
    volumes:
      - data:/var/lib/cockroach
    ports:
      - "8080:8080"
      - "26257:26257"
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost:8080/health?ready=1"]
      interval: 3s
      timeout: 3s
      retries: 5
volumes:
  data:
```

#### **Option B: Heroic Cloud (Recommended for production)**
```bash
# Sign up at https://heroiclabs.com/cloud
# Create new project
# Note connection details:
# - Server: https://your-project.heroiclabs.cloud
# - Server Key: your-generated-key
```

### **Step 2: Godot Nakama Client Installation**
```bash
# Download from: https://github.com/heroiclabs/nakama-godot
# Add to project: addons/nakama/
# Enable in Project Settings > Plugins
```

### **Step 3: Connection Configuration**
```gdscript
# Create scripts/NakamaManager.cs (replaces NetworkManager AWS logic)
public partial class NakamaManager : Node
{
    [Export] public string ServerKey = "sorelosers_server_key";
    [Export] public string Host = "127.0.0.1";  // or Heroic Cloud URL
    [Export] public int Port = 7350;
    [Export] public bool UseSSL = false;  // true for Heroic Cloud
    
    public NakamaClient Client { get; private set; }
    public NakamaSession Session { get; private set; }
    public NakamaSocket Socket { get; private set; }
    public NakamaMatch CurrentMatch { get; private set; }
}
```

---

## 📋 **PHASE 3: AUTHENTICATION SYSTEM**

### **Current vs Nakama Authentication**

| Current System | Nakama Replacement |
|----------------|-------------------|
| No authentication | Email/password or device ID |
| Manual room codes | Automatic matchmaking |
| Local player storage | Server-side user accounts |

### **Step 1: Replace Connection System**
```gdscript
# Replace scripts/MainMenuUI.cs connection logic
public partial class MainMenuUI : Control
{
    private NakamaManager nakamaManager;
    
    public async void OnHostGamePressed()
    {
        // Replace AWS connection with Nakama authentication
        await AuthenticateAndCreateMatch();
    }
    
    private async Task AuthenticateAndCreateMatch()
    {
        // Authenticate with device ID (for simplicity)
        var deviceId = OS.GetUniqueId();
        var session = await nakamaManager.Client.AuthenticateDeviceAsync(deviceId, create: true);
        nakamaManager.Session = session;
        
        // Create match using Nakama
        var socket = nakamaManager.Client.NewSocket();
        await socket.ConnectAsync(session);
        nakamaManager.Socket = socket;
        
        var match = await socket.CreateMatchAsync();
        nakamaManager.CurrentMatch = match;
        
        // Transition to lobby
        GetTree().ChangeSceneToFile("res://scenes/Lobby.tscn");
    }
}
```

### **Step 2: Remove Room Code System**
```gdscript
# Remove from scripts/NetworkManager.cs:
# - GenerateRoomCode()
# - CurrentRoomCode property
# - Room code validation logic

# Replace with Nakama match IDs (automatic)
public string GetMatchId() => nakamaManager.CurrentMatch?.MatchId ?? "";
```

---

## 📋 **PHASE 4: MATCHMAKING REPLACEMENT**

### **Current Room System → Nakama Matches**

| Current Feature | Nakama Equivalent |
|----------------|-------------------|
| 6-digit room codes | Match IDs |
| Manual room creation | CreateMatchAsync() |
| Room joining by code | JoinMatchAsync() |
| Host/client detection | Built-in presence system |

### **Step 1: Create Match Management**
```gdscript
# New: scripts/MatchManager.cs
public partial class MatchManager : Node
{
    private NakamaManager nakama;
    
    public async Task<bool> CreateMatch()
    {
        try
        {
            var match = await nakama.Socket.CreateMatchAsync();
            nakama.CurrentMatch = match;
            
            // Listen for players joining
            nakama.Socket.ReceivedMatchPresence += OnMatchPresenceEvent;
            nakama.Socket.ReceivedMatchState += OnMatchStateReceived;
            
            return true;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"Failed to create match: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> JoinMatch(string matchId)
    {
        try
        {
            var match = await nakama.Socket.JoinMatchAsync(matchId);
            nakama.CurrentMatch = match;
            return true;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"Failed to join match: {ex.Message}");
            return false;
        }
    }
    
    // Replace complex RPC system with simple match state
    public async void SendCardPlay(int playerId, string cardSuit, string cardRank)
    {
        var state = new CardPlayState
        {
            PlayerId = playerId,
            Suit = cardSuit,
            Rank = cardRank,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };
        
        var json = JsonSerializer.Serialize(state);
        await nakama.Socket.SendMatchStateAsync(nakama.CurrentMatch.MatchId, 1, json);
    }
}
```

### **Step 2: Remove Complex RPC System**
```gdscript
# Delete/Replace these RPC methods:
# NetworkManager.cs (12 methods) → Simple match state messages
# DedicatedServer.cs (8 methods) → Remove entirely  
# CardManager.cs (8 methods) → 2-3 simple state messages
# GameManager.cs (1 method) → Simple game start message

# New simplified message system:
public enum MatchOpCode
{
    PlayerJoined = 1,
    PlayerReady = 2,
    GameStart = 3,
    CardPlayed = 4,
    TurnChange = 5,
    GameEnd = 6
}
```

---

## 📋 **PHASE 5: GAME STATE SYNCHRONIZATION**

### **Current RPC Architecture → Nakama Match State**

| Current System | Nakama Replacement |
|----------------|-------------------|
| 29 RPC methods | 6 message types |
| Host/client validation | Client-authoritative with presence |
| Complex checksums | Built-in message delivery |
| Manual reconnection | Automatic presence management |

### **Step 1: Simplify Card Game Logic**
```gdscript
# Replace scripts/CardManager.cs RPC methods with:
public partial class CardManager : Node
{
    private MatchManager matchManager;
    
    // Replace NetworkPlayCard RPC
    public async void PlayCard(Card card)
    {
        var message = new
        {
            type = "card_played",
            player_id = GetLocalPlayerId(),
            suit = card.Suit.ToString(),
            rank = card.Rank.ToString(),
            position = GetPlayerPosition()
        };
        
        await matchManager.SendMatchState(MatchOpCode.CardPlayed, message);
    }
    
    // Replace NetworkSyncDealtHands RPC  
    public async void SyncGameStart(List<int> playerIds)
    {
        var message = new
        {
            type = "game_start",
            player_ids = playerIds,
            dealer = currentDealer,
            seed = deckSeed
        };
        
        await matchManager.SendMatchState(MatchOpCode.GameStart, message);
    }
    
    // Single message handler replaces all RPC receivers
    private void OnMatchStateReceived(IMatchState matchState)
    {
        var opCode = (MatchOpCode)matchState.OpCode;
        var json = System.Text.Encoding.UTF8.GetString(matchState.State);
        var data = JsonNode.Parse(json);
        
        switch (opCode)
        {
            case MatchOpCode.CardPlayed:
                HandleCardPlayed(data);
                break;
            case MatchOpCode.GameStart:
                HandleGameStart(data);
                break;
            // ... handle other message types
        }
    }
}
```

### **Step 2: Remove Host/Client Architecture**
```gdscript
# Remove from scripts/GameManager.cs:
# - IsHost/IsClient detection
# - Host-only validation logic
# - Complex player synchronization

# Replace with presence-based system:
public bool IsMatchOwner()
{
    var presences = nakama.CurrentMatch.Presences;
    var oldestPresence = presences.OrderBy(p => p.JoinedAt).First();
    return oldestPresence.UserId == nakama.Session.UserId;
}
```

---

## 📋 **PHASE 6: WEB DEPLOYMENT OPTIMIZATION**

### **Current Limitations → Nakama Solutions**

| AWS Web Issues | Nakama Solutions |
|----------------|------------------|
| No raw UDP in browser | WebSocket transport |
| HTTPS requirement | Built-in WSS support |
| CORS issues | Proper web headers |
| IPv4/IPv6 bridge complexity | Simple WebSocket connection |

### **Step 1: Web Export Configuration**
```gdscript
# Update export settings for HTML5:
# - Enable Godot 4.x web export
# - Configure WebSocket-only transport
# - Remove ENet dependency for web builds

# scripts/NakamaManager.cs - Web-compatible connection:
public async Task ConnectAsync()
{
    var scheme = UseSSL ? "https" : "http";
    Client = new NakamaClient(scheme, Host, Port, ServerKey);
    
    // Authenticate (device ID works great for web)
    var deviceId = GetWebDeviceId();
    Session = await Client.AuthenticateDeviceAsync(deviceId, create: true);
    
    // WebSocket connection (works in all browsers)
    Socket = Client.NewSocket(useMainThread: true);
    await Socket.ConnectAsync(Session);
}

private string GetWebDeviceId()
{
    // Use browser fingerprint for web builds
    return OS.HasFeature("web") ? 
        GenerateBrowserFingerprint() : 
        OS.GetUniqueId();
}
```

### **Step 2: Itch.io Deployment Prep**
```bash
# Build script for web deployment:
#!/bin/bash
echo "Building SoreLosers for Itch.io (Nakama)"

# Single web build (no server needed!)
godot --headless --export-release "HTML5" builds/sorelosers_web.html

# Zip for Itch.io
cd builds
zip -r sorelosers_itch.zip sorelosers_web.html *.js *.wasm *.pck

echo "Ready for Itch.io upload: builds/sorelosers_itch.zip"
```

---

## 📋 **PHASE 7: TESTING & VALIDATION**

### **Migration Testing Checklist**

#### **✅ Phase 1: Basic Connection**
- [ ] Nakama server running locally
- [ ] Client connects via WebSocket
- [ ] Device authentication working
- [ ] Match creation successful

#### **✅ Phase 2: Matchmaking**
- [ ] Host can create matches
- [ ] Players can join by match ID
- [ ] Presence events received correctly
- [ ] Multiple players in same match

#### **✅ Phase 3: Game Flow**
- [ ] Lobby system functional
- [ ] Card dealing synchronized
- [ ] Turn progression working
- [ ] Card plays appear on all clients

#### **✅ Phase 4: Web Deployment**
- [ ] HTML5 export loads in browser
- [ ] WebSocket connection stable
- [ ] Multiplayer working in browsers
- [ ] No CORS or security issues

#### **✅ Phase 5: Production Readiness**
- [ ] Heroic Cloud deployment
- [ ] Itch.io upload successful
- [ ] Public multiplayer testing
- [ ] Performance acceptable

### **Testing Script**
```bash
#!/bin/bash
echo "SoreLosers Nakama Migration Testing"

# Test 1: Local Nakama server
docker-compose up -d
sleep 10
curl -f http://localhost:7350/ || echo "❌ Nakama server not responding"

# Test 2: Build client
godot --headless --build-solutions --quit
godot --headless --export-release "HTML5" builds/test.html

# Test 3: Basic connection test
echo "Manual testing required:"
echo "1. Open builds/test.html in browser"
echo "2. Click 'Host Game'"
echo "3. Open second browser tab"
echo "4. Click 'Join Game' with match ID"
echo "5. Verify both players see each other"
```

---

## 📋 **PHASE 8: DEPLOYMENT & CLEANUP**

### **Step 1: Remove AWS Infrastructure**
```bash
# Cleanup AWS resources:
# 1. Terminate EC2 instance (3.16.16.22)
# 2. Release Elastic IP
# 3. Delete security groups
# 4. Remove SSH key pair

# Remove AWS-related files:
rm AWS_MIGRATION.md
rm build_server.sh
rm deploy_to_aws.sh
rm configure_security_group.sh
rm server_config.json
rm test_server.sh
rm project_server.godot
rm -rf scripts/DedicatedServer.cs*
rm -rf scenes/DedicatedServer.tscn
```

### **Step 2: Update Documentation**
```markdown
# New README.md section:
## 🌐 **Multiplayer (Nakama)**

### **Local Development**
```bash
# Start Nakama server
docker-compose up -d

# Build and run game
godot --headless --build-solutions --quit
godot --
```

### **Web Deployment (Itch.io)**
1. Build: `godot --headless --export-release "HTML5" builds/web.html`
2. Upload `builds/` folder to Itch.io
3. Configure as HTML5 game
4. Set viewport to 1024x768
5. Enable fullscreen

### **Production (Heroic Cloud)**
1. Sign up at https://heroiclabs.com/cloud
2. Create project, note server URL and key
3. Update NakamaManager.cs with production settings
4. Deploy to Itch.io with production config
```

### **Step 3: Cost Analysis**
| AWS Costs (Monthly) | Nakama Costs (Monthly) |
|-------------------|----------------------|
| EC2 t3.micro: $8.50 | Heroic Cloud Free: $0 |
| Elastic IP: $3.65 | (Up to 100 concurrent) |
| Data transfer: $2-5 | Additional users: $0.10/CCU |
| **Total: ~$15-20** | **Total: $0-10** |

---

## 📋 **IMPLEMENTATION TIMELINE**

### **Session 1 (2-3 hours): Foundation**
- [ ] Install Nakama locally via Docker
- [ ] Add Nakama client to Godot project
- [ ] Create basic NakamaManager.cs
- [ ] Test basic connection and authentication

### **Session 2 (2-3 hours): Matchmaking**
- [ ] Replace room code system with Nakama matches
- [ ] Update MainMenuUI.cs and LobbyUI.cs
- [ ] Test match creation and joining
- [ ] Verify presence events

### **Session 3 (3-4 hours): Game State**
- [ ] Replace RPC system with match state messages
- [ ] Update CardManager.cs message handling
- [ ] Remove DedicatedServer.cs entirely
- [ ] Test card game functionality

### **Session 4 (2-3 hours): Web Deployment**
- [ ] Configure HTML5 export settings
- [ ] Test WebSocket connectivity in browsers
- [ ] Build production-ready web version
- [ ] Upload to Itch.io for testing

### **Session 5 (1-2 hours): Production & Cleanup**
- [ ] Set up Heroic Cloud account
- [ ] Deploy production configuration
- [ ] Remove all AWS infrastructure
- [ ] Update documentation

**Total Estimated Time: 10-15 hours**

---

## 🎯 **SUCCESS METRICS**

### **Technical Goals**
- ✅ Zero RPC checksum failures
- ✅ Sub-100ms latency for card plays
- ✅ Works in all major browsers
- ✅ Supports 4 concurrent players per match
- ✅ Automatic reconnection handling

### **Business Goals**
- ✅ Deployable to Itch.io
- ✅ Zero infrastructure costs (free tier)
- ✅ Single codebase (no client/server split)
- ✅ Web-native multiplayer experience
- ✅ Scalable to hundreds of concurrent matches

### **User Experience Goals**
- ✅ One-click "Host Game" functionality
- ✅ Share match ID to invite friends
- ✅ Automatic matchmaking option
- ✅ Persistent user accounts (optional)
- ✅ Works on mobile browsers

---

## 🚀 **NEXT STEPS**

1. **Start Migration**: Begin with Session 1 implementation
2. **Parallel Development**: Keep current AWS system as fallback during migration
3. **Incremental Testing**: Test each phase thoroughly before proceeding
4. **Community Feedback**: Deploy web version early for community testing
5. **Performance Optimization**: Monitor and optimize based on real usage

**Ready to begin? Start with Phase 2: Nakama Setup & Configuration!**

---

*This migration plan transforms SoreLosers from a complex AWS-dependent multiplayer game into a simple, web-deployable, cost-effective Nakama-powered experience perfect for Itch.io distribution.* 