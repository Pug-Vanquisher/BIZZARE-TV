using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public ParticleSystem mF;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Baam();
        }
    }

    void Baam()
    {
        mF.Play();
    }
}
