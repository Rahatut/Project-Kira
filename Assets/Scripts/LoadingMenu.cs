using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    private ProgressBar loadingBar;

    private void OnEnable()
    {
        Debug.Log("LoadingScreenController: Loading screen started!");

        var root = GetComponent<UIDocument>().rootVisualElement;
        loadingBar = root.Q<ProgressBar>("LoadingBar");

        if (loadingBar == null)
        {
            Debug.LogError("LoadingScreenController: ProgressBar 'LoadingBar' not found in UI!");
        }

        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        Debug.Log("LoadingScreenController: Starting async load for GamePlayScene...");

        AsyncOperation operation = SceneManager.LoadSceneAsync("GamePlayScene");
        operation.allowSceneActivation = false; // Prevent auto-load until progress reaches 100%

        float displayedProgress = 0f;

        while (!operation.isDone)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize progress
            Debug.Log($"LoadingScreenController: Target progress - {targetProgress * 100}%");

            // Slow down progress bar update
            while (displayedProgress < targetProgress)
            {
                displayedProgress = Mathf.Lerp(displayedProgress, targetProgress, Time.deltaTime * 2.5f); // Faster animation

                if (loadingBar != null)
                    loadingBar.value = displayedProgress; // Corrected for 0-1 range

                yield return new WaitForSeconds(0.05f);
            }

            if (displayedProgress >= 1.0f) // Ensures scene loads when UI reaches 100%
            {
                Debug.Log("LoadingScreenController: Load completed! Waiting 3 seconds before activation...");
                yield return new WaitForSeconds(3.0f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
