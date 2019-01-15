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
    public Texture2D spriteTexture;

    private TileMapController tileMapController;

    private Vector3 initPlayerLocation = new Vector3(0, 0, 0);

    private GameObject player;
    private Sprite playerSprite;
    private SpriteRenderer playerSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World, motherfuckers!");
        
        tileMapController = mainTileMap.GetComponent<TileMapController>();

        autoGenButton.onClick.AddListener(() => tileMapController.startAutoGenMap());

        // create 'player' game object
        player = new GameObject("Player");
        player.transform.position = initPlayerLocation;
        player.AddComponent<MiniSpriteController>();
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<BoxCollider2D>();

        player.GetComponent<BoxCollider2D>().size = new Vector2(0.3f, 0.3f);

        player.GetComponent<Rigidbody2D>().gravityScale = 0;


        // attach 'sprite' to 'player'
        playerSpriteRenderer = player.AddComponent<SpriteRenderer>();
        playerSprite = Sprite.Create(spriteTexture, 
                                     new Rect(0.0f, 0.0f, spriteTexture.width, spriteTexture.height),
                                     new Vector2(0.5f, 0.5f), 100.0f);
        playerSpriteRenderer.sprite = playerSprite;


    }
}
