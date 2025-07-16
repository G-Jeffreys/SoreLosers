# SoreLosers 🃏💣

A multiplayer card game with real-time sabotage mechanics built in Godot 4.4.1 with C#.

## 🎮 Current Status: MULTIPLAYER WORKING ✅

The core networking and card game functionality is **fully operational**. You can run two instances and play synchronized multiplayer trick-taking card games.

### What's Working Now
- **Multiplayer Networking**: Host/client ENet connection with perfect synchronization
- **Card Game**: Complete trick-taking game with 52-card deck and proper rules  
- **Timer Sync**: Both instances show identical turn countdown timers
- **Game Visibility**: Players see all cards played, scores, and turn status
- **AI Players**: Automatic AI opponents fill empty player slots

## 🚀 Quick Start

### Requirements
- **Godot 4.4.1** or later
- **.NET 8.0** runtime
- **macOS/Linux/Windows** (developed on macOS)

### Testing Multiplayer
1. **Clone and build**:
   ```bash
   git clone [repository]
   cd SoreLosers
   godot --headless --build-solutions --quit
   ```

2. **Launch first instance (Host)**:
   ```bash
   godot --
   # Click "Host Game" in the main menu
   ```

3. **Launch second instance (Client)** in a new terminal:
   ```bash
   godot --
   # Click "Join Game" in the main menu
   ```

4. **Play the game**:
   - Both instances will connect automatically
   - Host starts the game when ready
   - Take turns playing cards with synchronized timers
   - Watch the players info panel to see everyone's status

## 🏗️ Architecture

### Core Systems
- **GameManager**: Central coordinator and phase management
- **NetworkManager**: ENet multiplayer with host/client architecture  
- **CardManager**: Trick-taking game logic with timer synchronization
- **CardGameUI**: Real-time interface with shared game visibility

### Network Design
```
Host Instance                    Client Instance
├── ENet Server (Port 7777)     ├── ENet Client
├── Authoritative Game State    ├── Synchronized Display
├── Timer Management            ├── Receives Timer Updates
├── AI Player Control           ├── Sends Card Plays
└── RPC Broadcasts              └── Mirrors Host State
```

## 🎯 Game Features

### Multiplayer Card Game
- **Trick-Taking Rules**: Standard highest-card-wins mechanics
- **4-Player Games**: Human players + AI opponents
- **Turn Timers**: 10-second turns with auto-forfeit
- **Score Tracking**: Real-time score updates for all players
- **Synchronized State**: Perfect sync between all instances

### Enhanced Visibility
- **Player Info Panel**: Shows all players' card counts and scores
- **Current Trick Display**: See cards played by everyone this round
- **Turn Indicators**: Clear visual indication of whose turn it is
- **Real-time Updates**: All changes appear instantly across instances

## 📁 Project Structure

```
SoreLosers/
├── scripts/                 # C# game logic
│   ├── GameManager.cs      # Central coordinator ✅
│   ├── NetworkManager.cs   # Multiplayer networking ✅
│   ├── CardManager.cs      # Card game logic ✅
│   ├── CardGameUI.cs       # Game interface ✅
│   ├── MainMenuUI.cs       # Menu interface ✅
│   ├── LobbyUI.cs          # Lobby interface ✅
│   └── [Other systems]     # Sabotage, UI, etc.
├── scenes/                 # Godot scene files
│   ├── MainMenu.tscn       # Entry point ✅
│   ├── CardGame.tscn       # Main game ✅
│   └── Lobby.tscn          # Player lobby ✅
├── assets/                 # Game assets (mostly placeholders)
└── docs/                   # Documentation
```

## 🔧 Development

### Building
```bash
# Build C# solutions
godot --headless --build-solutions --quit

# Run the game
godot --

# Open editor
godot
```

### Debugging
- **Console Logging**: Extensive debug output for multiplayer state
- **Network Status**: Look for "NetworkManager:", "CardManager:", "GameManager:" logs
- **Error Recovery**: System handles network failures gracefully

### Common Issues
- **Port 7777 in use**: System automatically tries alternative ports
- **Connection fails**: Check firewall settings for local connections
- **Timer desync**: Should not happen anymore - if it does, check console logs

## 🎲 What's Next

### Ready for Integration
- **Real-time Movement**: Framework exists, needs scene hookup
- **Sabotage System**: Logic complete, needs visual integration  
- **Chat Intimidation**: Framework ready, needs UI implementation

### Future Development
- **Asset Polish**: Replace placeholder graphics
- **Audio Integration**: Add sound effects and music
- **UI Improvements**: Better layouts and visual design
- **Additional Features**: More sabotage types, progression system

## 🏆 Game Design

Based on the Product Requirements Document (PRD), this is a **trick-taking card game** where losing rounds triggers real-time sabotage phases. Players can throw eggs, drop stink bombs, and intimidate opponents through chat panel manipulation.

### Core Loop
1. **Card Phase**: Turn-based trick-taking (WORKING ✅)
2. **Real-time Phase**: Movement and sabotage (Framework Ready 🟡)
3. **Results**: Score tallying and XP progression (Framework Ready 🟡)

## 📚 Documentation

- **[AGENT.md](AGENT.md)**: Complete development guide and current status
- **[CHANGELOG_2024_12.md](CHANGELOG_2024_12.md)**: Recent fixes and improvements
- **[docs/prd.md](docs/prd.md)**: Product Requirements Document
- **[docs/p0_implementation.md](docs/p0_implementation.md)**: Technical implementation details

## 🤝 Contributing

The networking foundation is solid and ready for feature development. The most helpful contributions would be:

1. **Scene Polish**: Improve UI layouts and visual design
2. **Asset Integration**: Replace placeholder graphics and audio
3. **Real-time Integration**: Connect movement system to existing framework
4. **Testing**: Multi-platform testing and edge case discovery

## 📄 License

[Add license information]

---

**Status**: Core multiplayer functionality working and ready for testing! 🚀 