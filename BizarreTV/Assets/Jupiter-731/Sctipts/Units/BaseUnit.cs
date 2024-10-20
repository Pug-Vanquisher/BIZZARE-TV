using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class BaseUnit : MonoBehaviour
    {
        public float PublicHP { get => _currHP; }
        [SerializeField] float HP = 10;
        [SerializeField] float _currHP;
        private float _maxHP;

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
            //Debug.Log(PublicHP);
            if (_currHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
