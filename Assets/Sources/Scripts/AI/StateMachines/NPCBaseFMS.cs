using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseFMS : StateMachineBehaviour
{

    public GameObject NPC;
    public Agent agent;
    public GameObject Destination;
    public float minDistancetoDestination = 3f;

 override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        Destination = animator.gameObject;
        agent = NPC.GetComponent<Enemy>().agent;
    }

}
