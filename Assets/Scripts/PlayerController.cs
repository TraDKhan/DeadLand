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

        //gọi phím x đê vứt vật phẩm đầu tiên
        if (Input.GetKeyDown(KeyCode.X))
        {
            DropSelectedItem();
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
    private void DropFirstItem()
    {
        if (InventoryManager.Instance.items.Count == 0)
        {
            Debug.Log("Không có item nào để vứt!");
            return;
        }

        // 🔹 Ở đây mình demo vứt item đầu tiên trong balo
        InventoryItem firstItem = InventoryManager.Instance.items[0];
        Vector3 dropPos = transform.position + transform.right * 1f;

        InventoryManager.Instance.DropItem(firstItem.itemData, 1, dropPos);

        Debug.Log($"Đã vứt 1 {firstItem.itemData.itemName}");
    }

    private void DropSelectedItem()
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

        // Nếu đã vứt hết thì bỏ chọn
        if (selected.amount <= 0)
        {
            InventoryUI.Instance.selectedItem = null;
        }
    }
}
