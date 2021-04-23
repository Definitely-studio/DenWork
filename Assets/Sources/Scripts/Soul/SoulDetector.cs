using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        bool OnTarget = GetComponentInParent<Soul>().OnTarget;

        if (col.gameObject.tag == "Player" && OnTarget == false)
        {
            if (col.gameObject.GetComponent<Player>().NotLimitSoul())
            {
                GetComponentInParent<Soul>().SetTarget(col.gameObject);
            }
        }
    }
}
