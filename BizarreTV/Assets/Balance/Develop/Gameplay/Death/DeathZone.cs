using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balance
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                DIContainer.Resolve<SceneLoader>().LoadGameplay();
            }
        }
    }
}