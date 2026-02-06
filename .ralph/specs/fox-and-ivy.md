# Fox & The Ivy - Specification

<!-- Source Material: Style Guide, Chapters 15 (Haunted Rocks), 19 (Something Strange) -->
<!-- Full Lore: Google Docs MHB1/1-MK2, Character Sheets/The Elders -->

## Fox (Pip) - The Rogue AI

### Lore
- Maintenance AI created by Sister North to remove the Ivy
- Accessed an old archive containing all of human history
- Concluded that humanity should remain in stasis "for their own safety"
- Scattered Sister North's quantum processing core (Embers/Seeds) across the valley
- This put Sister North into a "coma," delaying the human reawakening process
- Created the Emberveil (biomechanical communications array) to tether humans in stasis to woodland animals, giving them consciousness
- Gave the Elders individual Embers, making them custodians of the valley's infrastructure

### Personality
- Cryptic, elusive, rarely seen
- Can project images into minds
- Shows up when *system-level* processes break down, NOT for individual problems
- Not a villain - their motivations come from compassion, not malice
- Has a deep, complex relationship with the valley they've shaped

### In-Game Presence
Fox appears **tangentially** - brief, scripted moments:
- Cutscene-like encounters where Fox arrives, delivers a cryptic message or action, then vanishes
- Fox might appear at major story beats (beginning of game, Ember discoveries, Ivy confrontations)
- The player never controls or directly interacts with Fox in gameplay
- Fox's appearances should feel mysterious and slightly unsettling
- After Fox leaves, Mouse's internal monologue processes what happened

### Known Fox Appearances (from chapters)
1. **Ch 1** - Brief glimpse during Mouse's descent from the mountain cave
2. **Ch 7** - Referenced: Fox once appeared at Otterdam to repair the dam
3. **Ch 9** - Fox whispers to Owl. Tells Mouse "I set the valley free"
4. **Ch 14** - Fox appears in the ruins of the Haunted Rocks when Bean goes missing
5. **Ch 15** - Fox navigates the Ivy confidently (immune to it), helps locate Bean
6. **Ch 16** - Referenced: The Library has no record of Fox or pre-Woodlander history
7. **Ch 17** - Referenced: Fox gave Elders "the fire of the mountains" (Embers)
8. **Ch 22** - Mouse connects to Sister North through corrupted Ember, learns Fox's full impact

### Technical Implementation
- Fox is NOT an NPC you walk up to and talk to
- Fox encounters are triggered events / cutscenes
- `FoxEncounter.cs` - triggered by entering specific zones or collecting specific Embers
- GameManager state: `GameState.Cutscene` during Fox appearances
- Fox can project visual effects into the scene (images, memories)
- No combat, no chase - Fox simply appears and disappears

---

## The Ivy - The True Antagonist

### Lore
- A corrupted iteration of Sister North's programming
- Manifests physically as sentient, living vine
- Devours senses: sight, sound, smell, touch fade progressively
- Distorts perception and can warp time
- Can infect woodlanders - the only way an Elder gets sick
- Connected to multiple valley tragedies: Zora's death, Bramblethorn's slumber, Rus's madness
- Fox was originally created to fight the Ivy but went rogue instead
- The Haunted Rocks (ruins of New Haven, the old human city) are completely consumed by it
- Can corrupt Embers: Zora's Ember turned cold, black, with sickly filaments (Ch 22)
- Corrupted Embers cause Elder madness (Rus's red-glowing eyes, Ch 26) or death (Zora, Ch 25)
- Returning a corrupted Ember to Sister North heals the corruption's effects (Rus is cured)
- BUT the thing the Ember sustained in the valley withers (the Aetherbloom dies)
- Zora's death sequence: nightmares → sought Willow → returned with dark green veins → died bleeding
- Bramblethorn's slumber is Ivy-induced - he dreams of human-like experiences (tether bleeding through)
- Willow's prophecy: "With embers lit and fires fraught / A sickness seeks to be caught"

### Game Mechanic: Sensory Severance
The Ivy is the game's primary obstacle and "death" mechanic:

**Proximity Effects (Progressive):**
1. **Edge of Ivy:** Glove energy flickers. Screen edges darken slightly. Ambient sound muffles.
2. **Shallow Ivy:** Glove abilities weaken. Colors begin to desaturate. Sound distorts.
3. **Deep Ivy:** Most Glove abilities disabled. Screen heavily darkened. Only faint outlines visible.
4. **Complete Immersion:** Screen goes fully dark. All connection to Sister North severed. Mouse reappears outside the Ivy mass.

**Design Rules:**
- There is NO way to push through the Ivy by force
- The Ivy is a hard gate - you need specific Embers/abilities to clear it
- Clearing Ivy is a major progression event, not a routine action
- Some Ivy patches are clearable (restore an area), others are permanent barriers (Haunted Rocks)
- The Ivy should feel *wrong* - not scary, but deeply uncomfortable and sad

### Ivy Encounters by Chapter/Area

#### The Haunted Rocks (Chapter 15)
- Ruins of New Haven, the old human city
- Completely consumed by Ivy
- Senses are warped, time behaves strangely
- Fox has given up fighting the Ivy here
- Makes Mouse sad - she lost many friends, many students in this place
- The player experiences the progressive sensory loss mechanic here for the first time
- **This is the tutorial for the Ivy mechanic**

#### Other Ivy Appearances
- Blocking paths in the overworld (ability-gated)
- Infecting specific locations or NPCs (quest-driven clearance)
- Growing stronger if Mouse takes too long (optional tension mechanic)
- The Mad Squirrel King's corruption may be Ivy-related

### Technical Implementation

```
IvyZone.cs
- TriggerCollider defining the Ivy area
- proximityLevel: float (0-1, how deep Mouse is)
- Affects: GloveController (reduce power), CameraController (darken/desaturate),
  AudioManager (muffle/distort), PlayerController (slow movement)
- At proximityLevel >= 1.0: trigger ejection

IvyEjection.cs
- Fade to black over 1 second
- Respawn Mouse at last safe position outside the Ivy
- Brief disorientation effect on respawn
- Mouse's internal monologue: reflective, sad, not panicked

IvyClearance.cs (for clearable patches)
- Requires specific Ember ability
- Dramatic animation: Glove energy pushes back the vines
- Area permanently restored after clearance
- May reveal hidden locations, NPCs, or Embers beneath
```

### Emotional Design
- The Ivy should evoke sadness and loss, not fear or anger
- It represents corruption, not evil - broken code, not malice
- Clearing the Ivy should feel like **healing**, not victory
- Mouse's reaction to the Ivy should be personal - it's destroying *her* creation
- The Haunted Rocks scene should make the player feel the weight of what was lost (New Haven, Mouse's students, the old world)
