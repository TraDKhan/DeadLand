using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public Transform contentParent;
    public GameObject questItemPrefab;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (contentParent == null)
            Debug.LogError("❌ contentParent chưa được gán trong Inspector!");
        if (questItemPrefab == null)
            Debug.LogError("❌ questItemPrefab chưa được gán!");
        if (QuestManager.Instance == null)
            Debug.LogError("❌ QuestManager.Instance == null!");
        if (QuestManager.Instance.allQuests == null)
            Debug.LogError("❌ QuestManager.Instance.allQuests == null!");

        // Sau khi kiểm tra, nếu có null thì return
        if (contentParent == null || questItemPrefab == null || QuestManager.Instance == null)
            return;
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var quest in QuestManager.Instance.allQuests)
        {
            var obj = Instantiate(questItemPrefab, contentParent);
            var text = obj.GetComponentInChildren<TextMeshProUGUI>();
            int progress = QuestManager.Instance.GetProgress(quest.questID);
            text.text = $"{quest.title} ({quest.status})\n{quest.description}\nTiến độ: {progress}/{quest.requiredAmount}";
        }
    }
}
