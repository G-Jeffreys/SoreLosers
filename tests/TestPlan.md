# Sore Losers P0 Testing Plan

## Testing Overview

This document outlines a comprehensive testing strategy to validate that all P0 features are properly implemented and functioning correctly.

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
- [ ] Stink bomb mechanics
- [ ] Effect duration timers
- [ ] Overlay system

#### UIManager Tests
- [x] Compilation test
- [ ] UI state transitions
- [ ] **Dual-view system (Card Table vs Kitchen) (NEW)**
- [ ] **Leave/Return Table buttons (NEW)**
- [ ] **Location-based UI changes (NEW)**
- [ ] Chat intimidation mechanics
- [ ] Panel resizing
- [ ] Timer management

#### PlayerData Tests
- [x] Compilation test
- [ ] XP calculations
- [ ] Level progression
- [ ] Stat scaling
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
- [ ] Chat intimidation triggers on losses
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

### 3. End-to-End Tests (Complete Workflows)

#### Basic Game Flow
- [ ] 2-player match start to finish
- [ ] 4-player match start to finish
- [ ] Player win/loss scenarios
- [ ] Multiple rounds/games

#### Sabotage Scenarios
- [ ] Egg throw → Clean at sink
- [ ] Stink bomb → Effect duration
- [ ] Multiple sabotage effects
- [ ] Stat scaling verification

#### Network Scenarios
- [ ] Host migration
- [ ] Player reconnection
- [ ] Network latency handling
- [ ] Simultaneous actions

### 4. Performance Tests

#### Frame Rate
- [ ] 60fps maintenance during card play
- [ ] 60fps during real-time phase
- [ ] Performance with 4 players

#### Memory Usage
- [ ] No memory leaks during gameplay
- [ ] Reasonable memory footprint
- [ ] Garbage collection behavior

#### Network Performance
- [ ] Latency under 50ms (same region)
- [ ] Packet loss handling
- [ ] Bandwidth usage

## Test Implementation Strategy

### Phase 1: Basic Validation (Immediate)
1. Create minimal test scene
2. Test individual system initialization
3. Verify basic functionality works
4. Check event system connections

### Phase 2: Integration Testing (Short-term)
1. Test system interactions
2. Verify network connectivity
3. Test complete game flows
4. Validate PRD requirements

### Phase 3: Stress Testing (Medium-term)
1. Multiple simultaneous games
2. Extended play sessions
3. Performance under load
4. Edge case handling

## Test Execution

### Manual Testing Checklist
- [ ] Game builds and runs
- [ ] Main menu appears
- [ ] Host game creates room code
- [ ] Join game accepts room code
- [ ] Card game starts correctly
- [ ] Cards can be played
- [ ] Sabotage actions work
- [ ] Chat intimidation functions
- [ ] Game completes successfully

### Automated Testing Goals
- [ ] Unit test framework setup
- [ ] CI/CD integration
- [ ] Regression testing
- [ ] Performance benchmarking

## Success Criteria

### P0 Feature Validation
- ✅ **F1 - Private Match Hosting**: Room codes work, 4-player limit enforced
- ✅ **F2 - Trick-Taking Core**: Full card game plays correctly
- ✅ **F3 - Sabotage System**: Egg/stink bomb mechanics function
- ✅ **F4 - Chat Intimidation**: Panel grows/shrinks as specified

### Technical Requirements
- ✅ **60fps Performance**: Maintains target frame rate
- ✅ **Network Stability**: <50ms latency, no desyncs
- ✅ **Memory Efficiency**: No leaks, reasonable usage
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

1. **Immediate**: Create minimal test scene and validate basic functionality
2. **Short-term**: Set up multiplayer testing environment
3. **Medium-term**: Implement automated testing framework
4. **Long-term**: Continuous integration and regression testing

---

*This testing plan should be updated as testing progresses and new issues are discovered.* 