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
		spriteOffsetToCamera = transform.position - sprite.transform.position;
	}

	// NOTE:  calls AFTER Update() per frame
	void LateUpdate(){
		transform.position = sprite.transform.position + spriteOffsetToCamera;
	}


}