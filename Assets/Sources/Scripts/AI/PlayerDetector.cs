using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
  public float rayLenght = 1.0f;
  public Transform playerDetect;
  public Transform target;
  bool playerisFound;
  bool thereIsEnemyOnTheWay;
  bool canWeShoot;
  public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        playerisFound = false;
        thereIsEnemyOnTheWay = false;
        canWeShoot = false;

    }

    // Update is called once per frame
    void Update()
    {
        //DetectPlayer();

        if(playerisFound && !thereIsEnemyOnTheWay)
        {
          canWeShoot = true;
        }
        else
          canWeShoot = false;

        //Debug.Log("Player is" + playerisFound);

    }

    // детектирование через рэйкаст, временно не используется
    void DetectPlayer() {
      Vector2 lookDirection = new Vector2(target.position.x, target.position.y) - new Vector2(transform.position.x, transform.position.y);

      RaycastHit2D PlayerInfo = Physics2D.Raycast(playerDetect.position, lookDirection, rayLenght, layerMask);

    /*Debug.DrawRay(playerDetect.position, lookDirection * rayLenght, Color.red);

      Debug.Log(playerisFound);*/
    /*  if(PlayerInfo.collider == null){
      //    Debug.Log("Player info null");
        }*/

      if(PlayerInfo.collider != null){

        Debug.DrawRay(playerDetect.position, lookDirection * rayLenght, Color.red);

        if(PlayerInfo.collider.gameObject.tag == "Player" )
        {

            playerisFound = true;
        }
        else
            playerisFound = false;
        }
      else
        playerisFound = false;

    }




    void OnTriggerStay2D(Collider2D other)
    {
      if(other.gameObject.tag == "Player"){
        playerisFound = true;

      }

      if(other.gameObject.tag == "Enemy" && other.gameObject != gameObject ){
        thereIsEnemyOnTheWay = true;
      }

    }

    void OnTriggerExit2D(Collider2D other)
    {
      if(other.gameObject.tag == "Player"){
        playerisFound = false;
      }
      if(other.gameObject.tag == "Enemy"){
        thereIsEnemyOnTheWay = false;
      }
    }

    public bool GetCanWeShoot()
    {
        return canWeShoot;
    }


}
