using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOffsetFromPlayer : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private OffsetScroller offsetScroller;

    // Update is called once per frame
    void Update ()
    {
        offsetScroller.setVelocity(player.getVelocity());
	}
}
