using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedGame : MonoBehaviour
{
    [SerializeField] GameObject pausedGame;
    public void StopGame()
    {
        pausedGame.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        pausedGame.SetActive(false);
        Time.timeScale = 1f;
    }
}
