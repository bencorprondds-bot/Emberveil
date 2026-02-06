# Companion System Specification

## Overview
Mouse recruits friends who accompany her on adventures. Each companion has unique abilities that unlock new puzzle solutions and dialogue options.

## Design Principles
- Companions are **friends, not followers** - they have personality and agency
- They **comment on the world** - adding flavor dialogue as you explore
- They **unlock new solutions** - not just combat buffs, but genuine new ways to interact
- They **can refuse** - if something seems dangerous or wrong, they'll say so (cozy game reinforcement)

---

## Companions

### Hawk
- **Recruitment:** After crafting her glasses (first quest)
- **Visual:** Gray sprite, follows Mouse but perches on elevated objects when idle
- **Ability: Eagle Eye** - Can spot hidden items, see farther, identify objects from a distance
- **Puzzle use:** Spot hidden paths, read distant signs, find items in cluttered rooms
- **Personality in gameplay:**
  - Offers observations about surroundings
  - Nervous about going underground
  - Excited about high places
  - Methodical - comments on puzzle solutions before you attempt them

### Bear
- **Recruitment:** After dialogue at the river (Phase 5)
- **Visual:** Brown sprite, follows Mouse at a distance, larger hitbox
- **Ability: Strength** - Can help lift heavy objects, push large obstacles, carry more
- **Puzzle use:** Clear blocked paths (heavy logs, boulders), hold things in place
- **Personality in gameplay:**
  - Offers to help with everything (even when not needed)
  - Talks about food and the old days
  - Protective - positions himself between Mouse and anything unfamiliar
  - Enthusiastic but clumsy

---

## Technical Design

### CompanionManager.cs (Singleton)
```
Fields:
- activeCompanion: CompanionData (ScriptableObject)
- companionObject: GameObject (the companion in the scene)
- followDistance: float (how far behind they trail)
- isFollowing: bool

Methods:
- RecruitCompanion(CompanionData) - Add companion to roster
- SetActiveCompanion(CompanionData) - Switch active companion
- DismissCompanion() - Send companion home (they wait at their location)
- GetActiveCompanion() - Returns current companion or null

Events:
- OnCompanionRecruited
- OnCompanionChanged
- OnCompanionDismissed
```

### Follow Behavior
- Companion follows Mouse at `followDistance` (default 2 units)
- Smooth interpolation movement (no jittering)
- Stops when Mouse stops
- Has idle animations/behaviors when waiting
- Can be "parked" at specific locations

### Companion-Gated Puzzles
- `LiftableObject` already has `requiresCompanion` field
- Extend to support specific companion requirements: `requiredCompanionType`
- When player tries without the right companion, show contextual dialogue:
  - "This is too heavy for Mouse alone..." (needs Bear)
  - "I can't see what's up there..." (needs Hawk)

### Companion Dialogue
- Companions have ambient dialogue triggered by:
  - Entering new areas
  - Examining specific objects
  - Being idle for too long
  - Completing puzzles
- Implemented as short Yarn Spinner nodes, not full conversations

---

## Future Companions (Deferred)

### Bramblethorn (Rideable mount)
- Large friendly creature, possibly deer-like
- Can traverse difficult terrain
- Unlocks fast travel between known locations

### Bean (Hint System NPC)
- Not a traditional companion - appears contextually
- Pops up when player is stuck
- Offers gentle nudges, never solutions
