using UnityEngine;

namespace Balance
{
    public class ServiceManager : MonoBehaviour
    {
        private void Awake()
        {
            RegisterAllServices();
        }

        private void RegisterAllServices()
        {
            ServiceRegistrar[] registrars = FindObjectsOfType<ServiceRegistrar>();

            foreach (ServiceRegistrar registrar in registrars)
            {
                registrar.Register();
            }

            Debug.Log("All services registered");
        }
    }
}
