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
    [SerializeField]
    private Lure lure;
    
    private Vector2 velocity;
    private float currentSpeed = 0.0f;
    private float range = 64.0f;

    void Start()
    {
        ApplyDirection();
    }

    void Update()
    {
        Steer();
        Accelerate();
        ApplyDirection();
        Translate();

        if (Input.GetMouseButtonDown(0))
        {
            if (!lure.GetCast())
            {
                Cast();
            }
            else
            {
                Reel();
            }
        }
    }

    private void Steer()
    {
        // Turn based on the chosen input axis
        direction += Input.GetAxis(steeringAxis) * torque;

        float radians = direction * Mathf.Deg2Rad;

        // Update the velocity
        velocity.x = Mathf.Sin(radians) * currentSpeed;
        velocity.y = Mathf.Cos(radians) * currentSpeed;
    }

    private void Accelerate()
    {
        currentSpeed = Input.GetAxis(accelerationAxis) * maxSpeed;
    }

    private void ApplyDirection()
    {
        transform.eulerAngles = new Vector3
        (
            transform.eulerAngles.x,
            direction,
            transform.eulerAngles.z
        );
    }

    private void Translate()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void Cast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Vector3 hitPosition = hit.point;
            lure.transform.position = hitPosition;

            lure.SetCast(true);
        }
    }

    private void Reel()
    {
        lure.SetCast(false);
        lure.transform.position = transform.position;
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }
}
