using UnityEngine;

namespace Rogalik
{
    public class EnemyHP : MonoBehaviour
    {
        public int maxHealth = 10; // Начальное значение здоровья
        public int currentHealth; // Текущее значение здоровья
        public GameObject[] powerUps; // Массив игровых объектов усиления
        public float dropChance = 0.2f; // Шанс выпадения усиления (20%)

        void Start()
        {
            currentHealth = maxHealth;
        }

        // Метод для получения урона
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

        // Метод для выпуска усиления
        void DropPowerUp()
        {
            if (Random.value <= dropChance) // Проверяем, выпадет ли усиление
            {
                if (powerUps.Length > 0)
                {
                    int index = Random.Range(0, powerUps.Length); // Выбираем случайное усиление из массива
                    Instantiate(powerUps[index], transform.position, Quaternion.identity); // Создаем усиление на позиции врага
                }
            }
        }
    }


}