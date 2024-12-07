using Balance;
using UnityEngine;

namespace Balance
{
    public class GlobalServiceRegistrar : ServiceRegistrar
    {
        [SerializeField] private DefaultGameData _defaultGameData;
        [SerializeField] private LevelListConfig _levelListConfig;
        [SerializeField] private AudioConfig _audioConfig;

        [Space]

        [SerializeField] private AudioSourcer _audioSourcePrefab;

        public override void Register()
        {
            RegisterConfigs();
            RegisterSceneLoader();
            RegisterInput();
            RegisterStorage();
            RegisterLevelTracker();
            RegisterAudio();

            Debug.Log("Global services registered");
        }

        private void RegisterConfigs()
        {
            DIContainer.RegisterCofig(_levelListConfig);
            DIContainer.RegisterCofig(_audioConfig);
        }

        private void RegisterSceneLoader()
        {
            SceneLoader sceneLoader = new SceneLoader();
            DIContainer.Register(sceneLoader);
        }

        private void RegisterInput()
        {
            IInput input = new KeyboardInput();
            DIContainer.Register(input);
        }

        private void RegisterStorage()
        {
            DIContainer.RegisterCofig(_defaultGameData);

            Storage storage = new JsonStorage();
            DIContainer.Register(storage);
        }

        private void RegisterLevelTracker()
        {
            LevelTracker tracker = new LevelTracker();
            DIContainer.Register(tracker);
        }

        private void RegisterAudio()
        {
            DIContainer.Register(_audioSourcePrefab);

            AudioPlayer player = new AudioPlayer();
            DIContainer.Register(player);

            BackgroundMusic backgroundMusic = new BackgroundMusic(player);
            DIContainer.Register(backgroundMusic);
        }
    }
}