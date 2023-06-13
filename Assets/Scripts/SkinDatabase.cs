using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinDatabase", menuName = "Shopping/Skins database")] //menu pro vytvoreni databaze
public class SkinDatabase : ScriptableObject
{
	public Skin[] skins; //pole se skiny

	public int SkinsCount
	{
		get { return skins.Length; } //poèet skinu si ziskavam z pole
	}

	public Skin GetSkin(int index)
	{
		return skins [index];
	}

	public void PurchaseSkin(int index) //nastavim u skinu purchased na true protoze je jiz koupeny
	{
		skins [index].isPurchased = true;
	}
}
