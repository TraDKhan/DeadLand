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
    public void EquipItem(ItemData item)
    {
        if (runtimeStats == null) return;

        // Chỉ cho phép trang bị nếu item là Equipment
        if (item.itemType == ItemType.Equipment)
        {
            runtimeStats.Equip(item);
            Debug.Log($"🔧 Đã trang bị {item.itemName} ({item.equipmentType})");

            CharacterStatsUI.Instance?.UpdateUI();
        }
        else
        {
            Debug.LogWarning($"❌ {item.itemName} không phải là trang bị, không thể equip!");
        }
    }

    public void UnequipItem(EquipmentType type)
    {
        if (runtimeStats == null) return;

        runtimeStats.Unequip(type);
        Debug.Log($"❌ Đã tháo trang bị {type}");

        CharacterStatsUI.Instance?.UpdateUI();
    }
}
