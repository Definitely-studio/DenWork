using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList;
	public GameObject inventoryUI;
	[SerializeField] private Input _input;
	private bool isInventoryActive = true;
	private bool isPickupItemOverlap = false;
	private GameObject OverlapedItem;
	



  private void Awake()
    {
        _input = new Input();
        _input.Player.Invent.performed += context => ActivateInventory();
		_input.Player.Submit.performed += context => Pickup();

    }


	public Inventory() {
		itemList = new List<Item>();
		Debug.Log("Inventory");

		AddItem(new Item {itemType = Item.ItemType.RangedWeaponItem, amount = 1});
		Debug.Log(itemList.Count);
	}



	private void ActivateInventory()
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

	private void Pickup()
	{
		if(OverlapedItem != null)
		{
			AddItem(OverlapedItem.GetComponent<ItemWorld>().item);
        	Destroy(OverlapedItem);
		}
	}
	private void Update()
		{
			
		}
	public void AddItem(Item item) {

		itemList.Add(item);
	}

    void OnTriggerStay2D(Collider2D col)
    {
    	// Pickuping item func
        if(col.gameObject.tag == "Pickup")
        {
		  isPickupItemOverlap = true;
		  OverlapedItem = col.gameObject;

        }   

              
    }
	void OnTriggerExit2D(Collider2D col)
    {
    	// Pickuping item func
        if(col.gameObject.tag == "Pickup")
        {
		  OverlapedItem = null;
		  isPickupItemOverlap = false;

        }   

              
    }



}
