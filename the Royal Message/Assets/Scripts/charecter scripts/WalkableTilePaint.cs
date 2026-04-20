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



    [Header("Decoration Settings")]

    public Tilemap decorationMap;

    public List<DecorationTile> decorations;

    [Range(0, 100)]

    public float nothingSpawnChance = 90f;



    [Header("Setup")]

    public Tilemap groundPath; // Updated name

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