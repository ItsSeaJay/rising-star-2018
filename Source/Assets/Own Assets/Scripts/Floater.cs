using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField]
    private float amount = 1.0f;

    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update ()
    {
        float offset = Mathf.Sin(Time.time) * amount;

        transform.position = new Vector3
        (
            transform.position.x,
            transform.position.y + offset,
            transform.position.z
        );
	}
}
