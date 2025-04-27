using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class boar: Enemy
    {
        private float calculateSpeed;
        private float targetSpeed;
        public float chillingPeriod;
        public float runningPeriod;
        public override void Start()
        {
            base.Start();
            calculateSpeed = speed;
            Invoke("RunOver", runningPeriod);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            speed = Mathf.Lerp(speed, targetSpeed, 0.1f);
            animator.SetFloat("Speed", speed / calculateSpeed );
        }
        void RunOver()
        {
            targetSpeed = 0;
            Invoke("ChillOver", chillingPeriod);
        }
        void ChillOver()
        {
            targetSpeed = calculateSpeed;
            Invoke("RunOver", runningPeriod);
        }
    }
}
