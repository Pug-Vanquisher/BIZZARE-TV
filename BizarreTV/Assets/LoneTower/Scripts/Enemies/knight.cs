using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class knight : Enemy
    {
        public float agressiveSpeed;
        private bool unarmed;

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if(health <= maxHealth/2 && !unarmed)
            {
                speed = 0;
                Invoke("shieldDrop", 0.8f);
                unarmed = true;
                animator.Play("Armature|defDestroy");
            }
        }

        void shieldDrop()
        {
            speed = agressiveSpeed;
        }
    }
}
