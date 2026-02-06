# The Gloves - Core Mechanic Specification

## Overview
The Gloves are Emberveil's central gameplay mechanic. They are ancient technology that blurs the line between magic and engineering. Mouse wears them, and they respond contextually to whatever she's looking at.

## Design Philosophy
- The Gloves replace a traditional weapon/tool wheel. There's no menu - the Gloves just *know* what to do.
- They reinforce the game's identity: you don't destroy, you understand, repair, and create.
- The blue-green glow is the game's visual signature - it should feel warm and inviting, not threatening.

## Interaction Types

### Examine (Available from start)
- **Trigger:** Target any examinable object
- **Effect:** Display flavor text, lore, or puzzle clues
- **Visual:** Soft pulse from Gloves
- **Design notes:** First-time vs. repeat examination text supported

### Talk (Available from start)
- **Trigger:** Target any NPC
- **Effect:** Initiates Yarn Spinner dialogue
- **Visual:** Gloves dim during conversation
- **Design notes:** Freezes player movement, NPC faces player

### Lift (Available from start)
- **Trigger:** Target any liftable object
- **Effect:** Object floats above Mouse, follows her movement
- **Visual:** Object has subtle glow while carried
- **Design notes:** Some objects require companion (Bear) to lift. Drop on release or Glove deactivation.

### Mend (Unlocked in Phase 4)
- **Trigger:** Target any broken/damaged object
- **Effect:** Begins repair process (may require materials)
- **Visual:** Particles flow from Gloves into object, cracks seal
- **Design notes:** Core progression mechanic. Mended objects change appearance and may unlock areas or quests.

### Grow (Unlocked in Phase 5)
- **Trigger:** Target any growable plant or seed
- **Effect:** Accelerates growth to next stage
- **Visual:** Green particles, leaves unfurl
- **Design notes:** Powers the greenhouse/garden system. Some plants need specific conditions.

### Scan (Late game)
- **Trigger:** Activate in any area
- **Effect:** Reveals hidden information, Ember signatures, secret paths
- **Visual:** Overlay/filter effect on screen
- **Design notes:** Deferred to future development. Ties into deeper Emberveil lore.

### Use (Available from start)
- **Trigger:** Target any usable mechanism (door, switch, lever)
- **Effect:** Activates the mechanism
- **Visual:** Brief flash from Gloves
- **Design notes:** Generic catch-all for interactive mechanisms.

## Controls
| Input | Action |
|-------|--------|
| Right-click / Left Shift | Toggle Gloves active/inactive |
| Left-click / E | Interact with targeted object |
| E (quick) | Quick interact (auto-aim nearest) |

## Technical Implementation
- `GloveController.cs` on Mouse GameObject
- Physics2D raycasting (1.5 unit range) on "Interactable" layer
- Event system: `OnTargetChanged(IInteractable)` for UI updates
- Optional Light2D component (blue-green, RGB 50/200/150, intensity 0.5)
- `IInteractable` interface determines valid interaction type per object

## Progression
The Gloves start with basic abilities and gain new modes as the story progresses:
1. **Start:** Examine, Talk, Lift, Use
2. **Phase 4:** Mend (after discovering the Workshop's purpose)
3. **Phase 5:** Grow (after restoring the Greenhouse)
4. **Future:** Scan (deep lore unlock)
