using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private Animator enemyAnimator;

    void Start() {
    	enemyAnimator = GetComponent<Animator>();
    }


    public override void AttackStart(){

/*    хотел вторую анимацию добавить получше, но вышлдо так себе

     if( Mathf.RoundToInt(Random.Range(0f,1f)) != 0)
        enemyAnimator.SetTrigger("StabAttack");
      else
        enemyAnimator.SetTrigger("CutAttack");
*/

        enemyAnimator.SetTrigger("StabAttack");

        //Debug.Log("EnemyAttack");
        state = States.attackig;
        //EquippedWeapon.SetAttackColliderActive(true);
        StartCoroutine(AttackCooldown(attackCooldownTime));
    }

    public override void AttackEnd(){
        //enemyAnimator.SetBool("Attack", false);
        state = States.lookingfor;
        //EquippedWeapon.SetAttackColliderActive(true);
    }

    //  Медод, вызывающий включение/выключение коллизии оружия, мызывается из Animation Event
/*
    public void MeleeWeaponActivateCollider (int value)
    {
      if (value ==1 )
        EquippedWeapon.GetComponent<MeleeWeapon> ().SetActiveCollider(true);
      else
        EquippedWeapon.GetComponent<MeleeWeapon> ().SetActiveCollider(false);
    }
*/
/*
    public override void Death()
    {

//        MeleeWeaponActivateCollider (0);
        state = States.dead;
        transform.position = new Vector3 (transform.position.x,  transform.position.y, transform.position.y * 0.01f + 5.0f);
        enemyAnimator.SetBool("Death", true);
        if (AudioDead != null)
          AudioDead.Play();
        //Debug.Log("Enemy Death");
        gameObject.GetComponent<Collider2D> ().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        SetIsDead (true);
        StartCoroutine(Disappear(3.0f));
        
        GameObject.FindWithTag("MainCamera").GetComponent<MainLogic>().EnemyDead();
    }*/


}
