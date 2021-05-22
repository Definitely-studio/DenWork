using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D _rigidbody;

    [SerializeField] private Player player;
 
    // Start is called before the first frame update
    void Start()
    {
         _rigidbody = this.GetComponent<Rigidbody2D>();
         player = GetComponentInChildren<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Movement(Vector2 move, float _velocity)
    {
       
        Vector2 dir = _rigidbody.position + move * (Time.fixedDeltaTime * _velocity);
        _rigidbody.transform.position = new Vector3(dir.x, dir.y, _rigidbody.transform.position.z) ;
        
       //Debug.Log(_rigidbody.transform.position);
    }


    public void Rotation(Vector2 MouseDir, Vector2 moveDir) {
        Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(MouseDir);
        Vector3 lookDir = ( new Vector3(_rigidbody.position.x, _rigidbody.position.y ,0) - worldPosMouse);

        
        if (lookDir.x < 0) {
            player.body.transform.eulerAngles = new Vector3(0f, 0f, 0f);          
        }
        if (lookDir.x > 0) {
            player.body.transform.eulerAngles = new Vector3(0f, 180f, 0f);             
        }
        if (lookDir.y > -0.25) {
            player.f_body.SetActive(true);
            player.b_body.SetActive(false);           
        }
        if (lookDir.y < -0.25) {
            player.f_body.SetActive(false);
            player.b_body.SetActive(true);           
        }          


        if (moveDir.x > 0 && lookDir.x > 0) {
            player.f_Animator.SetBool("isWalk", false);
            player.b_Animator.SetBool("isWalk", false); 
            player.f_Animator.SetBool("isWalkBack", true);
            player.b_Animator.SetBool("isWalkBack", true);                         
        }

        if (moveDir.x > 0 && lookDir.x < 0) {
            player.f_Animator.SetBool("isWalk", true);
            player.b_Animator.SetBool("isWalk", true);            
            player.f_Animator.SetBool("isWalkBack", false);
            player.b_Animator.SetBool("isWalkBack", false);              
        }

        if (moveDir.x < 0 && lookDir.x < 0) {
            player.f_Animator.SetBool("isWalk", false);
            player.b_Animator.SetBool("isWalk", false);   
            player.f_Animator.SetBool("isWalkBack", true);
            player.b_Animator.SetBool("isWalkBack", true);                       
        }

        if (moveDir.x < 0 && lookDir.x > 0) {
            player.f_Animator.SetBool("isWalk", true);
            player.b_Animator.SetBool("isWalk", true);             
            player.f_Animator.SetBool("isWalkBack", false);
            player.b_Animator.SetBool("isWalkBack", false);              
        }

        if (moveDir.y > 0 && lookDir.y > 0) {
            player.f_Animator.SetBool("isWalk", false);
            player.b_Animator.SetBool("isWalk", false); 
            player.f_Animator.SetBool("isWalkBack", true);
            player.b_Animator.SetBool("isWalkBack", true);                         
        }

        if (moveDir.y > 0 && lookDir.y < 0) {
            player.f_Animator.SetBool("isWalk", true);
            player.b_Animator.SetBool("isWalk", true);            
            player.f_Animator.SetBool("isWalkBack", false);
            player.b_Animator.SetBool("isWalkBack", false);              
        }

        if (moveDir.y < 0 && lookDir.y < 0) {
            player.f_Animator.SetBool("isWalk", false);
            player.b_Animator.SetBool("isWalk", false);   
            player.f_Animator.SetBool("isWalkBack", true);
            player.b_Animator.SetBool("isWalkBack", true);                       
        }

        if (moveDir.y < 0 && lookDir.y > 0) {
            player.f_Animator.SetBool("isWalk", true);
            player.b_Animator.SetBool("isWalk", true);             
            player.f_Animator.SetBool("isWalkBack", false);
            player.b_Animator.SetBool("isWalkBack", false);              
        }



        if (moveDir.magnitude == 0) {
            player.f_Animator.SetBool("isWalk", false);
            player.b_Animator.SetBool("isWalk", false);  
            player.f_Animator.SetBool("isWalkBack", false);
            player.b_Animator.SetBool("isWalkBack", false);                          
        }     

    }

}
