using UnityEngine;

public class About : MonoBehaviour
{
    [Header("URL Settings")]
    [Tooltip("The URL to open when triggered.")]
    public string url = "https://example.com"; // URL input through Inspector.

    // Method to open the URL
    public void OpenURL()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Debug.Log("Opening URL: " + url);
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogError("No URL provided! Please set a valid URL in the Inspector.");
        }
    }
}

