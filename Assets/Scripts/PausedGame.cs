using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedGame : MonoBehaviour
{
    [SerializeField] GameObject pausedGame;
    public GameObject deathMenu;
    private ShootingController shootingController;
    private void Start()
    {
        shootingController = GameObject.Find("GameManager").GetComponent<ShootingController>();
    }
    public void StopGame()
    {
        pausedGame.SetActive(true); //zobraz� UI pausedGame
        Time.timeScale = 0f; //nastav� �as na 0
        shootingController.isPaused = true; //zastav� st��len�
    }
    public void ResumeGame()
    {
        pausedGame.SetActive(false); //schov� UI pausedGame
        Time.timeScale = 1f; //nastav� �as na 1
        shootingController.isPaused = false; //povol� st��len�
    }
    public void ReturnToMainMenu()
    {
        deathMenu = GameObject.Find("DeathMenu"); //nav�c ��dek kodu
        pausedGame.SetActive(false); //schov� UI pausedGame
        Time.timeScale = 1f; //nastav� �as na 1
        shootingController.isPaused = false;//povol� st��len�
        SceneManager.LoadScene("MainMenuScreen"); //na�te novou sc�nu
    }
    public void Retry()
    {
        deathMenu = GameObject.Find("DeathMenu"); 
        deathMenu.SetActive(false); //schov� UI
        Time.timeScale = 1f;//nastav� �as na 1
        shootingController.isPaused = false;//povol� st��len�
        SceneManager.LoadScene("MainGameScene"); //na�te scenu
    }
    
}
