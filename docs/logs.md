CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 12 valid cards
CardManager: AI player 100 playing Queen of Diamonds
CardManager: Nakama game - sending card play to MatchManager - Player 100: Queen of Diamonds
CardManager: Added pending card play: 100_Queen of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 100: Queen of Diamonds
CardManager: 🃏 Player 100 hand BEFORE removal: 12 cards [Queen of Diamonds, King of Hearts, King of Clubs, Six of Hearts, Jack of Diamonds, Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 11 cards [King of Hearts, King of Clubs, Six of Hearts, Jack of Diamonds, Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Queen of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Queen of Diamonds (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: Queen of Diamonds
CardGameUI: Player 100 played Queen of Diamonds
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: Queen of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 1
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card Queen of Diamonds for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Diamonds by player 100
CardGameUI: Added trick card 0: Queen of Diamonds (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Queen of Diamonds (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Two of Diamonds
MatchManager: Card play synchronized - Player_101 played Two of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 1
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 1
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager: OnNakamaCardPlayReceived called - Player 101: Two of Diamonds
CardManager: Received card play from Nakama - Player 101: Two of Diamonds
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Two of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Two of Diamonds
CardGameUI: Player 101 played Two of Diamonds
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Two of Diamonds
MatchManager: CardPlayReceived signal emitted for player 101: Two of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 1
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 1) - Previous: Turn 3, Tricks 1
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 1
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 1
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 1) - Last processed: Turn 0, Tricks 1
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 1
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Client timer synced to: 10.0s
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Diamonds by player 100
CardGameUI: Added trick card 0: Queen of Diamonds (P100) at position (-5, -20)
CardGameUI: Created trick card button for Two of Diamonds by player 101
CardGameUI: Added trick card 1: Two of Diamonds (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Queen of Diamonds (P100), Two of Diamonds (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 2 valid cards
CardManager: AI player 101 playing Two of Diamonds
CardManager: Nakama game - sending card play to MatchManager - Player 101: Two of Diamonds
CardManager: Added pending card play: 101_Two of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 101: Two of Diamonds
CardManager: 🃏 Player 101 hand BEFORE removal: 12 cards [Nine of Clubs, Eight of Hearts, Five of Hearts, Four of Clubs, Six of Clubs, Two of Diamonds, Two of Hearts, Seven of Diamonds, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 11 cards [Nine of Clubs, Eight of Hearts, Five of Hearts, Four of Clubs, Six of Clubs, Two of Hearts, Seven of Diamonds, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Two of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Two of Diamonds (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Two of Diamonds
CardGameUI: Player 101 played Two of Diamonds
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Two of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 1
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 1
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Two of Diamonds for AI player 101
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Diamonds by player 100
CardGameUI: Added trick card 0: Queen of Diamonds (P100) at position (-5, -20)
CardGameUI: Created trick card button for Two of Diamonds by player 101
CardGameUI: Added trick card 1: Two of Diamonds (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Queen of Diamonds (P100), Two of Diamonds (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 2 cards: [P100:Queen of Diamonds, P101:Two of Diamonds]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Two of Clubs
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 12 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Two of Clubs, Seven of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Two of Clubs
CardManager: Added pending card play: 0_Two of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 0: Two of Clubs
CardManager: 🃏 Player 0 hand BEFORE removal: 12 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Two of Clubs, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 11 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Two of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Two of Clubs (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Two of Clubs
CardGameUI: Player 0 played Two of Clubs
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 12 cards)
CardGameUI: 🃏 UI cards before update: [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Two of Clubs, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Two of Clubs'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 11 cards
CardGameUI: 🃏 UI currentPlayerCards had 12 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (12): [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Two of Clubs, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (11): [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 12 -> 11 cards
CardGameUI: 🃏 Current cards in hand: [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Two of Clubs
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 11 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Diamonds by player 100
CardGameUI: Added trick card 0: Queen of Diamonds (P100) at position (-60, -20)
CardGameUI: Created trick card button for Two of Diamonds by player 101
CardGameUI: Added trick card 1: Two of Diamonds (P101) at position (50, -20)
CardGameUI: Created trick card button for Two of Clubs by player 0
CardGameUI: Added trick card 2: Two of Clubs (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Queen of Diamonds (P100), Two of Diamonds (P101), Two of Clubs (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
CardGameUI: 🃏 Hand update complete - 12 -> 11 cards (played: Two of Clubs)
CardGameUI: Created card button for Three of Spades with texture
CardGameUI: Created card button for Seven of Hearts with texture
CardGameUI: Created card button for Jack of Spades with texture
CardGameUI: Created card button for Five of Spades with texture
CardGameUI: Created card button for Five of Clubs with texture
CardGameUI: Created card button for Six of Spades with texture
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Ten of Hearts with texture
CardGameUI: Created card button for Three of Hearts with texture
CardGameUI: Created card button for Seven of Spades with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 11 manually positioned cards in two rows (6 top, 5 bottom)
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 2 cards: [P100:Queen of Diamonds, P101:Two of Diamonds]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 1
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 1
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 1
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 1) - Previous: Turn 0, Tricks 1
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 1
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 1
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 1, Tricks: 1) - Last processed: Turn 1, Tricks 1
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 1
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Two of Clubs
MatchManager: Card play synchronized - Client played Two of Clubs
CardManager: OnNakamaCardPlayReceived called - Player 0: Two of Clubs
CardManager: Received card play from Nakama - Player 0: Two of Clubs
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Two of Clubs
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Two of Clubs
CardGameUI: Player 0 played Two of Clubs
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 1
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 1
CardGameUI: Turn started for player 2
CardManager: Synchronized card play completed - Player 0: Two of Clubs
MatchManager: CardPlayReceived signal emitted for player 0: Two of Clubs
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Diamonds by player 100
CardGameUI: Added trick card 0: Queen of Diamonds (P100) at position (-60, -20)
CardGameUI: Created trick card button for Two of Diamonds by player 101
CardGameUI: Added trick card 1: Two of Diamonds (P101) at position (50, -20)
CardGameUI: Created trick card button for Two of Clubs by player 0
CardGameUI: Added trick card 2: Two of Clubs (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Queen of Diamonds (P100), Two of Diamonds (P101), Two of Clubs (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Three of Diamonds
MatchManager: Card play synchronized - mXTgVBRUod played Three of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 1
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 100, Leader: 2, Score: 2
CardManager: OnNakamaCardPlayReceived called - Player 2: Three of Diamonds
CardManager: Received card play from Nakama - Player 2: Three of Diamonds
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Three of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Three of Diamonds
CardGameUI: Player 2 played Three of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Three of Diamonds
MatchManager: CardPlayReceived signal emitted for player 2: Three of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 1
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 1) - Previous: Turn 1, Tricks 1
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 1
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 100, Leader: 2, Score: 2
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Queen of Diamonds (P100), Two of Diamonds (P101), Two of Clubs (P0), Three of Diamonds (P2)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 100
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 2, Turn: 2
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 100, Leader: 2, Score: 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Diamonds by player 100
CardGameUI: Added trick card 0: Queen of Diamonds (P100) at position (-115, -20)
CardGameUI: Created trick card button for Two of Diamonds by player 101
CardGameUI: Added trick card 1: Two of Diamonds (P101) at position (-5, -20)
CardGameUI: Created trick card button for Two of Clubs by player 0
CardGameUI: Added trick card 2: Two of Clubs (P0) at position (105, -20)
CardGameUI: Created trick card button for Three of Diamonds by player 2
CardGameUI: Added trick card 3: Three of Diamonds (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Queen of Diamonds (P100), Two of Diamonds (P101), Two of Clubs (P0), Three of Diamonds (P2)]
CardGameUI: Current trick leader: 2, Current turn: 2
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P100:Queen of Diamonds, P101:Two of Diamonds, P0:Two of Clubs]
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Three of Diamonds
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 12 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades, Three of Diamonds]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Three of Diamonds
CardManager: Added pending card play: 2_Three of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 2: Three of Diamonds
CardManager: 🃏 Player 2 hand BEFORE removal: 12 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades, Three of Diamonds]
CardManager: 🃏 Player 2 hand AFTER removal: 11 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Three of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Three of Diamonds (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Three of Diamonds
CardGameUI: Player 2 played Three of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 12 cards)
CardGameUI: 🃏 UI cards before update: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades, Three of Diamonds]
CardGameUI: 🃏 Played card was: 'Three of Diamonds'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 11 cards
CardGameUI: 🃏 UI currentPlayerCards had 12 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (12): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades, Three of Diamonds]
CardGameUI: 🃏 New CardManager cards (11): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 12 -> 11 cards
CardGameUI: 🃏 Current cards in hand: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Three of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 1
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 100 wins trick with Queen of Diamonds
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 100)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 100, Leader: 2, Score: 2
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Queen of Diamonds (P100), Two of Diamonds (P101), Two of Clubs (P0), Three of Diamonds (P2)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 100
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 100, 10-second display period started
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 11 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Diamonds by player 100
CardGameUI: Added trick card 0: Queen of Diamonds (P100) at position (-115, -20)
CardGameUI: Created trick card button for Two of Diamonds by player 101
CardGameUI: Added trick card 1: Two of Diamonds (P101) at position (-5, -20)
CardGameUI: Created trick card button for Two of Clubs by player 0
CardGameUI: Added trick card 2: Two of Clubs (P0) at position (105, -20)
CardGameUI: Created trick card button for Three of Diamonds by player 2
CardGameUI: Added trick card 3: Three of Diamonds (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Queen of Diamonds (P100), Two of Diamonds (P101), Two of Clubs (P0), Three of Diamonds (P2)]
CardGameUI: Current trick leader: 2, Current turn: 2
CardGameUI: 🃏 Hand update complete - 12 -> 11 cards (played: Three of Diamonds)
CardGameUI: Created card button for Nine of Diamonds with texture
CardGameUI: Created card button for Ten of Diamonds with texture
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Ace of Clubs with texture
CardGameUI: Created card button for Six of Diamonds with texture
CardGameUI: Created card button for Ace of Hearts with texture
CardGameUI: Created card button for Queen of Hearts with texture
CardGameUI: Created card button for Five of Diamonds with texture
CardGameUI: Created card button for Three of Clubs with texture
CardGameUI: Created card button for Ten of Spades with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 11 manually positioned cards in two rows (6 top, 5 bottom)
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 100
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 2, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 100
CardGameUI: Turn started for player 100
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 100
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 2, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: King of Hearts
MatchManager: Card play synchronized - Player_100 played King of Hearts
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 2
CardManager: OnNakamaCardPlayReceived called - Player 100: King of Hearts
CardManager: Received card play from Nakama - Player 100: King of Hearts
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: King of Hearts
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: King of Hearts
CardGameUI: Player 100 played King of Hearts
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: King of Hearts
MatchManager: CardPlayReceived signal emitted for player 100: King of Hearts
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 2
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 2) - Previous: Turn 2, Tricks 1
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 2
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Hearts by player 100
CardGameUI: Added trick card 0: King of Hearts (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [King of Hearts (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 11 valid cards
CardManager: AI player 100 playing King of Hearts
CardManager: Nakama game - sending card play to MatchManager - Player 100: King of Hearts
CardManager: Added pending card play: 100_King of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 100: King of Hearts
CardManager: 🃏 Player 100 hand BEFORE removal: 11 cards [King of Hearts, King of Clubs, Six of Hearts, Jack of Diamonds, Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 10 cards [King of Clubs, Six of Hearts, Jack of Diamonds, Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: King of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: King of Hearts (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: King of Hearts
CardGameUI: Player 100 played King of Hearts
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: King of Hearts
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 2
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card King of Hearts for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Hearts by player 100
CardGameUI: Added trick card 0: King of Hearts (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [King of Hearts (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Eight of Hearts
MatchManager: Card play synchronized - Player_101 played Eight of Hearts
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 2
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 2
CardManager: OnNakamaCardPlayReceived called - Player 101: Eight of Hearts
CardManager: Received card play from Nakama - Player 101: Eight of Hearts
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Eight of Hearts
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Eight of Hearts
CardGameUI: Player 101 played Eight of Hearts
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Eight of Hearts
MatchManager: CardPlayReceived signal emitted for player 101: Eight of Hearts
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 2
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 2) - Previous: Turn 3, Tricks 2
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: Timer update received - 10.0s remaining
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 2
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 2
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 2) - Last processed: Turn 0, Tricks 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 2
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Client timer synced to: 10.0s
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Hearts by player 100
CardGameUI: Added trick card 0: King of Hearts (P100) at position (-5, -20)
CardGameUI: Created trick card button for Eight of Hearts by player 101
CardGameUI: Added trick card 1: Eight of Hearts (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [King of Hearts (P100), Eight of Hearts (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 4 valid cards
CardManager: AI player 101 playing Eight of Hearts
CardManager: Nakama game - sending card play to MatchManager - Player 101: Eight of Hearts
CardManager: Added pending card play: 101_Eight of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 101: Eight of Hearts
CardManager: 🃏 Player 101 hand BEFORE removal: 11 cards [Nine of Clubs, Eight of Hearts, Five of Hearts, Four of Clubs, Six of Clubs, Two of Hearts, Seven of Diamonds, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 10 cards [Nine of Clubs, Five of Hearts, Four of Clubs, Six of Clubs, Two of Hearts, Seven of Diamonds, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Eight of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Eight of Hearts (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Eight of Hearts
CardGameUI: Player 101 played Eight of Hearts
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Eight of Hearts
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 2
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 2
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Eight of Hearts for AI player 101
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Hearts by player 100
CardGameUI: Added trick card 0: King of Hearts (P100) at position (-5, -20)
CardGameUI: Created trick card button for Eight of Hearts by player 101
CardGameUI: Added trick card 1: Eight of Hearts (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [King of Hearts (P100), Eight of Hearts (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 2 cards: [P100:King of Hearts, P101:Eight of Hearts]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Three of Hearts
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 11 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Seven of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Three of Hearts
CardManager: Added pending card play: 0_Three of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 0: Three of Hearts
CardManager: 🃏 Player 0 hand BEFORE removal: 11 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 10 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Three of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Three of Hearts (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Three of Hearts
CardGameUI: Player 0 played Three of Hearts
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 11 cards)
CardGameUI: 🃏 UI cards before update: [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Three of Hearts'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 10 cards
CardGameUI: 🃏 UI currentPlayerCards had 11 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (11): [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (10): [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 11 -> 10 cards
CardGameUI: 🃏 Current cards in hand: [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Three of Hearts
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 10 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Hearts by player 100
CardGameUI: Added trick card 0: King of Hearts (P100) at position (-60, -20)
CardGameUI: Created trick card button for Eight of Hearts by player 101
CardGameUI: Added trick card 1: Eight of Hearts (P101) at position (50, -20)
CardGameUI: Created trick card button for Three of Hearts by player 0
CardGameUI: Added trick card 2: Three of Hearts (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [King of Hearts (P100), Eight of Hearts (P101), Three of Hearts (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
CardGameUI: 🃏 Hand update complete - 11 -> 10 cards (played: Three of Hearts)
CardGameUI: Created card button for Three of Spades with texture
CardGameUI: Created card button for Seven of Hearts with texture
CardGameUI: Created card button for Jack of Spades with texture
CardGameUI: Created card button for Five of Spades with texture
CardGameUI: Created card button for Five of Clubs with texture
CardGameUI: Created card button for Six of Spades with texture
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Ten of Hearts with texture
CardGameUI: Created card button for Seven of Spades with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 10 manually positioned cards in two rows (5 top, 5 bottom)
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 2 cards: [P100:King of Hearts, P101:Eight of Hearts]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 2
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 2
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 2
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 2) - Previous: Turn 0, Tricks 2
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 2
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 2
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 1, Tricks: 2) - Last processed: Turn 1, Tricks 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 2
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Three of Hearts
MatchManager: Card play synchronized - Client played Three of Hearts
CardManager: OnNakamaCardPlayReceived called - Player 0: Three of Hearts
CardManager: Received card play from Nakama - Player 0: Three of Hearts
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Three of Hearts
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Three of Hearts
CardGameUI: Player 0 played Three of Hearts
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 2
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 2
CardGameUI: Turn started for player 2
CardManager: Synchronized card play completed - Player 0: Three of Hearts
MatchManager: CardPlayReceived signal emitted for player 0: Three of Hearts
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Hearts by player 100
CardGameUI: Added trick card 0: King of Hearts (P100) at position (-60, -20)
CardGameUI: Created trick card button for Eight of Hearts by player 101
CardGameUI: Added trick card 1: Eight of Hearts (P101) at position (50, -20)
CardGameUI: Created trick card button for Three of Hearts by player 0
CardGameUI: Added trick card 2: Three of Hearts (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [King of Hearts (P100), Eight of Hearts (P101), Three of Hearts (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Queen of Hearts
MatchManager: Card play synchronized - mXTgVBRUod played Queen of Hearts
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 2
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 100, Leader: 2, Score: 3
CardManager: OnNakamaCardPlayReceived called - Player 2: Queen of Hearts
CardManager: Received card play from Nakama - Player 2: Queen of Hearts
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Queen of Hearts
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Queen of Hearts
CardGameUI: Player 2 played Queen of Hearts
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Queen of Hearts
MatchManager: CardPlayReceived signal emitted for player 2: Queen of Hearts
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 2
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 2) - Previous: Turn 1, Tricks 2
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 2
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 100, Leader: 2, Score: 3
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [King of Hearts (P100), Eight of Hearts (P101), Three of Hearts (P0), Queen of Hearts (P2)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 100
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 2, Turn: 2
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 100, Leader: 2, Score: 3
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Hearts by player 100
CardGameUI: Added trick card 0: King of Hearts (P100) at position (-115, -20)
CardGameUI: Created trick card button for Eight of Hearts by player 101
CardGameUI: Added trick card 1: Eight of Hearts (P101) at position (-5, -20)
CardGameUI: Created trick card button for Three of Hearts by player 0
CardGameUI: Added trick card 2: Three of Hearts (P0) at position (105, -20)
CardGameUI: Created trick card button for Queen of Hearts by player 2
CardGameUI: Added trick card 3: Queen of Hearts (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [King of Hearts (P100), Eight of Hearts (P101), Three of Hearts (P0), Queen of Hearts (P2)]
CardGameUI: Current trick leader: 2, Current turn: 2
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P100:King of Hearts, P101:Eight of Hearts, P0:Three of Hearts]
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Queen of Hearts
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 11 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Hearts
CardManager: Added pending card play: 2_Queen of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 2: Queen of Hearts
CardManager: 🃏 Player 2 hand BEFORE removal: 11 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 10 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Queen of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Queen of Hearts (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Queen of Hearts
CardGameUI: Player 2 played Queen of Hearts
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 11 cards)
CardGameUI: 🃏 UI cards before update: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Queen of Hearts'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 10 cards
CardGameUI: 🃏 UI currentPlayerCards had 11 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (11): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (10): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 11 -> 10 cards
CardGameUI: 🃏 Current cards in hand: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Queen of Hearts
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 2
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 100 wins trick with King of Hearts
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 100)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 100, Leader: 2, Score: 3
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [King of Hearts (P100), Eight of Hearts (P101), Three of Hearts (P0), Queen of Hearts (P2)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 100
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 100, 10-second display period started
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 10 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Hearts by player 100
CardGameUI: Added trick card 0: King of Hearts (P100) at position (-115, -20)
CardGameUI: Created trick card button for Eight of Hearts by player 101
CardGameUI: Added trick card 1: Eight of Hearts (P101) at position (-5, -20)
CardGameUI: Created trick card button for Three of Hearts by player 0
CardGameUI: Added trick card 2: Three of Hearts (P0) at position (105, -20)
CardGameUI: Created trick card button for Queen of Hearts by player 2
CardGameUI: Added trick card 3: Queen of Hearts (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [King of Hearts (P100), Eight of Hearts (P101), Three of Hearts (P0), Queen of Hearts (P2)]
CardGameUI: Current trick leader: 2, Current turn: 2
CardGameUI: 🃏 Hand update complete - 11 -> 10 cards (played: Queen of Hearts)
CardGameUI: Created card button for Nine of Diamonds with texture
CardGameUI: Created card button for Ten of Diamonds with texture
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Ace of Clubs with texture
CardGameUI: Created card button for Six of Diamonds with texture
CardGameUI: Created card button for Ace of Hearts with texture
CardGameUI: Created card button for Five of Diamonds with texture
CardGameUI: Created card button for Three of Clubs with texture
CardGameUI: Created card button for Ten of Spades with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 10 manually positioned cards in two rows (5 top, 5 bottom)
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 100
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 3, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 100
CardGameUI: Turn started for player 100
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 100
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 3, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: King of Clubs
MatchManager: Card play synchronized - Player_100 played King of Clubs
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 3
CardManager: OnNakamaCardPlayReceived called - Player 100: King of Clubs
CardManager: Received card play from Nakama - Player 100: King of Clubs
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: King of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: King of Clubs
CardGameUI: Player 100 played King of Clubs
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: King of Clubs
MatchManager: CardPlayReceived signal emitted for player 100: King of Clubs
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 3
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 3) - Previous: Turn 2, Tricks 2
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 3
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Clubs by player 100
CardGameUI: Added trick card 0: King of Clubs (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [King of Clubs (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 10 valid cards
CardManager: AI player 100 playing King of Clubs
CardManager: Nakama game - sending card play to MatchManager - Player 100: King of Clubs
CardManager: Added pending card play: 100_King of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 100: King of Clubs
CardManager: 🃏 Player 100 hand BEFORE removal: 10 cards [King of Clubs, Six of Hearts, Jack of Diamonds, Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 9 cards [Six of Hearts, Jack of Diamonds, Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: King of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: King of Clubs (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: King of Clubs
CardGameUI: Player 100 played King of Clubs
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: King of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 3
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card King of Clubs for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Clubs by player 100
CardGameUI: Added trick card 0: King of Clubs (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [King of Clubs (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Nine of Clubs
MatchManager: Card play synchronized - Player_101 played Nine of Clubs
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 3
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 3
CardManager: OnNakamaCardPlayReceived called - Player 101: Nine of Clubs
CardManager: Received card play from Nakama - Player 101: Nine of Clubs
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Nine of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Nine of Clubs
CardGameUI: Player 101 played Nine of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Nine of Clubs
MatchManager: CardPlayReceived signal emitted for player 101: Nine of Clubs
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 3
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 3) - Previous: Turn 3, Tricks 3
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 3
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 3
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 3) - Last processed: Turn 0, Tricks 3
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 3
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Clubs by player 100
CardGameUI: Added trick card 0: King of Clubs (P100) at position (-5, -20)
CardGameUI: Created trick card button for Nine of Clubs by player 101
CardGameUI: Added trick card 1: Nine of Clubs (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [King of Clubs (P100), Nine of Clubs (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 6 valid cards
CardManager: AI player 101 playing Nine of Clubs
CardManager: Nakama game - sending card play to MatchManager - Player 101: Nine of Clubs
CardManager: Added pending card play: 101_Nine of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 101: Nine of Clubs
CardManager: 🃏 Player 101 hand BEFORE removal: 10 cards [Nine of Clubs, Five of Hearts, Four of Clubs, Six of Clubs, Two of Hearts, Seven of Diamonds, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 9 cards [Five of Hearts, Four of Clubs, Six of Clubs, Two of Hearts, Seven of Diamonds, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Nine of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Nine of Clubs (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Nine of Clubs
CardGameUI: Player 101 played Nine of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Nine of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 3
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 3
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Nine of Clubs for AI player 101
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Clubs by player 100
CardGameUI: Added trick card 0: King of Clubs (P100) at position (-5, -20)
CardGameUI: Created trick card button for Nine of Clubs by player 101
CardGameUI: Added trick card 1: Nine of Clubs (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [King of Clubs (P100), Nine of Clubs (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 2 cards: [P100:King of Clubs, P101:Nine of Clubs]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Five of Clubs
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 10 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Five of Clubs
CardManager: Added pending card play: 0_Five of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 0: Five of Clubs
CardManager: 🃏 Player 0 hand BEFORE removal: 10 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 9 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Five of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Five of Clubs (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Five of Clubs
CardGameUI: Player 0 played Five of Clubs
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 10 cards)
CardGameUI: 🃏 UI cards before update: [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Five of Clubs'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 9 cards
CardGameUI: 🃏 UI currentPlayerCards had 10 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (10): [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (9): [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 10 -> 9 cards
CardGameUI: 🃏 Current cards in hand: [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Five of Clubs
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 9 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Clubs by player 100
CardGameUI: Added trick card 0: King of Clubs (P100) at position (-60, -20)
CardGameUI: Created trick card button for Nine of Clubs by player 101
CardGameUI: Added trick card 1: Nine of Clubs (P101) at position (50, -20)
CardGameUI: Created trick card button for Five of Clubs by player 0
CardGameUI: Added trick card 2: Five of Clubs (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [King of Clubs (P100), Nine of Clubs (P101), Five of Clubs (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
CardGameUI: 🃏 Hand update complete - 10 -> 9 cards (played: Five of Clubs)
CardGameUI: Created card button for Three of Spades with texture
CardGameUI: Created card button for Seven of Hearts with texture
CardGameUI: Created card button for Jack of Spades with texture
CardGameUI: Created card button for Five of Spades with texture
CardGameUI: Created card button for Six of Spades with texture
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Ten of Hearts with texture
CardGameUI: Created card button for Seven of Spades with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 9 manually positioned cards in two rows (5 top, 4 bottom)
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 2 cards: [P100:King of Clubs, P101:Nine of Clubs]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 3
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 3
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 3
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 3) - Previous: Turn 0, Tricks 3
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 3
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 3
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 1, Tricks: 3) - Last processed: Turn 1, Tricks 3
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 3
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Five of Clubs
MatchManager: Card play synchronized - Client played Five of Clubs
CardManager: OnNakamaCardPlayReceived called - Player 0: Five of Clubs
CardManager: Received card play from Nakama - Player 0: Five of Clubs
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Five of Clubs
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Five of Clubs
CardGameUI: Player 0 played Five of Clubs
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 3
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 3
CardGameUI: Turn started for player 2
CardManager: Synchronized card play completed - Player 0: Five of Clubs
MatchManager: CardPlayReceived signal emitted for player 0: Five of Clubs
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Clubs by player 100
CardGameUI: Added trick card 0: King of Clubs (P100) at position (-60, -20)
CardGameUI: Created trick card button for Nine of Clubs by player 101
CardGameUI: Added trick card 1: Nine of Clubs (P101) at position (50, -20)
CardGameUI: Created trick card button for Five of Clubs by player 0
CardGameUI: Added trick card 2: Five of Clubs (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [King of Clubs (P100), Nine of Clubs (P101), Five of Clubs (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Three of Clubs
MatchManager: Card play synchronized - mXTgVBRUod played Three of Clubs
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 3
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 100, Leader: 2, Score: 4
CardManager: OnNakamaCardPlayReceived called - Player 2: Three of Clubs
CardManager: Received card play from Nakama - Player 2: Three of Clubs
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Three of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Three of Clubs
CardGameUI: Player 2 played Three of Clubs
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Three of Clubs
MatchManager: CardPlayReceived signal emitted for player 2: Three of Clubs
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 3
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 3) - Previous: Turn 1, Tricks 3
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 3
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 100, Leader: 2, Score: 4
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [King of Clubs (P100), Nine of Clubs (P101), Five of Clubs (P0), Three of Clubs (P2)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 100
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 2, Turn: 2
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 100, Leader: 2, Score: 4
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Clubs by player 100
CardGameUI: Added trick card 0: King of Clubs (P100) at position (-115, -20)
CardGameUI: Created trick card button for Nine of Clubs by player 101
CardGameUI: Added trick card 1: Nine of Clubs (P101) at position (-5, -20)
CardGameUI: Created trick card button for Five of Clubs by player 0
CardGameUI: Added trick card 2: Five of Clubs (P0) at position (105, -20)
CardGameUI: Created trick card button for Three of Clubs by player 2
CardGameUI: Added trick card 3: Three of Clubs (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [King of Clubs (P100), Nine of Clubs (P101), Five of Clubs (P0), Three of Clubs (P2)]
CardGameUI: Current trick leader: 2, Current turn: 2
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P100:King of Clubs, P101:Nine of Clubs, P0:Five of Clubs]
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Three of Clubs
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 10 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Three of Clubs
CardManager: Added pending card play: 2_Three of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 2: Three of Clubs
CardManager: 🃏 Player 2 hand BEFORE removal: 10 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 9 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Three of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Three of Clubs (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Three of Clubs
CardGameUI: Player 2 played Three of Clubs
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 10 cards)
CardGameUI: 🃏 UI cards before update: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Three of Clubs'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 9 cards
CardGameUI: 🃏 UI currentPlayerCards had 10 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (10): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (9): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 10 -> 9 cards
CardGameUI: 🃏 Current cards in hand: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Three of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 3
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 100 wins trick with King of Clubs
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 100)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 100, Leader: 2, Score: 4
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [King of Clubs (P100), Nine of Clubs (P101), Five of Clubs (P0), Three of Clubs (P2)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 100
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 100, 10-second display period started
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 9 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for King of Clubs by player 100
CardGameUI: Added trick card 0: King of Clubs (P100) at position (-115, -20)
CardGameUI: Created trick card button for Nine of Clubs by player 101
CardGameUI: Added trick card 1: Nine of Clubs (P101) at position (-5, -20)
CardGameUI: Created trick card button for Five of Clubs by player 0
CardGameUI: Added trick card 2: Five of Clubs (P0) at position (105, -20)
CardGameUI: Created trick card button for Three of Clubs by player 2
CardGameUI: Added trick card 3: Three of Clubs (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [King of Clubs (P100), Nine of Clubs (P101), Five of Clubs (P0), Three of Clubs (P2)]
CardGameUI: Current trick leader: 2, Current turn: 2
CardGameUI: 🃏 Hand update complete - 10 -> 9 cards (played: Three of Clubs)
CardGameUI: Created card button for Nine of Diamonds with texture
CardGameUI: Created card button for Ten of Diamonds with texture
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Ace of Clubs with texture
CardGameUI: Created card button for Six of Diamonds with texture
CardGameUI: Created card button for Ace of Hearts with texture
CardGameUI: Created card button for Five of Diamonds with texture
CardGameUI: Created card button for Ten of Spades with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 9 manually positioned cards in two rows (5 top, 4 bottom)
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 100
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 4, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 100
CardGameUI: Turn started for player 100
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 100
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 4, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: Six of Hearts
MatchManager: Card play synchronized - Player_100 played Six of Hearts
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 4
CardManager: OnNakamaCardPlayReceived called - Player 100: Six of Hearts
CardManager: Received card play from Nakama - Player 100: Six of Hearts
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: Six of Hearts
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: Six of Hearts
CardGameUI: Player 100 played Six of Hearts
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: Six of Hearts
MatchManager: CardPlayReceived signal emitted for player 100: Six of Hearts
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 4
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 4) - Previous: Turn 2, Tricks 3
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 4
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Hearts by player 100
CardGameUI: Added trick card 0: Six of Hearts (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Six of Hearts (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 9 valid cards
CardManager: AI player 100 playing Six of Hearts
CardManager: Nakama game - sending card play to MatchManager - Player 100: Six of Hearts
CardManager: Added pending card play: 100_Six of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 100: Six of Hearts
CardManager: 🃏 Player 100 hand BEFORE removal: 9 cards [Six of Hearts, Jack of Diamonds, Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 8 cards [Jack of Diamonds, Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Six of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Six of Hearts (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: Six of Hearts
CardGameUI: Player 100 played Six of Hearts
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: Six of Hearts
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 4
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card Six of Hearts for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Hearts by player 100
CardGameUI: Added trick card 0: Six of Hearts (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Six of Hearts (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Five of Hearts
MatchManager: Card play synchronized - Player_101 played Five of Hearts
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 4
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 4
CardManager: OnNakamaCardPlayReceived called - Player 101: Five of Hearts
CardManager: Received card play from Nakama - Player 101: Five of Hearts
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Five of Hearts
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Five of Hearts
CardGameUI: Player 101 played Five of Hearts
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Five of Hearts
MatchManager: CardPlayReceived signal emitted for player 101: Five of Hearts
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 4
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 4) - Previous: Turn 3, Tricks 4
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 4
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 4
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 4) - Last processed: Turn 0, Tricks 4
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 4
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Hearts by player 100
CardGameUI: Added trick card 0: Six of Hearts (P100) at position (-5, -20)
CardGameUI: Created trick card button for Five of Hearts by player 101
CardGameUI: Added trick card 1: Five of Hearts (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Six of Hearts (P100), Five of Hearts (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: Client timer synced to: 10.0s
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 3 valid cards
CardManager: AI player 101 playing Five of Hearts
CardManager: Nakama game - sending card play to MatchManager - Player 101: Five of Hearts
CardManager: Added pending card play: 101_Five of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 101: Five of Hearts
CardManager: 🃏 Player 101 hand BEFORE removal: 9 cards [Five of Hearts, Four of Clubs, Six of Clubs, Two of Hearts, Seven of Diamonds, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 8 cards [Four of Clubs, Six of Clubs, Two of Hearts, Seven of Diamonds, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Five of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Five of Hearts (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Five of Hearts
CardGameUI: Player 101 played Five of Hearts
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Five of Hearts
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 4
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 4
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Five of Hearts for AI player 101
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Hearts by player 100
CardGameUI: Added trick card 0: Six of Hearts (P100) at position (-5, -20)
CardGameUI: Created trick card button for Five of Hearts by player 101
CardGameUI: Added trick card 1: Five of Hearts (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Six of Hearts (P100), Five of Hearts (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 2 cards: [P100:Six of Hearts, P101:Five of Hearts]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Seven of Hearts
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 9 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Seven of Hearts
CardManager: Added pending card play: 0_Seven of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 0: Seven of Hearts
CardManager: 🃏 Player 0 hand BEFORE removal: 9 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 8 cards [Three of Spades, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Seven of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Seven of Hearts (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Seven of Hearts
CardGameUI: Player 0 played Seven of Hearts
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 9 cards)
CardGameUI: 🃏 UI cards before update: [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Seven of Hearts'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 8 cards
CardGameUI: 🃏 UI currentPlayerCards had 9 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (9): [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (8): [Three of Spades, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 9 -> 8 cards
CardGameUI: 🃏 Current cards in hand: [Three of Spades, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Seven of Hearts
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 8 cards [Three of Spades, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Hearts by player 100
CardGameUI: Added trick card 0: Six of Hearts (P100) at position (-60, -20)
CardGameUI: Created trick card button for Five of Hearts by player 101
CardGameUI: Added trick card 1: Five of Hearts (P101) at position (50, -20)
CardGameUI: Created trick card button for Seven of Hearts by player 0
CardGameUI: Added trick card 2: Seven of Hearts (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Six of Hearts (P100), Five of Hearts (P101), Seven of Hearts (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
CardGameUI: 🃏 Hand update complete - 9 -> 8 cards (played: Seven of Hearts)
CardGameUI: Created card button for Three of Spades with texture
CardGameUI: Created card button for Jack of Spades with texture
CardGameUI: Created card button for Five of Spades with texture
CardGameUI: Created card button for Six of Spades with texture
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Ten of Hearts with texture
CardGameUI: Created card button for Seven of Spades with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 8 manually positioned cards in two rows (4 top, 4 bottom)
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 2 cards: [P100:Six of Hearts, P101:Five of Hearts]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 4
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 4
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 4
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 4) - Previous: Turn 0, Tricks 4
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 4
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 4
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 1, Tricks: 4) - Last processed: Turn 1, Tricks 4
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 4
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Seven of Hearts
MatchManager: Card play synchronized - Client played Seven of Hearts
CardManager: OnNakamaCardPlayReceived called - Player 0: Seven of Hearts
CardManager: Received card play from Nakama - Player 0: Seven of Hearts
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Seven of Hearts
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Seven of Hearts
CardGameUI: Player 0 played Seven of Hearts
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 4
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 4
CardGameUI: Turn started for player 2
CardManager: Synchronized card play completed - Player 0: Seven of Hearts
MatchManager: CardPlayReceived signal emitted for player 0: Seven of Hearts
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Hearts by player 100
CardGameUI: Added trick card 0: Six of Hearts (P100) at position (-60, -20)
CardGameUI: Created trick card button for Five of Hearts by player 101
CardGameUI: Added trick card 1: Five of Hearts (P101) at position (50, -20)
CardGameUI: Created trick card button for Seven of Hearts by player 0
CardGameUI: Added trick card 2: Seven of Hearts (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Six of Hearts (P100), Five of Hearts (P101), Seven of Hearts (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Ace of Hearts
MatchManager: Card play synchronized - mXTgVBRUod played Ace of Hearts
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 4
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 2, Leader: 1, Score: 1
CardManager: OnNakamaCardPlayReceived called - Player 2: Ace of Hearts
CardManager: Received card play from Nakama - Player 2: Ace of Hearts
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Ace of Hearts
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Ace of Hearts
CardGameUI: Player 2 played Ace of Hearts
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Ace of Hearts
MatchManager: CardPlayReceived signal emitted for player 2: Ace of Hearts
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 4
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 4) - Previous: Turn 1, Tricks 4
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 4
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 2, Leader: 1, Score: 1
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 2 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Six of Hearts (P100), Five of Hearts (P101), Seven of Hearts (P0), Ace of Hearts (P2)]
CardGameUI: Next trick leader will be: 2
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 2
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 2, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: mXTgVBRUod, Next Leader: mXTgVBRUod
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 1, Turn: 1
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 2, Leader: 1, Score: 1
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Hearts by player 100
CardGameUI: Added trick card 0: Six of Hearts (P100) at position (-115, -20)
CardGameUI: Created trick card button for Five of Hearts by player 101
CardGameUI: Added trick card 1: Five of Hearts (P101) at position (-5, -20)
CardGameUI: Created trick card button for Seven of Hearts by player 0
CardGameUI: Added trick card 2: Seven of Hearts (P0) at position (105, -20)
CardGameUI: Created trick card button for Ace of Hearts by player 2
CardGameUI: Added trick card 3: Ace of Hearts (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Six of Hearts (P100), Five of Hearts (P101), Seven of Hearts (P0), Ace of Hearts (P2)]
CardGameUI: Current trick leader: 1, Current turn: 1
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P100:Six of Hearts, P101:Five of Hearts, P0:Seven of Hearts]
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Ace of Hearts
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 9 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Ace of Hearts
CardManager: Added pending card play: 2_Ace of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 2: Ace of Hearts
CardManager: 🃏 Player 2 hand BEFORE removal: 9 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 8 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Ace of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Ace of Hearts (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Ace of Hearts
CardGameUI: Player 2 played Ace of Hearts
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 9 cards)
CardGameUI: 🃏 UI cards before update: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Ace of Hearts'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 8 cards
CardGameUI: 🃏 UI currentPlayerCards had 9 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (9): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Five of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (8): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Five of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 9 -> 8 cards
CardGameUI: 🃏 Current cards in hand: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Ace of Hearts
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 4
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 2 wins trick with Ace of Hearts
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 2)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 2, Leader: 1, Score: 1
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 2 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Six of Hearts (P100), Five of Hearts (P101), Seven of Hearts (P0), Ace of Hearts (P2)]
CardGameUI: Next trick leader will be: 2
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 2
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 2, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: mXTgVBRUod, Next Leader: mXTgVBRUod
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 2, 10-second display period started
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 8 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Hearts by player 100
CardGameUI: Added trick card 0: Six of Hearts (P100) at position (-115, -20)
CardGameUI: Created trick card button for Five of Hearts by player 101
CardGameUI: Added trick card 1: Five of Hearts (P101) at position (-5, -20)
CardGameUI: Created trick card button for Seven of Hearts by player 0
CardGameUI: Added trick card 2: Seven of Hearts (P0) at position (105, -20)
CardGameUI: Created trick card button for Ace of Hearts by player 2
CardGameUI: Added trick card 3: Ace of Hearts (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Six of Hearts (P100), Five of Hearts (P101), Seven of Hearts (P0), Ace of Hearts (P2)]
CardGameUI: Current trick leader: 1, Current turn: 1
CardGameUI: 🃏 Hand update complete - 9 -> 8 cards (played: Ace of Hearts)
CardGameUI: Created card button for Nine of Diamonds with texture
CardGameUI: Created card button for Ten of Diamonds with texture
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Ace of Clubs with texture
CardGameUI: Created card button for Six of Diamonds with texture
CardGameUI: Created card button for Five of Diamonds with texture
CardGameUI: Created card button for Ten of Spades with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 8 manually positioned cards in two rows (4 top, 4 bottom)
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 2
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 5, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 5
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 2
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 5, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 5
CardGameUI: Turn started for player 2
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 5
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 5) - Previous: Turn 2, Tricks 4
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 5
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 5.0s remaining
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Timer update received - 5.0s remaining
CardManager[PID:54510]: Client timer synced to: 5.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 31
MatchManager: Timer update received - 0.0s remaining
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 0.0s remaining
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 0 cards: []
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Five of Diamonds
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 8 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Five of Diamonds
CardManager: Added pending card play: 2_Five of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 2: Five of Diamonds
CardManager: 🃏 Player 2 hand BEFORE removal: 8 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Five of Diamonds, Ten of Spades, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 7 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Five of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Five of Diamonds (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Five of Diamonds
CardGameUI: Player 2 played Five of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 8 cards)
CardGameUI: 🃏 UI cards before update: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Five of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Five of Diamonds'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 7 cards
CardGameUI: 🃏 UI currentPlayerCards had 8 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (8): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Five of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (7): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 8 -> 7 cards
CardGameUI: 🃏 Current cards in hand: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Five of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 5
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 7 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Five of Diamonds by player 2
CardGameUI: Added trick card 0: Five of Diamonds (P2) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Five of Diamonds (P2)]
CardGameUI: Current trick leader: 1, Current turn: 2
CardGameUI: 🃏 Hand update complete - 8 -> 7 cards (played: Five of Diamonds)
CardGameUI: Created card button for Nine of Diamonds with texture
CardGameUI: Created card button for Ten of Diamonds with texture
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Ace of Clubs with texture
CardGameUI: Created card button for Six of Diamonds with texture
CardGameUI: Created card button for Ten of Spades with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 7 manually positioned cards in single row
CardManager[PID:54510]: Client timer synced to: 0.0s
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Five of Diamonds
MatchManager: Card play synchronized - mXTgVBRUod played Five of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 5
CardManager: OnNakamaCardPlayReceived called - Player 2: Five of Diamonds
CardManager: Received card play from Nakama - Player 2: Five of Diamonds
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Five of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Five of Diamonds
CardGameUI: Player 2 played Five of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Five of Diamonds
MatchManager: CardPlayReceived signal emitted for player 2: Five of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 5
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 5) - Previous: Turn 1, Tricks 5
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 5
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Five of Diamonds by player 2
CardGameUI: Added trick card 0: Five of Diamonds (P2) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Five of Diamonds (P2)]
CardGameUI: Current trick leader: 1, Current turn: 2
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: Jack of Diamonds
MatchManager: Card play synchronized - Player_100 played Jack of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 5
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 4 valid cards
CardManager: AI player 100 playing Jack of Diamonds
CardManager: Nakama game - sending card play to MatchManager - Player 100: Jack of Diamonds
CardManager: Added pending card play: 100_Jack of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 100: Jack of Diamonds
CardManager: 🃏 Player 100 hand BEFORE removal: 8 cards [Jack of Diamonds, Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 7 cards [Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Jack of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Jack of Diamonds (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: Jack of Diamonds
CardGameUI: Player 100 played Jack of Diamonds
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: Jack of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 5
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card Jack of Diamonds for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Five of Diamonds by player 2
CardGameUI: Added trick card 0: Five of Diamonds (P2) at position (-5, -20)
CardGameUI: Created trick card button for Jack of Diamonds by player 100
CardGameUI: Added trick card 1: Jack of Diamonds (P100) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Five of Diamonds (P2), Jack of Diamonds (P100)]
CardGameUI: Current trick leader: 1, Current turn: 3
CardManager: OnNakamaCardPlayReceived called - Player 100: Jack of Diamonds
CardManager: Received card play from Nakama - Player 100: Jack of Diamonds
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: Jack of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: Jack of Diamonds
CardGameUI: Player 100 played Jack of Diamonds
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: Jack of Diamonds
MatchManager: CardPlayReceived signal emitted for player 100: Jack of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 5
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 5) - Previous: Turn 2, Tricks 5
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 5
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Five of Diamonds by player 2
CardGameUI: Added trick card 0: Five of Diamonds (P2) at position (-5, -20)
CardGameUI: Created trick card button for Jack of Diamonds by player 100
CardGameUI: Added trick card 1: Jack of Diamonds (P100) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Five of Diamonds (P2), Jack of Diamonds (P100)]
CardGameUI: Current trick leader: 1, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Seven of Diamonds
MatchManager: Card play synchronized - Player_101 played Seven of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 5
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 5
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager: OnNakamaCardPlayReceived called - Player 101: Seven of Diamonds
CardManager: Received card play from Nakama - Player 101: Seven of Diamonds
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Seven of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Seven of Diamonds
CardGameUI: Player 101 played Seven of Diamonds
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Seven of Diamonds
MatchManager: CardPlayReceived signal emitted for player 101: Seven of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 5
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 5) - Previous: Turn 3, Tricks 5
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 5
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 5
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 5) - Last processed: Turn 0, Tricks 5
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 5
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Client timer synced to: 10.0s
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Five of Diamonds by player 2
CardGameUI: Added trick card 0: Five of Diamonds (P2) at position (-60, -20)
CardGameUI: Created trick card button for Jack of Diamonds by player 100
CardGameUI: Added trick card 1: Jack of Diamonds (P100) at position (50, -20)
CardGameUI: Created trick card button for Seven of Diamonds by player 101
CardGameUI: Added trick card 2: Seven of Diamonds (P101) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Five of Diamonds (P2), Jack of Diamonds (P100), Seven of Diamonds (P101)]
CardGameUI: Current trick leader: 1, Current turn: 0
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 1 valid cards
CardManager: AI player 101 playing Seven of Diamonds
CardManager: Nakama game - sending card play to MatchManager - Player 101: Seven of Diamonds
CardManager: Added pending card play: 101_Seven of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 101: Seven of Diamonds
CardManager: 🃏 Player 101 hand BEFORE removal: 8 cards [Four of Clubs, Six of Clubs, Two of Hearts, Seven of Diamonds, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 7 cards [Four of Clubs, Six of Clubs, Two of Hearts, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Seven of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Seven of Diamonds (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Seven of Diamonds
CardGameUI: Player 101 played Seven of Diamonds
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Seven of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 5
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 5
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Seven of Diamonds for AI player 101
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Five of Diamonds by player 2
CardGameUI: Added trick card 0: Five of Diamonds (P2) at position (-60, -20)
CardGameUI: Created trick card button for Jack of Diamonds by player 100
CardGameUI: Added trick card 1: Jack of Diamonds (P100) at position (50, -20)
CardGameUI: Created trick card button for Seven of Diamonds by player 101
CardGameUI: Added trick card 2: Seven of Diamonds (P101) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Five of Diamonds (P2), Jack of Diamonds (P100), Seven of Diamonds (P101)]
CardGameUI: Current trick leader: 1, Current turn: 0
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 3 cards: [P2:Five of Diamonds, P100:Jack of Diamonds, P101:Seven of Diamonds]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Three of Spades
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 8 cards [Three of Spades, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Three of Spades
CardManager: Added pending card play: 0_Three of Spades
CardManager: NAKAMA GAME - executing card immediately: Player 0: Three of Spades
CardManager: 🃏 Player 0 hand BEFORE removal: 8 cards [Three of Spades, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 7 cards [Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Three of Spades
CardManager: NAKAMA - Added card to CurrentTrick immediately: Three of Spades (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Three of Spades
CardGameUI: Player 0 played Three of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 8 cards)
CardGameUI: 🃏 UI cards before update: [Three of Spades, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Three of Spades'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 7 cards
CardGameUI: 🃏 UI currentPlayerCards had 8 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (8): [Three of Spades, Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (7): [Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 8 -> 7 cards
CardGameUI: 🃏 Current cards in hand: [Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Three of Spades
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 100 wins trick with Jack of Diamonds
CardManager[PID:54510]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 100)
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Five of Diamonds (P2), Jack of Diamonds (P100), Seven of Diamonds (P101), Three of Spades (P0)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 100
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 100, 10-second display period started
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 7 cards [Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Five of Diamonds by player 2
CardGameUI: Added trick card 0: Five of Diamonds (P2) at position (-115, -20)
CardGameUI: Created trick card button for Jack of Diamonds by player 100
CardGameUI: Added trick card 1: Jack of Diamonds (P100) at position (-5, -20)
CardGameUI: Created trick card button for Seven of Diamonds by player 101
CardGameUI: Added trick card 2: Seven of Diamonds (P101) at position (105, -20)
CardGameUI: Created trick card button for Three of Spades by player 0
CardGameUI: Added trick card 3: Three of Spades (P0) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Five of Diamonds (P2), Jack of Diamonds (P100), Seven of Diamonds (P101), Three of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 2
CardGameUI: 🃏 Hand update complete - 8 -> 7 cards (played: Three of Spades)
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P2:Five of Diamonds, P100:Jack of Diamonds, P101:Seven of Diamonds]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
CardGameUI: Created card button for Jack of Spades with texture
CardGameUI: Created card button for Five of Spades with texture
CardGameUI: Created card button for Six of Spades with texture
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Ten of Hearts with texture
CardGameUI: Created card button for Seven of Spades with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 7 manually positioned cards in single row
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Three of Spades
MatchManager: Card play synchronized - Client played Three of Spades
CardManager: OnNakamaCardPlayReceived called - Player 0: Three of Spades
CardManager: Received card play from Nakama - Player 0: Three of Spades
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Three of Spades
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Three of Spades
CardGameUI: Player 0 played Three of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 5
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 100 wins trick with Jack of Diamonds
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 100)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 100, Leader: 2, Score: 5
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Five of Diamonds (P2), Jack of Diamonds (P100), Seven of Diamonds (P101), Three of Spades (P0)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 100
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 100, 10-second display period started
CardManager: Synchronized card play completed - Player 0: Three of Spades
MatchManager: CardPlayReceived signal emitted for player 0: Three of Spades
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Five of Diamonds by player 2
CardGameUI: Added trick card 0: Five of Diamonds (P2) at position (-115, -20)
CardGameUI: Created trick card button for Jack of Diamonds by player 100
CardGameUI: Added trick card 1: Jack of Diamonds (P100) at position (-5, -20)
CardGameUI: Created trick card button for Seven of Diamonds by player 101
CardGameUI: Added trick card 2: Seven of Diamonds (P101) at position (105, -20)
CardGameUI: Created trick card button for Three of Spades by player 0
CardGameUI: Added trick card 3: Three of Spades (P0) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Five of Diamonds (P2), Jack of Diamonds (P100), Seven of Diamonds (P101), Three of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 2
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 5
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 100, Leader: 2, Score: 5
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 5
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 5) - Previous: Turn 0, Tricks 5
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: 🛑 Stopping active timer before turn change (had 9.9s remaining)
CardManager[PID:54510]: 🔄 FORCE RESET - Client stuck in EndOfRound, resetting to PlayerTurn for new turn
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 5
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 100, Leader: 2, Score: 5
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Five of Diamonds (P2), Jack of Diamonds (P100), Seven of Diamonds (P101), Three of Spades (P0)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 100
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 2, Turn: 2
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 100, Leader: 2, Score: 5
Leave Table button pressed
GameManager: Player 2 moved from AtTable to InKitchen
CardGameUI: Player 2 location changed to InKitchen
CardGameUI: Player 2 left the table
CardGameUI: Local player left table - switching to kitchen view
CardGameUI: Showing kitchen view (realtime phase) - card game UI hidden
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 100
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 6, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 100
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 6, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 100
CardGameUI: Turn started for player 100
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: Four of Diamonds
MatchManager: Card play synchronized - Player_100 played Four of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 6
CardManager: OnNakamaCardPlayReceived called - Player 100: Four of Diamonds
CardManager: Received card play from Nakama - Player 100: Four of Diamonds
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: Four of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: Four of Diamonds
CardGameUI: Player 100 played Four of Diamonds
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: Four of Diamonds
MatchManager: CardPlayReceived signal emitted for player 100: Four of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 6
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 6) - Previous: Turn 1, Tricks 5
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 6
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Four of Diamonds by player 100
CardGameUI: Added trick card 0: Four of Diamonds (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Four of Diamonds (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 7 valid cards
CardManager: AI player 100 playing Four of Diamonds
CardManager: Nakama game - sending card play to MatchManager - Player 100: Four of Diamonds
CardManager: Added pending card play: 100_Four of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 100: Four of Diamonds
CardManager: 🃏 Player 100 hand BEFORE removal: 7 cards [Four of Diamonds, Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 6 cards [Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Four of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Four of Diamonds (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: Four of Diamonds
CardGameUI: Player 100 played Four of Diamonds
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: Four of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 6
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card Four of Diamonds for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Four of Diamonds by player 100
CardGameUI: Added trick card 0: Four of Diamonds (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Four of Diamonds (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Four of Clubs
MatchManager: Card play synchronized - Player_101 played Four of Clubs
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 6
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 6
CardManager: OnNakamaCardPlayReceived called - Player 101: Four of Clubs
CardManager: Received card play from Nakama - Player 101: Four of Clubs
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Four of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Four of Clubs
CardGameUI: Player 101 played Four of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Four of Clubs
MatchManager: CardPlayReceived signal emitted for player 101: Four of Clubs
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 6
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 6) - Previous: Turn 3, Tricks 6
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 6
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 6
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 6) - Last processed: Turn 0, Tricks 6
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 6
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Four of Diamonds by player 100
CardGameUI: Added trick card 0: Four of Diamonds (P100) at position (-5, -20)
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
CardGameUI: Created trick card button for Four of Clubs by player 101
CardGameUI: Added trick card 1: Four of Clubs (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Four of Diamonds (P100), Four of Clubs (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: Client timer synced to: 10.0s
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 7 valid cards
CardManager: AI player 101 playing Four of Clubs
CardManager: Nakama game - sending card play to MatchManager - Player 101: Four of Clubs
CardManager: Added pending card play: 101_Four of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 101: Four of Clubs
CardManager: 🃏 Player 101 hand BEFORE removal: 7 cards [Four of Clubs, Six of Clubs, Two of Hearts, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 6 cards [Six of Clubs, Two of Hearts, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Four of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Four of Clubs (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Four of Clubs
CardGameUI: Player 101 played Four of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Four of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 6
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 6
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Four of Clubs for AI player 101
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Four of Diamonds by player 100
CardGameUI: Added trick card 0: Four of Diamonds (P100) at position (-5, -20)
CardGameUI: Created trick card button for Four of Clubs by player 101
CardGameUI: Added trick card 1: Four of Clubs (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Four of Diamonds (P100), Four of Clubs (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Timer update received - 5.0s remaining
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 5.0s remaining
CardManager[PID:54510]: Client timer synced to: 5.0s
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 2 cards: [P100:Four of Diamonds, P101:Four of Clubs]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Five of Spades
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 7 cards [Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Five of Spades
CardManager: Added pending card play: 0_Five of Spades
CardManager: NAKAMA GAME - executing card immediately: Player 0: Five of Spades
CardManager: 🃏 Player 0 hand BEFORE removal: 7 cards [Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 6 cards [Jack of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Five of Spades
CardManager: NAKAMA - Added card to CurrentTrick immediately: Five of Spades (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Five of Spades
CardGameUI: Player 0 played Five of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 7 cards)
CardGameUI: 🃏 UI cards before update: [Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Five of Spades'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 6 cards
CardGameUI: 🃏 UI currentPlayerCards had 7 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (7): [Jack of Spades, Five of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (6): [Jack of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 7 -> 6 cards
CardGameUI: 🃏 Current cards in hand: [Jack of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Five of Spades
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 6 cards [Jack of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Four of Diamonds by player 100
CardGameUI: Added trick card 0: Four of Diamonds (P100) at position (-60, -20)
CardGameUI: Created trick card button for Four of Clubs by player 101
CardGameUI: Added trick card 1: Four of Clubs (P101) at position (50, -20)
CardGameUI: Created trick card button for Five of Spades by player 0
CardGameUI: Added trick card 2: Five of Spades (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Four of Diamonds (P100), Four of Clubs (P101), Five of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
CardGameUI: 🃏 Hand update complete - 7 -> 6 cards (played: Five of Spades)
CardGameUI: Created card button for Jack of Spades with texture
CardGameUI: Created card button for Six of Spades with texture
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Ten of Hearts with texture
CardGameUI: Created card button for Seven of Spades with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 6 manually positioned cards in single row
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 2 cards: [P100:Four of Diamonds, P101:Four of Clubs]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 6
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 6
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Five of Spades
MatchManager: Card play synchronized - Client played Five of Spades
CardManager: OnNakamaCardPlayReceived called - Player 0: Five of Spades
CardManager: Received card play from Nakama - Player 0: Five of Spades
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Five of Spades
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Five of Spades
CardGameUI: Player 0 played Five of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 6
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 6
CardGameUI: Turn started for player 2
CardManager: Synchronized card play completed - Player 0: Five of Spades
MatchManager: CardPlayReceived signal emitted for player 0: Five of Spades
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Four of Diamonds by player 100
CardGameUI: Added trick card 0: Four of Diamonds (P100) at position (-60, -20)
CardGameUI: Created trick card button for Four of Clubs by player 101
CardGameUI: Added trick card 1: Four of Clubs (P101) at position (50, -20)
CardGameUI: Created trick card button for Five of Spades by player 0
CardGameUI: Added trick card 2: Five of Spades (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Four of Diamonds (P100), Four of Clubs (P101), Five of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 6
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 6) - Previous: Turn 0, Tricks 6
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 6
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 6
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 1, Tricks: 6) - Last processed: Turn 1, Tricks 6
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 6
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 26
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 26
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Right (animation: right)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 26
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Down (animation: down)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
Player: Setting sprite direction to Left (animation: left)
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Nine of Diamonds
MatchManager: Card play synchronized - mXTgVBRUod played Nine of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 6
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 2, Leader: 1, Score: 2
CardManager: OnNakamaCardPlayReceived called - Player 2: Nine of Diamonds
CardManager: Received card play from Nakama - Player 2: Nine of Diamonds
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Nine of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Nine of Diamonds
CardGameUI: Player 2 played Nine of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Nine of Diamonds
MatchManager: CardPlayReceived signal emitted for player 2: Nine of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 6
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 6) - Previous: Turn 1, Tricks 6
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 6
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 2, Leader: 1, Score: 2
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 2 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Four of Diamonds (P100), Four of Clubs (P101), Five of Spades (P0), Nine of Diamonds (P2)]
CardGameUI: Next trick leader will be: 2
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 2
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 2, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: mXTgVBRUod, Next Leader: mXTgVBRUod
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 1, Turn: 1
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 2, Leader: 1, Score: 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Four of Diamonds by player 100
CardGameUI: Added trick card 0: Four of Diamonds (P100) at position (-115, -20)
CardGameUI: Created trick card button for Four of Clubs by player 101
CardGameUI: Added trick card 1: Four of Clubs (P101) at position (-5, -20)
CardGameUI: Created trick card button for Five of Spades by player 0
CardGameUI: Added trick card 2: Five of Spades (P0) at position (105, -20)
CardGameUI: Created trick card button for Nine of Diamonds by player 2
CardGameUI: Added trick card 3: Nine of Diamonds (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Four of Diamonds (P100), Four of Clubs (P101), Five of Spades (P0), Nine of Diamonds (P2)]
CardGameUI: Current trick leader: 1, Current turn: 1
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P100:Four of Diamonds, P101:Four of Clubs, P0:Five of Spades]
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: False
CardManager[PID:54511]: Player 2 not at table - auto-playing any valid card
CardManager[PID:54511]: Auto-playing card Nine of Diamonds for player 2
CardManager: Nakama game - sending card play to MatchManager - Player 2: Nine of Diamonds
CardManager: Added pending card play: 2_Nine of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 2: Nine of Diamonds
CardManager: 🃏 Player 2 hand BEFORE removal: 7 cards [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 6 cards [Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Nine of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Nine of Diamonds (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Nine of Diamonds
CardGameUI: Player 2 played Nine of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 7 cards)
CardGameUI: 🃏 UI cards before update: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Nine of Diamonds'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 6 cards
CardGameUI: 🃏 UI currentPlayerCards had 7 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (7): [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (6): [Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 7 -> 6 cards
CardGameUI: 🃏 Current cards in hand: [Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Nine of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 6
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 2 wins trick with Nine of Diamonds
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 2)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 2, Leader: 1, Score: 2
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 2 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Four of Diamonds (P100), Four of Clubs (P101), Five of Spades (P0), Nine of Diamonds (P2)]
CardGameUI: Next trick leader will be: 2
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 2
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 2, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: mXTgVBRUod, Next Leader: mXTgVBRUod
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 2, 10-second display period started
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Four of Diamonds by player 100
CardGameUI: Added trick card 0: Four of Diamonds (P100) at position (-115, -20)
CardGameUI: Created trick card button for Four of Clubs by player 101
CardGameUI: Added trick card 1: Four of Clubs (P101) at position (-5, -20)
CardGameUI: Created trick card button for Five of Spades by player 0
CardGameUI: Added trick card 2: Five of Spades (P0) at position (105, -20)
CardGameUI: Created trick card button for Nine of Diamonds by player 2
CardGameUI: Added trick card 3: Nine of Diamonds (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Four of Diamonds (P100), Four of Clubs (P101), Five of Spades (P0), Nine of Diamonds (P2)]
CardGameUI: Current trick leader: 1, Current turn: 1
CardGameUI: 🃏 Hand update complete - 7 -> 6 cards (played: Nine of Diamonds)
CardGameUI: Created card button for Ten of Diamonds with texture
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Ace of Clubs with texture
CardGameUI: Created card button for Six of Diamonds with texture
CardGameUI: Created card button for Ten of Spades with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 6 manually positioned cards in single row
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Setting sprite direction to Up (animation: up)
Player: Space pressed in kitchen - checking for nearby items
Player: Found 2 potential interaction targets
Player: Found interactive object: CardTable
Player: Interacting with card table - returning to card game
GameManager: Player 2 moved from InKitchen to AtTable
CardGameUI: Player 2 location changed to AtTable
CardGameUI: Player 2 returned to table
CardGameUI: Local player returned to table - switching to card table view
CardGameUI: Showing card table view with full UI
Player: Player 2 returned to card table
DEBUG: Player interacted with item: card_table
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 2
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 7, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 7
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 7
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 7) - Previous: Turn 2, Tricks 6
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 7
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 2
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 7, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 7
CardGameUI: Turn started for player 2
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Timer update received - 5.0s remaining
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 5.0s remaining
CardManager[PID:54510]: Client timer synced to: 5.0s
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 31
MatchManager: Timer update received - 0.0s remaining
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Six of Diamonds
MatchManager: Card play synchronized - mXTgVBRUod played Six of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 7
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 0.0s remaining
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 0 cards: []
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Six of Diamonds
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 6 cards [Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Six of Diamonds
CardManager: Added pending card play: 2_Six of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 2: Six of Diamonds
CardManager: 🃏 Player 2 hand BEFORE removal: 6 cards [Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 5 cards [Ten of Diamonds, King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Six of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Six of Diamonds (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Six of Diamonds
CardGameUI: Player 2 played Six of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 6 cards)
CardGameUI: 🃏 UI cards before update: [Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Six of Diamonds'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 5 cards
CardGameUI: 🃏 UI currentPlayerCards had 6 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (6): [Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (5): [Ten of Diamonds, King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 6 -> 5 cards
CardGameUI: 🃏 Current cards in hand: [Ten of Diamonds, King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Six of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 7
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 5 cards [Ten of Diamonds, King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Diamonds by player 2
CardGameUI: Added trick card 0: Six of Diamonds (P2) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Six of Diamonds (P2)]
CardGameUI: Current trick leader: 1, Current turn: 2
CardGameUI: 🃏 Hand update complete - 6 -> 5 cards (played: Six of Diamonds)
CardGameUI: Created card button for Ten of Diamonds with texture
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Ace of Clubs with texture
CardGameUI: Created card button for Ten of Spades with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 5 manually positioned cards in single row
CardManager[PID:54510]: Client timer synced to: 0.0s
CardManager: OnNakamaCardPlayReceived called - Player 2: Six of Diamonds
CardManager: Received card play from Nakama - Player 2: Six of Diamonds
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Six of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Six of Diamonds
CardGameUI: Player 2 played Six of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Six of Diamonds
MatchManager: CardPlayReceived signal emitted for player 2: Six of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 7
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 7) - Previous: Turn 1, Tricks 7
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 7
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Diamonds by player 2
CardGameUI: Added trick card 0: Six of Diamonds (P2) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Six of Diamonds (P2)]
CardGameUI: Current trick leader: 1, Current turn: 2
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: Ace of Diamonds
MatchManager: Card play synchronized - Player_100 played Ace of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 7
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 2 valid cards
CardManager: AI player 100 playing Ace of Diamonds
CardManager: Nakama game - sending card play to MatchManager - Player 100: Ace of Diamonds
CardManager: Added pending card play: 100_Ace of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 100: Ace of Diamonds
CardManager: 🃏 Player 100 hand BEFORE removal: 6 cards [Ace of Diamonds, Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 5 cards [Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Ace of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Ace of Diamonds (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: Ace of Diamonds
CardGameUI: Player 100 played Ace of Diamonds
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: Ace of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 7
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card Ace of Diamonds for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Diamonds by player 2
CardGameUI: Added trick card 0: Six of Diamonds (P2) at position (-5, -20)
CardGameUI: Created trick card button for Ace of Diamonds by player 100
CardGameUI: Added trick card 1: Ace of Diamonds (P100) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Six of Diamonds (P2), Ace of Diamonds (P100)]
CardGameUI: Current trick leader: 1, Current turn: 3
CardManager: OnNakamaCardPlayReceived called - Player 100: Ace of Diamonds
CardManager: Received card play from Nakama - Player 100: Ace of Diamonds
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: Ace of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: Ace of Diamonds
CardGameUI: Player 100 played Ace of Diamonds
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: Ace of Diamonds
MatchManager: CardPlayReceived signal emitted for player 100: Ace of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 7
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 7) - Previous: Turn 2, Tricks 7
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 7
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Diamonds by player 2
CardGameUI: Added trick card 0: Six of Diamonds (P2) at position (-5, -20)
CardGameUI: Created trick card button for Ace of Diamonds by player 100
CardGameUI: Added trick card 1: Ace of Diamonds (P100) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Six of Diamonds (P2), Ace of Diamonds (P100)]
CardGameUI: Current trick leader: 1, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Six of Clubs
MatchManager: Card play synchronized - Player_101 played Six of Clubs
CardManager: OnNakamaCardPlayReceived called - Player 101: Six of Clubs
CardManager: Received card play from Nakama - Player 101: Six of Clubs
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Six of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Six of Clubs
CardGameUI: Player 101 played Six of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Six of Clubs
MatchManager: CardPlayReceived signal emitted for player 101: Six of Clubs
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Diamonds by player 2
CardGameUI: Added trick card 0: Six of Diamonds (P2) at position (-60, -20)
CardGameUI: Created trick card button for Ace of Diamonds by player 100
CardGameUI: Added trick card 1: Ace of Diamonds (P100) at position (50, -20)
CardGameUI: Created trick card button for Six of Clubs by player 101
CardGameUI: Added trick card 2: Six of Clubs (P101) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Six of Diamonds (P2), Ace of Diamonds (P100), Six of Clubs (P101)]
CardGameUI: Current trick leader: 1, Current turn: 3
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 7
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 7
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 7) - Previous: Turn 3, Tricks 7
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 7
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 7
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 7
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 7) - Last processed: Turn 0, Tricks 7
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 7
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 6 valid cards
CardManager: AI player 101 playing Six of Clubs
CardManager: Nakama game - sending card play to MatchManager - Player 101: Six of Clubs
CardManager: Added pending card play: 101_Six of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 101: Six of Clubs
CardManager: 🃏 Player 101 hand BEFORE removal: 6 cards [Six of Clubs, Two of Hearts, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 5 cards [Two of Hearts, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Six of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Six of Clubs (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Six of Clubs
CardGameUI: Player 101 played Six of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Six of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 7
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 7
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Six of Clubs for AI player 101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Diamonds by player 2
CardGameUI: Added trick card 0: Six of Diamonds (P2) at position (-60, -20)
CardGameUI: Created trick card button for Ace of Diamonds by player 100
CardGameUI: Added trick card 1: Ace of Diamonds (P100) at position (50, -20)
CardGameUI: Created trick card button for Six of Clubs by player 101
CardGameUI: Added trick card 2: Six of Clubs (P101) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Six of Diamonds (P2), Ace of Diamonds (P100), Six of Clubs (P101)]
CardGameUI: Current trick leader: 1, Current turn: 0
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P2:Six of Diamonds, P100:Ace of Diamonds, P101:Six of Clubs]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 3 cards: [P2:Six of Diamonds, P100:Ace of Diamonds, P101:Six of Clubs]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Six of Spades
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 6 cards [Jack of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Six of Spades
CardManager: Added pending card play: 0_Six of Spades
CardManager: NAKAMA GAME - executing card immediately: Player 0: Six of Spades
CardManager: 🃏 Player 0 hand BEFORE removal: 6 cards [Jack of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 5 cards [Jack of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Six of Spades
CardManager: NAKAMA - Added card to CurrentTrick immediately: Six of Spades (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Six of Spades
CardGameUI: Player 0 played Six of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 6 cards)
CardGameUI: 🃏 UI cards before update: [Jack of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Six of Spades'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 5 cards
CardGameUI: 🃏 UI currentPlayerCards had 6 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (6): [Jack of Spades, Six of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (5): [Jack of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 6 -> 5 cards
CardGameUI: 🃏 Current cards in hand: [Jack of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Six of Spades
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 100 wins trick with Ace of Diamonds
CardManager[PID:54510]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 100)
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Six of Diamonds (P2), Ace of Diamonds (P100), Six of Clubs (P101), Six of Spades (P0)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 100
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 100, 10-second display period started
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 5 cards [Jack of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Diamonds by player 2
CardGameUI: Added trick card 0: Six of Diamonds (P2) at position (-115, -20)
CardGameUI: Created trick card button for Ace of Diamonds by player 100
CardGameUI: Added trick card 1: Ace of Diamonds (P100) at position (-5, -20)
CardGameUI: Created trick card button for Six of Clubs by player 101
CardGameUI: Added trick card 2: Six of Clubs (P101) at position (105, -20)
CardGameUI: Created trick card button for Six of Spades by player 0
CardGameUI: Added trick card 3: Six of Spades (P0) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Six of Diamonds (P2), Ace of Diamonds (P100), Six of Clubs (P101), Six of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 2
CardGameUI: 🃏 Hand update complete - 6 -> 5 cards (played: Six of Spades)
CardGameUI: Created card button for Jack of Spades with texture
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Ten of Hearts with texture
CardGameUI: Created card button for Seven of Spades with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 5 manually positioned cards in single row
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Six of Spades
MatchManager: Card play synchronized - Client played Six of Spades
CardManager: OnNakamaCardPlayReceived called - Player 0: Six of Spades
CardManager: Received card play from Nakama - Player 0: Six of Spades
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Six of Spades
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Six of Spades
CardGameUI: Player 0 played Six of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 7
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 100 wins trick with Ace of Diamonds
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 100)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 100, Leader: 2, Score: 6
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Six of Diamonds (P2), Ace of Diamonds (P100), Six of Clubs (P101), Six of Spades (P0)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 100
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 100, 10-second display period started
CardManager: Synchronized card play completed - Player 0: Six of Spades
MatchManager: CardPlayReceived signal emitted for player 0: Six of Spades
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Six of Diamonds by player 2
CardGameUI: Added trick card 0: Six of Diamonds (P2) at position (-115, -20)
CardGameUI: Created trick card button for Ace of Diamonds by player 100
CardGameUI: Added trick card 1: Ace of Diamonds (P100) at position (-5, -20)
CardGameUI: Created trick card button for Six of Clubs by player 101
CardGameUI: Added trick card 2: Six of Clubs (P101) at position (105, -20)
CardGameUI: Created trick card button for Six of Spades by player 0
CardGameUI: Added trick card 3: Six of Spades (P0) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Six of Diamonds (P2), Ace of Diamonds (P100), Six of Clubs (P101), Six of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 2
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 7
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 100, Leader: 2, Score: 6
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 7
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 7) - Previous: Turn 0, Tricks 7
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: 🛑 Stopping active timer before turn change (had 9.9s remaining)
CardManager[PID:54510]: 🔄 FORCE RESET - Client stuck in EndOfRound, resetting to PlayerTurn for new turn
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 7
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 100, Leader: 2, Score: 6
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Six of Diamonds (P2), Ace of Diamonds (P100), Six of Clubs (P101), Six of Spades (P0)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 100
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 2, Turn: 2
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 100, Leader: 2, Score: 6
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 100
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 8, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 100
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 8, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 100
CardGameUI: Turn started for player 100
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 5 valid cards
CardManager: AI player 100 playing Eight of Diamonds
CardManager: Nakama game - sending card play to MatchManager - Player 100: Eight of Diamonds
CardManager: Added pending card play: 100_Eight of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 100: Eight of Diamonds
CardManager: 🃏 Player 100 hand BEFORE removal: 5 cards [Eight of Diamonds, Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 4 cards [Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Eight of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Eight of Diamonds (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: Eight of Diamonds
CardGameUI: Player 100 played Eight of Diamonds
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: Eight of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 8
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card Eight of Diamonds for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Eight of Diamonds by player 100
CardGameUI: Added trick card 0: Eight of Diamonds (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Eight of Diamonds (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: Eight of Diamonds
MatchManager: Card play synchronized - Player_100 played Eight of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 8
CardManager: OnNakamaCardPlayReceived called - Player 100: Eight of Diamonds
CardManager: Received card play from Nakama - Player 100: Eight of Diamonds
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: Eight of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: Eight of Diamonds
CardGameUI: Player 100 played Eight of Diamonds
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: Eight of Diamonds
MatchManager: CardPlayReceived signal emitted for player 100: Eight of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 8
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 8) - Previous: Turn 1, Tricks 7
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 8
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Eight of Diamonds by player 100
CardGameUI: Added trick card 0: Eight of Diamonds (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Eight of Diamonds (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Two of Hearts
MatchManager: Card play synchronized - Player_101 played Two of Hearts
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 8
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 8
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 5 valid cards
CardManager: AI player 101 playing Two of Hearts
CardManager: Nakama game - sending card play to MatchManager - Player 101: Two of Hearts
CardManager: Added pending card play: 101_Two of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 101: Two of Hearts
CardManager: 🃏 Player 101 hand BEFORE removal: 5 cards [Two of Hearts, Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 4 cards [Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Two of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Two of Hearts (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Two of Hearts
CardGameUI: Player 101 played Two of Hearts
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Two of Hearts
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 8
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 8
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Two of Hearts for AI player 101
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Eight of Diamonds by player 100
CardGameUI: Added trick card 0: Eight of Diamonds (P100) at position (-5, -20)
CardGameUI: Created trick card button for Two of Hearts by player 101
CardGameUI: Added trick card 1: Two of Hearts (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Eight of Diamonds (P100), Two of Hearts (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
CardManager: OnNakamaCardPlayReceived called - Player 101: Two of Hearts
CardManager: Received card play from Nakama - Player 101: Two of Hearts
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Two of Hearts
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Two of Hearts
CardGameUI: Player 101 played Two of Hearts
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Two of Hearts
MatchManager: CardPlayReceived signal emitted for player 101: Two of Hearts
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 8
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 8) - Previous: Turn 3, Tricks 8
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 8
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 8
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 8) - Last processed: Turn 0, Tricks 8
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 8
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Eight of Diamonds by player 100
CardGameUI: Added trick card 0: Eight of Diamonds (P100) at position (-5, -20)
CardGameUI: Created trick card button for Two of Hearts by player 101
CardGameUI: Added trick card 1: Two of Hearts (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Eight of Diamonds (P100), Two of Hearts (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Timer update received - 5.0s remaining
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 5.0s remaining
CardManager[PID:54510]: Client timer synced to: 5.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 0.0s remaining
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 2 cards: [P100:Eight of Diamonds, P101:Two of Hearts]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 2 cards: [P100:Eight of Diamonds, P101:Two of Hearts]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Seven of Spades
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 5 cards [Jack of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Seven of Spades
CardManager: Added pending card play: 0_Seven of Spades
CardManager: NAKAMA GAME - executing card immediately: Player 0: Seven of Spades
CardManager: 🃏 Player 0 hand BEFORE removal: 5 cards [Jack of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 4 cards [Jack of Spades, Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Seven of Spades
CardManager: NAKAMA - Added card to CurrentTrick immediately: Seven of Spades (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Seven of Spades
CardGameUI: Player 0 played Seven of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 5 cards)
CardGameUI: 🃏 UI cards before update: [Jack of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Seven of Spades'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 4 cards
CardGameUI: 🃏 UI currentPlayerCards had 5 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (5): [Jack of Spades, Ace of Spades, Ten of Hearts, Seven of Spades, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (4): [Jack of Spades, Ace of Spades, Ten of Hearts, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 5 -> 4 cards
CardGameUI: 🃏 Current cards in hand: [Jack of Spades, Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Seven of Spades
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 4 cards [Jack of Spades, Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Eight of Diamonds by player 100
CardGameUI: Added trick card 0: Eight of Diamonds (P100) at position (-60, -20)
CardGameUI: Created trick card button for Two of Hearts by player 101
CardGameUI: Added trick card 1: Two of Hearts (P101) at position (50, -20)
CardGameUI: Created trick card button for Seven of Spades by player 0
CardGameUI: Added trick card 2: Seven of Spades (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Eight of Diamonds (P100), Two of Hearts (P101), Seven of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
CardGameUI: 🃏 Hand update complete - 5 -> 4 cards (played: Seven of Spades)
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 31
MatchManager: Timer update received - 0.0s remaining
CardManager[PID:54510]: Client timer synced to: 0.0s
CardGameUI: Created card button for Jack of Spades with texture
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Ten of Hearts with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 4 manually positioned cards in single row
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Seven of Spades
MatchManager: Card play synchronized - Client played Seven of Spades
CardManager: OnNakamaCardPlayReceived called - Player 0: Seven of Spades
CardManager: Received card play from Nakama - Player 0: Seven of Spades
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Seven of Spades
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Seven of Spades
CardGameUI: Player 0 played Seven of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 8
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 8
CardGameUI: Turn started for player 2
CardManager: Synchronized card play completed - Player 0: Seven of Spades
MatchManager: CardPlayReceived signal emitted for player 0: Seven of Spades
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Eight of Diamonds by player 100
CardGameUI: Added trick card 0: Eight of Diamonds (P100) at position (-60, -20)
CardGameUI: Created trick card button for Two of Hearts by player 101
CardGameUI: Added trick card 1: Two of Hearts (P101) at position (50, -20)
CardGameUI: Created trick card button for Seven of Spades by player 0
CardGameUI: Added trick card 2: Seven of Spades (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Eight of Diamonds (P100), Two of Hearts (P101), Seven of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 8
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 8
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 8
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 8) - Previous: Turn 0, Tricks 8
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 8
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 8
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 1, Tricks: 8) - Last processed: Turn 1, Tricks 8
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 8
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 21
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 26
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 5.1s remaining
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
MatchManager: Timer update received - 5.1s remaining
CardManager[PID:54510]: Client timer synced to: 5.1s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 27
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 0.1s remaining
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 28
MatchManager: Timer update received - 0.1s remaining
CardManager[PID:54510]: Client timer synced to: 0.1s
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P100:Eight of Diamonds, P101:Two of Hearts, P0:Seven of Spades]
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Ten of Diamonds
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 5 cards [Ten of Diamonds, King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Ten of Diamonds
CardManager: Added pending card play: 2_Ten of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 2: Ten of Diamonds
CardManager: 🃏 Player 2 hand BEFORE removal: 5 cards [Ten of Diamonds, King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 4 cards [King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Ten of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: Ten of Diamonds (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Ten of Diamonds
CardGameUI: Player 2 played Ten of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 5 cards)
CardGameUI: 🃏 UI cards before update: [Ten of Diamonds, King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Ten of Diamonds'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 4 cards
CardGameUI: 🃏 UI currentPlayerCards had 5 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (5): [Ten of Diamonds, King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (4): [King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 5 -> 4 cards
CardGameUI: 🃏 Current cards in hand: [King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Ten of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 8
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 2 wins trick with Ten of Diamonds
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 2)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 2, Leader: 1, Score: 3
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 2 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Eight of Diamonds (P100), Two of Hearts (P101), Seven of Spades (P0), Ten of Diamonds (P2)]
CardGameUI: Next trick leader will be: 2
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 2
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 2, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: mXTgVBRUod, Next Leader: mXTgVBRUod
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 2, 10-second display period started
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 4 cards [King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Eight of Diamonds by player 100
CardGameUI: Added trick card 0: Eight of Diamonds (P100) at position (-115, -20)
CardGameUI: Created trick card button for Two of Hearts by player 101
CardGameUI: Added trick card 1: Two of Hearts (P101) at position (-5, -20)
CardGameUI: Created trick card button for Seven of Spades by player 0
CardGameUI: Added trick card 2: Seven of Spades (P0) at position (105, -20)
CardGameUI: Created trick card button for Ten of Diamonds by player 2
CardGameUI: Added trick card 3: Ten of Diamonds (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Eight of Diamonds (P100), Two of Hearts (P101), Seven of Spades (P0), Ten of Diamonds (P2)]
CardGameUI: Current trick leader: 1, Current turn: 1
CardGameUI: 🃏 Hand update complete - 5 -> 4 cards (played: Ten of Diamonds)
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Ace of Clubs with texture
CardGameUI: Created card button for Ten of Spades with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 4 manually positioned cards in single row
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Ten of Diamonds
MatchManager: Card play synchronized - mXTgVBRUod played Ten of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 8
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 2, Leader: 1, Score: 3
CardManager: OnNakamaCardPlayReceived called - Player 2: Ten of Diamonds
CardManager: Received card play from Nakama - Player 2: Ten of Diamonds
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Ten of Diamonds
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Ten of Diamonds
CardGameUI: Player 2 played Ten of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Ten of Diamonds
MatchManager: CardPlayReceived signal emitted for player 2: Ten of Diamonds
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 8
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 8) - Previous: Turn 1, Tricks 8
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 8
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 2, Leader: 1, Score: 3
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 2 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Eight of Diamonds (P100), Two of Hearts (P101), Seven of Spades (P0), Ten of Diamonds (P2)]
CardGameUI: Next trick leader will be: 2
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 2
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 2, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: mXTgVBRUod, Next Leader: mXTgVBRUod
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 1, Turn: 1
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 2, Leader: 1, Score: 3
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Eight of Diamonds by player 100
CardGameUI: Added trick card 0: Eight of Diamonds (P100) at position (-115, -20)
CardGameUI: Created trick card button for Two of Hearts by player 101
CardGameUI: Added trick card 1: Two of Hearts (P101) at position (-5, -20)
CardGameUI: Created trick card button for Seven of Spades by player 0
CardGameUI: Added trick card 2: Seven of Spades (P0) at position (105, -20)
CardGameUI: Created trick card button for Ten of Diamonds by player 2
CardGameUI: Added trick card 3: Ten of Diamonds (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Eight of Diamonds (P100), Two of Hearts (P101), Seven of Spades (P0), Ten of Diamonds (P2)]
CardGameUI: Current trick leader: 1, Current turn: 1
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 2
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 9, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 9
CardGameUI: Turn started for player 2
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 2
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 9, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 9
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 9
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 9) - Previous: Turn 2, Tricks 8
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 9
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 5.0s remaining
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Timer update received - 5.0s remaining
CardManager[PID:54510]: Client timer synced to: 5.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 0.0s remaining
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 0 cards: []
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Ten of Spades
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 4 cards [King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Ten of Spades
CardManager: Added pending card play: 2_Ten of Spades
CardManager: NAKAMA GAME - executing card immediately: Player 2: Ten of Spades
CardManager: 🃏 Player 2 hand BEFORE removal: 4 cards [King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 3 cards [King of Diamonds, Ace of Clubs, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Ten of Spades
CardManager: NAKAMA - Added card to CurrentTrick immediately: Ten of Spades (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Ten of Spades
CardGameUI: Player 2 played Ten of Spades
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 4 cards)
CardGameUI: 🃏 UI cards before update: [King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Ten of Spades'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 3 cards
CardGameUI: 🃏 UI currentPlayerCards had 4 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (4): [King of Diamonds, Ace of Clubs, Ten of Spades, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (3): [King of Diamonds, Ace of Clubs, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 4 -> 3 cards
CardGameUI: 🃏 Current cards in hand: [King of Diamonds, Ace of Clubs, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Ten of Spades
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 9
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 3 cards [King of Diamonds, Ace of Clubs, Queen of Spades]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Ten of Spades by player 2
CardGameUI: Added trick card 0: Ten of Spades (P2) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Ten of Spades (P2)]
CardGameUI: Current trick leader: 1, Current turn: 2
CardGameUI: 🃏 Hand update complete - 4 -> 3 cards (played: Ten of Spades)
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Ace of Clubs with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 3 manually positioned cards in single row
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 31
MatchManager: Timer update received - 0.0s remaining
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Ten of Spades
MatchManager: Card play synchronized - mXTgVBRUod played Ten of Spades
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 9
CardManager[PID:54510]: Client timer synced to: 0.0s
CardManager: OnNakamaCardPlayReceived called - Player 2: Ten of Spades
CardManager: Received card play from Nakama - Player 2: Ten of Spades
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Ten of Spades
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Ten of Spades
CardGameUI: Player 2 played Ten of Spades
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Ten of Spades
MatchManager: CardPlayReceived signal emitted for player 2: Ten of Spades
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 9
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 9) - Previous: Turn 1, Tricks 9
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 9
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Ten of Spades by player 2
CardGameUI: Added trick card 0: Ten of Spades (P2) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Ten of Spades (P2)]
CardGameUI: Current trick leader: 1, Current turn: 2
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 1 valid cards
CardManager: AI player 100 playing King of Spades
CardManager: Nakama game - sending card play to MatchManager - Player 100: King of Spades
CardManager: Added pending card play: 100_King of Spades
CardManager: NAKAMA GAME - executing card immediately: Player 100: King of Spades
CardManager: 🃏 Player 100 hand BEFORE removal: 4 cards [Queen of Clubs, Four of Hearts, King of Spades, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 3 cards [Queen of Clubs, Four of Hearts, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: King of Spades
CardManager: NAKAMA - Added card to CurrentTrick immediately: King of Spades (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: King of Spades
CardGameUI: Player 100 played King of Spades
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: King of Spades
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 9
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card King of Spades for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Ten of Spades by player 2
CardGameUI: Added trick card 0: Ten of Spades (P2) at position (-5, -20)
CardGameUI: Created trick card button for King of Spades by player 100
CardGameUI: Added trick card 1: King of Spades (P100) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Ten of Spades (P2), King of Spades (P100)]
CardGameUI: Current trick leader: 1, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: King of Spades
MatchManager: Card play synchronized - Player_100 played King of Spades
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 9
CardManager: OnNakamaCardPlayReceived called - Player 100: King of Spades
CardManager: Received card play from Nakama - Player 100: King of Spades
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: King of Spades
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: King of Spades
CardGameUI: Player 100 played King of Spades
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: King of Spades
MatchManager: CardPlayReceived signal emitted for player 100: King of Spades
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 9
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 9) - Previous: Turn 2, Tricks 9
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 9
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Ten of Spades by player 2
CardGameUI: Added trick card 0: Ten of Spades (P2) at position (-5, -20)
CardGameUI: Created trick card button for King of Spades by player 100
CardGameUI: Added trick card 1: King of Spades (P100) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Ten of Spades (P2), King of Spades (P100)]
CardGameUI: Current trick leader: 1, Current turn: 3
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 4 valid cards
CardManager: AI player 101 playing Nine of Hearts
CardManager: Nakama game - sending card play to MatchManager - Player 101: Nine of Hearts
CardManager: Added pending card play: 101_Nine of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 101: Nine of Hearts
CardManager: 🃏 Player 101 hand BEFORE removal: 4 cards [Nine of Hearts, Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 3 cards [Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Nine of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Nine of Hearts (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Nine of Hearts
CardGameUI: Player 101 played Nine of Hearts
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Nine of Hearts
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 9
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 9
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Nine of Hearts for AI player 101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Ten of Spades by player 2
CardGameUI: Added trick card 0: Ten of Spades (P2) at position (-60, -20)
CardGameUI: Created trick card button for King of Spades by player 100
CardGameUI: Added trick card 1: King of Spades (P100) at position (50, -20)
CardGameUI: Created trick card button for Nine of Hearts by player 101
CardGameUI: Added trick card 2: Nine of Hearts (P101) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Ten of Spades (P2), King of Spades (P100), Nine of Hearts (P101)]
CardGameUI: Current trick leader: 1, Current turn: 0
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Nine of Hearts
MatchManager: Card play synchronized - Player_101 played Nine of Hearts
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 9
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 9
CardManager: OnNakamaCardPlayReceived called - Player 101: Nine of Hearts
CardManager: Received card play from Nakama - Player 101: Nine of Hearts
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Nine of Hearts
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Nine of Hearts
CardGameUI: Player 101 played Nine of Hearts
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Nine of Hearts
MatchManager: CardPlayReceived signal emitted for player 101: Nine of Hearts
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 9
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 9) - Previous: Turn 3, Tricks 9
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 9
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 9
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 9) - Last processed: Turn 0, Tricks 9
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 9
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Ten of Spades by player 2
CardGameUI: Added trick card 0: Ten of Spades (P2) at position (-60, -20)
CardGameUI: Created trick card button for King of Spades by player 100
CardGameUI: Added trick card 1: King of Spades (P100) at position (50, -20)
CardGameUI: Created trick card button for Nine of Hearts by player 101
CardGameUI: Added trick card 2: Nine of Hearts (P101) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Ten of Spades (P2), King of Spades (P100), Nine of Hearts (P101)]
CardGameUI: Current trick leader: 1, Current turn: 0
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P2:Ten of Spades, P100:King of Spades, P101:Nine of Hearts]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 3 cards: [P2:Ten of Spades, P100:King of Spades, P101:Nine of Hearts]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Jack of Spades
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 4 cards [Jack of Spades, Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Jack of Spades
CardManager: Added pending card play: 0_Jack of Spades
CardManager: NAKAMA GAME - executing card immediately: Player 0: Jack of Spades
CardManager: 🃏 Player 0 hand BEFORE removal: 4 cards [Jack of Spades, Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 3 cards [Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Jack of Spades
CardManager: NAKAMA - Added card to CurrentTrick immediately: Jack of Spades (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Jack of Spades
CardGameUI: Player 0 played Jack of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 4 cards)
CardGameUI: 🃏 UI cards before update: [Jack of Spades, Ace of Spades, Ten of Hearts, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Jack of Spades'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 3 cards
CardGameUI: 🃏 UI currentPlayerCards had 4 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (4): [Jack of Spades, Ace of Spades, Ten of Hearts, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (3): [Ace of Spades, Ten of Hearts, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 4 -> 3 cards
CardGameUI: 🃏 Current cards in hand: [Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Jack of Spades
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 100 wins trick with King of Spades
CardManager[PID:54510]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 100)
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Ten of Spades (P2), King of Spades (P100), Nine of Hearts (P101), Jack of Spades (P0)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 100
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 100, 10-second display period started
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 3 cards [Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Ten of Spades by player 2
CardGameUI: Added trick card 0: Ten of Spades (P2) at position (-115, -20)
CardGameUI: Created trick card button for King of Spades by player 100
CardGameUI: Added trick card 1: King of Spades (P100) at position (-5, -20)
CardGameUI: Created trick card button for Nine of Hearts by player 101
CardGameUI: Added trick card 2: Nine of Hearts (P101) at position (105, -20)
CardGameUI: Created trick card button for Jack of Spades by player 0
CardGameUI: Added trick card 3: Jack of Spades (P0) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Ten of Spades (P2), King of Spades (P100), Nine of Hearts (P101), Jack of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 2
CardGameUI: 🃏 Hand update complete - 4 -> 3 cards (played: Jack of Spades)
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Ten of Hearts with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 3 manually positioned cards in single row
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Jack of Spades
MatchManager: Card play synchronized - Client played Jack of Spades
CardManager: OnNakamaCardPlayReceived called - Player 0: Jack of Spades
CardManager: Received card play from Nakama - Player 0: Jack of Spades
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Jack of Spades
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Jack of Spades
CardGameUI: Player 0 played Jack of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 9
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 100 wins trick with King of Spades
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 100)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 100, Leader: 2, Score: 7
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Ten of Spades (P2), King of Spades (P100), Nine of Hearts (P101), Jack of Spades (P0)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 100
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 100, 10-second display period started
CardManager: Synchronized card play completed - Player 0: Jack of Spades
MatchManager: CardPlayReceived signal emitted for player 0: Jack of Spades
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Ten of Spades by player 2
CardGameUI: Added trick card 0: Ten of Spades (P2) at position (-115, -20)
CardGameUI: Created trick card button for King of Spades by player 100
CardGameUI: Added trick card 1: King of Spades (P100) at position (-5, -20)
CardGameUI: Created trick card button for Nine of Hearts by player 101
CardGameUI: Added trick card 2: Nine of Hearts (P101) at position (105, -20)
CardGameUI: Created trick card button for Jack of Spades by player 0
CardGameUI: Added trick card 3: Jack of Spades (P0) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Ten of Spades (P2), King of Spades (P100), Nine of Hearts (P101), Jack of Spades (P0)]
CardGameUI: Current trick leader: 2, Current turn: 2
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 9
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 100, Leader: 2, Score: 7
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 9
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 9) - Previous: Turn 0, Tricks 9
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: 🛑 Stopping active timer before turn change (had 9.9s remaining)
CardManager[PID:54510]: 🔄 FORCE RESET - Client stuck in EndOfRound, resetting to PlayerTurn for new turn
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 9
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 100, Leader: 2, Score: 7
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 100 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Ten of Spades (P2), King of Spades (P100), Nine of Hearts (P101), Jack of Spades (P0)]
CardGameUI: Next trick leader will be: 100
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 100
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 100, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: AI_Player_100, Next Leader: AI_Player_100
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 2, Turn: 2
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 100, Leader: 2, Score: 7
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 100
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 10, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 100
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 10, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 100
CardGameUI: Turn started for player 100
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 3 valid cards
CardManager: AI player 100 playing Queen of Clubs
CardManager: Nakama game - sending card play to MatchManager - Player 100: Queen of Clubs
CardManager: Added pending card play: 100_Queen of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 100: Queen of Clubs
CardManager: 🃏 Player 100 hand BEFORE removal: 3 cards [Queen of Clubs, Four of Hearts, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 2 cards [Four of Hearts, Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Queen of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Queen of Clubs (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: Queen of Clubs
CardGameUI: Player 100 played Queen of Clubs
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: Queen of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 10
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card Queen of Clubs for AI player 100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Clubs by player 100
CardGameUI: Added trick card 0: Queen of Clubs (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Queen of Clubs (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: Queen of Clubs
MatchManager: Card play synchronized - Player_100 played Queen of Clubs
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 10
CardManager: OnNakamaCardPlayReceived called - Player 100: Queen of Clubs
CardManager: Received card play from Nakama - Player 100: Queen of Clubs
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: Queen of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: Queen of Clubs
CardGameUI: Player 100 played Queen of Clubs
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: Queen of Clubs
MatchManager: CardPlayReceived signal emitted for player 100: Queen of Clubs
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 10
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 10) - Previous: Turn 1, Tricks 9
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 10
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Clubs by player 100
CardGameUI: Added trick card 0: Queen of Clubs (P100) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Queen of Clubs (P100)]
CardGameUI: Current trick leader: 2, Current turn: 3
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 3 valid cards
CardManager: AI player 101 playing Seven of Clubs
CardManager: Nakama game - sending card play to MatchManager - Player 101: Seven of Clubs
CardManager: Added pending card play: 101_Seven of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 101: Seven of Clubs
CardManager: 🃏 Player 101 hand BEFORE removal: 3 cards [Seven of Clubs, Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 2 cards [Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Seven of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Seven of Clubs (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Seven of Clubs
CardGameUI: Player 101 played Seven of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Seven of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 10
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 10
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Seven of Clubs for AI player 101
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Clubs by player 100
CardGameUI: Added trick card 0: Queen of Clubs (P100) at position (-5, -20)
CardGameUI: Created trick card button for Seven of Clubs by player 101
CardGameUI: Added trick card 1: Seven of Clubs (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Queen of Clubs (P100), Seven of Clubs (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Seven of Clubs
MatchManager: Card play synchronized - Player_101 played Seven of Clubs
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 10
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 10
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager: OnNakamaCardPlayReceived called - Player 101: Seven of Clubs
CardManager: Received card play from Nakama - Player 101: Seven of Clubs
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Seven of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Seven of Clubs
CardGameUI: Player 101 played Seven of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Seven of Clubs
MatchManager: CardPlayReceived signal emitted for player 101: Seven of Clubs
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 10
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 10) - Previous: Turn 3, Tricks 10
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 10
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 10
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 10) - Last processed: Turn 0, Tricks 10
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 10
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Client timer synced to: 10.0s
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Clubs by player 100
CardGameUI: Added trick card 0: Queen of Clubs (P100) at position (-5, -20)
CardGameUI: Created trick card button for Seven of Clubs by player 101
CardGameUI: Added trick card 1: Seven of Clubs (P101) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Queen of Clubs (P100), Seven of Clubs (P101)]
CardGameUI: Current trick leader: 2, Current turn: 0
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 5.0s remaining
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Timer update received - 5.0s remaining
CardManager[PID:54510]: Client timer synced to: 5.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 2 cards: [P100:Queen of Clubs, P101:Seven of Clubs]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 2 cards: [P100:Queen of Clubs, P101:Seven of Clubs]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Ten of Hearts
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 3 cards [Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Ten of Hearts
CardManager: Added pending card play: 0_Ten of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 0: Ten of Hearts
CardManager: 🃏 Player 0 hand BEFORE removal: 3 cards [Ace of Spades, Ten of Hearts, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 2 cards [Ace of Spades, Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Ten of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Ten of Hearts (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Ten of Hearts
CardGameUI: Player 0 played Ten of Hearts
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 3 cards)
CardGameUI: 🃏 UI cards before update: [Ace of Spades, Ten of Hearts, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Ten of Hearts'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 2 cards
CardGameUI: 🃏 UI currentPlayerCards had 3 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (3): [Ace of Spades, Ten of Hearts, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (2): [Ace of Spades, Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 3 -> 2 cards
CardGameUI: 🃏 Current cards in hand: [Ace of Spades, Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Ten of Hearts
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 2 cards [Ace of Spades, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Clubs by player 100
CardGameUI: Added trick card 0: Queen of Clubs (P100) at position (-60, -20)
CardGameUI: Created trick card button for Seven of Clubs by player 101
CardGameUI: Added trick card 1: Seven of Clubs (P101) at position (50, -20)
CardGameUI: Created trick card button for Ten of Hearts by player 0
CardGameUI: Added trick card 2: Ten of Hearts (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Queen of Clubs (P100), Seven of Clubs (P101), Ten of Hearts (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
CardGameUI: 🃏 Hand update complete - 3 -> 2 cards (played: Ten of Hearts)
CardGameUI: Created card button for Ace of Spades with texture
CardGameUI: Created card button for Jack of Hearts with texture
CardGameUI: Created 2 manually positioned cards in single row
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Ten of Hearts
MatchManager: Card play synchronized - Client played Ten of Hearts
CardManager: OnNakamaCardPlayReceived called - Player 0: Ten of Hearts
CardManager: Received card play from Nakama - Player 0: Ten of Hearts
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Ten of Hearts
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Ten of Hearts
CardGameUI: Player 0 played Ten of Hearts
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 10
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 10
CardGameUI: Turn started for player 2
CardManager: Synchronized card play completed - Player 0: Ten of Hearts
MatchManager: CardPlayReceived signal emitted for player 0: Ten of Hearts
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Clubs by player 100
CardGameUI: Added trick card 0: Queen of Clubs (P100) at position (-60, -20)
CardGameUI: Created trick card button for Seven of Clubs by player 101
CardGameUI: Added trick card 1: Seven of Clubs (P101) at position (50, -20)
CardGameUI: Created trick card button for Ten of Hearts by player 0
CardGameUI: Added trick card 2: Ten of Hearts (P0) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Queen of Clubs (P100), Seven of Clubs (P101), Ten of Hearts (P0)]
CardGameUI: Current trick leader: 2, Current turn: 1
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 10
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 10
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 10
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 10) - Previous: Turn 0, Tricks 10
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 10
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 10
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 1, Tricks: 10) - Last processed: Turn 1, Tricks 10
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 10
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P100:Queen of Clubs, P101:Seven of Clubs, P0:Ten of Hearts]
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Ace of Clubs
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 3 cards [King of Diamonds, Ace of Clubs, Queen of Spades]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Ace of Clubs
CardManager: Added pending card play: 2_Ace of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 2: Ace of Clubs
CardManager: 🃏 Player 2 hand BEFORE removal: 3 cards [King of Diamonds, Ace of Clubs, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 2 cards [King of Diamonds, Queen of Spades]
CardManager: 🃏 Card removal success: True, Card was: Ace of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Ace of Clubs (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Ace of Clubs
CardGameUI: Player 2 played Ace of Clubs
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 3 cards)
CardGameUI: 🃏 UI cards before update: [King of Diamonds, Ace of Clubs, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Ace of Clubs'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 2 cards
CardGameUI: 🃏 UI currentPlayerCards had 3 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (3): [King of Diamonds, Ace of Clubs, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (2): [King of Diamonds, Queen of Spades]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 3 -> 2 cards
CardGameUI: 🃏 Current cards in hand: [King of Diamonds, Queen of Spades]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Ace of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 10
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 2 wins trick with Ace of Clubs
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 2)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 2, Leader: 1, Score: 4
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 2 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Queen of Clubs (P100), Seven of Clubs (P101), Ten of Hearts (P0), Ace of Clubs (P2)]
CardGameUI: Next trick leader will be: 2
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 2
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 2, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: mXTgVBRUod, Next Leader: mXTgVBRUod
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 2, 10-second display period started
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 2 cards [King of Diamonds, Queen of Spades]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Clubs by player 100
CardGameUI: Added trick card 0: Queen of Clubs (P100) at position (-115, -20)
CardGameUI: Created trick card button for Seven of Clubs by player 101
CardGameUI: Added trick card 1: Seven of Clubs (P101) at position (-5, -20)
CardGameUI: Created trick card button for Ten of Hearts by player 0
CardGameUI: Added trick card 2: Ten of Hearts (P0) at position (105, -20)
CardGameUI: Created trick card button for Ace of Clubs by player 2
CardGameUI: Added trick card 3: Ace of Clubs (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Queen of Clubs (P100), Seven of Clubs (P101), Ten of Hearts (P0), Ace of Clubs (P2)]
CardGameUI: Current trick leader: 1, Current turn: 1
CardGameUI: 🃏 Hand update complete - 3 -> 2 cards (played: Ace of Clubs)
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created card button for Queen of Spades with texture
CardGameUI: Created 2 manually positioned cards in single row
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Ace of Clubs
MatchManager: Card play synchronized - mXTgVBRUod played Ace of Clubs
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 10
MatchManager[PID:54510]: Received message TrickCompleted from mXTgVBRUod
MatchManager: Trick completed - Winner: 2, Leader: 1, Score: 4
CardManager: OnNakamaCardPlayReceived called - Player 2: Ace of Clubs
CardManager: Received card play from Nakama - Player 2: Ace of Clubs
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Ace of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Ace of Clubs
CardGameUI: Player 2 played Ace of Clubs
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Ace of Clubs
MatchManager: CardPlayReceived signal emitted for player 2: Ace of Clubs
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 10
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 10) - Previous: Turn 1, Tricks 10
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 10
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardManager[PID:54510]: OnNakamaTrickCompletedReceived called - Winner: 2, Leader: 1, Score: 4
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing trick completion from match owner
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 2 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Queen of Clubs (P100), Seven of Clubs (P101), Ten of Hearts (P0), Ace of Clubs (P2)]
CardGameUI: Next trick leader will be: 2
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 2
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 2, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: mXTgVBRUod, Next Leader: mXTgVBRUod
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager[PID:54510]: NAKAMA CLIENT - trick completion synchronized and entered end-of-round state - Leader: 1, Turn: 1
MatchManager: TrickCompletedReceived signal emitted safely from main thread - Winner: 2, Leader: 1, Score: 4
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Clubs by player 100
CardGameUI: Added trick card 0: Queen of Clubs (P100) at position (-115, -20)
CardGameUI: Created trick card button for Seven of Clubs by player 101
CardGameUI: Added trick card 1: Seven of Clubs (P101) at position (-5, -20)
CardGameUI: Created trick card button for Ten of Hearts by player 0
CardGameUI: Added trick card 2: Ten of Hearts (P0) at position (105, -20)
CardGameUI: Created trick card button for Ace of Clubs by player 2
CardGameUI: Added trick card 3: Ace of Clubs (P2) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Queen of Clubs (P100), Seven of Clubs (P101), Ten of Hearts (P0), Ace of Clubs (P2)]
CardGameUI: Current trick leader: 1, Current turn: 1
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 2
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 11, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 11
CardGameUI: Turn started for player 2
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 2
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 11, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 72cbb72a-f851-4dfd-a409-3103e33e93a4, PlayerTurn: 1, Tricks: 11
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
MatchManager: Timer update received - 10.0s remaining
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 1, Tricks: 11
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 1, Tricks: 11) - Previous: Turn 2, Tricks 10
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 1 (Player 2)
CardGameUI: Turn started for player 2
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 2
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 1, Tricks: 11
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 0 cards: []
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card Queen of Spades
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 2 cards [King of Diamonds, Queen of Spades]
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Spades
CardManager: Added pending card play: 2_Queen of Spades
CardManager: NAKAMA GAME - executing card immediately: Player 2: Queen of Spades
CardManager: 🃏 Player 2 hand BEFORE removal: 2 cards [King of Diamonds, Queen of Spades]
CardManager: 🃏 Player 2 hand AFTER removal: 1 cards [King of Diamonds]
CardManager: 🃏 Card removal success: True, Card was: Queen of Spades
CardManager: NAKAMA - Added card to CurrentTrick immediately: Queen of Spades (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: Queen of Spades
CardGameUI: Player 2 played Queen of Spades
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 2 cards)
CardGameUI: 🃏 UI cards before update: [King of Diamonds, Queen of Spades]
CardGameUI: 🃏 Played card was: 'Queen of Spades'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 1 cards
CardGameUI: 🃏 UI currentPlayerCards had 2 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (2): [King of Diamonds, Queen of Spades]
CardGameUI: 🃏 New CardManager cards (1): [King of Diamonds]
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 2 -> 1 cards
CardGameUI: 🃏 Current cards in hand: [King of Diamonds]
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: Queen of Spades
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 11
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 1 cards [King of Diamonds]
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Spades by player 2
CardGameUI: Added trick card 0: Queen of Spades (P2) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Queen of Spades (P2)]
CardGameUI: Current trick leader: 1, Current turn: 2
CardGameUI: 🃏 Hand update complete - 2 -> 1 cards (played: Queen of Spades)
CardGameUI: Created card button for King of Diamonds with texture
CardGameUI: Created 1 manually positioned cards in single row
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: Queen of Spades
MatchManager: Card play synchronized - mXTgVBRUod played Queen of Spades
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 11
CardManager: OnNakamaCardPlayReceived called - Player 2: Queen of Spades
CardManager: Received card play from Nakama - Player 2: Queen of Spades
CardManager[PID:54510]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54510]: OnNakama filtering - Player 2, LocalUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, SenderUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, willSkip: False
CardManager: Executing synchronized card play - Player 2: Queen of Spades
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 2: Queen of Spades
CardGameUI: Player 2 played Queen of Spades
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 2: Queen of Spades
MatchManager: CardPlayReceived signal emitted for player 2: Queen of Spades
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 2, Tricks: 11
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 2, Tricks: 11) - Previous: Turn 1, Tricks 11
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 1, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 1 -> 2 (Player 100)
CardGameUI: Turn started for player 100
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 100
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 2, Tricks: 11
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_100
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Spades by player 2
CardGameUI: Added trick card 0: Queen of Spades (P2) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Queen of Spades (P2)]
CardGameUI: Current trick leader: 1, Current turn: 2
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 2 valid cards
CardManager: AI player 100 playing Four of Hearts
CardManager: Nakama game - sending card play to MatchManager - Player 100: Four of Hearts
CardManager: Added pending card play: 100_Four of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 100: Four of Hearts
CardManager: 🃏 Player 100 hand BEFORE removal: 2 cards [Four of Hearts, Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 1 cards [Eight of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Four of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Four of Hearts (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: Four of Hearts
CardGameUI: Player 100 played Four of Hearts
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: Four of Hearts
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 11
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card Four of Hearts for AI player 100
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 2 valid cards
CardManager: AI player 101 playing Jack of Clubs
CardManager: Nakama game - sending card play to MatchManager - Player 101: Jack of Clubs
CardManager: Added pending card play: 101_Jack of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 101: Jack of Clubs
CardManager: 🃏 Player 101 hand BEFORE removal: 2 cards [Jack of Clubs, Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 1 cards [Ten of Clubs]
CardManager: 🃏 Card removal success: True, Card was: Jack of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Jack of Clubs (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Jack of Clubs
CardGameUI: Player 101 played Jack of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Jack of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 11
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 11
CardGameUI: Turn started for player 0
CardManager: Successfully auto-played card Jack of Clubs for AI player 101
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 101: Jack of Clubs
MatchManager: Card play synchronized - Player_101 played Jack of Clubs
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 11
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to 5af6f516-d9a4-424b-8447-0968cb480d1c, PlayerTurn: 0, Tricks: 11
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Spades by player 2
CardGameUI: Added trick card 0: Queen of Spades (P2) at position (-60, -20)
CardGameUI: Created trick card button for Four of Hearts by player 100
CardGameUI: Added trick card 1: Four of Hearts (P100) at position (50, -20)
CardGameUI: Created trick card button for Jack of Clubs by player 101
CardGameUI: Added trick card 2: Jack of Clubs (P101) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Queen of Spades (P2), Four of Hearts (P100), Jack of Clubs (P101)]
CardGameUI: Current trick leader: 1, Current turn: 0
CardManager: OnNakamaCardPlayReceived called - Player 101: Jack of Clubs
CardManager: Received card play from Nakama - Player 101: Jack of Clubs
CardManager[PID:54510]: OnNakama filtering - Player 101 is AI, not skipping
CardManager: Executing synchronized card play - Player 101: Jack of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 101: Jack of Clubs
CardGameUI: Player 101 played Jack of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 101: Jack of Clubs
MatchManager: CardPlayReceived signal emitted for player 101: Jack of Clubs
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 11
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 0, Tricks: 11) - Previous: Turn 3, Tricks 11
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 3, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 3 -> 0 (Player 0)
CardManager[PID:54510]: NAKAMA CLIENT - this is our turn, calling StartTurn() to manage own timer
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 11
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 0, Tricks: 11
CardManager[PID:54510]: ⚠️ DUPLICATE turn change detected - ignoring (Turn: 0, Tricks: 11) - Last processed: Turn 0, Tricks 11
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 0, Tricks: 11
MatchManager: TurnChanged signal emitted - CurrentPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 20
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Spades by player 2
CardGameUI: Added trick card 0: Queen of Spades (P2) at position (-60, -20)
MatchManager: Timer update received - 10.0s remaining
CardGameUI: Created trick card button for Four of Hearts by player 100
CardGameUI: Added trick card 1: Four of Hearts (P100) at position (50, -20)
CardGameUI: Created trick card button for Jack of Clubs by player 101
CardGameUI: Added trick card 2: Jack of Clubs (P101) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Queen of Spades (P2), Four of Hearts (P100), Jack of Clubs (P101)]
CardGameUI: Current trick leader: 1, Current turn: 0
CardManager[PID:54510]: Client timer synced to: 10.0s
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 3 cards: [P2:Queen of Spades, P100:Four of Hearts, P101:Jack of Clubs]
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Ace of Spades
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 2 cards [Ace of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Ace of Spades
CardManager: Added pending card play: 0_Ace of Spades
CardManager: NAKAMA GAME - executing card immediately: Player 0: Ace of Spades
CardManager: 🃏 Player 0 hand BEFORE removal: 2 cards [Ace of Spades, Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 1 cards [Jack of Hearts]
CardManager: 🃏 Card removal success: True, Card was: Ace of Spades
CardManager: NAKAMA - Added card to CurrentTrick immediately: Ace of Spades (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Ace of Spades
CardGameUI: Player 0 played Ace of Spades
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 2 cards)
CardGameUI: 🃏 UI cards before update: [Ace of Spades, Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Ace of Spades'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 1 cards
CardGameUI: 🃏 UI currentPlayerCards had 2 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (2): [Ace of Spades, Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (1): [Jack of Hearts]
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 2 -> 1 cards
CardGameUI: 🃏 Current cards in hand: [Jack of Hearts]
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Ace of Spades
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 0 wins trick with Ace of Spades
CardManager[PID:54510]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 0)
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 0 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Queen of Spades (P2), Four of Hearts (P100), Jack of Clubs (P101), Ace of Spades (P0)]
CardGameUI: Next trick leader will be: 0
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54510]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Starting end-of-round turn - winner: 0
CardManager[PID:54510]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 0, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: Client, Next Leader: Client
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54510]: Timer management decision: True (Nakama client)
CardManager[PID:54510]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54510]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54510]: NAKAMA CLIENT - end-of-round state started
CardManager[PID:54510]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 0, 10-second display period started
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 1 cards [Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 3, Actual: 4 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 3 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Queen of Spades by player 2
CardGameUI: Added trick card 0: Queen of Spades (P2) at position (-115, -20)
CardGameUI: Created trick card button for Four of Hearts by player 100
CardGameUI: Added trick card 1: Four of Hearts (P100) at position (-5, -20)
CardGameUI: Created trick card button for Jack of Clubs by player 101
CardGameUI: Added trick card 2: Jack of Clubs (P101) at position (105, -20)
CardGameUI: Created trick card button for Ace of Spades by player 0
CardGameUI: Added trick card 3: Ace of Spades (P0) at position (215, -20)
CardGameUI: Updated trick display with 4 cards: [Queen of Spades (P2), Four of Hearts (P100), Jack of Clubs (P101), Ace of Spades (P0)]
CardGameUI: Current trick leader: 0, Current turn: 0
CardGameUI: 🃏 Hand update complete - 2 -> 1 cards (played: Ace of Spades)
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 3 cards: [P2:Queen of Spades, P100:Four of Hearts, P101:Jack of Clubs]
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 0
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 12, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - managing own player's turn timer (Player 0)
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardGameUI: Turn started for player 0
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 0
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 12, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Starting next trick after end-of-round cleanup - calling StartTurn()
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 12
CardGameUI: Turn started for player 0
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 19
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 0 cards: []
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 0 cards: []
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Jack of Hearts
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 1 cards [Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Jack of Hearts
CardManager: Added pending card play: 0_Jack of Hearts
CardManager: NAKAMA GAME - executing card immediately: Player 0: Jack of Hearts
CardManager: 🃏 Player 0 hand BEFORE removal: 1 cards [Jack of Hearts]
CardManager: 🃏 Player 0 hand AFTER removal: 0 cards []
CardManager: 🃏 Card removal success: True, Card was: Jack of Hearts
CardManager: NAKAMA - Added card to CurrentTrick immediately: Jack of Hearts (Trick now has 1 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 0: Jack of Hearts
CardGameUI: Player 0 played Jack of Hearts
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 0, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 1 cards)
CardGameUI: 🃏 UI cards before update: [Jack of Hearts]
CardGameUI: 🃏 Played card was: 'Jack of Hearts'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 0 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 0 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(0) returned 0 cards
CardGameUI: 🃏 UI currentPlayerCards had 1 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (1): [Jack of Hearts]
CardGameUI: 🃏 New CardManager cards (0): []
CardGameUI: 🃏 UpdatePlayerHand - Player 0: 1 -> 0 cards
CardGameUI: No cards found - game may not have started yet or cards not dealt
CardManager[PID:54510]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54510]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54510]: Local user ID: 5af6f516-d9a4-424b-8447-0968cb480d1c
MatchManager[PID:54510]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 0: Jack of Hearts
CardManager: NAKAMA CLIENT - executed locally, no turn progression (wait for host)
CardManager: NAKAMA CLIENT - ending own turn locally to stop timer
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 0 cards []
CardManager[PID:54510]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54510]: Auto-forfeit successful for player 0
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Jack of Hearts by player 0
CardGameUI: Added trick card 0: Jack of Hearts (P0) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Jack of Hearts (P0)]
CardGameUI: Current trick leader: 0, Current turn: 1
CardGameUI: 🃏 Hand update complete - 1 -> 0 cards (played: Jack of Hearts)
MatchManager[PID:54511]: Received message CardPlayed from IDqHdkrrEK
MatchManager: Card play received - Player 0: Jack of Hearts
MatchManager: Card play synchronized - Client played Jack of Hearts
CardManager: OnNakamaCardPlayReceived called - Player 0: Jack of Hearts
CardManager: Received card play from Nakama - Player 0: Jack of Hearts
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: OnNakama filtering - Player 0, LocalUserId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, SenderUserId: 5af6f516-d9a4-424b-8447-0968cb480d1c, willSkip: False
CardManager: Executing synchronized card play - Player 0: Jack of Hearts
CardManager: DEBUG - Host processing card play for Player 0
CardManager: DEBUG - isOwnHumanCard: False (LocalPlayer.PlayerId: 2, playerId: 0)
CardManager: DEBUG - isOwnAICard: False (playerId: 0 >= 100)
CardManager: DEBUG - isOwnCardPlay: False (isOwnHumanCard: False, isOwnAICard: False)
CardManager: Executing card play from client - Player 0: Jack of Hearts
CardGameUI: Player 0 played Jack of Hearts
CardGameUI: OnCardPlayed - playerId: 0, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA MATCH OWNER - progressing turn after client card play: Player 0
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 0
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 12
CardManager: HOST trick continues - 1/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 2
CardManager: Human player turn for mXTgVBRUod (ID: 2)
CardManager: Starting turn timer for player 2
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 2, TurnIndex: 1, NextPlayerId: 72cbb72a-f851-4dfd-a409-3103e33e93a4, TricksPlayed: 12
CardGameUI: Turn started for player 2
CardManager: Synchronized card play completed - Player 0: Jack of Hearts
MatchManager: CardPlayReceived signal emitted for player 0: Jack of Hearts
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager: Timer sync - 10.0s remaining
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 0, Actual: 1 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Jack of Hearts by player 0
CardGameUI: Added trick card 0: Jack of Hearts (P0) at position (50, -20)
CardGameUI: Updated trick display with 1 cards: [Jack of Hearts (P0)]
CardGameUI: Current trick leader: 0, Current turn: 1
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
CardManager[PID:54510]: Client timer synced to: 1.0s
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 2: King of Diamonds
MatchManager: Card play synchronized - mXTgVBRUod played King of Diamonds
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_100, PlayerTurn: 2, Tricks: 12
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 2, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 1 cards: [P0:Jack of Hearts]
CardManager[PID:54511]: Turn timer expired for player 2 - executing auto-forfeit
CardManager[PID:54511]: Player 2 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 2 to Nakama user 72cbb72a-f851-4dfd-a409-3103e33e93a4
CardManager[PID:54511]: Auto-forfeit check for player 2: True (local player's own instance)
CardManager[PID:54511]: Player 2 at table - auto-forfeiting with lowest card
CardManager[PID:54511]: Auto-forfeiting player 2 with card King of Diamonds
CardManager[PID:54511]: 🃏 Player 2 hand BEFORE auto-forfeit: 1 cards [King of Diamonds]
CardManager: Nakama game - sending card play to MatchManager - Player 2: King of Diamonds
CardManager: Added pending card play: 2_King of Diamonds
CardManager: NAKAMA GAME - executing card immediately: Player 2: King of Diamonds
CardManager: 🃏 Player 2 hand BEFORE removal: 1 cards [King of Diamonds]
CardManager: 🃏 Player 2 hand AFTER removal: 0 cards []
CardManager: 🃏 Card removal success: True, Card was: King of Diamonds
CardManager: NAKAMA - Added card to CurrentTrick immediately: King of Diamonds (Trick now has 2 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 2: King of Diamonds
CardGameUI: Player 2 played King of Diamonds
CardGameUI: OnCardPlayed - playerId: 2, actualLocalPlayerId: 2, isLocalPlayer: True
CardGameUI: 🃏 LOCAL PLAYER CARD PLAYED - updating hand display (was 1 cards)
CardGameUI: 🃏 UI cards before update: [King of Diamonds]
CardGameUI: 🃏 Played card was: 'King of Diamonds'
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 0 cards
CardGameUI: 🃏 UI currentPlayerCards had 1 cards before update
CardGameUI: 🃏 CARD SYNC MISMATCH DETECTED!
CardGameUI: 🃏 Old UI cards (1): [King of Diamonds]
CardGameUI: 🃏 New CardManager cards (0): []
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 1 -> 0 cards
CardGameUI: No cards found - game may not have started yet or cards not dealt
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 2: King of Diamonds
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (HUMAN player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 2
CardManager: HOST EndTurn called for player 2
CardGameUI: Turn ended for player 2
CardManager: HOST moving to next player - new CurrentPlayerTurn: 2 (Player 100)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 100, TurnIndex: 2, NextPlayerId: AI_Player_100, TricksPlayed: 12
CardManager: HOST trick continues - 2/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 2, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 100
CardManager: AI turn detected for AI_Player_100 (ID: 100)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 100
CardManager[PID:54511]: 🃏 Player 2 hand AFTER auto-forfeit: 0 cards []
CardManager[PID:54511]: 🃏 Auto-forfeit success: True, Card removed: True
CardManager[PID:54511]: Auto-forfeit successful for player 2
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 1, Actual: 2 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 1 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Jack of Hearts by player 0
CardGameUI: Added trick card 0: Jack of Hearts (P0) at position (-5, -20)
CardGameUI: Created trick card button for King of Diamonds by player 2
CardGameUI: Added trick card 1: King of Diamonds (P2) at position (105, -20)
CardGameUI: Updated trick display with 2 cards: [Jack of Hearts (P0), King of Diamonds (P2)]
CardGameUI: Current trick leader: 0, Current turn: 2
CardGameUI: 🃏 Hand update complete - 1 -> 0 cards (played: King of Diamonds)
CardManager: Starting AutoPlayAITurn for AI_Player_100 (ID: 100)
CardManager: AutoPlayAITurn called for player 100
CardManager: GameInProgress: True, CurrentPlayerTurn: 2, PlayerOrder[CurrentPlayerTurn]: 100
CardManager: AI player 100 has 1 valid cards
CardManager: AI player 100 playing Eight of Clubs
CardManager: Nakama game - sending card play to MatchManager - Player 100: Eight of Clubs
CardManager: Added pending card play: 100_Eight of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 100: Eight of Clubs
CardManager: 🃏 Player 100 hand BEFORE removal: 1 cards [Eight of Clubs]
CardManager: 🃏 Player 100 hand AFTER removal: 0 cards []
CardManager: 🃏 Card removal success: True, Card was: Eight of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Eight of Clubs (Trick now has 3 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 100: Eight of Clubs
CardGameUI: Player 100 played Eight of Clubs
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 100: Eight of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 100
CardManager: HOST EndTurn called for player 100
CardGameUI: Turn ended for player 100
CardManager: HOST moving to next player - new CurrentPlayerTurn: 3 (Player 101)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 101, TurnIndex: 3, NextPlayerId: AI_Player_101, TricksPlayed: 12
CardManager: HOST trick continues - 3/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 3, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 101
CardManager: AI turn detected for AI_Player_101 (ID: 101)
CardManager: NetworkManager status - IsHost: False, IsConnected: False
CardGameUI: Turn started for player 101
CardManager: Successfully auto-played card Eight of Clubs for AI player 100
MatchManager[PID:54510]: Received message CardPlayed from mXTgVBRUod
MatchManager: Card play received - Player 100: Eight of Clubs
MatchManager: Card play synchronized - Player_100 played Eight of Clubs
MatchManager[PID:54510]: Received message TurnChange from mXTgVBRUod
MatchManager: Turn changed to AI_Player_101, PlayerTurn: 3, Tricks: 12
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Jack of Hearts by player 0
CardGameUI: Added trick card 0: Jack of Hearts (P0) at position (-60, -20)
CardGameUI: Created trick card button for King of Diamonds by player 2
CardGameUI: Added trick card 1: King of Diamonds (P2) at position (50, -20)
CardGameUI: Created trick card button for Eight of Clubs by player 100
CardGameUI: Added trick card 2: Eight of Clubs (P100) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Jack of Hearts (P0), King of Diamonds (P2), Eight of Clubs (P100)]
CardGameUI: Current trick leader: 0, Current turn: 3
CardManager: OnNakamaCardPlayReceived called - Player 100: Eight of Clubs
CardManager: Received card play from Nakama - Player 100: Eight of Clubs
CardManager[PID:54510]: OnNakama filtering - Player 100 is AI, not skipping
CardManager: Executing synchronized card play - Player 100: Eight of Clubs
CardManager: DEBUG - Not processing as host card play (HasActiveMatch: True, IsMatchOwner: False)
CardManager: Executing card play from host - Player 100: Eight of Clubs
CardGameUI: Player 100 played Eight of Clubs
CardGameUI: OnCardPlayed - playerId: 100, actualLocalPlayerId: 0, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager: NAKAMA CLIENT - card play synchronized, waiting for turn progression from match owner
CardManager: Synchronized card play completed - Player 100: Eight of Clubs
MatchManager: CardPlayReceived signal emitted for player 100: Eight of Clubs
CardManager[PID:54510]: OnNakamaTurnChangeReceived called - PlayerTurn: 3, Tricks: 12
CardManager[PID:54510]: ✅ Processing NEW turn change (Turn: 3, Tricks: 12) - Previous: Turn 2, Tricks 12
CardManager[PID:54510]: Current state before sync - CurrentPlayerTurn: 2, PlayerOrder: [0, 2, 100, 101]
CardManager[PID:54510]: NAKAMA CLIENT - synchronizing turn change
CardManager[PID:54510]: NAKAMA CLIENT - turn synchronized: 2 -> 3 (Player 101)
CardGameUI: Turn started for player 101
CardManager[PID:54510]: NAKAMA CLIENT - TurnStarted signal emitted for player 101
MatchManager: TurnChangeReceived signal emitted - PlayerTurn: 3, Tricks: 12
MatchManager: TurnChanged signal emitted - CurrentPlayerId: AI_Player_101
CardGameUI: 🔧 TRICK DESYNC DETECTED - Display: 2, Actual: 3 - Force refresh
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 2 trick cards immediately
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 0 trick cards immediately
CardGameUI: Created trick card button for Jack of Hearts by player 0
CardGameUI: Added trick card 0: Jack of Hearts (P0) at position (-60, -20)
CardGameUI: Created trick card button for King of Diamonds by player 2
CardGameUI: Added trick card 1: King of Diamonds (P2) at position (50, -20)
CardGameUI: Created trick card button for Eight of Clubs by player 100
CardGameUI: Added trick card 2: Eight of Clubs (P100) at position (160, -20)
CardGameUI: Updated trick display with 3 cards: [Jack of Hearts (P0), King of Diamonds (P2), Eight of Clubs (P100)]
CardGameUI: Current trick leader: 0, Current turn: 3
CardManager: Starting AutoPlayAITurn for AI_Player_101 (ID: 101)
CardManager: AutoPlayAITurn called for player 101
CardManager: GameInProgress: True, CurrentPlayerTurn: 3, PlayerOrder[CurrentPlayerTurn]: 101
CardManager: AI player 101 has 1 valid cards
CardManager: AI player 101 playing Ten of Clubs
CardManager: Nakama game - sending card play to MatchManager - Player 101: Ten of Clubs
CardManager: Added pending card play: 101_Ten of Clubs
CardManager: NAKAMA GAME - executing card immediately: Player 101: Ten of Clubs
CardManager: 🃏 Player 101 hand BEFORE removal: 1 cards [Ten of Clubs]
CardManager: 🃏 Player 101 hand AFTER removal: 0 cards []
CardManager: 🃏 Card removal success: True, Card was: Ten of Clubs
CardManager: NAKAMA - Added card to CurrentTrick immediately: Ten of Clubs (Trick now has 4 cards)
CardManager: 🃏 Emitting CardPlayed signal for Player 101: Ten of Clubs
CardGameUI: Player 101 played Ten of Clubs
CardGameUI: OnCardPlayed - playerId: 101, actualLocalPlayerId: 2, isLocalPlayer: False
CardGameUI: Other player's card - no hand update needed
CardManager[PID:54511]: About to send card play via MatchManager.Instance: 30047995238
MatchManager: Attempting to send message CardPlayed
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardPlayed
MatchManager: Synced card play - Player 101: Ten of Clubs
CardManager: NAKAMA MATCH OWNER - progressing turn immediately (AI player)
CardManager: NAKAMA MATCH OWNER - managing turn ending for Player 101
CardManager: HOST EndTurn called for player 101
CardGameUI: Turn ended for player 101
CardManager: HOST moving to next player - new CurrentPlayerTurn: 0 (Player 0)
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 12
CardManager: HOST trick complete with 4 cards
CardManager: HOST Player 0 wins trick with Jack of Hearts
CardManager[PID:54511]: 🎯 STATE CHANGE: PlayerTurn → EndOfRound (winner: 0)
CardManager: NAKAMA MATCH OWNER - syncing trick completion to all players
MatchManager: Attempting to send message TrickCompleted
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TrickCompleted
MatchManager: Synced trick completion - Winner: 0, Leader: 0, Score: 2
CardGameUI: ========== TRICK COMPLETED (LEGACY) ==========
CardGameUI: Player 0 won the trick - but end-of-round system will handle display
CardGameUI: Completed trick had 4 cards: [Jack of Hearts (P0), King of Diamonds (P2), Eight of Clubs (P100), Ten of Clubs (P101)]
CardGameUI: Next trick leader will be: 0
CardGameUI: Trick completed - waiting for end-of-round system to handle display
CardGameUI: ========== TRICK COMPLETED (LEGACY) END ==========
CardManager[PID:54511]: ========== STARTING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Starting end-of-round turn - winner: 0
CardManager[PID:54511]: Current turn state: EndOfRound
CardGameUI: ========== END OF ROUND STARTED ==========
CardGameUI: End-of-round phase started - winner: 0, 10-second display period
CardGameUI: Showing round results visual overlay - Winner: Client, Next Leader: Client
CardGameUI: Round results timer will sync with CardManager's end-of-round timer
CardGameUI: Round results panel positioned at top of screen for card visibility
CardGameUI: End-of-round display active - cards remain visible for 10 seconds
CardGameUI: ========== END OF ROUND STARTED END ==========
CardManager[PID:54511]: Timer management decision: True (Nakama match owner)
CardManager[PID:54511]: Starting end-of-round timer - Duration: 10 seconds
CardManager[PID:54511]: End-of-round timer started successfully - timerActive: True, WaitTime: 10
CardManager[PID:54511]: NAKAMA MATCH OWNER - end-of-round state started
CardManager[PID:54511]: ========== END-OF-ROUND TURN STARTED ==========
CardManager: Entered end-of-round state - winner: 0, 10-second display period started
CardManager: Successfully auto-played card Ten of Clubs for AI player 101
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: EndOfRound
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54510]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54510]: Current trick winner: 0
CardManager[PID:54510]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54510]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54510]: Current trick has 4 cards
CardManager[PID:54510]: Trick cleared, TricksPlayed: 13, State: PlayerTurn
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54510]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54510]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54510]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54510]: TrickArea has 0 children after cleanup
CardGameUI[PID:54510]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54510]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54510]: EndOfRoundCompleted signal emitted
CardManager[PID:54510]: Hand complete after end-of-round cleanup - calling CompleteHand()
========== HAND COMPLETED ==========
CardManager: Hand completed after 13 tricks
CardManager: Player 0 score: 2
CardManager: Player 2 score: 4
CardManager: Player 100 score: 7
CardManager: Player 101 score: 0
CardManager: Highest score: 7, Target: 10
CardGameUI: Hand completed
CardGameUI: DEBUG MODE - Applying chat intimidation regardless of game outcome
CardGameUI: Scaling font from 12 to 32 (multiplier: 4.0x)
CardGameUI: Applied font size 32 to chat label
CardGameUI: Chat label updated: DEBUG: Chat panel grew 4.0x! Font size: 32px. Growing UP and LEFT, bottom-right corner should stay fixed.
CardGameUI: Chat panel grew - starting shrink timer
CardGameUI: Chat shrink timer started - will auto-shrink in 30s
CardGameUI: === END CHAT PANEL GROWTH DEBUG ===
CardGameUI: Chat intimidation applied directly - 4x growth for debugging
PlayerData: Adding 10 XP for game outcome (won: False)
PlayerData: Adding 3 XP to ThrowPower (current: 0)
PlayerData: Adding 3 XP to MoveSpeed (current: 0)
PlayerData: Adding 3 XP to Composure (current: 0)
PlayerData: Adding 1 XP to ThrowPower (current: 3)
PlayerData: Adding 40 XP for game outcome (won: True)
PlayerData: Adding 13 XP to ThrowPower (current: 0)
PlayerData: Adding 13 XP to MoveSpeed (current: 0)
PlayerData: Adding 13 XP to Composure (current: 0)
PlayerData: Adding 1 XP to ThrowPower (current: 13)
CardManager: Game continuing - dealing next hand (no player has 10 points yet)
CardManager: New dealer: 1, Turn state reset to PlayerTurn
CardManager: NAKAMA CLIENT - waiting for match owner to deal cards
CardManager[PID:54510]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54510]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
Chat panel resized to: (286, 176)
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: EndOfRound
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: ========== END-OF-ROUND TIMER EXPIRED ==========
CardManager[PID:54511]: End-of-round timer expired - completing end-of-round phase
CardManager[PID:54511]: Current trick winner: 0
CardManager[PID:54511]: ========== COMPLETING END-OF-ROUND TURN ==========
CardManager[PID:54511]: Completing end-of-round turn - cleaning up trick and continuing
CardManager[PID:54511]: Current trick has 4 cards
CardManager[PID:54511]: Trick cleared, TricksPlayed: 13, State: PlayerTurn
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED ==========
CardGameUI[PID:54511]: End-of-round phase completed by CardManager - cleaning up trick display
CardGameUI[PID:54511]: TrickArea has 4 children before cleanup
CardGameUI: ClearTrickDisplay called - removing all TrickCard nodes
CardGameUI: Cleared 4 trick cards immediately
CardGameUI[PID:54511]: Hiding round results panel as part of end-of-round completion
CardGameUI: Round results hidden - visual overlay removed
CardGameUI: Card cleanup will be handled by CardManager's OnEndOfRoundCompleted event
CardGameUI[PID:54511]: TrickArea has 0 children after cleanup
CardGameUI[PID:54511]: End-of-round cleanup completed - ready for next trick
CardGameUI[PID:54511]: ========== END OF ROUND COMPLETED END ==========
CardManager[PID:54511]: EndOfRoundCompleted signal emitted
CardManager[PID:54511]: Hand complete after end-of-round cleanup - calling CompleteHand()
========== HAND COMPLETED ==========
CardManager: Hand completed after 13 tricks
CardManager: Player 0 score: 2
CardManager: Player 2 score: 4
CardManager: Player 100 score: 7
CardManager: Player 101 score: 0
CardManager: Highest score: 7, Target: 10
CardGameUI: Hand completed
CardGameUI: DEBUG MODE - Applying chat intimidation regardless of game outcome
CardGameUI: Scaling font from 12 to 32 (multiplier: 4.0x)
CardGameUI: Applied font size 32 to chat label
CardGameUI: Chat label updated: DEBUG: Chat panel grew 4.0x! Font size: 32px. Growing UP and LEFT, bottom-right corner should stay fixed.
CardGameUI: Chat panel grew - starting shrink timer
CardGameUI: Chat shrink timer started - will auto-shrink in 30s
CardGameUI: === END CHAT PANEL GROWTH DEBUG ===
CardGameUI: Chat intimidation applied directly - 4x growth for debugging
PlayerData: Adding 10 XP for game outcome (won: False)
PlayerData: Adding 3 XP to ThrowPower (current: 0)
PlayerData: Adding 3 XP to MoveSpeed (current: 0)
PlayerData: Adding 3 XP to Composure (current: 0)
PlayerData: Adding 1 XP to ThrowPower (current: 3)
PlayerData: Adding 40 XP for game outcome (won: True)
PlayerData: Adding 13 XP to ThrowPower (current: 0)
PlayerData: Adding 13 XP to MoveSpeed (current: 0)
PlayerData: Adding 13 XP to Composure (current: 0)
PlayerData: Adding 1 XP to ThrowPower (current: 13)
CardManager: Game continuing - dealing next hand (no player has 10 points yet)
CardManager: New dealer: 1, Turn state reset to PlayerTurn
CardManager: NAKAMA MATCH OWNER - dealing cards and syncing via Nakama
CardManager: HOST dealing cards and syncing to clients
CardManager: HOST using PlayerOrder: [0, 2, 100, 101]
CardManager: Shuffling deck with Nakama-synchronized seed: 1012449424
CardManager: HOST dealt 13 cards to player 0
CardManager: HOST dealt 13 cards to player 2
CardManager: HOST dealt 13 cards to player 100
CardManager: HOST dealt 13 cards to player 101
CardManager: NAKAMA MATCH OWNER - syncing ALL dealt cards to all players
MatchManager: Attempting to send message CardsDealt
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message CardsDealt
MatchManager: Synced dealt cards to all players - 4 hands
CardManager: NAKAMA MATCH OWNER - sent dealt cards for 4 players
CardGameUI: Cards dealt - updating hand display
========== NEW HAND STARTING ==========
CardManager: HOST starting new hand
CardManager: TrickLeader: 0, CurrentTurn: 0
CardManager: Turn player: 0
CardManager: Turn state: PlayerTurn
CardManager: Game in progress: True
CardManager: Timer active: False
CardManager: Timer exists: True
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager: NAKAMA MATCH OWNER - managing turn for player 0
CardManager: Human player turn for Client (ID: 0)
CardManager: Starting turn timer for player 0
CardManager: Timer state before start - exists: True, active: False, waitTime: 10
CardManager: Timer started successfully - duration: 10s, timeLeft: 10, active: True
CardManager: NAKAMA MATCH OWNER - syncing turn start to all instances
MatchManager: Attempting to send message TurnChange
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TurnChange
MatchManager: Synced turn change - CurrentPlayerId: 0, TurnIndex: 0, NextPlayerId: 5af6f516-d9a4-424b-8447-0968cb480d1c, TricksPlayed: 0
CardGameUI: Turn started for player 0
CardManager: ========== NEW HAND STARTED ==========
CardManager[PID:54511]: ========== END-OF-ROUND TURN COMPLETED ==========
CardManager[PID:54511]: ========== END-OF-ROUND CLEANUP COMPLETED ==========
CardGameUI: UpdatePlayerHand called
CardGameUI: Local player ID: 2 (actualLocalPlayerId)
CardGameUI: gameManager.LocalPlayer.PlayerId: 2 (for comparison)
CardGameUI: 🃏 CardManager.GetPlayerHand(2) returned 13 cards
CardGameUI: 🃏 UI currentPlayerCards had 0 cards before update
CardGameUI: 🃏 UpdatePlayerHand - Player 2: 0 -> 13 cards
CardGameUI: 🃏 Current cards in hand: [Nine of Diamonds, Ten of Diamonds, King of Diamonds, Ace of Clubs, Six of Diamonds, Ace of Hearts, Queen of Hearts, Five of Diamonds, Three of Clubs, Ten of Spades, Queen of Spades, Three of Diamonds, Four of Spades]
Chat panel resized to: (275.59998, 169.59998)
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager[PID:54510]: Raw TimerUpdate received - OpCode: 11, State length: 22
MatchManager: Attempting to send message TimerUpdate
MatchManager: Current match null: False
MatchManager: Nakama null: False
MatchManager: Nakama socket null: False
MatchManager[PID:54511]: Sending message to match f7f8a4a8-e36f-5ebb-8a51-8675d46a8dec.
MatchManager[PID:54511]: Local user ID: 72cbb72a-f851-4dfd-a409-3103e33e93a4
MatchManager[PID:54511]: Successfully sent message TimerUpdate
CardManager[PID:54510]: Client timer synced to: 1.0s
CardManager[PID:54511]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54511]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54511]: Current turn state: PlayerTurn
CardManager[PID:54511]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54511]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54511]: CurrentTrick has 0 cards: []
CardManager[PID:54511]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54511]: Player 0 isPlayerAtTable: True
CardManager[PID:54511]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54511]: Auto-forfeit check for player 0: False (different player's instance)
CardManager[PID:54511]: Skipping auto-forfeit for player 0 - different player's instance
CardManager[PID:54510]: ⏰ ========== TIMER EXPIRED ==========
CardManager[PID:54510]: OnTurnTimerExpired called - timerActive: True, GameInProgress: True
CardManager[PID:54510]: Current turn state: PlayerTurn
CardManager[PID:54510]: Current game state - CurrentPlayerTurn: 0, PlayerOrder.Count: 4
CardManager[PID:54510]: Timer expired for player 0, playerAlreadyPlayed: False
CardManager[PID:54510]: CurrentTrick has 0 cards: []
CardManager[PID:54510]: Turn timer expired for player 0 - executing auto-forfeit
CardManager[PID:54510]: Player 0 isPlayerAtTable: True
CardManager[PID:54510]: Mapped game player 0 to Nakama user 5af6f516-d9a4-424b-8447-0968cb480d1c
CardManager[PID:54510]: Auto-forfeit check for player 0: True (local player's own instance)
CardManager[PID:54510]: Player 0 at table - auto-forfeiting with lowest card
CardManager[PID:54510]: Auto-forfeiting player 0 with card Two of Clubs
CardManager[PID:54510]: 🃏 Player 0 hand BEFORE auto-forfeit: 13 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Eight of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Two of Clubs, Seven of Spades, Jack of Hearts]
CardManager: Nakama game - sending card play to MatchManager - Player 0: Two of Clubs
CardManager: Duplicate card play prevented - 0_Two of Clubs already pending
CardManager[PID:54510]: 🃏 Player 0 hand AFTER auto-forfeit: 13 cards [Three of Spades, Seven of Hearts, Jack of Spades, Five of Spades, Five of Clubs, Six of Spades, Eight of Spades, Ace of Spades, Ten of Hearts, Three of Hearts, Two of Clubs, Seven of Spades, Jack of Hearts]
CardManager[PID:54510]: 🃏 Auto-forfeit success: False, Card removed: False
  ERROR: CardManager[PID:54510]: Failed to auto-forfeit card Two of Clubs for player 0
CardManager: NAKAMA CLIENT - ending own player's turn (Player 0)
CardManager: HOST EndTurn called for player 0
CardGameUI: Turn ended for player 0
CardManager: HOST moving to next player - new CurrentPlayerTurn: 1 (Player 2)
CardManager: HOST trick continues - 0/4 cards played
CardManager: StartTurn() called - State: PlayerTurn, GameInProgress: True
CardManager: CurrentPlayerTurn: 1, PlayerOrder.Count: 4
CardManager: NAKAMA CLIENT - not my turn, just emitting signal for Player 2
CardGameUI: Turn started for player 2
CardGameUI: Player clicked Three of Diamonds
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Three of Diamonds
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Three of Diamonds - not valid move
CardGameUI: Player clicked Queen of Hearts
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Hearts
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Queen of Hearts - not valid move
CardGameUI: Player clicked Queen of Hearts
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Hearts
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Queen of Hearts - not valid move
CardGameUI: Player clicked Queen of Hearts
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Hearts
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Queen of Hearts - not valid move
CardGameUI: Chat shrink timer expired - auto-shrinking chat to normal size
CardGameUI: Scaling font from 12 to 12 (multiplier: 1.0x)
CardGameUI: Applied font size 12 to chat label
CardGameUI: Chat label updated: Chat panel back to normal size.
CardGameUI: Chat panel shrunk - stopping shrink timer
CardGameUI: === END CHAT PANEL GROWTH DEBUG ===
CardGameUI: Chat auto-shrink completed
Chat panel resized to: (1024.4, 630.4)
CardGameUI: Chat shrink timer expired - auto-shrinking chat to normal size
CardGameUI: Scaling font from 12 to 12 (multiplier: 1.0x)
CardGameUI: Applied font size 12 to chat label
CardGameUI: Chat label updated: Chat panel back to normal size.
CardGameUI: Chat panel shrunk - stopping shrink timer
CardGameUI: === END CHAT PANEL GROWTH DEBUG ===
CardGameUI: Chat auto-shrink completed
Chat panel resized to: (1014, 624)
Chat panel resized to: (1008.7999, 620.8)
Chat panel resized to: (993.19995, 611.2)
Chat panel resized to: (977.5999, 601.6)
Chat panel resized to: (962, 592)
Chat panel resized to: (946.4, 582.4)
Chat panel resized to: (988, 608)
Chat panel resized to: (962, 592)
Chat panel resized to: (936, 576)
Chat panel resized to: (910, 560)
Chat panel resized to: (930.7999, 572.8)
Chat panel resized to: (915.2, 563.2)
Chat panel resized to: (899.5999, 553.6)
Chat panel resized to: (884, 544)
Chat panel resized to: (868.4, 534.4)
Chat panel resized to: (884, 544)
Chat panel resized to: (858, 528)
Chat panel resized to: (832, 512)
Chat panel resized to: (806, 496)
Chat panel resized to: (852.7999, 524.8)
Chat panel resized to: (837.2, 515.2)
Chat panel resized to: (821.5999, 505.60004)
Chat panel resized to: (806, 496)
Chat panel resized to: (790.4, 486.40002)
Chat panel resized to: (780, 480)
Chat panel resized to: (754, 464)
Chat panel resized to: (728, 448)
Chat panel resized to: (702, 432)
Chat panel resized to: (774.7999, 476.8)
Chat panel resized to: (759.1999, 467.2)
Chat panel resized to: (743.6, 457.60004)
Chat panel resized to: (728, 448.00003)
Chat panel resized to: (712.4, 438.40002)
Chat panel resized to: (676, 416)
Chat panel resized to: (650, 400)
Chat panel resized to: (624, 384)
Chat panel resized to: (598, 368)
Chat panel resized to: (696.7999, 428.8)
Chat panel resized to: (681.1999, 419.2)
Chat panel resized to: (665.6, 409.60004)
Chat panel resized to: (650, 400.00003)
Chat panel resized to: (634.4, 390.40002)
Chat panel resized to: (572, 352)
Chat panel resized to: (546, 336)
Chat panel resized to: (520, 320)
Chat panel resized to: (494, 304)
Chat panel resized to: (618.7999, 380.8)
Chat panel resized to: (603.2, 371.2)
Chat panel resized to: (587.5999, 361.60004)
Chat panel resized to: (572, 352.00003)
Chat panel resized to: (556.4, 342.40002)
Chat panel resized to: (468, 288)
Chat panel resized to: (442, 272)
Chat panel resized to: (416, 256)
Chat panel resized to: (390, 240)
Chat panel resized to: (540.80005, 332.80002)
Chat panel resized to: (525.19995, 323.19998)
Chat panel resized to: (509.59998, 313.6)
Chat panel resized to: (494, 304.00003)
Chat panel resized to: (478.3999, 294.40002)
Chat panel resized to: (364, 224)
Chat panel resized to: (338, 208)
Chat panel resized to: (312, 192)
Chat panel resized to: (286, 176)
Chat panel resized to: (462.8, 284.80002)
Chat panel resized to: (447.2, 275.2)
Chat panel resized to: (431.60004, 265.60004)
Chat panel resized to: (415.99994, 256.00003)
Chat panel resized to: (400.40002, 246.40002)
Chat panel resized to: (260, 160)
Chat panel resized to: (384.80005, 236.80002)
Chat panel resized to: (369.19995, 227.19998)
Chat panel resized to: (353.5999, 217.6)
Chat panel resized to: (338, 208.00003)
Chat panel resized to: (322.3999, 198.40002)
Chat panel resized to: (306.8, 188.80002)
Chat panel resized to: (291.2, 179.20001)
Chat panel resized to: (275.5999, 169.59998)
Chat panel resized to: (260, 160)
CardGameUI: Player clicked Six of Diamonds
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Six of Diamonds
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Six of Diamonds - not valid move
CardGameUI: Player clicked Six of Diamonds
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Six of Diamonds
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Six of Diamonds - not valid move
CardGameUI: Player clicked Six of Diamonds
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Six of Diamonds
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Six of Diamonds - not valid move
CardGameUI: Player clicked Queen of Hearts
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Hearts
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Queen of Hearts - not valid move
CardGameUI: Player clicked Queen of Hearts
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Hearts
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Queen of Hearts - not valid move
CardGameUI: Player clicked Queen of Hearts
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Hearts
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Queen of Hearts - not valid move
CardGameUI: Player clicked Queen of Hearts
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Hearts
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Queen of Hearts - not valid move
CardGameUI: Player clicked Queen of Hearts
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Queen of Hearts
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Queen of Hearts - not valid move
CardGameUI: Player clicked Four of Spades
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Four of Spades
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Four of Spades - not valid move
CardGameUI: Player clicked Four of Spades
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Four of Spades
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Four of Spades - not valid move
CardGameUI: Player clicked Four of Spades
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Four of Spades
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Four of Spades - not valid move
CardGameUI: Player clicked Five of Diamonds
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Five of Diamonds
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Five of Diamonds - not valid move
CardGameUI: Player clicked Three of Clubs
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Three of Clubs
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Three of Clubs - not valid move
CardGameUI: Player clicked Three of Clubs
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Three of Clubs
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Three of Clubs - not valid move
CardGameUI: Player clicked Three of Clubs
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Three of Clubs
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Three of Clubs - not valid move
CardGameUI: Player clicked Three of Clubs
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Three of Clubs
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Three of Clubs - not valid move
CardGameUI: Player clicked Three of Clubs
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Three of Clubs
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Three of Clubs - not valid move
CardGameUI: Player clicked Three of Clubs
CardGameUI: Using actualLocalPlayerId: 2 for card play
CardManager: Nakama game - sending card play to MatchManager - Player 2: Three of Clubs
  ERROR: CardManager: Not player 2's turn
CardGameUI: Failed to play Three of Clubs - not valid move
--- Debugging process stopped ---
