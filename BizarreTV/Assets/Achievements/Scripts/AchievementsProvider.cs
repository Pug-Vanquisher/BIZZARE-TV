using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Achievements
{
    public class AchievementsProvider : MonoBehaviour
    {
        public static AchievementsProvider Instance;

        [SerializeField] private AchievementsMenu _menu;
        [SerializeField] private MedalsCounter _medalsCounter;
        [SerializeField] private PopUpAchievementBlock _popUpAchievementBlock; 

        [Space]

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

            _medalsCounter.SetMedals(_stateProvider.StateProxy.GetMedals());

            InitConfigsMap();

            _menu.OnCloseButtonClicked.Subscribe(_ => CloseMenu());
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

            if (isObtained)
                ObtainAchievement(config);

            _stateProvider.StateProxy.SetProgress(fullId, progress, isObtained).Subscribe(_ =>
            {
                _menu.CreateBlocks(project, _configsMap[project], _stateProvider.StateProxy);
            });
        }

        private void ObtainAchievement(AchievementConfig config)
        {
            _popUpAchievementBlock.Open(config);
            CreditReward(config);

            DOVirtual.DelayedCall(5, () => _popUpAchievementBlock.Close());
        }

        private void CreditReward(AchievementConfig config)
        {
            var medalsCount = _medalsCounter.Count + config.Reward;
            var currentViewState = _medalsCounter.CurrentViewState;

            // View.
            _medalsCounter.Open().Subscribe(_ =>
            {
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    var from = _medalsCounter.Count;
                    var to = medalsCount;

                    _medalsCounter.SetMedals(medalsCount);
                    _medalsCounter.ChangeCounterView(from, to).Subscribe(_ =>
                    {
                        DOVirtual.DelayedCall(4f, () => _medalsCounter.SetState(currentViewState));
                    });
                });
            });

            // State.
            _stateProvider.StateProxy.SetMedals(medalsCount);
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

            if (Input.GetKey(KeyCode.K))
            {
                Test1();
            }

            if (Input.GetKey(KeyCode.L))
            {
                Test2();
            }
        }

        [ContextMenu("Test1")]
        private void Test1()
        {
            SetProgress(AchievementProjects.Balance, 1, 3);
        }

        [ContextMenu("Test2")]
        private void Test2()
        {
            SetProgress(AchievementProjects.Balance, 3, 10);
        }

        private void OpenMenu(AchievementProjects project)
        {
            _menu.CreateBlocks(project, _configsMap[project], _stateProvider.StateProxy);
            _menu.Open().Subscribe(_ => Debug.Log("open"));
            _medalsCounter.Open();
        }

        private void CloseMenu()
        {
            _menu.Close().Subscribe(_ => Debug.Log("close"));
            _medalsCounter.Close();
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