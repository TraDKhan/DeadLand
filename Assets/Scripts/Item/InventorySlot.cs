using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI amountText;

    public Sprite goldIcon;
    public Sprite weaponIcon;
    public Sprite armorIcon;
    public Sprite hpPotionIcon;
    public Sprite mpPotionIcon;

    public void Setup(ItemType type, int amount)
    {
        iconImage.sprite = GetIcon(type);
        amountText.text = amount > 1 ? amount.ToString() : "";
    }

    private Sprite GetIcon(ItemType type)
    {
        switch (type)
        {
            case ItemType.Gold: return goldIcon;
            case ItemType.Weapon: return weaponIcon;
            case ItemType.Armor: return armorIcon;
            case ItemType.HpPotion: return hpPotionIcon;
            case ItemType.MpPotion: return mpPotionIcon;
        }
        return null;
    }
}
