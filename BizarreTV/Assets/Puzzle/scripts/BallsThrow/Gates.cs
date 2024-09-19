using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gates : MonoBehaviour
{
    public AudioSource audioSource;
    public static Action OnBallHitGoal;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "gateBall")
        {
            collision.transform.tag = "Untagged";
            OnBallHitGoal?.Invoke();
            audioSource.Play();
        }
    }
}
