using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    Rigidbody2D rb;
    float dx;
    float moveSpeed = 20f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dx = Input.acceleration.x * moveSpeed;
    }
    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = dx;
        rb.velocity = velocity;
    }
}
