using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private enum State
    {
        Normal,
        Curious,
        Hooked
    }

    [SerializeField]
    private float sight = 4.0f;
    [SerializeField]
    private float radius = 16.0f;

    private State state = State.Normal;
    private bool curious = false;
    private Transform lure;

	void Start ()
    {
        lure = GameObject.Find("Lure").transform;
	}
	
	void Update ()
    {
        switch (state)
        {
            case State.Normal:
                Swim();
                SearchForLure();
                break;
            case State.Curious:
                SearchForLure();
                break;
            case State.Hooked:
                break;
            default:
                break;
        }
    }

    private void Swim()
    {
        transform.Translate(0, 0, 1 * Time.deltaTime);
    }

    private void SearchForLure()
    {
        // Calculate vision cone
        Vector3 targetDir = lure.position - transform.position;
        Vector3 forward = transform.forward;

        float angle = Vector3.Angle(targetDir, forward);
        float distance = Vector3.Distance(transform.position, lure.position);

        if (angle < radius && distance < sight)
        {
            state = State.Curious;
        }
        else
        {
            state = State.Normal;
        }
    }
}
