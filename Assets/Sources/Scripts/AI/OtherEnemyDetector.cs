using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherEnemyDetector : MonoBehaviour
{

    bool canWeMove;

    // Start is called before the first frame update
    void Start()
    {
      canWeMove = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
      if(other.gameObject.tag == "Enemy" && other.gameObject != gameObject ){
        if(!other.gameObject.GetComponent<Enemy> ().GetIsDead())
          canWeMove = false;
      }

    }

    void OnTriggerExit2D(Collider2D other)
    {
      if(other.gameObject.tag == "Enemy"){
          canWeMove = true;
      }
    }

    public bool GetCanWeMove()
    {
        return canWeMove;
    }
}
