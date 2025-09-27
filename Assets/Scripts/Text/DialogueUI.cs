using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    [Header("Typewriter Effect")]
    public float typingSpeed = 0.03f; // tốc độ hiện chữ
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialoguePanel.SetActive(false);

        if (nextButton != null)
            nextButton.onClick.AddListener(NextLine);
    }

    public void ShowDialogue(string npcName, string message)
    {
        dialoguePanel.SetActive(true);
        nameText.text = npcName;

        // Nếu đang chạy hiệu ứng trước đó thì dừng
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        dialogueText.text = "";
        foreach (char c in message.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    // Hàm này sẽ được gọi từ NPCController để qua câu tiếp theo
    public void NextLine()
    {
        // chỉ để nút Next gọi, NPCController sẽ quản lý câu thoại
    }
}
