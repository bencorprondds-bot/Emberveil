# The Valley of Sisters - Implementation Plan

*Unity 2D Cozy Narrative Adventure*

---

## Overview

This plan takes you from an empty Unity project to a playable vertical slice. Each phase produces something playable in 1–2 weekends. Total estimated time: 8–12 weekends for a basic prototype.

## Guiding Principles

- **Ship early, ship often.** Each phase ends with something playable.
- **Art is a trap.** Use placeholder sprites until mechanics feel right.
- **One screen first.** Build the Old Oak interior completely before the overworld.
- **Kid-compatible commits.** Keep sessions to 45–60 minute chunks with clear goals.

---

## Tech Stack

| Component | Choice |
|-----------|--------|
| Engine | Unity 2022.3 LTS with URP |
| Language | C# |
| Art Style | Pixel art (32x32 tiles) |
| Version Control | Git + GitHub |
| Dialogue | Yarn Spinner |

---

## Phase 0: Environment Setup (Weekend 1)

**Goal:** Empty project that runs. No gameplay yet—just infrastructure.

### Tasks

- [ ] Install Unity Hub
- [ ] Install Unity 2022.3 LTS (select 2D URP template)
- [ ] Create new project called "Emberveil" or "ValleyOfSisters"
- [ ] Create folder structure:
  - [ ] Assets/Scenes
  - [ ] Assets/Scripts (copy provided scripts here)
  - [ ] Assets/Sprites
  - [ ] Assets/Prefabs
  - [ ] Assets/Audio
  - [ ] Assets/Dialogue
  - [ ] Assets/ScriptableObjects/Items
