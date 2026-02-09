# World & Locations Specification

<!-- Source Material: Style Guide, Chapters 1-26, Locations folder in Google Docs -->
<!-- Full Lore: https://drive.google.com/drive/folders/1hcnJ_ci-UJq-IYGOv7-Ixrz7ea1u9SK2 -->
<!-- Last sync with Google Docs: 2026-02-08 -->

## The Valley of Sisters - Overview

A vast valley nestled between mountains, maintained by the Three Sisters (supercomputers). On the surface: a pastoral fantasy world of talking animals, gentle seasons, and community. Beneath: a post-climate-catastrophe preservation system keeping humanity alive in stasis.

The Emberveil forest acts as a sentient guide, leading travelers where they "need" to go. The valley's infrastructure is slowly failing as Sister North remains in her coma. The Ivy encroaches. The Embers are scattered. Mouse must restore what was broken.

## Art Direction
- **Style:** Pixel art, 32x32 tile base
- **Palette:** Warm earth tones (greens, browns, golds) with blue-green accent from the Gloves/Embers
- **Mood:** Cozy, inviting, slightly melancholic - like a garden that's been waiting for someone to return
- **Lighting:** URP 2D lighting. Warm interiors, soft outdoor light. Glove glow as visual signature.
- **Scale:** Zelda: Echoes of Wisdom-inspired. Vast, explorable overworld. Not a small tile room.

## Valley Geography
The valley is **diamond-shaped**, open at the south, closed by a wall. North of the wall = the valley. South of the wall = farmland inhabited by a few hundred subsistence farmers.

The three largest mountains form the corners: Sister North (north), Sister West (southwest), Sister East (southeast). The valley contains meadows, forests (the Emberveil), rivers, orchards, underground systems, and multiple settlements.

### The Cooling System (Critical Infrastructure)
The valley's water system IS the Three Sisters' cooling system:
1. **The Cistern** (deep underground) — origin point. The Leviathan manages water levels
2. Water flows UP through the mountains, cooling their internal processors
3. Water exits the mountains as **ice** on the peaks and as the **River** flowing downhill
4. Water collects in **reservoirs and lakes** (The Mere, other lakes) where it cools
5. Water re-enters the Cistern at the base of the wall, beyond the farmlands
6. Cycle repeats: Cistern → Mountains → Ice/River/Lakes → Cistern

The River is NOT a natural waterway — it is the cooling system's effluent.

## Game Scale
The game overview doc specifies a "giant, to-scale map" with diverse biomes:
- Mountains, forests, rivers, fields, underground areas
- Ability-gated progression (quest-driven unlocks open new regions)
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
- **Lighting:** Warm amber, the Old Oak's supernatural warmth
- **Music:** Gentle, intimate, music-box quality

#### The Workshop
- **Size:** ~25x20 tiles, circular layout
- **Feel:** Industrial-magical. Ancient technology wrapped in living wood.
- **Contents:** Spiral staircase from Burrow, the Forge (dark rectangle, channels blue-green energy), workbenches, tool rack, dusty drawers (contain Lenses), metal scraps (contain Frames)
- **Story:** Where Mouse crafts Hawk's glasses (Chapter 3). Reveals Mouse's true nature - she built this.
- **Lighting:** Dim with Forge glow (blue-green), contrasts with warm Burrow above
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
- Rabbit community construction project, connected to Clover's family
- A hill that will be progressively hollowed out as new inhabitants arrive
- **Starts Book 1:** Rabbits only
- **Grows incrementally:** Moles, crows, a lone beaver — one new group per book
- Each group gets their own nook in the hill (modular architecture)
- **End state (across series):** Completely hollowed hill with a central courtyard/agora
- Grows in narrative importance across books — someone will eventually threaten it (possibly the Ivy)
- Community-building mechanics

### The Queen's Chamber (Chapter 13)
- Underground area beneath Westwatch Warren
- Connected to the rabbit warren storyline

### The Haunted Rocks (Chapter 15)
- **THE KEY IVY LOCATION**
- Ruins of New Haven — an old human city at the head of the river where it exits Sister North
- One of three cities that existed at the base of each Sister mountain (headquarters for engineers, scientists, politicians who built the Sisters)
- Woodlanders see it as an unusual rock formation — very geometric, tall rocks
- Completely consumed by the Ivy
- Senses are warped, time behaves strangely
- Fox has given up fighting the Ivy here
- Makes Mouse deeply sad — she lost friends, students here
- **Game function:** Tutorial for the Ivy severance mechanic. The player experiences progressive sensory loss for the first time
- This is where the game's tone shifts from cozy to something deeper
- Strategically located at Sister North's river exit — a chokepoint the Ivy controls

