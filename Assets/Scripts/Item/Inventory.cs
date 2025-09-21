using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int gold = 0;
    public int hpPotion = 0;
    public int mpPotion = 0;

    // Giả sử bạn có 1 danh sách vũ khí / áo giáp
    public string currentWeapon = null;
    public string currentArmor = null;

    public void AddItem(ItemType type, int amount)
    {
        switch (type)
        {
            case ItemType.Gold:
                gold += amount;
                Debug.Log("Player nhận " + amount + " vàng. Tổng: " + gold);
                break;

            case ItemType.HpPotion:
                hpPotion += amount;
                Debug.Log("Player nhận " + amount + " bình HP. Tổng: " + hpPotion);
                break;

            case ItemType.MpPotion:
                mpPotion += amount;
                Debug.Log("Player nhận " + amount + " bình MP. Tổng: " + mpPotion);
                break;

            case ItemType.Weapon:
                currentWeapon = "Vũ khí mới"; // hoặc bạn có thể lưu ID vũ khí
                Debug.Log("Player nhặt vũ khí mới!");
                break;

            case ItemType.Armor:
                currentArmor = "Áo giáp mới"; // hoặc bạn có thể lưu ID áo giáp
                Debug.Log("Player nhặt áo giáp mới!");
                break;
        }
    }
}
