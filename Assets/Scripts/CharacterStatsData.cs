using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "RPG/Character Stats")]
public class CharacterStatsData : ScriptableObject
{
    [Header("Thông tin chung")]
    public string characterName;
    public int level = 1;
    public int exp = 0;              // Kinh nghiệm hiện tại
    public int expToNextLevel = 100; // Mốc EXP cần để lên cấp

    [Header("Chỉ số cơ bản")]
    public int damage = 10;
    public int defense = 5;

    [Header("Chỉ số sinh tồn")]
    public int maxHP = 100;
    public int currentHP = 100;
    public int maxMP = 50;
    public int currentMP = 50;

    [Header("Chí mạng")]
    [Range(0f, 1f)] public float critChance = 0.1f; // 10%
    public float critDamage = 1.5f; // 150%
    public void GainExp(int amount)
    {
        exp += amount;
        while (exp >= expToNextLevel)
        {
            exp -= expToNextLevel;
            LevelUp();
        }
    }
    private void LevelUp()
    {
        level++;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.2f); // tăng yêu cầu EXP 20% mỗi cấp

        // Tăng chỉ số mỗi lần lên cấp
        damage += 2;
        defense += 1;
        maxHP += 20;
        maxMP += 10;

        // Hồi đầy máu và mana khi lên cấp
        currentHP = maxHP;
        currentMP = maxMP;

        Debug.Log($"{characterName} đã lên cấp {level}!");
    }
}
