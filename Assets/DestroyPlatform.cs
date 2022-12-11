using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    public void OnBecameVisible()
    {
        Debug.Log("Platform is visible!");
    }
    public void OnBecameInvisible()
    {
        Destroy(gameObject);
        Debug.Log("Platform destroyed!");
    }
}
