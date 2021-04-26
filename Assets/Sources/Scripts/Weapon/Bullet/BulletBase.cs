using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
  public float speed = 10f;
  protected Rigidbody2D rb;
  public int damage = 10;
  public float spreading = 0f;
  public float destroyTime = 20f;
  
  // Start is called before the first frame update
  void Start()
  {
      rb = GetComponent<Rigidbody2D>();
      //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      /*Vector2 trajectory = mousePos - rb.position;
      trajectory.x += Random.Range(-spreading, spreading);
      trajectory.y += Random.Range(-spreading, spreading);
      rb.AddForce(trajectory.normalized * speed, ForceMode2D.Impulse);*/
      rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
      //print (transform.rotation);
      Destroy(gameObject, destroyTime); // Destroy this after 20sec
  }

  void OnTriggerEnter2D(Collider2D other)
  {

      if (other.gameObject.tag == "Player" )
      {

          other.gameObject.GetComponent<PlayerActions>().ChangeHP(damage);
              Destroy(gameObject);
            
      }
      
      if (other.gameObject.tag == "Wall")
      {
          Destroy(gameObject);
      }       
  }


}