using System.Collections.Generic;
using UnityEngine;

namespace Achievements
{
    public class AchievementsProvider : MonoBehaviour
    {
        public static AchievementsProvider Instance;

        [SerializeField] private AchievementsMenu _menu;
        [SerializeField] private AchievementsConfig _config;

        private IAchievementsStateProvider _stateProvider;

        private Dictionary<AchievementProjects, AchievementConfig[]> _configsMap;

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

            _stateProvider = new JsonAchievementsStateProvider();
            _stateProvider.LoadGameState();

            InitConfigsMap();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                CloseMenu();
            }

            if (Input.GetKey(KeyCode.O))
            {
                OpenMenu(AchievementProjects.Balance);
            }
        }

        private void OpenMenu(AchievementProjects project)
        {
            _menu.CreateBlocks(_configsMap[project]);
            _menu.Open().Subscribe(_ => Debug.Log("open"));
        }

        private void CloseMenu()
        {
            _menu.Close().Subscribe(_ => Debug.Log("close"));
        }

        private void InitConfigsMap()
        {
            _configsMap = new();

            _configsMap[AchievementProjects.Arcanoid] = _config.Arcanoid;
            _configsMap[AchievementProjects.Balance] = _config.Balance;
            _configsMap[AchievementProjects.BomjInDungeon] = _config.BomjInDungeon;
            _configsMap[AchievementProjects.CyberRun] = _config.CyberRun;
            _configsMap[AchievementProjects.DeliciousDishes1] = _config.DeliciousDishes1;
            _configsMap[AchievementProjects.DeliciousDishes2] = _config.DeliciousDishes2;
            _configsMap[AchievementProjects.HitTheMole] = _config.HitTheMole;
            _configsMap[AchievementProjects.Jupiter] = _config.Jupiter;
            _configsMap[AchievementProjects.LoneTower] = _config.LoneTower;
            _configsMap[AchievementProjects.NuPogodi] = _config.NuPogodi;
            _configsMap[AchievementProjects.Puzzle] = _config.Puzzle;
            _configsMap[AchievementProjects.Rogalik] = _config.Rogalik;
            _configsMap[AchievementProjects.RythmGame] = _config.RythmGame;
            _configsMap[AchievementProjects.ShootingRanch] = _config.ShootingRanch;
        }
    }
}