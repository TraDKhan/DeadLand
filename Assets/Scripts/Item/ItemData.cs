using UnityEngine;

public enum ItemType { Gold, Potion, Equipment, Material, Quest }
public enum EquipmentType { Weapon, Armor, Ring, Necklace }

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("Cơ bản")]
    public string id;
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public bool stackable = true;
    public GameObject worldPrefab;

    [Header("Chỉ dành cho Equipment")]
    public EquipmentType equipmentType;
    public int bonusDamage;
    public int bonusDefense;
    public int bonusHP;
    public int bonusMP;
    public float bonusCritChance;
    public float bonusCritDamage;
}
