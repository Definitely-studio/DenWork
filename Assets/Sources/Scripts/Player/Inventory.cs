using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> itemList;
	public GameObject inventoryUI;
	private bool isInventoryActive = true;
	
	public Inventory() {
		itemList = new List<Item>();
		Debug.Log("Inventory");
	}

	private void Update()
	{
	    if( Input.GetKeyDown( KeyCode.I ) && isInventoryActive )  {
	    inventoryUI.SetActive(false);
	    isInventoryActive = false;
	}
	    else if( Input.GetKeyDown( KeyCode.I ) && !isInventoryActive )  {
	    inventoryUI.SetActive(true);
	    isInventoryActive = true;
	}	

}

}
