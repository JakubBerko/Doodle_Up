using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    Rigidbody2D rb;
    float dx; //sm�r a rychlost pohybu na x
    float moveSpeed = 20f; //rychlost pohybu

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //z�sk� RB od Doodlera
    }
    void Update()
    {
        //pohyb telefonu * moveSpeed = rychlost  pohybu na dan� sm�r
        dx = Input.acceleration.x * moveSpeed;
    }
    void FixedUpdate()
    {
        //p�eveden� rychlosti na ose x na rigidbody Doodlera
        Vector2 velocity = rb.velocity; 
        velocity.x = dx;
        rb.velocity = velocity;
    }
}
