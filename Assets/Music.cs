using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    private static Music instance;
    public string gameplaySceneName = "GamePlayScene"; // Change this to your actual gameplay scene name

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep music across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != gameplaySceneName)
        {
            Destroy(gameObject); // Stop music if it's not the gameplay scene
        }
    }
}
