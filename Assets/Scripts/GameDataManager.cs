using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SkinsShopData
{
    public List<int> purchasedSkinsIndexes = new List<int>();
}

[System.Serializable]
public class PlayerData
{
    public int coins = 0;
    public int selectedSkinIndex = 0;
}

public static class GameDataManager
{
    static PlayerData playerData = new PlayerData();
    static SkinsShopData skinsShopData = new SkinsShopData();

    static Skin selectedSkin;

    static string skinsShopDataFilePath = Application.persistentDataPath + "/skins-shop-data.json"; 

    static GameDataManager()
    {
        LoadSkinsShopData();
    }

    // Player Data Methods -----------------------------------------------------------------------------
    public static Skin GetSelectedCharacter() //vrátí vybraný skin
    {
        return selectedSkin;
    }

    public static void SetSelectedSkin(Skin skin, int index) //urèi vybraný skin
    {
        selectedSkin = skin;
        playerData.selectedSkinIndex = index;
        SavePlayerData();
    }

    public static int GetSelectedSkinIndex()
    {
        return playerData.selectedSkinIndex;
    }

    public static float GetCoins()
    {
        return PlayerPrefs.GetFloat("coins");
    }

    public static bool CanSpendCoins(int amount)
    {
        return (GetCoins() >= amount);
    }

    public static void SpendCoins(int amount)
    {
        int currentCoins = (int)GetCoins();
        currentCoins -= amount;
        PlayerPrefs.SetFloat("coins", currentCoins);
        SavePlayerData();
    }

    static void SavePlayerData() //ulozi data o hraci do jsonu
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(skinsShopDataFilePath, json);
        Debug.Log("<color=magenta>[PlayerData] Saved.</color>");
    }

                                                //funkce pro obchod
    public static void AddPurchasedSkin(int skinIndex)
    {
        skinsShopData.purchasedSkinsIndexes.Add(skinIndex);
        SaveSkinsShopData();
    }

    public static List<int> GetAllPurchasedSkin()
    {
        return skinsShopData.purchasedSkinsIndexes;
    }

    public static int GetPurchasedSkin(int index)
    {
        return skinsShopData.purchasedSkinsIndexes[index];
    }

    static void LoadSkinsShopData()
    {
        if (File.Exists(skinsShopDataFilePath))
        {
            string json = File.ReadAllText(skinsShopDataFilePath);
            skinsShopData = JsonUtility.FromJson<SkinsShopData>(json);
            Debug.Log("<color=green>[CharactersShopData] Loaded.</color>");
        }
        else
        {
            Debug.Log("<color=orange>[CharactersShopData] File not found. Creating new file.</color>");
            SaveSkinsShopData();
        }
    }

    static void SaveSkinsShopData() //ulozi data o itemech shopu do jsonu
    {
        string json = JsonUtility.ToJson(skinsShopData);
        File.WriteAllText(skinsShopDataFilePath, json);
        Debug.Log("<color=magenta>[CharactersShopData] Saved.</color>");
    }
}