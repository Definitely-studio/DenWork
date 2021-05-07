using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject meshParticlesSystem; // to spawn shell     
    [SerializeField] private Input _input;
    [SerializeField] private float _velocity;
    public GameObject socket;
    [SerializeField] private Gun gun;
    [SerializeField] private FieldOfView field;
    [SerializeField] private PlayerActions playerActions;
    [SerializeField] private Transform playerLeftSide;
    [SerializeField] private Transform playerRightSide;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int MaxHP = 100;
    [SerializeField] private PlayerMovement MovementComponent;
    
    private Gun _gun;
    private bool isDead = false;
    private int currentHP;
    private Rigidbody2D _rigidbody;
    private int ammoCount;
    private Vector2 moveDirection;
    public Animator f_Animator;
    public Animator b_Animator;
    public bool key = false;
    public GameObject body;
    public GameObject target;
    public Transform[] targets;

    public GameObject f_body;
    public GameObject b_body;
    public bool canMove = true;

    private void Awake()
    {   
        ammoCount = 15;
        Instantiate(field);
        _input = new Input();
        _rigidbody = this.GetComponentInParent<Rigidbody2D>();
        _input.Player.Shoot.performed += context => Shoot();
        _gun = Instantiate(gun, socket.transform);
        _gun.transform.SetParent(socket.transform);
        _input.Player.Pause.performed += context => Pause();
        _input.Player.Reload.performed += context => Reload();
        audioSource = this.GetComponent<AudioSource>();
        MovementComponent = GetComponentInParent<PlayerMovement>();
        
    }

    private void Start()
    {
        b_body.SetActive(false);
        SetHP(MaxHP);
        Debug.Log(currentHP);
    }
    
    void Pause(){
         //Debug.Log("Pause");
        playerActions.gameMenu.Pause();
    }
    public void SetKey(bool value)
    {
        key = value;
    }
     public bool GetKey()
    {
        return key;
    }
      public PlayerActions GetActions()
    {
        return playerActions;
    }
    public void SetAmmo(int value)
    {
        _gun.ShowBullets();
        ammoCount = value;
    }
    public void Reload()
    {
        _gun.Reload();
    
    }
     public int GetAmmo()
    {
          return ammoCount;
    }
    public Transform GetLeftSide(){
        return playerLeftSide;
    }
    public Transform GetRightSide(){
        return playerRightSide;
    }
    public bool GetIsDead(){
        return isDead;
    }

    public int GetHP()
    {
        return currentHP;
    }
    public int GetMaxHP()
    {
        return MaxHP;
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
            if (meshParticlesSystem!= null )
                if (meshParticlesSystem.GetComponent<MeshParticlesSystem>() != null )
                    meshParticlesSystem.GetComponent<MeshParticlesSystem>().SpawnShell(new Vector3(_gun.transform.position.x,_gun.transform.position.y, -0.15f));
            
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
         if(canMove){
        MovementComponent.Movement(moveDirection, _velocity);
         }

        MovementComponent.Rotation(mousePosition, moveDirection);        
        
    }

   
    

    private void OnTriggerEnter2D(Collider2D other)
    {
         //  Debug.Log(other.gameObject);
         /*  if(other.gameObject.tag == "bullet" )
        {
          if (other.gameObject.GetComponent<Bullet>().tag != "Player")
          {
                Bullet newBullet = other.gameObject.GetComponent<Bullet>();

            playerActions.ChangeHP(-newBullet.Damage);
          }
        }*/


        /*if (other.gameObject.GetComponent<Bullet>() != null && other.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer != this.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer)
        {
            Bullet newBullet = other.gameObject.GetComponent<Bullet>();

            playerActions.ChangeHP(-newBullet.Damage);

        }*/
    }


}
