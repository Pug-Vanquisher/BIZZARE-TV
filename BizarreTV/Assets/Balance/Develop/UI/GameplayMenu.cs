using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Balance
{
    public class GameplayMenu : MonoBehaviour
    {
        [SerializeField] private Image _audioIcon;
        [SerializeField] private Sprite _audioOnSprite;
        [SerializeField] private Sprite _audioOffSprite;

        public void LoadHomeScene()
        {
            DIContainer.Resolve<BackgroundMusic>().Destroy();
            Coroutines.Destroy();
            SceneManager.LoadScene("MainMenuTest");
        }

        public void LoadLevelListScene()
        {
            DIContainer.Resolve<SceneLoader>().LoadLevelList();
        }

        public void ChangeAudioVolume()
        {
            float volume = DIContainer.Resolve<AudioPlayer>().ChangeVolume();
            UpdateView(volume);
        }

        public void UpdateView(float volume)
        {
            _audioIcon.sprite = volume > 0 ? _audioOnSprite : _audioOffSprite;
        }
    }
}
