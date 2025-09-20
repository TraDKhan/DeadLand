using UnityEngine;
using System.Collections;

public class GhostAttack : MonoBehaviour, IAttackBehavior
{
    [Header("Attack Settings")]
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public int damage = 10;
    public LayerMask playerLayer;

    [Header("Damage Over Time")]
    public bool enableDoT = true;
    public int dotDamage = 2;
    public float dotDuration = 5f;
    public float dotInterval = 1f;

    private float cooldownTimer = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public void Attack(Transform target)
    {
        if (cooldownTimer > 0f) return;

        animator?.SetTrigger("Attack");
        cooldownTimer = attackCooldown;
    }

    // Gọi trong event Animation Ghost
    public void OnAttackHit()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hit != null)
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log($"👻 GhostAttack: Gây {damage} sát thương vào {hit.name}");

                if (enableDoT)
                {
                    StartCoroutine(ApplyDamageOverTime(player));
                }
            }
        }
    }

    IEnumerator ApplyDamageOverTime(PlayerHealth player)
    {
        float elapsed = 0f;
        while (elapsed < dotDuration)
        {
            if (player == null) yield break;

            player.TakeDamage(dotDamage);
            Debug.Log($"☠️ GhostAttack: Gây {dotDamage} sát thương DoT vào {player.name}");

            yield return new WaitForSeconds(dotInterval);
            elapsed += dotInterval;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}