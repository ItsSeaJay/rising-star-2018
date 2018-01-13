using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;
    [SerializeField]
    private float torque = 2.0f;
    [SerializeField]
    private float direction = 0.0f;
    [SerializeField]
    private string steeringAxis = "Horizontal_P1";

    private Vector2 velocity;

    void Update()
    {
        Steer();
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
        velocity.x = Mathf.Sin(direction);
        velocity.y = Mathf.Cos(direction);

        // Apply rotation
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            direction,
            transform.eulerAngles.z
        );
    }
}
