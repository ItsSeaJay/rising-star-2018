using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [System.Serializable]
    private struct VisionCone
    {
        public float length;
        public float radius;
    }

    [SerializeField]
    private string species = "Dopefish";
    [SerializeField]
    private VisionCone visionCone;
    [SerializeField]
    private Range recoilTime;
    [SerializeField]
    private float biteTime = 1.0f;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float acceleration = 0.33f;
    [SerializeField]
    [Tooltip("How many times the fish will approach the lure before biting.")]
    private int nibbles = 1;

    private Material material;
    private float velocity = 0;
    private float recoilTimer = 0;
    private float biteTimer = 0;
    private State state = State.Normal;

	void Start ()
    {
        Debug.Assert
        (
            recoilTime.max > recoilTime.min,
            name + "'s recoil time maximum must be greater than the minimum."
        );

        Debug.Assert
        (
            visionCone.length > 0,
            name + "'s vision cone length cannot be negative."
        );

        material = GetComponent<Material>();
	}
	
	void Update ()
    {
        HandleState();
    }

    private void HandleState()
    {
        switch (state)
        {
            case State.Normal:
                Swim();
                SearchForLure();
                break;
            case State.Curious:
                Swim();
                SearchForLure();
                transform.LookAtXZ(Lure.GetInstance().transform);
                break;
            case State.Recoil:
                recoilTimer = Mathf.Max(0, recoilTimer - Time.deltaTime);
                Recoil();
                transform.LookAtXZ(Lure.GetInstance().transform);

                if (recoilTimer == 0)
                {
                    state = State.Curious;
                }
                break;
            case State.Hooked:
                if (Lure.GetInstance().GetFish() == null)
                {
                    Lure.GetInstance().SetFish(this);
                }

                biteTimer = Mathf.Max(0, biteTimer - Time.deltaTime);

                if (biteTimer == 0)
                {
                    // The player failed to catch the fish in time
                    state = State.Dive;
                }
                break;
            case State.Dive:
                transform.Translate(0, -1 * Time.deltaTime, 0);
                break;
            default:
                Debug.LogError
                (
                    species + 
                    ' ' + 
                    name + 
                    "'s finite state machine broke!"
                );
                break;
        }
    }

    private void Swim()
    {
        velocity = Mathf.Min(speed, velocity + acceleration * Time.deltaTime);

        transform.Translate(0, 0, 1 * velocity * Time.deltaTime);
    }

    private void Recoil()
    {
        velocity = Mathf.Max(-speed, velocity - acceleration * Time.deltaTime);

        transform.Translate(0, 0, 1 * velocity * Time.deltaTime);
    }

    private void Bite()
    {
        // TODO: Implement biting
    }

    private void Escape()
    {
        // TODO: Implement escaping from the lure
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

    private void OnTriggerEnter(Collider other)
    {
        if (Lure.GetInstance().GetCast())
        {
            if (other.tag == "Hook")
            {
                if (nibbles > 0)
                {
                    state = State.Recoil;
                    velocity = 0;
                    recoilTimer = Random.Range(recoilTime.min, recoilTime.max);
                    --nibbles;
                }
                else
                {
                    state = State.Hooked;
                    biteTimer = biteTime;
                }
            }
        }
    }

    public string GetSpecies()
    {
        return this.species;
    }
}
