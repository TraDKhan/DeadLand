using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player reference")]
    public GameObject playerPrefab;
    private GameObject playerInstance;

    [Header("Spawn Info")]
    public string spawnPointName; // tên spawn point để spawn

    private void Awake()
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
        }

        // Tìm spawn point trong scene mới
        if (!string.IsNullOrEmpty(spawnPointName))
        {
            GameObject spawn = GameObject.Find(spawnPointName);
            if (spawn != null)
            {
                playerInstance.transform.position = spawn.transform.position;
            }
            else
            {
                Debug.LogWarning("⚠️ Không tìm thấy spawn point: " + spawnPointName);
            }
        }
    }

    public void ChangeScene(string sceneName, string spawnName)
    {
        spawnPointName = spawnName;
        SceneManager.LoadScene(sceneName);
    }
}
