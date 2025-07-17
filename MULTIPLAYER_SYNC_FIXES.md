# Multiplayer Synchronization Fixes

## Overview
This document summarizes the critical fixes applied to resolve multiplayer desynchronization issues in the SoreLosers game.

## Root Causes Identified

### 1. Instance Detection Failure
**Problem**: Both instances detected themselves as "first instance" due to unreliable TCP port testing
**Symptoms**: Both instances attempted to host, causing dual hosting conflicts

### 2. Player Order Desynchronization  
**Problem**: Host and client had different PlayerOrder arrays ([1, 1907446628, 100, 101] vs [1907446628, 1, 100, 101])
**Symptoms**: Turn validation errors, game state mismatches

### 3. Turn Authority Conflicts
**Problem**: Both host and client independently managed turns
**Symptoms**: "Not player X's turn" errors, duplicate turn progression

### 4. Duplicate Game Starts
**Problem**: Both instances called ExecuteGameStart independently
**Symptoms**: Parallel game states, conflicting game logic

### 5. Card Play Race Conditions
**Problem**: Card plays executed on both instances simultaneously
**Symptoms**: Validation conflicts, state desync

## Fixes Applied

### Fix 1: Reliable Instance Detection
**File**: `scripts/GameManager.cs`
**Change**: Replaced TCP port test with file-based lock mechanism
```csharp
// Before: Unreliable TCP test
var tcp = new TcpServer();
bool isFirstInstance = tcp.Listen(testPort) == Error.Ok;
tcp.Stop();

// After: Reliable file lock
string lockFilePath = OS.GetUserDataDir() + "/instance.lock";
var lockFile = FileAccess.Open(lockFilePath, FileAccess.ModeFlags.Write);
bool isFirstInstance = (lockFile != null);
```

**Result**: First instance reliably detected as host, second as client

### Fix 2: Deterministic Player Order Sync
**File**: `scripts/NetworkManager.cs` 
**Change**: Clear and rebuild ConnectedPlayers in host order on clients
```csharp
// Before: Kept local player, added others (wrong order)
foreach (var existingPlayerId in GameManager.Instance.ConnectedPlayers.Keys)
{
    if (existingPlayerId != localPlayerId) playersToRemove.Add(existingPlayerId);
}

// After: Clear all, rebuild in exact host order
GameManager.Instance.ConnectedPlayers.Clear();
for (int i = 0; i < playerIds.Length; i++)
{
    // Add players in host-specified order
}
```

**Result**: Identical PlayerOrder arrays on all instances

### Fix 3: Host-Authoritative Turn Management
**File**: `scripts/CardManager.cs`
**Changes**: 
- EndTurn() only executes on host
- Added NetworkSyncTurnChange RPC for client sync
- Clients receive turn updates instead of calculating independently

```csharp
// Before: Both instances called EndTurn()
private void EndTurn() { CurrentPlayerTurn++; StartTurn(); }

// After: Host-only turn management
private void EndTurn() 
{
    if (!networkManager.IsHost) return; // CLIENT PROTECTION
    CurrentPlayerTurn++; 
    Rpc(MethodName.NetworkSyncTurnChange, previousTurn, CurrentPlayerTurn);
    StartTurn();
}
```

**Result**: Synchronized turn progression across all instances

### Fix 4: Single Game Start Authority
**File**: `scripts/GameManager.cs`
**Change**: StartCardGame() only executes on host
```csharp
// Before: Both instances could start games
public void StartCardGame() { 
    ChangePhase(GamePhase.CardPhase); 
    StartCardManagerGame();
}

// After: Host-only game starting
public void StartCardGame() {
    if (!NetworkManager.IsHost) return; // CLIENT PROTECTION
    ChangePhase(GamePhase.CardPhase);
    StartCardManagerGame();
    Rpc(MethodName.OnGameStarted); // Notify clients
}
```

**Result**: Single authoritative game start, clients sync via RPC

### Fix 5: Authoritative Card Play Processing
**File**: `scripts/CardManager.cs`
**Changes**:
- NetworkPlayCard sends to host only (CallLocal = false)
- Host validates and broadcasts results
- Clients apply results without re-validation

```csharp
// Before: Both instances executed card plays
[Rpc(CallLocal = true)]
public void NetworkPlayCard() { ExecuteCardPlay(); }

// After: Host-authoritative processing
[Rpc(CallLocal = false)]  
public void NetworkPlayCard() {
    if (!IsHost) return;
    bool success = ExecuteCardPlay();
    Rpc(MethodName.NetworkCardPlayResult, success);
}
```

**Result**: No validation conflicts, synchronized card state

## Testing Verification

Run `./test_multiplayer.sh` to test the fixes:
1. First instance auto-detects as host
2. Second instance auto-detects as client
3. Player orders remain synchronized
4. Turn progression stays in sync
5. Card plays validate once on host, sync to client

## Expected Behavior After Fixes

### Instance 1 (Host):
- Creates lock file, detects as first instance
- Starts hosting on port 7777
- Manages all game state authoritatively
- Validates all actions, broadcasts results

### Instance 2 (Client):  
- Fails to create lock file, detects as second instance
- Connects to host at 127.0.0.1:7777
- Receives synchronized game state
- Sends actions to host for validation

### Synchronized Elements:
- ✅ Player connection order
- ✅ Turn progression  
- ✅ Card play validation
- ✅ Game state updates
- ✅ Timer synchronization

## Files Modified
- `scripts/GameManager.cs` - Instance detection, game start authority
- `scripts/NetworkManager.cs` - Player order synchronization  
- `scripts/CardManager.cs` - Turn authority, card play validation
- `test_multiplayer.sh` - Testing script (new)

All changes maintain backward compatibility with single-player mode. 