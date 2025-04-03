using System;

namespace Achievements
{
    [Serializable]
    public class AchievementState
    {
        public int Id;
        public float Progress;
        public bool IsObtained;
    }
}
