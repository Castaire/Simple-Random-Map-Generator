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

        cells.UnionWith(genBlock(-11, 0, 6, 6));
        cells.UnionWith(genBlock(3, 0, 6, 6));
        cells.UnionWith(genBlock(-2, -4, 4, 2));

        clearBlock(-10, 1, 4, 4, cells);
        clearBlock(4, 1, 4, 4, cells);

        clearBlock(-1, -4, 2, 1, cells);

        cells.UnionWith(genBlock(-8, 3, 2, 2));
        cells.UnionWith(genBlock(6, 3, 2, 2));

        foreach (Vector2 cell in cells)
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

    public HashSet<Vector2> genBlock(int x, int y, int w, int h)
    {
        HashSet<Vector2> set = new HashSet<Vector2>();

        for (int i = 0; i < w; i++)
        {
            for(int j = 0; j < h; j++)
            {
                set.Add(new Vector2(x + i, y + j));
            }
        }

        return set;
    }

    public HashSet<Vector2> clearBlock(int x, int y, int w, int h, HashSet<Vector2> set)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                set.Remove(new Vector2(x + i, y + j));
            }
        }

        return set;
    }



}