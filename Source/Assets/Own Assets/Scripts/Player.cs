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
    private string steeringAxis = "Horizontal";
    [SerializeField]
    private string accelerationAxis = "Vertical";
    [SerializeField]
    private string fishingButton = "Fire1";
    [SerializeField]
    private Lure lure;

    private List<Fish> hold;
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

        if (Input.GetButtonDown(fishingButton))
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
        // Don't allow the player to reverse
        currentSpeed = Mathf.Max(Input.GetAxis(accelerationAxis), 0) * maxSpeed;
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
        if (Lure.GetInstance().GetHooked())
        {
            Lure.GetInstance().GetFish().gameObject.SetActive(false);
        }
        
        lure.SetCast(false);
        lure.transform.position = transform.position;
    }

    private void DebugHold()
    {
        foreach (Fish fish in hold)
        {
            Debug.Log(fish.GetSpecies());
        }
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }
}
