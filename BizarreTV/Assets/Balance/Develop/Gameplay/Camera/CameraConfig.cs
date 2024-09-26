using UnityEngine;

namespace Balance
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "BalanceConfigs/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public float TrackSpeed { get; private set; }
    }
}