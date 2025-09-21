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

    // 🟢 Hàm vứt vật phẩm
    public void DropItem(ItemData itemData, int amount, Vector3 dropPosition)
    {
        if (itemData == null)
        {
            Debug.LogError("ItemData null khi DropItem!");
            return;
        }

        InventoryItem inventoryItem = items.Find(i => i.itemData == itemData);
        if (inventoryItem == null)
        {
            Debug.LogWarning("Không có item này trong balo để vứt!");
            return;
        }

        if (inventoryItem.amount < amount)
        {
            Debug.LogWarning("Số lượng vứt nhiều hơn số lượng đang có!");
            return;
        }

        // Giảm số lượng
        inventoryItem.amount -= amount;
        if (inventoryItem.amount <= 0)
        {
            items.Remove(inventoryItem);
        }

        // Spawn prefab ngoài thế giới
        if (itemData.worldPrefab != null)
        {
            GameObject go = Instantiate(itemData.worldPrefab, dropPosition, Quaternion.identity);

            ItemPickup pickup = go.GetComponent<ItemPickup>();
            if (pickup != null)
            {
                pickup.SetItem(itemData, amount);
            }
        }
        else
        {
            Debug.LogWarning($"Item {itemData.itemName} chưa có prefab để spawn khi vứt!");
        }

        InventoryUI.Instance.RefreshUI();
    }
}
