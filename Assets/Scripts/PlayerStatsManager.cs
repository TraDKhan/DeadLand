using UnityEngine;
using System.IO;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    [Header("Template dữ liệu (SO)")]
    public CharacterStatsData playerStatsTemplate;

    private Character runtimeStats;
    private string savePath;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        savePath = Path.Combine(Application.persistentDataPath, "playerStats.json");

        if (File.Exists(savePath))
            LoadStats();
        else
            CreateNewRuntimeStats();
    }

    private void CreateNewRuntimeStats()
    {
        if (playerStatsTemplate != null)
        {
            runtimeStats = new Character(playerStatsTemplate);
            runtimeStats.currentHP = runtimeStats.maxHP;
            runtimeStats.currentMP = runtimeStats.maxMP;
        }
    }

    public Character GetRuntimeStats() => runtimeStats;

    public void SaveStats()
    {
        if (runtimeStats == null) return;

        string json = JsonUtility.ToJson(runtimeStats, true);
        File.WriteAllText(savePath, json);
        Debug.Log("💾 Đã lưu stat nhân vật!" + savePath);
    }

    public void LoadStats()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("⚠ Không có file save, tạo mới!");
            CreateNewRuntimeStats();
            return;
        }

        string json = File.ReadAllText(savePath);
        runtimeStats = JsonUtility.FromJson<Character>(json);
        Debug.Log("📂 Đã tải stat nhân vật!");
    }

    void OnApplicationQuit()
    {
        SaveStats();
    }

    public void ResetStats()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("🗑️ Đã xóa file lưu stats: " + savePath);
        }
        else
        {
            Debug.LogWarning("⚠ Không tìm thấy file để xóa!");
        }

        CreateNewRuntimeStats();
        Debug.Log("🔄 Đã reset lại trạng thái ban đầu của nhân vật.");
    }
}
