
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventorySlot : MonoBehaviour {

    public enum SlotTypes{

        Backpack,
        Weapon,
        Quickslot
    }


    public Image IconImage;
    public int Amount
    {
        get
        {
            return amount;
        }
        set{
            amount = value;
            AmountText.text = amount.ToString();

            if(amount > 1) AmountText.enabled = true;   
            else AmountText.enabled = false; 
        }
    }

    public Text AmountText;
    public GameObject ItemObject;
    public Item Item;
    public SlotTypes SlotType = SlotTypes.Backpack;
    [SerializeField] private int amount;

    private void Start() {
        //Item = GetComponent<Item>();
    }

    private void Update() {
    }
}