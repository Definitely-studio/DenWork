using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    private Animator playerAnimator;
    Rigidbody2D rb2d;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerAnimator.Play("ZombieWalk");  
            rb2d.velocity = new Vector2(10f, 0);            
                  
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.Play("ZombieAttack");             
                  
        }
    }
}
