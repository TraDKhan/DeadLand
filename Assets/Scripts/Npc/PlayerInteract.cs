using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Cấu hình tương tác")]
    public float interactRange = 2f;
    public LayerMask npcLayer;

    private NPCController currentNPC;
    private QuestGiver nearbyQuestGiver;

    void Update()
    {
        // Phát hiện NPC gần nhất mỗi frame
        DetectNearbyNPC();

        // Nhấn E để nói chuyện
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNPC.Interact();
        }

        // Nếu có QuestGiver (ví dụ NPC cho nhiệm vụ) — nhấn Q để kích hoạt
        if (nearbyQuestGiver != null && Input.GetKeyDown(KeyCode.Q))
        {
            nearbyQuestGiver.Interact();
        }
    }

    private void DetectNearbyNPC()
    {
        // Quét NPC quanh người chơi
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange, npcLayer);

        if (hit != null)
        {
            // Nếu có NPC hoặc QuestGiver gần đó
            currentNPC = hit.GetComponent<NPCController>();
            nearbyQuestGiver = hit.GetComponent<QuestGiver>();

            if (currentNPC != null)
                Debug.Log("➡️ Ấn [E] để nói chuyện với " + currentNPC.name);
            else if (nearbyQuestGiver != null)
                Debug.Log("➡️ Ấn [Q] để tương tác với " + nearbyQuestGiver.name);
        }
        else
        {
            currentNPC = null;
            nearbyQuestGiver = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ vòng tròn hiển thị vùng tương tác trong Editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
