using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class lightingMain : MonoBehaviour
{
    
	public GameObject mainSprite;
	private GameObject bouncer;
	private GameObject antenna;
	private GameObject antennaLight;

    void Start()
    {
    	bouncer = mainSprite.transform.Find("bouncer").gameObject;
    	antenna = mainSprite.transform.Find("antenna").gameObject;
    	antennaLight = antenna.transform.Find("antenna_light").gameObject;

    	// add lighting features
    	bouncer.AddComponent<lightPulse>();
    	antennaLight.AddComponent<antennaController>();

    }
}
