using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class WalkableTilePaint : MonoBehaviour
{
    //tiles
    public Tile Grass;
    public Tile CenterPath;
    public Tile TopPath;
    public Tile BottomPath;
    //tilemap and position
    public Tilemap WalkablePath;
    public Vector3Int position;
    private string currentTile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 6; i == -6; i--)
        {

        }
    }
}
