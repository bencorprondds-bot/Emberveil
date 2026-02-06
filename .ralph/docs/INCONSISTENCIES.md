# Emberveil Inconsistencies & Open Questions

> Tracked contradictions, ambiguities, and unresolved questions between chapters,
> character sheets, worldbuilding docs, and game specs. Each item needs author review.

---

## Naming Inconsistencies

### I-001: Sciurus vs. Scurius von Appleseed
- **Where:** Two separate character sheet docs: `Sciurus von Appleseed` and `Scurius "Rus" von Appleseed`
- **Issue:** Different spellings of the same character's first name. Chapters consistently use "Rus" as the short form.
- **Suggestion:** Confirm canonical spelling and merge/reconcile the two docs.
- **Severity:** Low (cosmetic)

### I-002: Embers vs. Seeds
- **Where:** Style Guide uses both terms. Game overview uses "Embers." Some chapters use "Seeds."
- **Issue:** The same objects (fragments of Sister North's quantum core) are called by two names.
- **Current spec decision:** We use "Embers" as the primary game term, "Seeds" as lore/alternate name.
- **Question:** Is "Seeds" an in-world term used by Woodlanders who don't know the true nature? If so, that's intentional and should be documented as such.
- **Severity:** Medium (affects dialogue writing and UI text)

### I-003: Pip vs. Fox
- **Where:** Character sheets use "Pip" and "Fox" interchangeably. Chapters mostly use "Fox."
- **Issue:** Is "Pip" Fox's given name and "Fox" their species/title? Or are these separate characters?
- **Current understanding:** Pip is Fox's name; Fox is what everyone calls them.
- **Question:** Confirm: Is "Pip" the name Sister North gave the AI, and "Fox" the form they chose?
- **Severity:** Low (but important for dialogue)

---

## Lore Contradictions

### I-004: Number of Elders
- **Where:** Ch 17 says "20+ Elders in the valley." The Elders subfolder has 10 docs. Specs list ~9 named Elders.
- **Issue:** We have profiles for fewer than half the stated Elder count.
- **Question:** Are the remaining 10+ Elders intentionally unnamed (to be defined later), or do they exist in unpublished notes?
- **Severity:** Medium (affects game scope - do we need 20+ Elder NPCs?)

### I-005: Fox's Creation vs. Fox's Archive Discovery
- **Where:** Style Guide says Fox was "created by Sister North to remove the Ivy." Ch 9 (Owl's Tale) says Fox accessed "an old archive" that changed their perspective.
- **Issue:** Timeline question - was Fox created, did Fox's job (fight Ivy), THEN found the archive and went rogue? Or did Fox go rogue before completing their Ivy mission?
- **Current understanding:** Fox was created to fight Ivy > found archive > concluded humans should stay asleep > scattered Embers > created Emberveil. The Ivy pre-dates Fox's rebellion.
- **Question:** If the Ivy already existed when Fox was created, what CAUSED the Ivy in the first place? Was it a natural corruption of Sister North's code, or triggered by something specific?
- **Severity:** High (core lore question that affects game narrative)

### I-006: Zora's Death - Cause Specifics
- **Where:** Ch 7 mentions Zora and Daria died. Ch 25 (Shadows of the Emberveil) gives full details: Zora had nightmares, sought Willow, returned with dark green veins, died bleeding.
- **Issue:** Ch 7 treats it as known backstory. Ch 25 reveals it as a mystery to be uncovered. The game spec says "the only way Elders get sick is Ivy infection."
- **Question:** Is Zora's death definitively caused by Ivy infection of her Ember? The "dark green veins" and "sickly filaments" on her corrupted Ember (Ch 22) strongly suggest yes.
- **Follow-up:** If an Elder's Ember gets corrupted, does the Elder die? What happens to the tethered human? This has major implications for the Rus plotline.
- **Severity:** High (drives major story arc and game mechanics)

### I-007: The "0.01%" Revelation
- **Where:** Ch 22 - Mouse's gloves connect to Sister North through the corrupted Ember and learn "0.01%" of something.
- **Issue:** What exactly is 0.01%? Is it the percentage of Sister North's core that remains functional? The percentage of humans still viable? The success rate of the restoration?
- **Question:** What does "0.01%" refer to? This is a climactic reveal and needs to be clearly defined for game implementation.
- **Severity:** High (key story moment)

### I-008: Mountainborn Definition
- **Where:** Ch 16-17 - Mouse discovers she and Fox are "Mountainborn" - "crafted from mountain stone and sinew."
- **Issue:** This seems to contradict the "AI/custodian" framing. Were Mouse and Fox literally built from mountain materials? Is "Mountainborn" the Woodlander term for what we (the reader) know as AIs/custodians?
- **Current understanding:** "Mountainborn" is the Woodlander mythology for the truth that Mouse and Fox were created by the Sisters (who ARE the mountains, in a sense).
- **Question:** Confirm: Mouse was crafted by Sister North, Fox was crafted by Sister North. They are both AI systems given physical form, not biological creatures. "Mountainborn" is the folk-tale version of this.
- **Severity:** Medium (affects how we present Mouse's identity in dialogue)

---

## Story Logic Questions

### I-009: Hawk's Ember and the Secret
- **Where:** Hawk is confirmed as an Elder (carries an Ember) in the specs. Hawk asks Mouse to hide her abilities from Bear.
- **Issue:** If Hawk has an Ember, does she know what Embers truly are? Does she know about Sister North? How much does Hawk understand about Mouse's true nature?
- **Question:** What is the exact scope of Hawk's knowledge? She seems to know Mouse is special but may not understand the full sci-fi truth.
- **Severity:** Medium (critical for writing Hawk's dialogue accurately)

### I-010: Bear's Backstory Gap
- **Where:** Bear arrives in Ch 5 starving from failed hibernation prep. He's a yearling.
- **Issue:** Where did Bear come from? What happened to his family? Is Bear tethered to a human in stasis like all Woodlanders?
- **Question:** Is Bear's origin story told anywhere? Does it matter for the game, or is he simply "a young bear who showed up"?
- **Severity:** Low (characterization detail)

### I-011: The Dilemma of Returning Embers - RESOLVED
- **Where:** Ch 23 (Conflicting Goals) - returning Embers to Sister North would strip valley protection.
- **Resolution (author confirmed):** In Book 1, Mouse returns the first Ember (Zora's corrupted Ember from the Aetherbloom) to Sister North. Consequences:
  - Sister North begins to come back online (some capacity restored)
  - The Ancestral Apple Tree (Aetherbloom) withers and dies
  - No squirrels are directly harmed physically
  - BUT the tree was part of the squirrels' collective identity - an indirect, cultural loss
  - Returning the corrupted Ember HEALS Rus - his madness lifts
  - The trade-off is real: restoration costs the valley something irreplaceable each time
- **Design implication:** The game does NOT offer the player a "clean" resolution. Each Ember return carries a bittersweet cost. Present this as narrative weight, not a player choice (Mouse decides in the story).

### I-012: Bramblethorn's Dreams
- **Where:** Ch 19 - Bramblethorn's dreams include human-like experiences (vines crawling up arms).
- **Issue:** This implies the tethered human's experience is bleeding through into the Woodlander. Does this happen to other Woodlanders? Is it specific to Elders? Is it caused by the Ivy?
- **Question:** Is the "tether bleeding through" a general phenomenon, or specific to Bramblethorn's proximity to the Ivy?
- **Severity:** Medium (affects world mechanics design)

### I-013: The Library's Nature
- **Where:** Ch 16 - The Library is three redwoods spiraling together, produces books from reader's thoughts, keeps records of all Woodlanders.
- **Issue:** Is the Library part of Sister North's infrastructure? Part of Fox's Emberveil system? Or something else entirely? It "has no record of Fox or pre-Woodlander history" which suggests it's post-Fox-rebellion.
- **Question:** Who/what created the Library? Is it self-aware? Is it an Elder?
- **Severity:** Low (fascinating lore question but doesn't block development)

---

## Spec vs. Source Conflicts

### I-014: Ivy as "Death Mechanic" vs. Story
- **Where:** Game specs describe the Ivy as the "death mechanic" (sensory severance → ejection). Story treats Ivy encounters as rare, dramatic events.
- **Issue:** If Ivy encounters are common gameplay (like death in other games), it trivializes what the story treats as deeply significant. If they're rare, is there another "fail state" for puzzles?
- **Question:** How frequently should the player encounter Ivy in gameplay? Is it only at specific story-gated locations, or scattered throughout the world?
- **Severity:** High (core gameplay design decision)

### I-015: Game Overview Scale vs. Vertical Slice
- **Where:** Game Overview doc describes a "giant, to-scale map." Current implementation plan focuses on a vertical slice (Burrow → Workshop → Overworld).
- **Issue:** The scope described in the overview is enormous (multiple biomes, 20+ Elder NPCs, complex ability-gating). The vertical slice is necessarily small.
- **Question:** What's the actual target scope for the first playable build? The full Book 1, or a demo covering Act 1 only?
- **Severity:** Medium (project planning, not lore)

### I-016: Companion Limit
- **Where:** Specs describe Hawk and Bear as the two companions. Story has them traveling together as a trio for most of Book 1.
- **Issue:** Can the player have BOTH companions active simultaneously? The spec says "activeCompanion" (singular) suggesting only one at a time. The story has all three together constantly.
- **Question:** Should the game support multiple active companions (matching the story), or one-at-a-time switching (simpler gameplay)?
- **Severity:** Medium (affects companion system architecture)

---

## Timeline Uncertainties

### I-017: Seasonal Progression
- **Where:** Story moves from Mouse waking (unclear season) → winter (Bear arrives) → spring (greenhouse) → summer (Follies). Game Overview mentions "winter → spring → summer" progression.
- **Issue:** How long does the in-game story span? One year? The chapters suggest roughly one year from awakening to Summer Follies.
- **Question:** Does the game use real seasonal progression (day/night, seasonal changes) or is it scripted to story chapters?
- **Severity:** Low (aesthetic/technical decision)

### I-018: Pre-Story Timeline
- **Where:** How long were humans in stasis? How long has Fox been managing the valley? How old is the Woodlander society?
- **Issue:** "Centuries" is used loosely. The exact timeframe affects how established the Woodlander society should feel.
- **Question:** Is there a canonical number for how long the stasis has lasted?
- **Severity:** Low (background lore)

---

## Resolution Tracking

| ID | Status | Resolution |
|----|--------|------------|
| I-001 | OPEN | Awaiting author confirmation on spelling |
| I-002 | OPEN | Awaiting author confirmation on intentional dual naming |
| I-003 | OPEN | Awaiting author confirmation |
| I-004 | OPEN | Awaiting author confirmation on Elder count |
| I-005 | OPEN | Awaiting author clarification on Ivy origin |
| I-006 | OPEN | Awaiting author confirmation on death mechanism |
| I-007 | OPEN | Awaiting author definition of "0.01%" |
| I-008 | OPEN | Awaiting author confirmation of Mountainborn interpretation |
| I-009 | OPEN | Awaiting author scoping of Hawk's knowledge |
| I-010 | OPEN | Low priority, awaiting author |
| I-011 | **RESOLVED** | Ember return costs the valley something each time; first return withers Aetherbloom but heals Rus |
| I-012 | OPEN | Awaiting author clarification |
| I-013 | OPEN | Low priority |
| I-014 | OPEN | HIGH - gameplay design decision |
| I-015 | OPEN | Medium - project scope |
| I-016 | OPEN | Medium - companion architecture |
| I-017 | OPEN | Low priority |
| I-018 | OPEN | Low priority |
