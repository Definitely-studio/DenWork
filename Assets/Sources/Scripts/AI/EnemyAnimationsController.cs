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

    public void SetAttackAnimatorKey(bool value){
        animator.SetBool("Attack", value);
    }

    public void SetMovingAnimatorKey(bool value){
        animator.SetBool("isMoving", value);
    }

      public void SetPlayerFound(bool value){
        animator.SetBool("PlayerFound", value);
    }
      public void SetDistance(float value){
        animator.SetFloat("DistanceToTarget", value);
    }

    public void SetPlayerLookFor(bool value){
        animator.SetBool("LookFor", value);
    }

    public void SetIdleKey(bool value)
    {
        animator.SetBool("isIdle", value);
    }

    public void SetOuchTrigger()
    {
        animator.SetTrigger("Ouch");
    }

    public void SetDeathTrigger()
    {
        animator.SetTrigger("Death");
    }    
}
