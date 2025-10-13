using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<QuestData> allQuests;
    private Dictionary<string, int> progress = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartQuest(string questID)
    {
        var quest = allQuests.Find(q => q.questID == questID);
        if (quest != null && quest.status == QuestStatus.NotStarted)
        {
            quest.status = QuestStatus.InProgress;
            progress[questID] = 0;
            Debug.Log($"🟢 Nhận nhiệm vụ: {quest.title}");
        }
    }

    public void UpdateProgress(string targetID)
    {
        foreach (var quest in allQuests)
        {
            if (quest.status == QuestStatus.InProgress && quest.targetID == targetID)
            {
                if (quest.type == QuestType.Kill)
                {
                    progress[quest.questID]++;
                    Debug.Log($"🪓 Cập nhật nhiệm vụ {quest.title}: {progress[quest.questID]}/{quest.requiredAmount}");

                    if (progress[quest.questID] >= quest.requiredAmount)
                    {
                        quest.status = QuestStatus.Completed;
                        Debug.Log($"✅ Hoàn thành nhiệm vụ: {quest.title}");
                    }
                }
                else if (quest.type == QuestType.Talk)
                {
                    // ✅ Nhiệm vụ nói chuyện chỉ cần một lần
                    quest.status = QuestStatus.Completed;
                    progress[quest.questID] = quest.requiredAmount;
                    Debug.Log($"💬 Hoàn thành nhiệm vụ nói chuyện: {quest.title}");
                }
            }
        }
    }


    public void ClaimReward(string questID)
    {
        var quest = allQuests.Find(q => q.questID == questID);
        if (quest != null && quest.status == QuestStatus.Completed)
        {
            quest.status = QuestStatus.RewardClaimed;
            // Gọi hệ thống EXP/Vàng
            PlayerStats.Instance.AddExp(quest.rewardExp);
            PlayerStats.Instance.AddGold(quest.rewardGold);
            Debug.Log($"💰 Nhận thưởng: {quest.rewardGold} gold, {quest.rewardExp} exp");
        }
    }

    public int GetProgress(string questID)
    {
        return progress.ContainsKey(questID) ? progress[questID] : 0;
    }

    public void SaveProgress()
    {
        QuestSaveSystem.Save(allQuests, progress);
    }

    public void LoadProgress()
    {
        QuestSaveSystem.Load(allQuests, progress);
    }
}
