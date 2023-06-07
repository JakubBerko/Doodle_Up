using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillDodler : MonoBehaviour
{
    private GameObject Doodler;
    // Start is called before the first frame update
    void Start()
    {
        Doodler = GameObject.FindGameObjectWithTag("Doodler");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Doodler)
        {
            SceneManager.LoadScene("MainMenuScreen"); //TODO HandleDeath() + vsude kde umre doodler !!!
        }
    }
}
