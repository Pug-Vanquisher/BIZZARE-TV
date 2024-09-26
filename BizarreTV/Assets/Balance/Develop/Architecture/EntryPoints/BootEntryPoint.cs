using System.Collections;
using UnityEngine;

namespace Balance
{
    public class BootEntryPoint : EntryPoint
    {
        public override IEnumerator Run()
        {
            DIContainer.RemoveAll();
            InstallBindings();

            DIContainer.Resolve<TestService>().Print();
            Debug.Log(DIContainer.Resolve<TestConfig>().Message);

            Debug.Log("Boot scene loaded");

            yield return null;
        }
    }
}
