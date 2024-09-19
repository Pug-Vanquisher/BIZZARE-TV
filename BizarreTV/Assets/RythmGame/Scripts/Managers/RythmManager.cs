using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RythmManager : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] Track m_track;
    [SerializeField] AudioClip song;
    AudioSource manager;
    [SerializeField] float SecondsPerBeat;
    [SerializeField] float tempo;
    [SerializeField] int BeatsPerBar;
    [SerializeField] float StartTime;
    [SerializeField] float CurrentTime;
    [SerializeField] int[] Beat = new int[2];
    [SerializeField] bool isOnPause = false;

    public void Awake(){
        manager = GetComponent<AudioSource>();
        m_EventManager.OnTrackStarted += OnTrackStarted;
        m_EventManager.OnLevelIntChanged += OnLevelIntChanged;
        m_EventManager.OnLevelStringChanged += OnLevelStringChanged;
        m_EventManager.OnGamePaused += OnGamePaused;
        m_EventManager.OnDifficultyChanged += OnDifficultyChanged;
        Beat[0] = -1;
        tempo = 1;
    }

    public void Update(){
        if (StartTime != 0){
            if (isOnPause){
                StartTime = (float)AudioSettings.dspTime - CurrentTime;
            }
            CurrentTime = (float)AudioSettings.dspTime - StartTime;
            Beat[1] = (int)((CurrentTime / SecondsPerBeat) % BeatsPerBar);
            if (Beat[1] != Beat[0]){
                m_EventManager.SendBeat(Beat[1]);
                Beat[0] = Beat[1];
            }
        }
    }
    public void OnTrackStarted(Track track){
        // Getting a Track Object and it`s audio source 
        m_track = track;
        song = m_track.AudioClip;

        // Calculating
        SecondsPerBeat = (float)(60.0f / (m_track.BPM * tempo));
        BeatsPerBar = m_track.BeatsPerBar;

        // Playing the track
        manager.clip = song;
        manager.pitch = tempo;
        StartTime = (float)AudioSettings.dspTime + m_track.songDelay;
        manager.Play();
    }

    public void OnDifficultyChanged(string difficulty){
        switch(difficulty){
            case "veryeasy":
                tempo = 0.8f;
                break;
            case "easy":
                tempo = 0.9f;
                break;
            case "normal":
                tempo = 1f;
                break;
        }
        m_EventManager.StartTrack(m_track);
        m_EventManager.Respawn();
    }

    public void OnLevelIntChanged(int scene){
        SceneManager.LoadScene(scene); 
    }

    public void OnLevelStringChanged(string scene){
        SceneManager.LoadScene(scene); 
    }
    public void OnGamePaused(bool isPaused){
        isOnPause = isPaused;
        if (isOnPause){
            manager.Pause();    
        } else {
            manager.Play();
        }
    }
}