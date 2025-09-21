using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    public Image healthFillImage;

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
        Debug.Log("HP Enemy " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Sát thương " + damage);

        UpdateHealthUI();

        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = (float)currentHealth / maxHealth;
            Debug.Log($"HP Enemy còn {currentHealth} / {maxHealth}");
        }
    }

    public void Die()
    {
        isDead = true;

        EnemyController ec = GetComponent<EnemyController>();
        if (animator != null)
            animator.SetTrigger("Die");

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
