using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{

    [Header("Gun Variables")]

    // Gun values

    public bool isEquipped = false;
    public Ammo.AmmoTypes AmmoType;
    
    
    [SerializeField] private int shotsCount; // number of shots | 1 if just pistol or etc
    [SerializeField] private float spreading; // optimal is 0.1f
    [SerializeField] private float _shootingSpeed; // delay between shots
    [SerializeField] private float _reloadingTime;
    [SerializeField] private int _bulletsMaxCount;

    private bool canShoot;
    private int _bulletsCurrentCount;
    private bool _isReloading;

    [Space(20)] // other object references 

    public GameObject fowCenter; // FoW center where from rays will start
    public GameObject weapon;
    public Sprite AmmoIcon;

    [SerializeField] private GameObject _bulletPoint; // point where bullet will spawn
    [SerializeField] private Bullet _bulletType; // bullet reference
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
    private Transform socket;
    private Player player;
    

   
   
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
        
        Rigidbody2D rigidbody2D1 = this.gameObject.GetComponent<Rigidbody2D>();
        _rigidbody = rigidbody2D1;
        _rigidbody.gravityScale = 0;
        parentfrom = this.GetComponentInParent<ParentfromBullet>();

        _bulletsCurrentCount = _bulletsMaxCount;
        _isReloading = false;
        if(crosshair == null) crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();

        canShoot = true;

        ui = GameObject.Find("CanvasUI").GetComponent<UIGameMode>();
        socket = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>().socket.transform;

        
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
            _rigidbody.transform.position = new Vector3(socket.position.x, socket.position.y, _rigidbody.transform.position.z);
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            _rigidbody.SetRotation(angle);

            // weapon flip
            if (targetDirection.x > 0) weapon.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            else if (targetDirection.x < 0) weapon.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
        }
       
    }

    public void SetAimPoint(Vector2 aimPoint)
    {
        _aimPoint = aimPoint;
    }

// bullet instantiation on shot
    public void GetBullet(GameObject _bulletPoint)
    {
        Bullet newBullet = Instantiate(_bulletType, _bulletPoint.transform.position, _bulletPoint.transform.rotation);

        newBullet.gameObject.transform.SetParent(null);

        newBullet._rigidbody.AddForce((_bulletPoint.transform.up + new Vector3(Random.Range(-spreading, spreading), Random.Range(-spreading, spreading), 0)) * newBullet.Speed);

    }

    public void Shoot()
    {
        if(canShoot){
            
            if (_bulletsCurrentCount > 0)
            {
                if(AudioSource != null) AudioSource.PlayOneShot(shootSound); 
            
                if(crosshair != null)  crosshair.PlayShootingAnimate();
                    
                if(animator != null)  animator.SetTrigger("Shoot");
                    
		        for (int i = 0; i < shotsCount; i++)
		        {
                    GetBullet(_bulletPoint);
            	}

                _bulletsCurrentCount--;
                ShowBullets();
                StartCoroutine(WaitBeetwenShoots(_shootingSpeed));

                if(_bulletsCurrentCount <= 0) StartCoroutine(Reloading(_reloadingTime));
                
            }
            else if(GetAmmo() > 0 && !_isReloading) StartCoroutine(Reloading(_reloadingTime));
    
        }

    }

    public void Reload()
    {
        if(_bulletsCurrentCount != _bulletsMaxCount && GetAmmo() > 0)
            StartCoroutine(Reloading(_reloadingTime));
    }

    IEnumerator Reloading(float waitTime)
    {
        _isReloading = true;

        if(crosshair != null)
        {
            crosshair.PlayReloadingAnimate();
            crosshair.ResetShootingAnimate();
        }
     
        if(AudioSource != null && ReloadSound != null) AudioSource.PlayOneShot(ReloadSound); 
        
        yield return new WaitForSeconds(waitTime);
        
        if(_bulletsMaxCount - _bulletsCurrentCount > GetAmmo())
        {
            _bulletsCurrentCount = GetAmmo() + _bulletsCurrentCount;
            SetAmmo(_bulletsMaxCount - _bulletsCurrentCount);
        }
        else
        {
            SetAmmo(_bulletsMaxCount - _bulletsCurrentCount);
            _bulletsCurrentCount = _bulletsMaxCount;
        }

        ShowBullets();

        if(crosshair != null) crosshair.StopReloadingAnimate();

        _isReloading = false;
        
    }

     IEnumerator WaitBeetwenShoots(float waitTime)
    {
        canShoot = false;
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }
    public void ShowBullets()
    {
        ui.ShowBullet(_bulletsCurrentCount, _bulletsMaxCount, GetAmmo(), AmmoIcon);
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
            else continue;
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
                    if(ammo.Amount >= difference) ammo.Amount -= difference;
                    else
                    {
                        difference -= ammo.Amount;
                        ammo.Amount = 0;
                    }
                    if(difference <= 0) break;
                }
            }
            else continue;
        }
    }

}
