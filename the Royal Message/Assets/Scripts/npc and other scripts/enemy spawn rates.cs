using UnityEngine;

public class enemyspawnrates : MonoBehaviour
{
    public GameObject BarbarianPrefab;
    public float minX, maxX, minZ, maxZ;

    void SpawnEnemy()
    {
        Vector3 randomPos = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        Instantiate(BarbarianPrefab, randomPos, Quaternion.identity);
    }

}
