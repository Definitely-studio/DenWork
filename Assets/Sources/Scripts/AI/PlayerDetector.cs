using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
  public float rayLenght = 1.0f;
  public Transform playerDetect;
  public Transform target;
  public Transform lastViewdPosition;
  public LayerMask layerMask;
  public Enemy enemy;
  bool playerisFound = false;
  bool canWeAttack = false;




    // Start is called before the first frame update
    void Start()
    {
        playerDetect = transform;
        target = GameObject.FindWithTag("Player").transform;
        enemy = GetComponentInParent<Enemy>();
        lastViewdPosition = target;

    }

    // Update is called once per frame
    void Update()
    {
      if(!enemy.GetIsDead())
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

      
      //Debug.Log(PlayerInfo.collider);

      if(PlayerInfo.collider != null && playerisFound == false){

        if(PlayerInfo.collider.gameObject.tag == "Player" )
        {
            playerisFound = true;
            enemy.enemyActions.PlayRoar();
            StopCoroutine("LookForPlayer");
            enemy.animationsController.SetPlayerLookFor(false);
            enemy.animationsController.SetPlayerFound(true);
        }
        else
        {
            playerisFound = false;
        }
      }
      else if(PlayerInfo.collider != null && playerisFound == true)
      {
         if(PlayerInfo.collider.gameObject.tag == "Player" )
        {
            playerisFound = true;
            StopCoroutine("LookForPlayer");
            enemy.animationsController.SetPlayerLookFor(false);
            enemy.animationsController.SetPlayerFound(true);
        }
        
          else
        {
          
          StartCoroutine(LookForPlayer(5f));
          playerisFound = false;
          enemy.animationsController.SetPlayerFound(false);
        }
      }
      else if(PlayerInfo.collider == null && playerisFound == true)
      {
          
          StartCoroutine(LookForPlayer(5f));
          playerisFound = false;
          enemy.animationsController.SetPlayerFound(false);
      }
      else if(PlayerInfo.collider == null && playerisFound == false)
      {
        playerisFound = false;
        enemy.animationsController.SetPlayerFound(false);
      }
      

    }

  public void SetLookForState()
  {
      StartCoroutine(LookForPlayer(5f));
  }
  IEnumerator LookForPlayer(float waitTime)
  {
    lastViewdPosition.position = new Vector3(target.position.x,target.position.y, target.position.z);
    enemy.animationsController.SetPlayerLookFor(true);
    yield return new WaitForSeconds(waitTime);
    enemy.animationsController.SetPlayerLookFor(false);
    //enemy.animationsController.SetPatrolKey(true);
    
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
