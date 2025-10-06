using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;

    [Header("Inventory Buttons")]
    public Button toggleInventoryButton;
    public GameObject inventoryPanel;

    [Header("Infor Player Buttons")]
    public Button toggleInforPlayer;
    public GameObject inforPlayerPanel;

    [Header("Quest Buttons")]
    public Button toggleQuestButton;
    public GameObject questPanel;

    [Header("Game Control Buttons")]
    public Button startGameButton;
    public Button quitGameButton;

    [Header("Game Play Buttons")]
    public GameObject pausePanel;
    public Button openPausePanelButton;
    public Button closePausePanelButton;

    public GameObject audioSettingPanel;
    public Button openAudioSettingPanel;
    public Button closeAudioSettingPanel;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        SetActive();

        ActiveButton();
    }

    private void ActiveButton()
    {
        // Gán sự kiện cho các nút
        if (toggleInventoryButton != null)
            toggleInventoryButton.onClick.AddListener(() => InventoryUI.Instance.ToggleInventoryPanel());

        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);

        if (quitGameButton != null)
            quitGameButton.onClick.AddListener(QuitGame);

        if (openPausePanelButton != null)
            openPausePanelButton.onClick.AddListener(OpenPausePanel);

        if (closePausePanelButton != null)
            closePausePanelButton.onClick.AddListener(() => pausePanel.SetActive(false));

        //Audio Panel
        if (openAudioSettingPanel != null)
            openAudioSettingPanel.onClick.AddListener(() => audioSettingPanel.SetActive(true));

        if (closeAudioSettingPanel != null)
            closeAudioSettingPanel.onClick.AddListener(() => audioSettingPanel.SetActive(false));

        //Infor Player Panel
        if (toggleInforPlayer != null)
            toggleInforPlayer.onClick.AddListener(() => inforPlayerPanel.SetActive(!inforPlayerPanel.activeSelf));
        
        //Quest Panel
        if (toggleQuestButton != null)
            toggleQuestButton.onClick.AddListener(() => questPanel.SetActive(!questPanel.activeSelf));
    }
    private void SetActive()
    {
        pausePanel.SetActive(false);
        audioSettingPanel.SetActive(false);
        inforPlayerPanel.SetActive(false);
        questPanel.SetActive(false);
    }
    private void OpenPausePanel()
    {
        pausePanel.SetActive(true);
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