using UnityEngine;
using TMPro; // ����������� ��� ������������ ���� ��� ������ � TextMeshPro

public class BestTimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI bestTimeText; // ���������� ��� TextMeshPro ������ ���� ����� ���������

    void Start()
    {
        DisplayBestTime();
    }

    public void DisplayBestTime()
    {
        float bestTime = DataTimeStorage.LoadBestTime();
        if (bestTime == 0)
        {
            bestTimeText.text = "Best time: ---";
        }
        else
        {
            string minutes = ((int)bestTime / 60).ToString("00");
            string seconds = (bestTime % 60).ToString("00");
            bestTimeText.text = $"Best time: {minutes}:{seconds}";
        }
    }
}
