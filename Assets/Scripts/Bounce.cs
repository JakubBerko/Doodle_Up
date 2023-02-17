using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //velocity = rychlost objektu urèitým smìrem
    public float vel = 9f; //pokud zmìnìno zde, zmìnit i v Ghost platform

    void OnCollisionEnter2D(Collision2D collision)
    {
        //pokud nemají objekty velocity smìrem nahoru, tak se odrazí doodler smìrem nahoru
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = vel;
                rb.velocity = velocity;
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        // Check if the collider that entered the trigger is the invincible object
        if (col.gameObject.layer == LayerMask.NameToLayer("Invincible"))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = vel;
                rb.velocity = velocity;
            }
        }
    }
}
