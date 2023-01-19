using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public float xMin = -0.5f;
    public float xMax = 0.5f;
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, xMax - xMin) + xMin, transform.position.y, transform.position.z);
    }
}
