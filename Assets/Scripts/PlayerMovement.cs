using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;

    [Header("MP Settings")]
    public float maxMP = 100f;
    public float mpDrainPerSecond = 10f;
    public float mpRegenPerSecond = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private float currentMP;
    private bool isSprinting;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentMP = maxMP; // full MP khi bắt đầu
        ManaBarHUD.Instance?.UpdateMP(currentMP, maxMP);
    }

    void Update()
    {
        HandleInput();
        HandleAnimation();
        HandleMP();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0 && y != 0) // Ưu tiên 1 hướng
        {
            if (Mathf.Abs(x) > Mathf.Abs(y))
                y = 0;
            else
                x = 0;
        }

        movement = new Vector2(x, y);

        // Shift để chạy nhanh
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentMP > 0)
        {
            movement *= sprintMultiplier;
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
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

        if (isMoving)
        {
            animator.SetFloat("LastMoveX", movement.x);
            animator.SetFloat("LastMoveY", movement.y);
        }
    }

    void HandleMP()
    {
        if (isSprinting)
        {
            currentMP -= mpDrainPerSecond * Time.deltaTime;
            if (currentMP < 0) currentMP = 0;
        }
        else
        {
            currentMP += mpRegenPerSecond * Time.deltaTime;
            if (currentMP > maxMP) currentMP = maxMP;
        }

        // Cập nhật UI qua HUD
        ManaBarHUD.Instance?.UpdateMP(currentMP, maxMP);
    }
}
