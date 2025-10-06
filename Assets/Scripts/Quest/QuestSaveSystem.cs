using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class QuestSaveData
{
    public List<string> questIDs = new();
    public List<int> progresses = new();
    public List<int> statuses = new();
}

public static class QuestSaveSystem
{
    private static string path => Application.persistentDataPath + "/quests.json";

    public static void Save(List<QuestData> quests, Dictionary<string, int> progress)
    {
        QuestSaveData data = new();
        foreach (var quest in quests)
        {
            data.questIDs.Add(quest.questID);
            data.progresses.Add(progress.ContainsKey(quest.questID) ? progress[quest.questID] : 0);
            data.statuses.Add((int)quest.status);
        }
        File.WriteAllText(path, JsonUtility.ToJson(data, true));
        Debug.Log($"💾 Lưu nhiệm vụ tại: {path}");
    }

    public static void Load(List<QuestData> quests, Dictionary<string, int> progress)
    {
        if (!File.Exists(path)) return;
        string json = File.ReadAllText(path);
        var data = JsonUtility.FromJson<QuestSaveData>(json);

        progress.Clear();
        for (int i = 0; i < data.questIDs.Count; i++)
        {
            var quest = quests.Find(q => q.questID == data.questIDs[i]);
            if (quest != null)
            {
                quest.status = (QuestStatus)data.statuses[i];
                progress[quest.questID] = data.progresses[i];
            }
        }
        Debug.Log("📂 Đã tải tiến độ nhiệm vụ");
    }
}
