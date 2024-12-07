using System.Collections;
using UnityEngine;

namespace Pogodi
{
    public class Timer : MonoBehaviour
    {
        // Статический метод для вызова таймера
        public static void StartTimer(MonoBehaviour caller, float delay, System.Action callback)
        {
            // Запуск корутины для таймера
            caller.StartCoroutine(TimerCoroutine(delay, callback));
        }

        // Корутин для ожидания
        private static IEnumerator TimerCoroutine(float delay, System.Action callback)
        {
            yield return new WaitForSeconds(delay); // Ожидание указанного времени
            callback?.Invoke(); // Вызов переданной функции по завершению таймера
        }
    }
}