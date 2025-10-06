using UnityEngine;

public enum QuestType { Kill, Collect, Talk }
public enum QuestStatus { NotStarted, InProgress, Completed, RewardClaimed }

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questID;
    public string title;
    [TextArea] public string description;

    public QuestType type;
    public string targetID;      // ID quái / vật phẩm / NPC
    public int requiredAmount;   // số lượng cần đạt
    public int rewardExp;
    public int rewardGold;

    [HideInInspector] public QuestStatus status = QuestStatus.NotStarted;
}
