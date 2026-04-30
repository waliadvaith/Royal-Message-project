using UnityEngine;
using System.Collections;

public class BountyHunterManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject enemyPrefab;
    private Transform player;

    [Header("Settings")]
    public float spawnInterval = 10f; // Seconds between waves
    public float spawnDistance = 8f;  // How far behind the player they appear

    [Header("Bounty System (Integration)")]
    public int currentBountyLevel = 1; // Increase this to spawn more enemies

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (player != null)
            {
                SpawnWave();
            }
        }
    }

    void SpawnWave()
    {
        
        int amountToSpawn = currentBountyLevel;

        for (int i = 0; i < amountToSpawn; i++)
        {
            
            Vector3 spawnDir = -player.right;

            
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

            Vector3 spawnPos = player.position + (spawnDir * spawnDistance) + randomOffset;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }

        Debug.Log($"Bounty Level {currentBountyLevel}: Spawned {amountToSpawn} hunters behind player.");
    }
}