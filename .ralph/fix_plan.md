# Emberveil - Ralph Fix Plan

## Phase 1: Core Systems Completion (HIGH PRIORITY)

### Burrow Scene Setup
- [ ] Create Burrow scene with 20x15 tile room using Tilemap
- [ ] Set up Tilemap Collider 2D for walls
- [ ] Place Mouse GameObject with PlayerController, SpriteRenderer, Rigidbody2D, BoxCollider2D
- [ ] Set up Cinemachine camera to follow Mouse
- [ ] Verify wall collision and WASD movement

### Glove System Polish
- [ ] Create "Interactable" layer in Unity project settings
- [ ] Attach GloveController to Mouse and configure interactableLayer
- [ ] Create test crate with LiftableObject for testing lift/drop
- [ ] Add URP 2D Light to Mouse (blue-green, intensity 0.5) linked to Glove activation
- [ ] Verify hover highlighting and prompt text on interactables

### Yarn Spinner Dialogue Integration
- [ ] Set up Yarn Spinner DialogueRunner in Burrow scene
- [ ] Create Hawk_Intro.yarn dialogue file
- [ ] Build basic Dialogue UI (text box, character name, continue prompt)
- [ ] Uncomment and connect TalkableNPC Yarn Spinner integration
- [ ] Add Hawk NPC to Burrow (gray square, TalkableNPC component)
- [ ] Verify player freeze during dialogue and release after

---

## Phase 2: Workshop & Crafting (HIGH PRIORITY)

### Workshop Scene
- [ ] Create Workshop scene with circular room Tilemap
- [ ] Add visual spiral staircase entrance
- [ ] Place three workbench sprites along walls
- [ ] Create Forge interactable (black rectangle, far wall)

### Scene Transitions
- [ ] Create SpawnPoint GameObjects in Burrow and Workshop
- [ ] Add SceneTransition trigger zones at staircases in both scenes
- [ ] Test fade-to-black scene transitions
- [ ] Verify player spawns at correct SpawnPoint after transition

### Crafting System
- [ ] Create ForgeInteractable.cs with basic craft menu
- [ ] Create ItemData ScriptableObjects: MetalFrames, Lenses, HawksGlasses
- [ ] Implement HawksGlasses recipe (requires MetalFrames + Lenses)
- [ ] Create Crafting UI (simple list of available recipes)
- [ ] Add "give item to NPC" interaction for quest items
- [ ] Test full loop: Burrow → Workshop → Craft Glasses → Burrow → Give to Hawk

---

## Phase 3: Overworld & Companions (MEDIUM PRIORITY)

### Overworld Scene
- [ ] Create Overworld scene with 50x50 tile Tilemap
- [ ] Design: Old Oak building, grass field, forest edge, river
- [ ] Add scene transitions: Old Oak door ↔ Burrow
- [ ] Place environmental objects (trees, rocks, flowers)

### Bear Companion
- [ ] Create Bear NPC (brown square, TalkableNPC)
- [ ] Write Bear introduction dialogue (fishing and pie themes)
- [ ] Implement CompanionManager.cs (track active companion, follow behavior)
- [ ] Add "join party" dialogue option for Bear
- [ ] Implement Bear follow-at-distance behavior

### Companion Puzzles
- [ ] Create heavy log blocking path (LiftableObject, requiresCompanion = true)
- [ ] Test: Recruit Bear → Together clear log → Path opens
- [ ] Add visual/audio feedback for companion-assisted actions

---

## Phase 4: Mend System (MEDIUM PRIORITY)
<!-- NOTE: Mend is unlocked via a side-character quest chain, NOT via Ember collection. -->
<!-- The specific quest that unlocks Mend is TBD (author decision needed). -->

### Mend Interaction Type
- [ ] Create MendableObject.cs (extends Interactable)
- [ ] Define mend states: Broken → Mending → Repaired
- [ ] Add mend animation/visual feedback (particles, color change)
- [ ] Create mendable objects for Burrow (broken shelf, cracked window)
- [ ] Implement material requirements for some mend actions
- [ ] Mended objects should change appearance and unlock new interactions
- [ ] Hook unlock trigger to ProgressionManager quest completion (quest TBD)

---

