using Jupiter731;
using TMPro;
using UnityEngine;

public class TimerVisual : MonoBehaviour
{
    [SerializeField] Timer timerPhy;
    [SerializeField] TextMeshProUGUI energyForSec;

    private void FixedUpdate()
    {
        if (energyForSec != null)
            Refresh();
    }

    void Refresh()
    {
        energyForSec.text = string.Format("{0:f2}", timerPhy.CurrTime);
    }
}
