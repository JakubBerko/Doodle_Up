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
		//ziskej index
		int index = GameDataManager.GetSelectedSkinIndex();

		//nastav vybraný skin
		GameDataManager.SetSelectedSkin(skinDB.GetSkin(index), index);
	}
	private void GenerateShopSkinsUI()
    {
		for (int i = 0; i < GameDataManager.GetAllPurchasedSkin().Count; i++)
		{
			int purchasedSkinIndex = GameDataManager.GetPurchasedSkin(i); //ziskam vsechny koupene skiny
			skinDB.PurchaseSkin(purchasedSkinIndex);
		}

		skinHeight = ShopSkinsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
		Destroy(ShopSkinsContainer.GetChild(0).gameObject);
		ShopSkinsContainer.DetachChildren();

		for (int i = 0; i < skinDB.SkinsCount; i++)
		{
			
			Skin skin = skinDB.GetSkin(i); //ziskam skin
			SkinUI uiSkin = Instantiate(skinPrefab, ShopSkinsContainer).GetComponent<SkinUI>(); //vytvorim skin v ui

			
			uiSkin.SetSkinPosition(Vector2.down * i * (skinHeight + skinSpacing)); //pozicovani

			
			uiSkin.gameObject.name = "Skin" + i + "-" + skin.name; //jmeno skinu

			//nastaveni 
			uiSkin.SetSkinName(skin.name); 
			uiSkin.SetSkinImage(skin.image);
			uiSkin.SetSkinPrice(skin.price);
			uiSkin.SetSkinController(skin.animatorController);

			if (skin.isPurchased) //pokud je skin koupen nastavi se v ui jako koupeny pres SkinUI a je k vybrani
			{
				uiSkin.SetSkinAsPurchased();
				uiSkin.OnSkinSelect(i, OnSkinSelected);
			}
			else//pokud neni koupen tak se mu nastavi cena a je ke koupeni
			{
				uiSkin.SetSkinPrice(skin.price);
				uiSkin.OnSkinPurchase(i, OnSkinPurchased);
			}
			ShopSkinsContainer.GetComponent<RectTransform>().sizeDelta = //resizing
				Vector2.up * ((skinHeight + skinSpacing) * skinDB.SkinsCount + skinSpacing);
		}
	}
	void OnSkinSelected(int index) 
	{
		SelectSkinUI(index);

		GameDataManager.SetSelectedSkin(skinDB.GetSkin(index), index);
	}
	void SelectSkinUI(int skinIndex) //vybirani skinu
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
	void OnSkinPurchased(int index) //pri koupeni skinu
    {
		Skin skin = skinDB.GetSkin(index);//ziskam skin
		SkinUI uiSkin = GetSkinUI(index); //--

		if (GameDataManager.CanSpendCoins(skin.price))//pokud muzu koupit, utratim penize
		{
			//koupeni
			GameDataManager.SpendCoins(skin.price);
			//update textu penez
			coinsUI.text = PlayerPrefs.GetFloat("coins").ToString();

			//update databaze
			skinDB.PurchaseSkin(index);

			uiSkin.SetSkinAsPurchased();
			uiSkin.OnSkinSelect(index, OnSkinSelected);

			//pridani koupeneho skinu do dat
			GameDataManager.AddPurchasedSkin(index);
		}
        else //kdyz nemam penize
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
