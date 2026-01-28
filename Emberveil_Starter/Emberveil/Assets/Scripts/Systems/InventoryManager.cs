using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages Mouse's inventory - items she's carrying.
/// Uses ScriptableObject-based items for easy data-driven design.
/// 
/// Access via InventoryManager.Instance
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    [Header("Settings")]
    [Tooltip("Maximum number of items Mouse can carry")]
    [SerializeField] private int maxInventorySize = 20;
    
    [Header("Debug")]
    [SerializeField] private bool showDebugInventory = true;
    
    // The actual inventory
    private List<InventorySlot> inventory = new List<InventorySlot>();
    
    // Events
    public System.Action<ItemData> OnItemAdded;
    public System.Action<ItemData> OnItemRemoved;
    public System.Action OnInventoryChanged;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    /// <summary>
    /// Add an item to the inventory
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <param name="quantity">How many to add</param>
    /// <returns>True if successfully added</returns>
    public bool AddItem(ItemData item, int quantity = 1)
    {
        if (item == null) return false;
        
        // Check if we already have this item (and it's stackable)
        if (item.isStackable)
        {
            InventorySlot existingSlot = inventory.Find(s => s.item == item);
            if (existingSlot != null)
            {
                existingSlot.quantity += quantity;
                OnItemAdded?.Invoke(item);
                OnInventoryChanged?.Invoke();
                Debug.Log($"[Inventory] Added {quantity}x {item.itemName} (now have {existingSlot.quantity})");
                return true;
            }
        }
        
        // Check if we have room
        if (inventory.Count >= maxInventorySize)
        {
            Debug.Log($"[Inventory] Inventory full! Cannot add {item.itemName}");
            return false;
        }
        
        // Add new slot
        inventory.Add(new InventorySlot { item = item, quantity = quantity });
        OnItemAdded?.Invoke(item);
        OnInventoryChanged?.Invoke();
        Debug.Log($"[Inventory] Added {quantity}x {item.itemName}");
        return true;
    }
    
    /// <summary>
    /// Remove an item from the inventory
    /// </summary>
    /// <param name="item">The item to remove</param>
    /// <param name="quantity">How many to remove</param>
    /// <returns>True if successfully removed</returns>
    public bool RemoveItem(ItemData item, int quantity = 1)
    {
        if (item == null) return false;
        
        InventorySlot slot = inventory.Find(s => s.item == item);
        if (slot == null)
        {
            Debug.Log($"[Inventory] Don't have {item.itemName}");
            return false;
        }
        
        slot.quantity -= quantity;
        
        if (slot.quantity <= 0)
        {
            inventory.Remove(slot);
        }
        
        OnItemRemoved?.Invoke(item);
        OnInventoryChanged?.Invoke();
        Debug.Log($"[Inventory] Removed {quantity}x {item.itemName}");
        return true;
    }
    
    /// <summary>
    /// Check if we have at least a certain quantity of an item
    /// </summary>
    public bool HasItem(ItemData item, int quantity = 1)
    {
        if (item == null) return false;
        
        InventorySlot slot = inventory.Find(s => s.item == item);
        return slot != null && slot.quantity >= quantity;
    }
    
    /// <summary>
    /// Get how many of an item we have
    /// </summary>
    public int GetItemCount(ItemData item)
    {
        if (item == null) return 0;
        
        InventorySlot slot = inventory.Find(s => s.item == item);
        return slot?.quantity ?? 0;
    }
    
    /// <summary>
    /// Get all items in the inventory
    /// </summary>
    public List<InventorySlot> GetAllItems()
    {
        return new List<InventorySlot>(inventory);
    }
    
    /// <summary>
    /// Clear the entire inventory
    /// </summary>
    public void ClearInventory()
    {
        inventory.Clear();
        OnInventoryChanged?.Invoke();
    }
    
    void Update()
    {
        // Toggle inventory display with Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            showDebugInventory = !showDebugInventory;
        }
    }
    
    void OnGUI()
    {
        if (!showDebugInventory) return;
        
        // Simple debug inventory display
        GUILayout.BeginArea(new Rect(Screen.width - 210, 10, 200, 300));
        GUILayout.BeginVertical("box");
        
        GUILayout.Label("=== INVENTORY ===");
        
        if (inventory.Count == 0)
        {
            GUILayout.Label("(empty)");
        }
        else
        {
            foreach (var slot in inventory)
            {
                string label = slot.item.isStackable 
                    ? $"{slot.item.itemName} x{slot.quantity}"
                    : slot.item.itemName;
                GUILayout.Label(label);
            }
        }
        
        GUILayout.Label($"\n{inventory.Count}/{maxInventorySize} slots");
        GUILayout.Label("[Tab to toggle]");
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}

/// <summary>
/// Represents a slot in the inventory (item + quantity)
/// </summary>
[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;
}
