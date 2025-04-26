using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Archer : MonoBehaviour
    {
        public float speed;
        
        public GameObject arrow;
        public Transform arrowPos;
        public Animator animator;

        private bool isAiming = false;
        private bool isReloaded = true;

        private Transform target = null;

        void Start()
        {

        }

        void FixedUpdate()
        {
            speed = GameObject.Find("Game").GetComponent<Game>().upgrades["archers_speed"].value;
            Move();
        }

        public void Move()
        {
            animator.SetFloat("Speed", 0);
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            if (target == null) // если нет врага ищет
            {
                target = NearestNonAimedEnemy();
            }
            else if(!isAiming && isReloaded)
            {
                if (!isAccessed()) // если находится далеко то бежит
                {
                    transform.position += Vector3.right * Mathf.Sign(target.position.x - transform.position.x) * speed;
                    animator.SetFloat("Speed", 1);
                    transform.rotation = Quaternion.Euler(0, -90f * Mathf.Sign(target.position.x - transform.position.x), 0);
                }
                else // иначе стреляет
                {
                    Aim();
                }
            }
        }

        void MakeShoot()
        {
            var a = Instantiate(arrow, arrowPos.position, Quaternion.identity);
            a.transform.LookAt(target);
        }

        bool isAccessed()
        {
            if(target == null)
            {
                return false;
            }
            return (transform.position.x >= target.position.x - speed / 2 && transform.position.x <= target.position.x + speed / 2);
        }
        Transform NearestNonAimedEnemy()
        {
            Enemy nearestEnemy = null;
            float range = Mathf.Infinity;
            GameObject enemylist = GameObject.Find("EnemyList");

            for (int i = 0; i < enemylist.transform.childCount; i++)
            {
                Enemy enemy = enemylist.transform.GetChild(i).GetComponent<Enemy>();
                if (enemy != null && !enemy.aimed) 
                {
                    if(range > (enemy.transform.position - transform.position).magnitude)
                    {
                        nearestEnemy = enemy;
                        range = (enemy.transform.position - transform.position).magnitude;
                    }
                }
            }
            if (nearestEnemy != null)
            {
                nearestEnemy.aimed = true;
            }
            else { return null; }
            return nearestEnemy.transform;
        }

        public void Aim()
        {
            isAiming = true;
            animator.Play("Aim");
        }
        public void Shoot()
        {
            isAiming = false;
            if (!isAccessed())
            {
                animator.Play("Start");
                isReloaded = true;
                return;
            }
            isReloaded = false;
            MakeShoot();
        }
        public void Reload()
        {
            isReloaded = true;
        }

        public void Death()
        {
            Destroy(gameObject);
        }
    }

}