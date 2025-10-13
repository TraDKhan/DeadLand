using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public Transform slotParent;
    public GameObject slotPrefab;

    [Header("Panel phụ")]
    public GameObject itemDetailPanel;
    public Image imageItem;
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI descriptionItem;
    public Button useItemButton;
    public TextMeshProUGUI equipText;
    public Button removeItemButton;


    // 🔹 Item đang chọn
    public InventoryItem selectedItem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Hủy bản trùng lặp
            return;
        }

        Instance = this;
        gameObject.SetActive(false);
    }


    private void Start()
    {
        itemDetailPanel.SetActive(false);
    }
    public void RefreshUI()
    {
        // Xóa slot cũ
        //foreach (Transform child in slotParent)
        //    Destroy(child.gameObject);
        for (int i = slotParent.childCount - 1; i >= 0; i--)
        {
            Destroy(slotParent.GetChild(i).gameObject);
        }

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
            Button btn = slotObj.GetComponent<Button>();
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
        itemDetailPanel.SetActive(true);

        imageItem.sprite = item.itemData.icon;
        nameItem.text = item.itemData.itemName;
        if (item.itemData.itemType == ItemType.Equipment)
        {
            equipText.text = "Trang bị";
        }
        if (item.itemData.itemType == ItemType.Potion)
            equipText.text = "Sử dụng";
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

    public void OnUseItemButtonClick()
    {
        if (selectedItem == null || selectedItem.itemData == null) return;

        if (selectedItem.itemData.itemType == ItemType.Equipment)
        {
            // 🟢 gọi InventoryManager để equip
            InventoryManager.Instance.EquipFromUI(selectedItem);
        }
        if (selectedItem.itemData.itemType == ItemType.Potion)
        {
            // 🟢 gọi InventoryManager để use
            InventoryManager.Instance.UsePotion(selectedItem);
        }
    }
}
