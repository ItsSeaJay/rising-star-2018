using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledRotator : MonoBehaviour
{
    [SerializeField]
    private string axis = "Horizontal";
    [SerializeField]
    private Vector3 amount;
    [SerializeField]
    private bool rotateX, rotateY, rotateZ;
    [SerializeField]
    private bool invertX, invertY, invertZ;
    [SerializeField]
    private bool smooth = false;

    private Vector3 startingAngle;
    
	void Start ()
    {
        startingAngle = transform.localEulerAngles;
	}
	
	void Update ()
    {
        if (!smooth)
        {
            RotateDirect();
        }
	}

    void RotateDirect()
    {
        float input = Input.GetAxis(axis);
        Vector3 currentAngle = startingAngle;

        if (rotateX && !invertX)
        {
            currentAngle.x = input * amount.x;
        }
        else if (rotateX && invertX)
        {
            currentAngle.x = -input * amount.x;
        }

        if (rotateY && !invertY)
        {
            currentAngle.y = input * amount.y;
        }
        else if (rotateY && invertY)
        {
            currentAngle.y = -input * amount.y;
        }

        if (rotateZ && !invertZ)
        {
            currentAngle.z = input * amount.z;
        }
        else if (rotateZ && invertZ)
        {
            currentAngle.z = -input * amount.z;
        }

        transform.localEulerAngles = currentAngle;
    }
}
