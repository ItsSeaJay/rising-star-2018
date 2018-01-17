using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void LookAtXZ(this Transform transform, Transform target)
    {
        LookAtXZ(transform, target.position);
    }

    public static void LookAtXZ(this Transform transform, Vector3 target)
    {
        Vector3 position;

        position = target - transform.position;
        position.y = 0;
        position = position.normalized;

        transform.forward = position;
    }
}
