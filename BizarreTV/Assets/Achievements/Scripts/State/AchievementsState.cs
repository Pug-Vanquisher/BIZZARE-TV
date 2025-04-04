using System;
using System.Collections.Generic;

namespace Achievements
{
    [Serializable]
    public class AchievementsState
    {
        public int MedalsCount;
        public List<AchievementState> Achievements;
    }
}
