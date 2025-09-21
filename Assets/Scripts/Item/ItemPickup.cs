using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemType itemType;
    public int amount = 1; // số lượng (VD: vàng = 10, bình máu = 1)

    public void SetItem(ItemType type, int value)
    {
        itemType = type;
        amount = value;
        // có thể update icon/text prefab tại đây
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory inv = collision.GetComponent<Inventory>();
            if (inv != null)
            {
                inv.AddItem(itemType, amount);
            }

            Destroy(gameObject); // nhặt xong thì biến mất
        }
    }
}
