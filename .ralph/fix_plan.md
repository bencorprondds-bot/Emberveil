# Emberveil — HD-2D Vertical Slice Roadmap (Chapters 1–5)

This plan builds a playable vertical slice covering Act 1: Awakening (Chapters 1–5) using the HD-2D visual style. Each phase produces something runnable. Grey-box first, polish later.

**Intro sequence:** Sister North peak → mountain descent → bridge → Old Oak → Hawk → awakening

---

## Phase 0: HD-2D Foundation (CRITICAL — do this first)

### 0A. Script Migration (2D → 3D)
- [x] Migrate PlayerController.cs: Rigidbody2D → CharacterController, Vector2 → Vector3 (XZ plane)
- [x] Migrate GloveController.cs: Physics2D → Physics, Light2D → Light, Collider2D → Collider
- [x] Migrate Interactable.cs: SpriteRenderer → Renderer (material-based highlight)
- [x] Migrate LiftableObject.cs: Collider2D → Collider
- [x] Migrate TalkableNPC.cs: SpriteRenderer flip → Y-axis rotation for facing
- [x] Migrate SceneTransition.cs: OnTriggerEnter2D → OnTriggerEnter, Collider2D → Collider
- [ ] Verify all scripts compile (no 2D API references remain)

### 0B. HD-2D Camera & Rendering
- [ ] Create HD2DCameraController.cs — fixed ~35° tilt, follows player on XZ, smooth damp
- [ ] Create BillboardSprite.cs — makes sprite quads always face the camera
- [ ] Set up URP 3D pipeline asset with post-processing: bloom, depth of field, vignette
- [ ] Create point-filtered pixel-art material (unlit or lit, no texture smoothing)
- [ ] Create billboard sprite material (alpha cutout, point-filtered, receives shadows optional)
- [ ] Test: grey cube on 3D plane, camera at angle, billboard sprite quad — confirms pipeline works

### 0C. ProBuilder Grey-Box Toolkit
- [ ] Install ProBuilder package
- [ ] Create modular grey-box pieces: floor tile (1x1), wall segment, stair block, door frame
- [ ] Verify collision on all pieces (MeshCollider or BoxCollider)
- [ ] Create a test room (10x10 floor, walls, a door opening) and walk around in it

---

## Phase 1: The Awakening — Mountain Cave & Descent (Chapter 1 opening)

### 1A. Mountain Cave Scene (Opening)
- [ ] Create MountainCave scene — grey-box cave interior with ProBuilder
- [ ] Design: small chamber, narrow exit passage, light shaft from above
- [ ] Place Mouse at "awakening" position (lying down → stands up)
- [ ] Implement simple awakening cutscene: camera slowly reveals the cave
- [ ] Add cold ambient lighting (blue-white directional, dim)
- [ ] Mouse's Gloves flicker with faint blue-green light (first visual hook)
- [ ] Exit passage leads to MountainDescent scene transition

### 1B. Mountain Descent Scene
- [ ] Create MountainDescent scene — grey-box outdoor mountain path
- [ ] Design: winding path downhill, rocky terrain, distant valley visible below
- [ ] Add a bridge crossing (narrative landmark from the intro sequence)
- [ ] Brief Fox encounter: Fox silhouette visible on a ridge, then gone (no dialogue, just visual)
- [ ] Environmental storytelling: broken trail markers, overgrown path (hints of decay)
- [ ] Transition at bottom of path → Overworld (near Old Oak)

