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
        if (isPaused) return; //kdyz je hra paused, nestrili
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !controller.isInAir && canShoot)
        {
            if (EventSystem.current.IsPointerOverGameObject()) //pokud klikam na ui element
            {
                return;
            }
            else 
            { 
                Shoot(); //støel
                StartCoroutine(ShotLimiter()); //limitace støel (anti spam)
            }
        }
    }
    IEnumerator ShotLimiter() //antispam støel, zruš støíení, poèkej nìjaký èas, povol støílení
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
    public void Shoot() 
    {
        //spawni slinu trochu nad hráèem (offset)
        GameObject salivaBullet = Instantiate(salivaBulletPrefab, bulletDirection.position + new Vector3(0, 0.5f, 0), bulletDirection.rotation);
        Rigidbody2D rb = salivaBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletDirection.up * 1000f); //vymrštìní støely
        animator.SetTrigger("A_shoot"); //animace hráèe
        shootSound.clip = shootClip; //zvuk
        shootSound.Play();//zvuk
        achievementManager.UnlockAchievement(Achievements._Shoot);//achievement
    }
    
}


