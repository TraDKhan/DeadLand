using UnityEngine;

public class Character
{
    private CharacterStatsData template;

    public string characterName;
    public int level;
    public int exp;
    public int expToNextLevel;

    public int damage;
    public int defense;

    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;

    public float critChance;
    public float critDamage;

    // Trang bị (dùng ItemData thay vì EquipmentData)
    public ItemData weapon;
    public ItemData armor;
    public ItemData ring;
    public ItemData necklace;

    // Constructor
    public Character(CharacterStatsData data)
    {
        template = data;

        characterName = data.characterName;
        level = data.baseLevel;
        exp = 0;
        expToNextLevel = data.baseExpToNextLevel;

        damage = data.baseDamage;
        defense = data.baseDefense;

        maxHP = data.baseMaxHP;
        currentHP = maxHP;

        maxMP = data.baseMaxMP;
        currentMP = maxMP;

        critChance = data.baseCritChance;
        critDamage = data.baseCritDamage;
    }

    // ===== EXP & Level =====
    public void GainExp(int amount)
    {
        exp += amount;
        while (exp >= expToNextLevel)
        {
            exp -= expToNextLevel;
            LevelUp();
        }

        CharacterStatsUI.Instance?.UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.2f);

        damage += 2;
        defense += 1;
        maxHP += 20;
        maxMP += 10;

        currentHP = maxHP;
        currentMP = maxMP;

        Debug.Log($"{characterName} đã lên cấp {level}!");
    }

    // ===== Các chỉ số cộng thêm =====
    public int GetTotalDamage()
    {
        int total = damage;
        if (weapon != null) total += weapon.bonusDamage;
        if (armor != null) total += armor.bonusDamage;
        if (ring != null) total += ring.bonusDamage;
        if (necklace != null) total += necklace.bonusDamage;
        return total;
    }

    public int GetTotalDefense()
    {
        int total = defense;
        if (weapon != null) total += weapon.bonusDefense;
        if (armor != null) total += armor.bonusDefense;
        if (ring != null) total += ring.bonusDefense;
        if (necklace != null) total += necklace.bonusDefense;
        return total;
    }

    public int GetTotalMaxHP()
    {
        int total = maxHP;
        if (weapon != null) total += weapon.bonusHP;
        if (armor != null) total += armor.bonusHP;
        if (ring != null) total += ring.bonusHP;
        if (necklace != null) total += necklace.bonusHP;
        return total;
    }

    public int GetTotalMaxMP()
    {
        int total = maxMP;
        if (weapon != null) total += weapon.bonusMP;
        if (armor != null) total += armor.bonusMP;
        if (ring != null) total += ring.bonusMP;
        if (necklace != null) total += necklace.bonusMP;
        return total;
    }

    public float GetTotalCritChance()
    {
        float total = critChance;
        if (weapon != null) total += weapon.bonusCritChance;
        if (armor != null) total += armor.bonusCritChance;
        if (ring != null) total += ring.bonusCritChance;
        if (necklace != null) total += necklace.bonusCritChance;
        return Mathf.Clamp01(total);
    }

    public float GetTotalCritDamage()
    {
        float total = critDamage;
        if (weapon != null) total += weapon.bonusCritDamage;
        if (armor != null) total += armor.bonusCritDamage;
        if (ring != null) total += ring.bonusCritDamage;
        if (necklace != null) total += necklace.bonusCritDamage;
        return total;
    }

    // ===== Trang bị / Tháo trang bị =====
    public void Equip(ItemData item)
    {
        if (item == null || item.itemType != ItemType.Equipment) return;

        switch (item.equipmentType)
        {
            case EquipmentType.Weapon: weapon = item; break;
            case EquipmentType.Armor: armor = item; break;
            case EquipmentType.Ring: ring = item; break;
            case EquipmentType.Necklace: necklace = item; break;
        }
    }

    public void Unequip(EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.Weapon: weapon = null; break;
            case EquipmentType.Armor: armor = null; break;
            case EquipmentType.Ring: ring = null; break;
            case EquipmentType.Necklace: necklace = null; break;
        }
    }
}
