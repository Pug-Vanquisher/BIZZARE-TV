using UnityEngine;

namespace Balance
{
    public class GameplayMenu : MonoBehaviour
    {
        public void LoadHomeScene()
        {

        }

        public void LoadLevelListScene()
        {
            DIContainer.Resolve<SceneLoader>().LoadLevelList();
        }
    }
}
