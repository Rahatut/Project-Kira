using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class YouPassedUI : MonoBehaviour
// Inheritance - YouPassedUI inherits from MonoBehaviour, which is a Unity class.
{
    private void OnEnable() // Encapsulation - This method is private, meaning other scripts cannot directly modify how UI elements are retrieved and handled.
    // Polymorphism - OnEnable() is a virtual method in Unity's MonoBehaviour, and your script overrides it to customize behavior.
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Button exitButton = root.Q<Button>("exitButton"); // Abstraction - This abstracts the process of finding the UI button and assigning an event listener.
        // The user of this class does not need to know how the buttons are fetched from the UI.
        if (exitButton != null)
        {
            exitButton.clicked += () =>
            {
                Debug.Log("[UI] Exit Button Clicked! Loading 'MainMenu' scene...");
                SceneManager.LoadScene("MainMenu");
            };
        }
        else
        {
            Debug.LogError("[UI] exitButton not found!");
        }
    }
}
