using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelTeleporter : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 spawnPositionInNewScene;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the sequence and don't let the player die
            DontDestroyOnLoad(other.transform.root.gameObject);
            DontDestroyOnLoad(transform.root.gameObject);
            
            StartCoroutine(TeleportSequence(other.transform.root.gameObject));
        }
    }

    IEnumerator TeleportSequence(GameObject player)
    {
        // 1. Disable the player's movement and physics so they don't drift during load
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        // 2. Load the scene
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!loadScene.isDone) { yield return null; }

        // 3. WAIT. Give Unity 2 frames to settle the new scene's physics
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();

        // 4. Force the position
        player.transform.position = spawnPositionInNewScene;

        // 5. Re-enable physics AFTER the move is finished
        if (rb != null)
        {
            rb.simulated = true;
            rb.linearVelocity = Vector2.zero; // Stop any carry-over momentum
        }

        Debug.Log("Teleport Complete to: " + spawnPositionInNewScene);
    }
}