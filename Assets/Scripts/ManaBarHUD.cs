using UnityEngine;
using UnityEngine.UI;

public class ManaBarHUD : MonoBehaviour
{
    public static ManaBarHUD Instance;

    [Header("UI Reference")]
    public Image mpBar;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateMP(float current, float max)
    {
        if (mpBar != null)
        {
            mpBar.fillAmount = current / max;
        }
    }
}
