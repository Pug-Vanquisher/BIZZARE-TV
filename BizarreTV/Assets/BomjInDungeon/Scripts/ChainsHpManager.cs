using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class ChainsHpManager : HpManager
    {
        public override void TakeDamage(int damage)
        {
            ChainScript chains = GetComponent<ChainScript>();
            if (chains != null)
            {
                chains.destroyed = true;
            }
        }
    }
}
