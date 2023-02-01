using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject[] monsterPrefabs;

    public GameObject player;

    private Transform playerTrans;
    private float platformsSpawnLimit = 0.0f;
    private float spawnMorePlatformsIn = 0.0f;


    void Awake()
    {

        playerTrans = player.transform;

        GenerateMapFce(30.0f);
    }

    void Update()
    {
        //Pokud je pot�eba spawnout nov� platformy, zavol�me managera a ten zavol� generaci platforem
        float playerY = playerTrans.position.y;
        if (playerY > spawnMorePlatformsIn)
        {
            GenerationManager(); //spawne platformy
        }
    }

    void GenerationManager()
    {
        //nastav� kdy se spawnou dal�� platformy
        spawnMorePlatformsIn = playerTrans.position.y + 5;

        //spawne platformy dop�edu, aby hr�� generov�n� nevid�l
        GenerateMapFce(spawnMorePlatformsIn + 15);
    }

    void GenerateMapFce(float limit)
    {
        float spawnPosY = platformsSpawnLimit;
        while (spawnPosY <= limit)
        {
            float spawnPosX = Random.Range(-2.5f, 2.5f);
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 12.0f);

            int prefabIndex;
            float randomNumber = Random.Range(0f, 1f);
            if (randomNumber < 0.76f)
            {
                prefabIndex = 0; // platforma na 0 indexu se spawne vicekrat
            }
            else
            {
                prefabIndex = Random.Range(1, prefabs.Length); // zbytek platforem
            }

            Transform plat = (Transform)Instantiate(prefabs[prefabIndex].transform, spawnPos, Quaternion.identity);

            //monsters
            float monsterSpawnPosX = Random.Range(-2.5f, 2.5f);
            Vector3 monsterSpawnPos = new Vector3(monsterSpawnPosX, spawnPosY, 12.0f);
            int MonsterPrefabIndex;
            float MonsterRandomNumber = Random.Range(0f, 1f);
            if (MonsterRandomNumber < 0.03f)
            {
                MonsterPrefabIndex = Random.Range(0, monsterPrefabs.Length);
                Transform monst = (Transform)Instantiate(monsterPrefabs[MonsterPrefabIndex].transform, monsterSpawnPos, Quaternion.identity);
            }
            spawnPosY += Random.Range(0.5f, 1.5f);
        }
        platformsSpawnLimit = limit;
    }
}




//OLDER CODE 
/*
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
    spawnMorePlatformsIn = playerTrans.position.y + 5;

    //spawne platformy dop�edu, aby hr�� generov�n� nevid�l
    GeneratePlatforms(spawnMorePlatformsIn + 15);
}
void GeneratePlatforms(float limit)
{
    float spawnPosY = platformsSpawnLimit; //minim�ln� v��ka kde se maj� spawnovat (= spawned up to)
    while (spawnPosY <= limit) //dokud je pozice na Y men�� jak max. limit pro spawnut�, bude spawnowat 
    {
        float spawnPosX = Random.Range(-2.5f, 2.5f); //odkud kam se spawnou na X
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 12.0f); //pozice spawnu

        Transform plat = (Transform)Instantiate(platform, spawnPos, Quaternion.identity); //vytvo�en� platformy
        //platforms.Add(plat); //p�id�n� do arraylistu pro budouc� vyu�it�

        spawnPosY += Random.Range(.3f, 1f); //odkud kam se spawnou na Y + opakov�n� cyklu dokud while nebude true
    }
    platformsSpawnLimit = limit; //nastaven� nov�ho maxima kam se platformy spawnly
}
*/







































/*

    public GameObject[] prefabs;

    void Start()
    {
        // Instantiate the prefabs
        for (int i = 0; i < prefabs.Length; i++)
        {
            Instantiate(prefabs[i], new Vector3(i, 0, 0), Quaternion.identity);
        }
    }
 */