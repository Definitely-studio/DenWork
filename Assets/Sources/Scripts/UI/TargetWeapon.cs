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

    }

}
