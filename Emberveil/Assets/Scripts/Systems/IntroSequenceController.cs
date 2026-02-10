using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Controls the Chapter 1 intro sequence for Emberveil.
///
/// Sequence flow:
/// 1. Fade from black
/// 2. Camera sweeps from Sister North peak down to Mountain Cave entrance
/// 3. Narration: Mouse's first-person opening lines
/// 4. Fade to black → Load MountainCave scene
/// 5. Inside cave: Mouse awakens, Gloves flicker
/// 6. Player gains control, explores cave
/// 7. Exit cave → MountainDescent
/// 8. Descent: Fox silhouette sighting, bridge crossing
/// 9. Transition → Overworld near Old Oak
///
/// This script should be placed in the MountainCave scene.
/// The opening camera sweep can be in a dedicated "Intro" scene or the same scene.
/// </summary>
public class IntroSequenceController : MonoBehaviour
{
    [Header("Intro Camera Sweep")]
    [Tooltip("Camera waypoints for the opening sweep (mountain peak → cave entrance)")]
    [SerializeField] private Transform[] introCameraWaypoints;

    [Tooltip("Duration of the opening camera sweep")]
    [SerializeField] private float cameraSweepDuration = 8f;

    [Header("Awakening")]
    [Tooltip("Mouse's starting position in the cave (lying down)")]
    [SerializeField] private Transform awakeningPosition;

    [Tooltip("Mouse's standing position after waking")]
    [SerializeField] private Transform standingPosition;

    [Tooltip("Duration of the 'getting up' animation")]
    [SerializeField] private float awakeningDuration = 2f;

    [Header("Glove Flicker")]
    [Tooltip("Light component on Mouse's Gloves")]
    [SerializeField] private Light gloveLight;

    [Tooltip("Number of times the Gloves flicker during awakening")]
    [SerializeField] private int flickerCount = 5;

    [Header("Narration")]
    [Tooltip("Opening narration lines (Mouse's first-person voice)")]
    [SerializeField] private string[] narrationLines = new string[]
    {
        "I don't remember falling asleep.",
        "But I remember the cold.",
        "And something else... a light, blue-green, at the edge of my fingers."
    };

    [Tooltip("Time between narration lines")]
    [SerializeField] private float narrationPause = 1.5f;

    [Header("Scene Flow")]
    [Tooltip("Skip the intro sweep (for debugging — jump straight to cave)")]
    [SerializeField] private bool skipIntroSweep = false;

    [Tooltip("Scene to load after descending the mountain")]
    [SerializeField] private string overworldSceneName = "Overworld";

    // State
    private bool introComplete = false;
    private PlayerController player;
    private HD2DCameraController cameraController;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        cameraController = FindObjectOfType<HD2DCameraController>();

