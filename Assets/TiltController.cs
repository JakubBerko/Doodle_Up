using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    
    Rigidbody2D rb;
    float dx;
    float moveSpeed = 20f;
    private Renderer[] renderers;

    void Start()
    {
        //phasing through screen
        renderers = GetComponentsInChildren<Renderer>();
    }
    // Start is called before the first frame update
    void Awake()
    {
        //movement
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        dx = Input.acceleration.x * moveSpeed;
    }
    void FixedUpdate()
    {
        //movement
        Vector2 velocity = rb.velocity;
        velocity.x = dx;
        rb.velocity = velocity;
        //phasing through screen
        ScreenPhase();
    }
    void ScreenPhase()
    {
        bool isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }
        if(isWrappingX && isWrappingY)
        {
            return;
        }

        Vector3 newPosition = transform.position;

        if(newPosition.x > 1 || newPosition.x < 0)
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }
        if (newPosition.y > 1 || newPosition.y<0)
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }
        transform.position = newPosition;
    }
    bool CheckRenderers()
    {
        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                return true;
            }
            return false;
        }
    }
}
