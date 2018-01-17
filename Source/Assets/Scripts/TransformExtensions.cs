using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExt
{
  public static void LookAtXZ(this Transform transform, Transform target)
  {
    LookAtXZ(transform, target.position);
  }

  public static void LookAtXZ(this Transform transform, Vector3 target)
  {
    Vector3 p;

    p = target - transform.position;
    p = p.normalized;

    transform.forward = p;
  }
}
