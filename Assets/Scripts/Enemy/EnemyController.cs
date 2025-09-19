using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState { Idle, Patrol, Chase }
    private EnemyState currentState;

    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;

    [Header("Settings")]
    public float detectionRange = 5f;
    public float chaseSpeed = 2f;
    public float patrolSpeed = 1.5f;
    public float reachThreshold = 0.2f;
    public float stopDistance = 1.5f;

    private Rigidbody2D rb;
    private int currentPatrolIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = patrolPoints.Length > 0 ? EnemyState.Patrol : EnemyState.Idle;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (patrolPoints.Length > 0)
                    ChangeState(EnemyState.Patrol);
                else if (distanceToPlayer <= detectionRange)
                    ChangeState(EnemyState.Chase);
                break;

            case EnemyState.Patrol:
                Patrol();
                if (distanceToPlayer <= detectionRange)
                    ChangeState(EnemyState.Chase);
                break;

            case EnemyState.Chase:
                ChasePlayer();
                if (distanceToPlayer > detectionRange)
                    ChangeState(patrolPoints.Length > 0 ? EnemyState.Patrol : EnemyState.Idle);
                break;
        }
    }

    void ChangeState(EnemyState newState)
    {
        currentState = newState;
        Debug.Log("Enemy chuyển sang trạng thái: " + currentState);
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        if (targetPoint == null) return;

        Vector2 direction = (targetPoint.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < reachThreshold)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Nếu còn xa hơn khoảng stopDistance thì mới di chuyển
        if (distanceToPlayer > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * chaseSpeed * Time.deltaTime);
        }
        else
        {
            // Dừng lại hoặc có thể xoay mặt về phía player nếu muốn
            rb.linearVelocity = Vector2.zero;
        }
    }

    // --- Vẽ Gizmos để dễ chỉnh trong Scene ---
    void OnDrawGizmosSelected()
    {
        // Phạm vi phát hiện player
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Các điểm tuần tra
        Gizmos.color = Color.green;
        if (patrolPoints != null)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                if (patrolPoints[i] != null)
                    Gizmos.DrawSphere(patrolPoints[i].position, 0.1f);

                if (i < patrolPoints.Length - 1 && patrolPoints[i] != null && patrolPoints[i + 1] != null)
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
            }
        }
    }
}
