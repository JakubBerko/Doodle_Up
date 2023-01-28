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
    public float shrinkDistance = 10; // vzd�lenost od kter� se platforma za�ne zmen�ovat
    public float shrinkAmount = 0.5f; // jak moc se platforma bude zmen�ovat

    //holographic platform
    public GameObject[] holographicPlatforms;

    //destroyer platform
    public GameObject[] platformsToBeDestroyed;
    public GameObject[] destroyerPlatforms;

    private void OnBecameInvisible() //kill doodler
    {
        //kdy� Doodler nen� vid�t, zni�� se a na�te se znovu sc�na hry
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
        Debug.Log(gameObject.GetComponent<Rigidbody2D>().gravityScale);
        
        //score, highscore
        //na�ten� ulo�en�ho highscore do prom�nn� a pot� textu
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
        //pokud se hr�� pohybuje sm�rem nahoru a z�rove� pozice na y je v�ts� ne� aktu�ln� skor�, tak se p�ep�e sk�re podle pozice
        if (rb.velocity.y > 0 && transform.position.y > maxScore)
        {
            maxScore = transform.position.y;
        }
        //p�evod floatu na zaokrouhlen� string, kv�li textu v UI
        score.text = Mathf.Round(maxScore).ToString();
        
    }
    void SaveHighScore()
    {
        //na�ten� highscore a p�eps�n� pokud hr�� dos�hl v�t��ho sk�re ne� je highscore
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

        //destroyer platforms
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
        //holographic platform
        Vector2 playerVelocity = rb.velocity;
        for (int x = 0; x < holographicPlatforms.Length; x++)
        {
            if (collision.gameObject == holographicPlatforms[x] && playerVelocity.y <=0f)
            {
                //holographicPlatforms[x].GetComponent<EdgeCollider2D>().isTrigger = true;
                Destroy(holographicPlatforms[x]);
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
                //spo��t� shrinkfactor
                float shrinkFactor = 1 - (distance / shrinkDistance) * shrinkAmount;

                // zmen�� objekt shrinkfaktorem
                shrinkingPlatforms[j].transform.localScale = Vector3.one * shrinkFactor;
            }
        }
        
    }

    void DestroyerPlatform()
    {
        platformsToBeDestroyed = GameObject.FindObjectsOfType<GameObject>();
        int numberOfObjectsToDestroy = platformsToBeDestroyed.Length / 2;

        // Shuffle the array using the Fisher-Yates shuffle algorithm
        for (int i = platformsToBeDestroyed.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = platformsToBeDestroyed[i];
            platformsToBeDestroyed[i] = platformsToBeDestroyed[randomIndex];
            platformsToBeDestroyed[randomIndex] = temp;
        }

        for (int i = 0; i < numberOfObjectsToDestroy; i++)
        {
            if (platformsToBeDestroyed[i].GetComponent<Platform_tag>() != null)
            {
                Destroy(platformsToBeDestroyed[i]);
            }
        }

    }

}

