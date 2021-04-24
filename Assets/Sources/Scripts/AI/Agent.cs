using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Agent : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Enemy enemy;
    [SerializeField] EnemyActions enemyActions;
    [SerializeField] float minDistanceToPlayer = 2f;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyActions = GetComponent<EnemyActions>();
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        
  
        if(enemy.state != States.attackig){
            if(Vector2.Distance(transform.position, target.position) >= minDistanceToPlayer)
            {
            agent.SetDestination(target.position);
            enemy.animationsController.SetMovingAnimatorKey(true);
            }
        else{
            agent.SetDestination(transform.position);
            enemy.animationsController.SetMovingAnimatorKey(false);
            enemyActions.Attack();
            }

        if(enemy.GetIsDead())
            agent.SetDestination(transform.position);

        }

        
            
    }

    public NavMeshAgent GetAgent(){
        return agent;
    }

     public void SetAgentDestination(Transform DestTarget){
        Debug.Log(DestTarget.position);
        target = DestTarget;
    }
}
