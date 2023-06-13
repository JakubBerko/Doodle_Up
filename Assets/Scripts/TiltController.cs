using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    //screenWrap
    private float halfHeight; 
    private float halfWidth;

    Rigidbody2D rb;
    float dx; //smìr a rychlost pohybu na x
    float moveSpeed = 20f; //rychlost pohybu

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //získá RB od Doodlera
    }
    void Start()
    {
        //screenWrap (aka. co kamera vidí)
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }
    void Update()
    {
        //pohyb telefonu * moveSpeed = rychlost  pohybu na daný smìr
        dx = Input.acceleration.x * moveSpeed;

        //screenWrap 
        if (transform.position.x > halfWidth) //pokud je pozice hráèe na x vìtší než pozice kamery napravo
        {
            transform.position = new Vector3(-halfWidth, transform.position.y, transform.position.z); //objeví hráèe na opaèné stranì (-halfWidth) a ponechá výšku a z
        }
        else if (transform.position.x < -halfWidth) //pokud je pozice hráèe na x vìtší než pozice kamery nalevo
        {
            transform.position = new Vector3(halfWidth, transform.position.y, transform.position.z); //objeví hráèe na opaèné stranì (halfWidth) a ponechá výšku a z
        }

    }
    void FixedUpdate()
    {
        //pøevedení rychlosti na ose x na rigidbody Doodlera
        Vector2 velocity = rb.velocity; 
        velocity.x = dx;
        rb.velocity = velocity;
    }
}
