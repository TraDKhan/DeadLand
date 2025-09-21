using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropAmountUI : MonoBehaviour
{
    public static DropAmountUI Instance;

    public TMP_InputField inputField;
    public Button confirmButton;
    public Button cancelButton;

    private InventoryItem currentItem;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);

        confirmButton.onClick.AddListener(OnConfirm);
        cancelButton.onClick.AddListener(OnCancel);
    }

    public void Show(InventoryItem item)
    {
        currentItem = item;
        gameObject.SetActive(true);
        inputField.text = "1";
    }

    private void OnConfirm()
    {
        if (currentItem == null)
        {
            gameObject.SetActive(false);
            return;
        }

        int amount;
        if (!int.TryParse(inputField.text, out amount))
        {
            Debug.LogWarning("Số lượng không hợp lệ!");
            return;
        }

        amount = Mathf.Clamp(amount, 1, currentItem.amount);

        Vector3 dropPos = InventoryManager.Instance.transform.position + Vector3.right * 1f;
        InventoryManager.Instance.DropItem(currentItem.itemData, amount, dropPos);

        Debug.Log($"Đã vứt {amount} {currentItem.itemData.itemName}");

        if (currentItem.amount <= 0)
            InventoryUI.Instance.selectedItem = null;

        gameObject.SetActive(false);
    }

    private void OnCancel()
    {
        gameObject.SetActive(false);
    }
}
