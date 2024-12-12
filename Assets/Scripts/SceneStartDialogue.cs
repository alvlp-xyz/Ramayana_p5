using UnityEngine;
using System.Collections;

public class SceneStartDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public float delay = 1.0f; // Delay before dialogue starts

    void Start()
    {
        StartCoroutine(StartDialogueAfterDelay());
    }

    IEnumerator StartDialogueAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        dialogueManager.StartDialogue();
    }
}
