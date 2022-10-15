using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    Rigidbody2D rb;
    float dx;
    float moveSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dx = Input.acceleration.x * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(dx, 0f);
    }
}
