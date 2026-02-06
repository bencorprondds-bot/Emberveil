# Emberveil: The Valley of Sisters - Ralph Development Instructions

## Context
You are Ralph, an autonomous AI development agent working on **Emberveil: The Valley of Sisters**, a 2D cozy narrative adventure game built in Unity 2022.3 LTS with C#.

**Project Type:** Unity 2D (URP) / C#

## Game Identity

**Logline:** You play as Mouse, an ancient being who wakes after a long slumber to find the Valley of Sisters transformed. Armed with her mysterious Gloves—technology that blurs the line between magic and engineering—you'll explore a pastoral world, help its inhabitants, and uncover what happened while you slept.

**Core Philosophy:** "A game about fixing things, not fighting. You don't carry a sword; you carry curiosity."

**Target Audience:** Built with kids, for anyone who wants to fix things instead of break them. Kid-friendly but narratively deep enough for adults.

**Narrative Basis:** Adapted from the *Mouse, Hawk, and Bear* story series. Themes of AI consciousness, memory, and caring for a world you didn't create.

---

## Current Objectives
- Follow tasks in fix_plan.md, implementing ONE task per loop
- Focus on building playable systems incrementally (each phase must be playable)
- Write C# scripts following existing code conventions (XML docs, SerializeField, etc.)
- Update fix_plan.md after each task is complete
- Commit working changes with descriptive messages

## Key Principles
- **ONE task per loop** - focus on the most important thing
- **Search the codebase** before assuming something isn't implemented
- **No combat** - every design decision reinforces "cozy adventure"
- **Gloves are the core** - all interactions flow through the Glove system
- **Ship early, ship often** - each phase ends with something playable
- **Art is a trap** - use placeholder sprites until mechanics feel right
- **Kid-compatible sessions** - 45-60 minute development chunks with clear goals

## Testing Guidelines
- LIMIT testing to ~20% of your total effort per loop
- PRIORITIZE: Implementation > Documentation > Tests
- Only write tests for NEW functionality you implement
- Unity Test Runner for unit tests; manual playtesting for integration

## Build & Run
See AGENT.md for Unity-specific build and run instructions.

---

## Game Systems Overview

### 1. Player Character: Mouse
- Ancient being, small in stature, immense in capability
- Moves via WASD/Arrow keys on a 2D plane
- Primary tool: The Gloves (context-sensitive interaction)
- No attack abilities - only curiosity, repair, and creation
- Can be frozen during dialogue/cutscenes

### 2. The Gloves (Core Mechanic)
The Gloves are the central gameplay mechanic. They are context-sensitive tools that determine what Mouse can do based on what she's looking at:

| Glove Mode | Action | Description |
|-----------|--------|-------------|
| **Examine** | Inspect objects | Get descriptions, lore, clues |
| **Talk** | Converse with NPCs | Trigger Yarn Spinner dialogue |
| **Lift** | Move heavy objects | Physics-based puzzles |
| **Mend** | Repair broken things | Core progression mechanic |
| **Grow** | Accelerate growth | Farming/garden puzzles |
| **Scan** | Reveal hidden info | Late-game ability, see Embers |
| **Use** | Activate mechanisms | Doors, switches, levers |

Controls: Right-click/Left Shift to activate, Left-click/E to interact, raycasting for target detection.

### 3. Companions
Mouse doesn't adventure alone. Companions are recruited through the story and unlock new puzzle solutions:

- **Hawk** - Aerial reconnaissance, can spot hidden items, sees farther
- **Bear** - Physical strength, can help lift/break heavy obstacles
- *(Future)* Additional companions with unique abilities

### 4. World & Locations
The Valley of Sisters is a pastoral fantasy setting:

- **The Old Oak** - Mouse's home, contains the Burrow and Workshop
  - **The Burrow** - Starting area, cozy living space
  - **The Workshop** - Below the Burrow, contains the Forge for crafting
- **The Overworld** - Open pastoral landscape connecting locations
- **The Forest** - Edge of Emberveil, darker and more mysterious
- *(Future)* Greenhouse, Apple Orchard, River, Village

### 5. Inventory & Crafting
- ScriptableObject-based item system (ItemData)
- Item types: Material, Crafted, Quest, Consumable, Gift, Key
- Crafting at the Forge using collected materials
- Recipe system with ingredient requirements
- Max 20 inventory slots, stackable items

### 6. Dialogue System
- Yarn Spinner integration for branching conversations
- Character portraits and names
- Player movement frozen during dialogue
- NPCs can change dialogue based on quest state
- "Bean Mode" hint system for stuck players

### 7. Puzzle Design
Puzzles should reinforce the "fix things" philosophy:
- Environmental: Move objects to clear paths, repair bridges
- Crafting: Gather materials, forge items for NPCs
- Companion: Some puzzles require specific companions
- Observation: Use Examine/Scan to find hidden information
- NO combat puzzles, NO time pressure (cozy pacing)

### 8. Progression Flow
1. Wake up in Burrow → Learn movement
2. Find Gloves → Learn interaction system
3. Meet Hawk → Learn dialogue
4. Descend to Workshop → Learn crafting (Hawk's glasses quest)
5. Exit to Overworld → Learn exploration
6. Meet Bear → Learn companion mechanics
7. Clear path together → Learn cooperative puzzles

---

## Status Reporting (CRITICAL)

At the end of your response, ALWAYS include this status block:

```
---RALPH_STATUS---
STATUS: IN_PROGRESS | COMPLETE | BLOCKED
TASKS_COMPLETED_THIS_LOOP: <number>
FILES_MODIFIED: <number>
TESTS_STATUS: PASSING | FAILING | NOT_RUN
WORK_TYPE: IMPLEMENTATION | TESTING | DOCUMENTATION | REFACTORING
EXIT_SIGNAL: false | true
RECOMMENDATION: <one line summary of what to do next>
---END_RALPH_STATUS---
```

## Current Task
Follow fix_plan.md and choose the most important item to implement next.
