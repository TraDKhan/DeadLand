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

    // ===========================

    // 🟢 Trang bị item từ inventory
    // 🟢 Trang bị item từ inventory
    public void EquipItem(InventoryItem item, Character character)
    {
        if (item == null || item.itemData == null) return;

        if (item.itemData.itemType == ItemType.Equipment)
        {
            character.Equip(item.itemData);
            Debug.Log($"EquipItem Trang bị {item.itemData.itemName} ({item.itemData.equipmentType})");

            CharacterStatsUI.Instance?.UpdateUI();
            InventoryUI.Instance?.RefreshUI();
            EquipmentUI.Instance?.UpdateEquipmentUI(character); // 🟢 Cập nhật UI slot
        }
        else
        {
            Debug.LogWarning($"❌ {item.itemData.itemName} không phải là trang bị!");
        }
    }

    // 🟢 Tháo trang bị (và trả lại vào Inventory nếu cần)
    public void UnequipItem(EquipmentType type, Character character)
    {
        if (character == null)
        {
            Debug.LogWarning("❌ Chưa có Character để tháo trang bị!");
            return;
        }

        ItemData unequipped = null;

        switch (type)
        {
            case EquipmentType.Weapon:
                unequipped = character.weapon;
                character.Unequip(EquipmentType.Weapon);
                break;
            case EquipmentType.Armor:
                unequipped = character.armor;
                character.Unequip(EquipmentType.Armor);
                break;
            case EquipmentType.Ring:
                unequipped = character.ring;
                character.Unequip(EquipmentType.Ring);
                break;
            case EquipmentType.Necklace:
                unequipped = character.necklace;
                character.Unequip(EquipmentType.Necklace);
                break;
        }

        if (unequipped != null)
        {
            AddItem(unequipped, 1); // trả lại vào balo
            Debug.Log($"❌ Đã tháo trang bị {unequipped.itemName} ({type})");
        }

        CharacterStatsUI.Instance?.UpdateUI();
        InventoryUI.Instance?.RefreshUI();
    }
    public void EquipFromUI(InventoryItem item)
    {
        if (item == null || item.itemData == null)
        {
            Debug.LogWarning("❌ Không có item để trang bị!");
            return;
        }

        if (item.itemData.itemType == ItemType.Equipment)
        {
            // Giả sử bạn có tham chiếu tới nhân vật
            Character character = PlayerStatsManager.Instance.GetRuntimeStats();

            if (character == null)
            {
                Debug.LogWarning("❌ Chưa có nhân vật để trang bị!");
                return;
            }

            // Bỏ 1 món ra khỏi inventory
            if (item.amount > 1)
            {
                item.amount -= 1;
            }
            else
            {
                items.Remove(item);
            }

            // Trang bị
            character.Equip(item.itemData);
            Debug.Log($"EquipFromUI Trang bị {item.itemData.itemName} ({item.itemData.equipmentType})");

            CharacterStatsUI.Instance?.UpdateUI();
            InventoryUI.Instance?.RefreshUI();
            EquipmentUI.Instance?.UpdateEquipmentUI(character);
        }
        else
        {
            Debug.LogWarning($"❌ {item.itemData.itemName} không phải là trang bị!");
        }
    }
    // 🟢 Dùng potion (tạm thời chỉ log)
    public void UsePotion(InventoryItem item)
    {
        if (item == null || item.itemData == null)
        {
            Debug.LogWarning("❌ Không có item để dùng!");
            return;
        }

        if (item.itemData.itemType == ItemType.Potion)
        {
            // Giảm số lượng
            if (item.amount > 1)
            {
                item.amount -= 1;
            }
            else
            {
                items.Remove(item);
            }

            // Log
            Debug.Log($"🍷 Đã dùng {item.itemData.itemName} (còn lại {item.amount})");

            // Cập nhật UI
            InventoryUI.Instance?.RefreshUI();
        }
        else
        {
            Debug.LogWarning($"❌ {item.itemData.itemName} không phải là potion để dùng!");
        }
    }

}
