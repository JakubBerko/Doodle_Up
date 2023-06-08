using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingController : MonoBehaviour
{
    public GameObject salivaBulletPrefab;
    public Transform bulletDirection;
    public Animator animator;
    public bool isPaused = false;
    public AchievementManager achievementManager;
    void Update()
    {
        if (isPaused) return;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else 
            { 
            Shoot();
            }
        }
    }

    public void Shoot()
    {
        GameObject salivaBullet = Instantiate(salivaBulletPrefab, bulletDirection.position + new Vector3(0, 0.5f, 0), bulletDirection.rotation);
        Rigidbody2D rb = salivaBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletDirection.up * 1000f);
        animator.SetTrigger("A_shoot");
    }
    
}


