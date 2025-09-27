using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("NPC Info")]
    public string npcName = "NPC"; // tên NPC hiển thị
    [TextArea(2, 5)]
    public string[] dialogueLines;

    private int currentLine = 0;
    private bool isTalking = false;

    public void Interact()
    {
        if (!isTalking)
        {
            isTalking = true;
            currentLine = 0;
            ShowCurrentLine();
        }
        else
        {
            NextLine();
        }
    }

    private void ShowCurrentLine()
    {
        if (dialogueLines.Length > 0 && DialogueUI.Instance != null)
        {
            DialogueUI.Instance.ShowDialogue(npcName, dialogueLines[currentLine]);
        }
    }

    private void NextLine()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            EndInteraction();
        }
    }

    public void EndInteraction()
    {
        isTalking = false;
        if (DialogueUI.Instance != null)
            DialogueUI.Instance.HideDialogue();
    }
}
