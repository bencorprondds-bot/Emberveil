using UnityEngine;

/// <summary>
/// An item in the world that can be picked up and added to inventory.
///
/// Use for crafting materials, quest items, consumables, etc.
/// The item disappears after being collected.
/// </summary>
public class PickupItem : Interactable
{
    [Header("Item Settings")]
    [Tooltip("The item data to add to inventory")]
    [SerializeField] private ItemData itemData;

    [Tooltip("How many of this item to give")]
    [SerializeField] private int quantity = 1;

    [Header("Behavior")]
    [Tooltip("Should this item respawn after being collected?")]
    [SerializeField] private bool respawns = false;

    [Tooltip("Time until respawn (if respawns is true)")]
    [SerializeField] private float respawnTime = 60f;

    [Tooltip("Should item bob up and down?")]
    [SerializeField] private bool animateBob = true;

    [Tooltip("Bob animation height")]
    [SerializeField] private float bobHeight = 0.1f;

    [Tooltip("Bob animation speed")]
    [SerializeField] private float bobSpeed = 2f;

    [Header("Effects")]
    [Tooltip("Sound when picked up")]
    [SerializeField] private AudioClip pickupSound;

    [Tooltip("Particle effect on pickup")]
    [SerializeField] private ParticleSystem pickupParticles;

    [Tooltip("Should item spin toward player on pickup?")]
    [SerializeField] private bool animatePickup = true;

    [Tooltip("Duration of pickup animation")]
    [SerializeField] private float pickupAnimationDuration = 0.3f;

    // State
    private bool isCollected = false;
    private Vector3 originalPosition;
    private float bobTimer = 0f;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        interactionType = InteractionType.Use;

        // Set prompt based on item
        if (itemData != null)
        {
            interactionPrompt = $"Pick up {itemData.itemName}";
        }
        else
        {
            interactionPrompt = "Pick up";
        }

        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>();

        // Randomize bob phase so multiple items don't sync
        bobTimer = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        if (isCollected) return;

        // Bob animation
        if (animateBob)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            Vector3 pos = originalPosition;
            pos.y += Mathf.Sin(bobTimer) * bobHeight;
            transform.position = pos;
        }
    }

    public override void Interact(GloveController gloves)
    {
        if (!CanInteract() || isCollected) return;

        Collect(gloves.transform);
    }

    public override bool CanInteract()
    {
        return base.CanInteract() && !isCollected && itemData != null;
    }

    /// <summary>
    /// Collect this item
    /// </summary>
    /// <param name="collector">Transform of who collected it (for animation)</param>
    private void Collect(Transform collector)
    {
        if (itemData == null)
        {
            Debug.LogWarning("PickupItem has no ItemData assigned!");
            return;
        }

        // Try to add to inventory
        if (InventoryManager.Instance != null)
        {
            if (!InventoryManager.Instance.AddItem(itemData, quantity))
            {
                // Inventory full
                Debug.Log($"Cannot pick up {itemData.itemName} - inventory full!");
                return;
            }
        }
        else
        {
            Debug.Log($"[Pickup] Collected {quantity}x {itemData.itemName} (no InventoryManager)");
        }

        isCollected = true;

        // Play sound
        if (pickupSound != null)
        {
            // Play at position so it persists after object is disabled
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        // Spawn particles
        if (pickupParticles != null)
        {
            pickupParticles.Play();
            pickupParticles.transform.SetParent(null); // Detach so it persists
        }

        // Animate or immediately hide
        if (animatePickup && collector != null)
        {
            StartCoroutine(PickupAnimation(collector));
        }
        else
        {
            HideItem();
        }
    }

    /// <summary>
    /// Animate the item flying toward the collector
    /// </summary>
    private System.Collections.IEnumerator PickupAnimation(Transform target)
    {
        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        // Disable collider during animation
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        while (elapsed < pickupAnimationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / pickupAnimationDuration;

            // Ease out
            float easedT = 1f - Mathf.Pow(1f - t, 3f);

            // Move toward target
            transform.position = Vector3.Lerp(startPos, target.position, easedT);

            // Shrink
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, easedT);

            // Spin
            transform.Rotate(0f, 0f, 720f * Time.deltaTime);

            yield return null;
        }

        HideItem();
    }

    /// <summary>
    /// Hide the item after collection
    /// </summary>
    private void HideItem()
    {
        if (respawns)
        {
            // Disable and schedule respawn
            gameObject.SetActive(false);
            StartCoroutine(RespawnAfterDelay());
        }
        else
        {
            // Destroy permanently
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Respawn the item after delay
    /// </summary>
    private System.Collections.IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);

        // Reset state
        isCollected = false;
        transform.position = originalPosition;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        // Re-enable collider
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = true;

        gameObject.SetActive(true);
    }

    /// <summary>
    /// Set the item data at runtime
    /// </summary>
    public void SetItemData(ItemData data, int qty = 1)
    {
        itemData = data;
        quantity = qty;

        if (itemData != null)
        {
            interactionPrompt = $"Pick up {itemData.itemName}";

            // Update sprite if item has icon
            if (spriteRenderer != null && itemData.icon != null)
            {
                spriteRenderer.sprite = itemData.icon;
            }
        }
    }

    /// <summary>
    /// Get the item data
    /// </summary>
    public ItemData GetItemData()
    {
        return itemData;
    }

    /// <summary>
    /// Check if already collected
    /// </summary>
    public bool IsCollected()
    {
        return isCollected;
    }

    /// <summary>
    /// Force respawn (for puzzles that reset)
    /// </summary>
    public void ForceRespawn()
    {
        StopAllCoroutines();
        isCollected = false;
        transform.position = originalPosition;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = true;
    }

    void OnDrawGizmos()
    {
        // Show item in editor
        if (itemData != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.3f);
        }
    }
}