### The Emberveil Forest
- The sentient forest — biomechanical communications array disguised as a living forest
- Leads you where you "need" to go, not where you "want" to go
- Tied to Fox's systems
- Beautiful but unpredictable
- Contains hidden paths, Ember locations, and deeper mysteries
- **Two zones:** Emberveil Shallows (accessible, lighter) and Emberveil Deep (dangerous, darker)
- **Known residents:** Bramblethorn (Ancient Hollow, a Douglas-Fir trunk), Bear, Queen Bee Hyleae, Wolfmother
- Contains the Loaming Ponds deep within

### Cedartown
- A village in the Emberveil canopy — bustling crossroads between Emberveil Deep and Emberveil Shallows
- **Upper level (canopy):** Squirrels, raccoons, birds — arboreal species
- **Lower level (ground):** Boars, shrews, wolves, deer — ground species
- Diverse, well-populated settlement — natural waypoint for the player
- Potential hub for quests, fast-travel, or commerce

### Otterdam / Felmere (Chapter 7)
- Dam and water management system in the valley
- **Otterdam separates two bodies of water:**
  - **Leafmere** = the part of the lake at the base of Sister West, exposed to the sun
  - **Fellmere** = the part under Sister West / inside the mountain
  - Both are effectively the same lake ("The Mere" / "Leafmere Lake"), divided by the dam
  - Both cover a submerged human city from before stasis
- Fox once appeared here to repair the dam — one of Fox's rare direct interventions
- Beavers maintain the dam; muskrat families help patch holes beavers can't reach
- Connected to the Mere and the river network
- The Leviathan directs water to the spring that feeds the Fellmere

### The Mere / Leafmere Lake
- Large lake at one of the lowest points in the valley, hugging the base of **Sister West**
- "The Mere," "Leafmere Lake," and "Leafmere" all refer to the same body of water (Otterdam divides it into Leafmere and Fellmere)
- Sits over an **abandoned human city** (undisclosed lore)
- **Fed by the Leviathan** from the cistern deep below — NOT by the river (hidden lore, revealed several books in)
- Prone to flooding in heavy rain years
- **Muskrat families** run the only boat transport on the lake — two families who bicker constantly over territorial control
  - Pirate-like/sailor-like speech patterns
  - Help the beavers at the dam by patching holes
  - Host a **New Moon Market** attended by almost all Woodlanders
- On the NW corner: The Archenreach Academy (accessible only by boat or through the mountain)
- On the western bank: steep scalloped rock cliffs at the base of Sister West
- **New Year's Festival:** Annual celebration at The Mere marking the end of winter. Woodlanders place remembrances (small gifts) in little boats made of woven reeds/willow by kits, with elders helping. Celebrates differences and shared identity.
- **Fox's Origin:** When Fox first emerged from Sister North during a continent-spanning hurricane, the old cooling reservoir dam broke, flooding the valley. Fox rebuilt the valley, planted the Emberveil, and installed safeguards (including the Leviathan). Fox also purged the Ivy from the valley during this period, driving it underground. "This feels like its own story."

