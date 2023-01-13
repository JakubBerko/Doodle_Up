using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneratePlatform : MonoBehaviour
{
    

    public GameObject[] prefabs;

    public Transform platform;
    public GameObject player;

    private Transform playerTrans;
    private float platformsSpawnLimit = 0.0f;
    private ArrayList platforms;
    private float spawnMorePlatformsIn = 0.0f;


    void Awake()
    {
        playerTrans = player.transform;
        platforms = new ArrayList();

        GeneratePlatforms(30.0f);
    }

    void Start()
    {
        
    }

    void Update()
    {
        

        //Do we need to spawn new platforms yet? (we do this every X meters we climb)
        float playerY = playerTrans.position.y;
        if (playerY > spawnMorePlatformsIn)
        {
            PlatformManager(); //Spawn new platforms
        }
    }

    void PlatformManager()
    {
        //update next platform check
        spawnMorePlatformsIn = playerTrans.position.y + 10;

        //Spawn new platforms, 30 units in advance
        GeneratePlatforms(spawnMorePlatformsIn + 35);
    }
    void GeneratePlatforms(float limit)
    {
        float spawnPosY = platformsSpawnLimit;
        while (spawnPosY <= limit)
        {
            float spawnPosX = Random.Range(-2.5f, 2.5f);
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 12.0f);

            Transform plat = (Transform)Instantiate(platform, spawnPos, Quaternion.identity);
            platforms.Add(plat);

            spawnPosY += Random.Range(.5f, 1f);
        }
        platformsSpawnLimit = limit;
    }
    
}