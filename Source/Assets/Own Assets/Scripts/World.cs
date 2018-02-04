using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    private float top = 0.0f;
    [SerializeField]
    private float bottom = 0.0f;
    [SerializeField]
    private float left = 0.0f;
    [SerializeField]
    private float right = 0.0f;
    [SerializeField]
    private Range spawnNumbers;
    [SerializeField]
    private GameObject[] fishes;

    private int numberOfFish;

    void Start()
    {
        Debug.Assert
        (
            spawnNumbers.max > spawnNumbers.min,
            "Fish range incorrect"
        );

        numberOfFish = Mathf.RoundToInt
        (
            Random.Range(spawnNumbers.min, spawnNumbers.max)
        );

        // Spawn a bunch of random fish
        for (int i = 0; i < numberOfFish; i++)
        {
            Vector3 pos = new Vector3
            (
                Random.Range(bottom, top),
                -1.0f,
                Random.Range(left, right)
            );

            Quaternion rot = new Quaternion
            (
                0,
                Random.Range(0, 360),
                0,
                0
            );

            Instantiate
            (
                fishes[Mathf.RoundToInt(Random.Range(0, fishes.Length))],
                pos,
                rot,
                this.transform
            );
        }
    }

    void Update()
    {

    }
}
