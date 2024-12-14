using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class Timer : MonoBehaviour
    {
        public float CurrTime { get => timeToDie - _currTime; }
        [SerializeField, Range(10, 60)] float timeToDie;
        [SerializeField, Range(0.5f, 10)] float addingTime;
        [SerializeField] GameObject player;
        private float _currTime = 0;
        private GameObject[] _enemies;
        private BaseUnit _baseUnit;




        private void Awake()
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in _enemies)
            {
                enemy.TryGetComponent<BaseUnit>(out _baseUnit);
                Subscription();
            }
        }

        private void OnUnitDie(GameObject unitDie)
        {
            _currTime -= addingTime;
            UnSubscription(unitDie);
        }

        private void Subscription()
        {
            if (_baseUnit != null) _baseUnit.Die += OnUnitDie;
        }

        private void UnSubscription(GameObject dieUnit)
        {
            if (dieUnit != null)
            {
                 dieUnit.TryGetComponent<BaseUnit>(out _baseUnit);
                if (_baseUnit != null)
                {
                    _baseUnit.Die -= OnUnitDie;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_currTime > timeToDie )
            {
                GameObject.Destroy( player );
                
            }
            _currTime += Time.fixedDeltaTime;
        }


    }
}
