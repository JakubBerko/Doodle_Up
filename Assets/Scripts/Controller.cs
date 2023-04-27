using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Linq;
//using UnityEngine;
//using UnityEngine.UI;

//public class PowerUpController : MonoBehaviour
//{
//    public float powerUpTimeLeft = 5f;
//    public Text powerUpTimeText;

//    void Update()
//    {
//        powerUpTimeLeft -= Time.deltaTime;
//        if (powerUpTimeLeft <= 0f)
//        {
//            // Power up vypršel
//            Destroy(gameObject);
//        }
//        powerUpTimeText.text = string.Format("{0:0.00}", powerUpTimeLeft);
//    }
//}

public class Controller : MonoBehaviour
{
    //score, highscore
    private Rigidbody2D rb;
    public TextMeshProUGUI score;
    public TextMeshProUGUI Highscore;
    private float maxScore = 0.0f;
    public GameObject Doodler;

    //ghost platform
    private Sprite ghostSprite;
    private Sprite doodlerSprite;
    private SpriteRenderer playerSprite;
    private GameObject[] ghostPlatforms;

    private float flyingSpeed = 5f;
    private float flyingDuration = 5f;
    private float timeInAir = 0.0f;
    private bool isInAir = false;
    private float vel = 9;

    //shrink on distance
    public GameObject[] shrinkingPlatforms;
    public float shrinkAmount = 0.5f; // jak moc se platforma bude zmenšovat
    private float distance;
    private GameObject platform;
    private float radius;
    [SerializeField] GameObject shrinkPlatform;
    private float shrink;

    //holographic platform
    public GameObject[] holographicPlatforms;

    //destroyer platform
    public GameObject[] platformsToBeDestroyed;
    public GameObject[] destroyerPlatforms;

    //PowerUp
    public GameObject[] invincibilityPowerUps;
    private float timeInvincible = 0.0f;
    private bool isInvincible = false;
    private float invincibilityDuration = 5f;
    public TextMeshProUGUI powerUpTimeText;
    public Image powerUpTimeImg;

    //Animator
    public Animator animator;

    //Coins
    public TextMeshProUGUI coinText;
    private float coinAmount;

    //Achievements
    public AchievementManager achievementManager;

    private void OnBecameInvisible() //kill doodler
    {
        achievementManager.UnlockAchievement(Achievements._Die);
        //když Doodler není vidìt, znièí se a naète se znovu scéna hry
        Destroy(Doodler);
        SceneManager.LoadScene("MainGameScene");

    }
    private void OnDestroy()
    {
        SaveRunInfo();
    }
    //update score
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //score, highscore
        //naètení uloženého highscore do promìnné a poté textu
        rb = GetComponent<Rigidbody2D>();
        float highScore = PlayerPrefs.GetFloat("highScore");
        Highscore.text = "BEST:" + highScore.ToString();

        //ghost platform script
        //ghostSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/hutao_ghost.png");
        //doodlerSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/doggie-like-cropped.png");
        doodlerSprite = Resources.Load<Sprite>("doggie-like-cropped");
        ghostSprite = Resources.Load<Sprite>("hutao_ghost");
        playerSprite = GetComponent<SpriteRenderer>();

