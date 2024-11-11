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

                DIContainer.Resolve<SceneLoader>().LoadGameplay();
            }
        }
    }
}
