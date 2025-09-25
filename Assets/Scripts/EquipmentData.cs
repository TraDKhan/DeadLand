using UnityEngine;

public enum EquipmentType { Weapon, Armor, Ring, Necklace }

[CreateAssetMenu(fileName = "NewEquipment", menuName = "RPG/Equipment")]
public class EquipmentData : ScriptableObject
{
    public string itemName;
    public EquipmentType equipmentType;

    [Header("Chỉ số cộng thêm")]
    public int bonusDamage;
    public int bonusDefense;
    public int bonusHP;
    public int bonusMP;
    public float bonusCritChance;
    public float bonusCritDamage;
}
