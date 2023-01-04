using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThrough : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
