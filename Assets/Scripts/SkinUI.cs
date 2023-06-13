using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SkinUI : MonoBehaviour
{
	[SerializeField] Color skinNotSelectedColor;
	[SerializeField] Color skinSelectedColor;

	[SerializeField] Image skinImage;
	[SerializeField] RuntimeAnimatorController skinController;
	[SerializeField] TMP_Text skinNameText;
	[SerializeField] TMP_Text skinPriceText;
	[SerializeField] Button skinPurchaseButton;

	[SerializeField] Button skinButton;
	[SerializeField] Image skin_Image;
	[SerializeField] Outline skinOutline;

	public void SetSkinPosition(Vector2 pos) //pozice skinu
	{
		GetComponent<RectTransform>().anchoredPosition += pos;
	}

	public void SetSkinImage(Sprite sprite) //nastaveni obrazku skinu
	{
		skinImage.sprite = sprite;
	}
	public void SetSkinController(RuntimeAnimatorController runtimeAnimatorController) //nastaveni animator komponentu skinu
	{
		skinController = runtimeAnimatorController;
    }

	public void SetSkinName(string name) //nastaveni jmena skinu
	{
		skinNameText.text = name;
	}

	public void SetSkinPrice(int price) //nastaveni ceny skinu
	{
		skinPriceText.text = price.ToString();
	}
	public void SetSkinAsPurchased() //nastaveni skinu na koupeny
	{
		skinPurchaseButton.gameObject.SetActive(false);
		skinButton.interactable = true;

		skin_Image.color = skinNotSelectedColor;
	}
	public void OnSkinPurchase(int skinIndex, UnityAction<int> action) //pri koupi skinu
	{
		skinPurchaseButton.onClick.RemoveAllListeners(); //odstrani listenery
		skinPurchaseButton.onClick.AddListener(() => action.Invoke(skinIndex)); //prida listenery, ktere pri kliknuti na button poslou index koupeneho skinu
	}

	public void OnSkinSelect(int skinIndex, UnityAction<int> action)
	{
		skinButton.interactable = true;

		skinButton.onClick.RemoveAllListeners(); //odstrani listenery
		skinButton.onClick.AddListener(() => action.Invoke(skinIndex)); //prida listenery, ktere pri kliknuti na button poslou index vybraneho skinu
	}
	public void SelectSkin() //vybrat skin? 
	{
		skinOutline.enabled = true;
		skin_Image.color = skinSelectedColor;
		skinButton.interactable = false;
	}

	public void DeselectSkin() //od-vybrat skin? 
	{
		skinOutline.enabled = false;
		skin_Image.color = skinNotSelectedColor;
		skinButton.interactable = true;
	}
}