        // Start the intro sequence
        StartCoroutine(PlayIntroSequence());
    }

    /// <summary>
    /// The full intro sequence coroutine
    /// </summary>
    private IEnumerator PlayIntroSequence()
    {
        // Get cutscene manager
        CutsceneManager cutscene = CutsceneManager.Instance;
        if (cutscene == null)
        {
            Debug.LogWarning("[Intro] No CutsceneManager found. Creating one.");
            var cmGO = new GameObject("CutsceneManager");
            cutscene = cmGO.AddComponent<CutsceneManager>();
        }

        // Begin cutscene mode
        cutscene.BeginCutscene();

        // Disable HD2D camera during the cinematic parts
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        // Freeze player
        if (player != null)
        {
            player.DisableMovement();
            player.gameObject.SetActive(false); // Hide player during sweep
        }

        // --- Phase 1: Opening Camera Sweep ---
        if (!skipIntroSweep && introCameraWaypoints != null && introCameraWaypoints.Length >= 2)
        {
            // Start black
            yield return cutscene.FadeTo(Color.black, 0f);

            // Brief pause in black
            yield return new WaitForSeconds(1f);

            // Fade from black as camera begins sweep
            yield return cutscene.FadeFromBlack(2f);

            // Camera sweeps from Sister North peak to cave entrance
            yield return cutscene.MoveCameraAlongPath(introCameraWaypoints, cameraSweepDuration);
        }
        else
        {
            // Quick fade from black
            yield return cutscene.FadeTo(Color.black, 0f);
            yield return new WaitForSeconds(0.5f);
            yield return cutscene.FadeFromBlack(1.5f);
        }

        // --- Phase 2: Narration ---
        for (int i = 0; i < narrationLines.Length; i++)
        {
            yield return cutscene.ShowNarration(narrationLines[i]);
            if (i < narrationLines.Length - 1)
            {
                yield return new WaitForSeconds(narrationPause);
            }
        }

        yield return new WaitForSeconds(0.5f);

        // --- Phase 3: Awakening ---
        // Show player at awakening position
        if (player != null)
        {
            player.gameObject.SetActive(true);

            if (awakeningPosition != null)
            {
                // Disable CharacterController to teleport
                var cc = player.GetComponent<CharacterController>();
                if (cc != null) cc.enabled = false;
                player.transform.position = awakeningPosition.position;
                player.transform.rotation = awakeningPosition.rotation;
                if (cc != null) cc.enabled = true;
            }
        }

        // Re-enable HD2D camera, focused on awakening position
        if (cameraController != null)
        {
            cameraController.enabled = true;
            cameraController.SnapToTarget();
        }

        // Glove flicker effect
        yield return GloveFlickerEffect();

        // Brief pause
        yield return new WaitForSeconds(1f);

        // "Rise" animation — move from lying to standing
        if (player != null && standingPosition != null)
        {
            yield return MovePlayerSmooth(player, standingPosition.position, awakeningDuration);
        }

        // --- Phase 4: Player Gains Control ---
        yield return new WaitForSeconds(0.5f);

        // End cutscene — player can now move
        cutscene.EndCutscene();
        introComplete = true;

        Debug.Log("[Intro] Intro sequence complete. Player has control.");
    }

    /// <summary>
    /// Flicker the Glove light to create the first visual hook
    /// </summary>
    private IEnumerator GloveFlickerEffect()
    {
        if (gloveLight == null)
        {
            Debug.Log("[Intro] No Glove light assigned — skipping flicker.");
            yield break;
        }

        Color gloveColor = new Color(0.2f, 0.8f, 0.6f, 1f); // Blue-green
        gloveLight.color = gloveColor;

        for (int i = 0; i < flickerCount; i++)
        {
            // On
            gloveLight.enabled = true;
            gloveLight.intensity = Random.Range(0.3f, 1.2f);
            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

            // Off
            gloveLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }

        // Final steady glow
        gloveLight.enabled = true;
        gloveLight.intensity = 0.5f;

        // Fade up slowly
        float elapsed = 0f;
        float fadeDur = 1f;
        while (elapsed < fadeDur)
        {
            elapsed += Time.deltaTime;
            gloveLight.intensity = Mathf.Lerp(0.5f, 0.8f, elapsed / fadeDur);
            yield return null;
        }
    }

    /// <summary>
    /// Smoothly move the player to a position (for the "standing up" effect)
    /// </summary>
    private IEnumerator MovePlayerSmooth(PlayerController player, Vector3 targetPos, float duration)
    {
        var cc = player.GetComponent<CharacterController>();
        Vector3 startPos = player.transform.position;
        float elapsed = 0f;

        // Disable CC for direct position control
        if (cc != null) cc.enabled = false;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            player.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        player.transform.position = targetPos;

        // Re-enable CC
        if (cc != null) cc.enabled = true;
    }

    /// <summary>
    /// Check if the intro has finished playing
    /// </summary>
    public bool IsIntroComplete()
    {
        return introComplete;
    }
}
