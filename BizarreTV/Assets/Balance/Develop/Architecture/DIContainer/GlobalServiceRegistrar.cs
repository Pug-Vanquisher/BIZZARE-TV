using Balance;
using UnityEngine;

namespace Balance
{
    public class GlobalServiceRegistrar : ServiceRegistrar
    {
        [SerializeField] private DefaultGameData _defaultGameData;
        [SerializeField] private LevelListConfig _levelListConfig;

        public override void Register()
        {
            RegisterConfigs();
            RegisterSceneLoader();
            RegisterInput();
            RegisterStorage();
            RegisterLevelTracker();

            Debug.Log("Global services registered");
        }

        private void RegisterConfigs()
        {
            DIContainer.RegisterCofig(_levelListConfig);
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
    }
}
