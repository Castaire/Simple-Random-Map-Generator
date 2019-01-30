using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// USAGE: 	emits rings of light
public class lightPulse : MonoBehaviour 
{

	private GameObject pulseObject;
	private ParticleSystem pulseSystem;

	void Start()
	{
		Debug.Log("added lightpulse to " + gameObject.name);
		Debug.Log("USAGE: Press Q to emit 1 pulse");

		GameObject p = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Lighting/Prefabs/pulse_ps.prefab", 
						typeof(GameObject));
		pulseObject = Instantiate(p, gameObject.transform.position, Quaternion.identity, gameObject.transform);
		pulseSystem = pulseObject.GetComponent<ParticleSystem>();
	}

	void Update()
	{	
		if(Input.GetKeyUp(KeyCode.Q)){
			pulseSystem.Emit(1);
		}
	}
}