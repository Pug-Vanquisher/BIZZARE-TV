using Utils;

namespace Achievements
{
    public interface IAchievementsStateProvider
    {
        public AchievementsStateProxy StateProxy { get; }

        public Observable<AchievementsStateProxy> LoadState();
        public Observable<bool> SaveState();
        public Observable<bool> ResetState();
    }
}
