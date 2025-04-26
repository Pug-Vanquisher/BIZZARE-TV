using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Lightning : MonoBehaviour
    {
        public float height;
        public Vector2 edgesCount;
        public float angle;
        public CapsuleCollider collider;
        public LineRenderer line;


        private void Start()
        {
            Invoke("Death", 1f);
            Animate();
            DealDamagetoNearEnemies();
        }

        private void FixedUpdate()
        {
            line.startColor = new Color(Mathf.Clamp(line.startColor.b - 0.1f, 0f, 1f), 1, 1f, Mathf.Clamp(line.startColor.a - 0.05f, 0f, 1f));
        }
        
        void Animate()
        {
            line.startColor = new Color(1, 1, 1, 1);
            int edges = Random.Range((int)edgesCount.x, (int)edgesCount.y);
            line.positionCount = edges;
            float edgeHeight = height / edges;
            float startRadius = edgeHeight * Mathf.Tan(angle * Mathf.PI / 180);
            for (int i = 0; i < edges; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-startRadius * i, startRadius * i), edgeHeight * i, Random.Range(-startRadius * i, 0));
                line.SetPosition(i, pos);
            }
        }

        void DealDamagetoNearEnemies()
        {
            GameObject enemylist = GameObject.Find("EnemyList");

            for (int i = 0; i < enemylist.transform.childCount; i++)
            {
                Enemy enemy = enemylist.transform.GetChild(i).GetComponent<Enemy>();
                if (enemy != null && collider.radius > (enemy.transform.position - transform.position).magnitude)
                {
                    enemy.TakeDamage(1);
                    enemy.Knockback(new Vector3(
                            (enemy.transform.position - transform.position).x,
                            0,
                            (enemy.transform.position - transform.position).z)
                        );
                }
            }
        }

        void Death()
        {
            Destroy(gameObject);
        }
    }
}