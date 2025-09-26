using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemData itemData;
        public int amount;

        public InventorySlot(ItemData data, int amt)
        {
            itemData = data;
            amount = amt;
        }
    }

    public int maxSlots = 20;
    public List<InventorySlot> items = new List<InventorySlot>();

    public void AddItem(ItemData data, int amount)
    {
        if (data == null) return;

        // Nếu item có thể cộng dồn → cộng vào slot có sẵn
        InventorySlot existing = items.Find(i => i.itemData == data);
        if (existing != null && data.stackable)
        {
            existing.amount += amount;
        }
        else
        {
            if (items.Count < maxSlots)
            {
                items.Add(new InventorySlot(data, amount));
            }
            else
            {
                Debug.Log("⚠️ Balo đã đầy!");
            }
        }

        // Cập nhật UI nếu có
        if (InventoryUI.Instance != null)
            InventoryUI.Instance.RefreshUI();
    }

    public void RemoveItem(ItemData data, int amount)
    {
        InventorySlot existing = items.Find(i => i.itemData == data);
        if (existing != null)
        {
            existing.amount -= amount;
            if (existing.amount <= 0)
                items.Remove(existing);

            if (InventoryUI.Instance != null)
                InventoryUI.Instance.RefreshUI();
        }
    }

    //public void DropItem(ItemData data, int amount, Vector3 dropPosition)
    //{
    //    RemoveItem(data, amount);

    //    if (data != null && data.worldPrefab != null)
    //    {
    //        for (int i = 0; i < amount; i++)
    //        {
    //            Instantiate(data.worldPrefab, dropPosition, Quaternion.identity);
    //        }
    //    }
    //}
}
