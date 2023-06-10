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
    private Controller controller;
    private bool canShoot = true;
    public float shootCooldown = 0.3f;
    public AudioSource shootSound;
    public AudioClip shootClip;

    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Doodler").GetComponent<Controller>();
    }
    void Update()
    {
        if (isPaused) return;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !controller.isInAir && canShoot)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else 
            { 
                Shoot();
                StartCoroutine(ShotLimiter());
            }
        }
    }
    IEnumerator ShotLimiter()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
    public void Shoot()
    {
        GameObject salivaBullet = Instantiate(salivaBulletPrefab, bulletDirection.position + new Vector3(0, 0.5f, 0), bulletDirection.rotation);
        Rigidbody2D rb = salivaBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletDirection.up * 1000f);
        animator.SetTrigger("A_shoot");
        shootSound.clip = shootClip;
        shootSound.Play();
        achievementManager.UnlockAchievement(Achievements._Shoot);
    }
    
}


