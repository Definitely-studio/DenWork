using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBase : MonoBehaviour
{


    public int MaxHP = 100;
    private int currentHP = 100;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Movement(Vector2 direction, float Speed)
    {

    }

    public virtual void Death()
    {

    }


    public virtual void ChangeHP(int deltaHP)
    {

    }

    public void SetIsDead(bool value)
    {
       isDead = value;
    }


    public void SetCurrentHP(int value)
    {
       currentHP = value;
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

   

}
