using UnityEngine;

/// <summary>
/// An interactive door that can be opened, closed, and optionally locked.
///
/// Can require a key item to unlock, or be controlled by puzzles/switches.
/// Optionally triggers a scene transition when opened.
/// </summary>
public class DoorInteractable : Interactable
{
    [Header("Door Settings")]
    [Tooltip("Is the door currently open?")]
    [SerializeField] private bool isOpen = false;

    [Tooltip("Is the door locked?")]
    [SerializeField] private bool isLocked = false;

    [Tooltip("Item required to unlock (leave empty if not locked by key)")]
    [SerializeField] private ItemData keyItem;

    [Tooltip("Should the key be consumed when used?")]
    [SerializeField] private bool consumeKey = true;

    [Tooltip("Message shown when trying to open a locked door")]
    [SerializeField] private string lockedMessage = "The door is locked.";

    [Header("Scene Transition (Optional)")]
    [Tooltip("Should this door load another scene when opened?")]
    [SerializeField] private bool triggersSceneChange = false;

    [Tooltip("Scene to load when entering")]
    [SerializeField] private string targetSceneName;

    [Tooltip("Spawn point ID in the target scene")]
    [SerializeField] private string spawnPointId = "Default";

    [Header("Animation")]
    [Tooltip("Animator component (optional)")]
    [SerializeField] private Animator animator;

    [Tooltip("Sprite to show when door is closed")]
    [SerializeField] private Sprite closedSprite;

    [Tooltip("Sprite to show when door is open")]
    [SerializeField] private Sprite openSprite;

    [Header("Audio")]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private AudioClip lockedSound;
    [SerializeField] private AudioClip unlockSound;

    [Header("Collider")]
    [Tooltip("Collider that blocks movement (disabled when open)")]
    [SerializeField] private Collider2D blockingCollider;

    // Components
    private AudioSource audioSource;

    // Events
    public System.Action<bool> OnDoorStateChanged;
    public System.Action OnDoorUnlocked;

    protected override void Awake()
    {
        base.Awake();

        interactionType = InteractionType.Use;
        UpdatePrompt();

        audioSource = GetComponent<AudioSource>();

        // Apply initial state
        ApplyDoorState();
    }

    public override void Interact(GloveController gloves)
    {
        if (!CanInteract()) return;

        if (isLocked)
        {
            TryUnlock();
        }
        else
        {
            Toggle();
        }
    }

    /// <summary>
    /// Toggle door open/closed
    /// </summary>
    public void Toggle()
    {
        if (isLocked) return;

        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    /// <summary>
    /// Open the door
    /// </summary>
    public void Open()
    {
        if (isOpen || isLocked) return;

        isOpen = true;
        ApplyDoorState();

        // Play sound
        PlaySound(openSound);

        // Trigger animation
        if (animator != null)
        {
            animator.SetBool("IsOpen", true);
            animator.SetTrigger("Open");
        }

        UpdatePrompt();
        OnDoorStateChanged?.Invoke(true);

        Debug.Log("[Door] Opened");

        // Handle scene transition
        if (triggersSceneChange && !string.IsNullOrEmpty(targetSceneName))
        {
            // Use SceneTransition system
            SceneTransition.NextSpawnPointId = spawnPointId;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetGameState(GameState.Loading);
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
        }
    }

    /// <summary>
    /// Close the door
    /// </summary>
    public void Close()
    {
        if (!isOpen) return;

        isOpen = false;
        ApplyDoorState();

        // Play sound
        PlaySound(closeSound);

        // Trigger animation
        if (animator != null)
        {
            animator.SetBool("IsOpen", false);
            animator.SetTrigger("Close");
        }

        UpdatePrompt();
        OnDoorStateChanged?.Invoke(false);

        Debug.Log("[Door] Closed");
    }

    /// <summary>
    /// Try to unlock the door with the player's inventory
    /// </summary>
    private void TryUnlock()
    {
        // Check if we have the key
        if (keyItem != null && InventoryManager.Instance != null)
        {
            if (InventoryManager.Instance.HasItem(keyItem))
            {
                Unlock();

                // Consume key if configured
                if (consumeKey)
                {
                    InventoryManager.Instance.RemoveItem(keyItem);
                }

                return;
            }
        }

        // Can't unlock - show message
        PlaySound(lockedSound);
        Debug.Log($"[Door] {lockedMessage}");

        // Show message via dialogue UI if available
        if (DialogueUI.Instance != null)
        {
            DialogueUI.Instance.ShowLineInstant("", lockedMessage);
        }
    }

    /// <summary>
    /// Unlock the door
    /// </summary>
    public void Unlock()
    {
        if (!isLocked) return;

        isLocked = false;

        PlaySound(unlockSound);
        UpdatePrompt();
        OnDoorUnlocked?.Invoke();

        Debug.Log("[Door] Unlocked!");

        // Optionally auto-open when unlocked
        // Open();
    }

    /// <summary>
    /// Lock the door
    /// </summary>
    public void Lock()
    {
        isLocked = true;
        UpdatePrompt();

        // Force close if open
        if (isOpen)
        {
            Close();
        }
    }

    /// <summary>
    /// Apply the current door state to visuals and colliders
    /// </summary>
    private void ApplyDoorState()
    {
        // Update sprite
        if (spriteRenderer != null)
        {
            if (isOpen && openSprite != null)
            {
                spriteRenderer.sprite = openSprite;
            }
            else if (!isOpen && closedSprite != null)
            {
                spriteRenderer.sprite = closedSprite;
            }
        }

        // Update blocking collider
        if (blockingCollider != null)
        {
            blockingCollider.enabled = !isOpen;
        }
    }

    /// <summary>
    /// Update the interaction prompt based on state
    /// </summary>
    private void UpdatePrompt()
    {
        if (isLocked)
        {
            if (keyItem != null)
            {
                interactionPrompt = $"Unlock (needs {keyItem.itemName})";
            }
            else
            {
                interactionPrompt = "Locked";
            }
        }
        else if (triggersSceneChange)
        {
            interactionPrompt = "Enter";
        }
        else
        {
            interactionPrompt = isOpen ? "Close door" : "Open door";
        }
    }

    /// <summary>
    /// Play a sound effect
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }

