using System.Collections;
using UnityEngine;

namespace Balance
{
    public class LevelListEntryPoint : EntryPoint
    {
        public override IEnumerator Run()
        {
            RegisterServices();

            Debug.Log("Level list scene loaded");

            yield return null;
        }
    }
}
