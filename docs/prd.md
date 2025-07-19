# Sore Losers – Product Requirements Document  
**Version:** 0.1 (14 Jul 2025)  
**Owner:** _you_  

---

## 1  Purpose & Vision  
Create a light‑hearted, desktop (macOS‑first) multiplayer card‑and‑shenanigans game for friends.  
The core loop is a fast trick‑taking card match, but the real fun comes from sabotaging opponents’ user‑interfaces—throwing digital eggs, dropping stink bombs, and testing each player’s composure.  
The game should be easy to pick up in one session, run on modest hardware, and ship free of charge while still leaving room for post‑launch extensions.

---

## 2  Goals & Success Metrics  

| Goal | Metric | Target |
|------|--------|--------|
| **Fun in 10 minutes** | Average session length (closed alpha) | ≥ 10 min |
| **Low barrier to entry** | Compressed download size (.dmg) | ≤ 200 MB |
| **Smooth peer hosting** | Avg. round‑trip time (same‑region friends) | < 50 ms |
| **Sticky progression** | Day‑7 returning players (closed alpha) | ≥ 40 % |

---

## 3  Target Platform & Technical Stack  

| Layer | Tech | Notes |
|-------|------|-------|
| **Client** | Godot 4.x (GDScript) | macOS export template only for v0.1 |
| **Networking** | High‑level ENet host/peer | One player hosts; optional AWS headless server (stretch) |
| **Persistence** | Local JSON save files | Holds player profile & stat XP |
| **CI / CD** | GitHub Actions → macOS `.dmg` artifact | Nightly builds, manual release tags |
| **Art** | Envato Elements **or** processed phone photos | Import at native res, `Filter = Nearest` |
| **Audio** | Public‑domain SFX + 1–2 looping music tracks | Mix to –14 LUFS |

---

## 4  Key Features (MVP)  

| # | Title | Description | Priority |
|---|-------|-------------|----------|
| F‑1 | **Private Match Host & Join** | Player hosts a match, shares 6‑digit room code; up to 4 seats. | P0 |
| F‑2 | **Turn‑based Trick‑Taking Core** | Deck of 52 (or custom) cards. 10 s turn timer—auto‑forfeit if expired. | P0 |
| F‑3 | **Sabotage System** | Data‑driven shenanigans framework. Ships with:<br> • *Egg Throw* – smear egg overlay until washed.<br> • *Stink Bomb* – area fog if caught, lasts 30 s. | P0 |
| F‑4 | **Chat‑Window Intimidation** | Losing a round grows chat panel; shrinks on next win or after 30 s AFK. | P0 |
| F‑5 | **RPG Stats & Progression** | Throw Power, Move Speed, Composure (Lvl 1–10, XP‑based). | P1 |
| F‑6 | **Basic SFX & Music** | Shuffle, card‑place, footsteps, egg splat, fog hiss, ambient loop. | P1 |
| F‑7 | **Settings & Accessibility** | Volume sliders, key‑remap, “reduced obstruction” (dims overlays). | P2 |
| F‑8 | **Match Results Screen** | Summary of wins, XP gained, stat increases. | P2 |

*P0 = must for first playable; P1 = ship in same alpha if possible; P2 = nice‑to‑have before public itch.io build.*

---

## 5  Game‑Design Details  

### 5.1  Card Phase  
* Standard trick‑taking (highest card of led suit wins).  
* Round ends when all cards played; points tallied; loser flagged for chat growth.

### 5.2  Concurrent Gameplay Design (UPDATED - Major Change)
**Core Innovation**: Instead of sequential phases, the card game and real-time sabotage occur **simultaneously**.

* **Always-Available Movement**: Players can leave the card table anytime via "Leave Table" button
* **Continuous Card Game**: Card game continues regardless of player presence at table
* **Location-Based Interactions**: Players must be "At Table" to play cards, "In Kitchen" for sabotage
* **Missed Turn Handling**: Players away from table when their turn comes up miss their turn (no auto-forfeit)
* **Flexible Return**: Players can return to table anytime via "Return to Table" button
* **No Phase Transitions**: Game never locks players into specific phases

### 5.3  Dual-View System
* **Card Table View**: Traditional card game interface with hand, timer, scores
* **Kitchen View**: Top-down movement with WASD controls, item interaction
* **Seamless Switching**: UI instantly transitions between views when changing location
* **Location Status**: Clear indication whether player is "At Table" or "In Kitchen"

### 5.4  Real-Time Movement & Sabotage (In Kitchen)
* **Movement:** WASD (or arrows) in a small top‑down room.  
* **Interact (Space):** pick up items, drop bombs, wash face.  
* **Items spawn** at preset props (egg tray, drinks bar).  
* **Physics:** KinematicBody2D move & projectile arcs.
* **Targeting**: Sabotage affects players regardless of their current location

### 5.5  Sabotages  

