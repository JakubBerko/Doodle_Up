using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletHit : MonoBehaviour //skript pro kontrolu zda-li hrac byl trefen nabojem od prisery
{
    private GameObject[] bubbleBullets;
    private Controller controller;
    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Doodler").GetComponent<Controller>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bubbleBullets = GameObject.FindGameObjectsWithTag("BubbleBullet"); //vsechny bubliny monster
        for (int i = 0; i < bubbleBullets.Length; i++)
        {
            if (collision.gameObject.tag == "Doodler")
            {
                controller.DeathHandler(); //pokud bublina trefi hrace, hrac umre
            }
            if (collision.gameObject.tag == "SalivaBullet") //pokud bublina trefi slinu od hrace, umre bublina i slina
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
