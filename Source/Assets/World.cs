using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    private Player player;

    void Update()
    {
        Vector3 velocity = new Vector3
        (
            player.getVelocity().x * Time.deltaTime,
            0,
            player.getVelocity().y * Time.deltaTime
        );

        transform.Translate(velocity);
    }
}
