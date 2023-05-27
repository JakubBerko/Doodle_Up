using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinShopUI : MonoBehaviour
{
	[Header("Layout Settings")]
	[SerializeField] float skinSpacing = .5f;
	float skinHeight;

	[Header("UI elements")]
	//[SerializeField] Image selectedSkinIcon;
	[SerializeField] Transform ShopMenu;
	[SerializeField] Transform ShopSkinsContainer;
	[SerializeField] GameObject skinPrefab;
	[SerializeField] SkinDatabase skinDB;

	[Header("Shop Events")]
	[SerializeField] GameObject shopUI;
	[SerializeField] Button openShopButton;
	[SerializeField] Button closeShopButton;

	[SerializeField] TMP_Text coinsUI;
	int newSelectedSkinIndex = 0;
	int previousSelectedSkinIndex = 0;

	private void Start()
    {
		AddShopEvents();
		GenerateShopSkinsUI();
		SetSelectedCharacter();
		SelectSkinUI(GameDataManager.GetSelectedSkinIndex());
	}
	void SetSelectedCharacter()
	{
		//Get saved index
		int index = GameDataManager.GetSelectedSkinIndex();

		//Set selected character
		GameDataManager.SetSelectedSkin(skinDB.GetSkin(index), index);
	}
	private void GenerateShopSkinsUI()
    {
		for (int i = 0; i < GameDataManager.GetAllPurchasedSkin().Count; i++)
		{
			int purchasedSkinIndex = GameDataManager.GetPurchasedSkin(i);
			skinDB.PurchaseSkin(purchasedSkinIndex);
		}

		skinHeight = ShopSkinsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
		Destroy(ShopSkinsContainer.GetChild(0).gameObject);
		ShopSkinsContainer.DetachChildren();

		for (int i = 0; i < skinDB.SkinsCount; i++)
		{
			
			Skin skin = skinDB.GetSkin(i);
			SkinUI uiSkin = Instantiate(skinPrefab, ShopSkinsContainer).GetComponent<SkinUI>();

			
			uiSkin.SetSkinPosition(Vector2.down * i * (skinHeight + skinSpacing));

			
			uiSkin.gameObject.name = "Skin" + i + "-" + skin.name;

			
			uiSkin.SetSkinName(skin.name);
			uiSkin.SetSkinImage(skin.image);
			uiSkin.SetSkinPrice(skin.price);

			if (skin.isPurchased)
			{
				uiSkin.SetSkinAsPurchased();
				uiSkin.OnSkinSelect(i, OnSkinSelected);
			}
			else
			{
				uiSkin.SetSkinPrice(skin.price);
				uiSkin.OnSkinPurchase(i, OnSkinPurchased);
			}
			ShopSkinsContainer.GetComponent<RectTransform>().sizeDelta =
				Vector2.up * ((skinHeight + skinSpacing) * skinDB.SkinsCount + skinSpacing);
		}
	}
	void OnSkinSelected(int index)
	{
		SelectSkinUI(index);

		GameDataManager.SetSelectedSkin(skinDB.GetSkin(index), index);
	}
	void SelectSkinUI(int skinIndex)
    {
		previousSelectedSkinIndex = newSelectedSkinIndex;
		newSelectedSkinIndex = skinIndex;

		SkinUI prevUiSkin = GetSkinUI(previousSelectedSkinIndex);
		SkinUI newUiSkin = GetSkinUI(newSelectedSkinIndex);

		prevUiSkin.DeselectSkin();
		newUiSkin.SelectSkin();
	}
	SkinUI GetSkinUI(int index)
	{
		return ShopSkinsContainer.GetChild(index).GetComponent<SkinUI>();
	}
	void OnSkinPurchased(int index)
    {
		Skin skin = skinDB.GetSkin(index);
		SkinUI uiSkin = GetSkinUI(index);

		if (GameDataManager.CanSpendCoins(skin.price))
		{
			//Proceed with the purchase operation
			GameDataManager.SpendCoins(skin.price);
			//Update Coins UI text
			coinsUI.text = PlayerPrefs.GetFloat("coins").ToString();

			//Update DB's Data
			skinDB.PurchaseSkin(index);

			uiSkin.SetSkinAsPurchased();
			uiSkin.OnSkinSelect(index, OnSkinSelected);

			//Add purchased item to Shop Data
			GameDataManager.AddPurchasedSkin(index);
		}
        else
        {
			Debug.Log("Nemáš prachy");
        }
	}
	void AddShopEvents()
	{
		openShopButton.onClick.RemoveAllListeners();
		openShopButton.onClick.AddListener(OpenShop);

		closeShopButton.onClick.RemoveAllListeners();
		closeShopButton.onClick.AddListener(CloseShop);
	}
	void OpenShop()
	{
		shopUI.SetActive(true);
	}

	void CloseShop()
	{
		shopUI.SetActive(false);
	}
}
