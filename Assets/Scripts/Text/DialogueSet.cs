using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string dialogueID; // ví dụ: "first_meet", "quest_done", "shop"
    [TextArea(2, 5)]
    public string[] dialogueLines;
}

[CreateAssetMenu(fileName = "DialogueSet", menuName = "Dialogue/Dialogue Set")]
public class DialogueSet : ScriptableObject
{
    public string npcName;
    public DialogueEntry[] dialogues;
}
