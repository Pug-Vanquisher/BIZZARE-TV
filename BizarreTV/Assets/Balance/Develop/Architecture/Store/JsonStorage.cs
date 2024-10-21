using Balance;
using System;
using System.IO;
using UnityEngine;

namespace Balance
{
    public class JsonStorage : Storage
    {
        public override GameData GameData { get; protected set; }

        private string _path;

        public JsonStorage() : base()
        {
            BuildPath();
        }

        public override void Load(Action<bool> callback = null)
        {
            if (!File.Exists(_path))
            {
                Debug.Log("Load data error: file not exist");

                callback?.Invoke(false);
                return;
            }

            try
            {
                string json = File.ReadAllText(_path);
                GameData = JsonUtility.FromJson<GameData>(json);

                callback?.Invoke(true);

                Debug.Log("Load data complete");
            }
            catch { Debug.Log("Load data error"); }
        }

        public override void Save()
        {
            try
            {
                string json = JsonUtility.ToJson(GameData, true);
                File.WriteAllText(_path, json);

                Debug.Log("Save data complete");
            }
            catch { Debug.Log("Save data error"); }
        }

        private void BuildPath()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _path = Path.Combine (Application.persistentDataPath, "balanceData.json");
#else
            _path = Path.Combine(Application.dataPath, "balanceData.json");
#endif
        }
    }
}
