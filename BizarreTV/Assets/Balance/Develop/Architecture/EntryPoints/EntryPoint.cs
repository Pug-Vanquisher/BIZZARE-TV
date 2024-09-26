using System.Collections;
using UnityEngine;

namespace Balance
{
    public abstract class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ServiceRegistrar[] _serviceRegistrars;

        public abstract IEnumerator Run();

        protected void RegisterServices()
        {
            foreach (ServiceRegistrar registrar in _serviceRegistrars)
            {
                registrar.Register();
            }
        }
    }
}
