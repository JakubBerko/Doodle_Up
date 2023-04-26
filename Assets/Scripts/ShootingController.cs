using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject salivaBulletPrefab;
    public Transform bulletDirection;
    private bool isPaused;
    public Animator animator;

    public AchievementManager achievementManager;
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Shoot();
            animator.SetTrigger("A_shoot");
            //animator.ResetTrigger("A_shoot");
            achievementManager.UnlockAchievement(Achievements._Shoot);
        }

        if (isPaused)
        {
            return;
        }
    }

    public void Shoot()
    {
        GameObject salivaBullet = Instantiate(salivaBulletPrefab, bulletDirection.position + new Vector3(0, 0.5f, 0), bulletDirection.rotation);
        Rigidbody2D rb = salivaBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletDirection.up * 1000f);
    }
}


