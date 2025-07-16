# Sore Losers P0 Testing Plan

## Testing Overview

This document outlines a comprehensive testing strategy to validate that all P0 features are properly implemented and functioning correctly.

## Recent Testing Additions

### Visual Effects & Debug System Testing (NEW)
Major testing additions for the chat panel fixes and visual effects system implemented in the latest session:
- **Debug Button Suite Testing**: Validation of 5 comprehensive debug buttons
- **Chat Panel Growth Testing**: Direction, animation, and positioning validation
- **Visual Effects Testing**: Egg splat creation, persistence, and cleanup
- **Metadata System Testing**: Robust cleanup system validation
- **Enhanced Logging Testing**: Console output quality and debugging information

## Test Categories

### 1. Unit Tests (Individual System Testing)

#### GameManager Tests
- [x] Compilation test
- [ ] Singleton initialization
- [ ] Phase transitions
- [ ] Player management
- [ ] **PlayerLocation system (NEW)**
- [ ] **Leave/Return Table methods (NEW)**
- [ ] Event emission

#### NetworkManager Tests
- [x] Compilation test
- [ ] Room code generation (6-digit format)
- [ ] Host creation
- [ ] Client connection
- [ ] Disconnection handling
- [ ] RPC message routing

#### CardManager Tests
- [x] Compilation test
- [ ] Deck initialization (52 cards)
- [ ] Card shuffling
- [ ] Deal validation (13 cards per player)
- [ ] Turn timer functionality
- [ ] **Location-based card play validation (NEW)**
- [ ] **Absent player turn handling (NEW)**
- [ ] Trick-taking rules
- [ ] Scoring system

#### SabotageManager Tests
- [x] Compilation test
- [ ] Item spawn initialization
- [ ] Egg throw mechanics
- [ ] **Visual effects event emission (NEW)**
- [ ] **Event integration with CardGameUI (NEW)**
- [ ] Stink bomb mechanics
- [ ] Effect duration timers
- [ ] Overlay system

#### UIManager Tests
- [x] Compilation test
- [ ] UI state transitions
- [ ] **Dual-view system (Card Table vs Kitchen) (NEW)**
- [ ] **Leave/Return Table buttons (NEW)**
- [ ] **Location-based UI changes (NEW)**
- [ ] **Visual effects overlay system (NEW)**
- [ ] **Chat panel growth direction (NEW)**
- [ ] **Metadata-based cleanup system (NEW)**
- [ ] Chat intimidation mechanics
- [ ] Panel resizing
- [ ] Timer management

#### **Visual Effects System Tests (NEW)**
- [ ] **Overlay Layer Creation**: Verify overlay container exists and functions
- [ ] **Egg Splat Visual Creation**: Test CreateEggSplatVisual method
- [ ] **Metadata Tagging**: Verify SetMeta("IsEggSplat", true) works
- [ ] **Size Scaling**: Test 15x base size (3000px) scaling
- [ ] **Style Application**: Verify yellow/orange color, transparency, rounded corners
- [ ] **Multi-Round Cleanup**: Test 1-10 round cleanup system
- [ ] **Safe Node Iteration**: Verify collection modification safety
- [ ] **Deferred Removal**: Test QueueFree() usage
- [ ] **Persistence Testing**: Verify effects remain until cleaned

#### **Debug System Tests (NEW)**
- [ ] **Debug Button Creation**: All 5 buttons exist in scene
- [ ] **Button Event Connection**: Click handlers properly connected
- [ ] **Test Egg Effect Button**: Triggers SabotageManager + creates visual
- [ ] **Test Chat Growth Button**: Applies 4x growth with proper positioning
- [ ] **Simulate Hand Complete Button**: Triggers existing game flow
- [ ] **Shrink Chat Button**: Returns panel to normal size
- [ ] **Clean Egg Effects Button**: Removes all visual effects
- [ ] **Console Logging**: All debug actions produce detailed logs

#### PlayerData Tests
- [x] Compilation test
- [ ] XP calculations
- [ ] Level progression
- [ ] Stat scaling
- [ ] **ThrowPower coverage calculation for visual effects (NEW)**
- [ ] Persistence ready

### 2. Integration Tests (System Interactions)

