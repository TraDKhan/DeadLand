using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public int maxSlots = 20;
    public List<InventoryItem> items = new List<InventoryItem>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(ItemData newItem, int amount = 1)
    {
        if (newItem == null)
        {
            Debug.LogError("ItemData null khi AddItem!");
            return;
        }

        if (newItem.stackable)
        {
            InventoryItem existing = items.Find(i => i.itemData == newItem);
            if (existing != null)
            {
                existing.amount += amount;
                InventoryUI.Instance.RefreshUI();
                return;
            }
        }

        if (items.Count < maxSlots)
        {
            items.Add(new InventoryItem(newItem, amount));
            Debug.Log($"Đã thêm item {newItem.itemName} số lượng {amount}");
            InventoryUI.Instance.RefreshUI();
        }
        else
        {
            Debug.Log("Balo đã đầy!");
        }
    }
}
