using UnityEngine;

namespace Balance
{
    public class LevelCompleteZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                LevelTracker levelTracker = DIContainer.Resolve<LevelTracker>();
                Storage storage = DIContainer.Resolve<Storage>();

                int currentNumber = levelTracker.Current;
                levelTracker.IncreaseCurrentNumber();

                if (storage.GameData.LastCompletedLevel < currentNumber)
                    storage.SetLastCompletedLevel(currentNumber);

                AudioPlayer audioPlayer = DIContainer.Resolve<AudioPlayer>();
                audioPlayer.Play(audioPlayer.Config.FinishReachedSound);

                DIContainer.Resolve<SceneLoader>().LoadGameplay();
            }
        }
    }
}
