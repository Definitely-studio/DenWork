using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Input _input;
    [SerializeField] private float _velocity;
    public GameObject socket;
    [SerializeField] private Gun gun;
    [SerializeField] private FieldOfView field;
    [SerializeField] private PlayerActions playerActions;
    [SerializeField] private Transform playerRoot;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int MaxHP = 100;
    private Gun _gun;
    private bool isDead = false;
    private int currentHP;
    private Rigidbody2D _rigidbody;
    private Vector2 moveDirection;
    public Animator f_Animator;
    public Animator b_Animator;
    public bool facingRight = true;

    public GameObject body;
    public GameObject f_body;
    public GameObject b_body;

    private void Awake()
    {
         Instantiate(field);
        _input = new Input();
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _input.Player.Shoot.performed += context => Shoot();
        _gun = Instantiate(gun, socket.transform);
        _gun.transform.SetParent(socket.transform);
      

        audioSource = this.GetComponent<AudioSource>();
    }
    private void Start()
    {
        b_body.SetActive(false);
        SetHP(MaxHP);
        Debug.Log(currentHP);
    }
    
    public Transform GetRoot(){
        return playerRoot;
    }

    public bool GetIsDead(){
        return isDead;
    }

    public int GetHP()
    {
        return currentHP;
    }

     public void SetHP(int value)
    {
         currentHP = value;
    }

    public void SetIsDead(bool value){
        isDead = value;
    }

    private void OnEnable()
        {
            _input.Enable();
        }

    private void Shoot()
    {
        if (_gun != null){
            //_gun.GetComponent<Gun>().Shoot();
        if(audioSource.isPlaying != true)
            audioSource.PlayOneShot(_gun.GetAudioClip()); 

        
        
        _gun.Shoot();
            f_Animator.SetTrigger("Shot");   
            b_Animator.SetTrigger("Shot"); 
        }
    }
    
    public void UpdateGun(Gun targetGun)
    {
        Gun i = _gun;
        
        _gun = Instantiate(targetGun, this.transform);
        _gun.transform.SetParent(this.transform);
        Destroy(i.gameObject);

    }

    

    private void Update()
    {
         
    Vector2 AimPosition = _input.Player.MousePosition.ReadValue<Vector2>();
       if(_gun != null)
       {
            _gun.SetAimPoint(AimPosition);
        }           
            
    }
    private void FixedUpdate()
    {
        moveDirection = _input.Player.Move.ReadValue<Vector2>();
        Vector2 mousePosition = _input.Player.MousePosition.ReadValue<Vector2>();
        //Debug.Log(moveDirection);

        Movement(moveDirection);

        Rotation(mousePosition, moveDirection);        
        
    }

    private void Movement(Vector2 move)
    {
        Vector2 dir = _rigidbody.position + move * (Time.fixedDeltaTime * _velocity);
        _rigidbody.transform.position = new Vector3(dir.x, dir.y, _rigidbody.transform.position.z) ;
        
       
    }

    private void Rotation(Vector2 MouseDir, Vector2 moveDir) {
        Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(MouseDir);
        Vector3 lookDir = ( new Vector3(_rigidbody.position.x, _rigidbody.position.y ,0) - worldPosMouse);

        if (lookDir.x < 0) {
            body.transform.eulerAngles = new Vector3(0f, 0f, 0f);          
        }
        if (lookDir.x > 0) {
            body.transform.eulerAngles = new Vector3(0f, 180f, 0f);             
        }
        if (lookDir.y > 0) {
            f_body.SetActive(true);
            b_body.SetActive(false);           
        }
        if (lookDir.y < 0) {
            f_body.SetActive(false);
            b_body.SetActive(true);           
        }          


        if (moveDir.magnitude > 0) {
            f_Animator.SetBool("isWalk", true);
            b_Animator.SetBool("isWalk", true);              
        }

        if (moveDir.magnitude == 0) {
            f_Animator.SetBool("isWalk", false);
            b_Animator.SetBool("isWalk", false);               
        }     

    }

               private void OnTriggerEnter2D(Collider2D other)
    {
         //  Debug.Log(other.gameObject);
           if(other.gameObject.tag == "bullet" )
        {
          if (other.gameObject.GetComponent<Bullet>().tag != "Enemy")
          {
                Bullet newBullet = other.gameObject.GetComponent<Bullet>();

            playerActions.ChangeHP(-newBullet.Damage);
          }
        }


        /*if (other.gameObject.GetComponent<Bullet>() != null && other.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer != this.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer)
        {
            Bullet newBullet = other.gameObject.GetComponent<Bullet>();

            playerActions.ChangeHP(-newBullet.Damage);

        }*/
    }


}
