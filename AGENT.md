# AGENT.md - SoreLosers Godot C# Project

## Current Status: ✅ VISUAL EFFECTS & UI FIXES IMPLEMENTED
**Last Updated**: December 2024  
**State**: Multiplayer card game + Complete visual effects system + Enhanced debugging

## Recent Major Updates ✨

### Chat Panel & Visual Effects Implementation
**Session Completed**: Chat panel growth direction fix + Complete visual sabotage system
- ✅ **Chat Panel Direction**: Fixed to grow up/left with bottom-right corner anchored
- ✅ **Visual Egg Effects**: Complete on-screen splat system with 15x scaling
- ✅ **Metadata Cleanup**: Robust egg effect removal system using metadata tagging
- ✅ **Debug Button Suite**: 5 debug buttons for comprehensive testing
- ✅ **Enhanced Logging**: Comprehensive console output for all visual systems

### Debug Testing Capabilities
**New Debug Buttons Available**:
1. **"DEBUG: Test Egg Effect"** - Instant egg splat + visual overlay
2. **"DEBUG: Test Chat Growth"** - Direct 4x chat panel growth
3. **"DEBUG: Simulate Hand Complete"** - Test existing game flow
4. **"DEBUG: Shrink Chat"** - Return chat to normal size
5. **"DEBUG: Clean Egg Effects"** - Remove all visual effects from screen

## Build/Test/Lint Commands
- **Build**: `godot --headless --build-solutions --quit`
- **Run**: `godot --` (automatically builds first)
- **Debug**: Use VS Code debug configurations (Launch Godot, Launch Scene, Attach)
- **Export**: `godot --headless --export-release "macOS"`
- **Open Editor**: `godot`

## Quick Start - Testing Multiplayer
1. **Launch first instance**: Run `godot --` (becomes host automatically)
2. **Launch second instance**: Run `godot --` in new terminal (becomes client)
3. **First instance**: Click "Host Game" button in main menu
4. **Second instance**: Click "Join Game" button in main menu
5. **Both instances should connect and start card game with synchronized timers**

