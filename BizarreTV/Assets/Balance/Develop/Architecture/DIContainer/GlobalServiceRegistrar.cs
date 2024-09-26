using UnityEngine;

namespace Balance
{
    public class GlobalServiceRegistrar : ServiceRegistrar
    {
        [SerializeField] private TestConfig _testConfig;

        public override void Register()
        {
            RegisterInput();

            Debug.Log("Global services registered");
        }

        private void RegisterInput()
        {
            IInput input = new KeyboardInput();
            DIContainer.Register<IInput>(input);
        }
    }
}