    /// <summary>
    /// Check if door is currently open
    /// </summary>
    public bool IsOpen()
    {
        return isOpen;
    }

    /// <summary>
    /// Check if door is currently locked
    /// </summary>
    public bool IsLocked()
    {
        return isLocked;
    }

    /// <summary>
    /// Set the key item at runtime
    /// </summary>
    public void SetKeyItem(ItemData key)
    {
        keyItem = key;
        if (key != null)
        {
            isLocked = true;
        }
        UpdatePrompt();
    }

    void OnDrawGizmos()
    {
        // Visualize door state in editor
        Gizmos.color = isLocked ? Color.red : (isOpen ? Color.green : Color.yellow);
        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 1.5f, 0.1f));

        // Draw arrow to target scene if configured
        if (triggersSceneChange)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, Vector3.up * 0.5f);
        }
    }
}

/// <summary>
/// A simple switch/lever that can control doors and other objects.
/// </summary>
public class SwitchInteractable : Interactable
{
    [Header("Switch Settings")]
    [Tooltip("Is the switch currently activated?")]
    [SerializeField] private bool isActivated = false;

    [Tooltip("Can the switch be toggled back off?")]
    [SerializeField] private bool canDeactivate = true;

    [Tooltip("Objects to activate when switch is flipped")]
    [SerializeField] private GameObject[] activateTargets;

    [Tooltip("Doors to open when switch is activated")]
    [SerializeField] private DoorInteractable[] doorsToOpen;

    [Header("Visual")]
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;

    [Header("Audio")]
    [SerializeField] private AudioClip switchSound;

    private AudioSource audioSource;

    public System.Action<bool> OnSwitchToggled;

    protected override void Awake()
    {
        base.Awake();

        interactionType = InteractionType.Use;
        interactionPrompt = isActivated ? "Deactivate" : "Activate";

        audioSource = GetComponent<AudioSource>();

        ApplySwitchState();
    }

    public override void Interact(GloveController gloves)
    {
        if (!CanInteract()) return;

        if (isActivated && !canDeactivate)
        {
            Debug.Log("[Switch] Already activated and cannot be deactivated.");
            return;
        }

        Toggle();
    }

    /// <summary>
    /// Toggle the switch state
    /// </summary>
    public void Toggle()
    {
        isActivated = !isActivated;
        ApplySwitchState();

        // Play sound
        if (audioSource != null && switchSound != null)
        {
            audioSource.PlayOneShot(switchSound);
        }

        // Activate/deactivate targets
        foreach (var target in activateTargets)
        {
            if (target != null)
            {
                target.SetActive(isActivated);
            }
        }

        // Open/close doors
        foreach (var door in doorsToOpen)
        {
            if (door == null) continue;

            if (isActivated)
            {
                door.Open();
            }
            else if (canDeactivate)
            {
                door.Close();
            }
        }

        interactionPrompt = isActivated ? "Deactivate" : "Activate";
        OnSwitchToggled?.Invoke(isActivated);

        Debug.Log($"[Switch] {(isActivated ? "Activated" : "Deactivated")}");
    }

    /// <summary>
    /// Apply visual state
    /// </summary>
    private void ApplySwitchState()
    {
        if (spriteRenderer == null) return;

        if (isActivated && onSprite != null)
        {
            spriteRenderer.sprite = onSprite;
        }
        else if (!isActivated && offSprite != null)
        {
            spriteRenderer.sprite = offSprite;
        }
    }

    /// <summary>
    /// Check if switch is activated
    /// </summary>
    public bool IsActivated()
    {
        return isActivated;
    }

    /// <summary>
    /// Force a specific state
    /// </summary>
    public void SetActivated(bool activated)
    {
        if (isActivated == activated) return;
        Toggle();
    }
}
