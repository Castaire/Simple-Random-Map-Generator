using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class lightingMain : MonoBehaviour
{
    
	public GameObject mainSprite;
	private GameObject bouncer;
	private GameObject antenna;

    void Start()
    {

    	bouncer = mainSprite.transform.Find("bouncer").gameObject;
    	antenna = mainSprite.transform.Find("antenna").gameObject;

    	// add lighting features
    	bouncer.AddComponent<lightPulse>();

    }
}
