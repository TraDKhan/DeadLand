using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;

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
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        foreach (InventoryItem item in InventoryManager.Instance.items)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);

            Transform iconObj = slot.transform.Find("Icon");
            Transform amountObj = slot.transform.Find("Amount");

            UnityEngine.UI.Image icon = iconObj.GetComponent<UnityEngine.UI.Image>();
            TMPro.TextMeshProUGUI amountText = amountObj.GetComponent<TMPro.TextMeshProUGUI>();

            icon.sprite = item.itemData.icon;
            amountText.text = item.amount.ToString();

            // 🟢 Khi click vào slot → chọn item này
            UnityEngine.UI.Button btn = slot.GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() => SelectItem(item));
            }
        }
    }

    private void SelectItem(InventoryItem item)
    {
        selectedItem = item;
        Debug.Log($"Đã chọn item: {item.itemData.itemName}");
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
