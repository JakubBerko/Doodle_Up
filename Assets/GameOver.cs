using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class GameOver : MonoBehaviour
{
    public GameObject Doodler;
    private void OnBecameInvisible()
    {
        Destroy(Doodler);
        SceneManager.LoadScene("MainGameScene");
    }
}
