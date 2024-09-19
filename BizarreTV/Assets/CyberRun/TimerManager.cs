using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public float elapsedTime = 0f;
    private bool timerRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
    }

    public void PauseTimer()
    {
        timerRunning = false;
    }

    public void ResumeTimer()
    {
        timerRunning = true;
    }
}