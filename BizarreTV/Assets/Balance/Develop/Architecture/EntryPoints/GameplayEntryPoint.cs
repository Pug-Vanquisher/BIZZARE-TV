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

            Debug.Log("Gameplay scene loaded");

            yield return null;
        }

        private void CreateLevel()
        {
            GameObject prefab = DIContainer.Resolve<LevelTracker>().CurrentLevelData.Prefab;
            Instantiate(prefab);
        }
    }
}
