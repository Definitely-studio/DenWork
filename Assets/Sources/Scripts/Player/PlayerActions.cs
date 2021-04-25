using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject equippedweapon;
    private PlayerOld player;
    public Camera cam;

    public AudioSource MActive1;
    public AudioSource MActive2;
    public AudioSource MActive3;

    // Start is called before the first frame update
    void Start()
    {
      player = GetComponent<PlayerOld>();
      
      //Устанавливаем начальное значение Пулек
      if(equippedweapon != null){
        if(equippedweapon.GetComponent<GunBase>() != null)
          {
              RescaleBullet(equippedweapon.GetComponent<GunBase>().GetCurrentBulletInMagazine(), equippedweapon.GetComponent<GunBase>().MaxBulletsInMagazine);
          }
      }

    }

    // Update is called once per frame
    void Update()
    {
     /* if(!(player.GetIsDead()))
      {
        InputActions();
      }*/



    }
    void FixedUpdate()
    {

    }

    void InputActions()
    {
      /*if(Input.GetButton("Fire1"))
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
          
      }*/

    }

    IEnumerator HealPlayer(int waitTime)
    {
    
        yield return new WaitForSeconds(1);

  }

    void Attack()
    {
      if(equippedweapon != null){
        if(equippedweapon.GetComponent<GunBase>() != null)
        {
            equippedweapon.GetComponent<GunBase>().Shoot();
            RescaleBullet(equippedweapon.GetComponent<GunBase>().GetCurrentBulletInMagazine(), equippedweapon.GetComponent<GunBase>().MaxBulletsInMagazine);
        }
      }
    }

    void RescaleBullet(int buller, int maxBullet)
    {
      if(GetComponent<PlayerOld>().UI != null)
        GetComponent<PlayerOld>().UI.GetComponent<UIGameMode>().ShowBullet(buller, maxBullet); //��������� �������
    }


    void OnTriggerStay2D(Collider2D col)
    {
      // Door opening
        if(col.gameObject.tag == "Door")
        {
          // if(player.GetComponent<Inventory>().itemList. col.gameObject.GetComponent<DoorController>().keyNeeded == );
          Destroy(col.gameObject);
          Debug.Log("Picked item");
        }   

              
    }

}
