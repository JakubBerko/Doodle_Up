using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillDodler : MonoBehaviour
{
    private GameObject Doodler;
    private Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        Doodler = GameObject.FindGameObjectWithTag("Doodler");
        controller = Doodler.GetComponent<Controller>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Doodler)
        {
            controller.DeathHandler();
        }
    }
}
