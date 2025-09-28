using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Tên spawn point (đặt trùng với portal)")]
    public string spawnName;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
