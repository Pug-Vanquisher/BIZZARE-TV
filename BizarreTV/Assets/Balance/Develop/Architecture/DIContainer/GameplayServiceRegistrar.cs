using UnityEngine;

namespace Balance
{
    public class GameplayServiceRegistrar : ServiceRegistrar
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private CameraConfig _cameraConfig;

        public override void Register()
        {
            RegisterConfigs();

            Debug.Log("Gameplay services registered");
        }

        private void RegisterConfigs()
        {
            DIContainer.Register<PlayerConfig>(_playerConfig);
            DIContainer.Register<CameraConfig>(_cameraConfig);
        }
    }
}