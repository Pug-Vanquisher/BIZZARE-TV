using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHole : MonoBehaviour
{
    [HideInInspector]
    public bool isActive;

    [SerializeField]
    private GameObject flag;

    [SerializeField]
    private AudioSource goalSound;

    private void Start()
    {
        if (flag != null)
        {
            flag.SetActive(false);
        }
        isActive = true;
    }

    public void RiseFlag()
    {
        goalSound.Play();
        isActive = false;
        if (flag != null)
        {
            flag.SetActive(true);
        }

    }
}
