using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool stackable = true; // cho phép cộng dồn (vd: bình máu, vàng)

    [Header("Prefab ngoài thế giới")]
    public GameObject worldPrefab; // prefab xuất hiện khi rơi/vứt
}
