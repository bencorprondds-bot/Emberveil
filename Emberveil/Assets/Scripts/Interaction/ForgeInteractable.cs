using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// The Forge - Mouse's ancient crafting station in the Workshop.
/// Allows crafting items from materials using recipes defined in ItemData.
///
/// From the source material: The Forge is where Mouse creates and repairs
/// items using her Gloves. It's an ancient piece of technology that responds
/// to her touch.
/// </summary>
public class ForgeInteractable : Interactable
{
    [Header("Forge Settings")]
    [Tooltip("All items that can be crafted at this Forge")]
    [SerializeField] private List<ItemData> availableRecipes = new List<ItemData>();

    [Tooltip("Sound when Forge activates")]
    [SerializeField] private AudioClip forgeActivateSound;

    [Tooltip("Sound when crafting succeeds")]
    [SerializeField] private AudioClip craftSuccessSound;

    [Tooltip("Sound when crafting fails (missing materials)")]
    [SerializeField] private AudioClip craftFailSound;

    [Header("Visual Effects")]
    [Tooltip("Particle system for forge activation")]
    [SerializeField] private ParticleSystem forgeParticles;

    [Tooltip("Light that glows when Forge is active")]
    [SerializeField] private Light2D forgeLight;

    [Tooltip("Color of forge glow")]
    [SerializeField] private Color forgeGlowColor = new Color(1f, 0.5f, 0.2f);

    [Header("UI References (Optional - can use ForgeUI instead)")]
    [SerializeField] private GameObject craftingMenuPanel;

    // State
    private bool isForgeActive = false;
    private PlayerController playerController;
    private AudioSource audioSource;

    // Events
    public System.Action OnForgeOpened;
    public System.Action OnForgeClosed;
    public System.Action<ItemData> OnItemCrafted;

    protected override void Awake()
    {
        base.Awake();

        interactionType = InteractionType.Use;
        interactionPrompt = "Use Forge";

        audioSource = GetComponent<AudioSource>();

        // Start with effects off
        if (forgeLight != null)
        {
            forgeLight.enabled = false;
        }

        if (craftingMenuPanel != null)
        {
            craftingMenuPanel.SetActive(false);
        }
    }

    public override void Interact(GloveController gloves)
    {
        if (!CanInteract()) return;

        playerController = gloves.GetComponent<PlayerController>();

        if (!isForgeActive)
        {
            OpenForge();
        }
        else
        {
            CloseForge();
        }
    }

    /// <summary>
    /// Activate the Forge and show crafting options
    /// </summary>
    private void OpenForge()
    {
        isForgeActive = true;

        // Freeze player
        if (playerController != null)
        {
            playerController.DisableMovement();
        }

        // Visual effects
        if (forgeLight != null)
        {
            forgeLight.enabled = true;
            forgeLight.color = forgeGlowColor;
        }

        if (forgeParticles != null)
        {
            forgeParticles.Play();
        }

        // Play sound
        if (audioSource != null && forgeActivateSound != null)
        {
            audioSource.PlayOneShot(forgeActivateSound);
        }

        // Show UI
        if (craftingMenuPanel != null)
        {
            craftingMenuPanel.SetActive(true);
            PopulateCraftingMenu();
        }
        else if (ForgeUI.Instance != null)
        {
            ForgeUI.Instance.Open(this);
        }
        else
        {
            // Fallback: Debug log available recipes
            ShowDebugCraftingMenu();
        }

        // Update game state
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetGameState(GameState.Menu);
        }

