using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject[] monsterPrefabs;
    public GameObject[] powerUpPrefabs;
    public GameObject coinPrefab;

    public GameObject player;

    private Transform playerTrans;
    private float platformsSpawnLimit = 0.0f;
    private float spawnMorePlatformsIn = 0.0f;


    void Awake()
    {

        playerTrans = player.transform; //ziskam player transform

        GenerateMapFce(30.0f); //vygeneruji start mapy
    }

    void Update()
    {
        //Pokud je potøeba spawnout nové platformy, zavoláme managera a ten zavolá generaci platforem
        float playerY = playerTrans.position.y;
        if (playerY > spawnMorePlatformsIn)
        {
            GenerationManager(); //spawne platformy
        }
    }

    void GenerationManager()
    {
        //nastaví kdy se spawnou další platformy
        spawnMorePlatformsIn = playerTrans.position.y + 5;

        //spawne platformy dopøedu, aby hráè generování nevidìl
        GenerateMapFce(spawnMorePlatformsIn + 15);
    }

    void GenerateMapFce(float limit)
    {
        float spawnPosY = platformsSpawnLimit;
        while (spawnPosY <= limit)
        {
            //platformy
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

            //spawnování coinù
            
            if (randomNumber < 0.2)
            {
                Transform coinSpawnPoint = plat.Find("CoinSpawnLocation");

                if (coinSpawnPoint != null)
                {
                    GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, coinPrefab.transform.rotation);
                    coin.transform.parent = plat;
                }
                else
                {
                    Debug.LogError("Coin spawn point neexistuje: " + plat.name);
                }
            }
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

            //powerUps
            float powerUpSpawnPosX = Random.Range(-2.5f, 2.5f);
            Vector3 powerUpSpawnPos = new Vector3(powerUpSpawnPosX, spawnPosY, 12.0f);
            int powerUpPrefabIndex;
            float powerUpRandomNumber = Random.Range(0f, 1f);
            if (powerUpRandomNumber < 0.02f)
            {
                powerUpPrefabIndex = Random.Range(0, powerUpPrefabs.Length);
                Transform powerUp = (Transform)Instantiate(powerUpPrefabs[powerUpPrefabIndex].transform, powerUpSpawnPos, Quaternion.identity);
            }
            spawnPosY += Random.Range(0.5f, 1.5f);
        }
        platformsSpawnLimit = limit;
    }
}