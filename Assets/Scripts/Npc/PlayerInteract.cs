using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private NPCController currentNPC;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCController npc = collision.GetComponent<NPCController>();
        if (npc != null)
        {
            currentNPC = npc;
            Debug.Log("➡️ Ấn [E] để nói chuyện với " + npc.npcName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<NPCController>() == currentNPC)
        {
            currentNPC = null;
        }
    }
}
