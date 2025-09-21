using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;

    [Header("Inventory Buttons")]
    public Button toggleInventoryButton;

    [Header("Game Control Buttons")]
    public Button startGameButton;
    public Button quitGameButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Gán sự kiện cho các nút
        if (toggleInventoryButton != null)
            toggleInventoryButton.onClick.AddListener(() => InventoryUI.Instance.ToggleInventory());

        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);

        if (quitGameButton != null)
            quitGameButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        Debug.Log("Bắt đầu game!");
        // Thêm logic chuyển scene hoặc khởi tạo gameplay ở đây
    }

    private void QuitGame()
    {
        Debug.Log("Thoát game!");
        Application.Quit();
    }
}