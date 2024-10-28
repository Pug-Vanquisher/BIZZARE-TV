using Balance;
using UnityEngine;

namespace Balance
{
    public class GlobalServiceRegistrar : ServiceRegistrar
    {
        [SerializeField] private DefaultGameData defaultGameData;

        public override void Register()
        {
            RegisterSceneLoader();
            RegisterInput();
            RegisterStorage();

            Debug.Log("Global services registered");
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
            DIContainer.RegisterCofig(defaultGameData);

            Storage storage = new JsonStorage();
            DIContainer.Register(storage);
        }
    }
}
