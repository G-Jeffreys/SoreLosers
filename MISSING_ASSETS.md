# Missing Assets & Requirements for P0 Testing

**Status**: Identified during testing phase  
**Priority**: Required for runtime validation  

---

## 🎨 **VISUAL ASSETS NEEDED**

### Card Graphics
- **52 individual card images** (Ace through King, all 4 suits)
- **Card back design** for face-down cards
- **Card highlight/selection states**
- **Format**: PNG, recommended 64x96px minimum

### UI Elements  
- **Chat window background** and borders
- **Button graphics** (Host, Join, Play Card, etc.)
- **Panel backgrounds** for game phases
- **Player info displays**
- **Timer/countdown graphics**

### Sabotage Visuals
- **Egg splat overlay** images (various sizes for ThrowPower scaling)
- **Stink bomb fog/blur effects** (160px radius)
- **Wash sink** visual indicator
- **Item pickup** highlights

### Game Environment
- **Top-down room background** for real-time phase
- **Player avatar** sprites (simple 16x16 or 32x32)
- **Item spawn locations** (egg tray, drinks bar, sink)
- **Floor textures** and basic environment

---

## 🔊 **AUDIO ASSETS NEEDED**

### Sound Effects
- **Card shuffle** sound
- **Card play/place** sound  
- **Footstep** sounds for movement
- **Egg splat** sound effect
- **Stink bomb** hiss/explosion sound
- **Timer tick** or countdown sound

### Music
- **Background ambient loop** (low-volume, non-intrusive)
- **Menu music** (optional)
- **Victory/defeat** stingers

---

## 🎬 **SCENE FILES NEEDED**

### Core Game Scenes
- **MainMenu.tscn** - Entry point with Host/Join buttons
- **Lobby.tscn** - Pre-game waiting area
- **CardGame.tscn** - Turn-based card playing view
- **RealTime.tscn** - Top-down movement and sabotage
- **Results.tscn** - End-game results and XP display

### UI Scenes
- **HUD.tscn** - In-game interface overlay
- **ChatPanel.tscn** - Chat intimidation interface  
- **PlayerList.tscn** - Connected players display
- **SettingsMenu.tscn** - Basic settings (volume, etc.)

---

## 📐 **TECHNICAL SPECIFICATIONS**

### Resolution & Scaling
- **Base resolution**: 1920x1080 (can scale down)
- **UI scaling**: Support for different screen sizes
- **Pixel perfect**: Use `Filter = Nearest` for pixel art

### Performance Requirements
- **Target FPS**: 60 fps minimum
- **Memory usage**: Keep under 500MB
- **File size**: Total game under 200MB (PRD requirement)

---

## 🚨 **CRITICAL BLOCKERS**

### Scene Loading Issues
- **Problem**: C# scripts not loading in scene files
- **Solution**: Need proper scene setup or autoload configuration
- **Impact**: Cannot run any runtime tests

### Missing Autoload Setup
- **GameManager** needs to be autoloaded
- **NetworkManager** needs to be autoloaded  
- **Other managers** may need autoload configuration

### Input Mapping
- **Movement keys** (Arrow keys)
- **Interaction key** (Space)
- **UI navigation** (Tab, Enter, Escape)
- **Card playing** (mouse clicks)

---

## 💡 **TEMPORARY WORKAROUNDS**

### For Testing
1. **Use colored rectangles** instead of proper graphics
2. **Simple text buttons** instead of styled UI
3. **Basic shapes** for cards and effects
4. **Placeholder audio** or silence for now

### For Development
1. **Focus on functionality** first, visuals later
2. **Test with basic shapes** and debug printing
3. **Use Godot's built-in** UI themes temporarily
4. **Manual testing** of individual systems

---

## 📋 **IMPLEMENTATION PRIORITY**

### Phase 1: Basic Scenes (1-2 days)
1. Create minimal scene files with basic UI
2. Fix C# script loading issues
3. Implement basic navigation between scenes
4. Add simple placeholder graphics

### Phase 2: Core Visuals (3-5 days)
1. Add basic card graphics (can be simple colored rectangles)
2. Implement chat panel with basic styling
3. Add player movement visualization
4. Create basic sabotage effect overlays

### Phase 3: Polish (1-2 weeks)
1. Add proper card graphics and animations
2. Implement visual effects for sabotage
3. Add sound effects and music
4. Polish UI styling and layout

---

## 🎯 **SUCCESS CRITERIA**

### Minimal Viable Testing
- ✅ Scenes load without errors
- ✅ Basic navigation works
- ✅ Can see cards and UI elements
- ✅ Player movement visible
- ✅ Sabotage effects show up

### Full P0 Validation
- ✅ All P0 features visually functional
- ✅ Multiplayer works across machines
- ✅ Performance meets requirements
- ✅ User experience is acceptable
- ✅ All PRD requirements met

**The code foundation is solid - we just need the visual layer to bring it to life!** 