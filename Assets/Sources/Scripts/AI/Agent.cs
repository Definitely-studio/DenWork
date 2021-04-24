using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Agent : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Enemy enemy;
    [SerializeField] float minDistanceToPlayer = 2f;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(Vector2.Distance(transform.position, target.position));
        /*
        if(enemy.state != States.dead && Vector2.Distance(transform.position, target.position) >= minDistanceToPlayer)
            agent.SetDestination(target.position);
        else
            agent.SetDestination(transform.position);
            */
        if(Vector2.Distance(transform.position, target.position) >= minDistanceToPlayer)
            agent.SetDestination(target.position);
        else
            agent.SetDestination(transform.position);
    }
}
