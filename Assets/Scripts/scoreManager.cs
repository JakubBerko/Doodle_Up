using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    private Rigidbody2D rb;
    public TextMeshProUGUI score;
    private float maxScore = 0.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }
    void UpdateScore()
    {
        if (rb.velocity.y > 0 && transform.position.y > maxScore)
        {
            maxScore = transform.position.y;
        }
        score.text = Mathf.Round(maxScore).ToString();
    }
}
