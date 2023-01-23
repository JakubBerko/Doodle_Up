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

    public TextMeshProUGUI timer;
    private float timerFloat;
    //kill doodler
    private void OnBecameInvisible()
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
    }

    void Update()
    {
        //TestFunction();
        UpdateScore();
        //ChangePlatformSizeByPlayerPosition();
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
    void TestFunction()
    {
        timerFloat += Time.deltaTime;
        timer.text = timerFloat.ToString();
        if (timerFloat >= 5)
        {
            timer.text = "0";
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
