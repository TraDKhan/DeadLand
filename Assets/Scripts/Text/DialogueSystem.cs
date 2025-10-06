using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    [Header("Typing Effect")]
    public float typingSpeed = 0.03f;

    private string[] lines;
    private int index;
    private string currentNPCName;
    private System.Action onEnd;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialoguePanel.SetActive(false);

        if (nextButton != null)
            nextButton.onClick.AddListener(NextLine);
    }

    /// <summary>
    /// Gọi để hiển thị đoạn hội thoại.
    /// </summary>
    public void Show(string[] dialogueLines, System.Action endCallback = null, string npcName = "NPC")
    {
        dialoguePanel.SetActive(true);
        lines = dialogueLines;
        index = 0;
        onEnd = endCallback;
        currentNPCName = npcName;
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        if (index < 0 || index >= lines.Length)
        {
            EndDialogue();
            return;
        }

        nameText.text = currentNPCName;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(lines[index]));
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void NextLine()
    {
        if (isTyping)
        {
            // Nếu người chơi bấm khi đang gõ — hiện nhanh toàn bộ
            StopCoroutine(typingCoroutine);
            dialogueText.text = lines[index];
            isTyping = false;
            return;
        }

        index++;

        if (index < lines.Length)
            ShowCurrentLine();
        else
            EndDialogue();
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        onEnd?.Invoke();
    }

    private void Update()
    {
        // Cho phép nhấn Q để sang câu hoặc kết thúc
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            NextLine();
        }
    }
}
