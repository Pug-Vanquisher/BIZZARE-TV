using Balance;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Achievements
{
	public class AchievementsProvider : MonoBehaviour
    {
        public static AchievementsProvider Instance;

        [SerializeField] private AchievementsMenu _menu;
        [SerializeField] private AchievementsConfig _config;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            if (Instance != null) return;

            var prefab = Resources.Load<AchievementsProvider>("AchievementsProvider");
            Instantiate(prefab);
        }

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
            if (Input.GetKey(KeyCode.Escape))
            {
                CloseMenu();
            }

            if (Input.GetKey(KeyCode.O))
            {
                OpenMenu(_config.Balance);
            }
        }

        private void OpenMenu(AchievementConfig[] configs)
        {
            _menu.CreateBlocks(configs);
            _menu.Open().Subscribe(_ => Debug.Log("open"));
        }

        private void CloseMenu()
        {
            _menu.Close().Subscribe(_ => Debug.Log("close"));
        }
    }
}