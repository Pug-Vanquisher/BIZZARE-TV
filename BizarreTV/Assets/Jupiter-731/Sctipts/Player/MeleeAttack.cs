using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Jupiter731
{
    public class MeleeAttack : MonoBehaviour
    {
        [SerializeField] float attackRange = 1.0f;
        [SerializeField] float damage = 10f;
        [SerializeField] float timeToStrike;
        [SerializeField, Range(30, 90)] float attackAngle = 45f;
        [SerializeField] LayerMask[] enemyLayer;
        [SerializeField] KeyCode meleeAttackKey = KeyCode.Mouse1;
        [SerializeField] Transform attackPoint;
        [SerializeField] BaseAnimator baseAnimator;
        [SerializeField] TrailRenderer trailRenderer;
        [SerializeField] MeleeBlock block;
        private float _strikeTimer;


        void Update()
        {
            if (_strikeTimer + 0.1f >= timeToStrike - 0.2f)
            {
                trailRenderer.forceRenderingOff = true;
            }
            else if (_strikeTimer > 0.08f && _strikeTimer < timeToStrike)
            {
                trailRenderer.forceRenderingOff = false;
            }
            if (Input.GetKey(meleeAttackKey) && _strikeTimer > timeToStrike)
            {
                Hit();
                block.BlockProjectiles(attackPoint, attackRange);
                _strikeTimer = 0;
            }
            else
            {
                _strikeTimer += Time.deltaTime;
            }
        }
        private void Hit()
        {
            baseAnimator.PlayAnimations();
            if (attackPoint == null)
            {
                Debug.LogWarning("Attack Point не назначена.");
                return;
            }
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer[0]);
            hitEnemies.AddRange(Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer[1]));
            Debug.Log("Врагов найдено: " + hitEnemies.Length);
            foreach (Collider2D enemy in hitEnemies)
            {
                Vector2 directionToEnemy = ((Vector3)enemy.attachedRigidbody.position - attackPoint.position).normalized;
                Vector2 characterForward = transform.right;
                float angleToEnemy = Vector2.Angle(characterForward, directionToEnemy);
                Debug.Log($"Проверка врага: {enemy.name}, угол: {angleToEnemy}" + " " + attackAngle);
                if (angleToEnemy < attackAngle)
                {
                    BaseUnit enemyUnit = enemy.GetComponent<BaseUnit>();
                    if (enemyUnit != null)
                    {
                        enemyUnit.TakeDamage(damage);
                        Debug.Log("Попадание по врагу: " + enemy.name + damage);
                    }
                }
            }
        }

        //Vector2 GetCharacterForward()
        //{
        //    // Предполагается, что персонаж смотрит вправо по умолчанию
        //    // Если персонаж повёрнут, можно использовать scale.x или другой способ определения направления
        //    Vector2 forward = Vector2.right;
        //    if (transform.localScale.x < 0)
        //    {
        //        forward = Vector2.left;
        //    }
        //    return forward;
        //}

        void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);

            // Рисуем конус атаки
            Vector2 forward = transform.right * attackRange;
            Vector2 right = Quaternion.Euler(0, 0, attackAngle) * forward;
            Vector2 left = Quaternion.Euler(0, 0, -attackAngle) * forward;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(attackPoint.position, attackPoint.position + (Vector3)right);
            Gizmos.DrawLine(attackPoint.position, attackPoint.position + (Vector3)left);
        }
    }

}
