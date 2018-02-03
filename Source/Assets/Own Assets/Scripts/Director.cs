using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField]
    private Transform[] cameras;
    [SerializeField]
    private Transform player;

    private Dictionary<string, float> distances;

    void Start ()
    {
        Debug.Assert(cameras.Length > 0);
        Debug.Assert(player != null);

        RefreshCameraList();
    }

    void Update ()
    {
        foreach (Transform camera in cameras)
        {
            //float distance = Vector3.Distance(camera.position, player.position);
        }
    }

    private void RefreshCameraList()
    {
        distances.Clear();

        foreach (Transform camera in cameras)
        {
            distances.Add(camera.GetInstanceID().ToString(), 0.0f);
        }
    }
}
