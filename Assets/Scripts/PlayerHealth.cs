using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Image healthFillImage;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Player bị trúng đòn, còn lại: " + currentHealth);

        animator.SetTrigger("Hurt");

        UpdateHealthUI();

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
        }
    }

    void Die()
    {
        Debug.Log("Player chết!");

        if (animator != null)
            animator.SetTrigger("Die");

        this.enabled = false;

        Destroy(gameObject, 1.5f);
    }
}
