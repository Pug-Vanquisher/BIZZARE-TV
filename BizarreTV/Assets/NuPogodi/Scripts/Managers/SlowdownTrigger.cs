using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pogodi
{
    public class SlowdownTrigger : MonoBehaviour
    {
        [SerializeField] SlowdownManager _sdm;
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Falling")
            {
                other.gameObject.GetComponent<FallingObject>().fallingSpeed /= 2;
                _sdm.affectedGameObjects.Add(other.gameObject);
            }
        }
    }
}