## Architecture & Structure
- **Godot 4.4.1** game project with **C# .NET 8.0** support
- **Main directories**: `scripts/` (C# code), `scenes/` (Godot scenes), `.godot/` (build artifacts)
- **Assembly name**: SoreLosers
- **Environment**: DOTNET_ROOT="/opt/homebrew/opt/dotnet/libexec"
- **Rendering**: Mobile renderer optimized

## 🛠️ Critical Fixes Applied

### 1. ENet Host Creation Error Resolution
**Problem**: `Couldn't create an ENet host. Parameter "host" is null.`
**Root Cause**: Improper ENet multiplayer peer initialization timing
**Files Changed**: 
- `scripts/NetworkManager.cs`
- `scripts/GameManager.cs`

**Changes Made**:
- Enhanced ENet initialization with proper error handling in `StartHosting()`
- Added try-catch blocks around server creation with detailed logging
- Simplified server creation parameters: `peer.CreateServer(port, MaxClients, 0, 0, 0)`
- Removed problematic async/await pattern that was causing timing issues
- Added fresh peer creation for each port attempt to avoid state corruption

### 2. Auto-Hosting Bypass Fix  
**Problem**: Both instances automatically became hosts, bypassing main menu UI
**Root Cause**: Debug networking setup running automatically in `_Ready()`
**Files Changed**:
- `scripts/GameManager.cs`

**Changes Made**:
- Removed automatic `SetupDebugNetworking()` call from `_Ready()`
- Users now manually control host/join through UI buttons
- Both instances start properly at main menu
- Added comments explaining the change to prevent regression

### 3. Timer Synchronization Implementation
**Problem**: Turn timers didn't match between host and client instances
**Root Cause**: Only host was running timer, clients had no sync mechanism
**Files Changed**:
- `scripts/CardManager.cs`
- `scripts/CardGameUI.cs`

**Changes Made**:
- Added `TurnTimerUpdated` signal to CardManager
- Implemented `NetworkTimerUpdate` RPC method for host→client timer sync
- Added `_Process()` method in CardManager to broadcast timer updates every frame
- Modified `GetGameState()` to return synchronized timer for clients
- Connected timer update signal in CardGameUI for real-time display
- Added `networkTurnTimeRemaining` field for client-side timer tracking

### 4. Game Visibility Enhancement
**Problem**: Players could only see their own cards, no visibility into game state
**Root Cause**: UI only displayed local player's hand, no shared game information
**Files Changed**:
- `scripts/CardGameUI.cs`

**Changes Made**:
- Added `playersInfoContainer` and `playerInfoLabels` UI elements
- Implemented `CreatePlayersInfoPanel()` to show all players' status
- Added `UpdatePlayersInfo()` to display card counts, scores, and turn indicators
- Implemented `UpdateTrickDisplay()` to show cards played in current trick
- Enhanced `_Process()` to update trick and player info continuously
- Added visual turn indicators (yellow highlighting for current player)

### 5. Player Management Improvements
**Problem**: "Player ID already exists" errors and duplicate player handling
**Root Cause**: Player ID assignment conflicts and poor error messaging
**Files Changed**:
- `scripts/GameManager.cs`
- `scripts/NetworkManager.cs`

**Changes Made**:
- Modified `AddPlayer()` to only emit `PlayerJoined` signal for new players
- Changed error level from `PrintErr` to `Print` for duplicate player updates
- Added safety checks in NetworkManager for player ID updates
- Only re-add players when network ID actually changes
- Enhanced logging to show when player IDs are updated vs. when they're unchanged

### 6. Concurrent Gameplay Implementation ✨ NEW DESIGN
**Problem**: Sequential phases prevented strategic gameplay between card game and sabotage
**Root Cause**: Original design had distinct CardPhase → RealTimePhase transitions
**Files Changed**:
- `scripts/GameManager.cs` - Added player location tracking (AtTable vs InKitchen)
- `scripts/CardManager.cs` - Card play validation for table presence
- `scripts/CardGameUI.cs` - Leave/Return Table buttons and view switching
- `scenes/CardGame.tscn` - Integrated kitchen view alongside card table

**NEW DESIGN**: Players can now leave the table anytime to gather sabotage items while the card game continues. This creates strategic tension between participating in the card game vs. gathering eggs/stink bombs.

**Changes Made**:
- Added `PlayerLocation` enum (AtTable, InKitchen) to GameManager
- Added Leave Table / Return to Table buttons always available during card game
- Card plays only allowed when player is AtTable
- Turn timer handles absent players (they miss their turn)
- Integrated Player movement system into CardGame scene
- View switching between card table and kitchen based on player location
- Location tracking with events for multiplayer synchronization

## Current Working Features ✅

### Networking
- **Host-Client Architecture**: First instance hosts, second connects automatically
- **ENet Multiplayer**: Reliable connection with 6-digit room codes
- **RPC Synchronization**: Card plays, game state, and timer sync
- **Automatic Instance Detection**: Uses host lock files for clean separation

### Concurrent Gameplay ✨ NEW
- **Leave/Return Table**: Players can leave the card table anytime via buttons
- **Continuous Card Game**: Card game continues regardless of who's at/away from table
- **Strategic Decisions**: Balance between playing cards vs. gathering sabotage items
- **Location Tracking**: System tracks whether players are AtTable or InKitchen
- **Turn Handling**: Players miss their turn if away from table when timer expires
- **View Switching**: Seamless transition between card table and kitchen views

### Card Game
- **Trick-Taking Logic**: Standard 52-card deck with proper rules
- **Turn Management**: Host-authoritative with client synchronization
- **Timer System**: 10-second turns with synchronized countdowns
- **AI Players**: Automatic AI opponents fill empty slots
- **Deterministic Shuffling**: Fixed seed ensures same deck across instances
- **Table Presence Validation**: Cards can only be played when at table

### Movement & Sabotage
- **Player Movement**: Arrow key movement in kitchen area
- **Item Interaction**: Egg collection, sink washing, stink bomb pickup
- **Sabotage Framework**: Complete egg throwing and stink bomb systems
- **Real-time Actions**: Sabotage actions work concurrent with card game
- **Inventory System**: Track eggs and stink bombs during movement

### UI & Display
- **Player Hand**: Shows local player's cards with play validation
- **Players Info Panel**: Shows all players' names, card counts, scores
- **Current Trick Display**: Shows cards played by all players this round
- **Turn Indicators**: Highlights current player in yellow
- **Real-time Updates**: All UI elements update immediately across instances
- **Location Status**: Shows whether player is at table or in kitchen
- **Dynamic Views**: Card table and kitchen views switch based on location

## Network Architecture

```
Host Instance (First to Start)
├── Creates ENet Server (Port 7777)
├── Manages Game State (Authoritative)
├── Controls Timer & Turn Logic
├── Broadcasts Updates to Clients
└── Handles AI Player Actions

Client Instance (Second to Start)  
├── Connects to Host via ENet
├── Receives Game State Updates
├── Sends Card Plays to Host
├── Displays Synchronized UI
└── Waits for Host Commands
```

## System Components

### Core Managers (Autoloads)
- **GameManager**: Central coordinator, phase management, player tracking
- **NetworkManager**: ENet multiplayer, host/client logic, RPC routing  
- **CardManager**: Game logic, deck management, trick-taking rules
- **UIManager**: Interface coordination (placeholder)
- **SabotageManager**: Real-time mechanics (placeholder)

### Key Scripts
- **CardGameUI.cs**: Main game interface with hand display and game info
- **MainMenuUI.cs**: Host/Join buttons and room code input
- **LobbyUI.cs**: Player list and game start interface

## Code Style & Conventions
- **C# Classes**: PascalCase, inherit from Godot nodes
- **Public fields**: PascalCase with `[Export]` attribute for inspector exposure
- **Private fields**: camelCase
- **Methods**: PascalCase, override Godot methods with `_` prefix (`_Ready()`, `_Process()`)
- **Imports**: `using Godot;` first, then standard libraries
- **Comments**: Extensive logging for debugging multiplayer issues

## Debugging Tips

### Network Issues
- Check console for "NetworkManager:" logs showing host/client status
- Look for "CardManager:" logs showing game state changes
- "GameManager:" logs show phase transitions and player management

### Common Error Patterns
- **"Player ID X already exists"**: Normal info message, not an error (fixed)
- **"ENet host creation failed"**: Check if port 7777 is available
- **"Method not found"**: Usually RPC method name mismatch

### Testing Checklist
- [ ] Both instances start at main menu (not auto-hosting)
- [ ] Host can create game with room code display
- [ ] Client can join and see "Connected" status
- [ ] Timer countdowns match on both instances
- [ ] Card plays appear on both instances immediately
- [ ] Players info panel shows all players correctly
- [ ] Turn indicators match between instances

## Known Working Features
- ✅ Multiplayer connection and synchronization
- ✅ Turn-based card game with proper rules
- ✅ Synchronized timers and game state
- ✅ Real-time card play visibility
- ✅ Player management and AI opponents
- ✅ Host-authoritative game control

## Still TODO (Future Development)
- [ ] Real-time movement phase (RealTimeUI.cs exists but not integrated)
- [ ] Sabotage system (framework exists, needs scene integration)
- [ ] Chat intimidation (logic exists, needs UI integration)
- [ ] Player progression/XP system
- [ ] Audio and visual effects
- [ ] Proper asset integration (currently using placeholders)

## File Structure
```
SoreLosers/
├── scripts/          # C# game logic
│   ├── GameManager.cs      # Central coordinator ✅
│   ├── NetworkManager.cs   # Multiplayer networking ✅  
│   ├── CardManager.cs      # Card game logic ✅
│   ├── CardGameUI.cs       # Main game interface ✅
│   ├── MainMenuUI.cs       # Menu interface ✅
│   └── LobbyUI.cs          # Lobby interface ✅
├── scenes/           # Godot scene files
│   ├── MainMenu.tscn       # Entry point ✅
│   ├── CardGame.tscn       # Main game scene ✅
│   └── Lobby.tscn          # Player waiting room ✅
├── assets/          # Game assets (mostly placeholders)
└── docs/            # Project documentation
```

## Emergency Debugging
If networking breaks again:
1. Check console for ENet errors - usually port conflicts
2. Verify GameManager singleton is working - look for "Singleton instance created"
3. Check host detection - should see "Host lock file" messages
4. Look for RPC errors - usually method name mismatches
5. Restart both instances if state gets corrupted
