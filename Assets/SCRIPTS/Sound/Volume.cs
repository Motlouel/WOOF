using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    
    public AudioSource audio;
    
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    const string MIXER_MASTER = "MasterParameter";
    const string MIXER_MUSIC= "MusicParameter";
    const string MIXER_SFX= "SFXParameter";

    void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    void SetMasterVolume(float v)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(v) * 20);
    }
    void SetMusicVolume(float v)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(v) * 20);
    }
    void SetSFXVolume(float v)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(v) * 20);
    }

    void ButtonSound()
    {
        audio.Play();
    }
}
