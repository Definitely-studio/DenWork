using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public ParticleSystem RainSystem;

    void Start()
    {
        SetRain(0);
    }


    public void SetRain(int progress)
    {
        RainSystem.emissionRate = progress * 2 + 100;
        RainSystem.gravityModifier = Mathf.Floor(progress / 200) + 2;
        var externalForces = RainSystem.externalForces;
        externalForces.multiplier = progress / 50;
    }
}
