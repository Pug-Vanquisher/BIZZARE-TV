using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseResume : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;
    public static bool isPaused = false; 
    void Update()
    {
        if (!isPaused){
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            isPaused = !isPaused;
            m_EventManager.PauseGame(isPaused);
        }  
    }
}
