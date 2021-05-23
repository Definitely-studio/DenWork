using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Item : MonoBehaviour {

	
	public enum ItemType {
		RangedWeaponItem,
		MeleeWeaponItem,
		MiscItem,
		EdibleItem,
		ClothItem,
		AmmoItem,
		KeyItem
	}
	
	[Header("Item Base Variables")]

	public ItemType Type;
	public string Name;
	
	public int Amount = 1;
	/*{
		get {return amount;}
		set {
			amount = value;
			if(amount == 0) Destroy(gameObject);
		}
	}*/
	public int Id;
	public Sprite Icon;
	//protected int amount;


/*
	protected Item(ItemType type, int amount, int id, string name, Sprite icon)
	{
		Type = type;
		Amount = amount;
		Id = id;
		Name = name;
		Icon = icon;
	}*/
}
