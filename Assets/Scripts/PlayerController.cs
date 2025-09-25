using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    private Character runtimeStats;

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
        runtimeStats = PlayerStatsManager.Instance.GetRuntimeStats();
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

        if (Input.GetKeyDown(KeyCode.Z))
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

            AudioManager.Instance.PlayPlayerAttack();
        }
    }
    public void Damage()
    {
        if (runtimeStats == null) return;

        // Phát hiện enemy trong vùng đòn đánh
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hits)
        {
            int damage = runtimeStats.GetTotalDamage();
            bool isCrit = Random.value < runtimeStats.GetTotalCritChance();

            if (isCrit)
            {
                damage = Mathf.RoundToInt(damage * runtimeStats.GetTotalCritDamage());
                PopupTextManager.Instance.ShowDamageCrit(damage, enemy.transform.position);
                Debug.Log("🔥 Chí mạng!");
            }
            else
            {
                PopupTextManager.Instance.ShowDamage(damage, enemy.transform.position);
            }

            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
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
    //============= EQUIP / UNEQUIP ===========//
    public void EquipItem(EquipmentData item)
    {
        if (runtimeStats == null) return;
        runtimeStats.Equip(item);
        Debug.Log($"🔧 Đã trang bị {item.itemName} ({item.equipmentType})");

        CharacterStatsUI ui = GameObject.FindObjectOfType<CharacterStatsUI>();
        if (ui != null) ui.UpdateUI();
    }

    public void UnequipItem(EquipmentType type)
    {
        if (runtimeStats == null) return;
        runtimeStats.Unequip(type);
        Debug.Log($"❌ Đã tháo trang bị {type}");

        CharacterStatsUI ui = GameObject.FindObjectOfType<CharacterStatsUI>();
        if (ui != null) ui.UpdateUI();
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
