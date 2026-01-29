using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages Mouse's companions - friends who follow and help her.
///
/// From the source material: Bear, Hawk, and others can join Mouse
/// on her adventures. Each companion unlocks different abilities
/// (Bear can help lift heavy objects, Hawk can scout ahead, etc.)
/// </summary>
public class CompanionManager : MonoBehaviour
{
    public static CompanionManager Instance { get; private set; }

    [Header("Settings")]
    [Tooltip("How far companions follow behind Mouse")]
    [SerializeField] private float followDistance = 2f;

    [Tooltip("Speed multiplier for companion movement")]
    [SerializeField] private float followSpeed = 4f;

    [Tooltip("Distance at which companion teleports to catch up")]
    [SerializeField] private float teleportDistance = 10f;

    [Header("Debug")]
    [SerializeField] private bool showDebugInfo = true;

    // Currently active companions
    private List<Companion> activeCompanions = new List<Companion>();

    // All companions that have been recruited (even if not currently following)
    private HashSet<string> recruitedCompanionIds = new HashSet<string>();

    // Reference to player
    private Transform playerTransform;

    // Events
    public System.Action<Companion> OnCompanionJoined;
    public System.Action<Companion> OnCompanionLeft;
    public System.Action<string> OnCompanionRecruited;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Subscribe to scene loading
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        FindPlayer();
    }

    void Start()
    {
        FindPlayer();
    }

    /// <summary>
    /// Locate the player in the current scene
    /// </summary>
    private void FindPlayer()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        UpdateCompanionPositions();
    }

    /// <summary>
    /// Update all companion positions to follow the player
    /// </summary>
    private void UpdateCompanionPositions()
    {
        if (playerTransform == null) return;

        for (int i = 0; i < activeCompanions.Count; i++)
        {
            Companion companion = activeCompanions[i];
            if (companion == null) continue;

            // Calculate target position (staggered behind player)
            float offset = followDistance * (i + 1);
            Vector3 targetPos = playerTransform.position - (Vector3)(companion.GetLastMovementDirection() * offset);

            // Check if needs to teleport
            float distance = Vector3.Distance(companion.transform.position, playerTransform.position);
            if (distance > teleportDistance)
            {
                companion.TeleportTo(targetPos);
            }
            else
            {
                companion.MoveTo(targetPos, followSpeed);
            }
        }
    }

    /// <summary>
    /// Add a companion to the active party
    /// </summary>
    /// <param name="companion">The companion to add</param>
    /// <returns>True if successfully added</returns>
    public bool AddCompanion(Companion companion)
    {
        if (companion == null) return false;
        if (activeCompanions.Contains(companion)) return false;

        activeCompanions.Add(companion);
        companion.SetFollowing(true);

        // Mark as recruited if first time
        if (!recruitedCompanionIds.Contains(companion.CompanionId))
        {
            recruitedCompanionIds.Add(companion.CompanionId);
            OnCompanionRecruited?.Invoke(companion.CompanionId);
        }

        Debug.Log($"[CompanionManager] {companion.CompanionName} joined the party!");
        OnCompanionJoined?.Invoke(companion);

        return true;
    }

    /// <summary>
    /// Remove a companion from the active party
    /// </summary>
    /// <param name="companion">The companion to remove</param>
    /// <returns>True if successfully removed</returns>
    public bool RemoveCompanion(Companion companion)
    {
        if (companion == null) return false;
        if (!activeCompanions.Contains(companion)) return false;

        activeCompanions.Remove(companion);
        companion.SetFollowing(false);

        Debug.Log($"[CompanionManager] {companion.CompanionName} left the party.");
        OnCompanionLeft?.Invoke(companion);

        return true;
    }

    /// <summary>
    /// Check if a specific companion is currently in the party
    /// </summary>
    public bool HasCompanion(string companionId)
    {
        foreach (var companion in activeCompanions)
        {
            if (companion != null && companion.CompanionId == companionId)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Check if a companion has ever been recruited
    /// </summary>
    public bool HasEverRecruited(string companionId)
    {
        return recruitedCompanionIds.Contains(companionId);
    }

    /// <summary>
    /// Get a companion by ID (if in party)
    /// </summary>
    public Companion GetCompanion(string companionId)
    {
        foreach (var companion in activeCompanions)
        {
            if (companion != null && companion.CompanionId == companionId)
            {
                return companion;
            }
        }
        return null;
    }

    /// <summary>
    /// Get all active companions
    /// </summary>
    public List<Companion> GetActiveCompanions()
    {
        return new List<Companion>(activeCompanions);
    }

    /// <summary>
    /// Get count of active companions
    /// </summary>
    public int GetCompanionCount()
    {
        return activeCompanions.Count;
    }

    /// <summary>
    /// Dismiss all companions
    /// </summary>
    public void DismissAll()
    {
        while (activeCompanions.Count > 0)
        {
            RemoveCompanion(activeCompanions[0]);
        }
    }

    void OnGUI()
    {
        if (!showDebugInfo) return;

        GUILayout.BeginArea(new Rect(10, 120, 200, 100));
        GUILayout.BeginVertical("box");
        GUILayout.Label("=== COMPANIONS ===");

        if (activeCompanions.Count == 0)
        {
            GUILayout.Label("(none)");
        }
        else
        {
            foreach (var companion in activeCompanions)
            {
                if (companion != null)
                {
                    GUILayout.Label($"- {companion.CompanionName}");
                }
            }
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}

/// <summary>
/// Base class for companion characters.
/// Attach to NPCs that can join Mouse's party.
/// </summary>
public class Companion : MonoBehaviour
{
    [Header("Companion Identity")]
    [Tooltip("Unique identifier for this companion")]
    [SerializeField] private string companionId = "companion";

    [Tooltip("Display name")]
    [SerializeField] private string companionName = "Companion";

    [Header("Abilities")]
    [Tooltip("Can this companion help lift heavy objects?")]
    [SerializeField] private bool canHelpLift = false;

    [Tooltip("Abilities this companion provides")]
    [SerializeField] private List<CompanionAbility> abilities = new List<CompanionAbility>();

    [Header("Movement")]
    [Tooltip("How fast this companion moves")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Visual")]
    [Tooltip("Sprite renderer for facing direction")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    // State
    private bool isFollowing = false;
    private Vector2 lastMovementDirection = Vector2.down;
    private Vector3 targetPosition;

    // Properties
    public string CompanionId => companionId;
    public string CompanionName => companionName;
    public bool CanHelpLift => canHelpLift;
    public bool IsFollowing => isFollowing;

    void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        targetPosition = transform.position;
    }

    void Update()
    {
        if (isFollowing)
        {
            // Move toward target position
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;

                // Update facing direction
                if (direction.sqrMagnitude > 0.01f)
                {
                    lastMovementDirection = direction.normalized;
                    UpdateFacing();
                }
            }
        }
    }

    /// <summary>
    /// Set whether this companion is following the player
    /// </summary>
    public void SetFollowing(bool following)
    {
        isFollowing = following;
    }

    /// <summary>
    /// Move toward a target position
    /// </summary>
    public void MoveTo(Vector3 position, float speed)
    {
        targetPosition = position;
        moveSpeed = speed;
    }

    /// <summary>
    /// Instantly teleport to a position
    /// </summary>
    public void TeleportTo(Vector3 position)
    {
        transform.position = position;
        targetPosition = position;
    }

    /// <summary>
    /// Get the last movement direction
    /// </summary>
    public Vector2 GetLastMovementDirection()
    {
        return lastMovementDirection;
    }

    /// <summary>
    /// Update sprite facing based on movement
    /// </summary>
    private void UpdateFacing()
    {
        if (spriteRenderer == null) return;

        // Simple horizontal flip
        if (Mathf.Abs(lastMovementDirection.x) > 0.1f)
        {
            spriteRenderer.flipX = lastMovementDirection.x < 0;
        }
    }

    /// <summary>
    /// Check if this companion has a specific ability
    /// </summary>
    public bool HasAbility(CompanionAbilityType abilityType)
    {
        foreach (var ability in abilities)
        {
            if (ability.abilityType == abilityType)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Get an ability by type
    /// </summary>
    public CompanionAbility GetAbility(CompanionAbilityType abilityType)
    {
        foreach (var ability in abilities)
        {
            if (ability.abilityType == abilityType)
            {
                return ability;
            }
        }
        return null;
    }

    /// <summary>
    /// Join Mouse's party
    /// </summary>
    public void JoinParty()
    {
        if (CompanionManager.Instance != null)
        {
            CompanionManager.Instance.AddCompanion(this);
        }
    }

    /// <summary>
    /// Leave Mouse's party
    /// </summary>
    public void LeaveParty()
    {
        if (CompanionManager.Instance != null)
        {
            CompanionManager.Instance.RemoveCompanion(this);
        }
    }
}

/// <summary>
/// Types of abilities companions can have
/// </summary>
public enum CompanionAbilityType
{
    HelpLift,       // Help move heavy objects (Bear)
    Scout,          // See farther ahead (Hawk)
    Dig,            // Dig up buried items
    Swim,           // Cross water
    Climb,          // Reach high places
    Translate,      // Understand ancient text
    Heal,           // Restore health/energy
    Light           // Illuminate dark areas
}

/// <summary>
/// Represents a specific ability a companion has
/// </summary>
[System.Serializable]
public class CompanionAbility
{
    public CompanionAbilityType abilityType;

    [Tooltip("Display name for this ability")]
    public string abilityName;

    [Tooltip("Description shown in UI")]
    [TextArea(2, 4)]
    public string description;

    [Tooltip("Icon for this ability")]
    public Sprite icon;
}
