using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedGame : MonoBehaviour
{
    [SerializeField] GameObject pausedGame;
    private ShootingController shootingController;
    private Controller controller;
    private void Start()
    {
        shootingController = GameObject.Find("GameManager").GetComponent<ShootingController>();
        controller = GameObject.Find("GameManager").GetComponent<Controller>();
    }
    public void StopGame()
    {
        pausedGame.SetActive(true); //zobraz� UI pausedGame
        Time.timeScale = 0f; //nastav� �as na 0
        shootingController.isPaused = true;


    }
    public void ResumeGame()
    {
        pausedGame.SetActive(false); //schov� UI pausedGame
        Time.timeScale = 1f; //nastav� �as na 1
        shootingController.isPaused = false;
    }
    public void ReturnToMainMenu()
    {
        //TODO (NEFUNK�N� - PRODISKUTOVAT S VEDOUC�M PROJEKTU)
        pausedGame.SetActive(false); //schov� UI pausedGame
        Time.timeScale = 1f; //nastav� �as na 1
        shootingController.isPaused = false;
        controller.mainMenu = true;
        SceneManager.LoadScene("MainMenuScreen"); //na�te novou sc�nu
        //SceneManager.LoadScene("MainMenuScreen", LoadSceneMode.Single); //force? - nefunguje
    }
    
}
