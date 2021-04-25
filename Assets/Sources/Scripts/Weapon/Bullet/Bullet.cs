using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float enemy;
    [SerializeField]private int damage;
    [SerializeField] private ParticleSystem explosion;

    private Rigidbody2D _rigidbody;
    private int _consSpeed;

    public float Enemy { get => enemy; set => enemy = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }

    private void Awake()
    {
        Rigidbody2D rigidbody2D1 = this.gameObject.GetComponent<Rigidbody2D>();
        _rigidbody = rigidbody2D1;
        //_consSpeed = Speed;
    }

    private void OnEnable()
    {
        explosion.gameObject.SetActive(false);
        _rigidbody.AddForce(_rigidbody.transform.up * speed);
        //Speed = _consSpeed;
    }
    private void Start()
    {
        
    }

    /*private void Move()
    {
        Vector2 velocity = transform.up * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position +  velocity);
    }

    private void FixedUpdate()
    {
        Move();
    }*/

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(explosion.duration);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

         if ( collision.gameObject.GetComponent<ParentfromBullet>()
         != null && this.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer != null){

            if ( collision.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer
            != this.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer)
            {
                Speed = 0;
                explosion.gameObject.SetActive(true);
                //his.delay(explosion.duration);

                /*if(collision.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer == "Player")
                    {
                        collision.gameObject.GetComponent<PlayerOld>().ChangeHP(damage);
                    }*/

                if(collision.gameObject.tag == "Enemy")
                    {
                        collision.gameObject.GetComponent<EnemyActions>().ChangeHP(damage);
                    }
                if(collision.gameObject.tag == "Player")
                    {
                        collision.gameObject.GetComponent<PlayerActions>().ChangeHP(damage);
                    }


                StartCoroutine(ExampleCoroutine());
                
            }
         }
    }
}
