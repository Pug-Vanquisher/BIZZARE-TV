using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class Timer : MonoBehaviour
    {
        public float CurrTime {  get => _currTime;}
        [SerializeField, Range(10, 60)] float timeToDie;
        [SerializeField] GameObject player;
        private float _currTime;

        private void FixedUpdate()
        {
            if (_currTime > timeToDie )
            {
                GameObject.Destroy( player );
            }
        }
    }
}
