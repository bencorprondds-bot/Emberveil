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

## Party System (Multi-Companion)

### Core Rule
**Hawk and Bear are ALWAYS present** (unless the story specifically separates them). The player does not choose between them - the trio travels together, matching the book.

### Unlockable Companions
Additional characters join the party based on completed **side-quest chains**:

| Companion | Unlock Condition | Ability |
|-----------|-----------------|---------|
| **Bramblethorn** | Find and awaken the slumbering Elder | Rideable mount, traverse difficult terrain, fast travel |
| **Autumn** | Complete orchard diplomacy questline | Squirrel agility, tree navigation, Rus mediation |
| **Rue** | Complete Library questline | Knowledge/lore access, catalogue searches |
| **Bean** | Rescue from Haunted Rocks + follow-up quests | Hint system, small-space access |

### Final Confrontation Scaling
During the climactic confrontation with Sciurus von Appleseed:
- The more Woodlanders you've helped throughout the game, the more allies join you
- More allies = **more story content** (additional dialogue, scenes, character moments)
- More allies = **better outcomes for Mouse** (less harm during confrontation)
- Mouse's condition at the end of Book 1 **carries into the sequel**
- This creates meaningful replay value and rewards thorough exploration

### Technical Design

#### PartyManager.cs (Singleton)
```
Fields:
- coreCompanions: List<CompanionData> (Hawk, Bear - always present)
- unlockedCompanions: List<CompanionData> (earned through quests)
- activeParty: List<CompanionData> (currently following Mouse)
- alliedWoodlanders: List<string> (NPCs helped - affects final confrontation)
- followDistance: float (default 2 units)

Methods:
- RecruitCompanion(CompanionData) - Add to unlockedCompanions
- AddToParty(CompanionData) / RemoveFromParty(CompanionData)
- GetPartyMembers() - Returns all active companions
- HasCompanion(CompanionType) - Check if specific companion is available
- RegisterAlly(string npcId) - Track helped Woodlanders
- GetAllyCount() - For final confrontation scaling

Events:
- OnCompanionRecruited
- OnPartyChanged
- OnAllyRegistered
```

### Follow Behavior
- Multiple companions follow Mouse in formation
- Hawk perches on elevated surfaces; Bear walks near Mouse
- Unlockable companions have unique idle behaviors
- Companions can be "parked" at specific locations for story reasons
- Party spreads out when idle, clusters when moving

### Companion-Gated Puzzles
- Some puzzles require specific companions:
  - "This is too heavy for Mouse alone..." (needs Bear)
  - "I can't see what's up there..." (needs Hawk)
  - Specific unlockable companions open optional paths/puzzles
- Contextual dialogue when wrong/missing companion

### Companion Dialogue
Ambient dialogue triggered by:
- Entering new areas
- Examining specific objects
- Being idle too long
- Completing puzzles
- Story events (Ivy encounters, Fox sightings, Ember discoveries)
- Companions comment on EACH OTHER - Hawk/Bear banter, reactions to new party members

---

## Companion Details

### Bramblethorn (Unlockable)
- Elder character, currently in slumber beneath Ivy (Ch 18)
- Once awakened: rideable mount, traverse difficult terrain
- Unlocks fast travel between known locations
- 15-foot boar - dramatic visual presence in party

### Bean (Unlockable / Hint System)
- Appears contextually when player is stuck (60+ seconds)
- Gentle nudges, never solutions
- Small rabbit kit, cheerful and scattered
- Full companion after rescue + follow-up quests

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
