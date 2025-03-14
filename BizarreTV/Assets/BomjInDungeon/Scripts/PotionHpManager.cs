using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class PotionHpManager : HpManager
    {
        public override void TakeDamage(int damage)
        {
            PotionScript potion = GetComponent<PotionScript>();
            if(potion!= null)
            {
                potion.TakeAPotion();
            }
        }
    }

}