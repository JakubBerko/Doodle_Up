using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    //screenWrap
    private float halfHeight; 
    private float halfWidth;

    Rigidbody2D rb;
    float dx; //sm�r a rychlost pohybu na x
    float moveSpeed = 20f; //rychlost pohybu

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //z�sk� RB od Doodlera
    }
    void Start()
    {
        //screenWrap (aka. co kamera vid�)
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }
    void Update()
    {
        //pohyb telefonu * moveSpeed = rychlost  pohybu na dan� sm�r
        dx = Input.acceleration.x * moveSpeed;

        //screenWrap 
        if (transform.position.x > halfWidth) //pokud je pozice hr��e na x v�t�� ne� pozice kamery napravo
        {
            transform.position = new Vector3(-halfWidth, transform.position.y, transform.position.z); //objev� hr��e na opa�n� stran� (-halfWidth) a ponech� v��ku a z
        }
        else if (transform.position.x < -halfWidth) //pokud je pozice hr��e na x v�t�� ne� pozice kamery nalevo
        {
            transform.position = new Vector3(halfWidth, transform.position.y, transform.position.z); //objev� hr��e na opa�n� stran� (halfWidth) a ponech� v��ku a z
        }

    }
    void FixedUpdate()
    {
        //p�eveden� rychlosti na ose x na rigidbody Doodlera
        Vector2 velocity = rb.velocity; 
        velocity.x = dx;
        rb.velocity = velocity;
    }
}
