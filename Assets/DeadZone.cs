using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 
public class DeadZone : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the Player
        if (collision.CompareTag("Player"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.LogError("[Game Over] Player is dead! Loading GameOverScene...");
        SceneManager.LoadScene("GameOverScene");
    }
}
