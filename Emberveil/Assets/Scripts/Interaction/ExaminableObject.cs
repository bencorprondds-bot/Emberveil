using UnityEngine;

/// <summary>
/// An object that can be examined for a description.
/// Simplest form of interaction - just displays text.
/// 
/// Good for world-building details, hints, and flavor text.
/// </summary>
public class ExaminableObject : Interactable
{
    [Header("Examine Settings")]
    [Tooltip("Text displayed when examined")]
    [TextArea(3, 6)]
    [SerializeField] private string examineText = "You see nothing special.";
    
    [Tooltip("Optional: Different text after first examination")]
    [TextArea(3, 6)]
    [SerializeField] private string examineTextAfterFirst = "";
    
    [Tooltip("Has this been examined before?")]
    private bool hasBeenExamined = false;
    
    protected override void Awake()
    {
        base.Awake();
        
        interactionType = InteractionType.Examine;
        interactionPrompt = "Examine";
    }
    
    public override void Interact(GloveController gloves)
    {
        if (!CanInteract()) return;
        
        string textToShow = examineText;
        
        // Use alternate text if available and already examined
        if (hasBeenExamined && !string.IsNullOrEmpty(examineTextAfterFirst))
        {
            textToShow = examineTextAfterFirst;
        }
        
        hasBeenExamined = true;
        
        // Show the examination text
        // TODO: Replace with proper UI when DialogueUI is implemented
        Debug.Log($"[EXAMINE] {textToShow}");
        
        // For now, also log to a future UI system
        // ExamineUI.Instance?.ShowText(textToShow);
    }
    
    /// <summary>
    /// Change the examine text at runtime (for story progression)
    /// </summary>
    public void SetExamineText(string newText)
    {
        examineText = newText;
    }
    
    /// <summary>
    /// Reset the examined state
    /// </summary>
    public void ResetExaminedState()
    {
        hasBeenExamined = false;
    }
}
