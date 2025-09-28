using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Scene muốn chuyển tới")]
    public string targetScene;

    [Header("SpawnPoint ở scene kia")]
    public string targetSpawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ChangeScene(targetScene, targetSpawnPoint);
        }
    }
}
