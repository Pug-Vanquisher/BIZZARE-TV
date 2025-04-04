using System.IO;
using UnityEngine;
using Utils;

namespace Achievements
{
    public class JsonAchievementsStateProvider : IAchievementsStateProvider
    {
        private const string STATE_KEY = "ACHIEVEMENTS_STATE";

        private string _savePath;

        public AchievementsStateProxy StateProxy { get; private set; }

        public JsonAchievementsStateProvider()
        {
            _savePath = GetPath();
        }

        public Observable<AchievementsStateProxy> LoadState()
        {
            if (!File.Exists(_savePath))
            {
                StateProxy = CreateInitalState();
                SaveState();
            }
            else
            {
                var json = File.ReadAllText(_savePath);
                var state = JsonUtility.FromJson<AchievementsState>(json);

                StateProxy = new AchievementsStateProxy(state, this);
            }

            return Observable<AchievementsStateProxy>.Return(StateProxy);
        }

        public Observable<bool> SaveState()
        {
            var json = JsonUtility.ToJson(StateProxy.State, true);
            File.WriteAllText(_savePath, json);

            return Observable<bool>.Return(true);
        }

        public Observable<bool> ResetState()
        {
            StateProxy = CreateInitalState();
            SaveState();

            return Observable<bool>.Return(true);
        }

        private AchievementsStateProxy CreateInitalState()
        {
            var state = new AchievementsState
            {
                MedalsCount = 0,
                Achievements = new(),
            };

            return new AchievementsStateProxy(state, this);
        }

        private string GetPath()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return Path.Combine (Application.persistentDataPath, $"{STATE_KEY}.json");
#else
            return Path.Combine(Application.dataPath, $"{STATE_KEY}.json");
#endif
        }
    }
}
