using System.Collections;
using UnityEngine;

namespace Balance
{
    public abstract class EntryPoint : MonoBehaviour
    {
        [SerializeField] private DIInstaller _installer;

        public abstract IEnumerator Run();

        protected void InstallBindings() => _installer.InstallBindings();
    }
}
