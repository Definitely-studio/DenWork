using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	public enum ItemType {
		RangedWeaponItem,
		MeleeWeaponItem,
		MiscItem,
		EdibleItem,
		ClothItem,
		AmmoItem,
		KeyItem
	}
	public ItemType itemType;
	public string itemLabel = "Default item label";
	public int amount;
}
