using UnityEngine;

public class NPCAI : MonoBehaviour
{
    private bool playerIsClose = false;

    // Detected when the Player enters the NPC's trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            Debug.Log("Player is nearby! You can now interact.");
            // Example: Show an interaction prompt or UI
        }
    }

    // Detected when the Player leaves the NPC's trigger zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            Debug.Log("Player left the area.");
       
        }
    }

    private void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacting with NPC...");
      
        }
    }
}
