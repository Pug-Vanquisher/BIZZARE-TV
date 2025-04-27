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
            line.colorGradient.alphaKeys[0] = new GradientAlphaKey(line.colorGradient.alphaKeys[0].alpha - 0.05f, 0f);
            line.startColor = new Color(0, line.startColor.g - 0.001f, 1, line.startColor.a - 0.1f);

            for(int i = 0; i < line.positionCount; i++)
            {
                line.SetPosition(i, Vector3.Lerp(line.GetPosition(i), Vector3.up * line.GetPosition(i).y, 0.075f));
            }
        }
        
        void Animate()
        {
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