using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyActions1 : EnemyActions
{

   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //активируем коллизию которая отвечает за атаку
    public void ChangeStateMeleeAttackCollision(int value)
    {
        // 0 set false
        // 1 set true
        switch (value)
      {
          case 0:
            enemy.enemyMeleeWeapon.SetActiveCollider(false);
            break;
          case 1:
            enemy.enemyMeleeWeapon.SetActiveCollider(true);
              break;
          default:
            enemy.enemyMeleeWeapon.SetActiveCollider(false);
            break;
      }
        
    }

    public override void Attack(){
        Debug.Log("Attack");
       // enemy.state = States.attackig;
        enemy.animationsController.SetAttackAnimatorKey();
    }

}