### 1C. Intro Sequence Flow
- [ ] Implement cutscene manager basics (camera moves, fade in/out, text overlay)
- [ ] Opening text: brief first-person narration (Mouse's voice, 2-3 lines)
- [ ] Camera: start at Sister North peak (distant mountain shot), sweep down to cave entrance
- [ ] Seamless flow: Cave → Descent → Bridge → Valley → Old Oak exterior

---

## Phase 2: The Old Oak — Meeting Hawk (Chapter 1 continued)

### 2A. Overworld Scene (Initial Area)
- [ ] Create Overworld scene — grey-box open area around the Old Oak
- [ ] Design: clearing with the Old Oak (large tree structure), grass ground plane, forest edge
- [ ] Place the Old Oak as a 3D structure (hollow tree, door opening, branches above)
- [ ] Add the River nearby (flat blue plane or simple mesh, Bear's future fishing spot)
- [ ] Ivy patches visible at forest edge (visual area gates — can't pass yet)
- [ ] NPCs: none yet except Hawk at the Oak

### 2B. Meeting Hawk
- [ ] Place Hawk NPC near the Old Oak (billboard sprite, TalkableNPC component)
- [ ] Hawk is injured/disoriented — can barely see (no glasses)
- [ ] Dialogue: Hawk introduces herself, mentions she can't see, the tree feels warm
- [ ] Mouse offers to help → Hawk mentions the Workshop below might have tools
- [ ] This triggers the "Craft Hawk's Glasses" quest objective

### 2C. Burrow Scene
- [ ] Create Burrow scene — grey-box interior inside the Old Oak
- [ ] Design: cozy room (~20x15 unit footprint), sleeping area, bookshelf, broken shelf, window
- [ ] Stairs leading down to Workshop
- [ ] Warm amber lighting (point lights, bloom on warm sources)
- [ ] Scene transition: Overworld ↔ Burrow (door), Burrow ↔ Workshop (stairs)

---

## Phase 3: The Workshop — Crafting Hawk's Glasses (Chapter 3)

### 3A. Workshop Scene
- [ ] Create Workshop scene — grey-box circular room beneath the Burrow
- [ ] Design: spiral staircase entrance, the Forge (central/far wall), workbenches, drawers
- [ ] The Forge: 3D block with blue-green point light + bloom — the visual centrepiece
- [ ] Place MetalFrames on a workbench (ExaminableObject → pickup)
- [ ] Place Lenses in a drawer (ExaminableObject → pickup)
- [ ] Dim lighting with Forge glow contrast

### 3B. Crafting System (Minimal)
- [ ] Create ForgeInteractable.cs — interact with Forge to open craft menu
- [ ] Create ItemData ScriptableObjects: MetalFrames, Lenses, HawksGlasses
- [ ] Implement HawksGlasses recipe (requires MetalFrames + Lenses)
- [ ] Simple crafting UI: list available recipes, craft button, success feedback
- [ ] Blue-green energy animation on craft (particle system or shader pulse)

### 3C. Giving Glasses to Hawk
- [ ] Implement "give item to NPC" interaction on TalkableNPC
- [ ] Hawk receives glasses → dialogue changes (gratitude, can see now)
- [ ] Hawk asks Mouse to hide her abilities from Bear (important character moment)
- [ ] Quest completion: "Hawk's Glasses" marked done in progression

---

## Phase 4: Digging the Burrow & Settling In (Chapter 2 & 4)

### 4A. Yarn Spinner Dialogue Integration
- [ ] Set up Yarn Spinner DialogueRunner in a persistent scene object
- [ ] Create dialogue UI (text box, character name, portrait, continue prompt)
- [ ] Write Hawk_Intro.yarn (meeting at Old Oak)
- [ ] Write Hawk_Workshop.yarn (directing Mouse to the Workshop)
- [ ] Write Hawk_Glasses.yarn (receiving glasses, asking to keep abilities secret)
- [ ] Verify player freeze during dialogue and release after

### 4B. Burrow Interactables
- [ ] Broken shelf (ExaminableObject → later becomes MendableObject when Mend unlocks)
- [ ] Bookshelf (ExaminableObject — flavour text about old titles)
- [ ] Window (ExaminableObject — "The valley stretches out below...")
- [ ] Test crate (LiftableObject — for testing Glove lift/carry/drop in 3D)

### 4C. Glove System Polish (HD-2D)
- [ ] Blue-green point light on Mouse linked to Glove activation (Light component + bloom)
- [ ] Glove activation visual: subtle particle effect around hands
- [ ] Hover highlighting: material property change (emission pulse) instead of sprite color
- [ ] Verify raycast interaction works at the tilted camera angle
- [ ] Interaction range tuned for 3D space

---

## Phase 5: Bear Arrives — Winter Storm (Chapter 5)

### 5A. Bear Introduction
- [ ] Implement seasonal weather: snow particle system for winter storm
- [ ] Bear arrives at the Old Oak door — cutscene or triggered dialogue
- [ ] Bear NPC (billboard sprite, TalkableNPC component)
- [ ] Dialogue: Bear is hungry, failed hibernation, Mouse and Hawk take him in
- [ ] Bear joins the party (CompanionManager — tracks active companions)

### 5B. Companion System (Minimal)
- [ ] Create CompanionManager.cs — singleton, tracks active companions, follow behaviour
- [ ] Bear follows Mouse at a comfortable distance (pathfinding or simple follow)
- [ ] Bear waits outside during indoor scenes or follows to Burrow
- [ ] Hawk follow behaviour (already present, flies/perches nearby)

### 5C. Life at the Oak
- [ ] Chess/cards interactable in Burrow (ExaminableObject — flavour text for now)
- [ ] Fishing spot at the River (ExaminableObject — "Bear is learning to fish")
- [ ] Cooking interactable in Burrow (placeholder — future crafting expansion)
- [ ] Day/night cycle placeholder (scripted, not real-time — warm dawn → golden afternoon → blue dusk)

---

## Phase 6: Vertical Slice Polish

### 6A. Scene Transitions
- [ ] Fade-to-black transitions between all scenes (0.5s out, 0.5s in)
- [ ] Named SpawnPoints in every scene with correct facing directions
- [ ] Seamless overworld movement (no loading for walking around outside)
- [ ] Test full loop: Cave → Descent → Overworld → Old Oak → Burrow → Workshop → Burrow → Overworld

### 6B. Audio (Placeholder)
- [ ] Footstep sounds (3D, on ground contact)
- [ ] Glove activation hum (blue-green energy)
- [ ] Forge crafting sounds
- [ ] Dialogue text blip sounds
- [ ] Ambient: wind (mountain), birdsong (valley), fire crackle (Burrow)
- [ ] Music: Cave (sparse, eerie), Descent (building wonder), Overworld (pastoral), Burrow (cozy), Workshop (low hum)

### 6C. UI Systems
- [ ] Inventory UI (grid display, item names, basic tooltips)
- [ ] Interaction prompt UI (floating text near targeted object)
- [ ] Quest tracker (simple text list — current objectives)
- [ ] Main menu placeholder (Title screen, New Game, Continue)

### 6D. Save/Load
- [ ] Save system: player position, current scene, inventory, quest state, companion state
- [ ] JSON serialization
- [ ] Auto-save on scene transitions
- [ ] Load from main menu

---

## Deferred (Post-Vertical-Slice)

### Future Chapters (6+)
- [ ] Greenhouse (Chapter 6) — Grow ability, community cooperation
- [ ] Thanksgiving (Chapter 7) — Valley map, community event
- [ ] Fabula Noctua (Chapter 8) — Ringtail Tavern, Owl's tale
- [ ] Fox encounter expansion (Chapter 9)
- [ ] Orchard / Rus storyline (Chapters 10-13)

### Future Systems
- [ ] Mend ability (unlocked via quest chain — TBD which quest)
- [ ] Grow ability (unlocked via quest chain — TBD which quest)
- [ ] Full companion puzzle system (Bear lifts heavy objects)
- [ ] Ivy mechanic (sensory loss, area gating, severance)
- [ ] ProgressionManager (quest → ability unlock mappings)
- [ ] Bean Mode hint system
- [ ] Multiple Ember progression (Game 2+)

### Art & Assets (Replace Grey-Box)
- [ ] Pixel-art character sprites: Mouse, Hawk, Bear, Fox silhouette
- [ ] Environment textures: stone, wood, grass, dirt, snow
- [ ] 3D environment models: Old Oak tree, cave interior, mountain path
- [ ] Particle effects: Glove energy, Forge sparks, snow, dust motes
- [ ] Lighting polish: god rays through Oak windows, Forge glow refinement

---

## Completed
- [x] Project structure created
- [x] PlayerController.cs implemented (now 3D — CharacterController, XZ movement)
- [x] GloveController.cs implemented (now 3D — Physics raycasts, Light component)
- [x] IInteractable interface defined (8 interaction types)
- [x] Interactable base class (hover highlight via Renderer, prompt text)
- [x] LiftableObject.cs (lift, carry, drop with 3D colliders)
- [x] TalkableNPC.cs (character name, portrait, dialogue scaffolding)
- [x] ExaminableObject.cs (examine text, first-time vs repeat text)
- [x] GameManager.cs (6 game states, singleton, pause/resume)
- [x] InventoryManager.cs (add/remove items, stackable, max 20 slots, events)
- [x] ItemData.cs (ScriptableObject, crafting recipes, item types)
- [x] SceneTransition.cs (trigger-based, fade effect, spawn points — now 3D)
- [x] Ralph integration and configuration
- [x] Comprehensive game design documentation
- [x] HD-2D art direction specs updated
- [x] All project docs updated for URP 3D / HD-2D

## Notes
- Scripts live in `Emberveil/Assets/Scripts/` — always check there first
- Unity scenes are binary — focus on script changes
- Yarn Spinner is installed but dialogue integration is commented out pending scene setup
- All physics/rendering is now 3D (URP 3D pipeline, CharacterController, Physics raycasts)
- Characters are 2D pixel-art sprites on billboarded quads in 3D space
- Grey-box everything first with ProBuilder, replace with proper art later
- Camera is at ~35° tilt angle — test all interactions at this angle
