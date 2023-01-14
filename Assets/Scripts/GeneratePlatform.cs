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
        

        //Pokud je potøeba spawnout nové platformy, zavoláme managera a ten zavolá generaci platforem
        float playerY = playerTrans.position.y;
        if (playerY > spawnMorePlatformsIn)
        {
            PlatformManager(); //spawne platformy
        }
    }

    void PlatformManager()
    {
        //nastaví kdy se spawnou další platformy
        spawnMorePlatformsIn = playerTrans.position.y + 10;

        //spawne platformy dopøedu, aby hráè generování nevidìl
        GeneratePlatforms(spawnMorePlatformsIn + 35);
    }
    void GeneratePlatforms(float limit)
    {
        float spawnPosY = platformsSpawnLimit; //minimální výška kde se mají spawnovat (= spawned up to)
        while (spawnPosY <= limit) //dokud je pozice na Y menší jak max. limit pro spawnutí, bude spawnowat 
        {
            float spawnPosX = Random.Range(-2.5f, 2.5f); //odkud kam se spawnou na X
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 12.0f); //pozice spawnu

            Transform plat = (Transform)Instantiate(platform, spawnPos, Quaternion.identity); //vytvoøení platformy
            platforms.Add(plat); //pøidání do arraylistu pro budoucí využití

            spawnPosY += Random.Range(.5f, 1f); //odkud kam se spawnou na Y + opakování cyklu dokud while nebude true
        }
        platformsSpawnLimit = limit; //nastavení nového maxima kam se platformy spawnly
    }
    
}