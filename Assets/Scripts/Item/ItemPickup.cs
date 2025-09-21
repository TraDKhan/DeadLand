using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public int amount = 1;

    public void SetItem(ItemData data, int value)
    {
        itemData = data;
        amount = value;
        // Có thể update icon text tại đây
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddItem(itemData, amount);
            }

            Destroy(gameObject); // Nhặt xong biến mất
        }
    }
}
