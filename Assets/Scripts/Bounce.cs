using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //velocity = rychlost objektu ur�it�m sm�rem
    public float vel = 9f; //pokud zm�n�no zde, zm�nit i v Ghost platform
    Rigidbody2D rb;
    Vector2 velocity;
    Controller controller;
    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Doodler").GetComponent<Controller>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y > 0f) return;
        rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;
        //pokud nemaj� objekty velocity sm�rem nahoru, tak se odraz� doodler sm�rem nahoru
        rb.position.Set(rb.position.x, rb.position.y + 100);
        velocity = rb.velocity;
        velocity.y = vel;
        rb.velocity = velocity;
        controller.jumps++;
        
    }
    private void OnTriggerEnter(Collider col)
    {
        // Zkontroluje zdali collider trefil invincible objekt
        if (col.gameObject.layer != LayerMask.NameToLayer("Invincible")) return;
        rb = col.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;
        Vector2 velocity = rb.velocity;
        velocity.y = vel;
        rb.velocity = velocity;
        controller.jumps++;
    }
}
