using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPoint;
    [SerializeField] private Bullet _bulletType;
    [SerializeField] private GameObject _aimGameObject;
    [SerializeField] private float _enemy;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _reloadingTime;
    [SerializeField] private int _bulletsMaxCount;
    [SerializeField] private SpriteRenderer weapon;
    [SerializeField] private FieldOfView field;
    [SerializeField] private AudioSource ShootClip;
    [SerializeField] private AudioSource ReloadClip;


    private List<Bullet> _bullets;
    private int _bulletsCurrentCount;
    private Vector2 _aimPoint;
    private Rigidbody2D _rigidbody;
    private ParentfromBullet parentfrom;
    private void Awake()
    {
         field = FindObjectOfType<FieldOfView>();
        Rigidbody2D rigidbody2D1 = this.gameObject.GetComponent<Rigidbody2D>();
        _rigidbody = rigidbody2D1;
        parentfrom = this.GetComponentInParent<ParentfromBullet>();
        _rigidbody.gravityScale = 0;;
        GenerateBullet();
    }

    private void GenerateBullet()
    {
        _bullets = new List<Bullet>();
        for (int i = 0; i < 10; i++)
        {
            Bullet newBullet = Instantiate(_bulletType, _bulletPoint.transform.position, _bulletPoint.transform.rotation);
            newBullet.gameObject.SetActive(false);
            if(this.GetComponentInParent<ParentfromBullet>() != null)
                newBullet.transform.SetParent(this.GetComponentInParent<ParentfromBullet>().transform);
            _bullets.Add(newBullet);
        }
    }

    public void SetAimPoint(Vector2 aimPoint)
    {
        _aimPoint = aimPoint;
    }

    private void FixedUpdate()
    {
        Vector3 mouseWordPosition = Camera.main.ScreenToWorldPoint(_aimPoint);
        Vector3 targetDirection = (mouseWordPosition - transform.position).normalized;
         _rigidbody.transform.position = new Vector3(parentfrom.transform.position.x, parentfrom.transform.position.y, _rigidbody.transform.position.z);

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        bool flipSprite = (weapon.flipY ? (targetDirection.x > 0.01f) : (targetDirection.x<0.01));
        field.SetAimDirection(new Vector3(targetDirection.x, targetDirection.y, targetDirection.z ));
        field.SetOrigin(_aimGameObject.transform.position);

        if (flipSprite)
        {
            weapon.flipY = !weapon.flipY;
        }

        _rigidbody.SetRotation(angle);


    }

    public Bullet GetBullet(List<Bullet> _bullets)
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (!_bullets[i].gameObject.activeInHierarchy)
            {
                _bullets[i].Enemy = _enemy;
                _bullets[i].Speed = _bulletSpeed;
                return _bullets[i];
            }
        }
        //�������� ��� ���������, ���� �� ����� �������, + ���������� ������ �����������
        Bullet newBullet = Instantiate(_bulletType, _bulletPoint.transform);
        newBullet.gameObject.SetActive(false);
        newBullet.transform.SetParent(this.GetComponentInParent<ParentfromBullet>().transform);
        newBullet.Enemy = _enemy;
        newBullet.Speed = _bulletSpeed;
        _bullets.Add(newBullet);
        return newBullet;
    }

    public void Shoot()
    {
        if(ShootClip != null)
        {
            ShootClip.Play();
            Debug.Log("ShootSound");

        }
            
        
        //����������� ���� �� ����� ���������
        Bullet newBullet = GetBullet(_bullets);
        newBullet.transform.position = _bulletPoint.gameObject.transform.position;
        newBullet.transform.rotation = _bulletPoint.gameObject.transform.rotation;

        //��������� ����
        newBullet.gameObject.SetActive(true);
        _bulletsCurrentCount--;
        if(_bulletsCurrentCount <= 0){
            StartCoroutine(Reloading());
        }

    }

    IEnumerator Reloading(float waitTime)
    {
         if(ReloadClip != null)
        {
            ReloadClip.Play();
            Debug.Log("ReloadSound");

        }
        yield return new WaitForSeconds(waitTime);
        _bulletsCurrentCount = _bulletsMaxCount;
    }
}
