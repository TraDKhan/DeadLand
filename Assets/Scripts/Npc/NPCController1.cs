using UnityEngine;
using System.Linq;

public class NPCController1 : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueSet dialogueSet;

    private DialogueEntry currentDialogue;
    private int currentLine = 0;
    private bool isTalking = false;

    public void StartDialogue(string dialogueID)
    {
        if (dialogueSet == null || dialogueSet.dialogues.Length == 0)
        {
            Debug.LogWarning("⚠️ NPC chưa gán DialogueSet!");
            return;
        }

        // Tìm đoạn thoại theo ID
        currentDialogue = dialogueSet.dialogues
            .FirstOrDefault(d => d.dialogueID == dialogueID);

        if (currentDialogue == null)
        {
            Debug.LogWarning("⚠️ NPC không có đoạn thoại ID: " + dialogueID);
            return;
        }

        isTalking = true;
        currentLine = 0;
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        if (currentDialogue != null && DialogueUI.Instance != null)
        {
            DialogueUI.Instance.ShowDialogue(
                dialogueSet.npcName,
                currentDialogue.dialogueLines[currentLine]
            );
        }
    }

    public void NextLine()
    {
        if (currentDialogue == null) return;

        currentLine++;
        if (currentLine < currentDialogue.dialogueLines.Length)
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
        currentDialogue = null;
        if (DialogueUI.Instance != null)
            DialogueUI.Instance.HideDialogue();
    }
}
