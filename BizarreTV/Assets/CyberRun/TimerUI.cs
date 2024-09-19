using UnityEngine;
using TMPro; // ����������� ��� ������������ ���� ��� ������ � TextMeshPro

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText; // ���������� ���� ��� TextMeshPro ������ ����� ���������

    void Update()
    {
        if (TimerManager.Instance != null)
        {
            float time = TimerManager.Instance.elapsedTime;
            // ����������� ����� � ������:�������
            string minutes = ((int)time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            timerText.text = $"{minutes}:{seconds}";
        }
    }
}