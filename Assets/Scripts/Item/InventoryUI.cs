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

            Image icon = iconObj.GetComponent<Image>();
            TextMeshProUGUI amountText = amountObj.GetComponent<TextMeshProUGUI>();

            if (item.itemData == null)
            {
                Debug.LogError("itemData bị null trong InventoryItem!");
                continue;
            }
            if (item.itemData.icon == null)
            {
                Debug.LogError($"Item {item.itemData.itemName} chưa có icon!");
                continue;
            }

            icon.sprite = item.itemData.icon;
            amountText.text = item.amount.ToString();
        }

    }
    public void ToggleInventory()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            RefreshUI();
        }
    }
}
