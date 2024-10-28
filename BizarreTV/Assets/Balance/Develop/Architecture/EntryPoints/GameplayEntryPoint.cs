using System.Collections;
using UnityEngine;

namespace Balance
{
    public class GameplayEntryPoint : EntryPoint
    {
        public override IEnumerator Run()
        {
            RegisterServices();

            Debug.Log("Gameplay scene loaded");
            Debug.Log($"Level: {DIContainer.Resolve<LevelTracker>().Current}");

            yield return null;
        }
    }
}
