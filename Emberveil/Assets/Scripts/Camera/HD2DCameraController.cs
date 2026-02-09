using UnityEngine;

/// <summary>
/// HD-2D camera controller that follows the player from a fixed tilted angle.
/// Creates the signature HD-2D look: top-down-ish perspective with depth.
/// Attach to the Main Camera.
/// </summary>
public class HD2DCameraController : MonoBehaviour
{
    [Header("Follow Target")]
    [Tooltip("The transform to follow (usually the player). Auto-finds PlayerController if empty.")]
    [SerializeField] private Transform target;

    [Header("Camera Angle")]
    [Tooltip("Tilt angle in degrees (0 = top-down, 90 = side view). ~35° is classic HD-2D.")]
    [SerializeField] private float tiltAngle = 35f;

    [Tooltip("Distance from the target")]
    [SerializeField] private float distance = 12f;

    [Tooltip("Height offset above the target")]
    [SerializeField] private float heightOffset = 0f;

    [Header("Follow Behavior")]
    [Tooltip("How smoothly the camera follows (lower = snappier)")]
    [SerializeField] private float smoothTime = 0.15f;

    [Tooltip("Dead zone radius — camera won't move if target is within this distance of center")]
    [SerializeField] private float deadZone = 0.1f;

    [Header("Projection")]
    [Tooltip("Use orthographic projection (true) or perspective (false)")]
    [SerializeField] private bool useOrthographic = true;

    [Tooltip("Orthographic size (zoom level). Smaller = more zoomed in.")]
    [SerializeField] private float orthographicSize = 6f;

    [Tooltip("Field of view for perspective mode (ignored in orthographic)")]
    [SerializeField] private float fieldOfView = 30f;

    // Internal state
    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam == null)
        {
            Debug.LogError("HD2DCameraController requires a Camera component!");
            return;
        }

        // Auto-find player if target not assigned
        if (target == null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogWarning("HD2DCameraController: No target assigned and no PlayerController found.");
            }
        }

        // Apply projection settings
        ApplyProjectionSettings();

        // Apply initial rotation
        ApplyCameraRotation();

        // Snap to target immediately on start
        if (target != null)
        {
            transform.position = CalculateDesiredPosition();
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = CalculateDesiredPosition();

        // Check dead zone
        float distanceToDesired = Vector3.Distance(transform.position, desiredPosition);
        if (distanceToDesired < deadZone) return;

        // Smooth follow
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

    /// <summary>
    /// Calculate where the camera should be based on target position and tilt angle
    /// </summary>
    private Vector3 CalculateDesiredPosition()
    {
        // Calculate offset from the tilt angle
        // Camera sits behind and above the target, looking down at tiltAngle degrees
        float tiltRad = tiltAngle * Mathf.Deg2Rad;
        float yOffset = Mathf.Sin(tiltRad) * distance;
        float zOffset = -Mathf.Cos(tiltRad) * distance;

        Vector3 targetPos = target.position + Vector3.up * heightOffset;
        return targetPos + new Vector3(0f, yOffset, zOffset);
    }

    /// <summary>
    /// Apply the tilt rotation to the camera
    /// </summary>
    private void ApplyCameraRotation()
    {
        transform.rotation = Quaternion.Euler(tiltAngle, 0f, 0f);
    }

    /// <summary>
    /// Apply orthographic or perspective projection settings
    /// </summary>
    private void ApplyProjectionSettings()
    {
        if (cam == null) return;

        cam.orthographic = useOrthographic;

        if (useOrthographic)
        {
            cam.orthographicSize = orthographicSize;
        }
        else
        {
            cam.fieldOfView = fieldOfView;
        }
    }

    /// <summary>
    /// Change the follow target at runtime (e.g., during cutscenes)
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    /// <summary>
    /// Snap the camera to the target immediately (no smoothing)
    /// </summary>
    public void SnapToTarget()
    {
        if (target == null) return;
        velocity = Vector3.zero;
        transform.position = CalculateDesiredPosition();
    }

    /// <summary>
    /// Adjust zoom level at runtime
    /// </summary>
    public void SetZoom(float size)
    {
        if (useOrthographic)
        {
            orthographicSize = size;
            if (cam != null) cam.orthographicSize = size;
        }
    }

    void OnValidate()
    {
        // Update camera settings in editor when values change
        ApplyCameraRotation();
        ApplyProjectionSettings();
    }
}
