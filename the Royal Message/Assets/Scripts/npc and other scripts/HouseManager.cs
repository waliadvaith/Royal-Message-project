using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public GameObject housePrefab;
    public int numberOfHouses = 1;
    public Vector2 spawnAreaMin; // e.g., -50, -50
    public Vector2 spawnAreaMax; // e.g., 50, 50

    void Start()
    {
        SpawnVillages();
    }

    void SpawnVillages()
    {
        for (int i = 0; i < numberOfHouses; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0
            );

            // Check if position is clear (optional, but prevents overlapping)
            if (!Physics2D.OverlapCircle(randomPos, 3f))
            {
                Instantiate(housePrefab, randomPos, Quaternion.identity);
            }
        }
    }
}