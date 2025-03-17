using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
// Inheritance - MainMenuUIController inherits from MonoBehaviour, which is a Unity class.
{
    private void OnEnable() // Encapsulation - This method is private, meaning other scripts cannot directly modify how UI elements are retrieved and handled.
    // Polymorphism - OnEnable() is a virtual method in Unity's MonoBehaviour, and your script overrides it to customize behavior.
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Button newGameButton = root.Q<Button>("newGameButton"); // Abstraction - This abstracts the process of finding the UI button and assigning an event listener.
        // The user of this class does not need to know how the buttons are fetched from the UI.
        if (newGameButton != null)
        {
            newGameButton.clicked += () =>
            {
                Debug.Log("New Game Button Clicked! Loading 'LoadingMenu' scene...");
                SceneManager.LoadScene("LoadingMenu");
            };
        }
        else
        {
            Debug.LogError("newGameButton not found!");
        }
    }
}