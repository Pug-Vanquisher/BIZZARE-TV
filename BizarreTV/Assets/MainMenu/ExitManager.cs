using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    private void Start()
    {
        StopAllSources();
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    //Функция выключения звуков
    public void StopAllSources()
    {
        AudioSource[] audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in audioSources)
        {
            audioS.Stop();
        }
    }
}

