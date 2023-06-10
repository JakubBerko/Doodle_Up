using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleMonster : MonoBehaviour
{
    private float lastShotTime;
    public float ShootingInterval = 2f;
    public GameObject bubbleBulletPrefab;
    private Transform playerTrans;

    private bool wasOnScreen;

    public TextMeshProUGUI coinText;
    private float coinAmount;

    //Animator
    public Animator animator;

    //handle Death
    private Controller controller;
    private void Start()
    {
        animator = GameObject.Find("Doodler").GetComponent<Animator>();
        controller = GameObject.FindGameObjectWithTag("Doodler").GetComponent<Controller>();
    }
    void Update()
    {
        if (wasOnScreen)
        {
            lastShotTime += Time.deltaTime;
            if (lastShotTime >= ShootingInterval)
            {
                ShootAtPlayer();
                lastShotTime = 0f;
            }
        }
    }
    public void OnBecameVisible()
    {
        wasOnScreen = true;
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
        if (collision.relativeVelocity.y <= 0f && collision.gameObject.tag == "Doodler" || collision.relativeVelocity.y <= 0f && collision.gameObject.tag == "Doodler" && collision.gameObject.layer == 9)
        {
            animator.SetTrigger("A_kill");
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "SalivaBullet")
        {
            animator.SetTrigger("A_kill");
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else 
        {
            controller.DeathHandler();
        }
    }
}
