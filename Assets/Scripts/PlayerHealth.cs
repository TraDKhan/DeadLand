using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats Template (SO)")]
    public CharacterStatsData playerStats; // SO template

    private Character runtimeStats; // runtime data

    public Image healthFillImage;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        if (playerStats != null)
        {
            // Tạo runtime từ SO
            runtimeStats = new Character(playerStats);
            Debug.Log("Đã khơi tạo dữ liệu SO");

            // Đồng bộ máu ban đầu
            runtimeStats.currentHP = runtimeStats.maxHP;
            UpdateHealthUI();
        }
        else
        {
            Debug.LogError("⚠ PlayerStats chưa được gán trong Inspector!");
        }
    }
    public void TakeDamage(int damage)
    {
        if (runtimeStats == null) return;

        // Tính toán damage dựa trên phòng thủ
        int finalDamage = Mathf.Max(0, damage - runtimeStats.GetTotalDefense());
        runtimeStats.currentHP -= finalDamage;
        runtimeStats.currentHP = Mathf.Clamp(runtimeStats.currentHP, 0, runtimeStats.GetTotalMaxHP());

        // Popup sát thương
        PopupTextManager.Instance.ShowDamage(finalDamage, transform.position + Vector3.up * 0.5f);

        UpdateHealthUI();

        if (runtimeStats.currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthFillImage != null && runtimeStats != null)
        {
            healthFillImage.fillAmount = (float)runtimeStats.currentHP / runtimeStats.GetTotalMaxHP();
        }
    }

    void Die()
    {
        Debug.Log($"{runtimeStats.characterName} đã chết!");

        if (animator != null)
            animator.SetTrigger("Die");

        AudioManager.Instance.PlayPlayerDeath();

        this.enabled = false;
        Destroy(gameObject, 1.5f);
    }

    // 🟢 Cho phép PlayerController hoặc UI lấy runtime stats để đồng bộ
    public Character GetRuntimeStats()
    {
        return runtimeStats;
    }
    public void EquipItem(EquipmentData item)
    {
        if (runtimeStats == null) return;
        runtimeStats.Equip(item);
        Debug.Log($"🔧 Đã trang bị {item.itemName} ({item.equipmentType})");

        CharacterStatsUI ui = GameObject.FindObjectOfType<CharacterStatsUI>();
        if (ui != null) ui.UpdateUI();
    }
}
