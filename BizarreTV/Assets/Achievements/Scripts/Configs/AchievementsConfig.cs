using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Achievements
{
    [CreateAssetMenu(fileName = "AchievementsConfig", menuName = "Achievements/New Achievements Config")]
    public class AchievementsConfig : ScriptableObject
    {
        [field: SerializeField] public AchievementConfig[] Arcanoid { get; private set; }
        [field: SerializeField] public AchievementConfig[] Balance { get; private set; }
        [field: SerializeField] public AchievementConfig[] BomjInDungeon { get; private set; }
        [field: SerializeField] public AchievementConfig[] CyberRun { get; private set; }
        [field: SerializeField] public AchievementConfig[] DeliciousDishes1 { get; private set; }
        [field: SerializeField] public AchievementConfig[] DeliciousDishes2 { get; private set; }
        [field: SerializeField] public AchievementConfig[] HitTheMole { get; private set; }
        [field: SerializeField] public AchievementConfig[] Jupiter { get; private set; }
        [field: SerializeField] public AchievementConfig[] LoneTower { get; private set; }
        [field: SerializeField] public AchievementConfig[] NuPogodi { get; private set; }
        [field: SerializeField] public AchievementConfig[] Puzzle { get; private set; }
        [field: SerializeField] public AchievementConfig[] Rogalik { get; private set; }
        [field: SerializeField] public AchievementConfig[] RythmGame { get; private set; }
        [field: SerializeField] public AchievementConfig[] ShootingRanch { get; private set; }

        private void OnValidate()
        {
            ValidateId(Arcanoid, nameof(Arcanoid));
            ValidateId(Balance, nameof(Balance));
            ValidateId(BomjInDungeon, nameof(BomjInDungeon));
            ValidateId(CyberRun, nameof(CyberRun));
            ValidateId(DeliciousDishes1, nameof(DeliciousDishes1));
            ValidateId(DeliciousDishes2, nameof(DeliciousDishes2));
            ValidateId(HitTheMole, nameof(HitTheMole));
            ValidateId(Jupiter, nameof(Jupiter));
            ValidateId(LoneTower, nameof(LoneTower));
            ValidateId(NuPogodi, nameof(NuPogodi));
            ValidateId(Puzzle, nameof(Puzzle));
            ValidateId(Rogalik, nameof(Rogalik));
            ValidateId(RythmGame, nameof(RythmGame));
            ValidateId(ShootingRanch, nameof(ShootingRanch));
        }

        private void ValidateId(AchievementConfig[] configs, string name)
        {
            if (configs.Length == 0) return;

            for (int i = 0; i < configs.Length; i++)
            {
                for (int j = i+1; j < configs.Length; j++)
                {
                    if (configs[i].Id == configs[j].Id)
                        throw new System.ArgumentException($"Id {configs[i].Id} is repeated in {name} achievements config");
                }
            }
        }
    }
}
