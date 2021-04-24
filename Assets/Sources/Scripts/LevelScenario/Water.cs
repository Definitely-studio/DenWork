using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private float Wave = 0;
    public int ProgressLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetProgress(int progress)
    {
        ProgressLevel = progress;
    }

    // Update is called once per frame
    void Update()
    {
        float dTime = Time.deltaTime;
        Wave += dTime;
        float waveY = Mathf.Sin(Wave + ProgressLevel / 20) * ProgressLevel / 2000 + 0.25f;
        transform.position = new Vector3(0, waveY - 0.20f, 21);
    }
}
