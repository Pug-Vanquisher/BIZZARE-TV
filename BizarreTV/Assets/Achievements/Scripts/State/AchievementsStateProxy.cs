using Utils;

namespace Achievements
{
    public class AchievementsStateProxy
    {
        private AchievementsState _state;
        private IAchievementsStateProvider _stateProvider;

        public AchievementsState State => _state;

        public AchievementsStateProxy(AchievementsState state, IAchievementsStateProvider stateProvider)
        {
            _state = state;
            _stateProvider = stateProvider;
        }

        public bool IsObtained(int id)
        {
            return GetAchievement(id)?.IsObtained ?? false;
        }

        public float GetProgress(int id)
        {
            return GetAchievement(id)?.Progress ?? -1f;
        }

        public Observable<bool> SetProgress(int id, float progress, bool isObtained = false)
        {
            var achievement = GetAchievement(id);

            if (achievement == null)
            {
                return CreateAchivement(id, progress, isObtained);
            }

            achievement.Progress = progress;
            achievement.IsObtained = isObtained;
            return _stateProvider.SaveState();
        }

        private AchievementState GetAchievement(int id)
        {
            foreach (var achievement in _state.Achievements)
            {
                if (achievement.Id == id)
                    return achievement;
            }
            return null;
        }

        private Observable<bool> CreateAchivement(int id, float progress = 0f, bool isObtained = false)
        {
            _state.Achievements.Add(new AchievementState()
            {
                Id = id,
                Progress = progress,
                IsObtained = isObtained,
            });

            return _stateProvider.SaveState();
        }
    }
}
