using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWeapon : MonoBehaviour
{
    public Collider2D damageCollision;
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
      damageCollision = GetComponent<Collider2D>();
      damageCollision.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetActiveCollider(bool value){

      damageCollision.enabled = value;

    }


    void OnTriggerEnter2D(Collider2D other)
    {

      if(other.gameObject.tag == "Player"){
      

            if (other.gameObject.GetComponent<Player> () != null)
            {
                other.gameObject.GetComponent<Player> ().ChangeHP(damage);
            }
          }

      }
}