        OnForgeOpened?.Invoke();
    }

    /// <summary>
    /// Deactivate the Forge
    /// </summary>
    public void CloseForge()
    {
        isForgeActive = false;

        // Unfreeze player
        if (playerController != null)
        {
            playerController.EnableMovement();
        }

        // Turn off effects
        if (forgeLight != null)
        {
            forgeLight.enabled = false;
        }

        if (forgeParticles != null)
        {
            forgeParticles.Stop();
        }

        // Hide UI
        if (craftingMenuPanel != null)
        {
            craftingMenuPanel.SetActive(false);
        }
        else if (ForgeUI.Instance != null)
        {
            ForgeUI.Instance.Close();
        }

        // Restore game state
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetGameState(GameState.Playing);
        }

        OnForgeClosed?.Invoke();
    }

    /// <summary>
    /// Attempt to craft an item
    /// </summary>
    /// <param name="item">The item to craft</param>
    /// <returns>True if crafting succeeded</returns>
    public bool TryCraft(ItemData item)
    {
        if (item == null) return false;
        if (!item.isCraftable) return false;

        if (item.CanCraft())
        {
            // Craft the item (this consumes ingredients and adds result)
            if (item.TryCraft())
            {
                // Success effects
                if (audioSource != null && craftSuccessSound != null)
                {
                    audioSource.PlayOneShot(craftSuccessSound);
                }

                // Burst of particles
                if (forgeParticles != null)
                {
                    forgeParticles.Emit(20);
                }

                Debug.Log($"[Forge] Crafted: {item.itemName}");
                OnItemCrafted?.Invoke(item);

                return true;
            }
        }

        // Failed - missing ingredients
        if (audioSource != null && craftFailSound != null)
        {
            audioSource.PlayOneShot(craftFailSound);
        }

        Debug.Log($"[Forge] Cannot craft {item.itemName} - missing materials");
        return false;
    }

    /// <summary>
    /// Get list of craftable items (items player has materials for)
    /// </summary>
    public List<ItemData> GetCraftableItems()
    {
        List<ItemData> craftable = new List<ItemData>();

        foreach (var item in availableRecipes)
        {
            if (item != null && item.isCraftable && item.CanCraft())
            {
                craftable.Add(item);
            }
        }

        return craftable;
    }

    /// <summary>
    /// Get all available recipes (regardless of materials)
    /// </summary>
    public List<ItemData> GetAllRecipes()
    {
        return new List<ItemData>(availableRecipes);
    }

    /// <summary>
    /// Add a recipe to this Forge (for unlocking new recipes)
    /// </summary>
    public void AddRecipe(ItemData item)
    {
        if (item != null && !availableRecipes.Contains(item))
        {
            availableRecipes.Add(item);
        }
    }

    /// <summary>
    /// Populate the built-in crafting menu (if using one)
    /// </summary>
    private void PopulateCraftingMenu()
    {
        // This would be implemented based on your UI prefab structure
        // For now, we rely on ForgeUI or debug output
    }

    /// <summary>
    /// Debug fallback when no UI is set up
    /// </summary>
    private void ShowDebugCraftingMenu()
    {
        Debug.Log("=== FORGE CRAFTING MENU ===");
        Debug.Log("Available recipes:");

        foreach (var item in availableRecipes)
        {
            if (item == null) continue;

            string status = item.CanCraft() ? "[CAN CRAFT]" : "[MISSING MATERIALS]";
            Debug.Log($"  - {item.itemName} {status}");

            if (item.craftingRecipe != null)
            {
                foreach (var ingredient in item.craftingRecipe)
                {
                    if (ingredient.item != null)
                    {
                        int have = InventoryManager.Instance.GetItemCount(ingredient.item);
                        Debug.Log($"      Requires: {ingredient.quantity}x {ingredient.item.itemName} (have {have})");
                    }
                }
            }
        }

        Debug.Log("Press E to close Forge, or use ForgeUI for proper interface");
    }

    void Update()
    {
        // Allow closing with E or Escape when active
        if (isForgeActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                CloseForge();
            }
        }
    }

    /// <summary>
    /// Check if Forge is currently active
    /// </summary>
    public bool IsForgeActive()
    {
        return isForgeActive;
    }
}

/// <summary>
/// Dedicated UI for the Forge crafting system.
/// Create this as a separate UI canvas in your scene.
/// </summary>
public class ForgeUI : MonoBehaviour
{
    public static ForgeUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject forgePanel;
    [SerializeField] private Transform recipeListContainer;
    [SerializeField] private GameObject recipeButtonPrefab;

    [Header("Recipe Details")]
    [SerializeField] private Text recipeNameText;
    [SerializeField] private Text recipeDescriptionText;
    [SerializeField] private Transform ingredientsContainer;
    [SerializeField] private GameObject ingredientRowPrefab;
    [SerializeField] private Button craftButton;
    [SerializeField] private Text craftButtonText;

