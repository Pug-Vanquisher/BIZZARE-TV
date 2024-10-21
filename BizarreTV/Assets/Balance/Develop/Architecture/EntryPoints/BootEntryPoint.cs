using System.Collections;
using UnityEngine;

namespace Balance
{
    public class BootEntryPoint : EntryPoint
    {
        private Storage _storage;

        public override IEnumerator Run()
        {
            DIContainer.RemoveAll();
            RegisterServices();

            yield return LoadData();

            Debug.Log("Boot scene loaded");

            yield return null;
        }

        private IEnumerator LoadData()
        {
            _storage = DIContainer.Resolve<Storage>();
            bool isLoaded = false;

            _storage.Load((res) =>
            {
                if (!res)
                    _storage.DefaultData();

                isLoaded = true;
            });

            yield return new WaitUntil(() => isLoaded);
        }
    }
}
