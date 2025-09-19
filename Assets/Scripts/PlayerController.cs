using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackCooldown = 0.5f;   // thời gian chờ giữa các đòn
    public Transform attackPoint;         // vị trí trung tâm đòn đánh
    public float attackRange = 0.5f;      // bán kính đòn đánh
    public LayerMask enemyLayer;          // layer địch

    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAttack();
    }

    void HandleAttack()
    {
        // Nhấn phím tấn công (ví dụ: J hoặc chuột trái)
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");  // gọi animation tấn công

            // Gọi hàm đánh sau 1 chút (nếu muốn sync với animation)
            Invoke(nameof(PerformAttack), 0.1f);
        }
    }

    void PerformAttack()
    {
        // Phát hiện enemy trong vùng đòn đánh
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hits)
        {
            Debug.Log("Đánh trúng: " + enemy.name);
            // TODO: gọi hàm nhận sát thương của enemy
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
