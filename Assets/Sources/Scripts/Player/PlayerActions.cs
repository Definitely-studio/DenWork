using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject equippedweapon;
    private Player player;
    public Camera cam;
    private bool keyhere;
    private string keystring;

    public AudioSource MActive1;
    public AudioSource MActive2;
    public AudioSource MActive3;

    // Start is called before the first frame update
    void Start()
    {
      player = GetComponent<Player>();
//      RescaleBullet(equippedweapon.GetComponent<Gun>().currentBulletsInMagazine, equippedweapon.GetComponent<Gun>().MaxBulletsInMagazine);
    }

    // Update is called once per frame
    void Update()
    {
      if(!(player.GetIsDead()))
      {
        InputActions();
      }



    }
    void FixedUpdate()
    {

    }

    void InputActions()
    {
      if(Input.GetButton("Fire1"))
      {
            Attack();
      }


      if(Input.GetButtonDown("Submit"))
      {

      }

      if(Input.GetButtonDown("Ability1"))
      {
 

      }

      if(Input.GetButtonDown("Ability2"))
      {
      
        

      }

      if(Input.GetButtonDown("Ability3"))
      {
          
      }

    }

    IEnumerator HealPlayer(int waitTime)
    {
    
        yield return new WaitForSeconds(1);

  }

    void Attack()
    {
        if(equippedweapon)
        {
            equippedweapon.GetComponent<GunBase>().Shoot();
        }
    }

    void RescaleBullet(int buller, int maxBullet)
    {
        GetComponent<Player>().UI.GetComponent<UIGameMode>().ShowBullet(buller, maxBullet); //��������� �������
    }


    void OnTriggerEnter2D(Collider2D col) {
      // Door opening
        if(col.gameObject.tag == "Door")
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
          Debug.Log("keyhere = "+keyhere);
          // open door
          if(keyhere){
          Destroy(col.gameObject);
          }
          }
        }   
    }

}
