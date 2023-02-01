using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject salivaBulletPrefab;
    public Transform bulletDirection;
    private bool isPaused;
    void Update()
    {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Shoot();
        }

        if (isPaused)
        {
            return;
        }
    }

    void Shoot()
    {
        GameObject salivaBullet = Instantiate(salivaBulletPrefab, bulletDirection.position, bulletDirection.rotation);
        Rigidbody2D rb = salivaBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletDirection.up * 1000f);
    }
}


