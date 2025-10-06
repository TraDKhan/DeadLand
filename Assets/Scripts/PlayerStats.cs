using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int exp;
    public int gold;

    private void Awake()
    {
        Instance = this;
    }

    public void AddExp(int amount)
    {
        exp += amount;
        Debug.Log($"🔹 EXP: {exp}");
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log($"💰 Gold: {gold}");
    }
}
