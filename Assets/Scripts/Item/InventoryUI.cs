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
