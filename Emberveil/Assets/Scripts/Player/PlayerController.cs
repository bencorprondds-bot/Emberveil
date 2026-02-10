using UnityEngine;

/// <summary>
/// Controls Mouse's movement in the game world (HD-2D: 3D space, XZ plane movement).
/// Attach to the Mouse GameObject. Requires CharacterController component.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("How fast Mouse moves in units per second")]
    public float moveSpeed = 5f;

    [Tooltip("Gravity applied to the character")]
    [SerializeField] private float gravity = -20f;

    [Header("References")]
    [Tooltip("Leave empty to auto-find on this GameObject")]
    public CharacterController characterController;

    // Internal state
    private Vector3 movement;
    private float verticalVelocity;
    private bool canMove = true;

    /// <summary>
    /// Direction Mouse is facing on the XZ plane (for interaction raycasts).
    /// </summary>
    public Vector3 FacingDirection { get; private set; } = Vector3.forward;

    void Start()
    {
        // Auto-find CharacterController if not assigned in Inspector
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        if (characterController == null)
        {
            Debug.LogError("PlayerController requires a CharacterController component!");
        }
    }

    void Update()
    {
        // Don't read input if movement is disabled (e.g., during dialogue)
        if (!canMove)
        {
            movement = Vector3.zero;
            return;
        }

        // Read input (works with WASD and Arrow keys)
        // In HD-2D: X = left/right, Z = forward/back (into/out of screen)
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        movement = new Vector3(inputX, 0f, inputZ).normalized;

        // Update facing direction (only when actually moving)
        if (movement.sqrMagnitude > 0.01f)
        {
            // Prioritize horizontal or depth based on which input is larger
            if (Mathf.Abs(inputX) > Mathf.Abs(inputZ))
            {
                FacingDirection = inputX > 0 ? Vector3.right : Vector3.left;
            }
            else
            {
                FacingDirection = inputZ > 0 ? Vector3.forward : Vector3.back;
            }
        }

        // Apply gravity
        if (characterController.isGrounded)
        {
            verticalVelocity = -2f; // Small downward force to keep grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // Move using CharacterController (respects colliders)
        Vector3 finalMovement = movement * moveSpeed + Vector3.up * verticalVelocity;
        characterController.Move(finalMovement * Time.deltaTime);
    }

    /// <summary>
    /// Call this to freeze Mouse in place (e.g., during dialogue or cutscenes)
    /// </summary>
    public void DisableMovement()
    {
        canMove = false;
        movement = Vector3.zero;
    }

    /// <summary>
    /// Call this to restore Mouse's ability to move
    /// </summary>
    public void EnableMovement()
    {
        canMove = true;
    }

    /// <summary>
    /// Check if Mouse can currently move
    /// </summary>
    public bool CanMove()
    {
        return canMove;
    }
}
