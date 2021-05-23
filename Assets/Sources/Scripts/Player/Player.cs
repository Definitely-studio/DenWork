using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject Root;
    public List<GameObject> WeaponList;
    public GameObject socket;
    
    public Animator f_Animator;
    public Animator b_Animator;
    public bool key = false;
    public GameObject body;
    public GameObject target;
    public Transform[] targets;

    public GameObject f_body;
    public GameObject b_body;
    public bool canMove = true;
    public InventoryManager InventoryManager;

    [SerializeField] private GameObject meshParticlesSystem; // to spawn shell     
    [SerializeField] private Input _input;
    [SerializeField] private float _velocity;
    [SerializeField] private Gun _gun;
    [SerializeField] private FieldOfView field;
    [SerializeField] private PlayerActions playerActions;
    [SerializeField] private Transform playerLeftSide;
    [SerializeField] private Transform playerRightSide;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int MaxHP = 100;
    [SerializeField] private PlayerMovement MovementComponent;
    

    private bool isDead = false;
    private int currentHP;
    private int ammoCount;
    private Rigidbody2D _rigidbody;
    private Vector2 moveDirection;

    private void Awake()
    {   
        ammoCount = 15;
        Instantiate(field);
        _input = new Input();
        _input.Player.Shoot.performed += context => Shoot();
        _input.Player.Pause.performed += context => Pause();
        _input.Player.Reload.performed += context => Reload();
        _input.Player.FirstGun.performed += context => ChangeGun(1);
        _input.Player.SecondGun.performed += context => ChangeGun(2);

        _rigidbody = this.GetComponentInParent<Rigidbody2D>();
        audioSource = this.GetComponent<AudioSource>();
        MovementComponent = GetComponentInParent<PlayerMovement>();
        InventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Start()
    {

        b_body.SetActive(false);
        SetHP(MaxHP);
        if(_gun != null && InventoryManager != null) InventoryManager.AddItem(_gun.gameObject);
    }
    
    private void Update()
    {
        Vector2 AimPosition = _input.Player.MousePosition.ReadValue<Vector2>();
       
        MovementComponent.SetAimPoint(AimPosition);

        if(_gun != null) _gun.SetAimPoint(AimPosition);
              
    }
    private void FixedUpdate()
    {
        moveDirection = _input.Player.Move.ReadValue<Vector2>();
        Vector2 mousePosition = _input.Player.MousePosition.ReadValue<Vector2>();

        if(canMove) MovementComponent.Movement(moveDirection, _velocity);

        MovementComponent.Rotation(mousePosition, moveDirection);        
    }

    #region SetterGetters
    
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
    public void SetGun(Gun gun)
    {
        this._gun = gun;
    }

     public void SetHP(int value)
    {
         currentHP = value;
    }
    public void SetIsDead(bool value){
        isDead = value;
    }
    #endregion
    

    public void ChangeGun(int slotNumber)
    {
        if(InventoryManager.EquippedItemSlots[slotNumber - 1].ItemObject != null)
        {
            GameObject gunInSlot = InventoryManager.EquippedItemSlots[slotNumber - 1].ItemObject;
            if(_gun.gameObject != gunInSlot){
                gunInSlot.SetActive(true);
                _gun.gameObject.SetActive(false);
                _gun.isEquipped = false;
                _gun = gunInSlot.GetComponent<Gun>();
                gunInSlot.transform.SetParent(socket.transform);
                gunInSlot.transform.position = Vector3.zero;
                _gun.isEquipped = true;
                _gun.ShowHideHands(true);
                gunInSlot.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void Reload()
    {
        if(_gun != null) _gun.Reload();
    }

    private void OnEnable()
    {
        _input.Enable();
    }
    void Pause(){
        
        playerActions.gameMenu.Pause();
    }

    private void Shoot()
    {
        if (_gun != null){
            if(meshParticlesSystem != null){
                if (meshParticlesSystem.TryGetComponent(out MeshParticlesSystem particlesSystem)) 
                    particlesSystem.SpawnShell(new Vector3(_gun.transform.position.x,_gun.transform.position.y, -0.15f));
            }

            _gun.Shoot();
        }
    }

}
