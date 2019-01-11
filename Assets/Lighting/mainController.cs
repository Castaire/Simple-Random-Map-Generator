using System.Collections;
using System.Collections.Generic;

using UnityEngine;


// USAGE: 		extremely similar to Assets/MiniSpriteController
public class mainController : MonoBehaviour
{	
	private float rotationSpeed = 5.0f;
	private float speed = 10.0f;

	void Update(){
		Vector3 origPos = transform.position;

		Vector3 spriteMove = new Vector3(Input.GetAxis("Horizontal"), 
								 Input.GetAxis("Vertical"), 0);
		transform.position += spriteMove * speed * Time.deltaTime;

		// 
		Vector3 posDiff = transform.position - origPos;
		float targetAngle = Mathf.Atan(posDiff.y / posDiff.x) * 180 / Mathf.PI + 90;
        if (posDiff.x > 0){
            targetAngle = 180 + targetAngle;
        }
        targetAngle = (int)targetAngle;

        float objAngle = transform.rotation.eulerAngles.z;

        rotate(quickestRotation(objAngle, targetAngle));
	}


	// USAGE: 	ported from Nocturne =.=|||
	public void rotate(float horizontal){
		Vector3 temp = new Vector3(0, 0, -1 * rotationSpeed * horizontal);
        if (horizontal != 0)
            gameObject.transform.Rotate(temp);
        temp = Vector3.zero;
	}


	// find quickest path for thing at angle1 to reach angle2
    // if true, turn clockwise, otherwise turn counterclockwise
    //public static bool quickestRotation1(float angle1, float angle2)
    public float quickestRotation(float angle1, float angle2)
    {
        if (angle1 > 180)
        {
            if (angle2 > angle1 || (angle2 < angle1 - 180))
            	return(-1f);
            else
            	return(1f);
        }
        else
        {
            if (angle2 > angle1 && (angle2 < angle1 + 180))
            	return(-1f);
            else
            	return(1f);
        }
    }

}