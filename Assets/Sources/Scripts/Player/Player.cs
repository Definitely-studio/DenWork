using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject Root;
    public List<GameObject> WeaponList;
    public GameObject WeaponSocket;
    
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
    public UIGameMode UI;

    [SerializeField] private GameObject meshParticlesSystem; // to spawn shell     
    [SerializeField] private Input input;
    [SerializeField] private float velocity;
    [SerializeField] private Gun gun;
    [SerializeField] private FieldOfView field;
    [SerializeField] private PlayerActions playerActions;
    [SerializeField] private Transform playerLeftSide;
    [SerializeField] private Transform playerRightSide;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int MaxHP = 100;
    [SerializeField] private PlayerMovement MovementComponent;
    

    private bool isDead = false;
    private int currentHP;
    private Rigidbody2D _rigidbody;
    private Vector2 moveDirection;

    private void Awake()
    {   

        Instantiate(field);
        input = new Input();
        input.Player.Shoot.performed += context => Shoot();
        input.Player.Pause.performed += context => Pause();
        input.Player.Reload.performed += context => Reload();
        input.Player.FirstGun.performed += context => ChangeGun(1);
        input.Player.SecondGun.performed += context => ChangeGun(2);

        _rigidbody = this.GetComponentInParent<Rigidbody2D>();
        audioSource = this.GetComponent<AudioSource>();
        MovementComponent = GetComponentInParent<PlayerMovement>();
        InventoryManager = FindObjectOfType<InventoryManager>();
        UI = FindObjectOfType<UIGameMode>();
    }

    private void Start()
    {
        
        b_body.SetActive(false);
        SetHP(MaxHP);
        if(gun != null && InventoryManager != null) InventoryManager.AddItem(gun.gameObject);
    }
    
    private void Update()
    {
        Vector2 AimPosition = input.Player.MousePosition.ReadValue<Vector2>();
       
        MovementComponent.SetAimPoint(AimPosition);

        if(gun != null) gun.SetAimPoint(AimPosition);
              
    }
    private void FixedUpdate()
    {
        moveDirection = input.Player.Move.ReadValue<Vector2>();
        Vector2 mousePosition = input.Player.MousePosition.ReadValue<Vector2>();

        if(canMove) MovementComponent.Movement(moveDirection, velocity);

        MovementComponent.Rotation(mousePosition, moveDirection);        
    }

    // SETTERS REGION
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
        this.gun = gun;
    }

    public Gun GetGun()
    {
        return gun;
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
            if(gun.gameObject != gunInSlot){
                gunInSlot.SetActive(true);
                gun.gameObject.SetActive(false);
                gun.isEquipped = false;
                gun = gunInSlot.GetComponent<Gun>();
                gunInSlot.transform.SetParent(WeaponSocket.transform);
                gunInSlot.transform.position = Vector3.zero;
                gun.isEquipped = true;
                gun.canShoot = true;
                gun.RootSocket.position = Vector3.zero;
                gun.RootSocket.rotation = Quaternion.identity;
                gun.ShowHideHands(true);
                gunInSlot.GetComponent<Collider2D>().enabled = false;
                UI.ShowWeapon(gun.WeaponIcon);
            }
        }
    }

    public void Reload()
    {
        if(gun != null) gun.Reload();
    }

    private void OnEnable()
    {
        input.Enable();
    }
    void Pause(){
        
        playerActions.gameMenu.Pause();
    }

    private void Shoot()
    {
        if(Time.timeScale != 0){
            if (gun != null){
                if(meshParticlesSystem != null){
                    if (meshParticlesSystem.TryGetComponent(out MeshParticlesSystem particlesSystem)) 
                        particlesSystem.SpawnShell(new Vector3(gun.transform.position.x,gun.transform.position.y, -0.15f));
                }

                gun.Shoot();
            }
        }
    }

}
