using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BID
{
    public class GoblinMelee : Goblin
    {
        public GameObject dagger;
        public override void Attack()
        {
            var a = Instantiate(dagger, transform);
            a.transform.position = transform.position;
            a.transform.rotation = Quaternion.AngleAxis(-Vector2.SignedAngle(player.transform.position - a.transform.position, Vector3.right), Vector3.forward);

            Debug.Log("палучай!!!");

            current_vlframes = vlframes;
        }
    }

}