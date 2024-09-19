using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatReceiver : MonoBehaviour
{
    [SerializeField] protected EventManager m_EventManager;
    public void Awake(){
        m_EventManager.OnBeatSended += OnBeatSended;
    }
    void OnDisable()
    {
        m_EventManager.OnBeatSended -= OnBeatSended;
    }
    
    public virtual void OnBeatSended(int beat){}
}
