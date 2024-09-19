using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] private EventManager m_EventManager;
    public void Awake(){
        m_EventManager.OnGamePaused += OnGamePaused;
    }
    public void OnGamePaused(bool isPaused){
        GamePauseResume.isPaused = isPaused;
        pauseScreen.SetActive(isPaused);
    }
}
