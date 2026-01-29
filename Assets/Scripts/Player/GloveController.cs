using UnityEngine;

/// <summary>
/// Controls Mouse's Gloves - the context-sensitive interaction system.
/// The Gloves detect what Mouse is looking at and enable appropriate interactions.
/// Attach to the Mouse GameObject alongside PlayerController.
/// </summary>
public class GloveController : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("How far Mouse can interact with objects")]
    public float interactionRange = 1.5f;
    
    [Tooltip("Layer mask for interactable objects")]
    public LayerMask interactableLayer;
    
    [Header("Visual Feedback")]
    [Tooltip("Optional: Light component that glows when Gloves are active")]
    public Light2D gloveLight;
    
    [Tooltip("Color of the Glove glow")]
    public Color gloveGlowColor = new Color(0.2f, 0.8f, 0.6f, 1f); // Blue-green
    
    [Header("References")]
    public PlayerController playerController;
    
    // Internal state
    private Interactable currentTarget;
    private bool glovesActive = false;
    
    // Events other systems can subscribe to
    public System.Action<Interactable> OnTargetChanged;
    public System.Action<bool> OnGlovesActiveChanged;
    
    void Start()
    {
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
        
        // Set up the light if present
        if (gloveLight != null)
        {
            gloveLight.color = gloveGlowColor;
            gloveLight.enabled = false;
        }
    }
    
    void Update()
    {
        // Check for Glove activation (right mouse button or left trigger)
        bool gloveButtonHeld = Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftShift);
        
        if (gloveButtonHeld != glovesActive)
        {
            glovesActive = gloveButtonHeld;
            OnGlovesActiveChanged?.Invoke(glovesActive);
            
            if (gloveLight != null)
            {
                gloveLight.enabled = glovesActive;
            }
        }
        
        // Always scan for interactables when Gloves are active
        if (glovesActive)
        {
            ScanForInteractables();
        }
        else
        {
            // Clear target when Gloves are lowered
            if (currentTarget != null)
            {
                currentTarget.OnHoverExit();
                currentTarget = null;
                OnTargetChanged?.Invoke(null);
            }
        }
        
        // Handle interaction input (left click or E key when Gloves are active)
        if (glovesActive && currentTarget != null)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                currentTarget.Interact(this);
            }
        }
        
        // Quick interact without Gloves (just E key when near something)
        if (!glovesActive && Input.GetKeyDown(KeyCode.E))
        {
            TryQuickInteract();
        }
    }
    
    /// <summary>
    /// Casts a ray in the direction Mouse is facing to find interactables
    /// </summary>
    private void ScanForInteractables()
    {
        Vector2 origin = transform.position;
        Vector2 direction = playerController.FacingDirection;
        
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, interactionRange, interactableLayer);
        
        // Debug visualization
        Debug.DrawRay(origin, direction * interactionRange, hit.collider != null ? Color.green : Color.red);
        
        Interactable newTarget = null;
        
        if (hit.collider != null)
        {
            newTarget = hit.collider.GetComponent<Interactable>();
        }
        
        // Handle target changes
        if (newTarget != currentTarget)
        {
            // Exit old target
            if (currentTarget != null)
            {
                currentTarget.OnHoverExit();
            }
            
            // Enter new target
            currentTarget = newTarget;
            
            if (currentTarget != null)
            {
                currentTarget.OnHoverEnter();
            }
            
            OnTargetChanged?.Invoke(currentTarget);
        }
    }
    
    /// <summary>
    /// Attempts to interact with the nearest interactable without aiming
    /// </summary>
    private void TryQuickInteract()
    {
        // Find all colliders in range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);
        
        Interactable closest = null;
        float closestDistance = float.MaxValue;
        
        foreach (var col in colliders)
        {
            Interactable interactable = col.GetComponent<Interactable>();
            if (interactable != null)
            {
                float dist = Vector2.Distance(transform.position, col.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closest = interactable;
                }
            }
        }
        
        if (closest != null)
        {
            closest.Interact(this);
        }
    }
    
    /// <summary>
    /// Get the currently targeted interactable (if any)
    /// </summary>
    public Interactable GetCurrentTarget()
    {
        return currentTarget;
    }
    
    /// <summary>
    /// Check if Gloves are currently active
    /// </summary>
    public bool AreGlovesActive()
    {
        return glovesActive;
    }
}
