using UnityEngine;

public class Exit : MonoBehaviour
{
    // This method will exit the game
    public void ExitApplication()
    {
        Debug.Log("Exiting game...");

        // Exit the application
        Application.Quit();

        // Note: Application.Quit does not work in the Unity Editor.
        // This is for testing purposes in the Editor.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

