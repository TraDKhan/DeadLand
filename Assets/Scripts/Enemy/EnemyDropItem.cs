using UnityEngine;

public class EnemyDropItem : MonoBehaviour 
{
    [Header("Danh sách vật phẩm có thể rớt")]
    public DropItem[] dropItems;

    [Header("Bán kính spawn quanh enemy")]
    public float spawnRadius = 0.5f;

    // Gọi hàm này khi enemy chết
    public void DropLoot()
    {
        foreach (DropItem drop in dropItems)
        {
            float roll = Random.value;

            if (roll <= drop.chance)
            {
                int amount = Random.Range(drop.minAmount, drop.maxAmount + 1);

                if (drop.itemData != null && drop.itemData.worldPrefab != null)
                {
                    Vector3 pos = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
                    GameObject go = Instantiate(drop.itemData.worldPrefab, pos, Quaternion.identity);

                    ItemPickup pickup = go.GetComponent<ItemPickup>();
                    if (pickup != null)
                    {
                        pickup.SetItem(drop.itemData, amount);
                    }
                }
                else
                {
                    Debug.LogError("ItemData hoặc worldPrefab bị null!");
                }
            }
        }
    }


}

