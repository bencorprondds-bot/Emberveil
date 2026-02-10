using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Manages cutscene playback — camera movements, fade effects, and text overlays.
/// Used for the intro sequence, story beats, and scene transitions.
///
/// Access via CutsceneManager.Instance.
/// </summary>
public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("Canvas for cutscene overlays (created at runtime if null)")]
    [SerializeField] private Canvas cutsceneCanvas;

    [Tooltip("Text component for narration overlay")]
    [SerializeField] private Text narrationText;

    [Tooltip("Image component for fade overlay")]
    [SerializeField] private Image fadeImage;

    [Header("Settings")]
    [Tooltip("Default fade duration")]
    [SerializeField] private float defaultFadeDuration = 1f;

    [Tooltip("Narration text typing speed (characters per second)")]
    [SerializeField] private float typingSpeed = 30f;

    [Tooltip("Default narration display time after typing finishes")]
    [SerializeField] private float narrationHoldTime = 2f;

    // State
    private bool isPlayingCutscene = false;
    private Coroutine activeCutscene;
    private Camera cutsceneCamera;

    // Events
    public System.Action OnCutsceneStarted;
    public System.Action OnCutsceneEnded;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        EnsureUI();
    }

    /// <summary>
    /// Create the cutscene UI overlay if it doesn't exist
    /// </summary>
    private void EnsureUI()
    {
        if (cutsceneCanvas != null) return;

        // Create canvas
        var canvasGO = new GameObject("CutsceneCanvas");
        canvasGO.transform.SetParent(transform);
        cutsceneCanvas = canvasGO.AddComponent<Canvas>();
        cutsceneCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        cutsceneCanvas.sortingOrder = 100;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Fade overlay
        var fadeGO = new GameObject("FadeOverlay");
        fadeGO.transform.SetParent(canvasGO.transform, false);
        fadeImage = fadeGO.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.raycastTarget = false;
        var fadeRect = fadeImage.rectTransform;
        fadeRect.anchorMin = Vector2.zero;
        fadeRect.anchorMax = Vector2.one;
        fadeRect.sizeDelta = Vector2.zero;

        // Narration text
        var textGO = new GameObject("NarrationText");
        textGO.transform.SetParent(canvasGO.transform, false);
        narrationText = textGO.AddComponent<Text>();
        narrationText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        narrationText.fontSize = 24;
        narrationText.color = Color.white;
        narrationText.alignment = TextAnchor.MiddleCenter;
        narrationText.text = "";
        narrationText.raycastTarget = false;

        // Add outline for readability
        var outline = textGO.AddComponent<Outline>();
        outline.effectColor = new Color(0, 0, 0, 0.8f);
        outline.effectDistance = new Vector2(2, -2);

        var textRect = narrationText.rectTransform;
        textRect.anchorMin = new Vector2(0.1f, 0.05f);
        textRect.anchorMax = new Vector2(0.9f, 0.25f);
        textRect.sizeDelta = Vector2.zero;
    }

    /// <summary>
    /// Fade the screen to a color over duration
    /// </summary>
    public IEnumerator FadeTo(Color color, float duration = -1f)
    {
        if (duration < 0) duration = defaultFadeDuration;
        EnsureUI();

        Color startColor = fadeImage.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            fadeImage.color = Color.Lerp(startColor, color, elapsed / duration);
            yield return null;
        }

        fadeImage.color = color;
    }

    /// <summary>
    /// Fade to black
    /// </summary>
    public IEnumerator FadeToBlack(float duration = -1f)
    {
        yield return FadeTo(Color.black, duration);
    }

    /// <summary>
    /// Fade from black to clear
    /// </summary>
    public IEnumerator FadeFromBlack(float duration = -1f)
    {
        yield return FadeTo(new Color(0, 0, 0, 0), duration);
    }

    /// <summary>
    /// Display narration text with typewriter effect
    /// </summary>
    public IEnumerator ShowNarration(string text, float holdTime = -1f)
    {
        if (holdTime < 0) holdTime = narrationHoldTime;
        EnsureUI();

        narrationText.text = "";

        // Typewriter effect
        for (int i = 0; i < text.Length; i++)
        {
            narrationText.text = text.Substring(0, i + 1);

            // Skip ahead if player presses space
            if (Input.GetKey(KeyCode.Space))
            {
                narrationText.text = text;
                break;
            }

            yield return new WaitForSecondsRealtime(1f / typingSpeed);
        }

        // Hold the text on screen
        float elapsed = 0f;
        while (elapsed < holdTime)
        {
            // Allow skip
            if (Input.GetKeyDown(KeyCode.Space)) break;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // Fade out text
        Color startColor = narrationText.color;
        float fadeDur = 0.5f;
        elapsed = 0f;
        while (elapsed < fadeDur)
        {
            elapsed += Time.unscaledDeltaTime;
            narrationText.color = new Color(startColor.r, startColor.g, startColor.b, 1f - (elapsed / fadeDur));
            yield return null;
        }

        narrationText.text = "";
        narrationText.color = startColor;
    }

    /// <summary>
    /// Smoothly move the camera to a target position and rotation over duration
    /// </summary>
    public IEnumerator MoveCamera(Vector3 targetPos, Quaternion targetRot, float duration)
    {
        Camera cam = Camera.main;
        if (cam == null) yield break;

        Vector3 startPos = cam.transform.position;
        Quaternion startRot = cam.transform.rotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            cam.transform.position = Vector3.Lerp(startPos, targetPos, t);
            cam.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        cam.transform.position = targetPos;
        cam.transform.rotation = targetRot;
    }

    /// <summary>
    /// Smoothly move the camera along a path of waypoints
    /// </summary>
    public IEnumerator MoveCameraAlongPath(Transform[] waypoints, float totalDuration)
    {
        if (waypoints == null || waypoints.Length < 2) yield break;

        Camera cam = Camera.main;
        if (cam == null) yield break;

        float elapsed = 0f;
        float segmentDuration = totalDuration / (waypoints.Length - 1);

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Vector3 startPos = waypoints[i].position;
            Quaternion startRot = waypoints[i].rotation;
            Vector3 endPos = waypoints[i + 1].position;
            Quaternion endRot = waypoints[i + 1].rotation;

            float segElapsed = 0f;
            while (segElapsed < segmentDuration)
            {
                segElapsed += Time.deltaTime;
                float t = Mathf.SmoothStep(0, 1, segElapsed / segmentDuration);
                cam.transform.position = Vector3.Lerp(startPos, endPos, t);
                cam.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
                yield return null;
            }
        }

        cam.transform.position = waypoints[waypoints.Length - 1].position;
        cam.transform.rotation = waypoints[waypoints.Length - 1].rotation;
    }

    /// <summary>
    /// Wait for the player to press a key
    /// </summary>
    public IEnumerator WaitForInput(KeyCode key = KeyCode.Space)
    {
        while (!Input.GetKeyDown(key))
        {
            yield return null;
        }
    }

    /// <summary>
    /// Disable player control during cutscene
    /// </summary>
    public void BeginCutscene()
    {
        isPlayingCutscene = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetGameState(GameState.Cutscene);
        }

        OnCutsceneStarted?.Invoke();
        Debug.Log("[Cutscene] Started");
    }

    /// <summary>
    /// Restore player control after cutscene
    /// </summary>
    public void EndCutscene()
    {
        isPlayingCutscene = false;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetGameState(GameState.Playing);
            if (GameManager.Instance.player != null)
            {
                GameManager.Instance.player.EnableMovement();
            }
        }

        // Re-enable HD2D camera if present
        var hd2dCam = FindObjectOfType<HD2DCameraController>();
        if (hd2dCam != null)
        {
            hd2dCam.enabled = true;
            hd2dCam.SnapToTarget();
        }

        OnCutsceneEnded?.Invoke();
        Debug.Log("[Cutscene] Ended");
    }

    /// <summary>
    /// Check if a cutscene is currently playing
    /// </summary>
    public bool IsPlaying()
    {
        return isPlayingCutscene;
    }
}
