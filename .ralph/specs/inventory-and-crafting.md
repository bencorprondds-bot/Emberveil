# Inventory & Crafting Specification

## Inventory System

### Current Implementation (InventoryManager.cs)
- Singleton manager, persists across scenes
- Max 20 item slots (configurable)
- Items defined as ScriptableObjects (ItemData)
- Stackable and non-stackable items
- Events: OnItemAdded, OnItemRemoved, OnInventoryChanged
- Debug UI showing contents (Tab to toggle)

### Item Types
| Type | Description | Example |
|------|-------------|---------|
| Material | Raw crafting ingredients | Metal Frames, Lenses, Wood, Thread |
| Crafted | Items made at the Forge | Hawk's Glasses, Repaired Clock |
| Quest | Story-critical items | Old Key, Hawk's Letter |
| Consumable | Single-use items | Healing Honey, Ember Dust |
| Gift | Items to give to NPCs | Pie for Bear, Plant Seeds for Greenhouse |
| Key | Unlocks specific doors/areas | Workshop Key, Greenhouse Key |

### Item Properties (ItemData ScriptableObject)
- `itemName`: Display name
- `description`: Flavor text
- `icon`: Sprite for UI
- `isStackable`: Can stack in inventory
- `maxStackSize`: Stack limit (default 99)
- `itemType`: Enum from above
- `isUsable`: Can be used from inventory
- `craftingRecipe`: List of CraftingIngredient (for craftable items)

---

## Crafting System

### The Forge
- Located in the Workshop scene
- Central crafting station for the game
- Player interacts via Gloves → opens crafting interface

### Crafting Flow
1. Player approaches the Forge and interacts
2. Crafting UI opens showing available recipes
3. Recipes with all materials highlight as craftable
4. Player selects a recipe
5. Brief crafting animation
6. New item added to inventory
7. Materials consumed

### Recipe System (Already in ItemData.cs)
Each craftable ItemData has:
- `craftingRecipe`: List of `CraftingIngredient`
  - `item`: Required ItemData reference
  - `amount`: How many needed
- `CanCraft()`: Checks inventory for all ingredients
- `TryCraft()`: Consumes ingredients and adds crafted item

### Known Recipes (Vertical Slice)

#### Hawk's Glasses
- **Requires:** Metal Frames x1, Lenses x1
- **Result:** Hawk's Glasses (Quest item)
- **Location:** Found materials in Workshop, craft at Forge
- **Triggers:** Hawk recruitment dialogue when given

#### Repaired Clock (Future)
- **Requires:** Clock Gears x2, Glass Face x1
- **Result:** Repaired Clock
- **Triggers:** Time-related puzzle mechanic

#### Strengthened Bridge (Future)
- **Requires:** Planks x4, Rope x2
- **Result:** Not an inventory item - repairs the bridge in-world
- **Triggers:** Opens new Overworld path

---

## Crafting UI Design (TODO)

### Layout
- Grid of available recipes on the left
- Selected recipe details on the right:
  - Item name and description
  - Required materials (with owned/needed counts)
  - Craft button (enabled/disabled based on materials)
- Simple, clean, pixel-art styled

### Interaction
- Navigate with arrow keys or mouse
- E or Enter to craft
- Escape to close
- Visual feedback: grayed out recipes you can't craft yet
- Sound: satisfying crafting "ding" on success

---

## Item Discovery
Items are found through:
1. **Pickup in world** - Objects on the ground or surfaces
2. **Examination** - Hidden in examinable objects (drawers, shelves)
3. **Crafting** - Created at the Forge
4. **Quest rewards** - Given by NPCs after helping them
5. **Mending** - Repairing broken things sometimes yields items
6. **Growing** - Harvesting plants

## Balance Notes
- Materials should be plentiful enough that crafting doesn't feel grindy
- Quest items should never be missable
- Inventory size (20) should be generous enough that management isn't a chore
- This is a cozy game - resource scarcity creates frustration, not fun
