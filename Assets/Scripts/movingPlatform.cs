using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public float speed = 2f; // adjust the platform speed
    public float distance = 2f; // adjust the distance the platform moves

    private Vector3 startPos;
    private Vector3 endPos;
    private float journeyLength;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(distance, 0, 0);
    }

    void Update()
    {
        float pingPongValue = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPos + new Vector3(pingPongValue, 0, 0);
    }
}
