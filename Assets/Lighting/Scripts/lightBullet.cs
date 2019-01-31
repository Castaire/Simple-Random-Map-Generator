using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// USAGE: 	emits a light projectile in the direction the sprite is facing
public class lightBullet : MonoBehaviour
{

	private float lifetime = 5.0f;                 // bullet lifetime in seconds
	private Color color = new Color(0, 34, 255);   // blue
	private float speed = 40.0f;

    // TESTING ONLY:
    //GameObject bbb;


    void Start()
    {
    	Debug.Log("added lightBullet to " + gameObject.name);
		Debug.Log("USAGE: Press E to fire 1 light bullet");
    }

    
    void Update()
    {   
        if(Input.GetKeyUp(KeyCode.E)){
            fireBullet();
        }
    }

    public void fireBullet(){
        // get prefab
    	GameObject b = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Lighting/Prefabs/light_bullet.prefab",
    					typeof(GameObject));

        // instantiate prefab
 		Vector3 pos = new Vector3(transform.position.x, transform.position.y, -1);
    	GameObject bullet = Instantiate(b, pos, Quaternion.identity);

        // TESTING ONLY: 
        //bbb = bullet;

        // move bullet
        Rigidbody2D bulletRB = bullet.AddComponent<Rigidbody2D>();
        bulletRB.gravityScale = 0;
        bulletRB.drag = 0;

        bulletRB.velocity = new Vector2(transform.position.x, transform.position.y);
        //bulletRB.angularVelocity = speed;

        Debug.Log(bullet.transform);

        // destroy once lifetime is over
        Destroy(bullet, lifetime);
    }
}
