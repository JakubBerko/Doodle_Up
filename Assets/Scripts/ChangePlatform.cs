using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlatform : MonoBehaviour
{
    public GameObject[] platformPrefabs; //všechny prefaby ve scene
    void ChangePlatformPrefab() //zmeni prefab plaformy na stejne pozici jako puvodni platforma podle random indexu a znici starou
    {
        int randomIndex = Random.Range(0, platformPrefabs.Length);
        GameObject randomPlatform = Instantiate(platformPrefabs[randomIndex], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision) //pokud ji trefi slina zmeni se
    {
        if (collision.gameObject.tag=="SalivaBullet")
        {
            ChangePlatformPrefab();
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //to samé co výše, ale pro holografickou platformu
    {
        if (collision.gameObject.tag == "SalivaBullet")
        {
            ChangePlatformPrefab();
            Destroy(collision.gameObject);
        }
    }
}
