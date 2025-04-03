using DG.Tweening;
using UnityEngine;

namespace Achievements
{
	public class AchievementsProvider : MonoBehaviour
    {
        public static AchievementsProvider Instance;

        [SerializeField] private AchievementsMenu _menu;

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
                OpenMenu();
            }
        }

        [ContextMenu("Open Menu")]
        private void OpenMenu()
        {
            _menu.CreateBlocks(10);
            _menu.Open().Subscribe(_ => Debug.Log("open"));
        }

        [ContextMenu("Close Menu")]
        private void CloseMenu()
        {
            _menu.Close().Subscribe(_ => Debug.Log("close"));
        }
    }
}