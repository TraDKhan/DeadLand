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
            float roll = Random.value; // random 0..1
            if (roll <= drop.chance)
            {
                int amount = Random.Range(drop.minAmount, drop.maxAmount + 1);

                // Spawn item
                Vector3 pos = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
                GameObject go = Instantiate(drop.prefab, pos, Quaternion.identity);

                // Gán dữ liệu item cho prefab
                ItemPickup pickup = go.GetComponent<ItemPickup>();
                if (pickup != null)
                {
                    pickup.SetItem(drop.itemType, amount);
                }
            }
        }
    }
}

