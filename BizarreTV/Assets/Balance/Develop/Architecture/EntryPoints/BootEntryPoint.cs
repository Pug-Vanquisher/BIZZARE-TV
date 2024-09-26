using UnityEngine;

namespace Balance
{
    public class BootEntryPoint : EntryPoint
    {
        private void Awake()
        {
            InstallBindings();

            DIContainer.Resolve<TestService>().Print();
            Debug.Log(DIContainer.Resolve<TestConfig>().Message);
        }
    }
}
