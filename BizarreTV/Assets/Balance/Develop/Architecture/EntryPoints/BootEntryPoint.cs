using System.Collections;
using UnityEngine;

namespace Balance
{
    public class BootEntryPoint : EntryPoint
    {
        public override IEnumerator Run()
        {
            DIContainer.RemoveAll();
            RegisterServices();

            Debug.Log("Boot scene loaded");

            yield return null;
        }
    }
}
