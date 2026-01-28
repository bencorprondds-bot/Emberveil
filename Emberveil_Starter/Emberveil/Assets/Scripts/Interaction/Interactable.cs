using UnityEngine;

/// <summary>
/// Base class for all interactable objects.
/// Inherit from this to create specific interaction types.
/// Provides common functionality like hover highlighting.
/// </summary>
public abstract class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interaction Settings")]
    [Tooltip("What type of interaction this object supports")]
    [SerializeField] protected InteractionType interactionType = InteractionType.Examine;
    
    [Tooltip("Text shown when Mouse targets this object")]
    [SerializeField] protected string interactionPrompt = "Examine";
    
    [Tooltip("Can this object currently be interacted with?")]
    [SerializeField] protected bool isInteractable = true;
    
    [Header("Hover Feedback")]
    [Tooltip("Should this object highlight when targeted?")]
    [SerializeField] protected bool highlightOnHover = true;
    
    [Tooltip("Color to tint the sprite when hovered")]
    [SerializeField] protected Color hoverTint = new Color(0.8f, 1f, 0.9f, 1f); // Slight green tint
    
    // References
    protected SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isHovered = false;
    
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }
    
    /// <summary>
    /// Returns the type of interaction (Lift, Talk, etc.)
    /// </summary>
    public virtual InteractionType GetInteractionType()
    {
        return interactionType;
    }
    
    /// <summary>
    /// Returns the prompt text (e.g., "Pick up", "Talk to Hawk")
    /// </summary>
    public virtual string GetInteractionPrompt()
    {
        return interactionPrompt;
    }
    
    /// <summary>
    /// Called when Mouse's Gloves start targeting this object
    /// </summary>
    public virtual void OnHoverEnter()
    {
        isHovered = true;
        
        if (highlightOnHover && spriteRenderer != null && isInteractable)
        {
            spriteRenderer.color = hoverTint;
        }
    }
    
    /// <summary>
    /// Called when Mouse's Gloves stop targeting this object
    /// </summary>
    public virtual void OnHoverExit()
    {
        isHovered = false;
        
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
    
    /// <summary>
    /// Called when Mouse activates the interaction.
    /// Override this in derived classes to implement specific behavior.
    /// </summary>
    public abstract void Interact(GloveController gloves);
    
    /// <summary>
    /// Whether this object can currently be interacted with
    /// </summary>
    public virtual bool CanInteract()
    {
        return isInteractable;
    }
    
    /// <summary>
    /// Enable or disable interaction at runtime
    /// </summary>
    public void SetInteractable(bool value)
    {
        isInteractable = value;
        
        // If we're currently hovered and became non-interactable, remove highlight
        if (!isInteractable && isHovered && spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
    
    /// <summary>
    /// Check if currently being hovered
    /// </summary>
    public bool IsHovered()
    {
        return isHovered;
    }
}
