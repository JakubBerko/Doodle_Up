using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedGame : MonoBehaviour
{
    [SerializeField] GameObject pausedGame; 
    public void StopGame()
    {
        pausedGame.SetActive(true); //zobrazí UI pausedGame
        Time.timeScale = 0f; //nastaví èas na 0
    }
    public void ResumeGame()
    {
        pausedGame.SetActive(false); //schová UI pausedGame
        Time.timeScale = 1f; //nastaví èas na 1
    }
    public void ReturnToMainMenu()
    {
        //TODO (NEFUNKÈNÍ - PRODISKUTOVAT S VEDOUCÍM PROJEKTU)
        /*
        SceneManager.LoadScene("MainGameScene"); //naète novou scénu
        pausedGame.SetActive(false); //schová UI pausedGame
        Time.timeScale = 0f; //nastaví èas na 1
        Debug.Log("working");
        */
    }
}
