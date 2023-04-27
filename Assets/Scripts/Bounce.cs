using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //velocity = rychlost objektu urèitým smìrem
    public float vel = 9f; //pokud zmìnìno zde, zmìnit i v Ghost platform
    Rigidbody2D rb;
    Vector2 velocity;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y > 0f) return;
        rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;
        //pokud nemají objekty velocity smìrem nahoru, tak se odrazí doodler smìrem nahoru
        rb.position.Set(rb.position.x, rb.position.y + 100);
        velocity = rb.velocity;
        velocity.y = vel;
        rb.velocity = velocity;
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
    }
}
