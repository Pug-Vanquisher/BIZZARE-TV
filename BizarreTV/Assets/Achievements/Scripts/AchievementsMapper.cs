using System.Collections.Generic;

namespace Achievements
{
    public static class AchievementsMapper
    {
        private static Dictionary<AchievementProjects, int> _projectWeightsMap;

        static AchievementsMapper()
        {
            InitProjectWeightsMap();
        }

        public static int GetId(AchievementProjects project, int id) 
            => _projectWeightsMap[project] * 100 + id;
    
        private static void InitProjectWeightsMap()
        {
            _projectWeightsMap = new();

            _projectWeightsMap[AchievementProjects.Arcanoid] = 1;
            _projectWeightsMap[AchievementProjects.Balance] = 2;
            _projectWeightsMap[AchievementProjects.BomjInDungeon] = 3;
            _projectWeightsMap[AchievementProjects.CyberRun] = 4;
            _projectWeightsMap[AchievementProjects.DeliciousDishes1] = 5;
            _projectWeightsMap[AchievementProjects.DeliciousDishes2] = 6;
            _projectWeightsMap[AchievementProjects.HitTheMole] = 7;
            _projectWeightsMap[AchievementProjects.Jupiter] = 8;
            _projectWeightsMap[AchievementProjects.LoneTower] = 9;
            _projectWeightsMap[AchievementProjects.NuPogodi] = 10;
            _projectWeightsMap[AchievementProjects.Puzzle] = 11;
            _projectWeightsMap[AchievementProjects.Rogalik] = 12;
            _projectWeightsMap[AchievementProjects.RythmGame] = 13;
            _projectWeightsMap[AchievementProjects.ShootingRanch] = 14;
        }
    }
}
