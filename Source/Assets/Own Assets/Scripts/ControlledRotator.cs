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
    [SerializeField]
    private float delay = 1.0f;
	
	void Update ()
    {
        if (!smooth)
        {
            RotateDirect();
        }
        else
        {
            RotateSmooth();
        }
	}

    void RotateDirect()
    {
        float input = Input.GetAxis(axis);
        Vector3 currentAngle = transform.localEulerAngles;

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

    void RotateSmooth()
    {
        float input = Input.GetAxis(axis);
        Vector3 currentAngle = transform.localEulerAngles;
        Vector3 targetAngle = transform.localEulerAngles;

        if (rotateX && !invertX)
        {
            targetAngle.x = input * amount.x;
        }
        else if (rotateX && invertX)
        {
            targetAngle.x = -input * amount.x;
        }

        if (rotateY && !invertY)
        {
            targetAngle.y = input * amount.y;
        }
        else if (rotateY && invertY)
        {
            targetAngle.y = -input * amount.y;
        }

        if (rotateZ && !invertZ)
        {
            targetAngle.z = input * amount.z;
        }
        else if (rotateZ && invertZ)
        {
            targetAngle.z = -input * amount.z;
        }

        currentAngle = Vector3.Lerp(currentAngle, targetAngle, delay);
        transform.localEulerAngles = currentAngle;
    }
}
