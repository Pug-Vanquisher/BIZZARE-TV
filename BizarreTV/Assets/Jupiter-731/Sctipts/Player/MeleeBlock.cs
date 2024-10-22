using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Jupiter731
{
    public class MeleeBlock : MonoBehaviour
    {
        [SerializeField] LayerMask BlockingObjectLayer;
        [SerializeField, Range(30, 90)] float blockAngle;
        [SerializeField, Range(0.01f, 0.5f)] float blockDelay;
        [SerializeField, Range(0.01f, 0.5f)] float blockTime = 0.1f;
        private float _currBlockingTime;

        public void BlockProjectiles(Transform blockPoint, float blockRange)
        {
            StartCoroutine(Delay());
            StartCoroutine(Blocking(blockPoint, blockRange));
            
        }

        private void BlockContinuous(Transform blockPoint, float blockRange)
        {
            Collider2D[] hitProjectiles = Physics2D.OverlapCircleAll(blockPoint.position, blockRange, BlockingObjectLayer);
            //Debug.Log("Пуль найдено: " + hitProjectiles.Length);
            foreach (Collider2D Projectiles in hitProjectiles)
            {
                Vector2 directionToProjectile = (Projectiles.transform.position - blockPoint.position).normalized;
                Vector2 characterForward = transform.right;
                float angleToProjectile = Vector2.Angle(characterForward, directionToProjectile);
                //Debug.Log($"Проверка снаряда: {Projectiles.name}, угол: {angleToProjectile}");
                if (angleToProjectile < blockAngle)
                {
                    var projectile = Projectiles.GetComponent<BaseBullet>();
                    if (projectile != null)
                    {
                        Destroy(projectile.gameObject);
                       // Debug.Log("Цель сбита" + Projectiles.name);
                    }
                }
            }
        }

        private IEnumerator Blocking(Transform blockPoint, float blockRange)
        {
            while (_currBlockingTime < blockTime) 
            { 
                BlockContinuous(blockPoint, blockRange);
                _currBlockingTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            _currBlockingTime = 0;
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(blockDelay);
        }
    }
}
