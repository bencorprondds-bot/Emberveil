using UnityEngine;

/// <summary>
/// An object that Mouse can lift and move using her Gloves (HD-2D: 3D colliders).
/// Used for puzzles involving moving crates, furniture, etc.
///
/// From the source material: "Mouse claps to lift it" - the Gloves allow
/// telekinetic-style movement of heavy objects.
/// </summary>
public class LiftableObject : Interactable
{
    [Header("Lift Settings")]
    [Tooltip("How high the object floats when lifted")]
    [SerializeField] private float liftHeight = 0.5f;

    [Tooltip("How fast the object moves when being carried")]
    [SerializeField] private float followSpeed = 8f;

    [Tooltip("How fast the object rises/falls")]
    [SerializeField] private float liftSpeed = 5f;

    [Tooltip("Does this object require a companion to lift?")]
    [SerializeField] private bool requiresCompanion = false;

    [Tooltip("Which companion is required (if any)")]
    [SerializeField] private string requiredCompanionId = "";

    [Header("Audio (Optional)")]
    [SerializeField] private AudioClip liftSound;
    [SerializeField] private AudioClip dropSound;

    // State
    private bool isLifted = false;
    private Transform carrier;
    private Vector3 originalPosition;
    private float currentLiftOffset = 0f;
    private GloveController currentGloves;

    // Components
    private Collider objectCollider;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        interactionType = InteractionType.Lift;
        interactionPrompt = "Lift";

        objectCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();

        originalPosition = transform.position;
    }

    void Update()
    {
        if (isLifted && carrier != null)
        {
            // Follow the carrier (Mouse) — lift on Y axis in 3D
            Vector3 targetPos = carrier.position;
            targetPos.y += liftHeight;

            // Smooth follow
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

            // Check for drop input
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.E))
            {
                Drop();
            }

            // Also drop if Gloves are deactivated
            if (currentGloves != null && !currentGloves.AreGlovesActive())
            {
                Drop();
            }
        }
        else if (currentLiftOffset > 0)
        {
            // Settling back down after being dropped
            currentLiftOffset = Mathf.MoveTowards(currentLiftOffset, 0, liftSpeed * Time.deltaTime);
        }
    }

    public override void Interact(GloveController gloves)
    {
        if (!CanInteract()) return;

        if (!isLifted)
        {
            Lift(gloves);
        }
        else
        {
            Drop();
        }
    }

    public override bool CanInteract()
    {
        if (!base.CanInteract()) return false;

        // Check companion requirement
        if (requiresCompanion && !string.IsNullOrEmpty(requiredCompanionId))
        {
            // TODO: Check with CompanionManager if the required companion is present
            // For now, return true - we'll implement companion system later
            // return CompanionManager.Instance.HasCompanion(requiredCompanionId);
        }

        return true;
    }

    /// <summary>
    /// Pick up this object
    /// </summary>
    private void Lift(GloveController gloves)
    {
        isLifted = true;
        carrier = gloves.transform;
        currentGloves = gloves;

        // Disable collision so Mouse can walk through while carrying
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }

        // Play sound
        if (audioSource != null && liftSound != null)
        {
            audioSource.PlayOneShot(liftSound);
        }

        // Visual feedback - could add particles here
        currentLiftOffset = liftHeight;
    }

    /// <summary>
    /// Put this object down
    /// </summary>
    private void Drop()
    {
        isLifted = false;
        carrier = null;
        currentGloves = null;

        // Re-enable collision
        if (objectCollider != null)
        {
            objectCollider.enabled = true;
        }

        // Play sound
        if (audioSource != null && dropSound != null)
        {
            audioSource.PlayOneShot(dropSound);
        }
    }

    /// <summary>
    /// Optional: Snap position to a grid for cleaner puzzle solutions
    /// </summary>
    private void SnapToGrid(float gridSize = 1f)
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / gridSize) * gridSize;
        pos.z = Mathf.Round(pos.z / gridSize) * gridSize;
        transform.position = pos;
    }

    /// <summary>
    /// Reset this object to its original position
    /// </summary>
    public void ResetPosition()
    {
        if (isLifted) Drop();
        transform.position = originalPosition;
    }

    /// <summary>
    /// Check if this object is currently being carried
    /// </summary>
    public bool IsLifted()
    {
        return isLifted;
    }
}
