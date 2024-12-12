using UnityEngine;
using UnityEngine.SceneManagement;

public class CMM : MonoBehaviour
{
    // Method to switch from MainMenu to Game
    public void GoToGameScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
