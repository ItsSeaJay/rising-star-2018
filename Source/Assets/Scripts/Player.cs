using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 4.0f;
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
        Translate();
    }

    private void Steer()
    {
        // Turn based on the chosen input axis
        direction += Input.GetAxis(steeringAxis) * torque;

        float radians = direction * Mathf.Deg2Rad;

        // Update the velocity
        velocity.x = Mathf.Sin(radians) * currentSpeed;
        velocity.y = Mathf.Cos(radians) * currentSpeed;

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

    private void Translate()
    {
        transform.Translate
        (
            velocity.x * Time.deltaTime,
            0,
            velocity.y * Time.deltaTime
        );
    }

    public Vector2 getVelocity()
    {
        return velocity;
    }
}
