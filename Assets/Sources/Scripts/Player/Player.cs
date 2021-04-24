using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : PawnBase
{
    #region Fields
    public Text textObject;
    public Camera cam;
    public GameObject UI;
    public Transform WeaponSoket;
    public Animator playerAnimator;
    public float movespeed = 5f;
    public GameMenu gameMenu;
    //private variables
    private Inventory inventory;
    private Rigidbody2D rb;
    private Vector3 moveDir;
    private Vector2 mousePos;
    #endregion

    public GameObject MainLogic;

    public AudioSource AudioDamage;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        RescaleHealPoint();
        inventory = GetComponent<Inventory>();
    }

    #region Body
    // Update is called once per frame
    void Update()
    {
        if(cam != null)
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        HandleMovement();
    }
    #endregion

    void FixedUpdate()
    {
      if(!GetIsDead())
      {
        SetAnimatorKeys();
        Rotation(Angle());
        rb.velocity = moveDir * movespeed;
      }

    }

    #region Methods

    public void HandleMovement() {

      float speed = 5f;
      float moveX = 0f;
      float moveY = 0f;

      if (Input.GetKey(KeyCode.W)) {
        moveY = +1f;
      }
      if (Input.GetKey(KeyCode.S)) {
        moveY = -1f;
      }
      if (Input.GetKey(KeyCode.D)) {
        moveX = +1f;
      }        
      if (Input.GetKey(KeyCode.A)) {
        moveX = -1f;
      }

      moveDir = new Vector3(moveX, moveY).normalized;	
    }

    public void RescaleHealPoint()
   {
     if(UI != null){
      UI.GetComponent<UIGameMode>().ShowSoulLevel(GetSoul());
      UI.GetComponent<UIGameMode>().SetHealSlider(GetCurrentHP());
     }
   }


    void Rotation(float angle)
    {
      WeaponSoket.eulerAngles = new Vector3(0,0, angle);
    }

    float Angle()
    {
      Vector2 lookDirection = GetLookAtDirection();
      float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
      return angle;
    }

    Vector2 GetLookAtDirection(){

      if(mousePos != null)
        return mousePos - rb.position;
      else
        return Vector2.zero;
    }

    void SetAnimatorKeys(){

      Vector2 lookDirection = GetLookAtDirection();

      if(lookDirection.normalized.y < 0.5f && lookDirection.normalized.y > -0.5f)
      {

        if (lookDirection.normalized.x < - 0.5f)
        {
          //Debug.Log("MoveLeft");
          playerAnimator.SetBool("MoveRight", false);
          playerAnimator.SetBool("MoveLeft", true);
          playerAnimator.SetBool("MoveTop", false);
          playerAnimator.SetBool("MoveBack", false);
          transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.y);
        }
        else if (lookDirection.normalized.x > 0.5f)
        {
          playerAnimator.SetBool("MoveRight", true);
          playerAnimator.SetBool("MoveLeft", false);
          playerAnimator.SetBool("MoveTop", false);
          playerAnimator.SetBool("MoveBack", false);
          transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.y);
        }
      }
      else
      {
        if(lookDirection.normalized.y > 0.5f)
        {
          playerAnimator.SetBool("MoveBack", true);
            playerAnimator.SetBool("MoveLeft", false);
          playerAnimator.SetBool("MoveRight", false);
          playerAnimator.SetBool("MoveTop", false);
          transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.y);
        }
        else if(lookDirection.normalized.y < -0.5f)
        {
          playerAnimator.SetBool("MoveRight", false);
            playerAnimator.SetBool("MoveLeft", false);
          playerAnimator.SetBool("MoveTop", true);
          playerAnimator.SetBool("MoveBack", false);
          transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.y);
        }
      }

    }

    public override void ChangeHP(int deltaHP)
    {

      //print(deltaHP);
        if (deltaHP < 0)
        {
            playerAnimator.SetTrigger("Damage");
            AudioDamage.Play();
        }

        SetCurrentHP(GetCurrentHP() + deltaHP);
        RescaleHealPoint();
        
        if(GetCurrentHP() <= 0 && gameObject.GetComponent<Collider2D>().enabled == true)
        {
            Death();
        }
    }

    public override void Death()
    {
        gameObject.GetComponent<Collider2D> ().enabled = false;
        SetIsDead (true);
        gameMenu.ActivatePostPortus();

    }

    #endregion
}
    