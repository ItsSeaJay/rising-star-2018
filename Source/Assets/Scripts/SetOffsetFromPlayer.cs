using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OffsetScroller))]
public class SetOffsetFromPlayer : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private OffsetScroller offsetScroller;

    void Start()
    {
        Debug.Assert(player != null, "Player value cannot be null!");
        offsetScroller = GetComponent<OffsetScroller>();
    }
    
    void Update ()
    {
        offsetScroller.setVelocity(-player.GetVelocity() / 2);
	}
}
