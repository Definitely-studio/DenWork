using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Input _input;
    [SerializeField] private float _velocity;
     [SerializeField] private GameObject _gun;
 
    private Rigidbody2D _rigidbody;
    private Vector2 moveDirection;

    private void Awake()
    {
        _input = new Input();
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _input.Player.Shoot.performed += context => Shoot();
        //_gun = Instantiate(gun, this.transform);
       
        //_gun.transform.SetParent(this.transform);

    }
   

    private void OnEnable()
        {
            _input.Enable();
        }

    private void Shoot()
    {
         if (_gun != null){
        _gun.GetComponent<Gun>().Shoot();

        }    
    }

    

    private void Update()
    {
         
    Vector2 AimPosition = _input.Player.MousePosition.ReadValue<Vector2>();
       if(_gun != null)
            _gun.GetComponent<Gun>().SetAimPoint(AimPosition);
    }
    private void FixedUpdate()
    {
        moveDirection = _input.Player.Move.ReadValue<Vector2>();
        //Debug.Log(moveDirection);

        Movement(moveDirection);
    }

    private void Movement(Vector2 move)
    {
        //move = move.normalized;
        Vector2 dir = _rigidbody.position + move * (Time.fixedDeltaTime * _velocity);
        _rigidbody.transform.position = dir ;
        
       
    }

}
