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
	public ItemType Type;
	public string ItemLabel = "Default item label";
	public string Name;
	public int Amount;
	public int Id;

	public Sprite Icon;


	protected Item(ItemType type, string label, int amount, int id, string name, Sprite icon)
	{
		Type = type;
		ItemLabel = label;
		Amount = amount;
		Id = id;
		Name = name;
		Icon = icon;
	}
}
