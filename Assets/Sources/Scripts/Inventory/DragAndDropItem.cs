using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// IPointerDownHandler - Следит за нажатиями мышки по объекту на котором висит этот скрипт
/// IPointerUpHandler - Следит за отпусканием мышки по объекту на котором висит этот скрипт
/// IDragHandler - Следит за тем не водим ли мы нажатую мышку по объекту
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private InventorySlot oldSlot;
    [SerializeField] private Input input;
    [SerializeField] private GameObject player;
    
     void Awake()
    {
        input = new Input();
        /*input.UI.Clicl.performed += context => ClickPresed();
        input.UI.LeftMouseRelease.performed += context => ClickReleased();
        input.UI.LeftMouseHold.performed += context => Hold();*/
        oldSlot = transform.GetComponentInParent<InventorySlot>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /* void ClickPresed()
    {
        lmbIsPresed = true;
        if(hittedNode != null){
            firstNode = hittedNode;
        }

    }*/

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag Event");
        Debug.Log("oldSlot.ItemObject");
        // Если слот пустой, то мы не выполняем то что ниже return;
        //if (oldSlot.ItemObject == null)
           // return;
        Vector2 newPosition = new Vector2(eventData.delta.x, eventData.delta.y);
        GetComponent<RectTransform>().anchoredPosition += newPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
         Debug.Log("OnPointerDown Event");
        if (oldSlot.ItemObject == null)
            return;
        //Делаем картинку прозрачнее
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        // Делаем так чтобы нажатия мышкой не игнорировали эту картинку
        GetComponentInChildren<Image>().raycastTarget = false;
        // Делаем наш DraggableObject ребенком InventoryPanel чтобы DraggableObject был над другими слотами инвенторя
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp Event");
        if (oldSlot.ItemObject == null)
            return;
        // Делаем картинку опять не прозрачной
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        // И чтобы мышка опять могла ее засечь
        GetComponentInChildren<Image>().raycastTarget = true;

        //Поставить DraggableObject обратно в свой старый слот
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        //Если мышка отпущена над объектом по имени UIPanel, то...
        
        if (eventData.pointerCurrentRaycast.gameObject.name == "InventoryOut")
        {

            oldSlot.ItemObject.transform.position = player.transform.position;
            oldSlot.ItemObject.SetActive(true);
            
            // Устанавливаем количество объектов такое какое было в слоте
            oldSlot.ItemObject.GetComponent<Item>().Amount = oldSlot.Amount;
            //itemObject.GetComponent<Item>().amount = oldSlot.amount;
            // убираем значения InventorySlot
            NullifySlotData();

            
        }
        else if( eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            if(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != oldSlot)
            {
                //Перемещаем данные из одного слота в другой
                ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>());
            }
            
        }
       
    }


    void NullifySlotData()
    {
        // убираем значения InventorySlot
        oldSlot.Item = null;
        oldSlot.Amount = 0;
        oldSlot.IconImage.sprite = null;
        oldSlot.IconImage.color = new Color(1, 1, 1, 0f);;
        oldSlot.ItemObject = null;
        oldSlot.Amount = 0;
        oldSlot.AmountText.enabled = false;

    }


    void ExchangeSlotData(InventorySlot newSlot)
    {

        GameObject itemObject = null;
        Sprite icon  = null;
        Item item  = null;
        int amount = 0; 
        // Временно храним данные newSlot в отдельных переменных
        if(newSlot.ItemObject != null)
        {
            itemObject = newSlot.ItemObject;
            icon = newSlot.IconImage.sprite;
            item = newSlot.Item;
            amount = newSlot.Amount; 
        }
       
        // Заменяем значения newSlot на значения oldSlot

        if (oldSlot.ItemObject != null)
        {
            newSlot.Item =   oldSlot.Item;;
            newSlot.IconImage.sprite = oldSlot.IconImage.sprite;
            newSlot.Amount = oldSlot.Amount;
            newSlot.ItemObject = oldSlot.ItemObject;
            newSlot.IconImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            newSlot.AmountText.enabled = true;
        }
        else
        {
            newSlot.IconImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.IconImage.GetComponent<Image>().sprite = null;
            newSlot.AmountText.enabled = false;
        }
        
        // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
       

        oldSlot.Item = item;
        oldSlot.IconImage.sprite = icon;
        oldSlot.Amount = amount;
        oldSlot.ItemObject = itemObject;

        if (itemObject != null)
        {
            newSlot.IconImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            oldSlot.AmountText.enabled = true;
        }
        else
        {
            oldSlot.IconImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.AmountText.enabled = false;
            oldSlot.IconImage.GetComponent<Image>().sprite = null;
            
        }     
    }
}