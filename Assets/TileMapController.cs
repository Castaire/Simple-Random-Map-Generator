using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{

    public Tile tile;

    int width = 8;
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

        cells.UnionWith(genBlock(2, 2, 2, 3));

        //cells.UnionWith(genBlock(-1, -7, 4, 15));
        //cells.UnionWith(genBlock(-13, -2, 27, 2));

        //cells = clearBlock(0, 2, 2, 4, cells);
        //cells = clearBlock(-6, -2, 1, 2, cells);

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

    public int distBetweenNodes(Vector2Int node, Vector2Int goal)
    {
        return Mathf.Abs(goal.y - node.y) + Mathf.Abs(goal.x - node.x);
    }

    public void checkPath(Vector2Int start, Vector2Int goal)
    {
        print("checkPath from: [" + start.x + "," + start.y + "] -> ["
            + goal.x + "," + goal.y + "]");
        // max number of steps to get to goal
        int max_cost = 10 * width * height;
        
        // evaluated nodes
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();

        // current nodes
        HashSet<Vector2Int> openSet = new HashSet<Vector2Int>();

        // init with starting node
        openSet.Add(start);

        // heuristics: for each cell, the "closest" cell
        // (can be reached from there with minimal cost
        Vector2Int[,] cameFrom = new Vector2Int[width, height];

        // for each cell, cost to get there from start
        int[,] gScore = new int[width, height];

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                gScore[i, j] = max_cost;
            }
        }

        gScore[start.x, start.y] = 0;

        // cost of getting from start to goal via this node
        int[,] fScore = new int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                fScore[i, j] = max_cost;
            }
        }

        // totally guessing at a reasonable path length
        fScore[start.x, start.y] = width + height;

        // temp val to break while loop if long-running
        int max = 10000;

        while(openSet.Count > 0)
        {
            // check in case I did this wrong and we're in an inf loop
            max--;
            if(max <= 0)
            {
                print("checkPath: reached max iterations");
                return;
            }

            int currentScore = max_cost;
            Vector2Int current = start;

            // find cell with loweset fScore in openSet
            foreach(Vector2Int cell in openSet)
            {
                if (fScore[cell.x, cell.y] < currentScore)
                    current = cell;
            }

            print("lowest fScore cell: [" + current.x + "," + current.y + "]");

            if(current == goal)
            {
                print("checkPath: found goal! YAY");

                string s;
                for(int i = height-1; i > 0; i--)
                {
                    s = "";
                    for(int j = 0; j < width; j++)
                    {
                        s += gScore[j, i] + "   ";
                    }
                    print(s);
                }

                return;
            }

            openSet.Remove(current);
            closedSet.Add(current);


            HashSet<Vector2Int> neighbours = new HashSet<Vector2Int>();


            // add neighbours of current if valid
            Vector2Int cellLeft = new Vector2Int(current.x - 1, current.y);
            Vector2Int cellRight = new Vector2Int(current.x + 1, current.y);
            Vector2Int cellUp = new Vector2Int(current.x, current.y + 1);
            Vector2Int cellDown = new Vector2Int(current.x, current.y - 1);

            if (current.x > 0 && !cells.Contains(cellLeft))
                neighbours.Add(cellLeft);
            if (current.y > 0 && !cells.Contains(cellDown))
                neighbours.Add(cellDown);
            if(current.x < width - 1  && !cells.Contains(cellRight))
                neighbours.Add(cellRight);
            if (current.y < height - 1 && !cells.Contains(cellUp))
                neighbours.Add(cellUp);

            foreach (Vector2Int neighbour in neighbours)
            {
                print("current [" + current.x + "," + current.y + "] neighbour is ["
                    + neighbour.x + "," + neighbour.y + "]: " + distBetweenNodes(start, neighbour));

                // don't re-evaluate neighbours
                if (closedSet.Contains(neighbour))
                    continue;

                // dist between start and neighbour
                int tentative_gScore = gScore[current.x, current.y] + 1;

                if (!openSet.Contains(neighbour))
                    openSet.Add(neighbour);
                else if (tentative_gScore >= gScore[neighbour.x, neighbour.y]) 
                    continue;
                cameFrom[neighbour.x, neighbour.y] = current;
                gScore[neighbour.x, neighbour.y] = tentative_gScore;
                fScore[neighbour.x, neighbour.y] = gScore[neighbour.x, neighbour.y]
                    + distBetweenNodes(neighbour, goal);
            }
        }

        print("checkPath: no path detected");
    }
    
    public HashSet<Vector2Int> generateCellArray()
    {
        HashSet<Vector2Int> set = new HashSet<Vector2Int>();
        
        for(int i = 0; i <= width; i++)
        {
            set.Add(new Vector2Int(i, 0));
            set.Add(new Vector2Int(i, height));
        }

        for(int i = 0; i < height; i++)
        {
            set.Add(new Vector2Int(0, i));
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