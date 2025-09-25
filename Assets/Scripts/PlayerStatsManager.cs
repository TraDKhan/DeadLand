using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    [Header("Template dữ liệu (SO)")]
    public CharacterStatsData playerStatsTemplate;

    private Character runtimeStats;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (playerStatsTemplate != null)
        {
            runtimeStats = new Character(playerStatsTemplate);
            runtimeStats.currentHP = runtimeStats.maxHP;
            runtimeStats.currentMP = runtimeStats.maxMP;
            Debug.Log("✅ PlayerStatsManager đã khởi tạo runtimeStats từ SO");
        }
        else
        {
            Debug.LogError("⚠ PlayerStatsTemplate chưa được gán trong Inspector!");
        }
    }

    public Character GetRuntimeStats()
    {
        return runtimeStats;
    }
}
