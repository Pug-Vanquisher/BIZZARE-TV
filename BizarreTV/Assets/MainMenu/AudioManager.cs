using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer mixer;

    public AudioSetting[] audioSettings;
    private enum AudioGroups {Master, Music, SFX };

    void Awake()
    {
        instance = GetComponent<AudioManager>();
    }

    void Start()
    {
        for (int i = 0; i < audioSettings.Length; i++)
        {
            audioSettings[i].Initialize();
        }
    }

    public void SetMasterVolume(float value)
    {
        audioSettings[(int)AudioGroups.Master].SetExposedParam(value);
    }

    public void SetMusicVolume(float value)
    {
        audioSettings[(int)AudioGroups.Music].SetExposedParam(value);
    }

    public void SetSFXVolume(float value)
    {
        audioSettings[(int)AudioGroups.SFX].SetExposedParam(value);
    }
}


[System.Serializable]
public class AudioSetting
{
    public Slider slider;
    //public GameObject redX;
    public string exposedParam;

    public void Initialize()
    {
        slider.value = PlayerPrefs.GetFloat(exposedParam);
    }

    public void SetExposedParam(float value)
    {
        //redX.SetActive(value <= slider.minValue);
        AudioManager.instance.mixer.SetFloat(exposedParam, value);
        PlayerPrefs.SetFloat(exposedParam, value);
    }
}
