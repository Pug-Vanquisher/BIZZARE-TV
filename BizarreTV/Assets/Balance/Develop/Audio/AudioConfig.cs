using UnityEngine;

namespace Balance
{
    [CreateAssetMenu(fileName = "Audio", menuName = "BalanceConfigs/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        [field: SerializeField] public AudioClip BackgroundMusic {  get; private set; }
        [field: SerializeField] public AudioClip FinishReachedSound { get; private set; }
        [field: SerializeField] public AudioClip GameOverSound { get; private set; }
        [field: SerializeField] public AudioClip PlayerMovementSound { get; private set; }
    }
}
