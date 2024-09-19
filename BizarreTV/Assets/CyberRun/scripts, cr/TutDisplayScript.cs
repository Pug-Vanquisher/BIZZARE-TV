using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutDisplayScript : MonoBehaviour
{
    public GameObject Tutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Tutorial.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Tutorial.SetActive(false);
        }
    }
}
