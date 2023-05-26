using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinDatabase", menuName = "Shopping/Skins database")]
public class SkinDatabase : ScriptableObject
{
	public Skin[] skins;

	public int SkinsCount
	{
		get { return skins.Length; }
	}

	public Skin GetSkin(int index)
	{
		return skins [index];
	}

	public void PurchaseSkin(int index)
	{
		skins [index].isPurchased = true;
	}
}
