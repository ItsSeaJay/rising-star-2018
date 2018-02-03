using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Species : MonoBehaviour
{
    [SerializeField]
    // Thanks for forcing me to write 
    // this variable name in French, Unity!
    private string nom = "Dopefish";
    [SerializeField]
    private string description;
    [SerializeField]
    private float biteTime = 1.0f;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float acceleration = 0.33f;
    [SerializeField]
    [Tooltip("How many times the fish will approach the lure before biting.")]
    private Range nibbles;

    public string GetName()
    {
        return this.nom;
    }

    public float GetBiteTime()
    {
        return this.biteTime;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    public float GetAcceleration()
    {
        return this.acceleration;
    }

    public float GetNibbles()
    {
        return this.nibbles.current;
    }

    public void SetNibbles(float nibbles)
    {
        this.nibbles.current = nibbles;
    }
}
