using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> playerInventory = new List<string>() {};
	public GameObject inventoryUI;
	private bool isInventoryActive = true;
	void Start()
	{
		playerInventory.Add("Testname");
	}

	private void Update()
	{
	    if( Input.GetKeyDown( KeyCode.I ) && isInventoryActive )  {
	    inventoryUI.SetActive(false);
	    isInventoryActive = false;
	}
}

}
