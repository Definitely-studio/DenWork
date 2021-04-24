using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
  public float rayLenght = 1.0f;
  public Transform playerDetect;
  public Transform target;
  public LayerMask layerMask;
  public Enemy enemy;
  bool playerisFound = false;
  bool canWeAttack = false;




    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();

        if(playerisFound)
        {
          canWeAttack = true;
        }
        else
          canWeAttack = false;

        //Debug.Log("Player is" + playerisFound);

    }

    // детектирование через рэйкаст, временно не используется
    void DetectPlayer() {

      Vector2 lookDirection = new Vector2(target.position.x, target.position.y) - new Vector2(transform.position.x, transform.position.y);

      RaycastHit2D PlayerInfo = Physics2D.Raycast(playerDetect.position, lookDirection, rayLenght, layerMask);

      Debug.DrawRay(playerDetect.position, lookDirection * rayLenght, Color.red);

      
      Debug.Log(PlayerInfo.collider);
      if(PlayerInfo.collider != null){

        if(PlayerInfo.collider.gameObject.tag == "Player" )
        {
            playerisFound = true;
            Debug.Log(playerisFound);
            enemy.animationsController.SetPlayerFound(true);
        }
        else
            playerisFound = false;
        }
      else
        playerisFound = false;

    }




    void OnTriggerStay2D(Collider2D other)
    {
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }

    public bool GetCanWeShoot()
    {
        return canWeAttack;
    }


}
