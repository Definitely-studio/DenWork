using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
  public float speed = 10f;
  protected Rigidbody2D rb;
  public int damage = 2;
  public Transform barrel;
  public float spreading = 0f;
  public bool isThisEnemybullet = false; // если false, то пуля может дамажит врагов, если true, то только игрока

  // Start is called before the first frame update
  void Start()
  {
      rb = GetComponent<Rigidbody2D>();
      Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 trajectory = mousePos - rb.position;
      trajectory.x += Random.Range(-spreading, spreading);
      trajectory.y += Random.Range(-spreading, spreading);
      rb.AddForce(trajectory.normalized * speed, ForceMode2D.Impulse);
      //rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
      //print (transform.rotation);
      Destroy(gameObject, 20); // Destroy this after 20sec
  }

  void OnTriggerEnter2D(Collider2D other)
  {

      if (other.gameObject.tag == "Enemy" && !isThisEnemybullet)
      {

          if (other.gameObject.GetComponent<Enemy>() != null)
          {
            if(other.gameObject.GetComponent<Enemy> ().state != States.dead && !other.gameObject.GetComponent<Enemy> ().GetIsDead())
            {
              other.gameObject.GetComponent<Enemy>().ChangeHP(damage);
              Destroy(gameObject);
            }
          }
      }

      if (other.gameObject.tag == "Player" && isThisEnemybullet)
      {
          Destroy(gameObject);
          if (other.gameObject.GetComponent<Player>() != null)
          {
              other.gameObject.GetComponent<Player>().ChangeHP(damage);
          }
      } 

      if (other.gameObject.tag == "Wall")
      {
          Destroy(gameObject);
      }       
  }

}