using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI amountText;
    private InventoryItem currentItem;

    public Button equipButton; // 🟢 gán trong Inspector

    public void Setup(InventoryItem item)
    {
        currentItem = item;
        iconImage.sprite = item.itemData.icon;
        amountText.text = item.amount > 1 ? item.amount.ToString() : "";

        // Chỉ hiện nút Equip nếu là trang bị
        equipButton.gameObject.SetActive(item.itemData.itemType == ItemType.Equipment);
    }

    public void OnEquipButtonClick()
    {
        if (currentItem == null || currentItem.itemData == null) return;

        if (currentItem.itemData.itemType == ItemType.Equipment)
        {
            // 🟢 gọi InventoryManager để equip
            InventoryManager.Instance.EquipFromUI(currentItem);
        }
    }
}

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class InventorySlot : MonoBehaviour
//{
//    public Image iconImage;
//    public TextMeshProUGUI amountText;

//    private ItemData currentItem;

//    public void Setup(ItemData data, int amount)
//    {
//        if (data == null)
//        {
//            iconImage.sprite = null;
//            amountText.text = "";
//            return;
//        }

//        currentItem = data;
//        iconImage.sprite = data.icon;
//        amountText.text = data.stackable && amount > 1 ? amount.ToString() : "";
//    }
//}
