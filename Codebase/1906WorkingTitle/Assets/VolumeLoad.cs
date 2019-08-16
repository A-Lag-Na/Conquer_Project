using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeLoad : MonoBehaviour
{
    AudioMixer[] mixers;
    [Range(0.0001f, 1f)] private float masterVolume, musicVolume, sfxVolume;
    // Start is called before the first frame update
    void Start()
    {

        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        AudioListener.volume = masterVolume;

        mixers = GetComponents<AudioMixer>();
        if (mixers[0].name == "Music")
        {
            mixers[0].SetFloat("MusicVol", Mathf.Log10(musicVolume) * 20);
            mixers[1].SetFloat("SFXVol", Mathf.Log10(sfxVolume) * 20);
        }
        else
        {

            mixers[0].SetFloat("SFXVol", Mathf.Log10(sfxVolume) * 20);
            mixers[1].SetFloat("MusicVol", Mathf.Log10(musicVolume) * 20);
        }
    }
    
}
