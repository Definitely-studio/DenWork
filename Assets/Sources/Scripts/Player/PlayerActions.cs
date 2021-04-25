using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private GameObject _gun;
    [SerializeField] private Input _input;
    private Player player;
    public Camera cam;
    public AudioSource MActive1;
    public AudioSource MActive2;
    public AudioSource MActive3;



     private void Awake()
    {
        _input = new Input();
        _input.Player.Shoot.performed += context => Shoot();
        /*_gun = Instantiate(gun, this.transform);
       
        _gun.transform.SetParent(this.transform);*/

    }


    // Start is called before the first frame update
    void Start()
    {
      player = GetComponent<Player>();
      
     /* //Устанавливаем начальное значение Пулек
      if(equippedweapon != null){
        if(equippedweapon.GetComponent<GunBase>() != null)
          {
              RescaleBullet(equippedweapon.GetComponent<GunBase>().GetCurrentBulletInMagazine(), equippedweapon.GetComponent<GunBase>().MaxBulletsInMagazine);
          }
      }*/

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


      }*/

    }

      private void Shoot()
    {
      if (_gun != null){
        _gun.GetComponent<Gun>().Shoot();

        }        
    }

    IEnumerator HealPlayer(int waitTime)
    {
        yield return new WaitForSeconds(1);
    }

    void Attack()
    {
      /*if(equippedweapon != null){
        if(equippedweapon.GetComponent<GunBase>() != null)
        {
            equippedweapon.GetComponent<GunBase>().Shoot();
            RescaleBullet(equippedweapon.GetComponent<GunBase>().GetCurrentBulletInMagazine(), equippedweapon.GetComponent<GunBase>().MaxBulletsInMagazine);
        }
      }*/
    }

    /*void RescaleBullet(int buller, int maxBullet)
    {
      if(GetComponent<PlayerOld>().UI != null)
        GetComponent<PlayerOld>().UI.GetComponent<UIGameMode>().ShowBullet(buller, maxBullet); //��������� �������
    }
*/

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
