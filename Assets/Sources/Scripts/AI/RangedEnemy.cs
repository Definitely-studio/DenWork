using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

  public float waitTimeBeforeShoot = 1f;
    // Start is called before the first frame update
    /*void Start()
    {
      equippedweapon.GetComponent<Gun>().SetWeaponMode(1);
    }

    // Update is called once per frame
    void Update()
    {

    }*/


    public override void AttackStart(){
    //  anim.SetBool("Attack", true);
    //  Debug.Log("EnemyAttack");
        state = States.attackig;
        StartCoroutine(WaitToAttack(waitTimeBeforeShoot));

        //StartCoroutine(AttackCooldown(attackCooldownTime));
    }

    public override void AttackEnd(){
        //anim.SetBool("Attack", false);
        state = States.lookingfor;
        //EquippedWeapon.SetAttackColliderActive(true);
    }

    IEnumerator WaitToAttack(float waitTime)
    {
      yield return new WaitForSeconds(waitTime);
//      EquippedWeapon.GetComponent<Gun>().Shoot();

    }


}
