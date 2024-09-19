using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisabler : MonoBehaviour
{
    [SerializeField] GameObject tutorialScreen;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            tutorialScreen.SetActive(false);
        }
    }
}