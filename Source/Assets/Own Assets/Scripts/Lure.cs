using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private MeshRenderer bobberMesh, hookMesh;

    private static Lure instance;

    private Fish fish;
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

    void Update()
    {
        bobberMesh.enabled = cast;
        hookMesh.enabled = cast;

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
    }

    public bool GetCast()
    {
        return this.cast;
    }

    public void SetCast(bool cast)
    {
        this.cast = cast;
    }

    public bool GetHooked()
    {
        return this.hooked;
    }

    public void SetHooked(bool hooked)
    {
        this.hooked = hooked;
    }
}
