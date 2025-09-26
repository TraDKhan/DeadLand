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

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                DropAllSelected();
            }
            else
            {
                DropOneSelected();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DropCustomAmount();
        }
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
    //============= XU LY ITEM ===========//
    private void DropOneSelected()
    {
        if (InventoryUI.Instance.selectedItem == null)
        {
            Debug.Log("Chưa chọn item nào để vứt!");
            return;
        }

        InventoryItem selected = InventoryUI.Instance.selectedItem;
        Vector3 dropPos = transform.position + transform.right * 1f;

        DropItem(selected.itemData, 1, dropPos);
        Debug.Log($"Đã vứt 1 {selected.itemData.itemName}");

        if (selected.amount <= 0)
            InventoryUI.Instance.selectedItem = null;
    }

    private void DropAllSelected()
    {
        if (InventoryUI.Instance.selectedItem == null)
        {
            Debug.Log("Chưa chọn item nào để vứt!");
            return;
        }

        InventoryItem selected = InventoryUI.Instance.selectedItem;
        int amount = selected.amount;
        Vector3 dropPos = transform.position + transform.right * 1f;

        DropItem(selected.itemData, amount, dropPos);
        Debug.Log($"Đã vứt toàn bộ {amount} {selected.itemData.itemName}");

        InventoryUI.Instance.selectedItem = null;
    }

    private void DropCustomAmount()
    {
        if (InventoryUI.Instance.selectedItem == null)
        {
            Debug.Log("Chưa chọn item nào để vứt!");
            return;
        }

        // 🟢 Gọi UI popup nhập số lượng
        DropAmountUI.Instance.Show(InventoryUI.Instance.selectedItem);
    }
}
