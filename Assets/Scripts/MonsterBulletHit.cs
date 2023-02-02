using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletHit : MonoBehaviour
{
    private GameObject[] bubbleBullets;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bubbleBullets = GameObject.FindGameObjectsWithTag("BubbleBullet");
        for (int i = 0; i < bubbleBullets.Length; i++)
        {
            if (collision.gameObject.tag == "Doodler")
            {
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "SalivaBullet")
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
