using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class GoblinClone : GoblinBoss
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            velik = player.transform.position - transform.position;
            velik.Normalize();
        }

        private void OnDestroy()
        {
            BossManager.ChangePhase();
        }
    }
}
