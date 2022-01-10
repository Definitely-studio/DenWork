using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{

    [Header("Gun Variables")]

    // Gun values

    public bool isEquipped = false;
    public bool canShoot = true;
    public Ammo.AmmoTypes AmmoType;
    
    
    [SerializeField] private int shotsCount; // number of shots | 1 if just pistol or etc
    [SerializeField] private float spreading; // optimal is 0.1f
    [SerializeField] private float shootingSpeed; // delay between shots
    [SerializeField] private float reloadingTime;
    [SerializeField] private int bulletsMaxCount;

    
    private int bulletsCurrentCount;
    private bool isReloading;

    [Space(20)] // other object references 

    public GameObject fowCenter; // FoW center where from rays will start
    public GameObject weapon;
    public Sprite AmmoIcon;
    public Sprite WeaponIcon;
    public Transform RootSocket;
    public Transform WeaponSocket;

    [SerializeField] private GameObject _bulletPoint; // point where bullet will spawn
    [SerializeField] private Bullet bulletType; // bullet reference
    [SerializeField] private FieldOfView fieldOfView; // FoW reference
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioSource ReloadClip;
    [SerializeField] private Crosshair crosshair;
    [SerializeField] private UIGameMode ui;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip ReloadSound;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hands;
    
    private List<Bullet> _bullets;
    private Vector2 _aimPoint;
    private Rigidbody2D _rigidbody;
    private ParentfromBullet parentfrom;
    private Player player;
    

   
   
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
        
        Rigidbody2D rigidbody2D1 = this.gameObject.GetComponent<Rigidbody2D>();
        _rigidbody = rigidbody2D1;
        _rigidbody.gravityScale = 0;
        parentfrom = this.GetComponentInParent<ParentfromBullet>();

        bulletsCurrentCount = bulletsMaxCount;
        isReloading = false;
        if(crosshair == null) crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();

        ui = GameObject.Find("CanvasUI").GetComponent<UIGameMode>();
        WeaponSocket = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>().WeaponSocket.transform;

        
    }

    private void Start() {

        fieldOfView = FindObjectOfType<FieldOfView>();
        if(ui != null && isEquipped) ShowBullets();
    }
   
    private void FixedUpdate()
    {
        if(isEquipped)
        {
            Vector3 mouseWordPosition = Camera.main.ScreenToWorldPoint(_aimPoint);
            Vector3 targetDirection = (mouseWordPosition - transform.position).normalized;
            _rigidbody.transform.position = new Vector3(WeaponSocket.position.x, WeaponSocket.position.y, _rigidbody.transform.position.z);
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            
           // transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            _rigidbody.SetRotation(angle);
           

            // weapon flip
            if (targetDirection.x > 0) 
            {
                RootSocket.localRotation = Quaternion.Euler(0f, 0f, 0f);
                // Debug.Log($" left {RootSocket.localRotation}");
            }
            else if (targetDirection.x < 0)
            { 
               //Debug.Log($" right {RootSocket.localRotation}");
                RootSocket.localRotation = Quaternion.Euler(180f, 0f, 0f);
            }
        }
    }

    public void SetAimPoint(Vector2 aimPoint)
    {
        _aimPoint = aimPoint;
    }

// bullet instantiation on shot
    public void GetBullet(GameObject _bulletPoint)
    {
        Bullet newBullet = Instantiate(bulletType, _bulletPoint.transform.position, _bulletPoint.transform.rotation);

        newBullet.gameObject.transform.SetParent(null);

        newBullet._rigidbody.AddForce((_bulletPoint.transform.up + new Vector3(Random.Range(-spreading, spreading), Random.Range(-spreading, spreading), 0)) * newBullet.Speed);

    }

    public void Shoot()
    {
        if(canShoot && !isReloading){
            
            if (bulletsCurrentCount > 0)
            {
                if(AudioSource != null) AudioSource.PlayOneShot(shootSound); 
            
                if(crosshair != null)  crosshair.PlayShootingAnimate();
                    
                if(animator != null)  animator.SetTrigger("Shoot");
                    
		        for (int i = 0; i < shotsCount; i++) GetBullet(_bulletPoint);

                bulletsCurrentCount--;
                ShowBullets();
                StartCoroutine(WaitBeetwenShoots(60.0f / shootingSpeed));

                if(bulletsCurrentCount <= 0) Reload();
                
            }
            else if(GetAmmo() > 0 && !isReloading) 
            {
                Reload();
            }
        }

    }

    public void Reload()
    {   
        if(bulletsCurrentCount != bulletsMaxCount && GetAmmo() > 0) StartCoroutine(Reloading(reloadingTime));
    }

    IEnumerator Reloading(float waitTime)
    {
       
        isReloading = true;
        

        if(crosshair != null)
        {
            crosshair.PlayReloadingAnimate();
            crosshair.ResetShootingAnimate();
        }
     
        if(AudioSource != null && ReloadSound != null) AudioSource.PlayOneShot(ReloadSound); 
        
        yield return new WaitForSeconds(waitTime);
        
        if(bulletsMaxCount - bulletsCurrentCount > GetAmmo())
        {
            
            int currentAmmoInInventory = GetAmmo(); // workaround, need to fix SetAmmo()
            SetAmmo(bulletsMaxCount - bulletsCurrentCount);
            bulletsCurrentCount = currentAmmoInInventory + bulletsCurrentCount; 
        }
        else
        {
            SetAmmo(bulletsMaxCount - bulletsCurrentCount);
            bulletsCurrentCount = bulletsMaxCount;
        }

        ShowBullets();

        if(crosshair != null) crosshair.StopReloadingAnimate();

        isReloading = false;
       
        
    }

    IEnumerator WaitBeetwenShoots(float waitTime)
    {
        canShoot = false;
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }
    
    public void ShowBullets()
    {
        ui.ShowBullet(bulletsCurrentCount, bulletsMaxCount, GetAmmo(), AmmoIcon);
    }

    public void ShowHideHands(bool value)
    {
        hands.SetActive(value);
    }

    public int GetAmmo()
    {
        int ammoCount = 0; 
        foreach (InventorySlot slot in player.InventoryManager.CollectedItemSlots)
        {
            Ammo ammo = slot.Item as Ammo;
            if(ammo)
            {
                if(ammo.AmmoType == AmmoType) ammoCount += ammo.Amount;
            }
            else 
            {
                continue;
            }
        }
        return ammoCount;
    }

    public void SetAmmo(int delta)
    {
        int difference = delta;
        foreach (InventorySlot slot in player.InventoryManager.CollectedItemSlots)
        {
            Ammo ammo = slot.Item as Ammo;
            if(ammo)
            {
                if(ammo.AmmoType == AmmoType) 
                {
                    if(ammo.Amount >= difference) 
                    {
                        ammo.Amount -= difference;
                        difference = 0;
                        slot.AmountText.text = ammo.Amount.ToString();
                    }
                    else
                    {
                        difference -= ammo.Amount;
                        ammo.Amount = 0;
                        slot.NullifySlotData();
                    }

                    if(difference <= 0) break;
                }
            }
            else 
            {
                continue;
            }
        }
    }

}
