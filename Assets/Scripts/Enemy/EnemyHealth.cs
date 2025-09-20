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

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // gọi ở đây để đúng
        Debug.Log("HP E " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        Debug.Log("Sat thương " + damage);

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
            Debug.Log($"HP E còn {currentHealth} / {maxHealth}");
        }
    }
    void Die()
    {
        Debug.Log(gameObject.name + " chết");
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
