using System.Collections;
using System.Collections.Generic;

using UnityEngine;


// USAGE: 	extremely simple sprite-movement controller
public class MiniSpriteController : MonoBehaviour
{
	private Vector3 initLocation;
	private float speed = 200.0f;

    void Update() {
        var spriteMove = new Vector3(Input.GetAxis("Horizontal"),
                                 Input.GetAxis("Vertical"), 0);
        //transform.position += spriteMove * speed * Time.deltaTime;

        if (Input.GetAxisRaw("Horizontal") == 0 &&
            Input.GetAxisRaw("Vertical") == 0)
        {
            Vector2 actualVelocity = GetComponent<Rigidbody2D>().velocity;
            float brake = 0.01f;
            GetComponent<Rigidbody2D>().velocity = new Vector2(brake * actualVelocity.x, brake * actualVelocity.y);
            print("reset velocity");
        }
        GetComponent<Rigidbody2D>().velocity = spriteMove * speed * Time.deltaTime;
        //GetComponent<Rigidbody2D>().AddForce(spriteMove * speed * Time.deltaTime);

        //print("current position" + transform.position);
    }
}