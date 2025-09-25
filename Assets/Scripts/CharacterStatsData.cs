using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "RPG/Character Stats")]
public class CharacterStatsData : ScriptableObject
{
    [Header("Thông tin chung")]
    public string characterName;
    public int baseLevel = 1;
    public int baseExpToNextLevel = 100;

    [Header("Chỉ số cơ bản")]
    public int baseDamage = 10;
    public int baseDefense = 5;

    [Header("Chỉ số sinh tồn")]
    public int baseMaxHP = 100;
    public int baseMaxMP = 50;

    [Header("Chí mạng")]
    [Range(0f, 1f)] public float baseCritChance = 0.1f;
    public float baseCritDamage = 1.5f;
}
