using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueData dialogueData;

    private bool isTalking = false;

    public void Interact()
    {
        if (dialogueData == null || dialogueData.dialogueLines == null || dialogueData.dialogueLines.Length == 0)
        {
            Debug.LogWarning($"⚠️ NPC {name} chưa gán DialogueData hoặc trống!");
            return;
        }

        // Nếu đang nói, chuyển sang câu tiếp (nếu DialogueSystem hỗ trợ)
        if (isTalking)
        {
            if (DialogueSystem.Instance != null)
                DialogueSystem.Instance.NextLine();
            return;
        }

        // Bắt đầu hội thoại
        isTalking = true;

        if (DialogueSystem.Instance != null)
        {
            DialogueSystem.Instance.Show(
                dialogueData.dialogueLines,
                EndInteraction,                      // callback khi kết thúc hội thoại
                dialogueData.npcName                 // tên NPC
            );
        }
    }

    private void EndInteraction()
    {
        isTalking = false;
    }
}
