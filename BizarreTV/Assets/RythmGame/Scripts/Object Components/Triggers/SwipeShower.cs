using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeShower : MonoBehaviour
{
    public GameObject swingSprite;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            swingSprite.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            swingSprite.SetActive(false);
        }
    }
}
