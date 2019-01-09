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

        // TESTING:


        // set variables
        tilemap = gameObject.GetComponent<Tilemap>();
        int halfX = 19;  // in terms of cells
        int halfY = 8;   // in terms of cells

        addWall(halfX, halfY);
        



    }

    // USAGE: adds 1-depth walls to the grid within canvass
    // NOTE:  grid is centered at cell position (0, 0, 0)
    public void addWall(int halfX, int halfY){

        // top / bottom boundary
        for(int i = -halfX; i <= halfX; i++){
            tilemap.SetTile(new Vector3Int(i, -halfY, 0), tile);
            tilemap.SetTile(new Vector3Int(i, halfY, 0), tile);
        }

        // left / right boundary
        for(int i = -halfY; i <= halfY; i++){
            tilemap.SetTile(new Vector3Int(-halfX, i, 0), tile);
            tilemap.SetTile(new Vector3Int(halfX, i, 0), tile);
        }
    }

    public Vector3Int[] generateRandomPositionArray(){

        Vector3Int[] tilePositions = new Vector3Int[4];

        // TODO: NOTICE ME SENPAI


        return(tilePositions);
    }

    // USAGE: prints input array of positions as tiles
    public void arrayToTiles(Vector3Int[] arr, Tile t){



    }



}