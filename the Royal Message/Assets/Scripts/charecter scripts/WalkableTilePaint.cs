using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[System.Serializable]
public class DecorationTile
{
    public string name;
    public Tile tile;
    [Range(0, 100)]
    public float spawnChance;
}

public class WalkableTilePaint : MonoBehaviour
{
    [Header("Base Tile Assets")]
    public Tile grass;
    public Tile topPath;
    public Tile centerPath;
    public Tile bottomPath;

    [Header("House Spawning")]
    public GameObject housePrefab;
    [Range(0, 100)]
    public float houseSpawnChance = 2f; // Keep this low so houses aren't everywhere
    public float minDistanceBetweenHouses = 10f;
    private float lastHouseX = -100f;

    [Header("Decoration Settings")]
    public Tilemap decorationMap;
    public List<DecorationTile> decorations;
    [Range(0, 100)]
    public float nothingSpawnChance = 90f;

    [Header("Setup")]
    public Tilemap groundPath;
    public Transform player;

    [Header("Generation Settings")]
    public int grassLayerHeight = 10;
    public int renderDistance = 30;
    public int preBuildAmount = 20;

    private int currentColumnIndex = 0;

    void Start()
    {
        currentColumnIndex = -5;
        for (int i = 0; i < preBuildAmount; i++)
        {
            GenerateColumn();
        }
    }

    void Update()
    {
        if (player.position.x > currentColumnIndex - renderDistance)
        {
            GenerateColumn();
        }
    }

    void GenerateColumn()
    {
        int topLimit = 1 + grassLayerHeight;
        int bottomLimit = -1 - grassLayerHeight;

        // Try to spawn a house once per column
        TrySpawnHouse();

        for (int y = topLimit; y >= bottomLimit; y--)
        {
            Vector3Int pos = new Vector3Int(currentColumnIndex, y, 0);

            if (y == 1 || y == 0 || y == -1)
            {
                groundPath.SetTile(pos, centerPath);
            }
            else if (y == 2)
            {
                groundPath.SetTile(pos, topPath);
                TrySpawnDecoration(pos);
            }
            else if (y == -2)
            {
                groundPath.SetTile(pos, bottomPath);
                TrySpawnDecoration(pos);
            }
            else
            {
                groundPath.SetTile(pos, grass);
                TrySpawnDecoration(pos);
            }
        }
        currentColumnIndex++;
    }

    void TrySpawnHouse()
    {
        // 1. Distance check (so houses don't overlap)
        if (currentColumnIndex < lastHouseX + minDistanceBetweenHouses) return;

        // 2. Roll for spawn chance
        float roll = Random.Range(0f, 100f);
        if (roll < houseSpawnChance)
        {
            // 3. LOCK THE POSITION:
            // We use currentColumnIndex for X
            // We use a fixed Y value (e.g., 4.5f) so they are all in a perfect line
            // We use Z = 0 for 2D
            float fixedTopY = 6.2f; // <--- ADJUST THIS to line them up with your grass
            Vector3 spawnPos = new Vector3(currentColumnIndex, fixedTopY, 0);

            // 4. Spawn the House
            Instantiate(housePrefab, spawnPos, Quaternion.identity);

            lastHouseX = currentColumnIndex;
        }
    }

    void TrySpawnDecoration(Vector3Int pos)
    {
        float roll = Random.Range(0f, 100f);
        if (roll < nothingSpawnChance) return;

        foreach (var deco in decorations)
        {
            float decoRoll = Random.Range(0f, 100f);
            if (decoRoll <= deco.spawnChance)
            {
                decorationMap.SetTile(pos, deco.tile);
                break;
            }
        }
    }
}