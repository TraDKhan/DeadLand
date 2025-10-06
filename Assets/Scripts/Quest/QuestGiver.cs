using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public string questID;
    public string npcName = "NPC";
    public string[] dialogueBefore;
    public string[] dialogueInProgress;
    public string[] dialogueComplete;
    public string[] dialogueAfter;

    public void Interact()
    {
        var quest = QuestManager.Instance.allQuests.Find(q => q.questID == questID);
        if (quest == null) return;

        switch (quest.status)
        {
            case QuestStatus.NotStarted:
                DialogueSystem.Instance.Show(dialogueBefore,
                    () => QuestManager.Instance.StartQuest(questID), npcName);
                break;
            case QuestStatus.InProgress:
                DialogueSystem.Instance.Show(dialogueInProgress, null, npcName);
                break;
            case QuestStatus.Completed:
                DialogueSystem.Instance.Show(dialogueComplete,
                    () => QuestManager.Instance.ClaimReward(questID), npcName);
                break;
            case QuestStatus.RewardClaimed:
                DialogueSystem.Instance.Show(dialogueAfter, null, npcName);
                break;
        }
    }
}
