using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThrough : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) //pozn. nedokonalý a nepoužívaný kod 
    {
        if (collision.relativeVelocity.y <= 0f)//pøi kolizi znièí objekt pokud najde RB Doodlera
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
