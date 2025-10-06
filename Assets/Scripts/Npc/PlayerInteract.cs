using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private NPCController currentNPC;
    private NPCController1 currentNPC1;

    [Header("Cấu hình tương tác")]
    public float interactRange = 2f;
    public LayerMask npcLayer;
    private QuestGiver nearbyQuestGiver;

    void Update()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNPC.Interact();
        }

        if (currentNPC != null && Input.GetKeyDown(KeyCode.Escape))
        {
            currentNPC.EndInteraction();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentNPC1 != null)
            {
                // Ví dụ: lần đầu nói chuyện
                currentNPC1.StartDialogue("first_meet");

                // Sau này có thể đổi thành quest_done, shop... tùy logic game
            }
        }

        DetectNearbyNPC();

        // Khi nhấn Q
        if (nearbyQuestGiver != null && Input.GetKeyDown(KeyCode.Q))
        {
            nearbyQuestGiver.Interact();
        }
    }

    private void DetectNearbyNPC()
    {
        // Tìm tất cả NPC trong bán kính tương tác
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange, npcLayer);

        if (hit != null)
        {
            nearbyQuestGiver = hit.GetComponent<QuestGiver>();
        }
        else
        {
            nearbyQuestGiver = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCController npc = collision.GetComponent<NPCController>();
        if (npc != null)
        {
            currentNPC = npc;
            Debug.Log("➡️ Ấn [E] để nói chuyện với " + npc.name);
        }

        NPCController1 npc1 = collision.GetComponent<NPCController1>();
        if (npc1 != null)
        {
            currentNPC1 = npc1;
            Debug.Log("➡️ Ấn [Q] để nói chuyện với " + npc1.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<NPCController>() == currentNPC)
        {
            currentNPC = null;
        }
        if (collision.GetComponent<NPCController1>() == currentNPC1)
        {
            currentNPC1 = null;
        }
    }
}
