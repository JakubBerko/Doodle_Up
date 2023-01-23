using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ghost_platform : MonoBehaviour
{
    private Sprite ghostSprite;
    private Sprite doodlerSprite;
    private GameObject player;
    private SpriteRenderer playerSprite;
    private Rigidbody2D rb;
    
    private float flyingSpeed = 5f;
    private float flyingDuration = 5f;
    private float timeInAir = 0.0f;
    private bool isInAir = false;
    private float vel = 9;

    

    void Start()
    {
        player = GameObject.Find("Doodler");
        ghostSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/hutao_ghost.png");
        doodlerSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/doggie-like-cropped.png");
        rb = player.GetComponent<Rigidbody2D>();
        playerSprite = player.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        
        if (isInAir)
        {
            rb.velocity = new Vector2(0, flyingSpeed);
            timeInAir += Time.deltaTime;
            Debug.Log(timeInAir);
            if(timeInAir >= flyingDuration)
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
        if (collision.gameObject == player && collision.relativeVelocity.y <= 0f)
        {
            playerSprite.sprite = ghostSprite;
            isInAir = true;
        }
    }
    
}
