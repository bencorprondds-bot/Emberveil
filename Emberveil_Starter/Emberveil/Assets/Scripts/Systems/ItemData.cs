using UnityEngine;

/// <summary>
/// ScriptableObject that defines an item's properties.
/// 
/// To create a new item:
/// 1. Right-click in Project window
/// 2. Create > Emberveil > Item
/// 3. Fill in the details
/// 
/// Example items: Hawk's Glasses, Metal Frames, Lenses, Herbs, etc.
/// </summary>
[CreateAssetMenu(fileName = "NewItem", menuName = "Emberveil/Item")]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    [Tooltip("Display name")]
    public string itemName = "New Item";
    
    [Tooltip("Description shown in inventory")]
    [TextArea(2, 4)]
    public string description = "An item.";
    
    [Tooltip("Icon for inventory UI")]
    public Sprite icon;
    
    [Header("Properties")]
    [Tooltip("Can multiple of this item stack in one slot?")]
    public bool isStackable = true;
    
    [Tooltip("Maximum stack size (if stackable)")]
    public int maxStackSize = 99;
    
    [Tooltip("What type of item is this?")]
    public ItemType itemType = ItemType.Material;
    
    [Tooltip("Can this item be used/consumed?")]
    public bool isUsable = false;
    
    [Header("Forge Recipe (if craftable)")]
    [Tooltip("Is this item crafted at the Forge?")]
    public bool isCraftable = false;
    
    [Tooltip("Items required to craft this")]
    public CraftingIngredient[] craftingRecipe;
    
    [Header("Value")]
    [Tooltip("For future trading/shops")]
    public int baseValue = 0;
    
    /// <summary>
    /// Check if we have all ingredients to craft this item
    /// </summary>
    public bool CanCraft()
    {
        if (!isCraftable || craftingRecipe == null) return false;
        
        foreach (var ingredient in craftingRecipe)
        {
            if (!InventoryManager.Instance.HasItem(ingredient.item, ingredient.quantity))
            {
                return false;
            }
        }
        
        return true;
    }
    
    /// <summary>
    /// Attempt to craft this item (consumes ingredients)
    /// </summary>
    public bool TryCraft()
    {
        if (!CanCraft()) return false;
        
        // Remove ingredients
        foreach (var ingredient in craftingRecipe)
        {
            InventoryManager.Instance.RemoveItem(ingredient.item, ingredient.quantity);
        }
        
        // Add crafted item
        InventoryManager.Instance.AddItem(this, 1);
        
        return true;
    }
}

/// <summary>
/// Types of items in the game
/// </summary>
public enum ItemType
{
    Material,       // Raw crafting materials
    Crafted,        // Items made at the Forge
    Quest,          // Quest-related items (can't be discarded)
    Consumable,     // Items that can be used/eaten
    Gift,           // Items to give to NPCs
    Key             // Keys/access items
}

/// <summary>
/// Represents an ingredient in a crafting recipe
/// </summary>
[System.Serializable]
public class CraftingIngredient
{
    public ItemData item;
    public int quantity = 1;
}
