using TMPro;
using UnityEngine;

public class CharacterStatsUI : MonoBehaviour
{
    [Header("Nguồn dữ liệu")]
    public CharacterStatsData stats;

    [Header("Text hiển thị")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI critChanceText;
    public TextMeshProUGUI critDamageText;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (stats == null) return;

        nameText.text = stats.characterName;
        levelText.text = $"Cấp độ: {stats.level}";
        damageText.text = $"Sát thương: {stats.damage}";
        defenseText.text = $"Phòng thủ: {stats.defense}";
        hpText.text = $"HP: {stats.currentHP}/{stats.maxHP}";
        mpText.text = $"MP: {stats.currentMP}/{stats.maxMP}";
        critChanceText.text = $"Tỉ lệ chí mạng: {(stats.critChance * 100):F1}%";
        critDamageText.text = $"ST Chí mạng: {stats.critDamage}x";
    }
}
