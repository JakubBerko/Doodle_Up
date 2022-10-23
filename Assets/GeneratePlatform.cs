using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    public GameObject platform;

    public int platformCount = 300;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPos = new Vector3();

        for (int i = 0; i < platformCount; i++)
        {
            spawnPos.y += Random.Range(.5f, 2f);
            spawnPos.x = Random.Range(-2.5f, 2.5f);
            Instantiate(platform, spawnPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
