using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI amountText;

    private ItemData currentItem;

    public void Setup(ItemData data, int amount)
    {
        if (data == null)
        {
            iconImage.sprite = null;
            amountText.text = "";
            return;
        }

        currentItem = data;
        iconImage.sprite = data.icon;
        amountText.text = data.stackable && amount > 1 ? amount.ToString() : "";
    }
}
