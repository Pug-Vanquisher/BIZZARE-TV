using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Pogodi
{
    public abstract class FallingObject : MonoBehaviour
    {
        [SerializeField] protected GameManager gm;
        [SerializeField] GameObject FallingObj;
        Transform objTran;
        [SerializeField] public float fallingSpeed;
        protected int remainedTime = 1000;

        void Awake()
        {
            gm.OnGameEnded += OnGameEnded;
            gm.OnGameRestarted += OnGameRestarted;

            Random random = new Random();
            objTran = GetComponent<Transform>();
            fallingSpeed = (float)((random.NextDouble() + 0.1f) / 1.5f);
        }
        void FallByTick()
        {
            objTran.Translate(0, -fallingSpeed / 5, 0);
        }

        void OnGameEnded()
        {
            this.fallingSpeed = 0;
        }

        void OnGameRestarted()
        {
            Destroy(this.FallingObj);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                DoPickingEvent();
            }
            else if (other.gameObject.tag == "Floor")
            {
                DoMissingEvent();
            }
        }
        public virtual void DoPickingEvent() { }
        public virtual void DoMissingEvent() { }

        void TickAndSelfDestruct()
        {
            if (remainedTime == 0)
            {
                Destroy(FallingObj);
            }
            remainedTime--;
        }
        void FixedUpdate()
        {
            FallByTick();
            TickAndSelfDestruct();
        }
    }
}