using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BID
{
    public class RangedEnemyAi : MonoBehaviour
    {
        public GameObject player;

        public GameObject arrow;

        public float attackRange;
        public float vlframes;
        private float current_vlframes;

        void Start()
        {
            current_vlframes = 0;
        }

        private void FixedUpdate()
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange & current_vlframes == 0)
            {
                Attack();
            }

            else if (current_vlframes < 0) { current_vlframes = 0; }

            else { current_vlframes -= Time.deltaTime; }
        }

        void Attack()
        {
            var a = Instantiate(arrow, transform);
            a.transform.position = transform.position;
            a.transform.rotation = Quaternion.AngleAxis(-Vector2.SignedAngle(player.transform.position - a.transform.position, Vector3.right), Vector3.forward);

            Debug.Log("обстрел калом!!!");

            current_vlframes = vlframes;
        }
    }

}