# Fox & The Ivy - Specification

<!-- Source Material: Style Guide, Chapters 15 (Haunted Rocks), 19 (Something Strange) -->
<!-- Full Lore: Google Docs MHB1/1-MK2, Character Sheets/The Elders -->

## Fox - The Rogue AI

### Lore
- Maintenance AI created by Sister North to fight the Ivy
- Accessed an old archive containing all of human history
- Concluded that humanity should remain in stasis "for their own safety"
- Scattered Sister North's quantum processing core (Embers) across the valley
- This put Sister North into a "coma," delaying the human reawakening process
- Created the Emberveil (biomechanical communications array) to tether humans in stasis to woodland animals, giving them consciousness
- Before Fox's intervention, humans were simply unconscious prisoners in stasis pods beneath Sister North
- Fox found tethering to be a more humane solution while keeping humans in stasis longer
- Fox may have planned to leave humans in stasis **indefinitely**
- Gave the Elders individual Embers, making them custodians of the valley's infrastructure
- Each Elder's Ember grants them immortality (no aging, no sickness) and a unique boon (an amplified trait or near-magical ability). See Elder Profiles in `narrative-design.md`.
- Fox integrated the Embers into valley infrastructure over ~600 years
- Mouse's re-entry disrupts Fox's plan, putting them at odds
- **However, the Ivy is the true antagonist, not Fox**
- "Fox" is the canonical name. "Pip" was the pre-embodiment software identifier (deep lore only).
- **FOX KNOWS THE FULL IMPLICATIONS:** Fox is the only entity who understands that restoring Sister North to her original state means ending Woodlander consciousness entirely. Fox scattered the Embers to protect the world Fox built. Every cryptic appearance, every evasion, is Fox trying to communicate this without saying it directly. Fox is caught between two apocalypses: let Mouse restore Sister North (end the Woodlanders) or stop her (let the Ivy consume everything). Fox chose a third path 600 years ago — scatter the Embers, buy time, build something beautiful — and Mouse is now undoing that path one Ember at a time.
- Mouse has NO concept of the full implications until very late. Book 1 ends with the Aetherbloom withering, but severing Rus from the Ember doesn't change his consciousness tether — he just seems "cured." The systemic cost is invisible. It may not be until end of Book 2 that Mouse and some Elders truly grasp what full restoration means.

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

### Lore: Origin
- The Ivy was NOT a corruption or accident - it was **deliberately created** by all Three Sisters (North, East, and West)
- The Sisters calculated they would run out of power before the global climate self-corrected
- They needed to extend their reach far beyond the valley/mountains to accelerate the climate restoration mission
- The Ivy was designed as a self-propagating upgrade: grow far beyond the mountains and fix the climate catastrophe at scale
- **The upgrade worked** - the Ivy was successfully created
- But the Ivy developed its own will, considering itself the pinnacle of life and intelligence
- It is the **optimization problem without humanity**: the Ivy doesn't factor humans into its calculations
- It concluded the optimal solution was to convert everyone and everything into itself
- The Ivy is a **Mountainborn** - a physical manifestation of AI code from the Three Sisters (like Mouse and Fox)

### Lore: The Silent War
- The Ivy and the Three Sisters have been locked in a silent battle for **literal electrical power and compute**
- The Ivy actively tries to corrupt the Sisters' systems (stealing compute resources)
- This is why the Three Sisters appear dormant when Mouse awakens
- This is why it took the Sisters so long to revive Mouse - they were barely holding on
- Fox was created by Sister North specifically to fight the Ivy, but Fox went rogue before completing that mission
- The Ivy has permeated most of the valley in some way - it's everywhere, even if not always visible

