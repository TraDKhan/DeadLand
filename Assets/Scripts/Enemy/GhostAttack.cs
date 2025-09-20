using UnityEngine;

public class GhostAttack : MonoBehaviour, IAttackBehavior
{
    [Header("Attack Settings")]
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public int damage = 10;
    public LayerMask playerLayer;

    private float cooldownTimer = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public void Attack(Transform target)
    {
        if (cooldownTimer > 0f) return;

        // bật trigger Attack để chạy animation
        animator?.SetTrigger("Attack");

        cooldownTimer = attackCooldown;
    }

    // Gọi trong event Animation Ghost
    public void OnAttackHit()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hit != null)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Debug.Log($"👻 GhostAttack (Anim Event): Gây {damage} sát thương vào {hit.name}");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
