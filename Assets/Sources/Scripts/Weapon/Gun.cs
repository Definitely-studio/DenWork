using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPoint; // point where bullet will spawn
    [SerializeField] private GameObject _aimGameObject; // HZ
    [SerializeField] private Bullet _bulletType; // bullet reference
 
    [SerializeField] private int shots; // number of shots | 1 if just pistol or etc
    [SerializeField] private float spreading; // optimail is 0.1f
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootingSpeed; // delay between shots
    [SerializeField] private float _reloadingTime;
    [SerializeField] private int _bulletsMaxCount;
//   [SerializeField] private SpriteRenderer weapon;
//   [SerializeField] private SpriteRenderer hand1;
//   [SerializeField] private SpriteRenderer hand2;

    [SerializeField] private FieldOfView field; // FoW reference
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioSource ReloadClip;
    [SerializeField] private Crosshair crosshair;
    [SerializeField] private UIGameMode ui;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip ReloadSound;
    

    public GameObject fowCenter; // FoW center where from rays will start
    public GameObject weapon;
    private List<Bullet> _bullets;
    private bool canShoot;
    private int _bulletsCurrentCount;
    private bool _isReloading;
    private Vector2 _aimPoint;
    private Rigidbody2D _rigidbody;
    private ParentfromBullet parentfrom;
    private Transform socket;
    private Player player;
   
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
         field = FindObjectOfType<FieldOfView>();
        Rigidbody2D rigidbody2D1 = this.gameObject.GetComponent<Rigidbody2D>();
        _rigidbody = rigidbody2D1;
        parentfrom = this.GetComponentInParent<ParentfromBullet>();
        _rigidbody.gravityScale = 0;

        _bulletsCurrentCount = _bulletsMaxCount;
        _isReloading = false;
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        canShoot = true;
        ui = GameObject.Find("CanvasUI").GetComponent<UIGameMode>();
        socket = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().socket.transform;
         ui.ShowBullet(_bulletsCurrentCount, _bulletsMaxCount, player.GetAmmo());
    }


    public void SetAimPoint(Vector2 aimPoint)
    {
        _aimPoint = aimPoint;
    }

    private void FixedUpdate()
    {
        Vector3 mouseWordPosition = Camera.main.ScreenToWorldPoint(_aimPoint);
        Vector3 targetDirection = (mouseWordPosition - transform.position).normalized;
         _rigidbody.transform.position = new Vector3(socket.position.x, socket.position.y, _rigidbody.transform.position.z);

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        field.SetAimDirection(new Vector3(targetDirection.x, targetDirection.y, targetDirection.z ));
        field.SetOrigin(new Vector3(player.transform.position.x,player.transform.position.y + 0.25f ,10));


        //field.SetAimDirection(new Vector3(targetDirection.x, targetDirection.y, targetDirection.z ));
        // field.SetOrigin(new Vector3(player.transform.position.x,player.transform.position.y,10));


        _rigidbody.SetRotation(angle);

// weapon flip
        if (targetDirection.x > 0) {
        weapon.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    	}
        if (targetDirection.x < 0) {
        weapon.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
    	} 




    }
// bullet instantiation on shot
    public void GetBullet(GameObject _bulletPoint)
    {
        Bullet newBullet = Instantiate(_bulletType, _bulletPoint.transform.position, _bulletPoint.transform.rotation);

        newBullet.gameObject.transform.SetParent(null);
        //newBullet.Speed = _bulletSpeed;

        newBullet._rigidbody.AddForce((_bulletPoint.transform.up + new Vector3(Random.Range(-spreading, spreading), Random.Range(-spreading, spreading), 0)) * newBullet.Speed);
        

    }

    public void Shoot()
    {
        if(canShoot){
            if (_bulletsCurrentCount > 0)
            {
                if(AudioSource != null)
                {
                    AudioSource.PlayOneShot(shootSound); 
               

                }
                if(crosshair != null)
                crosshair.PlayShootingAnimate();
                    
                
                //????????????????????????????????? ???????????? ?????? ??????????????? ???????????????????????????
		        for (int i = 0; i < shots; i++)
		        {
                GetBullet(_bulletPoint);
            	}


                //??????????????????????????? ????????????
                _bulletsCurrentCount--;
                ui.ShowBullet(_bulletsCurrentCount, _bulletsMaxCount, player.GetAmmo());
                StartCoroutine(WaitBeetwenShoots(_shootingSpeed));
                if(_bulletsCurrentCount <= 0)
                    StartCoroutine(Reloading(_reloadingTime));
                
            }
            else
            {
                if(player.GetAmmo() > 0){
                    if(!_isReloading)
                        StartCoroutine(Reloading(_reloadingTime));
                }
            }
        }

    }

    public void Reload()
    {
        if(_bulletsCurrentCount != _bulletsMaxCount && player.GetAmmo() > 0)
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
     
         if(AudioSource != null && ReloadSound != null)
        {
            AudioSource.PlayOneShot(ReloadSound); 
            Debug.Log("ReloadSound");

        }
        yield return new WaitForSeconds(waitTime);
        if(_bulletsMaxCount > player.GetAmmo())
        {
            _bulletsCurrentCount = player.GetAmmo();
            player.SetAmmo(0);
        }
        else
        {
            player.SetAmmo(player.GetAmmo() - (_bulletsMaxCount - _bulletsCurrentCount));
            _bulletsCurrentCount = _bulletsMaxCount;
            
        }

        ui.ShowBullet(_bulletsCurrentCount, _bulletsMaxCount, player.GetAmmo());
        if(crosshair != null)
            crosshair.StopReloadingAnimate();
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
        ui.ShowBullet(_bulletsCurrentCount, _bulletsMaxCount, player.GetAmmo());
    }

}
