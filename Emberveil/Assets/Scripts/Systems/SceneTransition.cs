using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Handles transitions between scenes with fade effects (HD-2D: 3D triggers).
/// Place trigger zones at doors/stairs to move between areas.
///
/// Example: Trigger at the spiral staircase to go from Burrow to Workshop.
/// </summary>
public class SceneTransition : MonoBehaviour
{
    [Header("Destination")]
    [Tooltip("Name of the scene to load")]
    [SerializeField] private string targetSceneName;

    [Tooltip("Where to spawn the player in the new scene")]
    [SerializeField] private string spawnPointId = "Default";

    [Header("Transition Settings")]
    [Tooltip("Time for fade out/in")]
    [SerializeField] private float fadeDuration = 0.5f;

    [Tooltip("Should this trigger automatically when entered?")]
    [SerializeField] private bool autoTrigger = true;

    [Tooltip("Or require interaction?")]
    [SerializeField] private bool requireInteraction = false;

    [Header("Visual")]
    [Tooltip("Color to fade to (usually black)")]
    [SerializeField] private Color fadeColor = Color.black;

    // Static reference for passing spawn point between scenes
    public static string NextSpawnPointId { get; private set; }

    // Prevent double-triggering
    private bool isTransitioning = false;

    // Reference to fade overlay (created at runtime if needed)
    private static GameObject fadeOverlay;
    private static UnityEngine.UI.Image fadeImage;

    void OnTriggerEnter(Collider other)
    {
        if (!autoTrigger) return;
        if (isTransitioning) return;

        // Check if it's the player
        if (other.GetComponent<PlayerController>() != null)
        {
            StartTransition();
        }
    }

    /// <summary>
    /// Call this from an Interactable if requireInteraction is true
    /// </summary>
    public void TriggerTransition()
    {
        if (!requireInteraction) return;
        if (isTransitioning) return;

        StartTransition();
    }

    /// <summary>
    /// Begin the scene transition
    /// </summary>
    private void StartTransition()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("SceneTransition: No target scene specified!");
            return;
        }

        isTransitioning = true;
        NextSpawnPointId = spawnPointId;

        // Freeze player
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            GameManager.Instance.player.DisableMovement();
            GameManager.Instance.SetGameState(GameState.Loading);
        }

        StartCoroutine(TransitionSequence());
    }

    /// <summary>
    /// The actual transition coroutine
    /// </summary>
    private IEnumerator TransitionSequence()
    {
        // Create fade overlay if it doesn't exist
        EnsureFadeOverlay();

        // Fade out
        yield return StartCoroutine(Fade(0f, 1f));

        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Small delay for scene setup
        yield return new WaitForSeconds(0.1f);

        // Position player at spawn point
        PositionPlayerAtSpawnPoint();

        // Fade in
        yield return StartCoroutine(Fade(1f, 0f));

        // Restore player control
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetGameState(GameState.Playing);
            if (GameManager.Instance.player != null)
            {
                GameManager.Instance.player.EnableMovement();
            }
        }

        isTransitioning = false;
    }

    /// <summary>
    /// Fade the overlay in or out
    /// </summary>
    private IEnumerator Fade(float from, float to)
    {
        if (fadeImage == null) yield break;

        float elapsed = 0f;
        Color color = fadeColor;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; // Use unscaled in case game is paused
            float t = elapsed / fadeDuration;
            color.a = Mathf.Lerp(from, to, t);
            fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
    }

    /// <summary>
    /// Create the fade overlay canvas if it doesn't exist
    /// </summary>
    private void EnsureFadeOverlay()
    {
        if (fadeOverlay != null) return;

        // Create canvas
        fadeOverlay = new GameObject("FadeOverlay");
        DontDestroyOnLoad(fadeOverlay);

        Canvas canvas = fadeOverlay.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999; // On top of everything

        // Create image
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(fadeOverlay.transform, false);

        fadeImage = imageObj.AddComponent<UnityEngine.UI.Image>();
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);

        // Stretch to fill screen
        RectTransform rect = fadeImage.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// Find the spawn point and move the player there
    /// </summary>
    private void PositionPlayerAtSpawnPoint()
    {
        // Find all spawn points in the new scene
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();

        SpawnPoint targetSpawn = null;

        foreach (var sp in spawnPoints)
        {
            if (sp.spawnPointId == NextSpawnPointId)
            {
                targetSpawn = sp;
                break;
            }
        }

        // Fallback to any spawn point if specific one not found
        if (targetSpawn == null && spawnPoints.Length > 0)
        {
            targetSpawn = spawnPoints[0];
            Debug.LogWarning($"Spawn point '{NextSpawnPointId}' not found, using '{targetSpawn.spawnPointId}'");
        }

        // Move player
        if (targetSpawn != null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                // Disable CharacterController to teleport (required for CharacterController)
                CharacterController cc = player.GetComponent<CharacterController>();
                if (cc != null) cc.enabled = false;
                player.transform.position = targetSpawn.transform.position;
                if (cc != null) cc.enabled = true;
            }
        }
        else
        {
            Debug.LogWarning("No spawn points found in scene!");
        }
    }
}

/// <summary>
/// Simple component to mark spawn points in a scene (HD-2D: 3D positions).
/// Place these where players should appear after scene transitions.
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    [Tooltip("Unique identifier for this spawn point")]
    public string spawnPointId = "Default";

    [Tooltip("Direction player should face when spawning here (XZ plane)")]
    public Vector3 facingDirection = Vector3.forward;

    void OnDrawGizmos()
    {
        // Draw spawn point in editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        Gizmos.DrawRay(transform.position, facingDirection * 0.5f);
    }
}
