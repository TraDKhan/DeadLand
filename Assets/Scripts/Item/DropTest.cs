using UnityEngine;
using System.Collections.Generic;

public class DropTest : MonoBehaviour
{
    public EnemyDropItem enemyDrop; // drag enemy prefab (có EnemyDropSimple) vào đây
    public int testRuns = 100;        // số lần mô phỏng giết enemy

    void Start()
    {
        RunSimulation();
    }

    void RunSimulation()
    {
        if (enemyDrop == null || enemyDrop.dropItems.Length == 0)
        {
            Debug.LogWarning("Chưa gán EnemyDropSimple hoặc chưa có dropItems!");
            return;
        }

        // Tạo dictionary lưu số lần rớt
        Dictionary<string, int> dropCounts = new Dictionary<string, int>();
        foreach (var item in enemyDrop.dropItems)
        {
            dropCounts[item.name] = 0;
        }

        // Giả lập nhiều lần giết enemy
        for (int i = 0; i < testRuns; i++)
        {
            foreach (var drop in enemyDrop.dropItems)
            {
                float roll = Random.value;
                if (roll <= drop.chance)
                {
                    dropCounts[drop.name]++;
                }
            }
        }

        // In kết quả
        Debug.Log("Kết quả mô phỏng " + testRuns + " lần giết enemy:");
        foreach (var kvp in dropCounts)
        {
            float percent = (kvp.Value / (float)testRuns) * 100f;
            Debug.Log($"{kvp.Key}: {kvp.Value}/{testRuns} lần ({percent:0.0}%)");
        }
    }
}
