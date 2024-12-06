using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balance
{
    public class GameplayMenu : MonoBehaviour
    {
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
    }
}
