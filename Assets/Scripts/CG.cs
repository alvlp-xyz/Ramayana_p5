using UnityEngine;
using UnityEngine.SceneManagement;

public class CG : MonoBehaviour
{
    // Method to switch from MainMenu to Game
    public void GoToGameScene()
    {
        SceneManager.LoadScene("Intro");
    }
}
