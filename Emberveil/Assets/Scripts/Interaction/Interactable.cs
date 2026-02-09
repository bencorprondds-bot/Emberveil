using UnityEngine;

/// <summary>
/// Base class for all interactable objects (HD-2D: uses Renderer for material-based highlight).
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

    [Tooltip("Color to tint the material when hovered")]
    [SerializeField] protected Color hoverTint = new Color(0.8f, 1f, 0.9f, 1f); // Slight green tint

    // References
    protected Renderer objectRenderer;
    private Color originalColor;
    private bool isHovered = false;

    // Material property ID for efficient access
    private static readonly int ColorProperty = Shader.PropertyToID("_Color");
    private static readonly int BaseColorProperty = Shader.PropertyToID("_BaseColor");

    protected virtual void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null && objectRenderer.material != null)
        {
            // Try URP _BaseColor first, fall back to _Color
            if (objectRenderer.material.HasProperty(BaseColorProperty))
            {
                originalColor = objectRenderer.material.GetColor(BaseColorProperty);
            }
            else if (objectRenderer.material.HasProperty(ColorProperty))
            {
                originalColor = objectRenderer.material.GetColor(ColorProperty);
            }
            else
            {
                originalColor = Color.white;
            }
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

        if (highlightOnHover && objectRenderer != null && isInteractable)
        {
            SetMaterialColor(hoverTint);
        }
    }

    /// <summary>
    /// Called when Mouse's Gloves stop targeting this object
    /// </summary>
    public virtual void OnHoverExit()
    {
        isHovered = false;

        if (objectRenderer != null)
        {
            SetMaterialColor(originalColor);
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
        if (!isInteractable && isHovered && objectRenderer != null)
        {
            SetMaterialColor(originalColor);
        }
    }

    /// <summary>
    /// Check if currently being hovered
    /// </summary>
    public bool IsHovered()
    {
        return isHovered;
    }

    /// <summary>
    /// Sets the material color, handling both URP and standard shaders
    /// </summary>
    private void SetMaterialColor(Color color)
    {
        if (objectRenderer == null || objectRenderer.material == null) return;

        if (objectRenderer.material.HasProperty(BaseColorProperty))
        {
            objectRenderer.material.SetColor(BaseColorProperty, color);
        }
        else if (objectRenderer.material.HasProperty(ColorProperty))
        {
            objectRenderer.material.SetColor(ColorProperty, color);
        }
    }
}
