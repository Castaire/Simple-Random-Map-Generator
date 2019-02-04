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

        autoGenButton.onClick.AddListener(() => InitPlayerAndTiles());


    }

    void InitPlayerAndTiles()
    {
        autoGenButton.enabled = false;

        //tileMapController.startAutoGenMap();

        int width = 60;
        int height = 25;

        tileMapController.startAutoGenMap(width, height);

        initPlayerLocation = tileMapController.getPos(1, 1);

        Vector2Int start = new Vector2Int(1, 1);

        Vector2Int end = new Vector2Int();

        int dist = -1;
        int hype = Mathf.FloorToInt(Mathf.Sqrt(width * width + height * height));
        while(dist < 0)
        {
            int x = Mathf.FloorToInt(Random.Range(width * 0.1f, width * 0.9f));
            int y = Mathf.FloorToInt(Random.Range(height * 0.1f, height * 0.9f));

            int z = Mathf.FloorToInt(Mathf.Sqrt(x * x + y * y));

            print("x is " + x + " y is " + y + " -> " + z + " " + hype);

            if (z > (hype * 0.5))
                dist = z;

            end = new Vector2Int(x, y);
        }


        tileMapController.addOther(start.x, start.y, 2);
        tileMapController.addOther(end.x, end.y, 2);
        
        tileMapController.checkPath(start, end);

        tileMapController.makeSolvable(start, end);

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
