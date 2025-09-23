using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats (ScriptableObject)")]
    public CharacterStatsData playerStats; 

    public Image healthFillImage;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Đồng bộ máu ban đầu từ stats
        if (playerStats != null)
        {
            playerStats.currentHP = playerStats.maxHP;
            UpdateHealthUI();
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerStats == null) return;

        // Tính toán damage dựa trên phòng thủ
        int finalDamage = Mathf.Max(0, damage - playerStats.defense);
        playerStats.currentHP -= finalDamage;
        playerStats.currentHP = Mathf.Clamp(playerStats.currentHP, 0, playerStats.maxHP);

        // Popup sát thương
        PopupTextManager.Instance.ShowDamage(finalDamage, transform.position + Vector3.up * 0.5f);

        UpdateHealthUI();

        if (playerStats.currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthFillImage != null && playerStats != null)
        {
            healthFillImage.fillAmount = (float)playerStats.currentHP / playerStats.maxHP;
        }
    }

    void Die()
    {
        Debug.Log($"{playerStats.characterName} đã chết!");

        if (animator != null)
            animator.SetTrigger("Die");

        AudioManager.Instance.PlayPlayerDeath();

        this.enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
