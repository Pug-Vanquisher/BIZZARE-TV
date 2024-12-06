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
                AudioPlayer audioPlayer = DIContainer.Resolve<AudioPlayer>();
                audioPlayer.Play(audioPlayer.Config.GameOverSound);

                DIContainer.Resolve<SceneLoader>().LoadGameplay();
            }
        }
    }
}
