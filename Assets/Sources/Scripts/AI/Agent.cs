using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Agent : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Transform Player;
    [SerializeField] Enemy enemy;
    [SerializeField] EnemyActions enemyActions;
    [SerializeField] float minDistanceToPlayer = 2f;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        target = transform;
        enemyActions = GetComponent<EnemyActions>();
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {

        agent.SetDestination(target.position);
        

        enemy.animationsController.SetDistance(Vector2.Distance(transform.position, Player.position));

    }


    public NavMeshAgent GetAgent(){
        return agent;
    }

     public void SetAgentDestination(Transform DestTarget){
        //Debug.Log(DestTarget.position);
        
        target = DestTarget;
    }

    public void SetAgentDestinationDelay(float waitTime, Transform DestTarget)
    {
        StartCoroutine(Waiting( waitTime, DestTarget));

    }

IEnumerator Waiting(float waitTime, Transform DestTarget)
  {
    yield return new WaitForSeconds(waitTime);
     SetAgentDestination(DestTarget); 
    
  }

    

    
}
