using System.Collections.Generic;
using System.Linq;
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
            _stateProvider.LoadState();

            InitConfigsMap();
        }

        public bool IsObtained(AchievementProjects project, int id)
        {
            var fullId = AchievementsMapper.GetId(project, id);
            return _stateProvider.StateProxy.IsObtained(fullId);
        }

        public float GetProgress(AchievementProjects project, int id)
        {
            var fullId = AchievementsMapper.GetId(project, id);
            return _stateProvider.StateProxy.GetProgress(fullId);
        }

        public void SetProgress(AchievementProjects project, int id, float progress, bool checkObtained = true)
        {
            if (checkObtained && IsObtained(project, id)) return;
            
            var config = GetConfig(project, id);
            progress = Mathf.Clamp(progress, 0, config.Progress);

            var fullId = AchievementsMapper.GetId(project, id);
            var isObtained = progress.Equals(config.Progress);

            _stateProvider.StateProxy.SetProgress(fullId, progress, isObtained).Subscribe(_ =>
            {
                _menu.CreateBlocks(project, _configsMap[project], _stateProvider.StateProxy);
            });
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
            _menu.CreateBlocks(project, _configsMap[project], _stateProvider.StateProxy);
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
    
        private AchievementConfig GetConfig(AchievementProjects project, int id)
        {
            return _configsMap[project].FirstOrDefault(a => a.Id == id);
        }
    }
}