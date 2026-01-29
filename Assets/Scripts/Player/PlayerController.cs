using UnityEngine;

/// <summary>
/// Controls Mouse's movement in the game world.
/// Attach to the Mouse GameObject. Requires Rigidbody2D component.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("How fast Mouse moves in units per second")]
    public float moveSpeed = 5f;
    
    [Header("References")]
    [Tooltip("Leave empty to auto-find on this GameObject")]
    public Rigidbody2D rb;
    
    // Internal state
    private Vector2 movement;
    private bool canMove = true;
    
    // Direction Mouse is facing (for interaction raycasts)
    public Vector2 FacingDirection { get; private set; } = Vector2.down;
    
    void Start()
    {
        // Auto-find Rigidbody2D if not assigned in Inspector
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        if (rb == null)
        {
            Debug.LogError("PlayerController requires a Rigidbody2D component!");
        }
    }
    
    void Update()
    {
        // Don't read input if movement is disabled (e.g., during dialogue)
        if (!canMove)
        {
            movement = Vector2.zero;
            return;
        }
        
        // Read input (works with WASD and Arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        // Update facing direction (only when actually moving)
        if (movement.sqrMagnitude > 0.01f)
        {
            // Prioritize horizontal or vertical based on which is larger
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                FacingDirection = movement.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                FacingDirection = movement.y > 0 ? Vector2.up : Vector2.down;
            }
        }
    }
    
    void FixedUpdate()
    {
        // Move using physics (respects colliders)
        // Normalize to prevent faster diagonal movement
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
    
    /// <summary>
    /// Call this to freeze Mouse in place (e.g., during dialogue or cutscenes)
    /// </summary>
    public void DisableMovement()
    {
        canMove = false;
        movement = Vector2.zero;
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