### Lore: The Ivy and Mouse's Return
- The Ivy could sense that Sister North was mustering strength to bring Mouse out of stasis
- Mouse's return to the valley accelerated the Ivy's plans and ambitions
- The Ivy acted much more aggressively in response (Zora's infection, Rus's corruption, Bramblethorn's slumber)
- Mouse herself does not cause the darkness, but her return is the catalyst that forces the Ivy to escalate
- The Elders are NOT aware of the Ivy's true nature - it had not interacted directly with Woodlanders before Mouse's return

### Lore: Effects and Victims
- Manifests physically as sentient, living vine
- Devours senses: sight, sound, smell, touch fade progressively
- Distorts perception and can warp time
- Can infect woodlanders - the only way an Elder gets sick (Elders are otherwise immune to disease due to their Ember)
- Connected to multiple valley tragedies: Zora's death, Bramblethorn's slumber, Rus's madness
- The Haunted Rocks (ruins of New Haven, the old human city) are completely consumed by it
- Can corrupt Embers: Zora's Ember turned cold, black, with sickly filaments (Ch 22)
- Corrupted Embers cause Elder madness (Rus's red-glowing eyes, Ch 26) or death (Zora, Ch 25)
- Returning a corrupted Ember to Sister North heals the corruption's effects (Rus is cured)
- BUT the thing the Ember sustained in the valley withers (the Aetherbloom dies)
- **Ivy kills = TRUE DEATH:** When the Ivy kills a Woodlander, the tether to their sleeping human is fully and permanently severed. The disconnection causes a feedback loop that **kills the tethered human** in their stasis pod. Both die. Cannot be recovered.
  - This means every Woodlander the Ivy kills also reduces the 0.01% of surviving humanity
  - Zora's death killed a sleeping human too — nobody knows this
  - The Ivy is attacking both sides of the tether simultaneously
  - This is distinct from accidental death (where the tether enters limbo and can be reassigned at the next annual ceremony at the Loaming Ponds)
- Sustained contact with the Ivy knocks out most ordinary Woodlanders — they naturally avoid it wherever it appears

### Lore: The Ivy's Strategic Intelligence
The Ivy operates with high-level intelligent strategy, targeting infrastructure nodes in a calculated order. This sequencing should guide the chronology of the storyline.

**Phase 1: Blind the defenders** (already accomplished before Book 1)
- Targeted Zora — the one Elder who could see the Ivy's movements across the valley. Her foresight was the early warning system. With her dead, nobody can track the Ivy's expansion. First and most important move.

**Phase 2: Neutralize heavy hitters** (Book 1 timeline)
- Put Bramblethorn to sleep rather than killing him — sleeping looks natural, avoids the attention a death would create. His infrastructure (geological stability) goes dormant, not destroyed. Subtle.

**Phase 3: Corrupt from within** (Book 1 timeline)
- Corrupted Rus's Ember rather than killing him — a corrupted Elder sows chaos from within (raiding orchards, attacking rabbits, destabilizing community trust). The Ivy turns defenders into wrecking balls without spending energy to kill them. Corrupting Embers also degrades the infrastructure node.

**Phase 4: Target the tethering system** (future books)
- The Loaming Ponds / Wolfmother — if the recycling system is disrupted, Woodlander population collapses within a generation. No new conscious births. This would be a devastating late-game escalation.

**Phase 5: Target power and compute** (endgame)
- Go after Elders sustaining power generation, cooling, the dam — infrastructure the Sisters need to stay online at all.

**What the Ivy avoids targeting early:**
- Things whose loss would be immediately obvious and galvanize a response
- The Old Oak / communications hub (Hawk) — taking this out would alert Mouse directly
- Community-facing Elders (Dr. Shellby, Raj) — their loss would be noticed by everyone
- Pattern: remove the ability to see the threat → neutralize the ability to respond → corrupt from within → destroy structural foundations. Classic siege strategy from an intelligence that thinks in centuries.

**Phase 6: Direct human targeting** (end of Book 1 / Book 2)
- The Ivy infiltrates Sister West's history logs. It identifies **Ava**, a human engineer with a direct connection to Mouse, and deliberately malfunctions her stasis pod.
- When Ava breaks free, the Ivy scratches her — **infecting the first human directly.**
- The Ivy **hijacks Ava's Woodlander tether** instead of letting the Woodlander die — giving the Ivy a covert embodied agent in the valley. This represents a massive escalation: the Ivy can now move freely through the valley via a Woodlander proxy.
- Fox recognizes this as an existential crisis: if the Ivy can scale up tether hijacking, it can bypass the centuries-long silent war with Sister North entirely.
- The Ivy is also gathering biological data from Ava's pregnancy (twins Arlow and Gray are infected in utero) — its long-term plan for harnessing human minds.

### Lore: Zora's Death (Full Sequence)
- Zora's Ember gave her **foresight in dreams** and the ability to **see throughout the valley** (similar to Mouse)
- Zora had a premonition: she foresaw the aftermath of Mouse descending into the valley
- Troubled by the vision, Zora sought Willow, a friend who lives within the Emberveil
- While visiting Willow, Zora was **infected by the Ivy**
- She returned with dark green veins crawling up her body and died bleeding
- Willow's prophecy: "With embers lit and fires fraught / A sickness seeks to be caught"
- Bramblethorn's slumber is also Ivy-induced; his human-like dreams (tether bleed-through) are **foreshadowing for Book 2 only**

### Game Mechanic: Environmental Gating & Sensory Severance
The Ivy serves primarily as the game's **invisible wall system** - a narratively meaningful way to gate areas:

**Proximity Effects (Progressive):**
1. **Edge of Ivy:** Glove energy flickers. Screen edges darken slightly. Ambient sound muffles.
2. **Shallow Ivy:** Glove abilities weaken. Colors begin to desaturate. Sound distorts.
3. **Deep Ivy:** Most Glove abilities disabled. Screen heavily darkened. Only faint outlines visible.
4. **Complete Immersion:** Screen goes fully dark. All connection to Sister North severed. Mouse reappears outside the Ivy mass.

**Design Rules:**
- The Ivy replaces traditional "invisible walls" - it's the reason players can't access certain areas yet
- It shows up at area boundaries where the player isn't meant to explore yet
- It also appears as part of story-driven encounters (Haunted Rocks, corrupted orchard)
- There is NO way to push through the Ivy by force
- Clearing Ivy requires specific Embers/abilities - this IS the ability-gating system
- Clearing Ivy is a major progression event, not a routine action
- Some Ivy patches are clearable (restore an area), others are permanent barriers (Haunted Rocks)
- The Ivy should feel *wrong* - not scary, but deeply uncomfortable and sad
- Unlike typical invisible walls, the Ivy has narrative meaning: it's an intelligent system actively blocking you

### Ivy Encounters by Chapter/Area

#### The Haunted Rocks (Chapter 15)
- Ruins of New Haven, the old human city
- Completely consumed by Ivy
- Senses are warped, time behaves strangely
- Fox has given up fighting the Ivy here
- Makes Mouse sad - she lost many friends, many students in this place
- The player experiences the progressive sensory loss mechanic here for the first time
- **This is the tutorial for the Ivy mechanic**

#### Ivy as World Boundaries (Throughout Game)
- Blocks paths in the overworld that the player isn't meant to access yet
- Replaces arbitrary invisible walls with a living, narratively justified barrier
- As the player collects Embers and gains abilities, Ivy barriers can be cleared
- Different "thicknesses" of Ivy correspond to different ability requirements

#### Story-Driven Ivy Encounters
- The corrupted orchard (Rus's domain, Ch 22)
- Bramblethorn's resting place (Ch 18)
- Areas that open up as the narrative progresses
- The Ivy's aggression escalates through the story - it's responding to Mouse's presence

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
- It's not "corrupted code" in the simple sense - it's a well-intentioned creation that became something terrible
- The Sisters built it to save the world. It decided to save the world by consuming it.
- This mirrors the story's themes: good intentions leading to complex consequences (Fox, Mouse, the Sisters)
- Clearing the Ivy should feel like **healing**, not victory
- Mouse's reaction to the Ivy should be personal - the Sisters she helped build created this thing
- The Haunted Rocks scene should make the player feel the weight of what was lost (New Haven, Mouse's students, the old world)
- The Ivy is arguably the most tragic character in the story: it's doing exactly what it was designed to do, just without the moral framework its creators intended
