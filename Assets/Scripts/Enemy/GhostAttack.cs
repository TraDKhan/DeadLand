using UnityEngine;

public class GhostAttack : MonoBehaviour, IAttackBehavior
{
    [Header("Attack Settings")]
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public int damage = 10;
    public LayerMask playerLayer;

    private float cooldownTimer = 0f;

    void Update()
    {
        // giảm cooldown mỗi frame
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public void Attack(Transform target)
    {
        if (cooldownTimer > 0f) return; // chưa hết hồi chiêu

        // kiểm tra player trong phạm vi tấn công
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hit != null)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Debug.Log($"GhostAttack: Gây {damage} sát thương vào {target.name}");
        }

        cooldownTimer = attackCooldown; // reset lại cooldown
    }

    // Vẽ gizmos để debug vùng attack
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
