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
        private int LevelCount => _levelListConfig.Levels.Count;

        public void IncreaseCurrentNumber()
        {
            int newNumber = _currentNumber + 1;
            SetCurrentLevelNumber(newNumber);
        }

        public void SetCurrentLevelNumber(int newNumber)
        {
            newNumber = Mathf.Clamp(newNumber, 1, LevelCount);
            _currentNumber = newNumber;
        }
    }
}
