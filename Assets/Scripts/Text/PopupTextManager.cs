using UnityEngine;
using TMPro;

public class PopupTextManager : MonoBehaviour
{
    public static PopupTextManager Instance;

    [Header("UI References")]
    public Camera uiCamera;             
    public Canvas uiCanvas;             
    public PopupText popupPrefab;       

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowText(string message, Vector3 worldPos, Color color, float scale = 1f)
    {
        if (popupPrefab == null || uiCanvas == null || uiCamera == null)
        {
            Debug.LogWarning("PopupTextManager: Thiếu tham chiếu UI.");
            return;
        }

        PopupText popup = Instantiate(popupPrefab, uiCanvas.transform);
        popup.Setup(message, worldPos, uiCamera, color, scale);
    }

    public void ShowDamage(int damageAmount, Vector3 worldPos)
    {
        ShowText(damageAmount.ToString(), worldPos, Color.red, 1.3f);
    }

    public void ShowDamageCrit(int damageAmount, Vector3 worldPos)
    {
        ShowText(damageAmount.ToString(), worldPos, Color.yellow, 1.3f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PopupTextManager.Instance.ShowDamage(100, transform.position);
            PopupTextManager.Instance.ShowDamageCrit(200, transform.position + Vector3.right * 2);
        }
    }

}
