using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{

    public Tile tile;

    public Tile accessibleTile;
    public Tile inaccessibleTile;
    public Tile pathTile;

    private Tilemap tilemap = null;
    private Vector3Int tileMapSize;


    public void startAutoGenMap(){
        Debug.Log("starting autogeneration process!");
        
        // set variables
        tilemap = gameObject.GetComponent<Tilemap>();

        HashSet<Vector2> cells = generateCellArray();
        
        cells.UnionWith(genBlock(-1, -7, 4, 15));
        cells.UnionWith(genBlock(-13, -2, 27, 2));
        cells = clearBlock(0, 2, 2, 4, cells);

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

    public void publicCheckPath(Vector3Int start, Vector3Int pos)
    {

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


    public HashSet<Vector2> clearBlock(int x, int y, int w, int h, HashSet<Vector2> cells)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                cells.Remove(new Vector2(x + i, y + j));
            }
        }
        return cells;
    }

    public void checkPath(Vector2 startpos, Vector2 endpos)
    {

    }

}