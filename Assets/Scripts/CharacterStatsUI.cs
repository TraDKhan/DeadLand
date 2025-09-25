using TMPro;
using UnityEngine;

public class CharacterStatsUI : MonoBehaviour
{
    [Header("Nguồn dữ liệu (runtime)")]
    public PlayerHealth playerHealth; // Lấy runtime stats thông qua PlayerHealth

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
    public TextMeshProUGUI expText;

    void Start()
    {
        if (playerHealth != null)
        {
            runtimeStats = playerHealth.GetRuntimeStats();
        }

        UpdateUI();
    }

    void Update()
    {
        // Cập nhật realtime (HP, MP thay đổi liên tục)
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (runtimeStats == null) return;

        nameText.text = runtimeStats.characterName;
        levelText.text = $"Cấp độ: {runtimeStats.level}";
        damageText.text = $"Sát thương: {runtimeStats.damage}";
        defenseText.text = $"Phòng thủ: {runtimeStats.defense}";
        hpText.text = $"HP: {runtimeStats.currentHP}/{runtimeStats.maxHP}";
        mpText.text = $"MP: {runtimeStats.currentMP}/{runtimeStats.maxMP}";
        critChanceText.text = $"Tỉ lệ chí mạng: {(runtimeStats.critChance * 100):F1}%";
        critDamageText.text = $"ST Chí mạng: {runtimeStats.critDamage}x";

        // 🟢 EXP hiển thị
        expText.text = $"EXP: {runtimeStats.exp}/{runtimeStats.expToNextLevel}";
    }
}
