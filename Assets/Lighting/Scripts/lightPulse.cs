using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class lightPulse : MonoBehaviour 
{

	private GameObject pulseObject;
	private ParticleSystem pulseSystem;



	void Start(){
		Debug.Log("added lightpulse to " + gameObject.name);

		pulseObject = new GameObject("pulseObject");
		pulseSystem = pulseObject.AddComponent<ParticleSystem>();
		setupPulseSystem();
	}

	void Update()
	{	
		if(Input.GetKeyUp(KeyCode.P)){
			emitPulse(gameObject.transform.position);
		}
	}

	private void setupPulseSystem(){
		Debug.Log("setting up pulse system");

		// setup main module



	}

	public void emitPulse(Vector3 pos){
		Debug.Log("emitting pulse!");


	}
}