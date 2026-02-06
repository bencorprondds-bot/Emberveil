# World & Locations Specification

## The Valley of Sisters - Overview

A pastoral fantasy valley nestled between gentle mountains. Once thriving, now showing signs of neglect and decay. The Embers (floating motes of light representing life energy) are dim but not gone. The world feels like it's holding its breath, waiting to be fixed.

## Art Direction
- **Style:** Pixel art, 32x32 tile base
- **Palette:** Warm earth tones (greens, browns, golds) with blue-green accent from the Gloves
- **Mood:** Cozy, inviting, slightly melancholic - like a garden after a long rain
- **Lighting:** URP 2D lighting. Warm interiors, soft outdoor light, Glove glow as key visual

---

## Locations

### The Old Oak
Mouse's ancient home. A massive oak tree that has been shaped (not built) into a living structure. Contains multiple levels.

#### The Burrow (Starting Area)
- **Size:** 20x15 tiles
- **Feel:** Warm, cluttered, cozy - like a hobbit hole crossed with a workshop
- **Contents:**
  - Mouse's bed/sleeping area
  - Bookshelf (examinable - lore)
  - Broken shelf (mendable - tutorial)
  - Window overlooking the valley (examinable)
  - Stairs down to Workshop
  - Door to outside (initially blocked or locked)
  - Hawk NPC perched on a chair
- **Lighting:** Warm amber, fireplace glow
- **Music:** Gentle, intimate, music-box quality

#### The Workshop
- **Size:** ~25x20 tiles, circular layout
- **Feel:** Industrial-magical - gears and vines intertwined
- **Contents:**
  - Spiral staircase entrance from Burrow
  - The Forge (central crafting station)
  - Three workbenches (examine for materials/lore)
  - Tool rack (examinable)
  - Dusty drawer (contains Lenses)
  - Metal scraps on workbench (contains Metal Frames)
  - Broken pipe (mendable, future quest)
- **Lighting:** Dim with Forge glow (orange/red), Glove glow contrasts nicely
- **Music:** Low hum, occasional metallic tones, mysterious

### The Overworld
- **Size:** ~50x50 tiles (expandable)
- **Feel:** Open, pastoral, breathable after the tight interiors
- **Layout:**
  - The Old Oak (building, entrance on south side)
  - Grass meadow (center)
  - River running east-west (visual barrier, future bridge quest)
  - Forest edge to the north (dark trees, mysterious)
  - Path system connecting locations
  - Bear found near river, fishing
  - Scattered broken things (fences, signposts) for mend practice
- **Lighting:** Bright daylight, dappled shadows under trees
- **Music:** Open, pastoral, recorder/flute lead, birdsong ambient

### The Forest Edge (Future)
- Transition from pastoral to mysterious
- Darker palette, taller trees, less light
- Where the Embers are weakest
- Leads to deeper narrative content

### The Greenhouse (Future)
- Glass structure overgrown with dead vines
- Grow ability restores it
- Farming/garden mechanics hub
- Seasonal plants and ingredients

### Apple Orchard (Future)
- Bear's domain
- Stealth-adjacent mechanics (avoiding startled animals?)
- Harvest festival connection

### The Village (Future)
- Valley inhabitants
- Quest hub with multiple NPCs
- Shops, trades, social space

---

## Scene Transition Design

### Rules
- Every scene transition has a **fade to black** (0.5s fade out, 0.5s fade in)
- Player spawns at named **SpawnPoints** in destination scene
- Transitions triggered by either:
  - Walking into a trigger zone (doors, stairs)
  - Interacting with a transition object
- No loading screens beyond the fade - keep it seamless

### Connections
```
Burrow ←→ Workshop (stairs)
Burrow ←→ Overworld (door)
Overworld ←→ Forest Edge (path)
Overworld ←→ Greenhouse (door)
Overworld ←→ Village (path)
```

## Environmental Storytelling
- Broken objects tell the story of what went wrong during Mouse's slumber
- Repaired objects show the valley recovering
- NPCs comment on changes Mouse has made
- The Embers grow brighter as more is restored (global lighting system, future)