### The Archenreach Academy
- School for all Woodlanders — free, open, prestigious for those who become professors
- Carved by moles into scalloped rock cliffs on the western bank of The Mere, at the base of Sister West
- Accessible ONLY by boat from the lake or through the mountain itself — isolated, quiet retreat
- Built over an **old pumping station** that pre-dates the Leviathan (used to pump water from the cistern)
- Full of cloisters and courtyards; most classes held outside; weather always warm on the far side of the lake
- Has its **own library** (separate from the Emberveil Library)
- **Notable characters:**
  - Professor of Woodland Literature (lives in one of the Academy's towers — unnamed)
  - Hawk got her doctorate in library and archival sciences here (professor unnamed — **LORE GAP**)
  - Bear will eventually become the first professor of culinary arts (no culinary program exists yet)
- Currently no culinary arts program — Bear must first apprentice with valley cooks (multi-book arc)

### The Ancestral Apple Tree / Aetherbloom (Chapters 10-11, 21-22)
- Massive ancient tree in the orchards
- Contains a corrupted Ember (Zora's Ember) — cold, black, with sickly filaments
- Guarded by Rus (Von Appleseed) and the squirrels
- Mouse's gloves react strongly to it — connects to Sister North ("0.01%")
- A key Ember location the player must reach and reckon with
- The "Fire and Ice" chapter describes the Ember igniting/burning this tree

### The Three Sisters (Mountains)
- Visible on the horizon — three mountain peaks forming the diamond valley's corners
- The player doesn't know they're supercomputers until late in the story
- **Sister North:** The present — makes choices based on current conditions. Diameter ~25 miles, height ~11,000 ft (Mt. Rainier scale). Contains the Ark (humans in cryogenic stasis) and the QPU core (quantum processing units = "higher reasoning"). QPU core looked like a forest glen before Fox scattered it. Focus of Book 1.
- **Sister West:** Archive/memory — "infinite context window." Base of The Mere, Archenreach Academy
- **Sister East:** The future — plans survival of all living things. Neither West nor East act in the present, which is why they didn't stop Fox.
- Mouse is "Mountainborn" — crafted from mountain stone (i.e., created by Sister North)
- Each Sister had a human city at its base (all now in ruins; the Haunted Rocks are the remains of Sister North's city)

### The Cistern (Underground)
- Massive underground lake deep beneath the valley — the starting point of the entire cooling system
- **Built by humans** during original valley construction (part of the infrastructure that undergirds the Three Sisters)
- The **Leviathan** (Elder) lives at the bottom — a massive, deformed **axolotl**, dragon/wyrm-like. Its tail extends like roots deep into the earth, far beyond the valley, sucking moisture from the surrounding landscape. Dual purpose: (1) perpetual water supply, (2) creates the impassable desert that prevents invaders from reaching the valley. The Leviathan directs all water to the valley's rivers, tributaries, and springs. An elemental force, silent and implacable. Disrupting it = immediate existential threat.
- Leviathan's Ember **fused directly with it** (unique among Elders — most Embers are integrated into infrastructure or separate objects). The Leviathan cannot leave its post.
- Overhung by **Glowcap mushrooms** (which the moles harvest for Lottie's Casino)
- Contains an old maintenance facility — a shack where **Ava** once supervised the Cistern buildout with Mouse. Ava retreats here in Book 2 after her stasis pod malfunctions.
- Salmon raised in **raceways** near the cistern (food supply)
- Former human occupant was a reader and fisherman — kept a journal (discoverable lore item)
- **Talpa (the Mole King)** and his moles maintain the tunnel/maintenance infrastructure beneath the valley (but did NOT build the cistern — humans did)
- The Ivy has NOT found its way down here yet — the cistern is currently safe but a future target
- The Leviathan is asleep, managing water levels reflexively. Fox installed the Leviathan here during the valley rebuilding period.

### The Glass Caverns
- Caverns that look like a giant art installation — every surface covered in glass art
- **Sunbears:** Live within the caves, constantly blowing glass in their furnaces. Glass furniture, glass hanging from ceilings. Artisans devoted to experimentation.
- **Metal:** Sunbears use metal only for flues. Metal is an **extremely rare commodity** in the valley, reserved for very special uses.
- **Deep within:** A vein of iron worked by a family of **Honeybadgers**
  - Responsible for making ALL metal items for the valley
  - Very reclusive, very selective about who gets metal and what it's used for
  - NOT weapon-makers (reinforces no-combat design)
  - **Quest-gating potential:** Mouse may need to earn Honeybadger trust to obtain metal for the Forge
- **The Ivy has created its own forge** somewhere — inferior to Mouse's, can only make "monolithic pieces" (**LORE GAP: where is the Ivy's forge?**)

### Lottie's Casino (Book 2+)
- Underground casino "in the ribs" above the cistern — geological/structural framework above the cistern, below the valley surface
- Primary location for a large chunk of Book 2
- Run by moles (proprietor likely named **Lottie**)
- Moles brew a drink from the **Glowcap mushrooms** growing around the cistern
- NOT relevant to Game 1 implementation

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

### Connections (Full World - Conceptual)
```
Surface Layer:
  Mountain Cave → Old Oak (central hub)
  Old Oak ←→ Overworld (door)
  Overworld → Orchards / Ringtail Tavern / Emberveil / Otterdam / The Mere
  Emberveil Shallows → Cedartown (crossroads) → Emberveil Deep
  Emberveil Deep → Loaming Ponds / Bramblethorn's Hollow
  The Mere → Archenreach Academy (boat only) / Westwatch Warren

Underground Layer:
  Old Oak → Workshop (stairs)
  Glass Caverns (Sunbears/Honeybadgers) → Iron vein
  Westwatch → Queen's Chamber (below)
  Lottie's Casino (ribs above cistern)
  The Cistern (deepest - Leviathan)

Vertical Stack (SW corner, Sister West):
  The Mere (surface) → Archenreach Academy (cliff level) → Lottie's Casino (ribs) → The Cistern (deep)

Ivy-Blocked:
  The Haunted Rocks (completely consumed)
  Various overworld paths (ability-gated)
```

## Environmental Storytelling
- Broken objects tell the story of Sister North's failing systems
- Repaired objects show the valley recovering
- Ivy growth shows the corruption spreading
- NPCs react to changes Mouse makes
- The Old Oak grows brighter as Embers are collected
- Seasonal progression mirrors the story's emotional arc (winter → spring → summer)
