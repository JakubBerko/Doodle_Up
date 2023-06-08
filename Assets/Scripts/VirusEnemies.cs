using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VirusEnemies : MonoBehaviour
{
    public float speed = 1.5f; // adjust the platform speed
    public float distance = 0.5f; // adjust the distance the platform moves

    private Vector3 startPos;
    private Vector3 endPos;
    private float journeyLength;

    private bool wasOnScreen;

    //Animator
    public Animator animator;

    public TextMeshProUGUI coinText;
    private float coinAmount;

    //DeathHandler
    private Controller controller;

    void Start()
    {
        animator = GameObject.Find("Doodler").GetComponent<Animator>();
        controller = GameObject.FindGameObjectWithTag("Doodler").GetComponent<Controller>();

        startPos = transform.position;
        endPos = startPos + new Vector3(distance, 0, 0);
    }

    void Update()
    {
        if (wasOnScreen)
        {
            float pingPongValue = Mathf.PingPong(Time.time * speed, distance);
            transform.position = startPos + new Vector3(pingPongValue, 0, 0);
        }
    }
    public void OnBecameVisible()
    {
        wasOnScreen = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f && collision.gameObject.tag == "Doodler")
        {
            animator.SetTrigger("A_kill");
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "SalivaBullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            animator.SetTrigger("A_kill");
        }
        else
        {
            controller.DeathHandler();
        }
    }
}
