﻿using System.Collections;
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
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private int nibbles = 1;

    private State state = State.Normal;
    private Lure lure;

	void Start ()
    {
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
            case State.Hooked:
                if (Input.GetMouseButtonDown(0))
                {
                    gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }

    protected void Swim()
    {
        transform.Translate(0, 0, 1 * speed * Time.deltaTime);
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

            if (angle < radius)
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
                state = State.Hooked;
            }
        }
    }
}
