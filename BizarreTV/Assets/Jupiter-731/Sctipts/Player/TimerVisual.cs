using Jupiter731;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerVisual : MonoBehaviour
{
    [SerializeField] Timer timerPhy;
    [SerializeField] Image fillEnergy;
    [SerializeField] float maxSecEnergy = 60;

    private void FixedUpdate()
    {
        if (fillEnergy != null)
            Refresh();
    }

    void Refresh()
    {
        fillEnergy.fillAmount = timerPhy.CurrTime / maxSecEnergy;
    }
}
