using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject pausedGame;
    public void StartGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
