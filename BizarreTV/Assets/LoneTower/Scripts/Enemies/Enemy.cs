using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Enemy : MonoBehaviour
    {
        public float maxHealth;
        public float health;
        public float speed;
        public float attackTime;
        public int points;

        public bool aimed = false;
        private float slowed = 0f;
        private Vector3 knockback;

        public Animator animator;
        public ParticleSystem DeathParticles;
        public virtual void Start()
        {
            health = maxHealth;
        }

        // Update is called once per frame
        public virtual void FixedUpdate()
        {
            Move();
        }

        public virtual void Move()
        {
            if (slowed > 0f)
            {
                slowed = Mathf.Clamp(slowed - Time.deltaTime, 0, Mathf.Infinity);
            }
            knockback = Vector3.Lerp(knockback, Vector3.zero, 0.1f);

            if (transform.position.z >= PointManager.Instance.GetPoint("Attack").z)
            {
                transform.position += Vector3.back * speed // скорость
                    * 0.5f * ((slowed > 0) ? 1 : 2)  // замедление
                    + knockback; // откидывание
            }
            else
            {
                TakeDamage(1000);
            }
        }
        virtual public void TakeDamage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                Death();
                GameObject.Find("Game").GetComponent<Game>().score += points;
                GameObject.Find("Game").GetComponent<Game>().points += points;
            }
        }

        virtual public void Death()
        {
            DeathParticles.Play();
            DeathParticles.transform.parent = transform.parent;
            Destroy(gameObject);
        }

        public void SlowDown(float range)
        {
            slowed = range;
        }
        public void Knockback(Vector3 knock)
        {
            knockback = (knockback.normalized + knock).normalized * 0.1f;
        }
    }

}