#### Network + Game Flow
- [ ] Host creates game → Client joins → Game starts
- [ ] Player actions sync across network
- [ ] Disconnection handling

#### Card Game + Sabotage
- [ ] Card phase → Real-time phase transitions
- [ ] Sabotage effects during card play
- [ ] XP awarded correctly

#### **Visual Effects Integration (NEW)**
- [ ] **SabotageManager → CardGameUI**: Event properly triggers visual creation
- [ ] **PlayerData → Visual Effects**: ThrowPower stat affects visual size
- [ ] **Debug System → Game Logic**: Debug buttons integrate without conflicts
- [ ] **Cleanup → Multiple Systems**: Cleaning works across all effect sources
- [ ] **UI States → Visual Effects**: Effects persist across UI changes
- [ ] **Network → Visual Effects**: Effects work correctly in multiplayer

#### **Chat Panel System Integration (NEW)**
- [ ] **Growth Direction**: Up/left growth with bottom-right anchor
- [ ] **Position Calculation**: Manual position calculation works correctly
- [ ] **Parallel Animation**: Size and position tweens work simultaneously
- [ ] **Debug Integration**: Debug buttons trigger chat growth correctly
- [ ] **State Persistence**: Chat state survives scene changes

#### **Concurrent Gameplay Integration (NEW)**
- [ ] Leave Table → Card game continues without player
- [ ] Return to Table → Player can resume card play
- [ ] Location changes sync across all players
- [ ] Absent player turn handling works correctly
- [ ] UI transitions between views work seamlessly
- [ ] Multiple players can be InKitchen simultaneously
- [ ] Card play validation respects player location

#### UI + Game State
- [ ] UI updates match game state
- [ ] **Chat intimidation triggers on losses (with proper direction) (NEW)**
- [ ] **Visual effects display correctly (NEW)**
- [ ] **Debug buttons don't interfere with normal gameplay (NEW)**
- [ ] Overlay effects display correctly

### 2.5. Concurrent Gameplay Testing (NEW SECTION)

#### Location System Tests
- [ ] **PlayerLocation Enum**: Validates AtTable and InKitchen states
- [ ] **Location Tracking**: GameManager correctly tracks all player locations
- [ ] **Location Changes**: Players can switch between AtTable and InKitchen
- [ ] **Network Sync**: Location changes broadcast to all connected players
- [ ] **Initial State**: All players start AtTable by default

#### Leave/Return Table Functionality  
- [ ] **Leave Table Button**: Visible when AtTable, functional click handling
- [ ] **Return Table Button**: Visible when InKitchen, functional click handling
- [ ] **Button State Management**: Correct buttons shown per location
- [ ] **UI Transitions**: Instant view switching between card table and kitchen
- [ ] **Location Status Display**: Clear indication of current player location

#### Card Game Validation
- [ ] **AtTable Players**: Can play cards normally
- [ ] **InKitchen Players**: Cannot play cards (graceful rejection)
- [ ] **Location Validation**: Card play attempts check player location first
- [ ] **Error Handling**: Clear feedback when InKitchen player tries to play
- [ ] **Game Continuity**: Card game unaffected by player location changes

#### Turn Management for Absent Players
- [ ] **AtTable Timer Expiry**: Auto-forfeit when player doesn't play
- [ ] **InKitchen Timer Expiry**: Player misses turn, game continues  
- [ ] **Turn Progression**: Next player's turn starts correctly after missed turn
- [ ] **Game Flow**: Multiple missed turns don't break game state
- [ ] **Score Tracking**: Missed turns don't affect scoring calculations

#### Multi-Player Location Scenarios
- [ ] **All AtTable**: Traditional card game behavior
- [ ] **Mixed Locations**: Some AtTable, some InKitchen works correctly
- [ ] **All InKitchen**: Game pauses appropriately until someone returns
- [ ] **Rapid Location Changes**: Quick Leave/Return cycles handled gracefully
- [ ] **Simultaneous Changes**: Multiple players changing locations at once

#### AI Integration Testing
- [ ] **Single Player Mode**: 1 human + 3 AI players works correctly
- [ ] **AI Location Handling**: AI players remain AtTable appropriately
- [ ] **Test Game Button**: Main menu test game launches successfully
- [ ] **Player Count Validation**: Removed premature checks don't break flow
- [ ] **AI Turn Management**: AI players take turns correctly with location system

