using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationsController : MonoBehaviour
{
    public Enemy enemy;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttackAnimatorKey(){
        animator.SetTrigger("Attack");
    }

    public void SetMovingAnimatorKey(bool value){
        animator.SetBool("isMoving", value);
    }


}
