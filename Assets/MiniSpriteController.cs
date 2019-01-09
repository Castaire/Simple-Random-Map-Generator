using System.Collections;
using System.Collections.Generic;

using UnityEngine;


// USAGE: 	extremely simple sprite-movement controller
public class MiniSpriteController : MonoBehaviour
{
	private Vector3 initLocation;
	private float speed = 10.0f;

	void Update(){
		var spriteMove = new Vector3(Input.GetAxis("Horizontal"), 
								 Input.GetAxis("Vertical"), 0);
		transform.position += spriteMove * speed * Time.deltaTime;
	}
}