- [ ] Initialize Git repository
- [ ] Add Unity .gitignore
- [ ] Make first commit
- [ ] Push to GitHub (https://github.com/bencorprondds-bot/Emberveil)
- [ ] Create placeholder sprites (32x32 colored squares):
  - [ ] Blue square (Mouse)
  - [ ] Brown square (Bear)
  - [ ] Gray square (Hawk)
  - [ ] Green square (ground tile)
  - [ ] Dark gray square (wall tile)
- [ ] Import Yarn Spinner from Unity Asset Store

### Definition of Done

You can hit Play in Unity and see a blank scene with no errors. Project is on GitHub.

---

## Phase 1: Mouse Moves (Weekends 2–3)

**Goal:** Mouse walks around a single room.

### Tasks

- [ ] Create "Burrow" scene
- [ ] Set up Tilemap for floor
- [ ] Set up Tilemap for walls (with Tilemap Collider 2D)
- [ ] Build a simple 20x15 tile room
- [ ] Create Mouse GameObject:
  - [ ] Add SpriteRenderer (blue square)
  - [ ] Add Rigidbody2D (set to Kinematic)
  - [ ] Add BoxCollider2D
- [ ] Attach PlayerController.cs script to Mouse
- [ ] Test WASD movement
- [ ] Install Cinemachine package
- [ ] Set up Cinemachine camera to follow Mouse
- [ ] Verify Mouse can't walk through walls

### Definition of Done

Blue square (Mouse) moves around a room using WASD. Can't walk through walls. Camera follows.

---

## Phase 2: The Gloves (Weekends 4–5)

**Goal:** Mouse can interact with objects using context-sensitive Gloves.

### Tasks

- [ ] Create "Interactable" layer in Unity (Edit > Project Settings > Tags and Layers)
- [ ] Attach GloveController.cs to Mouse GameObject
- [ ] Set the interactableLayer on GloveController to the Interactable layer
- [ ] Create a test crate:
  - [ ] Create GameObject with SpriteRenderer (use a different colored square)
  - [ ] Add BoxCollider2D
  - [ ] Set layer to "Interactable"
  - [ ] Attach LiftableObject.cs script
- [ ] Test right-click to activate Gloves
- [ ] Test interaction with crate (should lift when clicked)
- [ ] Verify crate follows Mouse when lifted
- [ ] Verify crate drops when button released
- [ ] Add URP 2D Light to Mouse (blue-green color):
  - [ ] Add Light 2D component
  - [ ] Set color to RGB(50, 200, 150)
  - [ ] Set intensity low (0.5)
  - [ ] Enable/disable based on Gloves active state
- [ ] Test hover highlighting on interactables

### Definition of Done

Mouse can pick up a crate and move it around the room. Crate drops when you release the button. Gloves glow when active.

---

## Phase 3: Talking to Friends (Weekend 6)

**Goal:** Mouse can have conversations with NPCs.

### Tasks

- [ ] Add Hawk to the Burrow scene:
  - [ ] Create GameObject with SpriteRenderer (gray square)
  - [ ] Add BoxCollider2D
  - [ ] Set layer to "Interactable"
  - [ ] Attach TalkableNPC.cs script
  - [ ] Set characterName to "Hawk"
- [ ] Create first Yarn dialogue file (Assets/Dialogue/Hawk_Intro.yarn):
  ```yarn
  title: Hawk_Intro
  ---
  Hawk: Good morning, Mouse. Did you sleep well?
  Mouse: Well enough. The burrow is cozy.
  Hawk: I'm glad. There's much work to be done today.
  ===
  ```
- [ ] Set up Yarn Spinner DialogueRunner in scene
- [ ] Set up basic Dialogue UI:
  - [ ] Text box at bottom of screen
  - [ ] Character name display
  - [ ] "Press space to continue" prompt
- [ ] Connect TalkableNPC to Yarn Spinner (uncomment the Yarn code)
- [ ] Test talking to Hawk
- [ ] Verify Mouse freezes during dialogue
- [ ] Verify Mouse regains control after dialogue ends

### Definition of Done

Mouse walks up to Hawk, presses E, dialogue box appears, conversation plays, then Mouse regains control.

---

## Phase 4: The Workshop (Weekends 7–8)

**Goal:** Mouse can descend to the Workshop and use the Forge.

### Tasks

- [ ] Create "Workshop" scene:
  - [ ] Circular room layout with Tilemap
  - [ ] Spiral staircase entrance (visual only for now)
  - [ ] Three workbenches along walls (static sprites)
  - [ ] The Forge (black rectangle sprite on far wall)
- [ ] Create SpawnPoint GameObjects in both scenes:
  - [ ] "FromWorkshop" spawn point in Burrow (near stairs)
  - [ ] "FromBurrow" spawn point in Workshop (at staircase)
- [ ] Add SceneTransition trigger:
  - [ ] Trigger zone at stairs in Burrow → loads Workshop
  - [ ] Trigger zone at stairs in Workshop → loads Burrow
- [ ] Test scene transitions with fade effect
- [ ] Create Forge as an Interactable:
  - [ ] Create ForgeInteractable.cs (simple menu for now)
  - [ ] Display "Craft" option when interacted with
- [ ] Create ItemData ScriptableObjects:
  - [ ] MetalFrames.asset
  - [ ] Lenses.asset
  - [ ] HawksGlasses.asset (craftable, requires frames + lenses)
- [ ] Implement basic crafting at Forge:
  - [ ] For now: just give player the glasses when they interact
  - [ ] Later: proper crafting UI
- [ ] Create "give item to NPC" interaction:
  - [ ] Update TalkableNPC to check for quest items
  - [ ] Special dialogue when Mouse has Hawk's glasses
- [ ] Test full loop: Burrow → Workshop → Craft → Burrow → Give to Hawk

### Definition of Done

Mouse can go downstairs, interact with Forge, craft glasses, bring them to Hawk, and have a dialogue where Hawk thanks her.

---

## Phase 5: The Outside World (Weekends 9–10)

**Goal:** Mouse can leave the Old Oak and explore a small area.

### Tasks

- [ ] Create "Overworld" scene:
  - [ ] Larger Tilemap (maybe 50x50 tiles)
  - [ ] The Old Oak (building you can enter)
  - [ ] A field with grass
  - [ ] Edge of Emberveil forest (dark trees)
  - [ ] River running through
- [ ] Add scene transitions:
  - [ ] Door of Old Oak → Burrow
  - [ ] Burrow door → Overworld
- [ ] Add Bear as a wandering NPC:
  - [ ] Brown square sprite
  - [ ] TalkableNPC component
  - [ ] Dialogue about fishing and pie
- [ ] Implement basic companion system:
  - [ ] CompanionManager.cs (tracks active companion)
  - [ ] After dialogue option, Bear can "join" Mouse
  - [ ] Bear follows Mouse at a distance
- [ ] Create first outdoor puzzle:
  - [ ] Heavy log blocking path
  - [ ] LiftableObject with requiresCompanion = true
  - [ ] Only moveable when Bear is companion
- [ ] Test recruiting Bear and solving puzzle

### Definition of Done

Mouse exits the Old Oak, finds Bear, recruits him, and together they clear a blocked path.

---

## Phase 6: Vertical Slice Polish (Weekends 11–12)

**Goal:** Make what you have feel like a game.

### Tasks

- [ ] Replace placeholder sprites:
  - [ ] Find or create Mouse sprite (pixel art)
  - [ ] Find or create Hawk sprite
  - [ ] Find or create Bear sprite
  - [ ] Find or create environment tiles
- [ ] Add sound effects:
  - [ ] Footsteps
  - [ ] Glove activation hum
  - [ ] Forge door opening
  - [ ] Dialogue text blip
  - [ ] Item pickup
- [ ] Add ambient music:
  - [ ] Burrow theme (cozy, warm)
  - [ ] Workshop theme (mysterious, industrial)
  - [ ] Overworld theme (pastoral, open)
- [ ] Implement "Bean Mode" hint system:
  - [ ] Track time since last meaningful action
  - [ ] If stuck > 60 seconds, show hint prompt
  - [ ] Bean (rabbit kit) character pops up with suggestion
- [ ] Build main menu:
  - [ ] Title screen
  - [ ] "New Game" button
  - [ ] "Continue" button (disabled until save system exists)
  - [ ] "Options" button (volume sliders)
- [ ] Playtest with your kids:
  - [ ] Watch them play without helping
  - [ ] Note every point of confusion
  - [ ] Fix the top 3 issues
- [ ] Build and export a playable .exe

### Definition of Done

A stranger could pick up the game, understand what to do, and play through the crafting-glasses-for-Hawk quest without help.

---

## What's NOT in This Plan

These are explicitly deferred to later phases:

- [ ] The Greenhouse and farming mechanics
- [ ] The Summer Follies minigames (Bear's Bake-Off, Ring Toss)
- [ ] Stealth sections in the Apple Orchard
- [ ] Bramblethorn as a rideable mount
- [ ] The "Scan" ability to see Embers
- [ ] The storm/weather system
- [ ] Full narrative (26+ chapters)
- [ ] Save/load system
- [ ] Multiple companions
- [ ] Combat (there is none, but worth noting)

---

## Project Structure

```
Emberveil/
├── Assets/
│   ├── Scenes/
│   │   ├── MainMenu.unity
│   │   ├── Burrow.unity
│   │   ├── Workshop.unity
│   │   └── Overworld.unity
│   ├── Scripts/
│   │   ├── Player/
│   │   │   ├── PlayerController.cs
│   │   │   └── GloveController.cs
│   │   ├── Interaction/
│   │   │   ├── IInteractable.cs
│   │   │   ├── Interactable.cs
│   │   │   ├── LiftableObject.cs
│   │   │   ├── TalkableNPC.cs
│   │   │   └── ExaminableObject.cs
│   │   ├── Systems/
│   │   │   ├── GameManager.cs
│   │   │   ├── SceneTransition.cs
│   │   │   ├── InventoryManager.cs
│   │   │   └── ItemData.cs
│   │   └── UI/
│   │       ├── DialogueUI.cs
│   │       └── InventoryUI.cs
│   ├── Sprites/
│   │   ├── Characters/
│   │   ├── Tiles/
│   │   └── UI/
│   ├── Prefabs/
│   ├── Dialogue/
│   │   └── Hawk_Intro.yarn
│   ├── ScriptableObjects/
│   │   └── Items/
│   └── Audio/
│       ├── SFX/
│       └── Music/
├── Docs/
│   ├── Implementation_Plan.md (this file)
│   └── GDD_Gemini.md
├── ProjectSettings/
└── .gitignore
```

---

## Immediate Next Steps

1. [ ] Complete Ruby's Adventure Unity tutorial (2-3 hours)
2. [ ] Download scripts from Claude
3. [ ] Set up Unity project
4. [ ] Copy scripts into Assets/Scripts/
5. [ ] Get Mouse moving in a room

---

## Build Log

*Keep dated entries here as you progress. This becomes source material for the anthology.*

### [DATE] - Entry 1
*What you worked on, what the kids contributed, what broke, what surprised you.*

---

## Resources

- [Unity 2022.3 LTS Download](https://unity.com/releases/editor/qa/lts-releases)
- [Yarn Spinner Documentation](https://docs.yarnspinner.dev/)
- [Kenney Free Assets](https://kenney.nl/assets)
- [Ruby's Adventure Tutorial](https://learn.unity.com/project/ruby-s-2d-rpg)
- [Aseprite (Pixel Art)](https://www.aseprite.org/)
- [Piskel (Free Pixel Art)](https://www.piskelapp.com/)