| Sabotage | Flow | Effects |
|----------|------|---------|
| **Egg Throw** | Pick up ≤ 3 eggs → aim → throw. | Overlay splat image; washed off at sink. Coverage % scales with **Throw Power**. |
| **Stink Bomb** | Pick bomb → arm → drop. 0.8 s warning decal, 160 px radius. | Players inside radius when it detonates get green fog overlay & mild blur for **30 s** (fixed). High **Composure** reduces blur strength. Cannot be cleared early. Cooldown 20 s before next bomb spawn. |

Both sabotages use a common `ObstructionOverlay` node and share inventory logic.

**✅ Implementation Status (January 2025)**: The egg throwing system is **100% production-ready** with complete multiplayer support:
- **🌐 Nakama Multiplayer**: Full bidirectional synchronization between all game instances
- **🎯 Complete Targeting**: Host ↔ Client throwing and self-targeting support
- **🎨 Visual Overlays**: Realistic PNG graphics using `Raw_egg_splatter` PNG asset
- **📏 Enhanced Scaling**: 15x base size (3000px) for maximum visual impact  
- **⚡ ThrowPower Integration**: Size scales from 20% to 80% screen coverage based on player stats
- **🧹 Robust Cleanup**: Metadata-based removal system for reliable effect clearing
- **🔧 Thread Safety**: All network operations properly handled with CallDeferred patterns
- **🎮 Real-time Sync**: Perfect synchronization with professional Nakama backend
- **🏠 Kitchen Environment**: Authentic background using `background.png` with aligned interactive elements

### 5.6  Stat & XP Proposal  

| Stat | Base (Lvl 1) | Max (Lvl 10) | Gameplay impact |
|------|--------------|--------------|-----------------|
| **Throw Power** | 20 % screen coverage | 80 % coverage | Affects obstruction size (egg splat, fog density radius). |
| **Move Speed** | 110 px / s | 160 px / s | Map traversal, escape stink radius. |
| **Composure** | 100 % blur strength | 50 % blur strength | Reduces visual severity of overlays, but not duration. |

**XP curve (per stat)**  
`XP_to_next = 50 × level²` → Level 1→2: 50 XP … Level 9→10: 4 050 XP  

*Win hand*: +40 XP (split evenly across stats in use)  
*Lose hand*: +10 XP  
*Successful sabotage*: +20 XP to related stat

---

## 6  User Stories (excerpt)  

1. **As a host**, I can create a private room so my friends can join quickly.  
2. **As a player** who loses a round, I see the chat panel enlarge, increasing pressure.  
3. **As a strategic player**, I can leave the table to sabotage opponents while they continue playing cards.
4. **As a focused card player**, I can stay at the table and ignore sabotage opportunities to focus on winning hands.
5. **As an absent player**, I miss my turn when I'm away from the table, creating strategic timing decisions.
6. **As an aggressor**, I can drop a stink bomb forcing slow opponents to flee or suffer.  
7. **As a victim**, I must decide whether to ignore an overlay or take time to clear it.
8. **As a multitasker**, I can quickly leave table, perform sabotage, and return before my next turn.
9. **As a grinder**, I see tangible stat growth over multiple sessions.

---

## 7  Non‑Functional Requirements  

| Category | Requirement |
|----------|-------------|
| **Performance** | ≥ 60 FPS on 2017 MacBook Air (Intel HD 6000) |
| **Netcode** | Resilient to 250 ms latency without de‑sync |
| **Security** | Host authoritative over deck shuffle & timers |
| **Accessibility** | Subtitles for SFX, colour‑blind‑safe palette, overlay dim option |

---

## 8  Out‑Of‑Scope (v0.1)  

* Voice chat  
* Public matchmaking / ranked ladders  
* Monetization, DLC, cosmetics  
* Web, Windows, Linux, mobile ports  
* AI personas / LLM interactions  

---

## 9  Risks & Mitigations  

| Risk | Impact | Mitigation |
|------|--------|-----------|
| Inconsistent visual style | Low immersion | Pixelate all photos to a uniform grid; limit palette |
| Host advantage cheating | Unfair decks | Optional AWS authoritative server later |
| Overlay mechanic frustrates some | Early churn | Provide “reduced obstruction” option & balance via **Composure** |

---

## 10  Milestones (suggested)  

| Offset | Deliverable |
|--------|-------------|
| **T + 2 wks** | Card logic & local turn timer prototype |
| **T + 4 wks** | LAN host/client demo (2 Macs) |
| **T + 6 wks** | Egg throw & stink bomb in place, basic room art |
| **T + 8 wks** | Stat gain, XP curve, local saves |
| **T + 10 wks** | Alpha build: private matches, basic UI, SFX, music loop |
| **T + 12 wks** | Closed friend play‑test; telemetry & balance pass |

*(“T” = start of production sprint)*

---

## 11  Open Questions  

* Exact card rules (standard deck vs. custom suits?)  
* Map dimensions & stink radius fine‑tuning  
* Long‑term meta (cosmetics? leaderboard?) post‑alpha  

---

_This PRD is intended as a living document; update as scope evolves._  
