using UnityEngine;

public class LevelPortalManager : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public GameObject portalObject;
    public Vector3 playerSpawnInNextScene;

    private bool portalSpawned = false;

    void Update()
    {
        if (portalSpawned) return;

        // --- THE TRUTH CHECK ---
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // If it's NOT spawning, look at your Console (bottom left) 
        // It will tell you EXACTLY what is blocking the portal.
        if (enemies.Length > 0)
        {
            foreach (GameObject e in enemies)
            {
                Debug.Log("BLOCKING PORTAL: " + e.name + " at position " + e.transform.position);
            }
        }

        if (enemies.Length == 0)
        {
            SpawnPortal();
        }

        // --- EMERGENCY OVERRIDE ---
        // If the script fails, just press the 'K' key to force it!
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Manual Override: Forcing Portal!");
            SpawnPortal();
        }
    }

    void SpawnPortal()
    {
        if (portalSpawned) return;
        portalSpawned = true;

        if (portalObject != null)
        {
            LevelTeleporter tele = portalObject.GetComponent<LevelTeleporter>();
            if (tele != null) tele.spawnPositionInNewScene = playerSpawnInNextScene;

            portalObject.SetActive(true);
        }
    }
}