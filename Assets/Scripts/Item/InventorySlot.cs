using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI amountText;

    public void Setup(InventoryItem item)
    {
        iconImage.sprite = item.itemData.icon;
        amountText.text = item.amount > 1 ? item.amount.ToString() : "";
    }
}
