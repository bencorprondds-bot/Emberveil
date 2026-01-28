using UnityEngine;

/// <summary>
/// Central game manager that persists across scenes.
/// Handles game state, save/load, and cross-scene references.
/// 
/// Uses singleton pattern - access via GameManager.Instance
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }
    
    [Header("Game State")]
    [SerializeField] private GameState currentState = GameState.Playing;
    
    [Header("Player Reference")]
    [Tooltip("Automatically found if left empty")]
    public PlayerController player;
    public GloveController gloves;
    
    [Header("Debug")]
    [SerializeField] private bool showDebugInfo = true;
    
    // Events
    public System.Action<GameState> OnGameStateChanged;
    
    void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Subscribe to scene loading to find player in new scenes
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Find player in new scene
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
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
        
        if (gloves == null && player != null)
        {
            gloves = player.GetComponent<GloveController>();
        }
    }
    
    /// <summary>
    /// Change the current game state
    /// </summary>
    public void SetGameState(GameState newState)
    {
        if (currentState == newState) return;
        
        GameState oldState = currentState;
        currentState = newState;
        
        // Handle state transitions
        switch (newState)
        {
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Dialogue:
                // Player freezing handled by TalkableNPC
                break;
            case GameState.Cutscene:
                if (player != null) player.DisableMovement();
                break;
        }
        
        OnGameStateChanged?.Invoke(newState);
        
        if (showDebugInfo)
        {
            Debug.Log($"[GameManager] State changed: {oldState} -> {newState}");
        }
    }
    
    /// <summary>
    /// Get current game state
    /// </summary>
    public GameState GetGameState()
    {
        return currentState;
    }
    
    /// <summary>
    /// Pause the game
    /// </summary>
    public void Pause()
    {
        if (currentState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
        }
    }
    
    /// <summary>
    /// Resume from pause
    /// </summary>
    public void Resume()
    {
        if (currentState == GameState.Paused)
        {
            SetGameState(GameState.Playing);
        }
    }
    
    /// <summary>
    /// Toggle pause state
    /// </summary>
    public void TogglePause()
    {
        if (currentState == GameState.Paused)
            Resume();
        else if (currentState == GameState.Playing)
            Pause();
    }
    
    void Update()
    {
        // Global pause toggle
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Simple debug display
        GUILayout.BeginArea(new Rect(10, 10, 200, 100));
        GUILayout.Label($"State: {currentState}");
        GUILayout.Label($"Scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
        GUILayout.EndArea();
    }
}

/// <summary>
/// Possible game states
/// </summary>
public enum GameState
{
    Playing,    // Normal gameplay
    Paused,     // Pause menu open
    Dialogue,   // In conversation
    Cutscene,   // Watching a cutscene
    Loading,    // Scene transition
    Menu        // Main menu
}
