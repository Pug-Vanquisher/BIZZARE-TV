using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class PlayerFire : MonoBehaviour
    {
        [SerializeField] KeyCode fireKey = KeyCode.Mouse0;
        [SerializeField] Fire fire;
        [SerializeField] float fireRate = 10;
        private float _reloadTime;
        private float _currTime;

        private void Awake()
        {
            _reloadTime = 1/fireRate;
            _currTime = _reloadTime;
        }
        void Update()
        {
            if (Input.GetKey(fireKey) && _currTime >= _reloadTime)
            {
                fire.OpenFire();
                _currTime = 0;
            }
            else
            {
                _currTime += Time.deltaTime;
            }
        }
    }
}
