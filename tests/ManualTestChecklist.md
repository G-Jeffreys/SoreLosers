# Manual Testing Checklist for P0 Implementation

## Pre-Test Setup

### Prerequisites
- [ ] Godot 4.4.1 installed
- [ ] Project builds successfully (`godot --headless --build-solutions --quit`)
- [ ] Test scene created (`tests/TestScene.tscn`)

### Test Environment Setup
1. Open Godot editor
2. Navigate to `tests/TestScene.tscn`
3. Run the scene (F6)
4. Check console output for test results

## Basic System Tests

### Build and Compilation Tests ✅
- [x] Project compiles without errors
- [x] All scripts load successfully
- [x] No namespace conflicts
- [x] Export properties accessible

## Visual Effects & UI Testing (NEW SECTION)

### Debug Button Suite Validation
- [ ] **Debug Button Visibility**: All 5 debug buttons visible in CardGame scene
- [ ] **Button Placement**: Debug buttons positioned correctly and not overlapping game UI
- [ ] **Button Responsiveness**: All buttons respond to clicks with immediate feedback

#### Individual Debug Button Tests
- [ ] **"DEBUG: Test Egg Effect" Button**:
  1. Click button
  2. Verify immediate egg splat appears on screen
  3. Verify console shows "=== DEBUG: Testing Egg Effect ==="
  4. Verify egg splat is large and visible (3000px base size)
  5. Verify egg splat has yellow/orange color with transparency

- [ ] **"DEBUG: Test Chat Growth" Button**:
  1. Note initial chat panel size and position
  2. Click button
  3. Verify chat panel grows 4x in size
  4. Verify chat panel grows up and left (bottom-right corner stays fixed)
  5. Verify smooth animation during growth
  6. Verify console shows position calculations and "4x growth applied"

- [ ] **"DEBUG: Simulate Hand Complete" Button**:
  1. Click button
  2. Verify existing OnHandCompleted flow is triggered
  3. Verify console shows hand completion simulation
  4. Verify any existing chat growth behavior works

- [ ] **"DEBUG: Shrink Chat" Button**:
  1. Ensure chat is grown (use growth button first)
  2. Click shrink button
  3. Verify chat panel returns to normal size
  4. Verify smooth animation during shrinking
  5. Verify console shows shrinking action

- [ ] **"DEBUG: Clean Egg Effects" Button**:
  1. Ensure egg effects are visible (use egg effect button first)
  2. Click clean button
  3. Verify ALL egg splats disappear from screen
  4. Verify console shows cleanup progress with round-by-round details
  5. Verify console confirms cleanup completion

### Chat Panel Growth System Testing
- [ ] **Growth Direction Validation**:
  1. Note chat panel's initial position (record X,Y coordinates)
  2. Trigger chat growth (using debug button)
  3. Verify panel grows UP (Y position decreases)
  4. Verify panel grows LEFT (X position decreases)
  5. Verify bottom-right corner position remains unchanged
  6. Verify no part of panel goes off-screen

- [ ] **Animation Quality Testing**:
  1. Trigger chat growth
  2. Verify smooth, parallel animation for both size and position
  3. Verify animation duration is reasonable (~0.5 seconds)
  4. Verify no visual glitches or jumping during animation
  5. Verify tween completion is logged in console

- [ ] **Multiple Growth Cycles**:
  1. Grow chat panel (debug button)
  2. Shrink chat panel (debug button)
  3. Grow again
  4. Verify consistent behavior across multiple cycles
  5. Verify no position drift or size inconsistencies

### Visual Effects System Testing
- [ ] **Egg Splat Creation**:
  1. Use "Test Egg Effect" debug button
  2. Verify egg splat appears immediately
  3. Verify splat size is significantly large (should cover substantial screen area)
  4. Verify splat color is yellow/orange with semi-transparency
  5. Verify splat has rounded corners
  6. Verify console logs creation details

- [ ] **Egg Splat Persistence**:
  1. Create egg splat
  2. Wait 10 seconds
  3. Verify splat remains on screen (persistent effect)
  4. Create second egg splat
  5. Verify both splats remain visible
  6. Verify multiple splats can coexist

- [ ] **Metadata Tagging System**:
  1. Create egg splat
  2. Verify console shows metadata being set ("IsEggSplat: true")
  3. Use clean button
  4. Verify console shows metadata-based detection working
  5. Verify cleanup finds effects even with auto-generated node names

### Cleanup System Validation
- [ ] **Single Effect Cleanup**:
  1. Create one egg splat
  2. Use clean button
  3. Verify splat disappears
  4. Verify console shows "1 effect found and removed"
  5. Verify cleanup completes in round 1

- [ ] **Multiple Effects Cleanup**:
  1. Create 3-4 egg splats (multiple clicks of effect button)
  2. Use clean button
  3. Verify ALL splats disappear
  4. Verify console shows multiple effects found
  5. Verify cleanup may take multiple rounds but completes successfully

