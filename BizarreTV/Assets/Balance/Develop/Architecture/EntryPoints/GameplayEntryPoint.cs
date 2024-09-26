using System.Collections;
using UnityEngine;

namespace Balance
{
    public class GameplayEntryPoint : EntryPoint
    {
        public override IEnumerator Run()
        {
            Debug.Log("Gameplay scene loaded");

            yield return null;
        }
    }
}
