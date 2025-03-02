using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPSLimiter : MonoBehaviour
{
    public int targetFPS = 60; // Ограничение FPS

    void Awake()
    {
        // Отключаем VSync и устанавливаем targetFrameRate
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }
}
