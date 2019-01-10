using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{

    public Tile tile;

    private Tilemap tilemap;
    private Vector3Int tileMapSize;


    public void startAutoGenMap(){
        Debug.Log("starting autogeneration process!");
        
        // set variables
        tilemap = gameObject.GetComponent<Tilemap>();

        HashSet<Vector2> cells = generateCellArray();

        foreach(Vector2 cell in cells)
        {
            addCell(Mathf.FloorToInt(cell.x), Mathf.FloorToInt(cell.y));
        }
    }

    public void addCell(int x, int y)
    {
        tilemap.SetTile(new Vector3Int(x, y, 0), tile);
    }
    
    public HashSet<Vector2> generateCellArray()
    {
        HashSet<Vector2> set = new HashSet<Vector2>();

        int width = 14;
        int height = 8;

        for(int i = -width; i <= width; i++)
        {
            set.Add(new Vector2(i, -height));
            set.Add(new Vector2(i, height));
        }

        for(int i = 1-height; i < height; i++)
        {
            set.Add(new Vector2(-width, i));
            set.Add(new Vector2(width, i));
        }

        return set;
    }



}