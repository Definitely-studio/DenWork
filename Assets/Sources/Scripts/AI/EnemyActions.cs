using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{

    public Enemy enemy;
    public AudioSource AudioSound;
    public AudioClip DamageSound;
    public AudioClip DeathSound;
    public AudioClip RoarSound;
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
             if(AudioSound != null && !AudioSound.isPlaying)
             {
                 AudioSound.PlayOneShot(DamageSound);
             }
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

        if(AudioSound != null && !AudioSound.isPlaying)
            {
                AudioSound.PlayOneShot(DeathSound);
            }
            
        
         Collider2D[] colliders =  gameObject.GetComponents<Collider2D>();

        foreach (Collider2D item in colliders)
        {
            item.enabled = false;
        }
        enemy.state = States.dead;

        transform.position = new Vector3 (transform.position.x,  transform.position.y, transform.position.y * 0.01f + 5.0f);

        //enemy.enemyAnimator.SetBool("Death", true);


        enemy.GetRigidBody().bodyType = RigidbodyType2D.Static;
        enemy.SetIsDead (true);
        Destroy(gameObject, enemy.destroyTime);

    }

    public void PlayRoar(){
        AudioSound.PlayOneShot(RoarSound);
    }

    public virtual void AttackStart(){

    }
 
     public virtual void AttackEnd(){

    }

}
