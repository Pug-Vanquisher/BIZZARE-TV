using Balance;
using System;
using UnityEngine;

namespace Balance
{
    public abstract class Storage
    {
        public abstract GameData GameData { get; protected set; }
        private DefaultGameData _defaultGameData;
        
        public Storage()
        {
            _defaultGameData = DIContainer.Resolve<DefaultGameData>();
        }

        public abstract void Save();
        public abstract void Load(Action<bool> callback = null);

        public void DefaultData()
        {
            GameData = GameObject.Instantiate(_defaultGameData).GameData;
            Save();
        }

        public void SetAudioVolume(float volume)
        {
            GameData.AudioVolume = volume;
            Save();
        }

        public void SetLastCompletedLevel(int number)
        {
            GameData.LastCompletedLevel = number;
            Save();
        }
    }
}
