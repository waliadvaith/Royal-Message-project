using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class DialogueTrigger : MonoBehaviour
{
    // This creates the list in your Inspector!
    public List<DialogueLine> conversation;

    private bool playerInRange = false;
    

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E Pressed - Attempting to start dialogue");

                if (DialogueManager.instance == null)
                {
                    Debug.LogError("No DialogueManager found in the scene!");
                    return;
                }

                if (!DialogueManager.instance.isOpen)
                {
                    DialogueManager.instance.StartDialogue(conversation);
                }
                else
                {
                    Debug.Log("Dialogue is already open.");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered range");
            playerInRange = true;
            

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
        
    }
}