
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventorySlot : MonoBehaviour {

    public UnityEvent<int> OnAmountChange;
    public Image IconImage;
    [SerializeField] private int amount;
    public int Amount
    {
        get
        {
            return amount;
        }
        set{
            amount = value;
            AmountText.text = amount.ToString();
        }
    }

    public Text AmountText;
    public GameObject ItemObject;
    public Item Item;

    private void Start() {
        Item = GetComponent<Item>();

    }

    private void Update() {
        
    }

}