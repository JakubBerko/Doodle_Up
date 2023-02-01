using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlatform : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    void ChangePlatformPrefab()
    {
        int randomIndex = Random.Range(0, platformPrefabs.Length);
        GameObject randomPlatform = Instantiate(platformPrefabs[randomIndex], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="SalivaBullet")
        {
            ChangePlatformPrefab();
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SalivaBullet")
        {
            ChangePlatformPrefab();
            Destroy(collision.gameObject);
        }
    }
}
