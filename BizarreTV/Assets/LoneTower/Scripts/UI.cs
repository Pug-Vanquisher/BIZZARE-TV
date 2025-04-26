using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LT
{
    public class UI : MonoBehaviour
    {
        public TMP_Text score;
        public TMP_Text points;
        public Transform upgradeRect;
        public GameObject upgradeWindowPrefab;
        private void Start()
        {
            string[] keys = new string[GameObject.Find("Game").GetComponent<Game>().upgrades.Keys.Count];
            GameObject.Find("Game").GetComponent<Game>().upgrades.Keys.CopyTo(keys, 0);
            foreach (string key in keys)
            {
                var a = Instantiate(upgradeWindowPrefab, upgradeRect);
                a.name = key;
            }
        }

        void Update()
        {
            score.text = GameObject.Find("Game").GetComponent<Game>().score.ToString();
            points.text = GameObject.Find("Game").GetComponent<Game>().points.ToString();
        }
    }
}