- [ ] **Persistent Effect Cleanup**:
  1. Create egg splat
  2. Wait for Godot to auto-rename the node (check console for @Panel@ names)
  3. Use clean button
  4. Verify effect still gets removed despite name change
  5. Verify metadata-based detection working

- [ ] **Cleanup Edge Cases**:
  1. Use clean button when no effects are present
  2. Verify no errors occur
  3. Verify console shows "No egg effects found"
  4. Create effect, clean it, then clean again
  5. Verify second clean operation handles empty state gracefully

### Integration Testing
- [ ] **SabotageManager Integration**:
  1. Verify debug egg effect button triggers SabotageManager.ApplyEggThrow
  2. Verify visual effect creation happens via event system
  3. Verify ThrowPower stat affects visual effect size (if testable)
  4. Verify no conflicts between debug system and game logic

- [ ] **UI State Management**:
  1. Test debug buttons during different game states
  2. Verify debug buttons don't interfere with normal game UI
  3. Verify visual effects don't break other UI elements
  4. Verify chat growth doesn't affect other UI positioning

### Enhanced Logging Validation
- [ ] **Console Output Quality**:
  1. Use each debug button and verify detailed console output
  2. Verify console shows before/after states for chat growth
  3. Verify console shows step-by-step cleanup progress
  4. Verify console shows effect creation details
  5. Verify all operations are clearly logged with timestamps

## System Initialization Tests

#### GameManager
- [ ] Singleton instance created
- [ ] Initial phase is MainMenu
- [ ] Player management functions work
- [ ] Phase transitions emit signals

#### NetworkManager
- [ ] Room code generation works (6-digit format)
- [ ] Room code validation works
- [ ] Network configuration is correct
- [ ] Host/client states managed properly

#### CardManager
- [ ] Deck initializes with 52 cards
- [ ] Card objects created correctly
- [ ] Game state tracking works
- [ ] Timer system functions

#### SabotageManager
- [ ] Sabotage effects can be created
- [ ] Item spawns initialize
- [ ] Overlay system works
- [ ] Configuration values are correct

#### UIManager
- [ ] UI state transitions work
- [ ] Chat intimidation data structures work
- [ ] Panel management functions

#### PlayerData
- [ ] Player creation works
- [ ] Stat scaling functions correctly
- [ ] XP calculation is accurate
- [ ] Level progression works

## Integration Tests

### System Communication
- [ ] GameManager can communicate with other systems
- [ ] Event system works between components
- [ ] PlayerData integrates with other systems
- [ ] UI updates reflect game state changes

### Data Flow Tests
- [ ] Player actions trigger appropriate events
- [ ] Game state updates propagate correctly
- [ ] Network messages can be sent/received
- [ ] XP and progression updates work

## Feature-Specific Tests

### F1 - Private Match Hosting & Joining
- [ ] Room code generation produces valid codes
- [ ] Multiple room codes are unique
- [ ] Room code validation rejects invalid formats
- [ ] Host can create game instance
- [ ] Client can attempt to join (network setup needed)

### F2 - Turn-based Trick-Taking Core
- [ ] Deck shuffling produces different orders
- [ ] Card dealing distributes correctly
- [ ] Turn timer can be started/stopped
- [ ] Game state tracks current player
- [ ] Scoring system calculates correctly

### **F2+ - Concurrent Gameplay System (NEW)**

#### Location System Validation
- [ ] PlayerLocation enum has AtTable and InKitchen values
- [ ] GameManager tracks player locations correctly
- [ ] Initial player locations set to AtTable by default
- [ ] Location tracking persists across game state changes
- [ ] Multiple players can have different locations simultaneously

#### Leave/Return Table UI Testing
- [ ] **Leave Table Button Test**:
  1. Start game, verify player is AtTable
  2. Verify "Leave Table" button is visible
  3. Click "Leave Table" button
  4. Verify button disappears, "Return to Table" appears
  5. Verify UI switches to kitchen view
  6. Verify console shows location change log

- [ ] **Return to Table Button Test**:
  1. Player must be InKitchen (from previous test)
  2. Verify "Return to Table" button is visible
  3. Click "Return to Table" button  
  4. Verify button disappears, "Leave Table" appears
  5. Verify UI switches back to card table view
  6. Verify console shows location change log

#### Card Play Validation Testing
- [ ] **AtTable Card Play Test**:
  1. Ensure player is AtTable
  2. Wait for player's turn
  3. Attempt to play a card
  4. Verify card play succeeds
  5. Verify turn advances normally

- [ ] **InKitchen Card Play Rejection Test**:
  1. Click "Leave Table" to go InKitchen
  2. Wait for player's turn (timer should advance)
  3. Try to click a card (if possible)
  4. Verify card play is rejected/ignored
  5. Verify console shows "not at table" message
  6. Verify turn times out and advances

