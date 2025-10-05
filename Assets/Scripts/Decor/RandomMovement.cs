using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform movementArea; // GameObject đại diện vùng di chuyển
    public float waitTime = 1f;

    private Vector2 targetPosition;
    private bool isWaiting = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Bounds areaBounds;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (movementArea != null)
        {
            BoxCollider2D box = movementArea.GetComponent<BoxCollider2D>();
            areaBounds = new Bounds(box.bounds.center, box.bounds.size);
        }

        PickNewTarget();
    }

    void Update()
    {
        if (isWaiting) return;

        Vector2 currentPosition = transform.position;
        Vector2 direction = targetPosition - currentPosition;

        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

        animator.SetFloat("Speed", direction.magnitude);
        if (direction.x != 0) spriteRenderer.flipX = direction.x < 0;

        if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
        {
            StartCoroutine(WaitAndPickNewTarget());
        }
    }

    IEnumerator WaitAndPickNewTarget()
    {
        isWaiting = true;
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(waitTime);
        PickNewTarget();
        isWaiting = false;
    }

    void PickNewTarget()
    {
        float x = Random.Range(areaBounds.min.x, areaBounds.max.x);
        float y = Random.Range(areaBounds.min.y, areaBounds.max.y);
        targetPosition = new Vector2(x, y);
    }

    void OnDrawGizmos()
    {
        if (movementArea != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(movementArea.position, movementArea.localScale);
        }
    }
}