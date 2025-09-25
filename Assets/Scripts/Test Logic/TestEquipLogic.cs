using UnityEngine;

public class TestEquipLogic : MonoBehaviour
{
    public PlayerController player;

    void Start()
    {
        // Tạo dữ liệu giả cho trang bị
        EquipmentData fakeWeapon = ScriptableObject.CreateInstance<EquipmentData>();
        fakeWeapon.itemName = "Kiếm Lửa";
        fakeWeapon.equipmentType = EquipmentType.Weapon;
        fakeWeapon.bonusDamage = 10;
        fakeWeapon.bonusCritChance = 0.2f;

        EquipmentData fakeArmor = ScriptableObject.CreateInstance<EquipmentData>();
        fakeArmor.itemName = "Giáp Rồng";
        fakeArmor.equipmentType = EquipmentType.Armor;
        fakeArmor.bonusDefense = 15;
        fakeArmor.bonusHP = 50;

        // Gọi hàm trang bị
        player.EquipItem(fakeWeapon);
        player.EquipItem(fakeArmor);
    }
}