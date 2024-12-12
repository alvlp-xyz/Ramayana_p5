using UnityEngine; // Make sure this is included

[System.Serializable]
public class Dialogue
{
    public int sortOrder;     // Determines the order of dialogues
    public string header;     // Character name
    public string body;       // Dialogue message
    public Sprite image;      // Image to display with this dialogue
}
