using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private MeshRenderer mesh;
    private bool cast = false;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        mesh.enabled = cast;

        float distance = Vector3.Distance
        (
            transform.position,
            player.transform.position
        );

        if (cast)
        {

        }
        else
        {

        }
    }

    public bool GetCast()
    {
        return this.cast;
    }

    public void SetCast(bool cast)
    {
        this.cast = cast;
    }
}
