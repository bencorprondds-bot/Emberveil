using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Visual UI for the inventory system.
/// Displays items in a grid layout with icons, names, and quantities.
///
/// Replaces the debug OnGUI display in InventoryManager.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("Root panel for the inventory")]
    [SerializeField] private GameObject inventoryPanel;

    [Tooltip("Container for inventory slots (should have GridLayoutGroup)")]
    [SerializeField] private Transform slotsContainer;

    [Tooltip("Prefab for individual inventory slots")]
    [SerializeField] private GameObject slotPrefab;

    [Header("Item Details Panel")]
    [Tooltip("Panel showing selected item details")]
    [SerializeField] private GameObject detailsPanel;

    [Tooltip("Text for item name in details")]
    [SerializeField] private Text itemNameText;

    [Tooltip("Text for item description")]
    [SerializeField] private Text itemDescriptionText;

    [Tooltip("Image for large item icon")]
    [SerializeField] private Image itemIconLarge;

    [Header("Settings")]
    [Tooltip("Key to toggle inventory")]
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;

    [Tooltip("Should game pause when inventory is open?")]
    [SerializeField] private bool pauseWhenOpen = false;

    [Header("Audio")]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private AudioClip selectSound;

    // State
    private bool isOpen = false;
    private List<InventorySlotUI> slotUIList = new List<InventorySlotUI>();
    private InventorySlot selectedSlot;
    private AudioSource audioSource;

    // Events
    public System.Action OnInventoryOpen;
    public System.Action OnInventoryClose;
    public System.Action<ItemData> OnItemSelected;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();

        // Start closed
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        if (detailsPanel != null)
        {
            detailsPanel.SetActive(false);
        }
    }

    void Start()
    {
        // Subscribe to inventory changes
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged += RefreshUI;
        }
    }

    void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged -= RefreshUI;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Toggle();
        }

        // Close with Escape
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    /// <summary>
    /// Toggle inventory open/closed
    /// </summary>
    public void Toggle()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    /// <summary>
    /// Open the inventory
    /// </summary>
    public void Open()
    {
        if (isOpen) return;

        // Don't open during dialogue or cutscenes
        if (GameManager.Instance != null)
        {
            GameState state = GameManager.Instance.GetGameState();
            if (state == GameState.Dialogue || state == GameState.Cutscene)
            {
                return;
            }
        }

        isOpen = true;

        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(true);
        }

        RefreshUI();

        // Clear selection
        ClearSelection();

        // Pause if configured
        if (pauseWhenOpen && GameManager.Instance != null)
        {
            GameManager.Instance.Pause();
        }

        // Play sound
        PlaySound(openSound);

        OnInventoryOpen?.Invoke();
    }

    /// <summary>
    /// Close the inventory
    /// </summary>
    public void Close()
    {
        if (!isOpen) return;

        isOpen = false;

        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        if (detailsPanel != null)
        {
            detailsPanel.SetActive(false);
        }

        // Resume if we paused
        if (pauseWhenOpen && GameManager.Instance != null)
        {
            GameManager.Instance.Resume();
        }

        // Play sound
        PlaySound(closeSound);

        OnInventoryClose?.Invoke();
    }

    /// <summary>
    /// Refresh the inventory display
    /// </summary>
    public void RefreshUI()
    {
        if (InventoryManager.Instance == null) return;
        if (slotsContainer == null) return;

        List<InventorySlot> items = InventoryManager.Instance.GetAllItems();

        // Ensure we have enough slot UIs
        while (slotUIList.Count < items.Count)
        {
            CreateSlotUI();
        }

        // Update existing slots
        for (int i = 0; i < slotUIList.Count; i++)
        {
            if (i < items.Count)
            {
                slotUIList[i].SetItem(items[i]);
                slotUIList[i].gameObject.SetActive(true);
            }
            else
            {
                slotUIList[i].ClearItem();
                slotUIList[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Create a new slot UI element
    /// </summary>
    private void CreateSlotUI()
    {
        if (slotPrefab == null || slotsContainer == null) return;

        GameObject slotObj = Instantiate(slotPrefab, slotsContainer);
        InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();

        if (slotUI == null)
        {
            slotUI = slotObj.AddComponent<InventorySlotUI>();
        }

        slotUI.OnSlotClicked += OnSlotClicked;
        slotUIList.Add(slotUI);
    }

    /// <summary>
    /// Handle slot click
    /// </summary>
    private void OnSlotClicked(InventorySlot slot)
    {
        if (slot == null || slot.item == null) return;

        selectedSlot = slot;

        // Show details panel
        ShowItemDetails(slot.item);

        // Update selection visual
        foreach (var slotUI in slotUIList)
        {
            slotUI.SetSelected(slotUI.GetSlot() == slot);
        }

        PlaySound(selectSound);
        OnItemSelected?.Invoke(slot.item);
    }

    /// <summary>
    /// Show item details in the details panel
    /// </summary>
    private void ShowItemDetails(ItemData item)
    {
        if (detailsPanel == null) return;

        detailsPanel.SetActive(true);

        if (itemNameText != null)
        {
            itemNameText.text = item.itemName;
        }

        if (itemDescriptionText != null)
        {
            itemDescriptionText.text = item.description;
        }

        if (itemIconLarge != null)
        {
            if (item.icon != null)
            {
                itemIconLarge.sprite = item.icon;
                itemIconLarge.gameObject.SetActive(true);
            }
            else
            {
                itemIconLarge.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Clear the current selection
    /// </summary>
    public void ClearSelection()
    {
        selectedSlot = null;

        if (detailsPanel != null)
        {
            detailsPanel.SetActive(false);
        }

        foreach (var slotUI in slotUIList)
        {
            slotUI.SetSelected(false);
        }
    }

    /// <summary>
    /// Get the currently selected item
    /// </summary>
    public ItemData GetSelectedItem()
    {
        return selectedSlot?.item;
    }

    /// <summary>
    /// Check if inventory is open
    /// </summary>
    public bool IsOpen()
    {
        return isOpen;
    }

    /// <summary>
    /// Play a UI sound
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

/// <summary>
/// Individual slot in the inventory UI grid.
/// Attach to the slot prefab.
/// </summary>
public class InventorySlotUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image iconImage;
    [SerializeField] private Text quantityText;
    [SerializeField] private GameObject selectedHighlight;

    private InventorySlot currentSlot;
    private Button button;

    public System.Action<InventorySlot> OnSlotClicked;

    void Awake()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }

        if (selectedHighlight != null)
        {
            selectedHighlight.SetActive(false);
        }
    }

    /// <summary>
    /// Set the item for this slot
    /// </summary>
    public void SetItem(InventorySlot slot)
    {
        currentSlot = slot;

        if (slot == null || slot.item == null)
        {
            ClearItem();
            return;
        }

        // Show icon
        if (iconImage != null)
        {
            if (slot.item.icon != null)
            {
                iconImage.sprite = slot.item.icon;
                iconImage.color = Color.white;
            }
            else
            {
                // Placeholder color based on item type
                iconImage.sprite = null;
                iconImage.color = GetItemTypeColor(slot.item.itemType);
            }

            iconImage.gameObject.SetActive(true);
        }

        // Show quantity
        if (quantityText != null)
        {
            if (slot.item.isStackable && slot.quantity > 1)
            {
                quantityText.text = slot.quantity.ToString();
                quantityText.gameObject.SetActive(true);
            }
            else
            {
                quantityText.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Clear this slot
    /// </summary>
    public void ClearItem()
    {
        currentSlot = null;

        if (iconImage != null)
        {
            iconImage.gameObject.SetActive(false);
        }

        if (quantityText != null)
        {
            quantityText.gameObject.SetActive(false);
        }

        SetSelected(false);
    }

    /// <summary>
    /// Set selected state
    /// </summary>
    public void SetSelected(bool selected)
    {
        if (selectedHighlight != null)
        {
            selectedHighlight.SetActive(selected);
        }
    }

    /// <summary>
    /// Get the current slot data
    /// </summary>
    public InventorySlot GetSlot()
    {
        return currentSlot;
    }

    /// <summary>
    /// Handle click
    /// </summary>
    private void OnClick()
    {
        OnSlotClicked?.Invoke(currentSlot);
    }

    /// <summary>
    /// Get a color based on item type for placeholder icons
    /// </summary>
    private Color GetItemTypeColor(ItemType type)
    {
        switch (type)
        {
            case ItemType.Material:
                return new Color(0.6f, 0.4f, 0.2f); // Brown
            case ItemType.Crafted:
                return new Color(0.8f, 0.6f, 0.2f); // Gold
            case ItemType.Quest:
                return new Color(0.8f, 0.2f, 0.8f); // Purple
            case ItemType.Consumable:
                return new Color(0.2f, 0.8f, 0.2f); // Green
            case ItemType.Gift:
                return new Color(0.8f, 0.4f, 0.6f); // Pink
            case ItemType.Key:
                return new Color(0.8f, 0.8f, 0.2f); // Yellow
            default:
                return Color.gray;
        }
    }
}
