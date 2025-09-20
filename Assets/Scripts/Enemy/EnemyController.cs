using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    public enum EnemyState { Idle, Patrol, Chase, Attack }

    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;

    [Header("Settings")]
    public float detectionRange = 5f;
    public float chaseSpeed = 2f;
    public float patrolSpeed = 1.5f;
    public float reachThreshold = 0.2f;
    public float stopDistance = 1.5f;

    [Header("Timers")]
    public float patrolWaitTime = 2f;

    private EnemyState currentState;
    private IAttackBehavior attackBehavior;
    private Rigidbody2D rb;
    private Animator animator;

    private int patrolIndex = 0;
    private float patrolWaitTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();

        currentState = patrolPoints.Length > 0 ? EnemyState.Patrol : EnemyState.Idle;

        attackBehavior = GetComponent<IAttackBehavior>();
        if (attackBehavior == null && CompareTag("Ghost"))
        {
            attackBehavior = gameObject.AddComponent<GhostAttack>();
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayer <= detectionRange)
                    ChangeState(EnemyState.Chase);
                else if (patrolPoints.Length > 0)
                    ChangeState(EnemyState.Patrol);
                break;

            case EnemyState.Patrol:
                if (distanceToPlayer <= detectionRange)
                    ChangeState(EnemyState.Chase);
                break;

            case EnemyState.Chase:
                if (distanceToPlayer > detectionRange)
                    ChangeState(patrolPoints.Length > 0 ? EnemyState.Patrol : EnemyState.Idle);
                else if (distanceToPlayer <= stopDistance)
                    ChangeState(EnemyState.Attack);
                break;

            case EnemyState.Attack:
                if (distanceToPlayer > stopDistance)
                    ChangeState(EnemyState.Chase);
                break;
        }
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Patrol: HandlePatrol(); break;
            case EnemyState.Chase: HandleChase(); break;
            case EnemyState.Attack: HandleAttack(); break;
        }
    }

    void ChangeState(EnemyState newState)
    {
        currentState = newState;
        animator.SetBool("IsMoving", false);
    }

    void HandlePatrol()
    {
        if (patrolPoints.Length == 0) return;

        if (patrolWaitTimer > 0f)
        {
            patrolWaitTimer -= Time.fixedDeltaTime;
            rb.linearVelocity = Vector2.zero; // ✅ sửa
            UpdateAnimator(Vector2.zero);
            return;
        }

        Transform target = patrolPoints[patrolIndex];
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * patrolSpeed * Time.fixedDeltaTime);

        Flip(direction);
        UpdateAnimator(direction);

        if (Vector2.Distance(transform.position, target.position) < reachThreshold)
        {
            patrolWaitTimer = patrolWaitTime;
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    void HandleChase()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * chaseSpeed * Time.fixedDeltaTime);

        Flip(direction);
        UpdateAnimator(direction);
    }

    void HandleAttack()
    {
        rb.linearVelocity = Vector2.zero;
        UpdateAnimator(Vector2.zero);

        // Gọi hành vi attack (cooldown sẽ do GhostAttack quản lý)
        //animator.SetTrigger("Attack");
        if (attackBehavior != null)
        {
            attackBehavior.Attack(player);
        }
    }

    void UpdateAnimator(Vector2 direction)
    {
        animator.SetBool("IsMoving", direction != Vector2.zero);
    }

    void Flip(Vector2 direction)
    {
        if (direction.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = direction.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.green;
        foreach (var point in patrolPoints)
        {
            if (point != null)
                Gizmos.DrawSphere(point.position, 0.1f);
        }
    }
}
