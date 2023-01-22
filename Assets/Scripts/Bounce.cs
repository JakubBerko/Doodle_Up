using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //velocity = rychlost objektu ur�it�m sm�rem
    private float vel = 9f; //pokud zm�n�no zde, zm�nit i v Ghost platform

    void OnCollisionEnter2D(Collision2D collision)
    {
        //pokud nemaj� objekty velocity sm�rem nahoru, tak se odraz� doodler sm�rem nahoru
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
}