    private ForgeInteractable currentForge;
    private ItemData selectedRecipe;
    private List<GameObject> recipeButtons = new List<GameObject>();
    private List<GameObject> ingredientRows = new List<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (forgePanel != null)
        {
            forgePanel.SetActive(false);
        }
    }

    /// <summary>
    /// Open the Forge UI
    /// </summary>
    public void Open(ForgeInteractable forge)
    {
        currentForge = forge;

        if (forgePanel != null)
        {
            forgePanel.SetActive(true);
        }

        PopulateRecipeList();
        ClearSelection();
    }

    /// <summary>
    /// Close the Forge UI
    /// </summary>
    public void Close()
    {
        if (forgePanel != null)
        {
            forgePanel.SetActive(false);
        }

        currentForge = null;
        selectedRecipe = null;
    }

    /// <summary>
    /// Populate the recipe list
    /// </summary>
    private void PopulateRecipeList()
    {
        // Clear existing buttons
        foreach (var btn in recipeButtons)
        {
            Destroy(btn);
        }
        recipeButtons.Clear();

        if (currentForge == null || recipeButtonPrefab == null || recipeListContainer == null)
        {
            return;
        }

        foreach (var recipe in currentForge.GetAllRecipes())
        {
            if (recipe == null) continue;

            GameObject btnObj = Instantiate(recipeButtonPrefab, recipeListContainer);
            recipeButtons.Add(btnObj);

            // Set up button text
            Text btnText = btnObj.GetComponentInChildren<Text>();
            if (btnText != null)
            {
                btnText.text = recipe.itemName;

                // Gray out if can't craft
                if (!recipe.CanCraft())
                {
                    btnText.color = Color.gray;
                }
            }

            // Set up button click
            Button btn = btnObj.GetComponent<Button>();
            if (btn != null)
            {
                ItemData capturedRecipe = recipe;
                btn.onClick.AddListener(() => SelectRecipe(capturedRecipe));
            }
        }
    }

    /// <summary>
    /// Select a recipe to view details
    /// </summary>
    private void SelectRecipe(ItemData recipe)
    {
        selectedRecipe = recipe;

        if (recipeNameText != null)
        {
            recipeNameText.text = recipe.itemName;
        }

        if (recipeDescriptionText != null)
        {
            recipeDescriptionText.text = recipe.description;
        }

        // Show ingredients
        PopulateIngredients(recipe);

        // Update craft button
        if (craftButton != null)
        {
            craftButton.interactable = recipe.CanCraft();
        }

        if (craftButtonText != null)
        {
            craftButtonText.text = recipe.CanCraft() ? "Craft" : "Missing Materials";
        }
    }

    /// <summary>
    /// Populate ingredient list for selected recipe
    /// </summary>
    private void PopulateIngredients(ItemData recipe)
    {
        // Clear existing rows
        foreach (var row in ingredientRows)
        {
            Destroy(row);
        }
        ingredientRows.Clear();

        if (recipe.craftingRecipe == null || ingredientRowPrefab == null || ingredientsContainer == null)
        {
            return;
        }

        foreach (var ingredient in recipe.craftingRecipe)
        {
            if (ingredient.item == null) continue;

            GameObject rowObj = Instantiate(ingredientRowPrefab, ingredientsContainer);
            ingredientRows.Add(rowObj);

            Text rowText = rowObj.GetComponentInChildren<Text>();
            if (rowText != null)
            {
                int have = InventoryManager.Instance.GetItemCount(ingredient.item);
                rowText.text = $"{ingredient.item.itemName}: {have}/{ingredient.quantity}";
                rowText.color = have >= ingredient.quantity ? Color.white : Color.red;
            }
        }
    }

    /// <summary>
    /// Clear the current selection
    /// </summary>
    private void ClearSelection()
    {
        selectedRecipe = null;

        if (recipeNameText != null) recipeNameText.text = "Select a Recipe";
        if (recipeDescriptionText != null) recipeDescriptionText.text = "";
        if (craftButton != null) craftButton.interactable = false;

        foreach (var row in ingredientRows)
        {
            Destroy(row);
        }
        ingredientRows.Clear();
    }

    /// <summary>
    /// Called by Craft button
    /// </summary>
    public void OnCraftButtonClicked()
    {
        if (currentForge != null && selectedRecipe != null)
        {
            if (currentForge.TryCraft(selectedRecipe))
            {
                // Refresh UI after successful craft
                PopulateRecipeList();
                SelectRecipe(selectedRecipe);
            }
        }
    }
}
