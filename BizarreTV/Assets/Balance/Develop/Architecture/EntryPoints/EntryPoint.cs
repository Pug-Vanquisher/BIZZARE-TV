using System.Collections;
using UnityEngine;

namespace Balance
{
    public abstract class EntryPoint : MonoBehaviour
    {
        [SerializeField] private DIInstaller[] _installers;

        public abstract IEnumerator Run();

        protected void InstallBindings()
        {
            foreach (DIInstaller installer in _installers)
            {
                installer.InstallBindings();
            }
        }
    }
}
