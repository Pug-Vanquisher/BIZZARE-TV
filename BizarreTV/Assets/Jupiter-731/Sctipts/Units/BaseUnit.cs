using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Jupiter731
{
    public class BaseUnit : MonoBehaviour
    {
        internal delegate void Dies(GameObject gameObject);
        internal event Dies Die;
        public float PublicHP { get => _currHP; }
        [SerializeField] float HP = 10;
        [SerializeField] float _currHP;
        public void TakeDamage(float dmg)
        {
            _currHP -= dmg;
            if (_currHP <= 0 && gameObject.tag == "Player")
            { 
                Destroy(gameObject);
            }
            else if (_currHP <= 0 && gameObject.tag != "Player") { Destroy(gameObject); }
        }

        private void OnDestroy()
        {
            Die?.Invoke(gameObject);
            
        }

        private void Awake()
        {
            _currHP = HP;
        }

    }
}
