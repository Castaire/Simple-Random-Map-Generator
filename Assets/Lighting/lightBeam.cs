using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// USAGE: 	light beam feature (light type = spot light)
public class lightBeam : MonoBehaviour
{

	// testing variables; privatize later
	public float lightIntensity; 	  	// 0 to 8 value
	public float lightRange;
	public float lightAngle;
	public float lightShadowStrength; 	// 0 to 1 range

	private Color color = Color.blue;

    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("just added lightBeam to sprite " + gameObject.name);    

    	Light lightComp = gameObject.AddComponent<Light>();

    	lightComp.type = LightType.Spot;
    	lightComp.cullingMask = 1;

    	lightComp.intensity = lightIntensity;
    	lightComp.range = lightRange;
    	lightComp.spotAngle = lightAngle;
    	lightComp.shadowStrength = lightShadowStrength;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
