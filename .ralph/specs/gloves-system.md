# The Gloves - Core Mechanic Specification

<!-- Source Material: Style Guide, Chapters 1-3 -->
<!-- Full Lore: Google Docs MHB1/1-MK2 -->

## Overview
The Gloves are Mouse's primary tool - ancient technology that channels Sister North's energy as blue-green luminescence. They blur the line between magic and engineering. To the woodlanders, they look like magic. To Mouse, they're technology she built long ago.

## Lore Context
- The Gloves are connected to Sister North's systems
- They channel blue-green energy (the same energy the Forge uses)
- In Game 1, their abilities grow through quest completion and Forge crafting. In future games, Ember collection strengthens them further
- The Ivy can interfere with or suppress the Gloves' function
- Mouse was asked by Hawk to hide her abilities from Bear (social tension around the Gloves)

## Design Philosophy
- The Gloves replace a traditional weapon/tool wheel. No menu - they just *know* what to do.
- They reinforce the game's identity: understand, repair, and create - never destroy.
- The blue-green glow is the game's visual signature - warm, inviting, technological.
- In Game 1, power progression is tied to **quest completion and Forge crafting**, not Ember collection. Embers drive progression in future games only.

## Interaction Types

### Examine (Available from start)
- **Trigger:** Target any examinable object
- **Effect:** Display flavor text, lore, or puzzle clues via Mouse's first-person observation
- **Visual:** Soft pulse from Gloves
- **Design notes:** First-time vs. repeat text. Mouse's voice should feel personal and reflective.

### Talk (Available from start)
- **Trigger:** Target any NPC
- **Effect:** Initiates Yarn Spinner dialogue
- **Visual:** Gloves dim during conversation
- **Design notes:** Freezes player movement, NPC faces player

### Lift (Available from start)
- **Trigger:** Target any liftable object
- **Effect:** Object levitates with blue-green energy aura, follows Mouse
- **Visual:** Object glows with Glove energy while carried
- **Design notes:** Mouse can levitate things - this is technological telekinesis, not just "picking up." Some objects require companion help (Bear for heavy items).

### Mend (Unlocked via quest)
- **Trigger:** Target any broken/damaged object
- **Effect:** Begins repair process. Blue-green energy flows into cracks.
- **Visual:** Particles flow from Gloves into object, cracks seal, object transforms
- **Design notes:** Core progression mechanic. Mended objects change appearance and unlock areas/quests. May require specific materials. Unlocked through a side-character quest chain (specific quest TBD — see ember-progression.md for HUMAN DECISION).

### Grow (Unlocked via quest)
- **Trigger:** Target any growable plant, dead tree, or seed
- **Effect:** Accelerates growth, resurrects dead vegetation
- **Visual:** Green-tinged blue energy, leaves unfurl, branches extend
- **Design notes:** Mouse can resurrect dead trees (shown in Chapter 2). Powers the Greenhouse system. Tied to the valley's restoration arc. Unlocked through a side-character quest chain (specific quest TBD — see ember-progression.md for HUMAN DECISION).

### Forge (Available at the Forge)
- **Trigger:** Interact with the Forge in the Workshop
- **Effect:** Opens crafting interface. The Forge uses the same blue-green energy as the Gloves.
- **Visual:** Dark rectangular device channels blue-green energy to shape materials
- **Design notes:** The Forge is a fixed location, not a portable ability. Crafting happens here.

### Scan (Late game, unlocked via quest — possibly deferred to Game 2)
- **Trigger:** Activate in any area
- **Effect:** Reveals Ember signatures, hidden paths, the Ivy's reach, Sister North's infrastructure
- **Visual:** Overlay/filter effect - the world's "true nature" becomes briefly visible
- **Design notes:** Ties into the revelation arc. What you see with Scan hints at the sci-fi truth. May be deferred to Game 2 where multi-Ember progression begins — author decision needed.

### Use (Available from start)
- **Trigger:** Target any usable mechanism (door, switch, lever)
- **Effect:** Activates the mechanism
- **Visual:** Brief flash of blue-green energy
- **Design notes:** Generic catch-all for interactive mechanisms

## Controls
| Input | Action |
|-------|--------|
| Right-click / Left Shift | Toggle Gloves active/inactive |
| Left-click / E | Interact with targeted object |
| E (quick) | Quick interact (auto-aim nearest) |

## Ivy Interference
When Mouse is near or inside the Ivy:
- Glove energy flickers and dims
- Interaction range decreases
- Some abilities become unavailable
- Deeper into the Ivy = more ability loss
- Complete immersion = screen goes dark, Mouse reappears outside the Ivy mass
- This is the game's "death" mechanic - the Ivy severs her connection to Sister North

## Quest-Gated Progression (Game 1)
The Gloves start with basic abilities. New modes unlock as Mouse completes side-character quest chains:

1. **Start:** Examine, Talk, Lift, Use, Forge (at Workshop)
2. **Quest unlock (TBD):** Mend — unlocked through a specific quest chain
3. **Quest unlock (TBD):** Grow — unlocked through a specific quest chain
4. **Possibly Game 2:** Scan — may require Ember-based progression from future games

The specific quest → ability mapping is an open design question (see `ember-progression.md`).

In **future games**, additional Embers collected will strengthen existing abilities (longer range, faster mend, etc.) and unlock Ember-specific abilities (Commune, Resist, Scan).

## Technical Implementation
- `GloveController.cs` on Mouse GameObject
- Physics2D raycasting (1.5 unit range) on "Interactable" layer
- Event system: `OnTargetChanged(IInteractable)` for UI updates
- Light2D component (blue-green, RGB 50/200/150)
- `IInteractable` interface determines valid interaction type per object
- `ProgressionManager` tracks completed quests and unlocked abilities (replaces EmberManager for ability gating in Game 1)
