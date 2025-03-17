using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Required for InputAction

public class GamePlayUIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Handles exit game input action
    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
        Debug.Log("Application.Quit() called!"); // Check if this appears in the logs
        Application.Quit();
#elif (UNITY_WEBGL)
        SceneManager.LoadScene("QuitScene");
#endif
        }
    }

}