### 2.6. Visual Effects & Debug Testing (NEW SECTION)

#### Debug Button Functional Testing
- [ ] **All Buttons Respond**: Each of 5 debug buttons responds to clicks
- [ ] **Immediate Visual Feedback**: Effects appear instantly when triggered
- [ ] **Console Output**: Each button produces detailed logging
- [ ] **No UI Conflicts**: Debug buttons don't interfere with game UI
- [ ] **State Independence**: Debug buttons work in all game states

#### Chat Panel Growth Testing
- [ ] **Direction Validation**: Panel grows up and left (not down/right)
- [ ] **Anchor Point**: Bottom-right corner remains fixed
- [ ] **Position Calculation**: Manual position calculation works correctly
- [ ] **Animation Quality**: Smooth parallel tweens for size and position
- [ ] **Multiple Cycles**: Growth → Shrink → Growth works consistently

#### Visual Effects Comprehensive Testing
- [ ] **Egg Splat Creation**: Visual effects appear on screen
- [ ] **Size Scaling**: 15x base size (3000px) applied correctly
- [ ] **Styling**: Yellow/orange color, transparency, rounded corners
- [ ] **Positioning**: Effects appear in correct screen locations
- [ ] **Persistence**: Effects remain until manually cleaned

#### Cleanup System Robustness
- [ ] **Single Effect Cleanup**: One effect removes correctly
- [ ] **Multiple Effect Cleanup**: All effects remove in one operation
- [ ] **Persistent Effect Cleanup**: Effects with auto-generated names remove
- [ ] **Metadata Detection**: Both name and metadata checking works
- [ ] **Edge Case Handling**: Cleanup works when no effects present
- [ ] **Multi-Round Logic**: Cleanup continues until no effects found

### 3. End-to-End Tests (Complete Workflows)

#### Basic Game Flow
- [ ] 2-player match start to finish
- [ ] 4-player match start to finish
- [ ] Player win/loss scenarios
- [ ] Multiple rounds/games

#### **Enhanced Sabotage Scenarios (UPDATED)**
- [ ] Egg throw → **Immediate visual effect** → Clean at sink
- [ ] **Multiple egg throws** → **Multiple visual overlays** → **Comprehensive cleanup**
- [ ] Stink bomb → Effect duration
- [ ] **Visual effects during multiplayer** → **Effects sync correctly**
- [ ] **Debug testing integration** → **Rapid testing capabilities**
- [ ] Stat scaling verification

#### **UI & Visual Effects Scenarios (NEW)**
- [ ] **Chat Growth Trigger** → **Proper direction and animation** → **Return to normal**
- [ ] **Egg Effect Creation** → **Visual persistence** → **Manual cleanup**
- [ ] **Debug Button Usage** → **Immediate feedback** → **No system conflicts**
- [ ] **Multiple Visual Effects** → **Overlap handling** → **Complete removal**

#### Network Scenarios
- [ ] Host migration
- [ ] Player reconnection
- [ ] Network latency handling
- [ ] Simultaneous actions

### 4. Performance Tests

#### Frame Rate
- [ ] 60fps maintenance during card play
- [ ] 60fps during real-time phase
- [ ] **60fps with multiple visual effects active (NEW)**
- [ ] **60fps during chat panel animations (NEW)**
- [ ] Performance with 4 players

#### Memory Usage
- [ ] No memory leaks during gameplay
- [ ] **No memory leaks from visual effects creation/cleanup (NEW)**
- [ ] **Proper disposal of visual effect nodes (NEW)**
- [ ] Reasonable memory footprint
- [ ] Garbage collection behavior

#### **Visual Effects Performance (NEW)**
- [ ] **Visual Effect Creation Speed**: Effects appear without lag
- [ ] **Cleanup Performance**: Multi-round cleanup doesn't cause hitches
- [ ] **Multiple Effects**: Performance with many simultaneous effects
- [ ] **Animation Performance**: Smooth chat panel growth/shrink
- [ ] **Memory Cleanup**: Proper disposal of removed effects

