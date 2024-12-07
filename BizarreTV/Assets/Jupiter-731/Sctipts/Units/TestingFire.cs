using Jupiter731;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class TestingFire : MonoBehaviour
    {
        [SerializeField] float reloadTime;
        [SerializeField, Range(0, 10000)] float capacity;
        [SerializeField] Fire fire;
        private void Awake()
        {
            StartCoroutine(OpenFire());
        }

        IEnumerator OpenFire()
        {
            float counter = 0f;
            while (counter < capacity)
            {
                fire.OpenFire();
                yield return new WaitForSeconds(reloadTime);
            }
        }
    }
}