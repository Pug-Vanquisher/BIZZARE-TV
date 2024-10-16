using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BID
{
    public class GoblinRange : Goblin
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
            a.transform.position = transform.position;
            a.transform.rotation = Quaternion.AngleAxis(-Vector2.SignedAngle(player.transform.position - a.transform.position, Vector3.right), Vector3.forward);

            Debug.Log("обстрел калом!!!");

            current_vlframes = vlframes;
        }
        public override void Move()
        {
            //Ne dvigaetsa :D
        }
    }

}