using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadShopCoins : MonoBehaviour
{
    public TextMeshProUGUI coins;
    private void Start()
    {
        coins.text = PlayerPrefs.GetFloat("coins").ToString();
    }
}
