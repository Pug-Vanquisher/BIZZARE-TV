using System.Collections;
using UnityEngine;

namespace Balance
{
    public class BootEntryPoint : EntryPoint
    {
        private Storage _storage;

        public override IEnumerator Run()
        {
            // Сброс всех ранее зарегистрированных сервисов
            DIContainer.RemoveAll();

            // Зарегистрировать все сервисы на сцене
            RegisterSceneServices();

            yield return LoadData();

            InitLevelTracker();

            Debug.Log("Boot scene loaded");

            LoadGameplay();
            InitAudio();

            yield return null;
        }

        private void RegisterSceneServices()
        {
            ServiceRegistrar[] registrars = FindObjectsOfType<ServiceRegistrar>();
            foreach (var registrar in registrars)
            {
                registrar.Register();
            }

            Debug.Log("Scene services registered");
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

        private void InitLevelTracker()
        {
            int number = _storage.GameData.LastCompletedLevel + 1;
            DIContainer.Resolve<LevelTracker>().SetCurrentLevelNumber(number);
        }

        private void InitAudio()
        {
            DIContainer.Resolve<AudioPlayer>().SetVolume(_storage.GameData.AudioVolume);
        }

        private void LoadGameplay()
        {
            DIContainer.Resolve<SceneLoader>().LoadGameplay();
            DIContainer.Resolve<BackgroundMusic>().Start();
        }
    }
}
