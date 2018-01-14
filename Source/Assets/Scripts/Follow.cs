using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Vector3 offset;

    void Start()
    {
        offset = target.position + transform.position;
    }

    void Update ()
    {
        transform.position = target.position + offset;
	}
}
