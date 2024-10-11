using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class BaseUnit : MonoBehaviour
    {
        [SerializeField] float HP = 10;
        private float _maxHP;
        [SerializeField] private float _currHP;

        private void Awake()
        {
            _maxHP = HP;
            _currHP = HP;
        }

        public void TakeDamage(float dmg)
        {
            _currHP -= dmg;
        }

        private void FixedUpdate()
        {
            if (_currHP < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