## Phase 5: Grow System (MEDIUM PRIORITY)
<!-- NOTE: Grow is unlocked via a side-character quest chain, NOT via Ember collection. -->
<!-- The specific quest that unlocks Grow is TBD (author decision needed). -->

### Grow Interaction Type
- [ ] Create GrowableObject.cs (extends Interactable)
- [ ] Define growth stages with visual changes
- [ ] Add growth timer or instant-grow via Gloves
- [ ] Create garden area in Overworld or Greenhouse
- [ ] Implement seed planting and growth mechanics
- [ ] Grown items can be harvested into inventory
- [ ] Hook unlock trigger to ProgressionManager quest completion (quest TBD)

---

## Phase 6: Polish & Vertical Slice (LOW PRIORITY)

### Art & Visuals
- [ ] Replace placeholder squares with pixel art sprites (Mouse, Hawk, Bear)
- [ ] Create environment tile set (grass, dirt, stone, wood)
- [ ] Add particle effects for Glove activation
- [ ] Implement day/night lighting cycle (optional)

### Audio
- [ ] Add footstep sounds
- [ ] Add Glove activation hum
- [ ] Add Forge door/crafting sounds
- [ ] Add dialogue text blip sounds
- [ ] Add item pickup sounds
- [ ] Create ambient music: Burrow (cozy), Workshop (mysterious), Overworld (pastoral)

### UI Systems
- [ ] Build Inventory UI (grid display, item tooltips)
- [ ] Build main menu (Title, New Game, Continue, Options)
- [ ] Implement Bean Mode hint system (60s stuck → hint prompt)
- [ ] Add pause menu with volume controls

### Save/Load System
- [ ] Implement save system (player position, inventory, quest state, scene)
- [ ] JSON serialization for save data
- [ ] Auto-save on scene transitions
- [ ] Load from main menu

---

## Phase 4.5: Quest-Driven Progression System (MEDIUM PRIORITY)
<!-- This is the core progression architecture for Game 1 -->

### ProgressionManager
- [ ] Create ProgressionManager.cs (Singleton) — tracks quest completions, ability unlocks, ally count
- [ ] Define quest → ability unlock mappings (pending author decision on specific mappings)
- [ ] Integrate with GloveController for ability availability checks
- [ ] Integrate with PartyManager for ally tracking
- [ ] Create Forge recipe unlock system (recipes discovered through quests/exploration)
- [ ] Implement finale scaling based on ally count

---

## Deferred (Future Phases)
- [ ] Greenhouse and farming mechanics
- [ ] Summer Follies minigames (Bear's Bake-Off, Ring Toss)
- [ ] Stealth sections in Apple Orchard
- [ ] Bramblethorn as rideable mount
- [ ] Scan ability to see Embers (possibly Game 2)
- [ ] Storm/weather system
- [ ] Full 26+ chapter narrative
- [ ] Multiple companions beyond Bear and Hawk
- [ ] Multi-Ember progression system (Game 2+)

---

## Completed
- [x] Project structure created
- [x] PlayerController.cs implemented (WASD movement, facing direction, freeze)
- [x] GloveController.cs implemented (raycast targeting, activation, quick interact)
- [x] IInteractable interface defined (8 interaction types)
- [x] Interactable base class (hover highlight, prompt text, can/cannot interact)
- [x] LiftableObject.cs (lift, carry, drop with physics)
- [x] TalkableNPC.cs (character name, portrait, dialogue scaffolding)
- [x] ExaminableObject.cs (examine text, first-time vs repeat text)
- [x] GameManager.cs (6 game states, singleton, pause/resume)
- [x] InventoryManager.cs (add/remove items, stackable, max 20 slots, events)
- [x] ItemData.cs (ScriptableObject, crafting recipes, item types)
- [x] SceneTransition.cs (trigger-based, fade effect, spawn points)
- [x] Ralph integration and configuration
- [x] Comprehensive game design documentation

## Notes
- Scripts live in `Emberveil/Assets/Scripts/` - always check there first
- Unity scenes are binary - focus on script changes
- Yarn Spinner is installed but dialogue integration is commented out pending scene setup
- Current art is placeholder colored squares - this is intentional
- Focus on mechanics before polish
