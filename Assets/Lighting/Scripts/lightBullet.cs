using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// USAGE: 	emits a light projectile in the direction the sprite is facing
public class lightBullet : MonoBehaviour
{

	private float lifetime = 10.0f;
	private Color color = new Color(0, 34, 255); // blue
	private float speed = 15.0f;

    void Start()
    {
    	Debug.Log("added lightBullet to " + gameObject.name);
		Debug.Log("USAGE: Press 'e' to fire 1 light bullet");
    }

    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E)){
        	fireBullet(gameObject.transform.eulerAngles);
        }
    }

    public void fireBullet(Vector3 angle){
    	Debug.Log("firing bullet!");

    	GameObject b = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Lighting/Prefabs/light_bullet.prefab",
    					typeof(GameObject));
 		Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    	GameObject bullet = Instantiate(b, pos, Quaternion.identity);

    	// TODO: add rigidbody to bullet !!!
    	// move bullet


        Destroy(bullet, lifetime);
    }
}
