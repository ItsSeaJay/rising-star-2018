using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float sight = 4.0f;
    [SerializeField]
    private float radius = 16.0f;

    private bool curious = false;
    private Transform bobber;

	void Start ()
    {
        bobber = GameObject.Find("Bobber").transform;
	}
	
	void Update ()
    {
        Swim();
        SearchForLure();

        if (curious)
        {
            print("George");
        }
	}

    private void Swim()
    {
        transform.Translate(0, 0, 1 * Time.deltaTime);
    }

    private void SearchForLure()
    {
        // Calculate vision cone
        Vector3 targetDir = bobber.position - transform.position;
        Vector3 forward = transform.forward;

        float angle = Vector3.Angle(targetDir, forward);
        float distance = Vector3.Distance(transform.position, bobber.position);

        if (angle < radius && distance < sight)
        {
            curious = true;
        }
        else
        {
            curious = false;
        }
    }
}
