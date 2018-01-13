using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private const float maxSpeed = 4.0f;
    [SerializeField]
    private float torque = 2.0f;
    [SerializeField]
    private float direction = 0.0f;
    [SerializeField]
    private string steeringAxis = "Horizontal_P1";
    [SerializeField]
    private string accelerationAxis = "Vertical_P1";

    private Vector2 velocity;
    private float currentSpeed = 0.0f;

    void Update()
    {
        Steer();
        Accelerate();
    }

    private void Steer()
    {
        // Turn based on the chosen input axis
        direction += Input.GetAxis(steeringAxis) * torque;

        // Prevent direction overflow
        if (direction > 360)
        {
            direction = 0;
        }
        else if (direction < 0)
        {
            direction = 360;
        }

        // Update the velocity
        velocity.x = Mathf.Sin(direction) * currentSpeed;
        velocity.y = Mathf.Cos(direction) * currentSpeed;

        // Apply rotation
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            direction,
            transform.eulerAngles.z
        );
    }

    private void Accelerate()
    {
        currentSpeed = Input.GetAxis(accelerationAxis) * maxSpeed;
    }

    public Vector2 getVelocity()
    {
        return velocity;
    }
}
