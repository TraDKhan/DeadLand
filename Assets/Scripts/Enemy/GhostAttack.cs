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
        if (cooldownTimer > 0f) return; // chưa hết hồi chiêu

        // bật trigger Attack để chạy animation
        animator?.SetTrigger("Attack");

        // Damage có thể gọi trực tiếp tại đây
        // hoặc tốt hơn: gọi từ Animation Event (OnAttackHit) để khớp frame
        DoDamage(target);

        cooldownTimer = attackCooldown;
    }

    private void DoDamage(Transform target)
    {
        // kiểm tra player trong phạm vi attack
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hit != null)
        {
            animator.SetTrigger("Attack");
            hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Debug.Log($"👻 GhostAttack: Gây {damage} sát thương vào {target.name}");
        }
    }

    // Hàm này bạn có thể gọi bằng Animation Event ngay frame ra đòn
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
