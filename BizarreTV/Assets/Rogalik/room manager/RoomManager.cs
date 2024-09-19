using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] enemies; // Массив врагов в комнате
    public GameObject exitPortal; // Выходной портал
    private bool isActive = false; // Флаг, указывающий на активацию комнаты

    private void Start()
    {
        exitPortal.SetActive(false); // Изначально выходной портал неактивен
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            isActive = true; // Помечаем комнату как активированную
            StartCoroutine(ActivateEnemiesWithDelay(1.0f)); // Активируем врагов с задержкой
            StartCoroutine(CheckEnemies()); // Начинаем проверку состояния врагов
        }
    }

    IEnumerator ActivateEnemiesWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Ждем заданную задержку
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true); // Активируем каждого врага
        }
    }

    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f); // Небольшая задержка перед проверкой

        while (true)
        {
            bool allEnemiesDefeated = true;
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null && enemy.activeSelf)
                {
                    allEnemiesDefeated = false; // Если хотя бы один враг активен, комната еще не очищена
                    break;
                }
            }

            if (allEnemiesDefeated)
            {
                exitPortal.SetActive(true); // Активируем выходной портал, если все враги уничтожены
                DestroyEnemies(); // Уничтожаем врагов как игровые объекты
                break; // Выходим из цикла
            }

            yield return new WaitForSeconds(0.5f); // Проверяем снова через некоторое время
        }
    }

    void DestroyEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); // Уничтожаем врага как игровой объект
        }
    }
}