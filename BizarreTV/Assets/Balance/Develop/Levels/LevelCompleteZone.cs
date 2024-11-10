using UnityEngine;

namespace Balance
{
    public class LevelCompleteZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                int newNumber = DIContainer.Resolve<LevelTracker>().IncreaseCurrentNumber();
                DIContainer.Resolve<Storage>().SetLastCompletedLevel(newNumber);
                DIContainer.Resolve<SceneLoader>().LoadGameplay();
            }
        }
    }
}
