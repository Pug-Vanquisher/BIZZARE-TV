using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] ParticleSystem particleSystem;
        [SerializeField] KeyCode meleeAttackKey = KeyCode.Mouse1;
        ParticleSystem.EmissionModule _emission;
        
        private void Update()
        {
            if (Input.GetKey(meleeAttackKey))
            {
                particleSystem.enableEmission = true;
            }
            else
            {
                particleSystem.enableEmission = false;
            }
        }
    }
}
