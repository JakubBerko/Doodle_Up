using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        pausedGame.SetActive(true); //zobrazí UI pausedGame
        Time.timeScale = 0f; //nastaví èas na 0
        shootingController.isPaused = true;
    }
    public void ResumeGame()
    {
        pausedGame.SetActive(false); //schová UI pausedGame
        Time.timeScale = 1f; //nastaví èas na 1
        shootingController.isPaused = false;
    }
    public void ReturnToMainMenu()
    {
        deathMenu = GameObject.Find("DeathMenu");
        pausedGame.SetActive(false); //schová UI pausedGame
        Time.timeScale = 1f; //nastaví èas na 1
        shootingController.isPaused = false;
        SceneManager.LoadScene("MainMenuScreen"); //naète novou scénu
    }
    public void Retry()
    {
        deathMenu = GameObject.Find("DeathMenu");
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        shootingController.isPaused = false;
        SceneManager.LoadScene("MainGameScene");
    }
    
}
