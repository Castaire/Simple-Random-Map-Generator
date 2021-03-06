using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// USAGE: 	primitive attempt to follow sprite with camera
public class CameraController : MonoBehaviour
{

	private GameObject sprite;
	private Vector3 spriteOffsetToCamera;

	void Start(){
		sprite = GameObject.Find("Player");
		spriteOffsetToCamera.Set(0, 0, -10);
	}

	// NOTE:  calls AFTER Update() per frame
	void LateUpdate()
    {
        sprite = GameObject.Find("Player"); 		// wait, didn't we already do this in Start() ???
        if(sprite != null)
            transform.position = sprite.transform.position + spriteOffsetToCamera;
    }


}