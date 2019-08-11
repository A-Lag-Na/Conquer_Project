﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    [Range(0.0001f, 1f)] [SerializeField] private float masterVolume, musicVolume, sfxVolume;
    Slider masterSlider = null, musicSlider = null, sfxSlider = null;
    [SerializeField] AudioMixer musicMixer = null, sfxMixer = null;

    // Start is called before the first frame update
    void Start()
    {
        masterSlider = transform.Find("Master Volume").GetComponent<Slider>();
        musicSlider = transform.Find("Music Volume").GetComponent<Slider>();
        sfxSlider = transform.Find("SFX Volume").GetComponent<Slider>();

        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume");

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        masterVolume = masterSlider.value;
        musicVolume = musicSlider.value;
        sfxVolume = sfxSlider.value;
        
        AudioListener.volume = masterVolume;
        musicMixer.SetFloat("MusicVol", Mathf.Log10(musicVolume) * 20);
        sfxMixer.SetFloat("SFXVol", Mathf.Log10(sfxVolume) * 20);

        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        if(masterSlider != null)
            masterSlider.value = masterVolume;
        if(musicSlider != null)
            musicSlider.value = musicVolume;
        if(sfxSlider != null)
            sfxSlider.value = sfxVolume;
    }

}
