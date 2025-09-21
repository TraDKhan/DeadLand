using UnityEngine;

[System.Serializable]
public class DropItem
{
    public ItemData itemData;
    [Range(0f, 1f)] public float chance = 0.5f;
    public int minAmount = 1;
    public int maxAmount = 1;
}
