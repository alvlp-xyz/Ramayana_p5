using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    [Header("Scene Change Settings")]
    [Tooltip("The number of the scene to load (based on Build Settings).")]
    public int sceneNumber; // Scene number input through Inspector.

    // Call this method to change the scene using the configured scene number.
    public void ChangeScene()
    {
        Debug.Log("Attempting to load scene with number: " + sceneNumber);

        // Check if the scene number is valid.
        if (sceneNumber >= 0 && sceneNumber < SceneManager.sceneCountInBuildSettings)
        {
            // Load the scene by its index in the Build Settings.
            SceneManager.LoadScene(sceneNumber);
        }
        else
        {
            Debug.LogError("Scene number " + sceneNumber + " is out of range. Check your Build Settings.");
        }
    }
}

