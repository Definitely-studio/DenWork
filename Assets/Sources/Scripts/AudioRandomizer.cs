using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{

    public List<AudioClip> Clips;
    public AudioSource AudioSource;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        if(AudioSource!= null && Clips.Count != 0 && !AudioSource.isPlaying)
        {
            AudioSource.PlayOneShot(Clips[Random.Range(0,Clips.Count)]);
        }
        
    }
}
