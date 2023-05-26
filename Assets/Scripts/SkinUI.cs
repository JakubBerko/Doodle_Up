using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SkinUI : MonoBehaviour
{
	[SerializeField] Color skinNotSelectedColor;
	[SerializeField] Color skinSelectedColor;

	[SerializeField] Image skinImage;
	[SerializeField] TMP_Text skinNameText;
	[SerializeField] TMP_Text skinPriceText;
	[SerializeField] Button skinPurchaseButton;

	[SerializeField] Button skinButton;
	[SerializeField] Image skin_Image;
	[SerializeField] Outline skinOutline;

	public void SetSkinPosition(Vector2 pos)
	{
		GetComponent<RectTransform>().anchoredPosition += pos;
	}

	public void SetSkinImage(Sprite sprite)
	{
		skinImage.sprite = sprite;
	}

	public void SetSkinName(string name)
	{
		skinNameText.text = name;
	}

	public void SetSkinPrice(int price)
	{
		skinPriceText.text = price.ToString();
	}
	public void SetSkinAsPurchased()
	{
		skinPurchaseButton.gameObject.SetActive(false);
		skinButton.interactable = true;

		skin_Image.color = skinNotSelectedColor;
	}
	public void OnSkinPurchase(int skinIndex, UnityAction<int> action)
	{
		skinPurchaseButton.onClick.RemoveAllListeners();
		skinPurchaseButton.onClick.AddListener(() => action.Invoke(skinIndex));
	}

	public void OnSkinSelect(int skinIndex, UnityAction<int> action)
	{
		skinButton.interactable = true;

		skinButton.onClick.RemoveAllListeners();
		skinButton.onClick.AddListener(() => action.Invoke(skinIndex));
	}
	public void SelectSkin()
	{
		skinOutline.enabled = true;
		skin_Image.color = skinSelectedColor;
		skinButton.interactable = false;
	}

	public void DeselectSkin()
	{
		skinOutline.enabled = false;
		skin_Image.color = skinNotSelectedColor;
		skinButton.interactable = true;
	}
}
