using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	[System.Serializable]
	public struct Skin
	{
		public Sprite image;
		public string name;
		public int price;
		public RuntimeAnimatorController animatorController;

	public bool isPurchased;
	}