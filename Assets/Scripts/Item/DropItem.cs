using UnityEngine;

[System.Serializable]
public class DropItem
{
    public string name;                // tên hiển thị (vàng, áo giáp, vũ khí…)
    public GameObject prefab;          // prefab spawn ra
    public ItemType itemType;          // loại item
    [Range(0f, 1f)] public float chance = 0.5f; // tỉ lệ rớt (0.3 = 30%)
    public int minAmount = 1;
    public int maxAmount = 1;
}
