using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    private QuestData currentQuest;

    public void Setup(QuestData quest)
    {
        currentQuest = quest;
        titleText.text = quest.title;

        // Gán sự kiện khi click vào button
        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => QuestUI.Instance.ShowQuestDetail(currentQuest));
        }
    }
}