/// <summary>
/// Spawns pickup items at runtime.
/// Use for drops, rewards, etc.
/// </summary>
public static class PickupSpawner
{
    /// <summary>
    /// Spawn a pickup item at a position
    /// </summary>
    /// <param name="itemData">The item to spawn</param>
    /// <param name="position">World position</param>
    /// <param name="quantity">How many</param>
    /// <param name="pickupPrefab">Optional prefab to use (otherwise creates basic)</param>
    /// <returns>The spawned pickup</returns>
    public static PickupItem SpawnPickup(ItemData itemData, Vector3 position, int quantity = 1, GameObject pickupPrefab = null)
    {
        if (itemData == null) return null;

        GameObject obj;

        if (pickupPrefab != null)
        {
            obj = Object.Instantiate(pickupPrefab, position, Quaternion.identity);
        }
        else
        {
            // Create basic pickup object
            obj = new GameObject($"Pickup_{itemData.itemName}");
            obj.transform.position = position;

            // Add sprite renderer
            SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
            if (itemData.icon != null)
            {
                sr.sprite = itemData.icon;
            }
            else
            {
                // Create placeholder sprite
                sr.color = Color.yellow;
            }

            // Add collider
            BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = new Vector2(0.5f, 0.5f);

            // Set layer to Interactable (if it exists)
            int layer = LayerMask.NameToLayer("Interactable");
            if (layer >= 0)
            {
                obj.layer = layer;
            }
        }

        // Configure pickup component
        PickupItem pickup = obj.GetComponent<PickupItem>();
        if (pickup == null)
        {
            pickup = obj.AddComponent<PickupItem>();
        }

        pickup.SetItemData(itemData, quantity);

        return pickup;
    }

    /// <summary>
    /// Spawn multiple pickups in a burst pattern
    /// </summary>
    public static void SpawnPickupBurst(ItemData itemData, Vector3 position, int quantity, float spread = 1f)
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-spread, spread),
                Random.Range(-spread, spread),
                0f
            );

            SpawnPickup(itemData, position + offset, 1);
        }
    }
}
