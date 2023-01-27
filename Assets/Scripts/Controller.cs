using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Linq;

public class Controller : MonoBehaviour
{
    /*//size decreasing platform
    public GameObject objectToDecrease;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float distanceThreshold = 5.0f;
    private float initialSize;
    */

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
        /*
        //size decreasing platform
        if (objectToDecrease != null)
        {
            initialSize = objectToDecrease.transform.localScale.x;
        }
        else
        {
            Debug.LogError("objectToDecrease neeexistuje");
        }
        */
        //score, highscore
        //naètení uloženého highscore do promìnné a poté textu
        rb = GetComponent<Rigidbody2D>();
        float highScore = PlayerPrefs.GetFloat("highScore");
        Highscore.text = "BEST:" + highScore.ToString();

        //ghost platform script
        ghostSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/hutao_ghost.png");
        doodlerSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/doggie-like-cropped.png");
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        
    }

    void Update()
    {
        UpdateScore();
        //ChangePlatformSizeByPlayerPosition();

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

    void IsPlayerInAir() //shrink on distance
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
        //Ghost platform script
        for (int i = 0; i < ghostPlatforms.Length; i++)
        {
            if (collision.gameObject == ghostPlatforms[i] && collision.relativeVelocity.y >= 0f)
            {
                playerSprite.sprite = ghostSprite;
                isInAir = true;
            }
        }

        //holographic platform
        for (int x = 0; x < holographicPlatforms.Length; x++)
        {
            if (collision.gameObject == holographicPlatforms[x]&&collision.relativeVelocity.y >= 0f)
            {
                holographicPlatforms[x].GetComponent<EdgeCollider2D>().isTrigger = true;
                Destroy(holographicPlatforms[x]);
            }
        }

        //destroyer platforms
        for (int u = 0; u < destroyerPlatforms.Length; u++)
        {
            if (collision.gameObject == destroyerPlatforms[u] && collision.relativeVelocity.y >= 0f)
            {
                DestroyerPlatform();
            }
        }

    }

    void ShrinkPlatforms() //shrink on distance
    {
        for (int j = 0; j <shrinkingPlatforms.Length; j++)
        {
            float distance = Vector3.Distance(transform.position,shrinkingPlatforms[j].transform.position);

            if (distance < shrinkDistance)
            {
                //spoèítá shrinkfactor
                float shrinkFactor = 1 - (distance / shrinkDistance) * shrinkAmount;

                // zmenší objekt shrinkfaktorem
                shrinkingPlatforms[j].transform.localScale = Vector3.one * shrinkFactor;
            }
        }
        
    }

    void DestroyerPlatform()
    {
        platformsToBeDestroyed = GameObject.FindObjectsOfType<GameObject>();
        int deleteCount = 0;
        for (int i = 0; i < platformsToBeDestroyed.Length; i++)
        {
            if (platformsToBeDestroyed[i] != Doodler)
            {
                deleteCount++;
                if (deleteCount% 2 == 0)
                {
                    Destroy(platformsToBeDestroyed[i]);
                }
            }
        }
    }
}
    /*
    void ChangePlatformSizeByPlayerPosition()
    {
        if (objectToDecrease != null)
        {
            float distance = Vector3.Distance(objectToDecrease.transform.position, transform.position);
            float size = Mathf.Lerp(minSize, maxSize, distance / distanceThreshold);
            objectToDecrease.transform.localScale = new Vector3(size, size, size);
        }
    }
    */

