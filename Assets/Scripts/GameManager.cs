using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player reference")]
    public GameObject playerPrefab;
    private GameObject playerInstance;

    [Header("Spawn Info")]
    public string spawnPointName;

    [Header("UI")]
    public GameObject hudCanvas;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        QuestManager.Instance?.LoadProgress();
        PlayerStatsManager.Instance?.LoadStats();
    }

    void OnApplicationQuit()
    {
        QuestManager.Instance?.SaveProgress();
        PlayerStatsManager.Instance?.SaveStats();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
        }

        if (!string.IsNullOrEmpty(spawnPointName))
        {
            GameObject spawn = GameObject.Find(spawnPointName);
            if (spawn != null)
                playerInstance.transform.position = spawn.transform.position;
        }
    }

    public void ChangeScene(string sceneName, string spawnName)
    {
        spawnPointName = spawnName;
        SceneManager.LoadScene(sceneName);
    }
}
