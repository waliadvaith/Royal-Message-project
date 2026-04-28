using UnityEngine;
using UnityEngine.SceneManagement; // Essential for switching scenes

public class LevelTeleporter : MonoBehaviour
{
    [Header("Settings")]
    public string sceneToLoad; // Type the exact name of the Castle scene here
    public bool requiresInteraction = false; // Set true if they must press 'E'

    private bool playerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!requiresInteraction)
            {
                LoadNextScene();
            }
            else
            {
                playerInRange = true;
                Debug.Log("Press E to enter the Castle");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (requiresInteraction && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("You forgot to type the Scene Name in the Inspector!");
        }
    }
}