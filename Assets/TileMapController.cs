using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{

    public Tile tile;

    int width = 14;
    int height = 8;

    public Tile accessibleTile;
    public Tile inaccessibleTile;
    public Tile pathTile;

    private Tilemap tilemap = null;
    private Vector3Int tileMapSize;

    private HashSet<Vector2Int> cells;

    public void startAutoGenMap(){
        //Debug.Log("starting autogeneration process!");
        
        // set variables
        tilemap = gameObject.GetComponent<Tilemap>();

        cells = generateCellArray();
        
        cells.UnionWith(genBlock(-1, -7, 4, 15));
        cells.UnionWith(genBlock(-13, -2, 27, 2));

        cells = clearBlock(0, 2, 2, 4, cells);
        cells = clearBlock(-6, -2, 1, 2, cells);

        //clearBlock(-10, 1, 4, 4, cells);
        //clearBlock(4, 1, 4, 4, cells);

        //clearBlock(-1, -4, 2, 1, cells);

        //cells.UnionWith(genBlock(-8, 3, 2, 2));
        //cells.UnionWith(genBlock(6, 3, 2, 2));

        foreach (Vector2 cell in cells)
        {
            addCell(Mathf.FloorToInt(cell.x), Mathf.FloorToInt(cell.y));
        }
    }

    public Vector3 getPos(int x, int y)
    {
        if(tilemap != null)
            return tilemap.CellToWorld(new Vector3Int(x, y, 0));
        else
        {
            print("ERROR: attempted to retrieve position with invalid tilemap");
            return new Vector3(0, 0, 0);
        }
    }

    public void addCell(int x, int y)
    {
        tilemap.SetTile(new Vector3Int(x, y, 0), tile);
    }

    public void addOther(int x, int y, int type)
    {
        switch(type)
        {
            case 0:
                tilemap.SetTile(new Vector3Int(x, y, 0), accessibleTile);
                break;
            case 1:
                tilemap.SetTile(new Vector3Int(x, y, 0), inaccessibleTile);
                break;
            case 2:
                tilemap.SetTile(new Vector3Int(x, y, 0), pathTile);
                break;
        }
    }

    public void checkPath(Vector2Int start, Vector2Int pos)
    {
        int[,] heuristics = new int[width, height];
        int max = 10000;

        HashSet<Vector2Int> toExamine = new HashSet<Vector2Int>();
    }
    
    public HashSet<Vector2Int> generateCellArray()
    {
        HashSet<Vector2Int> set = new HashSet<Vector2Int>();
        
        for(int i = -width; i <= width; i++)
        {
            set.Add(new Vector2Int(i, -height));
            set.Add(new Vector2Int(i, height));
        }

        for(int i = 1-height; i < height; i++)
        {
            set.Add(new Vector2Int(-width, i));
            set.Add(new Vector2Int(width, i));
        }

        return set;
    }

    public HashSet<Vector2Int> genBlock(int x, int y, int w, int h)
    {
        HashSet<Vector2Int> set = new HashSet<Vector2Int>();

        for (int i = 0; i < w; i++)
        {
            for(int j = 0; j < h; j++)
            {
                set.Add(new Vector2Int(x + i, y + j));
            }
        }

        return set;
    }


    public HashSet<Vector2Int> clearBlock(int x, int y, int w, int h, HashSet<Vector2Int> set)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                set.Remove(new Vector2Int(x + i, y + j));
            }
        }
        return set;
    }

}