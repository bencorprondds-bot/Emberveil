# Companion System Specification

<!-- Source Material: Style Guide, Chapters 1-5, Character Sheets -->
<!-- Full Lore: Google Docs Character Sheets folder, The Elders subfolder -->

## Overview
Mouse recruits friends who accompany her on adventures. Each companion has unique abilities that unlock new puzzle solutions. They are friends with agency, not followers - they have opinions, refuse when something feels wrong, and comment on the world from their own perspective.

## Design Principles
- Companions are **friends, not tools** - they have personality and will disagree with you
- They **comment on the world** - adding flavor and their own perspective
- They **unlock new solutions** - genuine new ways to interact with the environment
- They **can refuse** - cozy game reinforcement; no one is forced into danger
- They reflect the theme of **community and mutual aid**
- Hawk specifically asked Mouse to **hide her abilities from Bear** - this creates social tension

---

## Companions

### Hawk (First Companion)
- **Recruitment:** After crafting her glasses (Chapter 3, first quest)
- **Visual:** Gray sprite (placeholder), perches on elevated objects when idle
- **Personality:** Scholarly, pragmatic, anxious, methodical. Careful planner.
  - Offers observations about surroundings
  - Nervous about going underground
  - Excited about high places
  - Comments on puzzle solutions before you attempt them
  - Respects the valley's social structures
- **Ability: Eagle Eye** - Spot hidden items, see farther, identify objects at distance
- **Puzzle use:** Find hidden paths, read distant signs, locate items in cluttered rooms
- **Key lore:** Hawk is an Elder (carries an Ember). This may not be revealed immediately.
- **Dialogue voice:** Short, precise sentences. Visual metaphors. Occasionally self-deprecating about her vision.

### Bear (Second Companion)
- **Recruitment:** After dialogue during winter storm arrival (Chapter 5)
- **Visual:** Brown sprite (placeholder), larger hitbox, follows at distance
- **Personality:** Young, endearing, earnest, enthusiastic but clumsy.
  - Offers to help with everything (even when not needed)
  - Talks about food constantly
  - Protective - positions himself between Mouse and anything unfamiliar
  - Learning as he goes - he's a yearling, still figuring things out
  - Doesn't know about Mouse's full abilities (Hawk asked Mouse to hide them)
- **Ability: Strength** - Help lift heavy objects, push large obstacles, carry more
- **Puzzle use:** Clear blocked paths (heavy logs, boulders), hold things in place
- **Dialogue voice:** Warm, slightly rambling. Food metaphors. Asks questions. Encouraging.

---

## Technical Design

### CompanionManager.cs (Singleton)
```
Fields:
- activeCompanion: CompanionData (ScriptableObject)
- companionObject: GameObject
- followDistance: float (default 2 units)
- isFollowing: bool
- companionRoster: List<CompanionData> (recruited companions)

Methods:
- RecruitCompanion(CompanionData)
- SetActiveCompanion(CompanionData)
- DismissCompanion()
- GetActiveCompanion()

Events:
- OnCompanionRecruited
- OnCompanionChanged
- OnCompanionDismissed
```

### Follow Behavior
- Companion follows Mouse at `followDistance` (2 units)
- Smooth interpolation movement
- Stops when Mouse stops, has idle behaviors
- Can be "parked" at specific locations
- Hawk perches on elevated surfaces; Bear stands near Mouse

### Companion-Gated Puzzles
- `LiftableObject` has `requiresCompanion` field (existing)
- Extend with `requiredCompanionType` for specific companion needs
- Contextual dialogue when wrong/no companion:
  - "This is too heavy for Mouse alone..." (needs Bear)
  - "I can't see what's up there..." (needs Hawk)

### Companion Dialogue
Ambient dialogue triggered by:
- Entering new areas
- Examining specific objects
- Being idle too long
- Completing puzzles
- Story events (Ivy encounters, Fox sightings, Ember discoveries)

---

## Future Companions (Deferred)

### Bramblethorn
- Elder character, currently in slumber (Ivy-related)
- Once awakened: rideable mount, traverse difficult terrain
- Unlocks fast travel between known locations

### Bean (Hint System)
- Not a traditional companion - appears contextually
- Pops up when player is stuck (60+ seconds)
- Gentle nudges, never solutions
- Small rabbit kit, cheerful and scattered

### Other Valley Characters
- 25+ supporting characters exist in the lore (see Character Sheets in Google Docs)
- Many could become quest-givers, temporary companions, or story NPCs
- Full roster documented in `.ralph/docs/DOCUMENT_CATALOG.md`

### Key NPCs from Chapters (potential quest-givers, story characters)
- **Tiny** - Raccoon, runs the Ringtail Tavern. Social hub for the valley. (Ch 8)
- **Daisy & Diggory** - Badger innkeepers. Community fixtures. (Ch 8)
- **Dr. Shellby** - Tortoise therapist/Elder. Wise, slow, deliberate. (Ch 8)
- **Owl** - Outsider who arrives with a creation myth. Fox whispers to Owl. (Ch 8-9)
- **Clover** - Rabbit matriarch of the Westwatch community. (Ch 10, 12)
- **Rus (Von Appleseed)** - Mad Squirrel King. Corrupted Ember. Grief-driven tyrant. (Ch 11, 13, 21, 26)
- **Autumn** - Rus's compassionate sister. Potential ally for diplomacy. (Ch 11, 21)
- **Garolus & Carolus** - Guides/mediators in the orchard conflict. (Ch 21)
- **Honeybadger glassmakers** - Artisan community, help build Westwatch. (Ch 12)
- **Rue** - Shrew librarian. Custodian of the Library. (Ch 16)
- **Rowan** - Zora's scribe. Holds crucial historical records about Zora's death. (Ch 24-25)
- **Willow** - Seer figure. Gives key prophecy. Zora sought Willow before dying. (Ch 25)
- **Mullvad** - Mole community. Help with greenhouse construction. (Ch 6)
- **Driftwoods** - Beaver family. Community builders. (Ch 6)
