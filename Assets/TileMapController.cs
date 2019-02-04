using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{

    public Tile tile;
    
    private int width = 14;
    private int height = 8;

    public Tile accessibleTile;
    public Tile inaccessibleTile;
    public Tile pathTile;

    private Tilemap tilemap = null;
    private Vector3Int tileMapSize;

    private HashSet<Vector2Int> gameCells;

    public void startAutoGenMap(int w, int h)
    {
        width = w;
        height = h;
        startAutoGenMap();
    }

    public void startAutoGenMap(){
        // set variables
        tilemap = gameObject.GetComponent<Tilemap>();

        gameCells = generateCellArray();
        /*for(int i = 0; i < 200; i++)
        {
            gameCells = randomize(gameCells);
        }*/

        /*
        gameCells.UnionWith(genCircle(2, 4, 1));

        gameCells.UnionWith(genCircle(6, 4, 2));

        gameCells.UnionWith(genCircle(10, 4, 3));

        gameCells.UnionWith(genCircle(14, 4, 4));

        gameCells.UnionWith(genCircle(6, 12, 5));

        gameCells.UnionWith(genCircle(14, 12, 6));

        gameCells.UnionWith(genCircle(24, 12, 7));

        gameCells.UnionWith(genCircle(36, 12, 8));
        */

        finalize();
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
        if(x >= 0 && x < width && y >= 0 && y< height)
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

    public bool checkPath(Vector2Int start, Vector2Int goal)
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
                return false;
            }

            int currentScore = max_cost;
            Vector2Int current = start;

            // find cell with loweset fScore in openSet
            foreach (Vector2Int cell in openSet)
            {
                if (fScore[cell.x, cell.y] < currentScore)
                {
                    currentScore = fScore[cell.x, cell.y];
                    current = cell;
                }
            }

            //print("lowest fScore cell: [" + current.x + "," + current.y + "]");

            if(current == goal)
            {
                print("checkPath: found goal! YAY");

                /*
                string s;
                for(int i = height-1; i > 0; i--)
                {
                    s = "";
                    for(int j = 0; j < width; j++)
                    {
                        s += gScore[j, i] + "   ";
                    }
                    print(s);
                }*/

                int temp = 0;
                while(current != start)
                {
                    temp++;
                    if (temp > 1000)
                    {
                        print("loop exceeded");
                        return true;
                    }

                    current = cameFrom[current.x, current.y];
                    addOther(current.x, current.y, 0);
                    //print("came from [" + current.x + "," + current.y + "]");
                }

                return true;
            }

            openSet.Remove(current);
            closedSet.Add(current);


            HashSet<Vector2Int> neighbours = new HashSet<Vector2Int>();


            // add neighbours of current if valid
            Vector2Int cellLeft = new Vector2Int(current.x - 1, current.y);
            Vector2Int cellRight = new Vector2Int(current.x + 1, current.y);
            Vector2Int cellUp = new Vector2Int(current.x, current.y + 1);
            Vector2Int cellDown = new Vector2Int(current.x, current.y - 1);

            if (current.x > 0 && !gameCells.Contains(cellLeft))
                neighbours.Add(cellLeft);
            if (current.y > 0 && !gameCells.Contains(cellDown))
                neighbours.Add(cellDown);
            if(current.x < width - 1  && !gameCells.Contains(cellRight))
                neighbours.Add(cellRight);
            if (current.y < height - 1 && !gameCells.Contains(cellUp))
                neighbours.Add(cellUp);

            foreach (Vector2Int neighbour in neighbours)
            {
                //print("current [" + current.x + "," + current.y + "] neighbour is ["
                //    + neighbour.x + "," + neighbour.y + "]: " + distBetweenNodes(start, neighbour));

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

        return false;
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

    public HashSet<Vector2Int> genDiamond(int x, int y, int r)
    {
        HashSet<Vector2Int> cells = new HashSet<Vector2Int>();

        for (int i = 0; i < r; i++)
        {
            cells.UnionWith(genBlock(x + i, y + i - r, 1, 2 * (r - i) - 1));
            cells.UnionWith(genBlock(x - i, y + i - r, 1, 2 * (r - i) - 1));
        }

        return cells;
    }

    public HashSet<Vector2Int> clearDiamond(int x, int y, int r, HashSet<Vector2Int> cells)
    {
        HashSet<Vector2Int> diamond = genDiamond(x, y, r);

        foreach(Vector2Int cell in diamond)
        {
            cells.Remove(cell);
        }

        return cells;
    }

    public HashSet<Vector2Int> genCircle(int x, int y, int d)
    {
        HashSet<Vector2Int> cells = new HashSet<Vector2Int>();

        bool even = (d % 2 == 0);

        int r = d / 2;

        if (even) r--;

        for(int i = x - r; i <= x + r; i++)
        {
            for (int j = y - r; j <= y + r; j++)
            {
                int a = i - x;
                int b = j - y;
                if (a * a + b * b <= r * r)
                {
                    cells.Add(new Vector2Int(i, j));
                    if(even)
                    {
                        cells.Add(new Vector2Int(i + 1, j));
                        cells.Add(new Vector2Int(i, j + 1));
                        cells.Add(new Vector2Int(i + 1, j + 1));
                    }
                }
            }

        }

        return cells;
    }

    public HashSet<Vector2Int> clearCircle(int x, int y, int r, HashSet<Vector2Int> cells)
    {
        HashSet<Vector2Int> circle = genCircle(x, y, r);

        foreach (Vector2Int cell in circle)
        {
            cells.Remove(cell);
        }

        return cells;
    }

    public HashSet<Vector2Int> randomize(HashSet<Vector2Int> cells)
    {
        int randA = Random.Range(0, 10);

        int randB = Random.Range(0, width / 5);
        int randC = Random.Range(0, height / 5);

        int randSmaller = Mathf.Min(randB, randC);

        int x = Random.Range(0, width - 1);
        int y = Random.Range(0, height - 1);
        
         if(randA < 3)
            cells.UnionWith(genBlock(x, y, randB, randC));
         else if(randA < 6)
            cells = clearBlock(x, y, randB, randC, cells);
         else if(randA < 8)
            cells.UnionWith(genDiamond(x, y, randSmaller));
         else
            cells = clearDiamond(x, y, randSmaller, cells);

        return cells;
    }

    public void makeSolvable(Vector2Int start, Vector2Int end)
    {
        int i = 0;
        while(checkPath(start, end) == false)
        {
            if(i > 2000)
            {
                print("ugh fuck it I give up");
                return;
            }
            gameCells = randomize(gameCells);
            i++;
        }

        finalize();
    }

    public void finalize()
    {
        foreach (Vector2 cell in gameCells)
        {
            addCell(Mathf.FloorToInt(cell.x), Mathf.FloorToInt(cell.y));
        }
    }
}