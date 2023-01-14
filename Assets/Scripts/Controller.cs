using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


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
    
    //kill doodler
    private void OnBecameInvisible()
    {
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
        rb = GetComponent<Rigidbody2D>();
        float highScore = PlayerPrefs.GetFloat("highScore");
        Highscore.text = "BEST:" + highScore.ToString();
    }

    void Update()
    {
        UpdateScore();
        //ChangePlatformSizeByPlayerPosition();
    }
    void UpdateScore()
    {
        if (rb.velocity.y > 0 && transform.position.y > maxScore)
        {
            maxScore = transform.position.y;
        }
        score.text = Mathf.Round(maxScore).ToString();
        
    }
    void SaveHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("highScore");
        if (maxScore > highScore)
        {
            PlayerPrefs.SetFloat("highScore", Mathf.Round(maxScore));
            PlayerPrefs.Save();
            Debug.Log("saved highscore!");
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
