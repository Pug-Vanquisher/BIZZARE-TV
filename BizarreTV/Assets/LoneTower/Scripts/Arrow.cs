using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Arrow : MonoBehaviour
    {
        public float speed;
        public Rigidbody rb;
        public TrailRenderer trail;
        public GameObject lightning;
        public Collider collider;
        public List<Transform> hitted;
        public int ricoshets;
        public ParticleSystem hitEffect;
        void Start()
        {
            hitted = new List<Transform>();
            ricoshets = (int)GameObject.Find("Game").GetComponent<Game>().upgrades["ricoshet_count"].value;
            speed = GameObject.Find("Game").GetComponent<Game>().upgrades["arrow_speed"].value;
            Invoke("Death", 5f);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(rb != null)
            {
                MoveWithDetectTrigger(transform.forward * speed);
            }
            else
            {
                trail.time = Mathf.Lerp(trail.time, 0, 0.1f);
            }
        }

        public void MoveWithDetectTrigger(Vector3 vector)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, vector, out hit, vector.magnitude))
            {
                rb.MovePosition(hit.point);
            }
            else
            {
                rb.MovePosition(transform.position + vector);
            }
        }
        Transform NearestEnemy()
        {
            Enemy nearestEnemy = null;
            float range = Mathf.Infinity;
            GameObject enemylist = GameObject.Find("EnemyList");

            for (int i = 0; i < enemylist.transform.childCount; i++)
            {
                Enemy enemy = enemylist.transform.GetChild(i).GetComponent<Enemy>();
                if (enemy != null && !hitted.Contains(enemy.transform))
                {
                    if (range > (enemy.transform.position - transform.position).magnitude)
                    {
                        nearestEnemy = enemy;
                        range = (enemy.transform.position - transform.position).magnitude;
                    }
                }
            }

            return nearestEnemy.transform;
        }
        void Death()
        {
            Destroy(gameObject);
        }


        private void OnTriggerEnter(Collider other)
        {
            hitEffect.Stop();
            hitEffect.Play();
            Enemy enemy = other.GetComponent<Enemy>();
            if (other.gameObject.tag == "Damage" || enemy == null) { return; }
            if (other.gameObject.tag == "Tower") 
            {
                Destroy(rb);
                rb = null;
                Destroy(collider);
                collider = null;
                ricoshets = 0;
            }

            if (!hitted.Contains(enemy.transform))
            {
                enemy.TakeDamage(1);
                enemy.SlowDown(GameObject.Find("Game").GetComponent<Game>().upgrades["slowdown_time"].value);
                if (ricoshets > 0)
                {
                    hitted.Add(enemy.transform);

                    transform.LookAt(NearestEnemy().position);
                }
                else
                {
                    Destroy(rb);
                    rb = null;
                    Destroy(collider);
                    collider = null;
                }

                ricoshets -= 1;
                if (Random.Range(0, 100) < GameObject.Find("Game").GetComponent<Game>().upgrades["lighting_chance"].value)
                {
                    var a = Instantiate(lightning, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                }
            }
        }
    }

}