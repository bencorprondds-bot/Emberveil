using UnityEngine;

/// <summary>
/// An NPC that Mouse can talk to (HD-2D: Y-axis rotation for facing).
/// Integrates with Yarn Spinner for dialogue.
///
/// Attach to NPCs like Hawk, Bear, Clover, etc.
/// </summary>
public class TalkableNPC : Interactable
{
    [Header("NPC Settings")]
    [Tooltip("Display name for this character")]
    [SerializeField] private string characterName = "NPC";

    [Tooltip("Portrait sprite for dialogue UI")]
    [SerializeField] private Sprite portrait;

    [Header("Dialogue")]
    [Tooltip("The Yarn node to start when talking to this NPC")]
    [SerializeField] private string dialogueNodeName = "Start";

    [Tooltip("Reference to the Yarn Project asset (assign in inspector)")]
    // Note: Uncomment when Yarn Spinner is installed
    // [SerializeField] private YarnProject yarnProject;

    [Header("Behavior")]
    [Tooltip("Should this NPC turn to face Mouse when talked to?")]
    [SerializeField] private bool facePlayerOnTalk = true;

    [Tooltip("Should Mouse freeze during dialogue?")]
    [SerializeField] private bool freezePlayerDuringDialogue = true;

    // State
    private bool isInDialogue = false;
    private PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();

        interactionType = InteractionType.Talk;
        interactionPrompt = $"Talk to {characterName}";
    }

    public override void Interact(GloveController gloves)
    {
        if (!CanInteract() || isInDialogue) return;

        StartDialogue(gloves);
    }

    /// <summary>
    /// Begin the dialogue sequence
    /// </summary>
    private void StartDialogue(GloveController gloves)
    {
        isInDialogue = true;

        // Get player controller for freezing movement
        playerController = gloves.GetComponent<PlayerController>();

        if (freezePlayerDuringDialogue && playerController != null)
        {
            playerController.DisableMovement();
        }

        // Face the player
        if (facePlayerOnTalk)
        {
            FacePlayer(gloves.transform);
        }

        // Start Yarn dialogue
        // Note: Uncomment when Yarn Spinner is installed and DialogueRunner is set up
        /*
        DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
        if (dialogueRunner != null)
        {
            dialogueRunner.StartDialogue(dialogueNodeName);
            dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
        }
        else
        {
            Debug.LogWarning("No DialogueRunner found in scene!");
            OnDialogueComplete();
        }
        */

        // TEMPORARY: For testing without Yarn Spinner installed
        Debug.Log($"[DIALOGUE] {characterName}: Starting node '{dialogueNodeName}'");
        Debug.Log($"[DIALOGUE] Press Space to end dialogue (placeholder)");

        // Placeholder - end dialogue on space press (remove when Yarn is set up)
        StartCoroutine(PlaceholderDialogue());
    }

    /// <summary>
    /// Temporary placeholder until Yarn Spinner is installed
    /// </summary>
    private System.Collections.IEnumerator PlaceholderDialogue()
    {
        // Show some placeholder text
        Debug.Log($"[{characterName}]: Hello there, Mouse!");

        yield return new WaitForSeconds(0.5f);

        // Wait for space to continue
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        Debug.Log($"[{characterName}]: It's good to see you.");

        yield return new WaitForSeconds(0.5f);

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        OnDialogueComplete();
    }

    /// <summary>
    /// Called when dialogue ends (by Yarn Spinner or placeholder)
    /// </summary>
    private void OnDialogueComplete()
    {
        isInDialogue = false;

        if (freezePlayerDuringDialogue && playerController != null)
        {
            playerController.EnableMovement();
        }

        Debug.Log($"[DIALOGUE] Conversation with {characterName} ended.");
    }

    /// <summary>
    /// Rotate to face the player (HD-2D: rotate around Y axis)
    /// </summary>
    private void FacePlayer(Transform player)
    {
        // In HD-2D, face the player by rotating around Y axis
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f; // Keep rotation on XZ plane only

        if (directionToPlayer.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }
    }

    /// <summary>
    /// Get this NPC's display name
    /// </summary>
    public string GetCharacterName()
    {
        return characterName;
    }

    /// <summary>
    /// Get this NPC's portrait for dialogue UI
    /// </summary>
    public Sprite GetPortrait()
    {
        return portrait;
    }

    /// <summary>
    /// Change which dialogue node plays (for story progression)
    /// </summary>
    public void SetDialogueNode(string nodeName)
    {
        dialogueNodeName = nodeName;
        interactionPrompt = $"Talk to {characterName}";
    }

    /// <summary>
    /// Check if currently in dialogue
    /// </summary>
    public bool IsInDialogue()
    {
        return isInDialogue;
    }
}
