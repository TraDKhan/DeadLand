using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        HandleAnimation();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Nếu có input cả hai trục -> ưu tiên trục vừa nhấn mạnh hơn
        if (x != 0 && y != 0)
        {
            // Ưu tiên trục được nhấn gần nhất (trục vừa thay đổi)
            if (Mathf.Abs(x) > Mathf.Abs(y))
                y = 0;
            else
                x = 0;
        }

        movement = new Vector2(x, y);
    }

    void MovePlayer()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    void HandleAnimation()
    {
        bool isMoving = movement != Vector2.zero;

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetBool("IsMoving", isMoving);

        // Lưu hướng di chuyển cuối cùng (dùng khi Idle)
        if (isMoving)
        {
            animator.SetFloat("LastMoveX", movement.x);
            animator.SetFloat("LastMoveY", movement.y);
        }
    }
}
