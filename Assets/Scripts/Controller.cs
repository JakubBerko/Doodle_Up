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
    public GameObject[] shrinkingPlatform;
    public float shrinkDistance = 10; // vzd�lenost od kter� se platforma za�ne zmen�ovat
    public float shrinkAmount = 0.5f; // jak moc se platforma bude zmen�ovat

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
        shrinkingPlatform = GameObject.FindGameObjectsWithTag("ShrinkOnDistancePlatform");
        ShrinkPlatforms();

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
            if (collision.gameObject == ghostPlatforms[i])
            {
                Debug.Log(collision.relativeVelocity.y);
            }
            if (collision.gameObject == ghostPlatforms[i] && collision.relativeVelocity.y <= -1f)
            {
                playerSprite.sprite = ghostSprite;
                isInAir = true;
            }
        }

    }

    void ShrinkPlatforms() //shrink on distance
    {
        for (int j = 0; j <shrinkingPlatform.Length; j++)
        {
            float distance = Vector3.Distance(transform.position,shrinkingPlatform[j].transform.position);

            if (distance < shrinkDistance)
            {
                //spo��t� shrinkfactor
                float shrinkFactor = 1 - (distance / shrinkDistance) * shrinkAmount;

                // zmen�� objekt shrinkfaktorem
                shrinkingPlatform[j].transform.localScale = Vector3.one * shrinkFactor;
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
}
