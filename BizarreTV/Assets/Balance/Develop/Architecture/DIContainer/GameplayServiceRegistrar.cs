using UnityEngine;

namespace Balance
{
    public class GameplayServiceRegistrar : ServiceRegistrar
    {
        [SerializeField] private PlayerConfig playerConfig;

        public override void Register()
        {
            RegisterConfigs();
        }

        private void RegisterConfigs()
        {
            DIContainer.Register<PlayerConfig>(playerConfig);

            Debug.Log("Gameplay services registered");
        }
    }
}
