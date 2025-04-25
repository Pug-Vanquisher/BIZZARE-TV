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

        void Start()
        {
            health = maxHealth;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (transform.position.z >= PointManager.Instance.GetPoint("Attack").z)
            {
                transform.position += Vector3.back * speed;
            }
            else
            {
                Invoke("Attack", attackTime);
                TakeDamage(1000);
            }
        }

        public void Attack() 
        {
            Debug.Log("Attacked");
        }

        virtual public void TakeDamage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                Death();
            }
        }

        virtual public void Death()
        {
            CancelInvoke("Attack");
            Destroy(gameObject);
        }
    }

}