#### Network Performance
- [ ] Latency under 50ms (same region)
- [ ] Packet loss handling
- [ ] **Visual effects don't affect network performance (NEW)**
- [ ] Bandwidth usage

## Test Implementation Strategy

### Phase 1: Basic Validation (Immediate)
1. Create minimal test scene
2. Test individual system initialization
3. **Test all 5 debug buttons functionality (NEW)**
4. **Verify visual effects system works (NEW)**
5. **Validate chat panel growth direction (NEW)**
6. Verify basic functionality works
7. Check event system connections

### Phase 2: Integration Testing (Short-term)
1. Test system interactions
2. **Test visual effects integration with sabotage system (NEW)**
3. **Test debug system integration with game logic (NEW)**
4. **Test cleanup system robustness (NEW)**
5. Verify network connectivity
6. Test complete game flows
7. Validate PRD requirements

### Phase 3: Stress Testing (Medium-term)
1. **Test performance with many visual effects (NEW)**
2. **Test cleanup system with edge cases (NEW)**
3. Multiple simultaneous games
4. Extended play sessions
5. Performance under load
6. Edge case handling

## Test Execution

### Manual Testing Checklist
- [ ] Game builds and runs
- [ ] Main menu appears
- [ ] **All 5 debug buttons functional (NEW)**
- [ ] **Chat panel grows in correct direction (NEW)**
- [ ] **Visual egg effects appear and can be cleaned (NEW)**
- [ ] Host game creates room code
- [ ] Join game accepts room code
- [ ] Card game starts correctly
- [ ] Cards can be played
- [ ] Sabotage actions work
- [ ] Chat intimidation functions
- [ ] Game completes successfully

### **Enhanced Manual Testing (NEW)**
- [ ] **Debug Button Suite**: All buttons respond with immediate feedback
- [ ] **Visual Effects**: Egg splats appear, persist, and can be removed
- [ ] **Chat Growth**: Panel grows up/left with smooth animation
- [ ] **Cleanup System**: All visual effects remove reliably
- [ ] **Console Logging**: Detailed output for all operations
- [ ] **Performance**: No lag or hitches with visual effects

### Automated Testing Goals
- [ ] Unit test framework setup
- [ ] **Visual effects system testing (NEW)**
- [ ] **Debug functionality validation (NEW)**
- [ ] CI/CD integration
- [ ] Regression testing
- [ ] Performance benchmarking

## Success Criteria

### P0 Feature Validation
- ✅ **F1 - Private Match Hosting**: Room codes work, 4-player limit enforced
- ✅ **F2 - Trick-Taking Core**: Full card game plays correctly
- ✅ **F3 - Sabotage System**: Egg/stink bomb mechanics function **+ Visual Effects**
- ✅ **F4 - Chat Intimidation**: Panel grows/shrinks as specified **+ Proper Direction**

### **Enhanced Feature Validation (NEW)**
- ✅ **Visual Effects System**: Complete egg splat visuals with scaling
- ✅ **Debug Testing Suite**: 5 functional debug buttons for rapid testing
- ✅ **Chat Panel Enhancement**: Proper up/left growth with anchored positioning
- ✅ **Cleanup System**: Robust removal of all visual effects
- ✅ **Enhanced Logging**: Comprehensive debugging information

### Technical Requirements
- ✅ **60fps Performance**: Maintains target frame rate **+ with visual effects**
- ✅ **Network Stability**: <50ms latency, no desyncs
- ✅ **Memory Efficiency**: No leaks, reasonable usage **+ visual effect cleanup**
- ✅ **Code Quality**: Clean, maintainable, documented

## Known Test Limitations

### Current Constraints
- No visual scenes implemented yet
- No audio system for testing
- No save/load system testing
- Limited error handling testing

### Testing Dependencies
- Requires 2+ machines for network testing
- Needs visual assets for complete testing
- Requires performance profiling tools
- Needs automated test framework

## Next Steps

1. **Immediate**: **Test all new visual effects and debug functionality**
2. **Short-term**: **Validate visual effects integration in multiplayer**
3. **Medium-term**: **Performance testing with visual effects**
4. **Long-term**: Continuous integration and regression testing

---

*This testing plan should be updated as testing progresses and new issues are discovered.* 