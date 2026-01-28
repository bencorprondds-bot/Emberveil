/// <summary>
/// Interface for all interactable objects in the game.
/// Implement this on any object Mouse can interact with using her Gloves.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// The type of interaction this object supports.
    /// Used by the UI to show the correct prompt/icon.
    /// </summary>
    InteractionType GetInteractionType();
    
    /// <summary>
    /// Called when Mouse's Gloves target this object
    /// </summary>
    void OnHoverEnter();
    
    /// <summary>
    /// Called when Mouse's Gloves stop targeting this object
    /// </summary>
    void OnHoverExit();
    
    /// <summary>
    /// Called when Mouse activates the interaction
    /// </summary>
    /// <param name="gloves">Reference to the GloveController for callbacks</param>
    void Interact(GloveController gloves);
    
    /// <summary>
    /// Whether this object can currently be interacted with
    /// </summary>
    bool CanInteract();
}

/// <summary>
/// Types of interactions the Gloves can perform.
/// Each type may show a different cursor/prompt.
/// </summary>
public enum InteractionType
{
    None,       // Not interactable
    Examine,    // Look at / get description
    Talk,       // Start dialogue
    Lift,       // Pick up and move
    Mend,       // Repair broken objects
    Grow,       // Accelerate plant growth
    Scan,       // Reveal hidden information (late game)
    Use         // Generic use (doors, switches, etc.)
}
