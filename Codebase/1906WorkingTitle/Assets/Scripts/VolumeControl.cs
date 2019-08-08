using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    [Range(0.0001f, 1f)] [SerializeField] private float masterVolume, musicVolume, sfxVolume;
    Slider master = null, music = null, sfx = null;
    [SerializeField] AudioMixer musicMixer, sfxMixer;

    // Start is called before the first frame update
    void Start()
    {
        master = transform.Find("Master Volume").GetComponent<Slider>();
        music = transform.Find("Music Volume").GetComponent<Slider>();
        sfx = transform.Find("SFX Volume").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        masterVolume = master.value;
        musicVolume = music.value;
        sfxVolume = sfx.value;
        
        AudioListener.volume = masterVolume;
        musicMixer.SetFloat("MusicVol", Mathf.Log10(musicVolume) * 20);
        sfxMixer.SetFloat("MusicVol", Mathf.Log10(sfxVolume) * 20);
    }
    

}
