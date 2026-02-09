using UnityEngine;

/// <summary>
/// Makes a sprite quad always face the camera, creating the HD-2D look
/// where 2D pixel-art characters exist in a 3D world.
/// Attach to any GameObject with a sprite/quad that should billboard.
/// </summary>
public class BillboardSprite : MonoBehaviour
{
    [Header("Billboard Settings")]
    [Tooltip("Lock the Y-axis rotation only (sprite stays upright). Recommended for characters.")]
    [SerializeField] private bool lockYAxis = true;

    [Tooltip("Offset the rotation (degrees) — useful if sprite is not facing default direction")]
    [SerializeField] private float rotationOffset = 0f;

    // Cached reference
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("BillboardSprite: No main camera found. Will search each frame.");
        }
    }

    void LateUpdate()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) return;
        }

        if (lockYAxis)
        {
            // Only rotate around Y axis — sprite stays upright
            // This is the standard HD-2D billboard approach
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0f; // Flatten to XZ plane
            cameraForward.Normalize();

            if (cameraForward.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(cameraForward, Vector3.up);
                transform.rotation = targetRotation * Quaternion.Euler(0f, rotationOffset, 0f);
            }
        }
        else
        {
            // Full billboard — sprite always faces camera exactly
            transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0f, rotationOffset, 0f);
        }
    }

    /// <summary>
    /// Force update the billboard rotation (useful after teleporting)
    /// </summary>
    public void ForceUpdate()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if (mainCamera != null) LateUpdate();
    }
}
