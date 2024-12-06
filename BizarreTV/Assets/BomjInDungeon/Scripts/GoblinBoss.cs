using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class GoblinBoss : Goblin
    {
        public GameObject arrow;

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            velik = player.transform.position - transform.position;
            velik.Normalize();
        }

        public override void Attack()
        {
            var a = Instantiate(arrow, transform);
            a.GetComponent<Dagger>().creator = HitCollider;
            a.transform.position = transform.position + Vector3.up;
            a.transform.rotation = Quaternion.AngleAxis(-Vector2.SignedAngle(player.transform.position + Vector3.up - a.transform.position, Vector3.right), Vector3.forward);

            current_vlframes = vlframes;
        }
        public override void Move()
        {
            //Ne dvigaetsa :D
        }

        private void OnDestroy()
        {
            BossManager.EndFight();
        }

    }
}
