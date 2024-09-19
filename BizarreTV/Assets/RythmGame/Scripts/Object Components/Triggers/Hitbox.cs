using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] private int Damage;
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            Damage++ ;
            if(Damage == 11){
                m_EventManager.Respawn();
                Damage = 0;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Damage = 0;
    }
}
