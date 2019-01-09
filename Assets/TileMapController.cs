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

        addWall();


    }

    // USAGE: adds 1-depth walls to the grid within canvass
    //        grid is centered at (0,0), +/- 13 (x), +/- 5 (y)
    // TODO: 
    public void addWall(){
        // top / bottom boundary
        for(int i = -13; i <= 13; i++){
            tilemap.SetTile(new Vector3Int(i, -5, 0), tile);
            tilemap.SetTile(new Vector3Int(i, 5, 0), tile);
        }

        // left / right boundary
        for(int i = -5; i <= 5; i++){
            tilemap.SetTile(new Vector3Int(-13, i, 0), tile);
            tilemap.SetTile(new Vector3Int(13, i, 0), tile);
        }
    }

    //public Vector3Int[] generateRandomPositionArray(){
    public void generateRandomPositionArray(){


        
    }



}