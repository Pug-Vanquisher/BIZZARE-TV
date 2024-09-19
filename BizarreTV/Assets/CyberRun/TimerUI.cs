using UnityEngine;
using TMPro; // Используйте это пространство имен для работы с TextMeshPro

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Перетащите сюда ваш TextMeshPro объект через инспектор

    void Update()
    {
        if (TimerManager.Instance != null)
        {
            float time = TimerManager.Instance.elapsedTime;
            // Форматируем время в минуты:секунды
            string minutes = ((int)time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            timerText.text = $"{minutes}:{seconds}";
        }
    }
}