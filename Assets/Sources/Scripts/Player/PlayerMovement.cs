using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    [SerializeField] private Player player;
    [SerializeField] private FieldOfView fieldOfView; // FoW reference
    private Vector2 _aimPoint;
 
    // Start is called before the first frame update
    void Start()
    {
         _rigidbody = this.GetComponent<Rigidbody2D>();
         player = GetComponentInChildren<Player>();
         fieldOfView = FindObjectOfType<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAimPoint(Vector2 aimPoint)
    {
        _aimPoint = aimPoint;
    }
    private void FixedUpdate() {
        if(Time.timeScale != 0f )
        {
            Vector3 mouseWordPosition = Camera.main.ScreenToWorldPoint(_aimPoint);
            Vector3 targetDirection = (mouseWordPosition - transform.position).normalized;
            fieldOfView.SetAimDirection(new Vector3(targetDirection.x, targetDirection.y, targetDirection.z ));
            fieldOfView.SetOrigin(new Vector3(player.transform.position.x,player.transform.position.y + 0.25f ,10));
        }
        
    }

    public void Movement(Vector2 move, float _velocity)
    {
       
        Vector2 dir = _rigidbody.position + move * (Time.fixedDeltaTime * _velocity);
        _rigidbody.transform.position = new Vector3(dir.x, dir.y, _rigidbody.transform.position.z) ;
        
    }


    public void Rotation(Vector2 MouseDir, Vector2 moveDir) {
        Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(MouseDir);
       // Vector3 targetDirection = (worldPosMouse - transform.position).normalized;
       // float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Vector3 lookDir = ( new Vector3(_rigidbody.position.x, _rigidbody.position.y ,0) - worldPosMouse);
       // player.WeaponSocket.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
       // Debug.Log(angle);

        if (lookDir.x < 0) {
           // player.transform.parent.transform.eulerAngles = new Vector3(0f, 0f, 0f);            
            player.body.transform.eulerAngles = new Vector3(0f, 0f, 0f);          
        }
        if (lookDir.x > 0) {
            //player.transform.parent.transform.eulerAngles =  new Vector3(0f, 180f, 0f);            
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
