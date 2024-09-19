using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnter : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] private AudioClip sound;
    [SerializeField] private string scene;
    void OnTriggerEnter(Collider other) { 
        if (other.gameObject.tag == "Player"){
            m_EventManager.ChangeStringLevel(scene);
            m_EventManager.PlaySoundEffect(sound, false);
        }
    }
}
