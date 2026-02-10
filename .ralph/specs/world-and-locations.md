# World & Locations Specification

<!-- Source Material: Style Guide, Chapters 1-6, Locations folder in Google Docs -->
<!-- Full Lore: https://drive.google.com/drive/folders/1hcnJ_ci-UJq-IYGOv7-Ixrz7ea1u9SK2 -->

## The Valley of Sisters - Overview

A vast valley nestled between mountains, maintained by the Three Sisters (supercomputers). On the surface: a pastoral fantasy world of talking animals, gentle seasons, and community. Beneath: a post-climate-catastrophe preservation system keeping humanity alive in stasis.

The Emberveil forest acts as a sentient guide, leading travelers where they "need" to go. The valley's infrastructure is slowly failing as Sister North remains in her coma. The Ivy encroaches. The Embers are scattered. Mouse must restore what was broken.

## Art Direction — HD-2D Style
- **Visual Style:** HD-2D (Octopath Traveler / Triangle Strategy inspired). 3D environments with 2D pixel-art character sprites rendered as billboarded quads in 3D space.
- **Environments:** 3D geometry (ProBuilder or modular meshes) with pixel-art textures, point-filtered (no bilinear smoothing). Terrain, walls, structures, and props are all 3D.
- **Characters:** 2D pixel-art sprites on billboarded quads that always face the camera. 32x32 base resolution for characters.
- **Camera:** Fixed ~30-45° tilt angle, orthographic or low-FOV perspective. Slight depth of field to separate foreground/background.
- **Palette:** Warm earth tones (greens, browns, golds) with blue-green accent from the Gloves/Embers.
- **Mood:** Cozy, inviting, slightly melancholic - like a garden that's been waiting for someone to return.
- **Lighting:** URP 3D lighting with post-processing. Bloom for Glove glow and Ember luminescence. Warm point lights for interiors, directional light for outdoors. Volumetric light shafts through trees and windows.
- **Post-Processing:** Bloom (blue-green Glove signature), depth of field (tilt-shift feel), vignette (subtle), color grading (warm).
- **Scale:** Zelda: Echoes of Wisdom-inspired. Vast, explorable overworld. Not a small tile room.

## Game Scale
The game overview doc specifies a "giant, to-scale map" with diverse biomes:
- Mountains, forests, rivers, fields, underground areas
- Ability-gated progression (Ember unlocks open new regions)
- Each area should feel like a meaningful discovery

---

## Locations

### The Old Oak
Mouse's ancient home. A massive oak tree shaped (not built) into a living structure. It responds to Mouse's presence - produces supernatural warmth, awakens from dormancy when she returns. Through it, Mouse can (eventually) commune with Sister North.

#### The Burrow (Starting Area)
- **Size:** 20x15 tiles initially, expandable as story progresses
- **Feel:** Warm, cluttered, cozy. Mouse and Hawk's home.
- **Contents:** Sleeping area, bookshelf, broken shelf (mendable), window, stairs down to Workshop, door to outside
- **Story:** Mouse and Hawk dig this out together (Chapter 2). It's their first act of creation.
- **Lighting:** Warm amber point lights, the Old Oak's supernatural warmth. Bloom on warm light sources.
- **Music:** Gentle, intimate, music-box quality

#### The Workshop
- **Size:** ~25x20 unit footprint, circular layout (3D geometry)
- **Feel:** Industrial-magical. Ancient technology wrapped in living wood.
- **Contents:** Spiral staircase from Burrow, the Forge (dark metallic structure, channels blue-green energy), workbenches, tool rack, dusty drawers (contain Lenses), metal scraps (contain Frames)
- **Story:** Where Mouse crafts Hawk's glasses (Chapter 3). Reveals Mouse's true nature - she built this.
- **Lighting:** Dim with Forge glow (blue-green point light + bloom), contrasts with warm Burrow above
- **Music:** Low hum, occasional metallic tones, technological undertone

### The Mountain Cave (Opening Area)
- Where Mouse wakes up at the start of the game
- Cold, dark, disorienting - the "emergence from coma" feeling
- Brief area - descent from cave to valley is the game's opening sequence
- Mouse encounters Fox briefly during descent

