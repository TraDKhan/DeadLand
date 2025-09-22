using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public int amount = 1;
    [Header("Tự hủy sau X giây (0 = không tự hủy)")]
    public float lifeTime = 20f;

    public void SetItem(ItemData data, int value)
    {
        itemData = data;
        amount = value;

        // Nếu có thời gian tự hủy
        if (lifeTime > 0)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (itemData == null)
        {
            Debug.LogError("ItemPickup chưa có ItemData!");
            return;
        }

        if (collision.CompareTag("Player"))
        {
            if (InventoryManager.Instance != null)
            {
                PopupTextManager.Instance.ShowText($"+ {amount} {itemData.itemName}",transform.position, Color.blue, 1f);
                InventoryManager.Instance.AddItem(itemData, amount);
            }

            Destroy(gameObject); // Nhặt xong biến mất
        }
    }
}
