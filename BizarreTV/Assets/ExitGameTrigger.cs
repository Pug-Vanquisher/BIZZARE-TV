using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject UI;
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] private string scene;
    public void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            Destroy(other.gameObject);
            Destroy(UI);
            m_EventManager.ChangeStringLevel(scene);
        }
    }
}
