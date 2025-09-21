using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    public float moveUpSpeed = 50f;
    public float fadeDuration = 1f;

    private TextMeshProUGUI textMesh;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private float timer;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += Vector2.up * moveUpSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

        if (timer >= fadeDuration)
            Destroy(gameObject);
    }

    public void Setup(string message, Vector3 worldPosition, Camera uiCamera)
    {
        textMesh.text = message;

        // Chuyển world → screen
        Vector3 screenPos = uiCamera.WorldToScreenPoint(worldPosition);

        // Chuyển screen → local UI position
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform, // hoặc uiCanvas.transform nếu bạn truyền vào
            screenPos,
            uiCamera,
            out localPoint
        );

        rectTransform.anchoredPosition = localPoint;

        timer = 0f;
        canvasGroup.alpha = 1f;
    }

    public void SetColor(Color color)
    {
        textMesh.color = color;
    }

    public void SetScale(float scale)
    {
        rectTransform.localScale = Vector3.one * scale;
    }
}
