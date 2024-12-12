using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For scene management
using System.Collections.Generic;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public int sortOrder;     // Determines the order of dialogues
        public string header;     // Character name
        public string body;       // Dialogue message
        public Sprite image;      // Image to display with this dialogue
    }

    public GameObject dialogueBox;     // UI Panel for the dialogue
    public Text headerText;            // UI Text for the header
    public Text bodyText;              // UI Text for the body
    public Image dialogueImage;        // UI Image to display associated image

    public List<Dialogue> dialogues;   // List of dialogues
    public int nextSceneId;            // Scene ID to load after dialogue ends

    private Queue<Dialogue> dialogueQueue;
    private bool isDialogueActive = false;

    void Start()
    {
        dialogueQueue = new Queue<Dialogue>();
        dialogueBox.SetActive(false);
    }

    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
        isDialogueActive = true;

        // Sort dialogues by their sortOrder before enqueueing
        foreach (var dialogue in dialogues.OrderBy(d => d.sortOrder))
        {
            dialogueQueue.Enqueue(dialogue);
        }

        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue currentDialogue = dialogueQueue.Dequeue();

        // Update UI with the current dialogue
        headerText.text = currentDialogue.header;
        bodyText.text = currentDialogue.body;

        if (currentDialogue.image != null)
        {
            dialogueImage.gameObject.SetActive(true);
            dialogueImage.sprite = currentDialogue.image;
        }
        else
        {
            dialogueImage.gameObject.SetActive(false);
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        isDialogueActive = false;

        // Load the next scene
        SceneManager.LoadScene(nextSceneId);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0)) // On screen tap or mouse click
        {
            DisplayNextDialogue();
        }
    }
}
