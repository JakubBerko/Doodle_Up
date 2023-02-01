using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedGame : MonoBehaviour
{
    [SerializeField] GameObject pausedGame;
    private bool isPaused = false;
    public void StopGame()
    {
        pausedGame.SetActive(true); //zobraz� UI pausedGame
        Time.timeScale = 0f; //nastav� �as na 0
        isPaused = true;

    }
    public void ResumeGame()
    {
        pausedGame.SetActive(false); //schov� UI pausedGame
        Time.timeScale = 1f; //nastav� �as na 1
        isPaused = false;
    }
    public void ReturnToMainMenu()
    {
        //TODO (NEFUNK�N� - PRODISKUTOVAT S VEDOUC�M PROJEKTU)
        
        SceneManager.LoadScene("MainGameScene"); //na�te novou sc�nu
        pausedGame.SetActive(false); //schov� UI pausedGame
        Time.timeScale = 1f; //nastav� �as na 1
        //Debug.Log("working");
        
    }
}
