using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private static Lure instance;

    private Fish fish;
    private MeshRenderer mesh;
    private bool cast = false;
    private bool hooked = false;

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

    public static Lure GetInstance()
    {
        return instance;
    }

    public Fish GetFish()
    {
        return this.fish;
    }

    public void SetFish(Fish fish)
    {
        this.fish = fish;
        Debug.Log(this.fish.GetSpecies());
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
