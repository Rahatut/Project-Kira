using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverUIController : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Get the Restart and Exit buttons
        Button restartButton = root.Q<Button>("restartButton");
        Button exitButton = root.Q<Button>("exitButton");

        // Check if restartButton exists
        if (restartButton != null)
        {
            restartButton.clicked += () =>
            {
                Debug.Log("Restart Button Clicked! Reloading 'GamePlayScene'...");
                SceneManager.LoadScene("GamePlayScene"); // Ensure the scene name is correct
            };
        }
        else
        {
            Debug.LogError("restartButton not found in Game Over UI!");
        }

        // Check if exitButton exists
        if (exitButton != null)
        {
            exitButton.clicked += () =>
            {
                Debug.Log("Exit Button Clicked! Loading 'MainMenu'...");
                SceneManager.LoadScene("MainMenu"); // Ensure the scene name is correct
            };
        }
        else
        {
            Debug.LogError("exitButton not found in Game Over UI!");
        }
    }
}
