[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int amount;

    public InventoryItem(ItemData data, int amt)
    {
        itemData = data;
        amount = amt;
    }
}
