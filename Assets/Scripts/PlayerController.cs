using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Attack Settings")]
    public int attackDamage = 1;
    public float attackCooldown = 0.5f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAttack();

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                DropAllSelected();
            }
            else
            {
                DropOneSelected();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) // 🟢 phím mở popup để nhập số lượng vứt
        {
            DropCustomAmount();
        }
    }
    
    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            float attackX = animator.GetFloat("LastMoveX");
            float attackY = animator.GetFloat("LastMoveY");

            animator.SetFloat("AttackX", attackX);
            animator.SetFloat("AttackY", attackY); 

            UpdateAttackPointDirection();
            animator.SetTrigger("Attack");
        }
    }
    public void Damage()
    {
        // Phát hiện enemy trong vùng đòn đánh
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hits)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage); 
        }
    }
    void UpdateAttackPointDirection()
    {
        float attackX = animator.GetFloat("LastMoveX");
        float attackY = animator.GetFloat("LastMoveY");

        Vector2 direction = new Vector2(attackX, attackY).normalized;

        // Đặt vị trí cục bộ của attackPoint theo hướng
        float offsetDistance = 0.5f; // khoảng cách từ nhân vật đến điểm tấn công
        attackPoint.localPosition = direction * offsetDistance;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    //============= XU LY ITEM ===========//
    private void DropOneSelected()
    {
        if (InventoryUI.Instance.selectedItem == null)
        {
            Debug.Log("Chưa chọn item nào để vứt!");
            return;
        }

        InventoryItem selected = InventoryUI.Instance.selectedItem;
        Vector3 dropPos = transform.position + transform.right * 1f;

        InventoryManager.Instance.DropItem(selected.itemData, 1, dropPos);
        Debug.Log($"Đã vứt 1 {selected.itemData.itemName}");

        if (selected.amount <= 0)
            InventoryUI.Instance.selectedItem = null;
    }

    private void DropAllSelected()
    {
        if (InventoryUI.Instance.selectedItem == null)
        {
            Debug.Log("Chưa chọn item nào để vứt!");
            return;
        }

        InventoryItem selected = InventoryUI.Instance.selectedItem;
        int amount = selected.amount;
        Vector3 dropPos = transform.position + transform.right * 1f;

        InventoryManager.Instance.DropItem(selected.itemData, amount, dropPos);
        Debug.Log($"Đã vứt toàn bộ {amount} {selected.itemData.itemName}");

        InventoryUI.Instance.selectedItem = null;
    }

    private void DropCustomAmount()
    {
        if (InventoryUI.Instance.selectedItem == null)
        {
            Debug.Log("Chưa chọn item nào để vứt!");
            return;
        }

        // 🟢 Gọi UI popup nhập số lượng
        DropAmountUI.Instance.Show(InventoryUI.Instance.selectedItem);
    }
}
