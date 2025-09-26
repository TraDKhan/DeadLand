using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsUI : MonoBehaviour
{
    [Header("Nguồn dữ liệu (runtime)")]
    public PlayerStatsManager playerHealth; // Lấy runtime stats thông qua PlayerHealth

    private Character runtimeStats;

    [Header("Text hiển thị")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI critChanceText;
    public TextMeshProUGUI critDamageText;

    [Header("EXP")]
    public Image expFillImage;
    public TextMeshProUGUI expText;

    void Start()
    {
        if (playerHealth != null)
        {
            runtimeStats = playerHealth.GetRuntimeStats();
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (runtimeStats == null)
        {
            Debug.LogWarning("⚠ runtimeStats vẫn là null!");
            return;
        }

        nameText.text = runtimeStats.characterName;
        levelText.text = $"Cấp độ: {runtimeStats.level}";
        damageText.text = $"Sát thương: {runtimeStats.GetTotalDamage()}";
        defenseText.text = $"Phòng thủ: {runtimeStats.GetTotalDefense()}";
        hpText.text = $"HP: {runtimeStats.currentHP}/{runtimeStats.GetTotalMaxHP()}";
        mpText.text = $"MP: {runtimeStats.currentMP}/{runtimeStats.GetTotalMaxMP()}";
        critChanceText.text = $"Tỉ lệ chí mạng: {(runtimeStats.GetTotalCritChance() * 100):F1}%";
        critDamageText.text = $"ST Chí mạng: {runtimeStats.GetTotalCritDamage():F1}x";

        expText.text = $"EXP: {runtimeStats.exp}/{runtimeStats.expToNextLevel}";
        if (expFillImage != null)
        {
            expFillImage.fillAmount = (float)runtimeStats.exp / runtimeStats.expToNextLevel;
        }
    }
}
