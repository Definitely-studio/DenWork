using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWeapon : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Gun gun1;
    [SerializeField] private Gun gun2;
    private Input inputs;
    private void Awake()
    {
        inputs = new Input();
        inputs.Enable();
        inputs.Player.FirstGun.performed += context => GetGun1();
        inputs.Player.SecondGun.performed += context => GetGun2();
        
    }

    private void GetGun1()
    {
        player.UpdateGun(gun1);
    }

    private void GetGun2()
    {
        player.UpdateGun(gun2);
    }

}
