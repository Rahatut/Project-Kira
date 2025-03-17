using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    // Reference to the InGamePauseUI GameObject
    public GameObject inGamePauseUI;

    // Track whether the game is paused
    public static bool isPaused = false;

    private void Start()
    {
        // Ensure the pause menu is hidden at the start
        if (inGamePauseUI == null)
        {
            Debug.LogError("[PauseMenu] inGamePauseUI is not assigned in the Inspector!");
            return;
        }
        inGamePauseUI.SetActive(false);
        Debug.Log("[PauseMenu] inGamePauseUI initialized and hidden.");
    }

    private void Update()
    {
        // Check for Esc key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("[PauseMenu] Esc key pressed.");
            TogglePauseMenu();
        }
    }

    private void OnEnable()
    {
        // Ensure inGamePauseUI is assigned
        if (inGamePauseUI == null)
        {
            Debug.LogError("[PauseMenu] inGamePauseUI is not assigned in the Inspector!");
            return;
        }

        // Get the root VisualElement of the UIDocument
        var root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null)
        {
            Debug.LogError("[PauseMenu] UIDocument is missing or not properly set up.");
            return;
        }

        // Find the buttons by their IDs
        Button resumeButton = root.Q<Button>("resumeButton");
        Button restartButton = root.Q<Button>("restartButton");
        Button exitButton = root.Q<Button>("exitButton");

        // Add click event listeners
        if (resumeButton != null)
        {
            resumeButton.clicked += () =>
            {
                Debug.Log("[PauseMenu] Resume button clicked.");
                TogglePauseMenu();
            };
        }
        else
        {
            Debug.LogError("[PauseMenu] Resume button NOT found! Check UI Document.");
        }

        if (restartButton != null)
        {
            restartButton.clicked += () =>
            {
                Debug.Log("[PauseMenu] Restart button clicked. Reloading current scene...");
                Time.timeScale = 1f; // Unfreeze the game
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
            };
        }
        else
        {
            Debug.LogError("[PauseMenu] Restart button NOT found! Check UI Document.");
        }

        if (exitButton != null)
        {
            exitButton.clicked += () =>
            {
                Debug.Log("[PauseMenu] Exit button clicked. Returning to MainMenu...");
                Time.timeScale = 1f; // Unfreeze the game
                SceneManager.LoadScene("MainMenu"); // Load the MainMenu scene
            };
        }
        else
        {
            Debug.LogError("[PauseMenu] Exit button NOT found! Check UI Document.");
        }
    }

    private void TogglePauseMenu()
    {
        // Ensure inGamePauseUI is assigned
        if (inGamePauseUI == null)
        {
            Debug.LogError("[PauseMenu] Cannot toggle pause menu because inGamePauseUI is null.");
            return;
        }

        // Toggle pause state
        isPaused = !isPaused;

        // Show/hide the pause menu
        inGamePauseUI.SetActive(isPaused);
        Debug.Log($"[PauseMenu] Toggling Pause Menu. isPaused: {isPaused}, UI Active: {inGamePauseUI.activeSelf}");

        // Freeze/unfreeze the game
        Time.timeScale = isPaused ? 0f : 1f;
        Debug.Log($"[PauseMenu] Time.timeScale set to {Time.timeScale}");
    }
}