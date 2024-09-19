using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTime : MonoBehaviour
{
    public GameObject Time;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.SetActive(true);
        }
    }
}
