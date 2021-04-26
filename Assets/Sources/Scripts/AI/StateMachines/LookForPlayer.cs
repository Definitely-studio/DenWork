using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayer : NPCBaseFMS
{
    Transform trans;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        trans = new GameObject().transform;
        trans.position = new Vector3(NPC.GetComponent<Enemy>().Player.transform.position.x, NPC.GetComponent<Enemy>().Player.transform.position.y, NPC.GetComponent<Enemy>().Player.transform.position.z);
        agent = NPC.GetComponent<Agent>();
        agent.SetAgentDestination(NPC.GetComponent<Enemy>().playerDetector.lastViewdPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         agent.SetAgentDestination(NPC.GetComponent<Enemy>().playerDetector.lastViewdPosition);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if(NPC.GetComponent<Enemy>().wayPoint.Length != 0){
            //NPC.GetComponent<Enemy>().animationsController.SetIdleKey(false);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
