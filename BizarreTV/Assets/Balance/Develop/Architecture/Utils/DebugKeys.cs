using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balance
{
    public class DebugKeys : MonoBehaviour
    {
        private static DebugKeys Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

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
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene("MainMenuTest");
                }
            }
        }
    }
}
