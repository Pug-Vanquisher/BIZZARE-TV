using UnityEngine;

public class TriggerStartStop : MonoBehaviour
{
    public enum TriggerType { Start, Stop }
    public TriggerType triggerType;
    private bool activate = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Start)
            {
                if (activate)
                {
                    TimerManager.Instance.ResetTimer();
                    TimerManager.Instance.StartTimer();
                    activate = false;
                }
            }
            else if (triggerType == TriggerType.Stop)
            {
                TimerManager.Instance.StopTimer();
                DataTimeStorage.SaveTime(TimerManager.Instance.elapsedTime);
            }
        }
    }
}