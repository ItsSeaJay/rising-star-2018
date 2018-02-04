using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    [SerializeField]
    private string axis = "Mouse ScrollWheel";
    [SerializeField]
    private bool invert = true;
    [SerializeField]
    private float sensitivity = 16.0f;
    [SerializeField]
    private Range zoom;

    private Camera lens;
    
	void Start ()
    {
        Debug.Assert(zoom.max > zoom.min);

        lens = GetComponent<Camera>();
	}
	
	void Update ()
    {
        float fieldOfView = lens.fieldOfView;

        if (!invert)
        {
            fieldOfView += Input.GetAxis(axis) * sensitivity;
            fieldOfView = Mathf.Clamp(fieldOfView, zoom.min, zoom.max);
        }
        else
        {
            fieldOfView += -Input.GetAxis(axis) * sensitivity;
            fieldOfView = Mathf.Clamp(fieldOfView, zoom.min, zoom.max);
        }

        lens.fieldOfView = fieldOfView;
	}
}
