using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBase : MonoBehaviour
{


    public int MaxHP = 100;
    public float maxSpeed;
    private int currentHP = 100;
    private bool isDead = false;
    private int Soul = 10;
    public int LimitSoul = 100;

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

    public int GetSoul()
    {
        return Soul;
    }

    public void SetSouls(int soul)
    {
        Soul = soul;
    }

    public bool AddSoul()
    {
        if(Soul < LimitSoul)
        {
            Soul+=5;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool NotLimitSoul()
    {
        return (Soul < LimitSoul);
    }

    public void TakeAwaySoul()
    {
        Soul--;
    }
}
