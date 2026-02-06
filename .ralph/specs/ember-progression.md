# Ember / Seeds Progression System

<!-- Source Material: "The Embers: A Core Mechanic of the Emberveil" doc, Style Guide -->
<!-- Full Lore: Google Docs MHB1/1-MK2 -->

## Overview
Embers (also called Seeds) are fragments of Sister North's quantum processing core (QPUs), scattered across the valley by Fox. They are the game's primary progression currency - each one collected grants Mouse new abilities or strengthens existing ones, gating access to new areas in Zelda-style fashion.

## Lore Context
- Sister North's core was a garden of technobiologic quantum processing units
- Fox scattered them to put Sister North into a "coma"
- The Elders each carry an Ember, linking them to valley infrastructure
- Embers grant consciousness-related abilities to those who carry them
- Embers near unbound/corrupted entities can cause problems (the Mad Squirrel King)
- Mouse can hear Sister North through the Embers if she's close enough
- As Mouse collects more Embers, her connection to Sister North strengthens

## Game Design: Zelda-Style Ability Gating

### Core Loop
1. Player explores accessible areas
2. Discovers an Ember (through puzzle, quest, story event, or Elder interaction)
3. Collecting the Ember grants a new ability or upgrades an existing one
4. New ability unlocks previously inaccessible areas
5. New areas contain more Embers, NPCs, quests, and story content
6. Repeat with increasing narrative depth

### Ability Unlock Sequence (Tentative)

| Ember # | Ability Unlocked | Areas It Opens |
|---------|-----------------|----------------|
| Start | Examine, Talk, Lift, Use | Burrow, Workshop, immediate surroundings |
| 1 | **Mend** (repair broken objects) | Broken bridges, collapsed paths, damaged structures |
| 2 | **Grow** (resurrect plants, accelerate growth) | Greenhouse, overgrown areas, blocked forest paths |
| 3 | **Strengthen** (enhanced Lift, move heavier objects) | Heavy obstacles, boulder-blocked caves |
| 4 | **Commune** (sense nearby Embers, hear Sister North faintly) | Hidden Ember locations, secret areas |
| 5 | **Resist** (push slightly into Ivy without full severance) | Shallow Ivy zones, partially consumed areas |
| 6+ | **Scan** (see the world's true nature briefly) | Deepest lore, final areas, Haunted Rocks interior |

### Ember Sources
Embers are found through various means:
- **Environmental puzzles** - Hidden in hard-to-reach places, revealed by solving puzzles
- **Quest rewards** - NPCs or Elders give Embers after significant help
- **Elder interactions** - Some Elders willingly share their Ember; others need convincing
- **Ivy clearance** - Clearing Ivy patches may reveal buried Embers
- **Story events** - Major narrative moments culminate in Ember discovery
- **Fox encounters** - Fox may leave an Ember behind after a cryptic visit

### Collection Feedback
When Mouse collects an Ember:
- Major visual event: blue-green energy surge, the Ember integrates into the Gloves
- Sister North's voice becomes slightly clearer (audio cue)
- The Old Oak glows brighter (if in range)
- Mouse's internal monologue: a memory fragment, a feeling, a piece of the past
- New ability is demonstrated in a brief, guided moment
- The world subtly changes (lighting, ambient sound, NPC reactions)

### Strengthening Existing Abilities
Beyond unlocking new modes, Embers passively improve existing abilities:
- **Lift** range and weight capacity increase
- **Mend** speed increases, can repair more complex objects
- **Grow** can affect larger areas, more types of plants
- **Glove light** becomes brighter and reaches farther
- **Interaction range** grows (raycasting distance)

## Connection to Sister North
The Ember count represents Mouse's restoration of Sister North:
- 0 Embers: Sister North is in a coma. Mouse is alone with her Gloves.
- Early Embers: Faint whispers, vague feelings. Mouse senses *something*.
- Mid Embers: Mouse can hear Sister North during meditation under the Old Oak.
- Late Embers: Full communion possible. Mouse understands the scope of what happened.
- All Embers: Sister North is restored. The game's climactic revelation.

This progression IS the gradual lore revelation - as Mouse's connection strengthens, the player learns more about the true nature of the world.

## Technical Implementation

```
EmberManager.cs (Singleton)
- embersCollected: int
- totalEmbers: int (set per chapter/game)
- unlockedAbilities: List<GloveAbility>
- emberLocations: Dictionary<string, bool> (track which Embers are found)

Methods:
- CollectEmber(string emberId) - Add Ember, unlock ability if threshold met
- HasAbility(GloveAbility ability) - Check if ability is unlocked
- GetEmberCount() - Current count
- GetConnectionStrength() - Float 0-1 representing Sister North connection

Events:
- OnEmberCollected(int newTotal)
- OnAbilityUnlocked(GloveAbility ability)
- OnConnectionStrengthChanged(float strength)

Integration:
- GloveController reads EmberManager to determine available abilities
- IvyZone reads EmberManager to determine resistance level
- DialogueManager reads EmberManager to unlock lore dialogue
- AudioManager reads EmberManager to adjust Sister North audio presence
```

## Balance Notes
- Embers should be discoverable through exploration, not grinding
- No Ember should be permanently missable
- The player should always have something to do even without the "next" Ember
- Ability unlocks should feel earned but not frustrating
- The connection-to-Sister-North arc should feel emotional, not mechanical
