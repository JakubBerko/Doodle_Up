using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    //Skin
    [SerializeField] SpriteRenderer playerSkin;

    //Death handler
    [SerializeField] GameObject deathMenuUI;
    [SerializeField] TMP_Text deathScore_text;
    [SerializeField] TMP_Text deathCoin_text;
    [SerializeField] GameObject gameManager;
    //update score
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ChangePlayerSkin();

        //score, highscore
        //načtení uloženého highscore do proměnné a poté textu
        rb = GetComponent<Rigidbody2D>();
        float highScore = PlayerPrefs.GetFloat("highScore");
        Highscore.text = "BEST:" + highScore.ToString();

        //ghost platform script
        doodlerSprite = Resources.Load<Sprite>("doggie-like-cropped");
        ghostSprite = Resources.Load<Sprite>("hutao_ghost");
        playerSprite = GetComponent<SpriteRenderer>();

        //PowerUp
        powerUpTimeText.enabled = false;
        powerUpTimeImg.enabled = false;
        //coins
        coinAmount = 0;
    }
    void Update()
    {

        UpdateScore();

        // Ghost platform script
        ghostPlatforms = GameObject.FindGameObjectsWithTag("GhostPlatform");
        IsPlayerInAir();

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

    public void DeathHandler()
    {
        achievementManager.UnlockAchievement(Achievements._Die);
        Doodler.SetActive(false);
        gameManager.GetComponent<ShootingController>().enabled = false;
        gameManager.GetComponent<GenerateMap>().enabled = false;
        Time.timeScale = 0f;
        deathScore_text.text = "Score: "+ Mathf.Round(maxScore).ToString();
        deathCoin_text.text = "Coins: "+ coinAmount.ToString();
        SaveRunInfo();
        deathMenuUI.SetActive(true);
    }
    void UpdateScore()
    {
        //pokud se hráč pohybuje směrem nahoru a zároveň pozice na y je větsí než aktuální skoré, tak se přepíše skóre podle pozice
        if (rb.velocity.y > 0 && transform.position.y > maxScore)
        {
            maxScore = transform.position.y;
        }
        //převod floatu na zaokrouhlený string, kvúli textu v UI
        score.text = Mathf.Round(maxScore).ToString();
        
    }
    void SaveRunInfo()
    {
        //načtení highscore a přepsání pokud hráč dosáhl většího skóre než je highscore
        float highScore = PlayerPrefs.GetFloat("highScore");
        if (maxScore > highScore)
        {
            PlayerPrefs.SetFloat("highScore", Mathf.Round(maxScore));
            PlayerPrefs.Save();
            Debug.Log("saved highscore!");
        }
        //uložení coinů do PlayerPrefs
        float coins = PlayerPrefs.GetFloat("coins");
        coins += coinAmount;
        PlayerPrefs.SetFloat("coins",coins);
    }

    void IsPlayerInAir() //shrink on distance //pokud je hráč ve vzduchu, tak se pousouvá nahoru, a po 5 vteřinách se změní sprite, vyresetuje čas ve vzduchu a lehce vyskočí
                         //(to jsem přidal kvůli zlehčení a menšímu překvapení z toho že hráč padá dolu) a hráč není ve vzduchu (isInAir = false;)
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
        //Ghost platform script //po dotknutí platformy se hráčovi změní bool isInAir
        for (int i = 0; i < ghostPlatforms.Length; i++)
        {
            if (collision.gameObject == ghostPlatforms[i] && collision.relativeVelocity.y >= 0f)
            {
               
                isInAir = true;
            }
        }

        //destroyer platforms //po dotknutí platformy se aktivuje skript a zničí platforma, na kterou skočil (aby hráč nemohl ničt platformy vícekrát)
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
        //holographic platform //(zde jsem použil on trigger, jelikož přes OnCollisionEnter se hráč zasekl o platformu) pokud hráč projde platformou, tak se zničí objekt platformy
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

    void DestroyerPlatform() //najde všechny spawnuté platformy na obrazovce (jelikož platformy se spawnují těsně nad limitem obrazovky) a rozpůlí ho
    {
        platformsToBeDestroyed = GameObject.FindObjectsOfType<GameObject>();
        int numberOfObjectsToDestroy = platformsToBeDestroyed.Length / 2;

        // zamíchání pole Fisher-Yates shuffle algoritmem ((VELMI zajímavý algoritmus!), kvůli tomu, že platformy jsou v poli od vrchu obrazovky dolu)
        for (int i = platformsToBeDestroyed.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = platformsToBeDestroyed[i];
            platformsToBeDestroyed[i] = platformsToBeDestroyed[randomIndex];
            platformsToBeDestroyed[randomIndex] = temp;
        }

        for (int i = 0; i < numberOfObjectsToDestroy; i++) // zde se smažou všechny objekty s komponentem (prázdný skript Platform_Tag), je to kvůli oddělení platform od ostatních gameobjektů, jak je např. hráč, nepřátelé
        {
            if (platformsToBeDestroyed[i].GetComponent<Platform_tag>() != null)
            {
                Destroy(platformsToBeDestroyed[i]);
            }
        }
    }
    void ChangePlayerSkin()
    {
        Skin skin = GameDataManager.GetSelectedCharacter();
        if (skin.image != null)
        {
            playerSkin.sprite = skin.image;
            Debug.LogWarning("Changed skin");
        }
    }
}

