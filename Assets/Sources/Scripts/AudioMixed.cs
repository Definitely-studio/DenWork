using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixed : MonoBehaviour
{
    public GameObject ScrollBarSound;
    public GameObject ScrollBarMusic;

    public AudioMixerGroup AudioMixer;

    // Start is called before the first frame update
    void Start()
    {
        ScrollBarMusic.GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat("MusicVolume", 1);
        ScrollBarSound.GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat("SoundVolume", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeVolumeMusic(float volume)
    {
        GlobalList.MusicValue = ScrollBarMusic.GetComponent<Scrollbar>().value;

        AudioMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void ChangeVolumeSound(float volume)
    {
        GlobalList.SaundValue = ScrollBarSound.GetComponent<Scrollbar>().value;

        AudioMixer.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, volume));

        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
}
