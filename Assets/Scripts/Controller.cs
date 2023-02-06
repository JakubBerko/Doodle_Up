using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Linq;

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
    public float shrinkDistance = 10; // vzdálenost od které se platforma zaène zmenšovat
    public float shrinkAmount = 0.5f; // jak moc se platforma bude zmenšovat

    //holographic platform
    public GameObject[] holographicPlatforms;

    //destroyer platform
    public GameObject[] platformsToBeDestroyed;
    public GameObject[] destroyerPlatforms;

    //PowerUp

    private void OnBecameInvisible() //kill doodler
    {
        //když Doodler není vidìt, znièí se a naète se znovu scéna hry
        Destroy(Doodler);
        SceneManager.LoadScene("MainGameScene");
    }
    private void OnDestroy()
    {
        SaveHighScore();
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

        
    }

    void Update()
    {
        UpdateScore();

        // Ghost platform script
        ghostPlatforms = GameObject.FindGameObjectsWithTag("GhostPlatform");
        IsPlayerInAir();

        //shrink on distance
        shrinkingPlatforms = GameObject.FindGameObjectsWithTag("ShrinkOnDistancePlatform");
        ShrinkPlatforms();

        //holographic platform
        holographicPlatforms = GameObject.FindGameObjectsWithTag("HolographicPlatform");

        //destroyer platform
        destroyerPlatforms = GameObject.FindGameObjectsWithTag("DestroyerPlatform");
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
    void SaveHighScore()
    {
        //naètení highscore a pøepsání pokud hráè dosáhl vìtšího skóre než je highscore
        float highScore = PlayerPrefs.GetFloat("highScore");
        if (maxScore > highScore)
        {
            PlayerPrefs.SetFloat("highScore", Mathf.Round(maxScore));
            PlayerPrefs.Save();
            Debug.Log("saved highscore!");
        }
    }

    void IsPlayerInAir() //shrink on distance //pokud je hráè ve vzduchu, tak se pousouvá nahoru, a po 5 vteøinách se zmìní sprite, vyresetuje èas ve vzduchu a lehce vyskoèí
                         //(to jsem pøidal kvùli zlehèení a menšímu pøekvapení z toho že hráè padá dolu) a hráè není ve vzduchu (isInAir = false;)
    {
        if (isInAir)
        {
            rb.velocity = new Vector2(0, flyingSpeed);
            timeInAir += Time.deltaTime;
            //Debug.Log(timeInAir);
            if (timeInAir >= flyingDuration)
            {
                playerSprite.sprite = doodlerSprite;
                timeInAir = 0.0f;
                Vector2 velocity = rb.velocity;
                velocity.y = vel;
                rb.velocity = velocity;
                isInAir = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Ghost platform script //po dotknutí platformy se hráèovi zmìní sprite a bool isInAir
        for (int i = 0; i < ghostPlatforms.Length; i++)
        {
            if (collision.gameObject == ghostPlatforms[i] && collision.relativeVelocity.y >= 0f)
            {
                playerSprite.sprite = ghostSprite;
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
            
        }
    }

    void ShrinkPlatforms() //shrink on distance 
    {
        for (int j = 0; j <shrinkingPlatforms.Length; j++)
        {
            float distance = Vector3.Distance(transform.position,shrinkingPlatforms[j].transform.position); //vzdálenost hráèe od platformy

            if (distance < shrinkDistance)
            {
                //spoèítá shrinkElementu (o kolik se zmensi)
                float shrinkElement = 1 - (distance / shrinkDistance) * shrinkAmount;

                //zmenší objekt shrinkfaktorem vynásobením normálního vektoru tím zmenšením,,
                shrinkingPlatforms[j].transform.localScale = Vector3.one * shrinkElement;
            }
        }
        
    }

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

