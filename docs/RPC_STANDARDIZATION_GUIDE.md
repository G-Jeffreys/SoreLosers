# RPC Standardization Guide
**SoreLosers Game - Comprehensive RPC Method Documentation**
*Created: 2025-01-18*

## 🚨 CRITICAL STANDARDIZATION RULE

**ALL RPC SIGNATURES MUST BE IDENTICAL ACROSS CLIENT AND SERVER**

### ✅ **STANDARDIZED RPC SIGNATURE FORMAT:**
```csharp
[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
[Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]
[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]  // Only for SendChatMessage
```

### ❌ **NEVER USE:**
```csharp
// NEVER include TransferMode parameter - causes checksum failures
[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
```

---

## 📋 **COMPLETE RPC METHOD INVENTORY**

### **NetworkManager.cs (12 methods)**

| Line | RPC Signature | Method Name | Parameters |
|------|---------------|-------------|------------|
| 224 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `SendPlayerData` | `(PlayerData playerData)` |
| 238 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `ReceivePlayerData` | `(int playerId, string playerName)` |
| 266 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `NetworkSyncPlayers` | `(int[] playerIds, string[] playerNames)` |
| 355 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]` | `SendChatMessage` | `(string message)` |
| 368 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `SendSabotageAction` | `(int sourcePlayerId, SabotageType sabotageType, Vector2 targetPosition)` |
| 380 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `SendCardPlay` | `(int playerId, int cardId)` |
| 532 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `RequestJoinGame` | `(string roomCode, string playerName)` |
| 553 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `JoinGameResponse` | `(bool success, string roomCode, string message)` |
| 585 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `PlayerJoinedGame` | `(int playerId, string playerName)` |
| 595 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `PlayerLeftGame` | `(int playerId)` |
| 605 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `GameStarted` | `(int[] playerIds)` |
| 615 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `PlayerAction` | `(string actionType, Variant[] actionData)` |

### **DedicatedServer.cs (8 methods)**

| Line | RPC Signature | Method Name | Parameters |
|------|---------------|-------------|------------|
| 130 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `RequestJoinGame` | `(string roomCode, string playerName)` |
| 169 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `JoinGameResponse` | `(bool success, string roomCode, string message)` |
| 287 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `PlayerAction` | `(string actionType, Variant[] actionData)` |
| 353 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `PlayerJoinedGame` | `(int playerId, string playerName)` |
| 356 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `PlayerLeftGame` | `(int playerId)` |
| 359 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `GameStarted` | `(int[] playerIds)` |
| 362 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `CardPlayed` | `(int playerId, Variant[] cardData)` |
| 365 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `SabotageAction` | `(int playerId, Variant[] sabotageData)` |

### **CardManager.cs (8 methods)**

| Line | RPC Signature | Method Name | Parameters |
|------|---------------|-------------|------------|
| 186 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `NetworkStartGame` | `(int[] playerIds)` |
| 458 | `[Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]` | `NetworkSyncDealtHands` | `(int[] playerIds, int[] playerCardCounts, int[] allCardSuits, int[] allCardRanks, int trickLeader, int currentTurn)` |
| 632 | `[Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]` | `NetworkTurnStarted` | `(int playerId)` |
| 642 | `[Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]` | `NetworkTimerUpdate` | `(float timeRemaining)` |
| 710 | `[Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]` | `NetworkSyncTurnChange` | `(int previousTurn, int newTurn, int trickCardCount)` |
| 774 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `NetworkPlayCard` | `(int playerId, int suitInt, int rankInt)` |
| 812 | `[Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]` | `NetworkCardPlayResult` | `(int playerId, int suitInt, int rankInt, bool success)` |
| 1004 | `[Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = false)]` | `NetworkSyncTrickComplete` | `(int winnerId, int newTrickLeader, int newCurrentTurn, int winnerScore)` |

### **GameManager.cs (1 method)**

| Line | RPC Signature | Method Name | Parameters |
|------|---------------|-------------|------------|
| 825 | `[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]` | `OnGameStarted` | `()` |

---

## 🔍 **SIGNATURE ANALYSIS**

### **Total RPC Methods: 29**
- **NetworkManager.cs**: 12 methods
- **DedicatedServer.cs**: 8 methods  
- **CardManager.cs**: 8 methods
- **GameManager.cs**: 1 method

### **RPC Mode Distribution:**
- **AnyPeer**: 23 methods (79.3%)
- **Authority**: 6 methods (20.7%)

### **CallLocal Distribution:**
- **CallLocal = false**: 28 methods (96.6%)
- **CallLocal = true**: 1 method (3.4%) - `SendChatMessage` only

---

## ⚠️ **CRITICAL MATCHING REQUIREMENTS**

### **Methods That MUST Match Exactly:**

1. **RequestJoinGame**
   - NetworkManager.cs:532 ⟷ DedicatedServer.cs:130
   - ✅ **VERIFIED MATCH**: Both use `(string roomCode, string playerName)`

2. **JoinGameResponse**  
   - NetworkManager.cs:553 ⟷ DedicatedServer.cs:169
   - ✅ **VERIFIED MATCH**: Both use `(bool success, string roomCode, string message)`

3. **PlayerJoinedGame**
   - NetworkManager.cs:585 ⟷ DedicatedServer.cs:353
   - ✅ **VERIFIED MATCH**: Both use `(int playerId, string playerName)`

4. **PlayerLeftGame**
   - NetworkManager.cs:595 ⟷ DedicatedServer.cs:356
   - ✅ **VERIFIED MATCH**: Both use `(int playerId)`

5. **GameStarted**
   - NetworkManager.cs:605 ⟷ DedicatedServer.cs:359
   - ✅ **VERIFIED MATCH**: Both use `(int[] playerIds)`

---

## 🛠️ **MAINTENANCE PROCEDURES**

### **When Adding New RPC Methods:**

1. **Define in both Client and Server files** with IDENTICAL signatures
2. **Use standardized RPC attribute format** (no TransferMode parameter)
3. **Update this documentation** immediately
4. **Test with rebuild and redeploy** before production

### **When Modifying Existing RPC Methods:**

1. **Update ALL matching methods simultaneously**
2. **Rebuild both client and server**
3. **Redeploy server to AWS**
4. **Verify no RPC checksum failures in logs**

### **RPC Checksum Failure Troubleshooting:**

1. **Search for the failing node** (e.g., "NetworkManager")
2. **Find all RPC methods** in that node across client/server
3. **Compare signatures character-by-character**
4. **Look for TransferMode differences**
5. **Rebuild and redeploy after fixing**

---

## 🚨 **CRITICAL TROUBLESHOOTING DISCOVERIES**

### **BUILD/DEPLOYMENT FAILURES**

**CRITICAL ISSUE**: Godot export process fails silently when overwriting existing build files.

**Root Cause**: The command `/Applications/Godot_mono.app/Contents/MacOS/Godot --headless --export-release "Linux Server" ./builds/sorelosers_server.x86_64` would fail silently if the target files already existed, leaving old build files in place.

**Solution**: Always delete existing build files before export:
```bash
rm -f builds/sorelosers_server.* && /Applications/Godot_mono.app/Contents/MacOS/Godot --headless --export-release "Linux Server" ./builds/sorelosers_server.x86_64
```

**Impact**: Hours of troubleshooting were wasted because code changes weren't taking effect on the server. Always verify file timestamps after export.

### **AUTOLOAD EXECUTION ORDER CONFLICTS**

**CRITICAL ISSUE**: NetworkManager loads as autoload on dedicated server, registering RPC methods before DedicatedServer can remove it.

**Execution Order on Server**:
1. `GameManager` (autoload 1)
2. **`NetworkManager`** (autoload 2) ← **RPC methods registered HERE**
3. `SabotageManager`, `UIManager` (autoloads 3, 4)  
4. `DedicatedServer` (autoload 5) ← **Removal logic runs HERE (too late)**

**Failed Solutions Attempted**:
- ❌ **QueueFree() in NetworkManager._Ready()**: RPC registration happens before _Ready()
- ❌ **Server detection logic**: Autoload execution order prevents proper removal
- ❌ **DedicatedServer removing NetworkManager**: NetworkManager already registered RPCs

**Ultimate Solution**: **Server-specific project.godot** that excludes NetworkManager entirely:
```gdscript
[autoload]
GameManager="*res://scenes/GameManager.tscn"
; NetworkManager="*res://scenes/NetworkManager.tscn"  <- REMOVED FOR SERVER
CardManager="*res://scenes/CardManager.tscn"
SabotageManager="*res://scenes/SabotageManager.tscn"
UIManager="*res://scenes/UIManager.tscn"
```

**Status**: ⚠️ **Still experiencing RPC checksum failures despite this approach**

### **RPC ARCHITECTURE INSIGHTS**

**Key Discovery**: Client-side autoloads (especially NetworkManager) should NEVER load on dedicated servers as they create RPC signature conflicts.

**Best Practice**: Use separate project configurations for client and server builds to prevent autoload conflicts.

**Warning Signs**:
- Server logs showing `NetworkManager: _Ready() called - starting initialization`
- RPC checksum failures: `"The rpc node checksum failed. Make sure to have the same methods on both nodes. Node path: NetworkManager"`
- Clients connecting then immediately disconnecting

---

## 📝 **CHANGE LOG**

| Date | Change | Files Modified | Status |
|------|--------|---------------|---------|
| 2025-01-18 | **MAJOR TROUBLESHOOTING SESSION** | Multiple files | ⚠️ **In Progress** |
| 2025-01-18 | Created server-specific project.godot | project_server.godot | ✅ Complete |
| 2025-01-18 | Fixed silent export build failures | Build process | ✅ Complete |
| 2025-01-18 | Added NetworkManager server detection | NetworkManager.cs | ✅ Complete |
| 2025-01-18 | Added DedicatedServer NetworkManager removal | DedicatedServer.cs | ❌ **Failed** |
| 2025-01-18 | Multiple QueueFree() attempts | NetworkManager.cs | ❌ **Failed** |
| 2025-01-18 | Corrected line numbers in documentation | docs/RPC_STANDARDIZATION_GUIDE.md | ✅ Complete |
| 2025-01-18 | Removed all TransferMode parameters | NetworkManager.cs, CardManager.cs, GameManager.cs | ✅ Complete |
| 2025-01-18 | Added RequestJoinGame to NetworkManager | NetworkManager.cs | ✅ Complete |
| 2025-01-18 | Initial RPC documentation | docs/RPC_STANDARDIZATION_GUIDE.md | ✅ Complete |

---

## 🚀 **NEXT STEPS & CURRENT STATUS**

### **IMMEDIATE PRIORITY**
⚠️ **RPC checksum failures persist** despite all architectural fixes. Current server status shows:
- ✅ Server running and accepting connections on port 7777
- ❌ RPC checksum failures: `"The rpc node checksum failed. Make sure to have the same methods on both nodes. Node path: NetworkManager"`
- ❌ Clients connect then immediately disconnect

### **TROUBLESHOOTING PLAN**
1. **Investigate Why Server-Specific project.godot Failed**
   - Verify the server build actually used `project_server.godot`
   - Check if NetworkManager is still loading despite being commented out
   - Consider complete architectural restructure

2. **Alternative Solutions to Consider**
   - **Option 2**: Complete network architecture simplification
   - Remove overlapping RPC methods between NetworkManager and DedicatedServer
   - Consolidate all server-side networking into DedicatedServer only

3. **Build Process Verification**
   - Always delete build files before export: `rm -f builds/sorelosers_server.*`
   - Verify file timestamps match export time
   - Deploy both `.x86_64` and `.pck` files to AWS

### **MONITORING CHECKLIST**
- [ ] **AWS Server Logs**: `sudo journalctl -u sorelosers --since '5 minutes ago' --no-pager`
- [ ] **Build File Timestamps**: Verify recent modification times
- [ ] **NetworkManager Presence**: Should NOT appear in server logs
- [ ] **RPC Checksum Errors**: Monitor for failure patterns

### **ARCHITECTURAL LESSONS LEARNED**
1. **Silent build failures** can waste hours of debugging
2. **Autoload execution order** is critical for server-side RPC management
3. **Client-side autoloads** should never run on dedicated servers
4. **Separate project configurations** may be necessary for client vs server

**Current Status**: ⚠️ **RPC Issues Unresolved** - Requires further architectural investigation

**Last Updated**: 2025-01-18 22:45 UTC (Major troubleshooting session documented)
**AWS Server**: 3.16.16.22:7777 (DedicatedServer) + 7778 (IPv4 Bridge)  
**Build Status**: ⚠️ Server-specific project.godot approach unsuccessful 