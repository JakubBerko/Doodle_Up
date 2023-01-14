using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThrough : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) //pozn. nedokonal� a nepou��van� kod 
    {
        if (collision.relativeVelocity.y <= 0f)//p�i kolizi zni�� objekt pokud najde RB Doodlera
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
