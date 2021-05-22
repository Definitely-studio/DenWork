using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerActions : MonoBehaviour
{
	public Text uiText;
  public GameObject equippedweapon;
  private Player player;
  public Camera cam;
  private bool keyhere = false;
  private string keystring;
  public InventoryManager Inventory;
  [SerializeField] private Input _input;

  public AudioSource Sound;
  public AudioClip DeathSound;
  public AudioClip DamageSound;
  public GameMenu gameMenu;
  public UIGameMode ui;
  public GameObject OverlappedItem;


    // Start is called before the first frame update

  private void Awake(){
    ui = GameObject.Find("CanvasUI").GetComponent<UIGameMode>();
    gameMenu = GameObject.Find("CanvasUI").GetComponent<GameMenu>();
    Inventory = GameObject.Find("CanvasUI").GetComponentInChildren<InventoryManager>();
    
    
  }



  #region OnTriggers
  void OnTriggerEnter2D(Collider2D collider) {
    // Door opening
    if(collider.gameObject.tag == "Door")
    {
      uiText.text = "Press E to open the door";
    }
      
    if(collider.gameObject.layer == LayerMask.NameToLayer("Pickup"))
    {
      if(Inventory!=null)
        Inventory.OverlapItem(collider.gameObject);
    }
      
  }

  void OnTriggerStay2D(Collider2D collider) {

    // Door opening
      /*if(col.gameObject.tag == "Door")
      {
          
          // Check if key is need
          if(col.gameObject.GetComponent<DoorController>().keyName != null) {
          // Check if player have needed key

          keystring = col.gameObject.GetComponent<DoorController>().keyName;

          // search for key
          foreach(Item it in player.GetComponent<Inventory>().itemList) {
            if (it.itemLabel == keystring)
            {keyhere = true;}
          }

          // open door
          if( Input.GetKeyDown( KeyCode.E )) {
            
            if(keyhere){
            Destroy(col.gameObject);
            }
            else {uiText.text = "You don't have the nessesary key";}
          }
        }
      }*/
  }

    void OnTriggerExit2D(Collider2D collider) {
    // Door opening
      if(collider.gameObject.tag == "Door")
      {
        uiText.text = null;
      }

      if(collider.gameObject.layer == LayerMask.NameToLayer("Pickup"))
      {
        if(Inventory!=null)
          Inventory.OverlapItem();
      }

  }
  #endregion



  public void ChangeHP(int deltaHP)
  {
    if(player!= null ){

      if (deltaHP <0)
      {
        if(Sound != null)
        {
          Sound.PlayOneShot(DamageSound);
        }
      }

      player.SetHP(player.GetHP() + deltaHP);
    
      ui.SetHealSlider(player.GetHP(), player.GetMaxHP());
      if(player.GetHP() <= 0 && gameObject.GetComponent<Collider2D>().enabled == true)
      {
          Death();
      }
    }
  }

  private void Death()
  {
    Collider2D[] colliders =  gameObject.GetComponents<Collider2D>();
    foreach (Collider2D item in colliders)
    {
        item.enabled = false;
    }
    
    player.SetIsDead(true);

    if(Sound != null)
      {
        Sound.PlayOneShot(DeathSound);
      }
    Destroy(gameObject);
    if(gameMenu!= null)
    {
      gameMenu.ActivatePostPortus();
      //gameMenu.RestartGame();
    }

  }


   

}
