using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTrigger : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] Track track;
    [SerializeField] GameObject? sectionActivate;
    [SerializeField] GameObject? sectionDeactivate;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            m_EventManager.StartTrack(track);
            sectionActivate?.SetActive(true);
            sectionDeactivate?.SetActive(false);
        }
    }
}