### The Overworld
- **Scale:** Vast - Zelda-style exploration map
- **Biomes:** Meadows, river, forest edge, orchards, mountains in distance
- **Key features:**
  - The Old Oak (building, central hub)
  - The River (geographic feature, redwood bridge, Bear fishes here)
  - The Orchards (Apple Orchard with the Ancestral Apple Tree)
  - Forest paths leading to the Emberveil
  - Scattered broken things (fences, signposts, paths) showing decay
  - Ivy patches blocking certain paths (ability-gated)
- **NPCs:** Various woodlander species - wolves, goats, shrews, moles, crows, squirrels, beavers, raccoons, rabbits
- **Lighting:** Bright daylight, dappled shadows, seasonal changes
- **Music:** Open, pastoral, recorder/flute, birdsong

### The Greenhouse (Chapter 6)
- Growing facility connected to the Old Oak's systems
- Where the Grow ability is most useful
- Restoration of the Greenhouse is a major progression milestone
- Plants, seeds, and growth mechanics centered here

### The Ringtail Tavern
- Community gathering place, run by "Tiny"
- Social hub for woodlanders
- Source of quests, rumors, and community events
- Warm, busy, full of personality

### The Library
- Seemingly sentient structure within the Emberveil
- Produces books based on a reader's thoughts
- Keeps records of all woodlanders (except Fox and pre-Woodlander history)
- Contains Rue's Ember somewhere in its depths
- A place of mystery and knowledge

### Westwatch Warren (Chapter 12)
- Rabbit community construction project
- Community-building mechanics
- Connected to Clover's family and the rabbit characters

### The Queen's Chamber (Chapter 13)
- Underground area
- Connected to the rabbit warren storyline

### The Haunted Rocks (Chapter 15)
- **THE KEY IVY LOCATION**
- Ruins of New Haven - an old human city
- Completely consumed by the Ivy
- Senses are warped, time behaves strangely
- Fox has given up fighting the Ivy here
- Makes Mouse deeply sad - she lost friends, students here
- **Game function:** Tutorial for the Ivy severance mechanic. The player experiences progressive sensory loss for the first time.
- This is where the game's tone shifts from cozy to something deeper

### The Emberveil Forest
- The sentient forest that guides travelers
- Leads you where you "need" to go, not where you "want" to go
- Tied to Fox's systems
- Beautiful but unpredictable
- Contains hidden paths, Ember locations, and deeper mysteries

### Otterdam / Felmere (Chapter 7)
- Water management system in the valley
- Fox once appeared here to repair the dam - one of Fox's rare direct interventions
- Connected to the Mere and the river network
- Important geography established during the Thanksgiving map-drawing

### The Ancestral Apple Tree / Aetherbloom (Chapters 10-11, 21-22)
- Massive ancient tree in the orchards
- Contains a corrupted Ember (Zora's Ember) - cold, black, with sickly filaments
- Guarded by Rus (Von Appleseed) and the squirrels
- Mouse's gloves react strongly to it - connects to Sister North ("0.01%")
- A key Ember location the player must reach and reckon with
- The "Fire and Ice" chapter describes the Ember igniting/burning this tree

### The Three Sisters (Mountains)
- Visible on the horizon - three mountain peaks
- The player doesn't know they're supercomputers until late in the story
- Sister North is the focus - her "garden" core is beneath/within the mountain
- Visual landmark that gains meaning as lore is revealed
- Mouse is "Mountainborn" - crafted from mountain stone (i.e., created by Sister North)

---

## Scene Transition Design

### Rules
- Fade to black (0.5s out, 0.5s in) for interior transitions
- Seamless for overworld exploration (no loading screens)
- Named SpawnPoints in destination scenes
- Transitions via trigger zones (doors, stairs) or interaction

### Connections (Vertical Slice)
```
Mountain Cave → Valley (opening sequence, one-way)
Burrow ←→ Workshop (stairs)
Burrow ←→ Overworld (door)
Overworld → Various locations (paths, doors)
```

## Environmental Storytelling
- Broken objects tell the story of Sister North's failing systems
- Repaired objects show the valley recovering
- Ivy growth shows the corruption spreading
- NPCs react to changes Mouse makes
- The Old Oak grows brighter as Embers are collected
- Seasonal progression mirrors the story's emotional arc (winter → spring → summer)
