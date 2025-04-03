using Utils;

namespace Achievements
{
    public interface IAchievementsStateProvider
    {
        public AchievementsStateProxy StateProxy { get; }

        public Observable<AchievementsStateProxy> LoadGameState();
        public Observable<bool> SaveGameState();
        public Observable<bool> ResetGameState();
    }
}
