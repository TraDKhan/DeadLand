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

    private Vector2 lastPosition;

    void Awake()
    {
        GameObject foundPlayer = GameObject.FindWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        currentState = patrolPoints.Length > 0 ? EnemyState.Patrol : EnemyState.Idle;

        attackBehavior = GetComponent<IAttackBehavior>();
        if (attackBehavior == null && CompareTag("Ghost"))
        {
            attackBehavior = gameObject.AddComponent<GhostAttack>();
        }

        lastPosition = rb.position;
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

        // 🔎 Nếu đang Chase thì log tốc độ thực
        if (currentState == EnemyState.Chase)
        {
            float distance = Vector2.Distance(rb.position, lastPosition);
            float speed = distance / Time.fixedDeltaTime;
        }

        lastPosition = rb.position; // cập nhật lại sau mỗi FixedUpdate
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
            UpdateAnimator(Vector2.zero);
            return;
        }

        Transform target = patrolPoints[patrolIndex];
        if (target == null) return;

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        Vector2 newPosition = rb.position + direction * patrolSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition); // ✅ di chuyển bằng MovePosition
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
        Vector2 newPosition = rb.position + direction * chaseSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
        Flip(direction);
        UpdateAnimator(direction);
    }

    void HandleAttack()
    {
        UpdateAnimator(Vector2.zero);
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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        Gizmos.color = Color.green;
        foreach (var point in patrolPoints)
        {
            if (point != null)
                Gizmos.DrawSphere(point.position, 0.1f);
        }
    }
}
