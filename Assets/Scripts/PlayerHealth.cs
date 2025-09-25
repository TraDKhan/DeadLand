using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats Template (SO)")]
    private Character runtimeStats; // runtime data

    public Image healthFillImage;
    private Animator animator;

    private void Start()
    {
        
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        runtimeStats = PlayerStatsManager.Instance.GetRuntimeStats();
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
}
