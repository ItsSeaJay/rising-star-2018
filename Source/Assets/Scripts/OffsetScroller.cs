using UnityEngine;
using System.Collections;

// This the following script was adapted from the Unity tutorial:
// 2D Scrolling Background
// https://unity3d.com/learn/tutorials/topics/2d-game-creation/2d-scrolling-backgrounds

public class OffsetScroller : MonoBehaviour
{
    [SerializeField]
    private Vector2 velocity;

    private Vector2 offset;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        offset = rend.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        offset.x += velocity.x * Time.deltaTime;
        offset.y += velocity.y * Time.deltaTime;

        rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    public void setVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }
}