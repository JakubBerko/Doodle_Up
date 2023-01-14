using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    Rigidbody2D rb;
    float dx; //smìr a rychlost pohybu na x
    float moveSpeed = 20f; //rychlost pohybu

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //získá RB od Doodlera
    }
    void Update()
    {
        //pohyb telefonu * moveSpeed = rychlost  pohybu na daný smìr
        dx = Input.acceleration.x * moveSpeed;
    }
    void FixedUpdate()
    {
        //pøevedení rychlosti na ose x na rigidbody Doodlera
        Vector2 velocity = rb.velocity; 
        velocity.x = dx;
        rb.velocity = velocity;
    }
}
