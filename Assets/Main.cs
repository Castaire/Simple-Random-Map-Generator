using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;



// USAGE: attached to control button in main scene to auto-generate maps 
public class Main : MonoBehaviour
{   
    public Tilemap mainTileMap;
    public Button autoGenButton;
    private TileMapController tileMapController;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World, motherfuckers!");
        tileMapController = mainTileMap.GetComponent<TileMapController>();

        autoGenButton.onClick.AddListener(() => tileMapController.startAutoGenMap());
    }
}
