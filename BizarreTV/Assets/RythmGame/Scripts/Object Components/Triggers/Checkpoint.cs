using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;

    private Transform position;
    public void Awake(){
        position = GetComponent<Transform>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            m_EventManager.OverrideCheckpoint(position.position + Vector3.up);
        }
    }
}
