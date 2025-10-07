using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public static QuestUI Instance;

    [Header("Danh sách nhiệm vụ")]
    public Transform contentParent;
    public GameObject questItemPrefab;

    [Header("Khung chi tiết nhiệm vụ")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI progressText;
    public Button claimButton; // 🟢 Nút nhận thưởng
    public TextMeshProUGUI claimButtonText; // 🟡 Text hiển thị trên nút

    private QuestData currentQuest;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        RefreshList();
    }

    public void RefreshList()
    {
        if (contentParent == null || questItemPrefab == null || QuestManager.Instance == null)
        {
            Debug.LogError("⚠️ Chưa gán tham chiếu cần thiết trong QuestUI!");
            return;
        }

        // Xóa danh sách cũ
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Tạo danh sách nhiệm vụ
        foreach (var quest in QuestManager.Instance.allQuests)
        {
            var obj = Instantiate(questItemPrefab, contentParent);
            var itemUI = obj.GetComponent<QuestItemUI>();
            if (itemUI != null)
                itemUI.Setup(quest);
        }

        ClearDetail();
    }

    public void ShowQuestDetail(QuestData quest)
    {
        currentQuest = quest;

        int progress = QuestManager.Instance.GetProgress(quest.questID);

        titleText.text = $"{quest.title} ({quest.status})";
        descriptionText.text = quest.description;
        progressText.text = $"Tiến độ: {progress}/{quest.requiredAmount}";

        UpdateClaimButton(quest);
    }

    private void UpdateClaimButton(QuestData quest)
    {
        if (claimButton == null || claimButtonText == null)
            return;

        claimButton.onClick.RemoveAllListeners();

        switch (quest.status)
        {
            case QuestStatus.Completed:
                claimButton.interactable = true;
                claimButtonText.text = "Hoàn thành";
                claimButtonText.color = Color.green;
                claimButton.onClick.AddListener(() =>
                {
                    QuestManager.Instance.ClaimReward(quest.questID);
                    ShowQuestDetail(quest); // cập nhật lại giao diện
                    RefreshList();
                });
                break;

            case QuestStatus.RewardClaimed:
                claimButton.interactable = false;
                claimButtonText.text = "Đã nhận";
                claimButtonText.color = new Color(1f, 0.85f, 0.3f); // vàng nhạt
                break;

            default:
                claimButton.interactable = false;
                claimButtonText.text = "Chưa hoàn thành";
                claimButtonText.color = Color.gray;
                break;
        }
    }

    public void ClearDetail()
    {
        titleText.text = "";
        descriptionText.text = "";
        progressText.text = "";

        if (claimButtonText != null)
        {
            claimButtonText.text = "Chưa hoàn thành";
            claimButtonText.color = Color.gray;
        }

        if (claimButton != null)
            claimButton.interactable = false;
    }
}
