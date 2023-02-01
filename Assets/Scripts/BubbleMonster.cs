using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMonster : MonoBehaviour
{
    private float lastShotTime;
    public float ShootingInterval = 2f;
    public GameObject bubbleBulletPrefab;
    private Transform playerTrans;

    void Update()
    {
        lastShotTime += Time.deltaTime;
        if (lastShotTime >= ShootingInterval)
        {
            //ShootAtPlayer();
            lastShotTime = 0f;
        }
        
    }
    public void ShootAtPlayer()
    {
        playerTrans = GameObject.Find("Doodler").transform;

        GameObject bubbleBullet = Instantiate(bubbleBulletPrefab, transform.position, transform.rotation);
        Vector3 playerPos = (playerTrans.position - transform.position).normalized;
        bubbleBullet.GetComponent<Rigidbody2D>().velocity = playerPos * 3;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f && collision.gameObject.tag == "Doodler")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "SalivaBullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else 
        {
            Destroy(collision.gameObject);
        }

        
    }
}
