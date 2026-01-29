using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Manages the dialogue UI display for conversations.
/// Shows character name, dialogue text, and portrait.
///
/// Designed to integrate with Yarn Spinner when installed.
/// For now, works with the placeholder dialogue system.
/// </summary>
public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("The root panel containing all dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [Tooltip("Text component for character name")]
    [SerializeField] private Text characterNameText;

    [Tooltip("Text component for dialogue content")]
    [SerializeField] private Text dialogueText;

    [Tooltip("Image component for character portrait")]
    [SerializeField] private Image portraitImage;

    [Tooltip("Text showing 'Press Space to continue'")]
    [SerializeField] private Text continuePrompt;

    [Header("Typewriter Effect")]
    [Tooltip("Characters per second for typewriter effect")]
    [SerializeField] private float typewriterSpeed = 40f;

    [Tooltip("Sound to play for each character (optional)")]
    [SerializeField] private AudioClip typewriterSound;

    [Tooltip("Skip typewriter on input?")]
    [SerializeField] private bool allowSkipTypewriter = true;

    [Header("Animation")]
    [Tooltip("Time to fade panel in/out")]
    [SerializeField] private float fadeDuration = 0.2f;

    // State
    private bool isShowing = false;
    private bool isTyping = false;
    private string fullText = "";
    private Coroutine typewriterCoroutine;
    private AudioSource audioSource;

    // Events
    public System.Action OnDialogueShow;
    public System.Action OnDialogueHide;
    public System.Action OnLineComplete;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();

        // Start hidden
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    /// <summary>
    /// Show the dialogue panel
    /// </summary>
    public void Show()
    {
        if (isShowing) return;

        isShowing = true;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(FadePanel(0f, 1f));
        }

        // Hide continue prompt initially
        if (continuePrompt != null)
        {
            continuePrompt.gameObject.SetActive(false);
        }

        OnDialogueShow?.Invoke();
    }

    /// <summary>
    /// Hide the dialogue panel
    /// </summary>
    public void Hide()
    {
        if (!isShowing) return;

        // Stop any ongoing typewriter
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
            typewriterCoroutine = null;
        }

        isTyping = false;
        isShowing = false;

        StartCoroutine(FadeAndHide());

        OnDialogueHide?.Invoke();
    }

    /// <summary>
    /// Display a line of dialogue
    /// </summary>
    /// <param name="characterName">Name of the speaking character</param>
    /// <param name="text">The dialogue text</param>
    /// <param name="portrait">Optional portrait sprite</param>
    public void ShowLine(string characterName, string text, Sprite portrait = null)
    {
        if (!isShowing)
        {
            Show();
        }

        // Set character name
        if (characterNameText != null)
        {
            characterNameText.text = characterName;
        }

        // Set portrait
        if (portraitImage != null)
        {
            if (portrait != null)
            {
                portraitImage.sprite = portrait;
                portraitImage.gameObject.SetActive(true);
            }
            else
            {
                portraitImage.gameObject.SetActive(false);
            }
        }

        // Hide continue prompt while typing
        if (continuePrompt != null)
        {
            continuePrompt.gameObject.SetActive(false);
        }

        // Start typewriter effect
        fullText = text;

        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        typewriterCoroutine = StartCoroutine(TypewriterEffect());
    }

    /// <summary>
    /// Display dialogue instantly without typewriter effect
    /// </summary>
    public void ShowLineInstant(string characterName, string text, Sprite portrait = null)
    {
        if (!isShowing)
        {
            Show();
        }

        if (characterNameText != null)
        {
            characterNameText.text = characterName;
        }

        if (dialogueText != null)
        {
            dialogueText.text = text;
        }

        if (portraitImage != null)
        {
            if (portrait != null)
            {
                portraitImage.sprite = portrait;
                portraitImage.gameObject.SetActive(true);
            }
            else
            {
                portraitImage.gameObject.SetActive(false);
            }
        }

        if (continuePrompt != null)
        {
            continuePrompt.gameObject.SetActive(true);
        }

        isTyping = false;
        fullText = text;
        OnLineComplete?.Invoke();
    }

    /// <summary>
    /// Skip the typewriter effect and show full text immediately
    /// </summary>
    public void SkipTypewriter()
    {
        if (!isTyping) return;

        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
            typewriterCoroutine = null;
        }

        if (dialogueText != null)
        {
            dialogueText.text = fullText;
        }

        if (continuePrompt != null)
        {
            continuePrompt.gameObject.SetActive(true);
        }

        isTyping = false;
        OnLineComplete?.Invoke();
    }

    void Update()
    {
        // Allow skipping typewriter with input
        if (isTyping && allowSkipTypewriter)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                SkipTypewriter();
            }
        }
    }

    /// <summary>
    /// Typewriter effect coroutine
    /// </summary>
    private IEnumerator TypewriterEffect()
    {
        isTyping = true;

        if (dialogueText != null)
        {
            dialogueText.text = "";

            foreach (char c in fullText)
            {
                dialogueText.text += c;

                // Play sound for visible characters
                if (audioSource != null && typewriterSound != null && !char.IsWhiteSpace(c))
                {
                    audioSource.pitch = Random.Range(0.9f, 1.1f); // Slight variation
                    audioSource.PlayOneShot(typewriterSound);
                }

                // Wait based on character type
                float delay = 1f / typewriterSpeed;

                // Pause longer for punctuation
                if (c == '.' || c == '!' || c == '?')
                {
                    delay *= 4f;
                }
                else if (c == ',')
                {
                    delay *= 2f;
                }

                yield return new WaitForSeconds(delay);
            }
        }

        isTyping = false;

        // Show continue prompt
        if (continuePrompt != null)
        {
            continuePrompt.gameObject.SetActive(true);
        }

        OnLineComplete?.Invoke();
    }

    /// <summary>
    /// Fade the panel's CanvasGroup alpha
    /// </summary>
    private IEnumerator FadePanel(float from, float to)
    {
        CanvasGroup canvasGroup = dialoguePanel.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = dialoguePanel.AddComponent<CanvasGroup>();
        }

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    /// <summary>
    /// Fade out then hide the panel
    /// </summary>
    private IEnumerator FadeAndHide()
    {
        yield return StartCoroutine(FadePanel(1f, 0f));

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    /// <summary>
    /// Check if dialogue is currently showing
    /// </summary>
    public bool IsShowing()
    {
        return isShowing;
    }

    /// <summary>
    /// Check if typewriter is still running
    /// </summary>
    public bool IsTyping()
    {
        return isTyping;
    }

    /// <summary>
    /// Set the continue prompt text
    /// </summary>
    public void SetContinuePromptText(string text)
    {
        if (continuePrompt != null)
        {
            continuePrompt.text = text;
        }
    }
}
