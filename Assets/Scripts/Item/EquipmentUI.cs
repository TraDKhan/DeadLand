using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI Instance;

    [Header("Equipment Slots UI")]
    public Image weaponSlot;
    public Image armorSlot;
    public Image ringSlot;
    public Image necklaceSlot;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateEquipmentUI(Character character)
    {
        if (character == null) return;

        // Weapon
        if (character.weapon != null)
        {
            weaponSlot.sprite = character.weapon.icon;
            weaponSlot.enabled = true;
        }
        else weaponSlot.enabled = false;

        // Armor
        if (character.armor != null)
        {
            armorSlot.sprite = character.armor.icon;
            armorSlot.enabled = true;
        }
        else armorSlot.enabled = false;

        // Ring
        if (character.ring != null)
        {
            ringSlot.sprite = character.ring.icon;
            ringSlot.enabled = true;
        }
        else ringSlot.enabled = false;

        // Necklace
        if (character.necklace != null)
        {
            necklaceSlot.sprite = character.necklace.icon;
            necklaceSlot.enabled = true;
        }
        else necklaceSlot.enabled = false;
    }
}
