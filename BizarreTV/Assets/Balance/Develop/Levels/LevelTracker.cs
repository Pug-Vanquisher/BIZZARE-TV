using Balance;
using UnityEngine;

namespace Balance
{
    public class LevelTracker
    {
        private LevelListConfig _levelListConfig;

        private int _currentNumber = 1;

        public LevelTracker()
        {
            _levelListConfig = DIContainer.Resolve<LevelListConfig>();
        }

        public int Current => _currentNumber;
        public float Progress => (_currentNumber - 1f) / (LevelCount - 1f);
        public LevelData CurrentLevelData => _levelListConfig.Levels[_currentNumber - 1];
        public int LevelCount => _levelListConfig.Levels.Count;

        public int IncreaseCurrentNumber()
        {
            int newNumber = _currentNumber + 1;
            return SetCurrentLevelNumber(newNumber);
        }

        public int SetCurrentLevelNumber(int newNumber)
        {
            newNumber = Mathf.Clamp(newNumber, 1, LevelCount);
            _currentNumber = newNumber;

            return newNumber;
        }
    }
}