        //PowerUp
        powerUpTimeText.enabled = false;
        powerUpTimeImg.enabled = false;
        //coins
        coinAmount = 0;
        //shrinkOnPlatform
        radius = 1;
    }

    void Update()
    {
        UpdateScore();

        // Ghost platform script
        ghostPlatforms = GameObject.FindGameObjectsWithTag("GhostPlatform");
        IsPlayerInAir();

        //shrink on distance
        shrinkingPlatforms = GameObject.FindGameObjectsWithTag("ShrinkOnDistancePlatform");
        //ShrinkPlatforms();

        //holographic platform
        holographicPlatforms = GameObject.FindGameObjectsWithTag("HolographicPlatform");

        //destroyer platform
        destroyerPlatforms = GameObject.FindGameObjectsWithTag("DestroyerPlatform");

        //powerUp
        invincibilityPowerUps = GameObject.FindGameObjectsWithTag("TeleportPowerUp");
        isPlayerInvincible();

        //animator
        animator.SetFloat("A_time_inv", timeInvincible);
        animator.SetBool("A_isInv", isInvincible);
        animator.SetFloat("A_rbVel", rb.velocity.y);
        animator.SetBool("A_isInAir", isInAir);
    }
    void UpdateScore()
    {
        //pokud se hráè pohybuje smìrem nahoru a zároveò pozice na y je vìtsí než aktuální skoré, tak se pøepíše skóre podle pozice
        if (rb.velocity.y > 0 && transform.position.y > maxScore)
        {
            maxScore = transform.position.y;
        }
        //pøevod floatu na zaokrouhlený string, kvúli textu v UI
        score.text = Mathf.Round(maxScore).ToString();
        
    }
    void SaveRunInfo()
    {
        //naètení highscore a pøepsání pokud hráè dosáhl vìtšího skóre než je highscore
        float highScore = PlayerPrefs.GetFloat("highScore");
        if (maxScore > highScore)
        {
            PlayerPrefs.SetFloat("highScore", Mathf.Round(maxScore));
            PlayerPrefs.Save();
            Debug.Log("saved highscore!");
        }
        //uložení coinù do PlayerPrefs
        float coins = PlayerPrefs.GetFloat("coins");
        coins += coinAmount;
        PlayerPrefs.SetFloat("coins",coins);
    }

    void IsPlayerInAir() //shrink on distance //pokud je hráè ve vzduchu, tak se pousouvá nahoru, a po 5 vteøinách se zmìní sprite, vyresetuje èas ve vzduchu a lehce vyskoèí
                         //(to jsem pøidal kvùli zlehèení a menšímu pøekvapení z toho že hráè padá dolu) a hráè není ve vzduchu (isInAir = false;)
    {
        if (isInAir)
        {
            rb.velocity = new Vector2(0, flyingSpeed);
            timeInAir += Time.deltaTime;
            if (timeInAir >= flyingDuration)
            {
                timeInAir = 0.0f;
                Vector2 velocity = rb.velocity;
                velocity.y = vel;
                rb.velocity = velocity;
                isInAir = false;
            }
        }
    }
    void isPlayerInvincible()
    {
        if (isInvincible)
        {
            timeInvincible += Time.deltaTime;
            Debug.Log(timeInvincible);
            if (invincibilityDuration - timeInvincible <1)
            {
                powerUpTimeText.text = string.Format("{0:0.0}", invincibilityDuration - timeInvincible);
            }
            else
            {
                powerUpTimeText.text = string.Format("{0:0}", invincibilityDuration - timeInvincible);
            }
            //Debug.Log(timeInAir);
            if (timeInvincible >= invincibilityDuration)
            {
                isInvincible = false;
                gameObject.layer = 0;
                powerUpTimeText.enabled = false;
                powerUpTimeImg.enabled = false;
                timeInvincible = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Ghost platform script //po dotknutí platformy se hráèovi zmìní bool isInAir
        for (int i = 0; i < ghostPlatforms.Length; i++)
        {
            if (collision.gameObject == ghostPlatforms[i] && collision.relativeVelocity.y >= 0f)
            {
               
                isInAir = true;
            }
        }

        //destroyer platforms //po dotknutí platformy se aktivuje skript a znièí platforma, na kterou skoèil (aby hráè nemohl nièt platformy vícekrát)
        for (int u = 0; u < destroyerPlatforms.Length; u++)
        {
            if (collision.gameObject == destroyerPlatforms[u] && collision.relativeVelocity.y >= 0f)
            {
                DestroyerPlatform();
                Destroy(destroyerPlatforms[u]);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        platform = collision.gameObject;
        Debug.Log(platform.name);
        Debug.Log(platform.tag);
        Debug.Log(platform.gameObject.tag);
        Debug.Log(platform.gameObject.name);
        if (platform.tag != "ShrinkOnDistancePlatform") return;
        distance = (platform.transform.position - transform.position).magnitude;
        Debug.Log(distance);
        if (distance > radius) return;
        shrink = (1 - distance / radius) * shrinkAmount;
        platform.transform.localScale -= new Vector3(shrink, shrink, shrink);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //holographic platform //(zde jsem použil on trigger, jelikož pøes OnCollisionEnter se hráè zasekl o platformu) pokud hráè projde platformou, tak se znièí objekt platformy
        Vector2 playerVelocity = rb.velocity;
        for (int x = 0; x < holographicPlatforms.Length; x++)
        {
            if (collision.gameObject == holographicPlatforms[x] && playerVelocity.y <=0f)
            {
                Destroy(holographicPlatforms[x]);
            }
        }
        //PowerUp
        if (collision.gameObject.tag == "TeleportPowerUp")
        {
            timeInvincible = 0f;
            isInvincible = false;
            powerUpTimeText.enabled = true;
            powerUpTimeImg.enabled = true;
            for (int x = 0; x < invincibilityPowerUps.Length; x++)
            {
                if (collision.gameObject == invincibilityPowerUps[x])
                {
                    Destroy(invincibilityPowerUps[x]);
                }
            }
            gameObject.layer = 9;
            isInvincible = true;
        }
        //coins score update
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coinAmount++;
            coinText.text = coinAmount.ToString();
        }
    }

    //void ShrinkPlatforms() //shrink on distance 
    //{
    //    for (int j = 0; j <shrinkingPlatforms.Length; j++)
    //    {
    //        float distance = Vector3.Distance(transform.position,shrinkingPlatforms[j].transform.position); //vzdálenost hráèe od platformy

    //        if (distance < shrinkDistance)
    //        {
    //            //spoèítá shrinkElementu (o kolik se zmensi)
    //            float shrinkElement = 1 - (distance / shrinkDistance) * shrinkAmount;

    //            //zmenší objekt shrinkfaktorem vynásobením normálního vektoru tím zmenšením,,
    //            shrinkingPlatforms[j].transform.localScale = Vector3.one * shrinkElement;
    //        }
    //    }
    //}

    void DestroyerPlatform() //najde všechny spawnuté platformy na obrazovce (jelikož platformy se spawnují tìsnì nad limitem obrazovky) a rozpùlí ho
    {
        platformsToBeDestroyed = GameObject.FindObjectsOfType<GameObject>();
        int numberOfObjectsToDestroy = platformsToBeDestroyed.Length / 2;

        // zamíchání pole Fisher-Yates shuffle algoritmem ((VELMI zajímavý algoritmus!), kvùli tomu, že platformy jsou v poli od vrchu obrazovky dolu)
        for (int i = platformsToBeDestroyed.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = platformsToBeDestroyed[i];
            platformsToBeDestroyed[i] = platformsToBeDestroyed[randomIndex];
            platformsToBeDestroyed[randomIndex] = temp;
        }

        for (int i = 0; i < numberOfObjectsToDestroy; i++) // zde se smažou všechny objekty s komponentem (prázdný skript Platform_Tag), je to kvùli oddìlení platform od ostatních gameobjektù, jak je napø. hráè, nepøátelé
        {
            if (platformsToBeDestroyed[i].GetComponent<Platform_tag>() != null)
            {
                Destroy(platformsToBeDestroyed[i]);
            }
        }
    }
}

