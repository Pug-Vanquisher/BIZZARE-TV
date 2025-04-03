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
    }
}
