using Balance;
using UnityEngine;

namespace Balance
{
    public class GlobalServiceRegistrar : ServiceRegistrar
    {
        [SerializeField] private DefaultGameData defaultGameData;

        public override void Register()
        {
            RegisterInput();
            RegisterStorage();

            Debug.Log("Global services registered");
        }

        private void RegisterInput()
        {
            IInput input = new KeyboardInput();
            DIContainer.Register<IInput>(input);
        }

        private void RegisterStorage()
        {
            DIContainer.RegisterCofig(defaultGameData);

            Storage storage = new JsonStorage();
            DIContainer.Register<Storage>(storage);
        }
    }
}
