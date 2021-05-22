using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Agent : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Transform Root;
    [SerializeField] GameObject Player;
    [SerializeField] Enemy enemy;
    [SerializeField] EnemyActions enemyActions;

    [SerializeField] Transform PlayerTargetTransfrom;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        target = transform;
        PlayerTargetTransfrom = Player.transform;
        enemyActions = GetComponentInChildren<EnemyActions>();
        enemy = GetComponentInChildren<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(target.position);
        Root = enemy.Root.transform;
        
    }


    // Update is called once per frame
    void Update()
    {
        if(!enemy.GetIsDead()) {
            agent.SetDestination(target.position);
            AnimateEnemy();
           
            enemy.animationsController.SetDistance(Vector2.Distance(Root.position, PlayerTargetTransfrom.position)); // Set Distance key in Animator
        }
        else 
        {
            agent.SetDestination(transform.position);
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(target.position, (float) 0.1);
        Gizmos.DrawSphere(Root.position, (float) 0.1);
    }


    public NavMeshAgent GetAgent(){
        return agent;
    }

     public void SetAgentDestination(Transform DestTarget){   
        target = DestTarget;
    }

    public void SetAgentDestinationDelay(float waitTime, Transform DestTarget)
    {
        StartCoroutine(Waiting( waitTime, DestTarget));
        

    }


// Wait few seconds between patrol points
IEnumerator Waiting(float waitTime, Transform DestTarget)
  {
    enemy.animationsController.SetIdleKey(true);
    yield return new WaitForSeconds(waitTime);
    SetAgentDestination(DestTarget); 
    enemy.animationsController.SetIdleKey(false);
    if(enemy.wayPoint.Length != 0)
    {
        enemy.animationsController.SetIdleKey(true);
    }
  }

// Rotate enemy due to movement direction
public void AnimateEnemy() {
    if ((transform.position - Player.transform.position).x > 0) {
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        PlayerTargetTransfrom = Player.GetComponentInChildren<Player>().GetRightSide();
    }
    if ((transform.position - Player.transform.position).x < 0) {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        PlayerTargetTransfrom = Player.GetComponentInChildren<Player>().GetLeftSide();
    }
    }        
} 
