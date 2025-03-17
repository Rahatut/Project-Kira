using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 
public class GameEnd : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the Player
        if (collision.CompareTag("Player"))
        {
            GameExit();
        }
    }

    private void GameExit()
    {
        Debug.LogError("[Game Ended] Player Escaped! You Won!");
        SceneManager.LoadScene("GameExitScene");
    }
}
