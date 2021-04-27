using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PawnBase
{

    // значние отрицательно т.к. метод, который считает HP добавляет отрицательное значение и уменьшает так хп
    public float attackCooldownTime = 2.0f;
   
    public States state;
    
    public GameObject Player;
    public Agent agent;
    public PlayerDetector playerDetector;
    public EnemyActions enemyActions;
    public EnemyMeleeWeapon enemyMeleeWeapon;
    public float destroyTime = 3f;
    public GameObject[] wayPoint;
    public EnemyAnimationsController animationsController;
    // приватные поля
    protected Rigidbody2D rb;
    protected bool isAttackCooldown = false;
    private Transform target;


    public AudioSource AudioDead;
    public int currentWP;


    // Start is called before the first frame update
    protected override void Start()
    {
      base.Start();
        currentWP = 0;
        Player = GameObject.FindWithTag("Player");
        target = GameObject.FindWithTag("Player").transform;
        state = (state != States.passive) ? States.lookingfor : States.passive;
        rb = GetComponent<Rigidbody2D> ();

      
    }


    // Update is called once per frame
    public virtual void Update()
    {


      // проверяем, что ИИ не мертв
      if(state != States.dead && state != States.passive)
      {
        transform.position = new Vector3 (transform.position.x,  transform.position.y, transform.position.y * 0.01f);
        
        
       /*  if(WeaponSocket != null)
        {
          rangedWeaponRotation();
        }*/
        
        
      }


    }

    public void SetCurrentWP(int value)
    {
      currentWP = value;
    }
    public int GetCurrentWP()
    {
      return currentWP;
    }

    public Rigidbody2D GetRigidBody(){

      return rb;
    }


    // петод разворачивающий оружие в сторону игрока
    void rangedWeaponRotation()
    {

      if(target != null && !GetIsDead()){
        //Debug.Log("rangedWeaponRotation");
        Vector2 lookDirection = GetLookAtDirection();
        float angle = Mathf.Atan2(lookDirection.y,lookDirection.x) * Mathf.Rad2Deg - 90f;
        //float angle = Mathf.Atan2(lookDirection.y,lookDirection.x) * Mathf.Rad2Deg;
       // WeaponSocket.eulerAngles = new Vector3(0,0, angle);
        
      }
    }

    Vector2 GetLookAtDirection(){

      if(target != null)
        return new Vector2(target.position.x, target.position.y) - rb.position;
      else
        return Vector2.zero;
    }


  private void OnTriggerEnter2D(Collider2D other)
    {
      
        if(other.gameObject.tag == "bullet" )
        {
          if (other.gameObject.GetComponent<Bullet>().tag != "Enemy")
          {
            Debug.Log("CollisionEnter");
            Bullet newBullet = other.gameObject.GetComponent<Bullet>();
            playerDetector.SetLookForState();
            playerDetector.lastViewdPosition.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            agent.SetAgentDestination(playerDetector.lastViewdPosition);
            //animationsController.SetPlayerLookFor(true);
            enemyActions.ChangeHP(-newBullet.Enemy);
          }
        }
       /* if (other.gameObject.GetComponent<Bullet>() != null)
        {
          if (other.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer != this.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer)
        {
            Bullet newBullet = other.gameObject.GetComponent<Bullet>();
            playerDetector.SetLookForState();
            playerDetector.lastViewdPosition.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            agent.SetAgentDestination(playerDetector.lastViewdPosition);
            //animationsController.SetPlayerLookFor(true);
            enemyActions.ChangeHP(-newBullet.Enemy);

           
        }

        }
        */
    }
  

    // кулдаун атаки
    public IEnumerator AttackCooldown(float waitTime)
    {
        isAttackCooldown = true;
        yield return new WaitForSeconds(waitTime);
        isAttackCooldown = false;
        state = States.attackig;

    }

    

}




public enum States
{
  passive,
  lookingfor,
  attackig,
  dead
}
