using UnityEngine;
using UnityEngine.UI;

public class HealthBarHUD : MonoBehaviour
{
    public static HealthBarHUD Instance;

    [Header("UI Reference")]
    public Image healthFillImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateHealth(int currentHP, int maxHP)
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = (float)currentHP / maxHP;
        }
    }
}
