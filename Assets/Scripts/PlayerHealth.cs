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
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        animator.SetTrigger("Hurt");

        //gọi popup text để hiển thị sát thương nhân vào
        PopupTextManager.Instance.ShowDamage(damage, transform.position + Vector3.up * 0.5f);

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
