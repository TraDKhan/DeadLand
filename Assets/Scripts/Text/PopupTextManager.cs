using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupTextManager : MonoBehaviour
{
    public static PopupTextManager Instance;

    [Header("UI References")]
    public Camera uiCamera;             // Camera dùng để chuyển world -> screen
    public Canvas uiCanvas;             // Canvas chứa popup
    public PopupText popupPrefab;       // Prefab popup (chứa TextMeshPro)

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded; // lắng nghe khi scene mới load
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // mỗi lần scene đổi → tìm lại MainCamera
        if (uiCamera == null)
            uiCamera = Camera.main;

        if (uiCanvas != null)
            uiCanvas.worldCamera = uiCamera;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ShowText(string message, Vector3 worldPos, Color color, float scale = 1f)
    {
        if (popupPrefab == null || uiCanvas == null || uiCamera == null)
        {
            Debug.LogWarning("⚠️ PopupTextManager: Thiếu tham chiếu UI.");
            return;
        }

        // Tạo popup và setup
        PopupText popup = Instantiate(popupPrefab, uiCanvas.transform);
        popup.Setup(message, worldPos, uiCamera, color, scale);
    }

    public void ShowDamage(int damageAmount, Vector3 worldPos)
    {
        ShowText(damageAmount.ToString(), worldPos, Color.red, 1.3f);
    }

    public void ShowDamageCrit(int damageAmount, Vector3 worldPos)
    {
        ShowText(damageAmount.ToString(), worldPos, Color.yellow, 1.5f);
    }

    public void ShowEXP(int exp, Vector3 worldPos)
    {
        ShowText($"+{exp} Exp", worldPos + Vector3.right * 0.2f, Color.green, 1.2f);
    }

    public void ShowHeal(int healedAmount, Vector3 worldPos)
    {
        ShowText($"+{healedAmount} Exp", worldPos + Vector3.right * 0.2f, Color.green, 1.3f);
    }
}
