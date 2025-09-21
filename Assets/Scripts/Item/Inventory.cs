using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemType itemType;
        public int amount;

        public InventorySlot(ItemType type, int amt)
        {
            itemType = type;
            amount = amt;
        }
    }

    public int maxSlots = 20;
    public List<InventorySlot> items = new List<InventorySlot>();

    public void AddItem(ItemType type, int amount)
    {
        // Nếu item có thể cộng dồn (Gold, Potion) → cộng vào slot có sẵn
        InventorySlot existing = items.Find(i => i.itemType == type);
        if (existing != null && CanStack(type))
        {
            existing.amount += amount;
        }
        else
        {
            if (items.Count < maxSlots)
            {
                items.Add(new InventorySlot(type, amount));
            }
            else
            {
                Debug.Log("Balo đã đầy!");
            }
        }

        // Cập nhật UI nếu có
        if (InventoryUI.Instance != null)
            InventoryUI.Instance.RefreshUI();
    }

    private bool CanStack(ItemType type)
    {
        return type == ItemType.Gold || type == ItemType.HpPotion || type == ItemType.MpPotion;
    }
}
