using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{

    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHP(int deltaHP)
    {
        if(enemy!= null ){

        
            if (deltaHP <0)
            {
            //enemy.enemyAnimator.SetTrigger("Damage");
            }
            enemy.SetCurrentHP(enemy.GetCurrentHP() + deltaHP);
            Debug.Log(enemy.GetCurrentHP());

            if(enemy.GetCurrentHP() <= 0 && gameObject.GetComponent<Collider2D>().enabled == true)
            {
                //SetIsDead(true);
                Death();
            }
        }
    }



    private void Death(){

        if(enemy.AudioDead != null)
            enemy.AudioDead.Play();
            
        enemy.state = States.dead;

        transform.position = new Vector3 (transform.position.x,  transform.position.y, transform.position.y * 0.01f + 5.0f);

        //enemy.enemyAnimator.SetBool("Death", true);
        gameObject.GetComponent<Collider2D> ().enabled = false;
        enemy.GetRigidBody().bodyType = RigidbodyType2D.Static;
        enemy.SetIsDead (true);
        Destroy(gameObject, enemy.destroyTime);
    }

    public virtual void AttackStart(){

    }
 
     public virtual void AttackEnd(){

    }

}
