using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public List<InventorySlot> CollectedItemSlots;
	public List<InventorySlot> EquippedItemSlots;
	public List<InventorySlot> StoryItemSlots;
	
	public Transform CollectedItem;
	public Transform EquippedItem;
	public Transform StoryItem;
    public List<Item> ItemList = new List<Item>();
	public GameObject inventoryUI;
	[SerializeField] private Input _input;
	[SerializeField] private GameObject crosshair;
	private bool isInventoryActive = true;
	private GameObject OverlapedItem;


  private void Awake()
    {
        _input = new Input();
        _input.Player.Invent.performed += context => ActivateInventory();
		_input.Player.Submit.performed += context => Pickup();
    }
	private void Start() {
		CollectSlots();
	}

	public InventoryManager() {
		
	}
	  private void OnEnable()
    {
        _input.Enable();
    }


	private void CollectSlots()
	{
		for (int i = 0; i< EquippedItem.childCount; i++)
		{
			if(EquippedItem.GetChild(i).TryGetComponent<InventorySlot>(out InventorySlot slot))
				EquippedItemSlots.Add(slot);
		}

		for (int i = 0; i< CollectedItem.childCount; i++)
		{
			if(CollectedItem.GetChild(i).TryGetComponent<InventorySlot>(out InventorySlot slot))
				CollectedItemSlots.Add(slot);
		}

		for (int i = 0; i< StoryItem.childCount; i++)
		{
			if(StoryItem.GetChild(i).TryGetComponent<InventorySlot>(out InventorySlot slot))
				StoryItemSlots.Add(slot);
		}
		

	}



	private void ActivateInventory()
	{
	
		if (isInventoryActive)  {
				inventoryUI.SetActive(false);
				isInventoryActive = false;
				Cursor.visible = false;
        		crosshair.SetActive(true);
    
				Time.timeScale = 1.0f;
			}
				else{
				inventoryUI.SetActive(true);
				isInventoryActive = true;
				Cursor.visible = true;
        		crosshair.SetActive(false);
				Time.timeScale = 1.0f;
			}	
	}

	private void Pickup()
	{
		if(OverlapedItem != null)
		{
			
			AddItem(OverlapedItem);
        	OverlapedItem.SetActive(false);
		}
	}

	public int AddItem(GameObject itemObject) {

		Item item = itemObject.GetComponent<Item>();
		foreach (InventorySlot slot in CollectedItemSlots)
		{
			if(slot.Item != null){
				if(slot.Item.Type == item.Type)
				{
					slot.Amount += item.Amount;
					return 0;		
				}
			}
		}

		foreach (InventorySlot slot in CollectedItemSlots)
		{
			if(slot.Item == null){

				slot.Item = item;
				slot.ItemObject = itemObject;
				slot.Amount = item.Amount;	

				slot.IconImage.sprite = item.Icon;	
				slot.IconImage.color = new Color(1,1,1,1);
				slot.AmountText.enabled = true;
				return 0;
			}
		}
		return 0;
	}

		
	public void OverlapItem(GameObject OverlapedItem)
	{
		Debug.Log("PickUp");
		this.OverlapedItem = OverlapedItem;
	}

	public void OverlapItem()
	{
		this.OverlapedItem = null;
	}



    void OnTriggerStay2D(Collider2D collider)
    {
    	// Pickuping item func
        if(collider.gameObject.TryGetComponent(out Item item))
        {
		  Debug.Log("PickUp");
		
		  OverlapedItem = collider.gameObject;

        }   

              
    }
	void OnTriggerExit2D(Collider2D collider)
    {
    	// Pickuping item func
        if(collider.gameObject.TryGetComponent(out Item item))
        {
		  OverlapedItem = null;
		  

        }   

              
    }



}
