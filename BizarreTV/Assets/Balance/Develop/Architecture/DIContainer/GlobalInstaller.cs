using UnityEngine;

namespace Balance
{
    public class GlobalInstaller : DIInstaller
    {
        [SerializeField] private TestConfig _testConfig;

        public override void InstallBindings()
        {
            DIContainer.Register(typeof(TestService), new TestService());
            DIContainer.Register(typeof(TestConfig), _testConfig);
        }
    }
}
