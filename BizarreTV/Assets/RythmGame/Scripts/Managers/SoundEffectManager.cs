using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] private AudioSource manager; 
    public void Awake()
    {
        m_EventManager.OnSoundEffectPlayed += OnSoundEffectPlayed;
        manager = GetComponent<AudioSource>();
    }
    public void OnSoundEffectPlayed(AudioClip sound, bool isPitchable){
        manager.clip = sound;
        manager.pitch = isPitchable ? Random.Range(0.5f, 1.5f) : 1f;
        manager.PlayOneShot(sound, 1f);
    }
}