#### Absent Player Turn Handling
- [ ] **Miss Turn When InKitchen Test**:
  1. Start game with multiple players
  2. Leave table (go InKitchen) during another player's turn
  3. Wait for your turn to come up
  4. Verify turn timer counts down normally
  5. Verify you miss your turn (no auto-forfeit)
  6. Verify next player's turn starts
  7. Verify game continues without interruption

- [ ] **Auto-Forfeit When AtTable Test**:
  1. Ensure player is AtTable
  2. Wait for your turn
  3. Do NOT play a card, let timer expire
  4. Verify auto-forfeit behavior (existing functionality)
  5. Verify turn advances to next player

#### UI View Switching
- [ ] **Card Table View Test**:
  1. Ensure player is AtTable
  2. Verify card hand is visible
  3. Verify timer display is visible
  4. Verify other players' status visible
  5. Verify "Leave Table" button visible

- [ ] **Kitchen View Test**:
  1. Click "Leave Table"
  2. Verify kitchen/movement interface appears
  3. Verify card interface is hidden
  4. Verify "Return to Table" button visible
  5. Verify location status shows "InKitchen"

#### Multi-Player Scenarios
- [ ] **Mixed Location Test** (requires 2+ players):
  1. Start multiplayer game
  2. Have one player leave table
  3. Verify remaining players can continue card game
  4. Verify absent player misses turns
  5. Verify present players' turns work normally

- [ ] **All Players InKitchen Test**:
  1. All players leave table
  2. Verify game state doesn't break
  3. Verify turn timer still advances
  4. Verify any player can return and resume

#### Single-Player Testing
- [ ] **Test Game Button**:
  1. From main menu, click "Test Game (Single Player)"
  2. Verify game starts with 1 human + 3 AI players
  3. Verify Leave/Return Table buttons work
  4. Verify AI players continue playing when human is InKitchen
  5. Verify full game completion works

### F3 - Sabotage System
- [ ] Egg throw effects can be applied
- [ ] Stink bomb mechanics work
- [ ] Effect duration tracking functions
- [ ] Item spawn system works
- [ ] Overlay system can be applied

### F4 - Chat-Window Intimidation
- [ ] Chat intimidation data structures work
- [ ] Panel sizing calculations are correct
- [ ] Timer management functions
- [ ] Intimidation level tracking works

## Performance Tests

### Memory Usage
- [ ] No immediate memory leaks on startup
- [ ] Objects are properly disposed
- [ ] Timers are cleaned up correctly

### Initialization Speed
- [ ] Systems initialize in reasonable time
- [ ] No blocking operations during startup
- [ ] Error handling doesn't crash system

## Current Test Results

### Automated Test Results
Run the `TestScene.tscn` and check console output for:
```
=== STARTING P0 VALIDATION TESTS ===
--- Testing System Initialization ---
✅ GameManager Singleton: PASSED
✅ NetworkManager Creation: PASSED
... (continue with results)
=== TEST RESULTS: X/Y PASSED, Z FAILED ===
```

### Known Limitations
- [ ] **Visual Testing**: No visual scenes implemented yet
- [ ] **Network Testing**: Requires multiple machines
- [ ] **Real-time Testing**: No physics simulation
- [ ] **Audio Testing**: No audio system implemented
- [ ] **Save/Load Testing**: No persistence system

### Testing Workarounds
1. **Network Testing**: Use localhost loopback for basic testing
2. **Visual Testing**: Use console logging for validation
3. **Integration Testing**: Mock missing components
4. **Performance Testing**: Monitor console for errors

## Next Steps for Testing

### Immediate Actions
1. [ ] Run automated tests and fix any failures
2. [ ] Test room code generation uniqueness
3. [ ] Validate PRD specification compliance
4. [ ] Test error handling edge cases

### Short-term Testing Goals
1. [ ] Create multiplayer test environment
2. [ ] Implement visual validation
3. [ ] Add performance benchmarking
4. [ ] Create regression test suite

### Long-term Testing Strategy
1. [ ] Automated CI/CD testing
2. [ ] Load testing with multiple players
3. [ ] Integration with full game scenes
4. [ ] User acceptance testing

## Test Execution Notes

### How to Run Tests
1. **Automated Tests**: Run `TestScene.tscn` in Godot
2. **Manual Tests**: Follow this checklist systematically
3. **Network Tests**: Requires separate machines or VMs
4. **Performance Tests**: Use Godot profiler

### Recording Results
- Use checkboxes to track completion
- Note any failures or unexpected behavior
- Document performance metrics
- Keep testing log for reference

### Troubleshooting Common Issues
- **Compilation Errors**: Check script references
- **Null Reference Errors**: Check initialization order
- **Signal Errors**: Verify event connections
- **Network Errors**: Check firewall/port settings

---

**Last Updated**: December 2024  
**Status**: Ready for basic validation testing  
**Next Review**: After implementing visual scenes 