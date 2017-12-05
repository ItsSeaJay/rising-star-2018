using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    private bool smooth = true;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private Transform target;

    void Update ()
    {
        if (smooth)
        {
            // Smoothly rotate towards the target point
            Vector3 positionDifference = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(positionDifference);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(target);
        }
    }
}
