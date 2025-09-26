//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;
//using static Inventory;

//public class InventoryUI : MonoBehaviour
//{
//    public static InventoryUI Instance;
//    public Transform slotParent;
//    public GameObject slotPrefab;

//    // 🔹 Item đang chọn
//    public InventoryItem selectedItem;

//    private void Awake()
//    {
//        Instance = this;
//        gameObject.SetActive(false);
//    }

//    public void RefreshUI()
//    {
//        foreach (Transform child in slotParent)
//            Destroy(child.gameObject);

//        foreach (InventoryItem item in InventoryManager.Instance.items)
//        {
//            GameObject slot = Instantiate(slotPrefab, slotParent);

//            // Lấy Icon + Amount
//            Image icon = slot.transform.Find("Icon").GetComponent<Image>();
//            TextMeshProUGUI amountText = slot.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
//            icon.sprite = item.itemData.icon;
//            amountText.text = item.amount > 1 ? item.amount.ToString() : "";

//            // Lấy các nút hành động
//            Button equipBtn = slot.transform.Find("EquipButton")?.GetComponent<Button>();
//            Button useBtn = slot.transform.Find("UseButton")?.GetComponent<Button>();

//            // Reset trạng thái
//            if (equipBtn != null) equipBtn.gameObject.SetActive(false);
//            if (useBtn != null) useBtn.gameObject.SetActive(false);

//            // Tùy loại item → hiển thị nút phù hợp
//            switch (item.itemData.itemType)
//            {
//                case ItemType.Equipment:
//                    if (equipBtn != null)
//                    {
//                        equipBtn.gameObject.SetActive(true);
//                        equipBtn.onClick.RemoveAllListeners();
//                        equipBtn.onClick.AddListener(() => {
//                            InventoryManager.Instance.EquipFromUI(item);
//                        });
//                    }
//                    break;

//                case ItemType.Potion:
//                    if (useBtn != null)
//                    {
//                        useBtn.gameObject.SetActive(true);
//                        useBtn.onClick.RemoveAllListeners();
//                        useBtn.onClick.AddListener(() => {
//                            InventoryManager.Instance.UsePotion(item);
//                        });
//                    }
//                    break;

//                case ItemType.Gold:
//                    // Vàng chỉ để hiển thị số lượng → không nút gì
//                    break;
//            }

//            // 🟢 Click chọn slot
//            Button slotBtn = slot.GetComponent<Button>();
//            if (slotBtn != null)
//            {
//                slotBtn.onClick.AddListener(() => SelectItem(item));
//            }
//        }
//    }


//    private void SelectItem(InventoryItem item)
//    {
//        selectedItem = item;
//        Debug.Log($"Đã chọn item: {item.itemData.itemName}");
//    }

//    public void OnClickEquip()
//    {
//        if (selectedItem == null) return;

//        Character character = PlayerStatsManager.Instance.GetRuntimeStats();
//        InventoryManager.Instance.EquipItem(selectedItem, character);
//    }

//    public void ToggleInventoryPanel()
//    {
//        gameObject.SetActive(!gameObject.activeSelf);
//        if (gameObject.activeSelf)
//        {
//            RefreshUI();
//        }
//    }
//}
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public Transform slotParent;
    public GameObject slotPrefab;

    // 🔹 Item đang chọn
    public InventoryItem selectedItem;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void RefreshUI()
    {
        // Xóa slot cũ
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        // Tạo slot mới
        foreach (InventoryItem item in InventoryManager.Instance.items)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);

            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null)
            {
                slot.Setup(item);
            }

            // 🟢 Khi click vào toàn bộ slot (background), chọn item
            UnityEngine.UI.Button btn = slotObj.GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => SelectItem(item));
            }
        }
    }

    private void SelectItem(InventoryItem item)
    {
        selectedItem = item;
        Debug.Log($"📦 Đã chọn item: {item.itemData.itemName}");
    }

    public void ToggleInventoryPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            RefreshUI();
        }
    }
}
