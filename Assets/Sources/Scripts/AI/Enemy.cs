using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PawnBase
{

    //public int damage = -1; // значние отрицательно т.к. метод, который считает HP добавляет отрицательное значение и уменьшает так хп
    public float attackCooldownTime = 2.0f;
    public GameObject EquippedWeapon;
    public Transform WeaponSokect;
    public States state;
    public EnemyTypes enemyType;
    public PlayerDetector playerDetector;
    public Animator enemyAnimator;
    public OtherEnemyDetector otherEnemyDetector;

    private Transform target;
    private Transform bodySprite;
    protected Rigidbody2D rb;
    protected bool isAttackCooldown = false;


    public GameObject PrefabSoul;

    public AudioSource AudioDead;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        state = (state != States.passive) ? States.lookingfor : States.passive;
        rb = GetComponent<Rigidbody2D> ();
        bodySprite = transform.Find("Body").transform;
        enemyAnimator = GetComponent <Animator> ();
        //playerDetector = transform.Find("PlayerDetector").GetComponent<PlayerDetector>();
        target = GameObject.FindWithTag("Player").transform;
    }


    // Update is called once per frame
    public virtual void Update()
    {


      // проверяем, что ИИ не мертв
      if(state != States.dead && state != States.passive)
      {
        transform.position = new Vector3 (transform.position.x,  transform.position.y, transform.position.y * 0.01f);
        SetAnimatorKeys();

        rangedWeaponRotation();

        //Debug.Log($" isAttackCooldown {isAttackCooldown}");
        //Debug.Log($" can we shoot {playerDetector.GetCanWeShoot()}");
        //Debug.Log($" isAttackCooldown {isAttackCooldown}");
        //Debug.Log($" summt {!isAttackCooldown && playerDetector.GetCanWeShoot()}");

        if(!isAttackCooldown && playerDetector.GetCanWeShoot() && !GetIsDead())
        {
          //Debug.Log("EnemyAttack");
          //Debug.Log($" player is found {playerDetector.GetCanWeShoot()}");
          AttackStart();
          //Debug.Log("EnemyAttack");
        }

        else if(!isAttackCooldown && !playerDetector.GetCanWeShoot())
        {
          AttackEnd();
        }
      }


    }

    public override void Movement(Vector2 direction, float speed)
    {
        if (rb)
        {
            rb.AddForce(direction * speed * Time.fixedDeltaTime);
        }
    }

    // метод изменения количества HP
    public override void ChangeHP(int deltaHP)
    {

        if (deltaHP <0)
        {
          enemyAnimator.SetTrigger("Damage");
        }
        SetCurrentHP(GetCurrentHP() + deltaHP);
        //Debug.Log(GetCurrentHP());

        if(GetCurrentHP() <= 0 && gameObject.GetComponent<Collider2D>().enabled == true)
        {
        //Debug.Log("Death");
            //SetIsDead(true);
            Death();
        }
    }

    // метод смерти
    public override void Death()
    {

        AudioDead.Play();
        state = States.dead;

        transform.position = new Vector3 (transform.position.x,  transform.position.y, transform.position.y * 0.01f + 5.0f);

        enemyAnimator.SetBool("Death", true);
        gameObject.GetComponent<Collider2D> ().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        SetIsDead (true);
        StartCoroutine(Disappear(3.0f));
        SoulGenerate();
        GameObject.FindWithTag("MainCamera").GetComponent<MainLogic>().EnemyDead();
    }

    protected void SoulGenerate()
    {
        GameObject soul = Instantiate(PrefabSoul);
        soul.transform.position = transform.position;
    }

    void SetAnimatorKeys(){

      if(state == States.passive)
      {
        enemyAnimator.SetBool("MoveRight", false);
        enemyAnimator.SetBool("MoveLeft", false);
        enemyAnimator.SetBool("MoveTop", false);
        enemyAnimator.SetBool("MoveBack", false);
        enemyAnimator.SetBool("Idle", true);
      }
      else
      {
      Vector2 lookDirection = GetLookAtDirection();

      if(lookDirection.normalized.y < 0.5f && lookDirection.normalized.y > -0.5f)
      {

        if (lookDirection.normalized.x < - 0.5f)
        {
          //Debug.Log("MoveLeft");
          enemyAnimator.SetBool("MoveRight", false);
          enemyAnimator.SetBool("MoveLeft", true);
          enemyAnimator.SetBool("MoveTop", false);
          enemyAnimator.SetBool("MoveBack", false);
        //  enemyAnimator.SetBool("Idle", false);
          transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.y);
        }
        else if (lookDirection.normalized.x > 0.5f)
        {
          enemyAnimator.SetBool("MoveRight", true);
          enemyAnimator.SetBool("MoveLeft", false);
          enemyAnimator.SetBool("MoveTop", false);
          enemyAnimator.SetBool("MoveBack", false);
        //  enemyAnimator.SetBool("Idle", false);
          transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.y);
        }
      }
      else
      {
        if(lookDirection.normalized.y > 0.5f)
        {
          enemyAnimator.SetBool("MoveBack", true);
          enemyAnimator.SetBool("MoveLeft", false);
          enemyAnimator.SetBool("MoveRight", false);
          enemyAnimator.SetBool("MoveTop", false);
        //  enemyAnimator.SetBool("Idle", false);
          transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.y);
        }
        else if(lookDirection.normalized.y < -0.5f)
        {
          enemyAnimator.SetBool("MoveRight", false);
          enemyAnimator.SetBool("MoveLeft", false);
          enemyAnimator.SetBool("MoveTop", true);
          enemyAnimator.SetBool("MoveBack", false);
        //  enemyAnimator.SetBool("Idle", false);
          transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.y);
        }
      }

  /*  else
    {
      enemyAnimator.SetBool("MoveRight", false);
      enemyAnimator.SetBool("MoveLeft", false);
      enemyAnimator.SetBool("MoveTop", false);
      enemyAnimator.SetBool("MoveBack", false);
      enemyAnimator.SetBool("Idle", true);
    }*/
    }
  }


    public virtual void AttackStart(){

    }

    public virtual void AttackEnd(){

    }

    // петод разворачивающий оружие в сторону игрока
    void rangedWeaponRotation()
    {

      if(target != null && !GetIsDead()){
        //Debug.Log("rangedWeaponRotation");
        Vector2 lookDirection = GetLookAtDirection();
        float angle = Mathf.Atan2(lookDirection.y,lookDirection.x) * Mathf.Rad2Deg - 90f;
        //float angle = Mathf.Atan2(lookDirection.y,lookDirection.x) * Mathf.Rad2Deg;
        WeaponSokect.eulerAngles = new Vector3(0,0, angle);
        //Debug.Log($" lookat {WeaponSokect.rotation}");
      }
    }

    Vector2 GetLookAtDirection(){

      if(target != null)
        return new Vector2(target.position.x, target.position.y) - rb.position;
      else
        return Vector2.zero;
    }



    void OnCollisionEnter2D(Collision2D other)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    // кулдаун атаки
    public IEnumerator AttackCooldown(float waitTime)
    {
        isAttackCooldown = true;
        yield return new WaitForSeconds(waitTime);
        isAttackCooldown = false;
        state = States.attackig;

    }

    public IEnumerator Disappear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}

public enum States
{
  passive,
  lookingfor,
  attackig,
  dead
}

public enum EnemyTypes
{
  ranged,
  melee
}
