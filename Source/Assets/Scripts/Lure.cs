using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private static Lure instance;

    private MeshRenderer mesh;
    private bool cast = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

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
    }

    void Catch()
    {

    }

    public static Lure GetInstance()
    {
        return instance;
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
