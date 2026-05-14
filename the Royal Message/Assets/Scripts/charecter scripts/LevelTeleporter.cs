using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LevelTeleporter : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 spawnPositionInNewScene;
    public GameObject Player;
    private bool isTeleporting = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Safety: Prevent triggering multiple times if the player bounces in the trigger
        if (isTeleporting) return;

        if (other.CompareTag("Player"))
        {
            isTeleporting = true;

            // Ensure player and portal survive the transition
            DontDestroyOnLoad(other.transform.root.gameObject);
            DontDestroyOnLoad(transform.root.gameObject);

            StartCoroutine(TeleportSequence(other.transform.root.gameObject));
        }
    }

    IEnumerator TeleportSequence(GameObject player)
    {
        // 1. Disable physics so they don't fall through the floor during load
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        // 2. Load the scene
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!loadScene.isDone) { yield return null; }

        // 3. Let the new scene initialize
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();

        // 4. Set position in new scene
        player.transform.position = spawnPositionInNewScene;


        // --- THE DYNAMIC CLAMP FIX ---
        // Check if the NEW scene name contains "Jonah"
        bool isJonahScene = sceneToLoad.ToLower().Contains("Jonah");

        // Tell the player script to enable or disable clamps
        CharacterMovement movement = player.GetComponentInChildren<CharacterMovement>();
        if (movement != null)
        {
            movement.useLimits = isJonahScene;
        }
        // ----------------------------

        yield return new WaitForFixedUpdate();

        // 5. Re-enable physics
        if (rb != null)
        {
            rb.simulated = true;
            rb.linearVelocity = Vector2.zero;
        }

        Debug.Log("Teleport Complete. Cleaning up portal...");

        // --- THE FIX ---
        // Now that the player is safely moved, the portal can finally delete itself
        Destroy(transform.root.gameObject);
    }
    public void Teleport()
    {
        if (isTeleporting) return;

        if (Player.CompareTag("Player"))
        {
            isTeleporting = true;

            // Ensure player and portal survive the transition
            DontDestroyOnLoad(Player.transform.root.gameObject);
            DontDestroyOnLoad(transform.root.gameObject);

            StartCoroutine(TeleportSequence(Player.transform.root.gameObject));
        }
    }
}

