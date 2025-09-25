using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    public Image healthFillImage;

    [Header("Audio Death")]
    public AudioClip enemyDeathClip;

    [Header("EXP")]
    public int expReward = 50;
    public PlayerHealth playerHealth;

    [HideInInspector] public bool isDead = false;

    private Animator animator;
    private EnemyDropItem dropSystem;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        dropSystem = GetComponent<EnemyDropItem>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        UpdateHealthUI();

        

        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            AudioManager.Instance.PlayEnemyDeath(enemyDeathClip);
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    public void Die()
    {
        isDead = true;

        EnemyController ec = GetComponent<EnemyController>();
        if (animator != null)
            animator.SetTrigger("Die");

        if (playerHealth != null)
        {
            playerHealth.GetRuntimeStats().GainExp(expReward);
            PopupTextManager.Instance.ShowEXP(expReward, transform.position);
            Debug.Log($"<color=yellow>Player nhận {expReward} EXP</color>");
        }

        if (ec != null)
            ec.enabled = false;

        // Gọi hệ thống rớt vật phẩm
        if (dropSystem != null)
            dropSystem.DropLoot();
    }

    // gọi từ event cuối animation Die
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
