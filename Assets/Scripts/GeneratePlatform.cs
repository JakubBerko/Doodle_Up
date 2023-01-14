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
        

        //Pokud je pot�eba spawnout nov� platformy, zavol�me managera a ten zavol� generaci platforem
        float playerY = playerTrans.position.y;
        if (playerY > spawnMorePlatformsIn)
        {
            PlatformManager(); //spawne platformy
        }
    }

    void PlatformManager()
    {
        //nastav� kdy se spawnou dal�� platformy
        spawnMorePlatformsIn = playerTrans.position.y + 10;

        //spawne platformy dop�edu, aby hr�� generov�n� nevid�l
        GeneratePlatforms(spawnMorePlatformsIn + 35);
    }
    void GeneratePlatforms(float limit)
    {
        float spawnPosY = platformsSpawnLimit; //minim�ln� v��ka kde se maj� spawnovat (= spawned up to)
        while (spawnPosY <= limit) //dokud je pozice na Y men�� jak max. limit pro spawnut�, bude spawnowat 
        {
            float spawnPosX = Random.Range(-2.5f, 2.5f); //odkud kam se spawnou na X
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 12.0f); //pozice spawnu

            Transform plat = (Transform)Instantiate(platform, spawnPos, Quaternion.identity); //vytvo�en� platformy
            platforms.Add(plat); //p�id�n� do arraylistu pro budouc� vyu�it�

            spawnPosY += Random.Range(.5f, 1f); //odkud kam se spawnou na Y + opakov�n� cyklu dokud while nebude true
        }
        platformsSpawnLimit = limit; //nastaven� nov�ho maxima kam se platformy spawnly
    }
    
}