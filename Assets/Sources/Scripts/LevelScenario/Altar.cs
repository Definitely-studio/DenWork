using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Altar : MonoBehaviour
{
    //public bool player_in_col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Altar_Eyes Aeyes;
    public bool souls_transfer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.isTrigger)
        {
            Aeyes = GameObject.FindGameObjectWithTag("Altar_Eyes").GetComponent<Altar_Eyes>();
            Debug.Log("Player in Trigger Zone");
            //sn.ignite_eyes(true);
            souls_transfer = true;
            Aeyes.vizvano(souls_transfer);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player" && other.isTrigger)
        {
            Aeyes = GameObject.FindGameObjectWithTag("Altar_Eyes").GetComponent<Altar_Eyes>();
            Debug.Log("Player Exited");
            souls_transfer = false;
            //sn.ignite_eyes(false);
            Aeyes.vizvano(souls_transfer);
        }

    }


    //bool OnTarget = GetComponentInParent<Soul>().OnTarget;

    /*if (col.gameObject.tag == "Player" && OnTarget == false)
    {
        if (col.gameObject.GetComponent<Player>().NotLimitSoul())
        {
            GetComponentInParent<Soul>().SetTarget(col.gameObject);
        }
    }*/
    //}

}
