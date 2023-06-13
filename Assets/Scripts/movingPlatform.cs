using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public float speed = 2f; //rychlost pohybu
    public float distance = 2f; //vzdalenost

    private Vector3 startPos;
    private Vector3 endPos;
    private float journeyLength; 

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(distance, 0, 0); //konecna vzdalenost je rovna zacatecni + vzdalenosti
    }

    void Update()
    {
        float pingPongValue = Mathf.PingPong(Time.time * speed, distance); //pingpong: sem a tam
        transform.position = startPos + new Vector3(pingPongValue, 0, 0); //zmena pozice
    }
}
