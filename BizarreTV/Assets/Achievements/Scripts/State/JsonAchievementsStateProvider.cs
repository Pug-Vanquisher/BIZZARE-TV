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

        public Observable<AchievementsStateProxy> LoadGameState()
        {
            if (!File.Exists(_savePath))
            {
                StateProxy = CreateInitalState();
                SaveGameState();
            }
            else
            {
                var json = File.ReadAllText(_savePath);
                var state = JsonUtility.FromJson<AchievementsState>(json);

                StateProxy = new AchievementsStateProxy(state, this);
            }

            return Observable<AchievementsStateProxy>.Return(StateProxy);
        }

        public Observable<bool> SaveGameState()
        {
            var json = JsonUtility.ToJson(StateProxy.State, true);
            File.WriteAllText(_savePath, json);

            return Observable<bool>.Return(true);
        }

        public Observable<bool> ResetGameState()
        {
            StateProxy = CreateInitalState();
            SaveGameState();

            return Observable<bool>.Return(true);
        }

        private AchievementsStateProxy CreateInitalState()
        {
            var gameState = new AchievementsState
            {
                Achievements = new(),
            };

            return new AchievementsStateProxy(gameState, this);
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
