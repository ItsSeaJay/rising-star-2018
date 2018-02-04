using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Species))]
public class Fish : MonoBehaviour
{
    private enum State
    {
        Normal,
        Curious,
        Recoil,
        Hooked,
        Dive
    }

    private enum Turn
    {
        Left,
        Right,
        None
    }

    [System.Serializable]
    private struct VisionCone
    {
        public float length;
        public float radius;
    }
    
    [SerializeField]
    private VisionCone visionCone;
    [SerializeField]
    private Range recoilTime;
    [SerializeField]
    private Range swimTime;
    [SerializeField]
    private Range turnTime;

    private Material material;
    private Species species;

    private float velocity = 0;
    private float recoilTimer = 0;
    private float turnTimer = 0;
    private float biteTimer = 0;
    private float swimTimer = 0;
    private float nibbles;
    private bool bitten = false;

    private State state = State.Normal;
    private Turn turning = Turn.None;

    void Start ()
    {
        Debug.Assert
        (
            recoilTime.max > recoilTime.min,
            name + "'s recoil time maximum must be greater than the minimum."
        );

        Debug.Assert
        (
            turnTime.max > turnTime.min,
            name + "'s turn time maximum must be greater than the minimum."
        );

        Debug.Assert
        (
            swimTime.max > swimTime.min,
            name + "'s swim time maximum must be greater than the minimum."
        );

        Debug.Assert
        (
            visionCone.length > 0,
            name + "'s vision cone length cannot be negative."
        );

        species = GetComponent<Species>();
        material = GetComponent<Renderer>().material;

        transform.Rotate(transform.up, Random.Range(0.0f, 360.0f));

        nibbles = species.GetNibbles().max;
        turnTimer = turnTime.max;
        swimTimer = Random.Range(swimTime.min, swimTime.max);
	}
	
	void Update ()
    {
        HandleState();
    }

    private void HandleState()
    {
        Debug.Log(state.ToString());

        switch (state)
        {
            case State.Normal:
                swimTimer = Mathf.Max(0, swimTimer - Time.deltaTime);

                Swim();
                SearchForLure();
                TurnRandomly();
                break;
            case State.Curious:
                turning = Turn.None;

                Swim();
                SearchForLure();
                transform.LookAtXZ(Lure.GetInstance().transform);
                break;
            case State.Recoil:
                turning = Turn.None;

                recoilTimer = Mathf.Max(0, recoilTimer - Time.deltaTime);
                Recoil();
                transform.LookAtXZ(Lure.GetInstance().transform);

                if (recoilTimer == 0)
                {
                    state = State.Curious;
                }
                break;
            case State.Hooked:
                turning = Turn.None;

                biteTimer = Mathf.Max(0, biteTimer - Time.deltaTime);

                if (biteTimer == 0)
                {
                    // The player failed to catch the fish in time
                    state = State.Dive;
                }
                break;
            case State.Dive:
                Color faded = new Color(0, 0, 0, 0);

                if (Lure.GetInstance().GetHooked())
                {
                    Lure.GetInstance().SetHooked(false);
                }

                transform.Translate(0, -1 * Time.deltaTime, species.GetSpeed() * Time.deltaTime);

                material.color = Color.Lerp(material.color, faded, Time.deltaTime);

                if (material.color.a <= 0.1f)
                {
                    gameObject.SetActive(false);
                }
                break;
            default:
                Debug.LogError
                (
                    species.GetName() + 
                    ' ' + 
                    GetInstanceID().ToString() + 
                    "'s finite state machine broke!"
                );
                break;
        }
    }

    private void Swim()
    {
        velocity = Mathf.Min
        (
            species.GetSpeed(),
            velocity + species.GetAcceleration() * Time.deltaTime
       );

        transform.Translate(0, 0, 1 * velocity * Time.deltaTime);
    }

    private void Recoil()
    {
        velocity = Mathf.Max
        (
            -species.GetSpeed(),
            velocity - species.GetAcceleration() * Time.deltaTime
        );

        transform.Translate(0, 0, 1 * velocity * Time.deltaTime);
    }

    private void Nibble()
    {
        state = State.Recoil;
        velocity = 0;
        recoilTimer = Random.Range(recoilTime.min, recoilTime.max);
        nibbles = Mathf.Max(nibbles - 1, 0);
    }

    private void Bite()
    {
        state = State.Hooked;
        biteTimer = species.GetBiteTime();

        Lure.GetInstance().SetFish(this);
        Lure.GetInstance().SetHooked(true);

        bitten = true;
    }

    private void SearchForLure()
    {
        if (Lure.GetInstance().GetCast())
        {
            // Calculate vision cone
            Vector3 lurePosition = Lure.GetInstance().transform.position;
            Vector3 targetDirection = lurePosition - transform.position;
            Vector3 forward = transform.forward;

            float angle = Vector3.Angle(targetDirection, forward);
            float distance = Vector3.Distance(transform.position, Lure.GetInstance().transform.position);

            if (angle < visionCone.radius)
            {
                state = State.Curious;
            }
            else
            {
                state = State.Normal;
            }
        }
    }

    private void TurnRandomly()
    {
        if (swimTimer <= 0)
        {
            // Choose a direction
            if (turning == Turn.None)
            {
                turning = GetRandomDirection();
                turnTimer = Random.Range(turnTime.min, turnTime.max);
            }

            // Turn in that direction
            if (turning == Turn.Left)
            {
                transform.Rotate(Vector3.up, -species.GetTorque() * Time.deltaTime);
            }
            else
            {
                //Turning right
                transform.Rotate(Vector3.up, species.GetTorque() * Time.deltaTime);
            }

            // Wait for direction reset
            if (turning != Turn.None)
            {
                turnTimer = Mathf.Max(turnTimer - Time.deltaTime, 0);

                if (turnTimer <= 0)
                {
                    swimTimer = Random.Range(turnTime.min, turnTime.max);
                    turning = Turn.None;
                }
            }
        }
    }

    private Turn GetRandomDirection()
    {
        int go = Mathf.RoundToInt(Random.Range(0, 2));

        if (go == 0)
        {
            return Turn.Left;
        }
        else
        {
            return Turn.Right;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hate");
        Debug.Log(Lure.GetInstance().GetCast());

        if (Lure.GetInstance().GetCast() &&
            state == State.Curious)
        {
            if (other.tag == "Hook")
            {
                if (nibbles > 0)
                {
                    Nibble();
                }
                else
                {
                    if (!bitten)
                    {
                        Bite();
                    }
                }
            }
        }
    }

    public Species GetSpecies()
    {
        return this.species;
    }
}
