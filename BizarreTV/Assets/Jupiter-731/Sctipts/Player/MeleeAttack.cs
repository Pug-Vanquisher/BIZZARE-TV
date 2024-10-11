using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class MeleeAttack : MonoBehaviour
    {
        [SerializeField] KeyCode meleeAttackKey = KeyCode.Mouse1;
        [SerializeField] float attackRange = 1.0f;
        [SerializeField] float attackAngle = 45f;
        [SerializeField] float damage = 10f;
        [SerializeField] LayerMask enemyLayer;
        [SerializeField] Transform attackPoint;


        void Update()
        {
            if (Input.GetKeyDown(meleeAttackKey))
            {
                Hit();
            }
        }
        private void Hit()
        {
            if (attackPoint == null)
            {
                Debug.LogWarning("Attack Point �� ���������.");
                return;
            }
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            Debug.Log("������ �������: " + hitEnemies.Length);
            foreach (Collider2D enemy in hitEnemies)
            {
                Vector2 directionToEnemy = (enemy.transform.position - attackPoint.position).normalized;
                Vector2 characterForward = GetCharacterForward();
                float angleToEnemy = Vector2.Angle(characterForward, directionToEnemy);
                Debug.Log($"�������� �����: {enemy.name}, ����: {angleToEnemy}");
                if (angleToEnemy < attackAngle)
                {
                    BaseUnit enemyUnit = enemy.GetComponent<BaseUnit>();
                    if (enemyUnit != null)
                    {
                        enemyUnit.TakeDamage(damage);
                        Debug.Log("��������� �� �����: " + enemy.name);
                    }
                }
            }
        }

        Vector2 GetCharacterForward()
        {
            // ��������������, ��� �������� ������� ������ �� ���������
            // ���� �������� �������, ����� ������������ scale.x ��� ������ ������ ����������� �����������
            Vector2 forward = Vector2.right;
            if (transform.localScale.x < 0)
            {
                forward = Vector2.left;
            }
            return forward;
        }

        void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);

            // ������ ����� �����
            Vector2 forward = GetCharacterForward() * attackRange;
            Vector2 right = Quaternion.Euler(0, 0, attackAngle) * forward;
            Vector2 left = Quaternion.Euler(0, 0, -attackAngle) * forward;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(attackPoint.position, attackPoint.position + (Vector3)right);
            Gizmos.DrawLine(attackPoint.position, attackPoint.position + (Vector3)left);
        }
    }

}
