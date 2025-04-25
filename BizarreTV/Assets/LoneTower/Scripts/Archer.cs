using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Archer : MonoBehaviour
    {
        public float spawntime;
        public float arrowSpeed;
        
        
        public GameObject arrow;
        public Transform arrowPos;
        public Animator animator;

        private float shootAnimtimer = 0.4f;
        private float reloadAnimtimer = 1.2f;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!IsInvoking("Shoot"))
            {
                Invoke("Shoot", Mathf.Clamp(spawntime, reloadAnimtimer + shootAnimtimer, spawntime));
                animator.Play("Aim");
            }
        }
        void Shoot()
        {
            animator.Play("Aim");
            Invoke("Spawn", shootAnimtimer);
        }
        void Spawn()
        {
            Vector3 spawnpoint = PointManager.Instance.GetPoint("EnemySpawn");
            spawnpoint.x = PointManager.Instance.GetPoint("LeftSide").x;
            var a = Instantiate(arrow, arrowPos.position, Quaternion.identity);
            a.GetComponent<Arrow>().speed = arrowSpeed;
        }
    }

}