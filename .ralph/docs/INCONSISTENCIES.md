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

### I-005: Fox's Creation vs. Fox's Archive Discovery - RESOLVED
- **Where:** Style Guide says Fox was "created by Sister North to remove the Ivy." Ch 9 (Owl's Tale) says Fox accessed "an old archive" that changed their perspective.
- **Resolution (author confirmed):** The Ivy was **deliberately created** by all Three Sisters (North, East, and West) as an upgrade. The Sisters calculated they'd run out of power before the climate self-corrected, so they built the Ivy to extend their reach beyond the valley and fix the climate at scale. The upgrade worked - but the Ivy developed its own will, considering itself the pinnacle of life/intelligence. It's the optimization problem without humanity: it decided converting everything into itself was optimal. The Ivy and the Three Sisters have been in a silent war for power (electrical) and compute ever since. This is why the Sisters appear dormant and why it took so long to revive Mouse. Fox was then created by Sister North to fight the Ivy, but Fox went rogue (found the archive, scattered Embers) before completing that mission.
- **Timeline:** Sisters create Ivy → Ivy goes rogue → Sisters fight Ivy (drain power) → Sister North creates Fox to fight Ivy → Fox finds archive → Fox goes rogue → Mouse put in stasis → story begins

### I-006: Zora's Death - Cause Specifics - RESOLVED
- **Resolution (author confirmed):** Zora's Ember gave her foresight in dreams and the ability to see throughout the valley (similar to Mouse's connection to Sister North). Zora had a premonition of the aftermath of Mouse's descent into the valley. Troubled, she visited Willow, a friend who lives within the Emberveil. While there, Zora was **infected by the Ivy**. She returned with dark green veins crawling up her body and died bleeding. Key detail: Mouse's return doesn't directly cause the darkness, but it accelerated the Ivy's plans. The Ivy sensed Sister North mustering strength to revive Mouse and acted more aggressively in response - Zora's infection was part of this escalation.
- **Follow-up still open:** What happens to a tethered human when their Woodlander Elder dies? (Relevant for Zora's human counterpart)

### I-007: The "0.01%" Revelation - RESOLVED
- **Resolution (author confirmed):** 0.01% is **all that remains of humanity**. Of everyone Mouse tried to save by putting them in stasis, only 0.01% survive. This is the story's most devastating revelation - Mouse sacrificed everything, entered stasis herself, and nearly all of it was for nothing. The weight of this number drives the rest of the narrative.
- **Design implication:** This reveal must be handled with care in-game. It should hit the player the way it hits Mouse - a single, quiet, devastating number.

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

### I-014: Ivy as "Death Mechanic" vs. Story - RESOLVED
- **Resolution (author confirmed):** The Ivy is NOT primarily a "death mechanic" - it's the game's **invisible wall system**. Just as other games use invisible barriers to prevent players from accessing areas too early, the Ivy serves that role with narrative justification. It appears at boundaries where players aren't meant to explore yet, and in story-driven encounters. The sensory severance mechanic still applies when the player pushes into Ivy, but the primary design role is **area gating**, not frequent death.
- **Design implication:** Reframe the Ivy in all specs as "narratively justified invisible walls + story encounters" rather than "death mechanic." The sensory effects (darkening, muffling) serve as the warning that the player is approaching a boundary. Full ejection only happens if they push past the warning.

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
| I-005 | **RESOLVED** | Ivy deliberately created by all Three Sisters as climate-fix upgrade; gained own will; silent war for power/compute |
| I-006 | **RESOLVED** | Zora infected by Ivy while visiting Willow; Ivy escalated in response to Sister North reviving Mouse |
| I-007 | **RESOLVED** | 0.01% = all that remains of humanity in stasis |
| I-008 | OPEN | Awaiting author confirmation of Mountainborn interpretation |
| I-009 | OPEN | Awaiting author scoping of Hawk's knowledge |
| I-010 | OPEN | Low priority, awaiting author |
| I-011 | **RESOLVED** | Ember return costs the valley something each time; first return withers Aetherbloom but heals Rus |
| I-012 | OPEN | Awaiting author clarification |
| I-013 | OPEN | Low priority |
| I-014 | **RESOLVED** | Ivy = narratively justified invisible walls + story encounters, not frequent death mechanic |
| I-015 | OPEN | Medium - project scope |
| I-016 | OPEN | Medium - companion architecture |
| I-017 | OPEN | Low priority |
| I-018 | OPEN | Low priority |
