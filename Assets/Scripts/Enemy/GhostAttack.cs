using UnityEngine;
public class GhostAttack : IAttackBehavior
{
    public void Attack(Transform target)
    {
        Debug.Log("Enemy tấn công cận chiến vào " + target.name);
        // Thêm logic gây sát thương, animation, v.v.
    }

}