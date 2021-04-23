using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public GameObject Target;
    public bool OnTarget = false;
    public float Speed = 1f;
    public string NamePickup = "Soul";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dTime = Time.deltaTime;

        if (OnTarget)
        {
            float positionX = Mathf.Sign(Target.transform.position.x - transform.position.x) * Speed * dTime + transform.position.x;
            float positionY = Mathf.Sign(Target.transform.position.y - transform.position.y) * Speed * dTime + transform.position.y;
            transform.position = new Vector3(
                positionX,
                positionY,
                transform.position.z
            );
        }
    }

    public void SetTarget(GameObject TargetDetected)
    {
        if (!OnTarget)
        {
            OnTarget = true;
            Target = TargetDetected;
        }
    }
}
