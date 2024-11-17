using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class PrefabStorage: MonoBehaviour
    {
        public static List<GameObject> enemiesPrefabs;
        public List<GameObject> LocalenemiesPrefabs;
        public void Start()
        {
            enemiesPrefabs = LocalenemiesPrefabs;
        }

    }
}