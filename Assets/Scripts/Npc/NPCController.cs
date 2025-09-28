using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueData dialogueData;

    private int currentLine = 0;
    private bool isTalking = false;

    public void Interact()
    {
        if (dialogueData == null || dialogueData.dialogueLines.Length == 0)
        {
            Debug.LogWarning("⚠️ NPC chưa gán DialogueData!");
            return;
        }

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
        if (DialogueUI.Instance != null)
        {
            DialogueUI.Instance.ShowDialogue(
                dialogueData.npcName,
                dialogueData.dialogueLines[currentLine]
            );
        }
    }

    private void NextLine()
    {
        currentLine++;
        if (currentLine < dialogueData.dialogueLines.Length)
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
