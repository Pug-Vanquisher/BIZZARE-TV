using UnityEngine;

namespace Balance
{
    public class DebugKeys : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }


        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    DIContainer.Resolve<SceneLoader>().LoadGameplay();
                }
                else if (Input.GetKeyDown(KeyCode.L))
                {
                    DIContainer.Resolve<SceneLoader>().LoadLevelList();
                }
            }
        }
    }
}
