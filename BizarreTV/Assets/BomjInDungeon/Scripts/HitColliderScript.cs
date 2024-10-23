using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class HitColliderScript : HpManager
    {
        public HpManager origin;
        public override void TakeDamage(int damage)
        {
            origin.TakeDamage(damage);
            Debug.Log(123);
        }
    }
}