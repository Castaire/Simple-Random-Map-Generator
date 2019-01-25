using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// USAGE: 	use this class to control antenna-light behaviour
public class antennaController : MonoBehaviour
{

	private Light antennaLight;

    void Start()
    {
    	Debug.Log("added antennaController to " + gameObject.name);
		Debug.Log("USAGE: press X to decrease light intensity");
		Debug.Log("USAGE: press C to increase light intensity");

		antennaLight = gameObject.GetComponent<Light>();
    }
    
    void Update()
    {
		if(Input.GetKeyUp(KeyCode.X)){
			if(antennaLight.range >= 7){
				antennaLight.range = antennaLight.range - 2;
			}

		}else if(Input.GetKeyUp(KeyCode.C)){
			if(antennaLight.range <= 13){
				antennaLight.range = antennaLight.range + 2;
			}
		}
    }


}
