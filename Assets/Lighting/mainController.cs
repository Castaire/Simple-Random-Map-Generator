using System.Collections;
using System.Collections.Generic;

using UnityEngine;


// USAGE: 		sprite simple movement and rotation
public class mainController : MonoBehaviour
{	

	private float rotationSpeed = 4.0f;
	private float speed = 10.0f;


	void Update(){
		Vector3 origPos = transform.position;

        // movement
		Vector3 spriteMove = new Vector3(Input.GetAxis("Horizontal"), 
								 Input.GetAxis("Vertical"), 0);
        if (spriteMove == Vector3.zero)
            return;

		transform.position += spriteMove * speed * Time.deltaTime;

        // rotation
		Vector3 posDiff = origPos - transform.position;
		float targetAngle = Mathf.Atan(posDiff.y / posDiff.x) * 180 / Mathf.PI + 90;
        if (posDiff.x < 0){
            targetAngle = 180 + targetAngle;
        }
        targetAngle = (int)targetAngle;

        float currentAngle = transform.rotation.eulerAngles.z;

        rotate(quickestRotation(currentAngle, targetAngle));
	}


	public void rotate(float horizontal){
		Vector3 temp = new Vector3(0, 0, -1 * rotationSpeed * horizontal);
        if (horizontal != 0)
            gameObject.transform.Rotate(temp);
        temp = Vector3.zero;
	}


    // USAGE:   finds quickest past between angle1 and angle
    //          returns 1 for clockwise rotation, -1f for counterclockwise
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