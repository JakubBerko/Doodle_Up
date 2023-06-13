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
        pausedGame.SetActive(true); //zobrazí UI pausedGame
        Time.timeScale = 0f; //nastaví èas na 0
        shootingController.isPaused = true; //zastaví støílení
    }
    public void ResumeGame()
    {
        pausedGame.SetActive(false); //schová UI pausedGame
        Time.timeScale = 1f; //nastaví èas na 1
        shootingController.isPaused = false; //povolí støílení
    }
    public void ReturnToMainMenu()
    {
        deathMenu = GameObject.Find("DeathMenu"); //navíc øádek kodu
        pausedGame.SetActive(false); //schová UI pausedGame
        Time.timeScale = 1f; //nastaví èas na 1
        shootingController.isPaused = false;//povolí støílení
        SceneManager.LoadScene("MainMenuScreen"); //naète novou scénu
    }
    public void Retry()
    {
        deathMenu = GameObject.Find("DeathMenu"); 
        deathMenu.SetActive(false); //schová UI
        Time.timeScale = 1f;//nastaví èas na 1
        shootingController.isPaused = false;//povolí støílení
        SceneManager.LoadScene("MainGameScene"); //naète scenu
    }
    
}
