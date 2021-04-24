using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	public enum ItemType {
		RangedWeapon,
		MeleeWeapon,
		MiscItem,
		Edible,
		Cloth,
		Ammo
	}
	public ItemType itemType;
	public int amount;
}
