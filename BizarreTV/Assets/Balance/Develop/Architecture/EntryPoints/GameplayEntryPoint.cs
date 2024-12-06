using System.Collections;
using UnityEngine;

namespace Balance
{
    public class GameplayEntryPoint : EntryPoint
    {
        public override IEnumerator Run()
        {
            RegisterServices();

            CreateLevel();
            UpdateUIView();

            Debug.Log("Gameplay scene loaded");

            yield return null;
        }

        private void CreateLevel()
        {
            GameObject prefab = DIContainer.Resolve<LevelTracker>().CurrentLevelData.Prefab;
            Instantiate(prefab);
        }

        private void UpdateUIView()
        {
            float volume = DIContainer.Resolve<AudioPlayer>().Volume;
            Object.FindObjectOfType<GameplayMenu>().UpdateView(volume);
        }
    }
}
