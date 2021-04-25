using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList;
	public GameObject inventoryUI;
	private bool isInventoryActive = true;
	
	public Inventory() {
		itemList = new List<Item>();
		Debug.Log("Inventory");

		AddItem(new Item {itemType = Item.ItemType.RangedWeaponItem, amount = 1});
		Debug.Log(itemList.Count);
	}

	private void Update()
		{
			if( Input.GetKeyDown( KeyCode.I )) 
			{

				    if (isInventoryActive)  {
				    inventoryUI.SetActive(false);
				    isInventoryActive = false;
				}
				    else{
				    inventoryUI.SetActive(true);
				    isInventoryActive = true;
				}	
			}
		}
	public void AddItem(Item item) {
		itemList.Add(item);
	}

    void OnTriggerStay2D(Collider2D col)
    {
    	// Pickuping item func
        if(col.gameObject.tag == "Pickup" && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Submit")))
        {
          AddItem(col.gameObject.GetComponent<ItemWorld>().item);
          Destroy(col.gameObject);
          Debug.Log("Picked item");
        }   

              
    }



}
