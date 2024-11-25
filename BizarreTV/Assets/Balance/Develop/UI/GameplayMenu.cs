using UnityEngine;

namespace Balance
{
    public class GameplayMenu : MonoBehaviour
    {
        public void LoadHomeScene()
        {
            DIContainer.Resolve<BackgroundMusic>().Destroy();
        }

        public void LoadLevelListScene()
        {
            DIContainer.Resolve<SceneLoader>().LoadLevelList();
        }
    }
}
