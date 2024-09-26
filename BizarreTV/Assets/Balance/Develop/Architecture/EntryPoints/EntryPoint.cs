using UnityEngine;

namespace Balance
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private DIInstaller _installer;

        protected void InstallBindings()
        {
            _installer.InstallBindings();
        }
    }
}
