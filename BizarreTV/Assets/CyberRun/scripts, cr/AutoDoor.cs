using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public Animator A_animator;
    public AudioSource DoorSound;
    public GameObject Trigger;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            A_animator.SetBool("NeedToOpen", true);
            DoorSound.Play();
            Trigger.SetActive(false);
        }
    }
}
