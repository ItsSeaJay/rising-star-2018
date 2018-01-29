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
    private struct RecoilTime
    {
        public float min;
        public float max;
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
    private RecoilTime recoilTime;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float acceleration = 0.33f;
    [SerializeField]
    private int nibbles = 1;

    private float velocity = 0;
    private float recoilTimer = 0;
    private State state = State.Normal;
    private Lure lure;

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

        lure = GameObject.Find("Lure").GetComponent<Lure>();
	}
	
	void Update ()
    {
        HandleState();
    }

    protected virtual void HandleState()
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
                transform.LookAtXZ(lure.transform);
                break;
            case State.Recoil:
                recoilTimer = Mathf.Max(0, recoilTimer - Time.deltaTime);
                Recoil();
                transform.LookAtXZ(lure.transform);

                if (recoilTimer == 0)
                {
                    state = State.Curious;
                }
                break;
            case State.Hooked:
                if (Input.GetMouseButtonDown(0))
                {
                    gameObject.SetActive(false);
                }
                break;
            case State.Dive:

                break;
            default:
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

    private void SearchForLure()
    {
        if (lure.GetCast())
        {
            // Calculate vision cone
            Vector3 targetDir = lure.transform.position - transform.position;
            Vector3 forward = transform.forward;

            float angle = Vector3.Angle(targetDir, forward);
            float distance = Vector3.Distance(transform.position, lure.transform.position);

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
        if (lure.GetCast())
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
                }
            }
        }
    }
}
