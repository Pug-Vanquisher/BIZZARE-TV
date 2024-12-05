using UnityEngine;

namespace Rogalik
{
    public class EnemyHP : MonoBehaviour
    {
        public int maxHealth = 10; // ��������� �������� ��������
        public int currentHealth; // ������� �������� ��������
        public GameObject[] powerUps; // ������ ������� �������� ��������
        public float dropChance = 0.2f; // ���� ��������� �������� (20%)

        void Start()
        {
            currentHealth = maxHealth;
        }

        // ����� ��� ��������� �����
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            SoundManager.Instance.PlaySound(4);
            if (currentHealth <= 0)
            {
                DropPowerUp();
                gameObject.SetActive(false);
            }
        }

        // ����� ��� ������� ��������
        void DropPowerUp()
        {
            if (Random.value <= dropChance) // ���������, ������� �� ��������
            {
                if (powerUps.Length > 0)
                {
                    int index = Random.Range(0, powerUps.Length); // �������� ��������� �������� �� �������
                    Instantiate(powerUps[index], transform.position, Quaternion.identity); // ������� �������� �� ������� �����
                }
            }
        }
    }


}