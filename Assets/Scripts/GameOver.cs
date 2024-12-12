using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;  // Reference to the Game Over UI screen (optional)
    public GameObject controlUI;       // Reference to the Control UI (buttons, movement UI)
    
    void Start()
    {
        // Ensure the Game Over screen is hidden at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Ensure the Control UI is visible at the start
        if (controlUI != null)
        {
            controlUI.SetActive(true);
        }
    }

    // Triggered when player collides with the invisible object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // Ensure it is the player
        {
            GameOverSequence();
        }
    }

    // Handle game over logic (show UI and hide controls)
    private void GameOverSequence()
    {
        // Show the Game Over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Hide the control UI (buttons, movement UI)
        if (controlUI != null)
        {
            controlUI.SetActive(false);
        }
    }

    // Method to retry the game (reload current scene)
    public void RetryGame()
    {
        // Reload the current scene using the scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

