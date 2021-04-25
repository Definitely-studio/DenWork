using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
	// Create object of class Item
    public Item item = new Item {itemType = Item.ItemType.RangedWeaponItem, amount = 1, itemLabel = "White Key"};
}
