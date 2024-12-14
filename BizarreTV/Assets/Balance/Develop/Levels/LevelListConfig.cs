using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balance
{
    [CreateAssetMenu(fileName = "LevelListConfig", menuName = "BalanceConfigs/LevelList")]
    public class LevelListConfig : ScriptableObject
    {
        public List<LevelData> Levels = new();

        // Sets the level numbers based on the location in the list.
        [ContextMenu("Set numbers")]
        private void SetNumbers()
        {
            for (int i = 0; i < Levels.Count; i++)
            {
                int number = i + 1;
                Levels[i].SetNumber(number);
            }
        }
    }

    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public int Number { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void SetNumber(int number)
        {
            Number = number;
        }
    }

}
